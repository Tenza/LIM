<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<Global.System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726")> _
Partial Class Login
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
    Friend WithEvents login_lbl As System.Windows.Forms.Label
    Friend WithEvents password_lbl As System.Windows.Forms.Label
    Friend WithEvents login_txt As System.Windows.Forms.TextBox
    Friend WithEvents password_txt As System.Windows.Forms.TextBox
    Friend WithEvents ok_btn As System.Windows.Forms.Button
    Friend WithEvents registar_btn As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Login))
        Me.login_lbl = New System.Windows.Forms.Label
        Me.password_lbl = New System.Windows.Forms.Label
        Me.login_txt = New System.Windows.Forms.TextBox
        Me.password_txt = New System.Windows.Forms.TextBox
        Me.ok_btn = New System.Windows.Forms.Button
        Me.registar_btn = New System.Windows.Forms.Button
        Me.aviso_lbl = New System.Windows.Forms.Label
        Me.logo = New System.Windows.Forms.Label
        Me.roda_pb = New System.Windows.Forms.PictureBox
        Me.logo2 = New System.Windows.Forms.Label
        Me.bola_pb = New System.Windows.Forms.PictureBox
        Me.alterar_btn = New System.Windows.Forms.Label
        CType(Me.roda_pb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.bola_pb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'login_lbl
        '
        Me.login_lbl.BackColor = System.Drawing.Color.Transparent
        Me.login_lbl.Location = New System.Drawing.Point(246, 108)
        Me.login_lbl.Name = "login_lbl"
        Me.login_lbl.Size = New System.Drawing.Size(220, 23)
        Me.login_lbl.TabIndex = 0
        Me.login_lbl.Text = "&Login"
        Me.login_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'password_lbl
        '
        Me.password_lbl.BackColor = System.Drawing.Color.Transparent
        Me.password_lbl.Location = New System.Drawing.Point(246, 160)
        Me.password_lbl.Name = "password_lbl"
        Me.password_lbl.Size = New System.Drawing.Size(220, 23)
        Me.password_lbl.TabIndex = 2
        Me.password_lbl.Text = "&Password"
        Me.password_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'login_txt
        '
        Me.login_txt.Location = New System.Drawing.Point(248, 128)
        Me.login_txt.MaxLength = 12
        Me.login_txt.Name = "login_txt"
        Me.login_txt.Size = New System.Drawing.Size(220, 20)
        Me.login_txt.TabIndex = 1
        '
        'password_txt
        '
        Me.password_txt.Location = New System.Drawing.Point(248, 180)
        Me.password_txt.MaxLength = 50
        Me.password_txt.Name = "password_txt"
        Me.password_txt.Size = New System.Drawing.Size(220, 20)
        Me.password_txt.TabIndex = 3
        '
        'ok_btn
        '
        Me.ok_btn.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ok_btn.Location = New System.Drawing.Point(247, 236)
        Me.ok_btn.Name = "ok_btn"
        Me.ok_btn.Size = New System.Drawing.Size(94, 23)
        Me.ok_btn.TabIndex = 4
        Me.ok_btn.Text = "&OK"
        Me.ok_btn.UseVisualStyleBackColor = False
        '
        'registar_btn
        '
        Me.registar_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.registar_btn.Location = New System.Drawing.Point(372, 236)
        Me.registar_btn.Name = "registar_btn"
        Me.registar_btn.Size = New System.Drawing.Size(94, 23)
        Me.registar_btn.TabIndex = 5
        Me.registar_btn.Text = "&Registar"
        '
        'aviso_lbl
        '
        Me.aviso_lbl.AutoSize = True
        Me.aviso_lbl.BackColor = System.Drawing.Color.Transparent
        Me.aviso_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aviso_lbl.ForeColor = System.Drawing.Color.Red
        Me.aviso_lbl.Location = New System.Drawing.Point(246, 211)
        Me.aviso_lbl.Name = "aviso_lbl"
        Me.aviso_lbl.Size = New System.Drawing.Size(87, 13)
        Me.aviso_lbl.TabIndex = 6
        Me.aviso_lbl.Text = "Login Errado !"
        Me.aviso_lbl.Visible = False
        '
        'logo
        '
        Me.logo.AutoSize = True
        Me.logo.BackColor = System.Drawing.Color.Transparent
        Me.logo.Font = New System.Drawing.Font("Courier New", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.logo.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.logo.Location = New System.Drawing.Point(283, 30)
        Me.logo.Name = "logo"
        Me.logo.Size = New System.Drawing.Size(146, 73)
        Me.logo.TabIndex = 7
        Me.logo.Text = "LIM"
        '
        'roda_pb
        '
        Me.roda_pb.BackColor = System.Drawing.Color.Transparent
        Me.roda_pb.Image = Global.LIM.My.Resources.Resources.load
        Me.roda_pb.Location = New System.Drawing.Point(90, 261)
        Me.roda_pb.Name = "roda_pb"
        Me.roda_pb.Size = New System.Drawing.Size(39, 37)
        Me.roda_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.roda_pb.TabIndex = 9
        Me.roda_pb.TabStop = False
        '
        'logo2
        '
        Me.logo2.AutoSize = True
        Me.logo2.BackColor = System.Drawing.Color.Transparent
        Me.logo2.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.logo2.ForeColor = System.Drawing.SystemColors.Desktop
        Me.logo2.Location = New System.Drawing.Point(280, 88)
        Me.logo2.Name = "logo2"
        Me.logo2.Size = New System.Drawing.Size(154, 14)
        Me.logo2.TabIndex = 11
        Me.logo2.Text = "Lan Instant Messaging"
        '
        'bola_pb
        '
        Me.bola_pb.BackColor = System.Drawing.Color.Transparent
        Me.bola_pb.Image = Global.LIM.My.Resources.Resources.load_click
        Me.bola_pb.Location = New System.Drawing.Point(97, 266)
        Me.bola_pb.Name = "bola_pb"
        Me.bola_pb.Size = New System.Drawing.Size(24, 25)
        Me.bola_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.bola_pb.TabIndex = 12
        Me.bola_pb.TabStop = False
        Me.bola_pb.Visible = False
        '
        'alterar_btn
        '
        Me.alterar_btn.AutoSize = True
        Me.alterar_btn.BackColor = System.Drawing.Color.Transparent
        Me.alterar_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.alterar_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.alterar_btn.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.alterar_btn.Location = New System.Drawing.Point(380, 306)
        Me.alterar_btn.Name = "alterar_btn"
        Me.alterar_btn.Size = New System.Drawing.Size(102, 15)
        Me.alterar_btn.TabIndex = 13
        Me.alterar_btn.Text = "Alterar a conexão"
        '
        'Login
        '
        Me.AcceptButton = Me.ok_btn
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImage = Global.LIM.My.Resources.Resources.login_fundo
        Me.ClientSize = New System.Drawing.Size(494, 330)
        Me.Controls.Add(Me.alterar_btn)
        Me.Controls.Add(Me.bola_pb)
        Me.Controls.Add(Me.logo2)
        Me.Controls.Add(Me.logo)
        Me.Controls.Add(Me.aviso_lbl)
        Me.Controls.Add(Me.registar_btn)
        Me.Controls.Add(Me.ok_btn)
        Me.Controls.Add(Me.password_txt)
        Me.Controls.Add(Me.login_txt)
        Me.Controls.Add(Me.password_lbl)
        Me.Controls.Add(Me.login_lbl)
        Me.Controls.Add(Me.roda_pb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        CType(Me.roda_pb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.bola_pb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents aviso_lbl As System.Windows.Forms.Label
    Friend WithEvents logo As System.Windows.Forms.Label
    Friend WithEvents roda_pb As System.Windows.Forms.PictureBox
    Friend WithEvents logo2 As System.Windows.Forms.Label
    Friend WithEvents bola_pb As System.Windows.Forms.PictureBox
    Friend WithEvents alterar_btn As System.Windows.Forms.Label

End Class
