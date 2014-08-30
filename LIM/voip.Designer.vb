<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class voip
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(voip))
        Me.btn_stop = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btn_accept = New System.Windows.Forms.Button
        Me.btn_cancel = New System.Windows.Forms.Button
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.lbl_info_key = New System.Windows.Forms.Label
        Me.txt_hotkey = New System.Windows.Forms.TextBox
        Me.BackgroundWorker3 = New System.ComponentModel.BackgroundWorker
        Me.Timer3 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.pb_mute = New System.Windows.Forms.PictureBox
        Me.btn_redefenir = New System.Windows.Forms.Button
        Me.lbl_quality = New System.Windows.Forms.Label
        Me.bar_quality = New System.Windows.Forms.TrackBar
        Me.lbl_keytext = New System.Windows.Forms.Label
        Me.pb_img1 = New System.Windows.Forms.PictureBox
        Me.pb_img2 = New System.Windows.Forms.PictureBox
        Me.lbl_me_info = New System.Windows.Forms.Label
        Me.lbl_other_info = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.pb_mute, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bar_quality, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_img1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_img2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_stop
        '
        Me.btn_stop.Location = New System.Drawing.Point(283, 68)
        Me.btn_stop.Name = "btn_stop"
        Me.btn_stop.Size = New System.Drawing.Size(99, 23)
        Me.btn_stop.TabIndex = 1
        Me.btn_stop.Text = "Parar"
        Me.btn_stop.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'btn_accept
        '
        Me.btn_accept.Location = New System.Drawing.Point(283, 10)
        Me.btn_accept.Name = "btn_accept"
        Me.btn_accept.Size = New System.Drawing.Size(99, 23)
        Me.btn_accept.TabIndex = 2
        Me.btn_accept.Text = "Aceitar"
        Me.btn_accept.UseVisualStyleBackColor = True
        '
        'btn_cancel
        '
        Me.btn_cancel.Location = New System.Drawing.Point(283, 39)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(99, 23)
        Me.btn_cancel.TabIndex = 3
        Me.btn_cancel.Text = "Cancelar"
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'BackgroundWorker2
        '
        Me.BackgroundWorker2.WorkerSupportsCancellation = True
        '
        'Timer2
        '
        '
        'lbl_info_key
        '
        Me.lbl_info_key.AutoSize = True
        Me.lbl_info_key.BackColor = System.Drawing.Color.Transparent
        Me.lbl_info_key.Location = New System.Drawing.Point(283, 12)
        Me.lbl_info_key.Name = "lbl_info_key"
        Me.lbl_info_key.Size = New System.Drawing.Size(87, 13)
        Me.lbl_info_key.TabIndex = 6
        Me.lbl_info_key.Text = "Tecla para falar :"
        '
        'txt_hotkey
        '
        Me.txt_hotkey.Enabled = False
        Me.txt_hotkey.Location = New System.Drawing.Point(286, 31)
        Me.txt_hotkey.Name = "txt_hotkey"
        Me.txt_hotkey.Size = New System.Drawing.Size(34, 20)
        Me.txt_hotkey.TabIndex = 7
        Me.txt_hotkey.Text = "75"
        Me.txt_hotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BackgroundWorker3
        '
        Me.BackgroundWorker3.WorkerSupportsCancellation = True
        '
        'Timer3
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.pb_mute)
        Me.GroupBox1.Controls.Add(Me.btn_redefenir)
        Me.GroupBox1.Controls.Add(Me.lbl_quality)
        Me.GroupBox1.Controls.Add(Me.bar_quality)
        Me.GroupBox1.Controls.Add(Me.txt_hotkey)
        Me.GroupBox1.Controls.Add(Me.lbl_info_key)
        Me.GroupBox1.Controls.Add(Me.lbl_keytext)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 108)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(376, 87)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Definiçoes"
        '
        'pb_mute
        '
        Me.pb_mute.BackColor = System.Drawing.Color.Transparent
        Me.pb_mute.Location = New System.Drawing.Point(216, 19)
        Me.pb_mute.Name = "pb_mute"
        Me.pb_mute.Size = New System.Drawing.Size(48, 48)
        Me.pb_mute.TabIndex = 13
        Me.pb_mute.TabStop = False
        '
        'btn_redefenir
        '
        Me.btn_redefenir.BackColor = System.Drawing.Color.Transparent
        Me.btn_redefenir.Location = New System.Drawing.Point(286, 57)
        Me.btn_redefenir.Name = "btn_redefenir"
        Me.btn_redefenir.Size = New System.Drawing.Size(84, 20)
        Me.btn_redefenir.TabIndex = 10
        Me.btn_redefenir.Text = "Redefinir"
        Me.btn_redefenir.UseVisualStyleBackColor = False
        '
        'lbl_quality
        '
        Me.lbl_quality.BackColor = System.Drawing.Color.Transparent
        Me.lbl_quality.Location = New System.Drawing.Point(14, 54)
        Me.lbl_quality.Name = "lbl_quality"
        Me.lbl_quality.Size = New System.Drawing.Size(184, 23)
        Me.lbl_quality.TabIndex = 9
        Me.lbl_quality.Text = "lbl_quality"
        Me.lbl_quality.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'bar_quality
        '
        Me.bar_quality.Location = New System.Drawing.Point(14, 19)
        Me.bar_quality.Maximum = 15
        Me.bar_quality.Name = "bar_quality"
        Me.bar_quality.Size = New System.Drawing.Size(184, 45)
        Me.bar_quality.TabIndex = 8
        '
        'lbl_keytext
        '
        Me.lbl_keytext.BackColor = System.Drawing.Color.Transparent
        Me.lbl_keytext.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_keytext.Location = New System.Drawing.Point(326, 31)
        Me.lbl_keytext.Name = "lbl_keytext"
        Me.lbl_keytext.Size = New System.Drawing.Size(26, 20)
        Me.lbl_keytext.TabIndex = 11
        Me.lbl_keytext.Text = "K"
        Me.lbl_keytext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pb_img1
        '
        Me.pb_img1.Location = New System.Drawing.Point(16, 19)
        Me.pb_img1.Name = "pb_img1"
        Me.pb_img1.Size = New System.Drawing.Size(32, 32)
        Me.pb_img1.TabIndex = 9
        Me.pb_img1.TabStop = False
        '
        'pb_img2
        '
        Me.pb_img2.Location = New System.Drawing.Point(16, 57)
        Me.pb_img2.Name = "pb_img2"
        Me.pb_img2.Size = New System.Drawing.Size(32, 32)
        Me.pb_img2.TabIndex = 10
        Me.pb_img2.TabStop = False
        '
        'lbl_me_info
        '
        Me.lbl_me_info.AutoSize = True
        Me.lbl_me_info.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_me_info.Location = New System.Drawing.Point(71, 28)
        Me.lbl_me_info.Name = "lbl_me_info"
        Me.lbl_me_info.Size = New System.Drawing.Size(83, 15)
        Me.lbl_me_info.TabIndex = 11
        Me.lbl_me_info.Text = "lbl_me_info"
        '
        'lbl_other_info
        '
        Me.lbl_other_info.AutoSize = True
        Me.lbl_other_info.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_other_info.Location = New System.Drawing.Point(71, 66)
        Me.lbl_other_info.Name = "lbl_other_info"
        Me.lbl_other_info.Size = New System.Drawing.Size(96, 15)
        Me.lbl_other_info.TabIndex = 12
        Me.lbl_other_info.Text = "lbl_other_info"
        '
        'voip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(400, 206)
        Me.Controls.Add(Me.lbl_other_info)
        Me.Controls.Add(Me.lbl_me_info)
        Me.Controls.Add(Me.pb_img2)
        Me.Controls.Add(Me.pb_img1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btn_accept)
        Me.Controls.Add(Me.btn_stop)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "voip"
        Me.Text = "Chamada"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pb_mute, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bar_quality, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_img1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_img2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_stop As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btn_accept As System.Windows.Forms.Button
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents lbl_info_key As System.Windows.Forms.Label
    Friend WithEvents txt_hotkey As System.Windows.Forms.TextBox
    Friend WithEvents BackgroundWorker3 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Timer3 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents bar_quality As System.Windows.Forms.TrackBar
    Friend WithEvents lbl_quality As System.Windows.Forms.Label
    Friend WithEvents btn_redefenir As System.Windows.Forms.Button
    Friend WithEvents pb_img1 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_img2 As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_me_info As System.Windows.Forms.Label
    Friend WithEvents lbl_other_info As System.Windows.Forms.Label
    Friend WithEvents lbl_keytext As System.Windows.Forms.Label
    Friend WithEvents pb_mute As System.Windows.Forms.PictureBox
End Class
