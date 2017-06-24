<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.menuMain = New System.Windows.Forms.MenuStrip()
        Me.tsFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsLstClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsDevice = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCon = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsCom = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsLogClear = New System.Windows.Forms.ToolStripMenuItem()
        Me.ヘルプHToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.sp = New System.IO.Ports.SerialPort(Me.components)
        Me.stbInd = New System.Windows.Forms.StatusStrip()
        Me.stbCom = New System.Windows.Forms.ToolStripStatusLabel()
        Me.stbBaud = New System.Windows.Forms.ToolStripStatusLabel()
        Me.stbMsg = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pb = New System.Windows.Forms.ToolStripProgressBar()
        Me.pnlSD = New System.Windows.Forms.Panel()
        Me.lblSDVol = New System.Windows.Forms.Label()
        Me.pbSD = New System.Windows.Forms.ProgressBar()
        Me.lblSDDet = New System.Windows.Forms.Label()
        Me.picSD = New System.Windows.Forms.PictureBox()
        Me.btnCon = New System.Windows.Forms.Button()
        Me.btnFileWrite = New System.Windows.Forms.Button()
        Me.lblSD = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstFile = New System.Windows.Forms.ListView()
        Me.chName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chSize = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblFileCount = New System.Windows.Forms.Label()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.lstLog = New System.Windows.Forms.ListBox()
        Me.tim = New System.Windows.Forms.Timer(Me.components)
        Me.menuMain.SuspendLayout()
        Me.stbInd.SuspendLayout()
        Me.pnlSD.SuspendLayout()
        CType(Me.picSD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'menuMain
        '
        Me.menuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsFile, Me.tsDevice, Me.ヘルプHToolStripMenuItem})
        Me.menuMain.Location = New System.Drawing.Point(0, 0)
        Me.menuMain.Name = "menuMain"
        Me.menuMain.Size = New System.Drawing.Size(656, 26)
        Me.menuMain.TabIndex = 0
        Me.menuMain.Text = "MenuStrip1"
        '
        'tsFile
        '
        Me.tsFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsOpen, Me.tsLstClear, Me.tsExit})
        Me.tsFile.Name = "tsFile"
        Me.tsFile.Size = New System.Drawing.Size(85, 22)
        Me.tsFile.Text = "ファイル(&F)"
        '
        'tsOpen
        '
        Me.tsOpen.Name = "tsOpen"
        Me.tsOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.tsOpen.Size = New System.Drawing.Size(160, 22)
        Me.tsOpen.Text = "開く"
        '
        'tsLstClear
        '
        Me.tsLstClear.Name = "tsLstClear"
        Me.tsLstClear.Size = New System.Drawing.Size(160, 22)
        Me.tsLstClear.Text = "リストをクリア"
        '
        'tsExit
        '
        Me.tsExit.Name = "tsExit"
        Me.tsExit.Size = New System.Drawing.Size(160, 22)
        Me.tsExit.Text = "終了"
        '
        'tsDevice
        '
        Me.tsDevice.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsCon, Me.tsCom, Me.tsLogClear})
        Me.tsDevice.Name = "tsDevice"
        Me.tsDevice.Size = New System.Drawing.Size(87, 22)
        Me.tsDevice.Text = "デバイス(&D)"
        '
        'tsCon
        '
        Me.tsCon.Image = CType(resources.GetObject("tsCon.Image"), System.Drawing.Image)
        Me.tsCon.Name = "tsCon"
        Me.tsCon.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.tsCon.Size = New System.Drawing.Size(207, 22)
        Me.tsCon.Text = "接続"
        '
        'tsCom
        '
        Me.tsCom.Name = "tsCom"
        Me.tsCom.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.tsCom.Size = New System.Drawing.Size(207, 22)
        Me.tsCom.Text = "COMポート設定"
        '
        'tsLogClear
        '
        Me.tsLogClear.Name = "tsLogClear"
        Me.tsLogClear.Size = New System.Drawing.Size(207, 22)
        Me.tsLogClear.Text = "受信ログクリア"
        '
        'ヘルプHToolStripMenuItem
        '
        Me.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem"
        Me.ヘルプHToolStripMenuItem.Size = New System.Drawing.Size(75, 22)
        Me.ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)"
        '
        'sp
        '
        Me.sp.ReadTimeout = 5000
        Me.sp.WriteTimeout = 60000
        '
        'stbInd
        '
        Me.stbInd.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.stbCom, Me.stbBaud, Me.stbMsg, Me.pb})
        Me.stbInd.Location = New System.Drawing.Point(0, 403)
        Me.stbInd.Name = "stbInd"
        Me.stbInd.Size = New System.Drawing.Size(656, 27)
        Me.stbInd.TabIndex = 1
        Me.stbInd.Text = "StatusStrip1"
        '
        'stbCom
        '
        Me.stbCom.AutoSize = False
        Me.stbCom.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.stbCom.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.stbCom.Name = "stbCom"
        Me.stbCom.Size = New System.Drawing.Size(60, 22)
        Me.stbCom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'stbBaud
        '
        Me.stbBaud.AutoSize = False
        Me.stbBaud.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.stbBaud.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.stbBaud.Name = "stbBaud"
        Me.stbBaud.Size = New System.Drawing.Size(100, 22)
        Me.stbBaud.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'stbMsg
        '
        Me.stbMsg.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.stbMsg.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.stbMsg.Name = "stbMsg"
        Me.stbMsg.Size = New System.Drawing.Size(379, 22)
        Me.stbMsg.Spring = True
        Me.stbMsg.Text = "   "
        Me.stbMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pb
        '
        Me.pb.Name = "pb"
        Me.pb.Size = New System.Drawing.Size(100, 21)
        Me.pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'pnlSD
        '
        Me.pnlSD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSD.BackColor = System.Drawing.Color.White
        Me.pnlSD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlSD.Controls.Add(Me.lblSDVol)
        Me.pnlSD.Controls.Add(Me.pbSD)
        Me.pnlSD.Controls.Add(Me.lblSDDet)
        Me.pnlSD.Controls.Add(Me.picSD)
        Me.pnlSD.Location = New System.Drawing.Point(14, 56)
        Me.pnlSD.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.pnlSD.Name = "pnlSD"
        Me.pnlSD.Padding = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.pnlSD.Size = New System.Drawing.Size(503, 75)
        Me.pnlSD.TabIndex = 2
        '
        'lblSDVol
        '
        Me.lblSDVol.Location = New System.Drawing.Point(96, 53)
        Me.lblSDVol.Name = "lblSDVol"
        Me.lblSDVol.Size = New System.Drawing.Size(240, 17)
        Me.lblSDVol.TabIndex = 3
        '
        'pbSD
        '
        Me.pbSD.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbSD.Location = New System.Drawing.Point(95, 27)
        Me.pbSD.Name = "pbSD"
        Me.pbSD.Size = New System.Drawing.Size(358, 23)
        Me.pbSD.Step = 1
        Me.pbSD.TabIndex = 2
        '
        'lblSDDet
        '
        Me.lblSDDet.AutoSize = True
        Me.lblSDDet.Location = New System.Drawing.Point(96, 7)
        Me.lblSDDet.Name = "lblSDDet"
        Me.lblSDDet.Size = New System.Drawing.Size(153, 17)
        Me.lblSDDet.TabIndex = 1
        Me.lblSDDet.Text = "SDカードを認識していません"
        '
        'picSD
        '
        Me.picSD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picSD.BackColor = System.Drawing.Color.Transparent
        Me.picSD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picSD.Image = CType(resources.GetObject("picSD.Image"), System.Drawing.Image)
        Me.picSD.Location = New System.Drawing.Point(8, 7)
        Me.picSD.Name = "picSD"
        Me.picSD.Size = New System.Drawing.Size(68, 58)
        Me.picSD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picSD.TabIndex = 0
        Me.picSD.TabStop = False
        '
        'btnCon
        '
        Me.btnCon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCon.Font = New System.Drawing.Font("Meiryo UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnCon.Image = CType(resources.GetObject("btnCon.Image"), System.Drawing.Image)
        Me.btnCon.Location = New System.Drawing.Point(523, 56)
        Me.btnCon.Name = "btnCon"
        Me.btnCon.Size = New System.Drawing.Size(121, 75)
        Me.btnCon.TabIndex = 3
        Me.btnCon.Text = "接続"
        Me.btnCon.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCon.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnCon.UseVisualStyleBackColor = True
        '
        'btnFileWrite
        '
        Me.btnFileWrite.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFileWrite.Font = New System.Drawing.Font("Meiryo UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnFileWrite.Image = CType(resources.GetObject("btnFileWrite.Image"), System.Drawing.Image)
        Me.btnFileWrite.Location = New System.Drawing.Point(523, 306)
        Me.btnFileWrite.Name = "btnFileWrite"
        Me.btnFileWrite.Size = New System.Drawing.Size(121, 91)
        Me.btnFileWrite.TabIndex = 4
        Me.btnFileWrite.Text = "転送"
        Me.btnFileWrite.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnFileWrite.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnFileWrite.UseVisualStyleBackColor = True
        '
        'lblSD
        '
        Me.lblSD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSD.BackColor = System.Drawing.SystemColors.ControlLight
        Me.lblSD.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSD.Location = New System.Drawing.Point(14, 26)
        Me.lblSD.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.lblSD.Name = "lblSD"
        Me.lblSD.Size = New System.Drawing.Size(630, 21)
        Me.lblSD.TabIndex = 5
        Me.lblSD.Text = "SDカード"
        Me.lblSD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Label1.Font = New System.Drawing.Font("メイリオ", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 152)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(630, 21)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "書き込みファイル"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstFile
        '
        Me.lstFile.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lstFile.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstFile.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chName, Me.chSize, Me.chPath})
        Me.lstFile.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lstFile.FullRowSelect = True
        Me.lstFile.GridLines = True
        Me.lstFile.Location = New System.Drawing.Point(14, 180)
        Me.lstFile.Name = "lstFile"
        Me.lstFile.Size = New System.Drawing.Size(503, 217)
        Me.lstFile.TabIndex = 7
        Me.lstFile.UseCompatibleStateImageBehavior = False
        Me.lstFile.View = System.Windows.Forms.View.Details
        '
        'chName
        '
        Me.chName.Text = "ファイル名"
        Me.chName.Width = 157
        '
        'chSize
        '
        Me.chSize.Text = "サイズ"
        Me.chSize.Width = 85
        '
        'chPath
        '
        Me.chPath.Text = "場所"
        Me.chPath.Width = 277
        '
        'lblFileCount
        '
        Me.lblFileCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFileCount.AutoSize = True
        Me.lblFileCount.Location = New System.Drawing.Point(523, 286)
        Me.lblFileCount.Name = "lblFileCount"
        Me.lblFileCount.Size = New System.Drawing.Size(64, 17)
        Me.lblFileCount.TabIndex = 8
        Me.lblFileCount.Text = "ファイル数:"
        '
        'ofd
        '
        Me.ofd.Multiselect = True
        Me.ofd.Title = "ファイルを選択"
        '
        'lstLog
        '
        Me.lstLog.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstLog.Font = New System.Drawing.Font("Meiryo UI", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lstLog.ForeColor = System.Drawing.Color.Black
        Me.lstLog.FormattingEnabled = True
        Me.lstLog.ItemHeight = 12
        Me.lstLog.Items.AddRange(New Object() {"受信ログ"})
        Me.lstLog.Location = New System.Drawing.Point(523, 180)
        Me.lstLog.Name = "lstLog"
        Me.lstLog.Size = New System.Drawing.Size(121, 100)
        Me.lstLog.TabIndex = 9
        '
        'tim
        '
        Me.tim.Interval = 1000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(656, 430)
        Me.Controls.Add(Me.lstLog)
        Me.Controls.Add(Me.lblFileCount)
        Me.Controls.Add(Me.lstFile)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSD)
        Me.Controls.Add(Me.btnFileWrite)
        Me.Controls.Add(Me.btnCon)
        Me.Controls.Add(Me.pnlSD)
        Me.Controls.Add(Me.stbInd)
        Me.Controls.Add(Me.menuMain)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMain"
        Me.Text = "Intra"
        Me.menuMain.ResumeLayout(False)
        Me.menuMain.PerformLayout()
        Me.stbInd.ResumeLayout(False)
        Me.stbInd.PerformLayout()
        Me.pnlSD.ResumeLayout(False)
        Me.pnlSD.PerformLayout()
        CType(Me.picSD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents menuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents tsFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsDevice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCon As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsCom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sp As System.IO.Ports.SerialPort
    Friend WithEvents stbInd As System.Windows.Forms.StatusStrip
    Friend WithEvents pnlSD As System.Windows.Forms.Panel
    Friend WithEvents pbSD As System.Windows.Forms.ProgressBar
    Friend WithEvents lblSDDet As System.Windows.Forms.Label
    Friend WithEvents picSD As System.Windows.Forms.PictureBox
    Friend WithEvents btnCon As System.Windows.Forms.Button
    Friend WithEvents btnFileWrite As System.Windows.Forms.Button
    Friend WithEvents lblSD As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstFile As System.Windows.Forms.ListView
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPath As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblFileCount As System.Windows.Forms.Label
    Friend WithEvents ヘルプHToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsLstClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lstLog As System.Windows.Forms.ListBox
    Friend WithEvents stbCom As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsLogClear As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tim As System.Windows.Forms.Timer
    Friend WithEvents lblSDVol As System.Windows.Forms.Label
    Friend WithEvents stbBaud As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents stbMsg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents pb As System.Windows.Forms.ToolStripProgressBar

End Class
