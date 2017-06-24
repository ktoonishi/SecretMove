Imports System.IO.Ports
Imports System.Collections.Generic
Imports System.Management

Public Class frmCom


    ' ---------------------------------------------------------------------------------------------
    ' フォームロード
    ' ---------------------------------------------------------------------------------------------
    Private Sub frmCom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' コンボボックス初期化
            ' 暫定でCOM20まで表示
            For i As Integer = 1 To 20
                cmbCom.Items.Add("COM" & i.ToString())
            Next

            '' COM構成をロードしコンボボックスに追記
            'Dim c As Integer
            'Dim mcW32SerPort As New ManagementClass("Win32_PnPEntity")
            'For Each mo As ManagementObject In mcW32SerPort.GetInstances()
            '    With mo.GetPropertyValue("Caption").ToString()
            '        ' COMポートデバイスを抽出
            '        If .IndexOf("(COM") > 0 Then
            '            ' 番号を抽出しコンボボックスに追記
            '            c = CInt(.Split("(")(1).Replace("COM", "").Replace(")", ""))
            '            cmbCom.Items.Item(c - 1) = .ToString()
            '        End If
            '    End With
            'Next

            ' 設定を読み込む
            With My.Settings
                cmbCom.SelectedIndex = .com - 1             ' COMポート
                cmbBaud.Text = .baud.ToString               ' ボーレート
            End With

        Catch ex As Exception
            MessageBox.Show("設定の読み込みに失敗しました" & vbCrLf & "規定値を設定します", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbCom.SelectedIndex = -1
            cmbBaud.Text = "9600"
        End Try
    End Sub


    ' ---------------------------------------------------------------------------------------------
    ' OKボタンクリック
    ' ---------------------------------------------------------------------------------------------
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        ' 設定を保存
        With My.Settings
            If cmbCom.SelectedIndex > 0 Then
                .com = cmbCom.SelectedIndex + 1
            End If
            .baud = cmbBaud.Text
        End With
        Me.Dispose()
    End Sub

    ' ---------------------------------------------------------------------------------------------
    ' キャンセルボタンクリック
    ' ---------------------------------------------------------------------------------------------
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Me.Dispose()
    End Sub

    Private Sub frmCom_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Dispose()
    End Sub
End Class