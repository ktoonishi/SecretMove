Imports System.IO
Imports System.IO.Ports
Imports System.Collections.Generic
Imports System.Management
Imports System.Threading.Thread
Imports System.Text


Public Class frmMain

    Private Const BUFFER_SIZE As Integer = 63      ' 1ブロックあたりの送信バイト数（Arduinoバッファが64Bytesのため）
    ' 通信コマンド
    ' アスキーコードの0から開始
    Private Const CMD_ACCEPT As Byte = Asc("0")          ' 通信受信正常
    Private Const CMD_END As Byte = Asc("1")             ' 通信終了指令

    Private Const CMD_PING As Byte = Asc("2")            ' PINGコマンド
    Private Const CMD_SD_CONFIG As Byte = Asc("3")      ' SD構成情報要求
    Private Const CMD_SD_DISCONNECT As Byte = Asc("4")  ' SD取り外し検出
    Private Const CMD_FILE_SEND As Byte = Asc("5")      ' ファイル送信
    Private Const CMD_FILE_NACK As Byte = Asc("6")      ' ファイル受信失敗
    Private Const CMD_FILE_ACK As Byte = Asc("7")       ' ファイル受信応答

    Private Const CMD_NOP As Byte = &HFF

    ' 各種フラグ
    Dim connect As Boolean                  ' 接続フラグ
    Dim load_file As Boolean                ' ファイル読み込みフラグ
    Dim opcode As Byte = 1                  ' 現在の処理
    Dim send_flag As Boolean                ' データ転送中
    Dim open_flag As Boolean                ' ファイル選択中

    ' SD情報
    Dim sd_exist As Boolean                 ' SD認識フラグ
    Dim sd_type As Byte                     ' SDカードタイプ(0:SD1, 1:SD2, 2:SDHC)
    Dim volumesize As ULong                 ' SDボリュームサイズ
    Dim usedsize As ULong                   ' SD使用サイズ


    ' ---------------------------------------------------------------------------------------------
    ' フォームロード
    ' ---------------------------------------------------------------------------------------------
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 設定表示
        stbCom.Text = "COM" & My.Settings.com
        stbBaud.Text = My.Settings.baud & "bps"

    End Sub


    ' ---------------------------------------------------------------------------------------------
    ' データ受信割り込み
    ' ---------------------------------------------------------------------------------------------
    Private Sub sp_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles sp.DataReceived
        Dim rData As Byte = sp.ReadByte

        If tim.Enabled = False Then
            opcode = rData

        Else
            ' コマンドセット
            ' （ログに受信データ表示）
            Invoke(New ReciveDataInsertDelegate(AddressOf ReciveDataInsert), rData)

            ' 現在のコマンドより処理
            Select Case opcode
                Case CMD_END
                    ' PINGに対してENDが応答
                    ' (マイコン初期化中に接続すると発生する)
                    device_close()
                    MessageBox.Show("通信エラーが発生しました" & vbCrLf & _
                                    "（PINGに対してデバイス切断要求の応答）", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Case CMD_SD_CONFIG
                    spWriteByte(CMD_ACCEPT)
                    ' SDカード構成情報の受信 (1Byte)
                    waitData(1)
                    sd_type = sp.ReadByte()
                    spWriteByte(CMD_ACCEPT)
                    ' SDボリュームサイズ (4Byte)
                    waitData(4)
                    ' 分割データを結合（ビットシフト）
                    volumesize = CULng(sp.ReadByte) * 16777216
                    volumesize += CULng(sp.ReadByte) * 65536
                    volumesize += CULng(sp.ReadByte * 256)
                    volumesize += CULng(sp.ReadByte)

                    spWriteByte(CMD_ACCEPT)
                    ' SD使用済みサイズ (4Byte)
                    waitData(4)
                    usedsize = CULng(sp.ReadByte) * 16777216
                    usedsize += CULng(sp.ReadByte) * 65536
                    usedsize += CULng(sp.ReadByte * 256)
                    usedsize += CULng(sp.ReadByte)
                    spWriteByte(CMD_ACCEPT)
                    ' SD情報の表示更新(デリゲート)
                    Invoke(New SDInfoDelegate(AddressOf SDInfo), True)
                    opcode = CMD_NOP

                Case CMD_SD_DISCONNECT
                    ' SDカードが抜かれた
                    Invoke(New SDInfoDelegate(AddressOf SDInfo), False)
                    opcode = CMD_NOP

            End Select
        End If
    End Sub



    ' ---------------------------------------------------------------------------------------------
    ' タイマー割り込み
    ' ---------------------------------------------------------------------------------------------
    Private Sub tim_Tick(sender As Object, e As EventArgs) Handles tim.Tick
        If send_flag = False Then
            ' マイコンにPING送信
            If sp.IsOpen = False Then
                tim.Stop()
                device_close()
                MessageBox.Show("デバイスが切断されました", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' PING応答を待つ
            spWriteByte(CMD_PING)
            If waitCommand(CMD_PING) = False Then
                tim.Stop()
                device_close()
                MessageBox.Show("デバイスから応答がありません", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
        ' 送信ボタン有効無効切替
        btnFileWrite.Enabled = connect And sd_exist And load_file
    End Sub




    ' *********************************************************************************************
    ' * コントロール
    ' *********************************************************************************************

    ' ---------------------------------------------------------------------------------------------
    ' ファイルを開く
    ' ---------------------------------------------------------------------------------------------
    Private Sub tsOpen_Click(sender As Object, e As EventArgs) Handles tsOpen.Click
        ' フラグにてタイマー処理を抑制
        open_flag = True

        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each f As String In ofd.FileNames
                If IsZenkaku(f) Then
                    MessageBox.Show(f & vbCrLf & "は全角文字が含まれているため使用できません", "ファイル名異常", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit For
                ElseIf Path.GetFileNameWithoutExtension(f).Length >= 8 Then
                    MessageBox.Show("ファイル名は８文字以内のみ送信可能です", "ファイル名長すぎ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit For
                End If

                With New FileInfo(f)
                    ' 新しい行の追加
                    lstFile.Items.Add(New ListViewItem({.Name, (.Length / 1024).ToString("0") & "kB", .FullName}))
                End With

            Next
            ' 情報の更新
            lblFileCount.Text = "ファイル数：" & lstFile.Items.Count.ToString     ' ファイル数
            load_file = lstFile.Items.Count > 0                                 ' ファイルロードフラグ更新
            stbMsg.Text = "ファイルを読み込みました"
        End If
        open_flag = False
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' リストをクリア
    ' ---------------------------------------------------------------------------------------------
    Private Sub tsLstClear_Click(sender As Object, e As EventArgs) Handles tsLstClear.Click
        lstFile.Items.Clear()
        load_file = False
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' 閉じる
    ' ---------------------------------------------------------------------------------------------
    Private Sub tsExit_Click(sender As Object, e As EventArgs) Handles tsExit.Click, Me.FormClosing
        ' 接続されていれば閉じる
        If connect Then
            tim.Stop()
            Sleep(100)
            spWriteByte(CMD_END)
            sp.Close()
        End If
        Me.Dispose()
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' 接続･切断
    ' ---------------------------------------------------------------------------------------------
    Private Sub device_connect(sender As Object, e As EventArgs) Handles tsCon.Click, btnCon.Click
        Try
            ' コマンドを一時無効
            btnCon.Enabled = False
            tsCon.Enabled = False
            tim.Stop()
            ' 接続状態で分岐
            If connect Then
                ' 接続済み→切断処理
                device_close()
            Else
                ' 未接続→接続処理
                device_open()
            End If

        Catch ex As Exception
            MessageBox.Show("接続に失敗しました" & vbCrLf & _
                            "(" & ex.Message & ")", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            ' コマンドを有効に戻す
            btnCon.Enabled = True
            tsCon.Enabled = True
        End Try
    End Sub


    ' ---------------------------------------------------------------------------------------------
    ' COMポート設定
    ' ---------------------------------------------------------------------------------------------
    Private Sub tsCom_Click(sender As Object, e As EventArgs) Handles tsCom.Click
        frmCom.ShowDialog()       ' 設定ウィンドウ表示
        ' パラメータ表示更新
        stbBaud.Text = My.Settings.baud.ToString() & "bps"
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' 受信ログクリア
    ' ---------------------------------------------------------------------------------------------
    Private Sub tsLogClear_Click(sender As Object, e As EventArgs) Handles tsLogClear.Click
        lstLog.Items.Clear()
        lstLog.Items.Add("受信ログ")
    End Sub
    ' ---------------------------------------------------------------------------------------------
    ' 受信ログキー入力時
    ' ---------------------------------------------------------------------------------------------
    Private Sub lstLog_KeyUp(sender As Object, e As KeyEventArgs) Handles lstLog.KeyUp
        ' Deleteでログ削除
        If e.KeyCode = Keys.Delete Then
            lstLog.Items.Clear()
            lstLog.Items.Add("受信ログ")
        End If
    End Sub




    ' ---------------------------------------------------------------------------------------------
    ' ファイルを送信する
    ' ---------------------------------------------------------------------------------------------
    Private Sub btnFileWrite_Click(sender As Object, e As EventArgs) Handles btnFileWrite.Click
        If Not sd_exist OrElse Not load_file Then
            Return
        End If

        send_flag = True
        Try
            ' タイマーをいったん停止
            tim.Stop()
            sp.DiscardInBuffer()
            opcode = CMD_NOP
            ' ファイル存在を確認
            For Each l As ListViewItem In lstFile.Items
                If Not File.Exists(l.SubItems(2).Text) Then
                    MessageBox.Show(l.SubItems(0).Text & "は存在しないためリストから削除されます", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    l.Remove()
                End If
            Next
            lblFileCount.Text = lstFile.Items.Count

            If True OrElse MessageBox.Show("リストに表示されているファイルを転送します", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                ' リストに表示されているファイルを送信
                For fi As Integer = 0 To lstFile.Items.Count - 1
                    Dim fpath As String = lstFile.Items(fi).SubItems(2).Text
                    Dim fname As String = Path.GetFileName(fpath)
                    Dim last_num As Integer = 0
                    Try
                        stbMsg.Text = fname & "を転送中..."
                        Application.DoEvents()
                        Using sr As New FileStream(fpath, FileMode.Open, FileAccess.Read)
                            ' 64文字以上はスキップ
                            If fname.Length >= BUFFER_SIZE Then
                                Continue For
                            End If

                            Dim data(BUFFER_SIZE) As Byte

                            ' コマンド送信
                            ' Arduino側はこのコマンド受信により受信モードに移る
                            stbMsg.Text = fname & "ファイル送信指令"
                            spWriteByte(CMD_FILE_SEND)
                            If waitCommand(CMD_FILE_ACK) = False Then
                                Throw New ApplicationException("デバイスからコマンド応答がありません")
                            End If
                            stbMsg.Text = fname & "ＯＫ"

                            ' ファイル名送信
                            ' UTF8形式（終端文字：0x0A）
                            stbMsg.Text = fname & "ファイル名送信"
                            data = Encoding.UTF8.GetBytes(fname & Chr(10))
                            sp.Write(data, 0, data.Length)             ' ファイル名を送信
                            If waitCommand(CMD_FILE_ACK) = False Then
                                Throw New ApplicationException("デバイスからファイル名受信応答がありません")
                            End If
                            stbMsg.Text = fname & "ＯＫ"

                            ' ファイルサイズ送信
                            stbMsg.Text = fname & "ファイルサイズ送信"
                            data(0) = (sr.Length \ 16777216) And &HFF
                            data(1) = (sr.Length \ 65536) And &HFF
                            data(2) = (sr.Length \ 256) And &HFF
                            data(3) = sr.Length And &HFF
                            sp.Write(data, 0, 4)
                            If waitCommand(CMD_FILE_ACK) = False Then
                                Throw New ApplicationException("デバイスからファイルサイズ受信応答がありません")
                            End If
                            stbMsg.Text = fname & "ＯＫ"

                            ' ファイルデータ送信
                            ' １ブロック63Bytes単位でＳＤ読み込み→送信
                            stbMsg.Text = fname & "データ送信"
                            ReDim Preserve data(BUFFER_SIZE)
                            ' プログレスバー設定
                            pb.Value = 0
                            pb.Maximum = sr.Length - BUFFER_SIZE - 1
                            For i As Integer = 0 To sr.Length - BUFFER_SIZE - 1 Step BUFFER_SIZE
                                sr.Read(data, 0, BUFFER_SIZE)
                                sp.Write(data, 0, BUFFER_SIZE)
                                If waitCommand(CMD_FILE_ACK) = False Then
                                    Throw New ApplicationException("デバイスからデータ受信応答がありません")
                                End If
                                pb.Value = i
                            Next

                            ' 最後のデータを送信
                            last_num = sr.Read(data, 0, sr.Length Mod BUFFER_SIZE)
                            sp.Write(data, 0, last_num)
                            If waitCommand(CMD_FILE_ACK) = False Then
                                Throw New ApplicationException("末尾データの受信応答がありません")
                            End If
                        End Using
                        stbMsg.Text = fname & "の転送完了"
                    Catch ex As Exception
                        stbMsg.Text = "転送に失敗"
                        spWriteByte(CMD_END)
                        MessageBox.Show(fname & "の転送に失敗しました" & vbCrLf & ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                Next
            End If

        Catch ex As Exception
            stbMsg.Text = "転送に失敗"
            MessageBox.Show(ex.Message, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            pb.Value = 0
            send_flag = False
            tim.Start()
        End Try
    End Sub



    ' *********************************************************************************************
    ' * 関数
    ' *********************************************************************************************

    ' ---------------------------------------------------------------------------------------------
    ' シリアルポート　1バイト書き込み
    '   b       : 書き込みデータバイト
    ' ---------------------------------------------------------------------------------------------
    Private Function spWriteByte(ByVal b As Byte)
        Try
            ' バイト配列を生成しバイトデータとして送信
            Dim buf(1) As Byte
            buf(0) = b
            sp.Write(buf, 0, 1)
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function


    ' ---------------------------------------------------------------------------------------------
    ' 接続開始処理
    ' ---------------------------------------------------------------------------------------------
    Private Sub device_open()
        
        sp.PortName = "COM" & My.Settings.com.ToString
        sp.BaudRate = My.Settings.baud
        sp.Open()       ' ポートオープン
        ' 接続要求
        spWriteByte(CMD_ACCEPT)
        
        ' 応答を待つ
        If waitCommand(CMD_ACCEPT) Then
            ' 接続成功
            btnCon.Text = "接続中"
            tsCon.Text = "接続中"
            btnCon.Enabled = False
            connect = True
            tim.Start()
            stbMsg.Text = "接続しました"
        Else
            ' 接続失敗
            If sp.IsOpen Then
                sp.Close()
            End If
            Throw New ApplicationException("接続要求への応答がタイムアウトしました")
        End If
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' 切断終了処理
    ' ---------------------------------------------------------------------------------------------
    Private Sub device_close()
        ' 接続済み→切断処理
        'tim.Stop()
        If sp.IsOpen Then
            spWriteByte(CMD_END)
            sp.Close()
        End If

        ' ボタン文字切り替え
        Invoke(New btnComTextChangeDelegate(AddressOf btnComTextChange), "接続")
        connect = False
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' 特定コマンドまでウェイト
    ' タイムアウトは3秒（くらい）
    ' ---------------------------------------------------------------------------------------------
    Private Function waitCommand(ByVal cmd As Byte) As Boolean
        Dim timeout As Integer = 0
        While opcode <> cmd
            Sleep(1)
            Application.DoEvents()
            timeout += 1
            ' タイムアウト
            If timeout > 5000 Then
                Return False
            End If
        End While
        ' ACCEPTならリセットする
        If cmd = CMD_FILE_ACK Then
            opcode = CMD_NOP
        End If

        Return True
    End Function

    ' ---------------------------------------------------------------------------------------------
    ' データバッファが一定数まで待機
    ' タイムアウトは3秒（くらい）
    ' ---------------------------------------------------------------------------------------------
    Private Function waitData(ByVal num As Integer) As Boolean
        Dim timeout As Integer = 0
        While sp.BytesToRead < num
            Sleep(9)
            Application.DoEvents()
            timeout += 1
            ' タイムアウト
            If timeout > 300 Then
                Return False
            End If
        End While
        Return True
    End Function

    ' ---------------------------------------------------------------------------------------------
    ' 全角文字が含まれるか調べる
    '   Value   : 調べる文字列
    '   returns : True->全角文字がある
    ' ---------------------------------------------------------------------------------------------
    Private Function IsZenkaku(ByVal Value As String) As Boolean
        Dim ByteLength As Integer
        ByteLength = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(Value)
        Return Len(Value) <> ByteLength
    End Function


    ' *********************************************************************************************
    ' * デリゲート
    ' *********************************************************************************************

    ' ---------------------------------------------------------------------------------------------
    ' 受信データを更新するデリゲート
    '   rec     : ログパターン番号
    ' ---------------------------------------------------------------------------------------------
    Private Delegate Sub ReciveDataInsertDelegate(ByVal rec As Byte)
    Private Sub ReciveDataInsert(ByVal rec As Byte)
        opcode = rec
        If lstLog.Items.Count >= 100 Then
            ' 受信ログがいっぱいならクリア
            lstLog.Items.Clear()
            lstLog.Items.Add("受信ログ")
        Else
            ' 受信コマンドを表示
            Dim str As String = rec.ToString("000") & ":"
            Select Case rec
                Case CMD_ACCEPT
                    str &= "ACCEPT"
                Case CMD_END
                    str &= "END"
                Case CMD_PING
                    str &= "PING"
                Case CMD_SD_CONFIG
                    str &= "SD_CONFIG"
                Case CMD_SD_DISCONNECT
                    str &= "SD_DISCONNECT"
                Case CMD_FILE_ACK
                    str &= "ACK"
                Case CMD_FILE_NACK
                    str &= "NACK"
                Case CMD_NOP
                    str &= "NOP"
                Case Else
                    str &= "???"
            End Select
            lstLog.Items.Insert(0, str)
        End If
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' SD情報の表示を更新するデリゲート
    ' ---------------------------------------------------------------------------------------------
    Private Delegate Sub SDInfoDelegate(ByVal flag As Boolean)
    Private Sub SDInfo(ByVal flag As Boolean)
        If flag Then
            If volumesize = 0 Then
                stbMsg.Text = "SDカード情報の取得に失敗しました"
                SDInfo(False)
            End If

            ' 接続として処理
            Dim str As String = ""
            ' SDフォーマット
            Select Case sd_type
                Case 1
                    str = "SD1  "
                Case 2
                    str = "SD2  "
                Case 3
                    str = "SDHC "
                Case Else
                    str = "認識できないSD(" & sd_type.ToString & ")"
            End Select
            lblSDDet.Text = str & " (" & Math.Round(usedsize / volumesize * 100, 3).ToString() & "%)"

            ' SD容量
            pbSD.Value = usedsize / volumesize * 100
            lblSDVol.Text = usedsize.ToString & " Byte / " & volumesize.ToString & " Byte"

            sd_exist = True
        Else
            ' 切断として処理
            lblSDDet.Text = "SDカードを認識していません"
            lblSDVol.Text = ""
            pbSD.Value = 0
            sd_exist = False
        End If

    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' 接続ボタンの文字をセットするデリゲート
    '   str     : ボタンにセットする文字
    ' ---------------------------------------------------------------------------------------------
    Private Delegate Sub btnComTextChangeDelegate(ByVal str As String)
    Private Sub btnComTextChange(ByVal str As String)
        btnCon.Text = str
    End Sub


    Private Sub ヘルプHToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ヘルプHToolStripMenuItem.Click
        MessageBox.Show("データ転送ツール", Me.Text)
    End Sub

End Class
