<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Registar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Registar))
        Me.registar_btn = New System.Windows.Forms.Button
        Me.login_txt = New System.Windows.Forms.TextBox
        Me.password_txt = New System.Windows.Forms.TextBox
        Me.login_lbl = New System.Windows.Forms.Label
        Me.password_lbl = New System.Windows.Forms.Label
        Me.size_lbl = New System.Windows.Forms.Label
        Me.procurar_btn = New System.Windows.Forms.Button
        Me.link_txt = New System.Windows.Forms.TextBox
        Me.avatar_lbl = New System.Windows.Forms.Label
        Me.registar_g = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.meu_avatar_g = New System.Windows.Forms.GroupBox
        Me.avatar = New System.Windows.Forms.PictureBox
        Me.registar_g.SuspendLayout()
        Me.meu_avatar_g.SuspendLayout()
        CType(Me.avatar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'registar_btn
        '
        Me.registar_btn.BackColor = System.Drawing.Color.Transparent
        Me.registar_btn.FlatAppearance.BorderSize = 0
        Me.registar_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro
        Me.registar_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.registar_btn.ForeColor = System.Drawing.SystemColors.Highlight
        Me.registar_btn.Location = New System.Drawing.Point(295, 158)
        Me.registar_btn.Name = "registar_btn"
        Me.registar_btn.Size = New System.Drawing.Size(126, 26)
        Me.registar_btn.TabIndex = 0
        Me.registar_btn.Text = "Registar"
        Me.registar_btn.UseVisualStyleBackColor = False
        '
        'login_txt
        '
        Me.login_txt.Location = New System.Drawing.Point(7, 25)
        Me.login_txt.MaxLength = 12
        Me.login_txt.Name = "login_txt"
        Me.login_txt.Size = New System.Drawing.Size(255, 20)
        Me.login_txt.TabIndex = 2
        '
        'password_txt
        '
        Me.password_txt.Location = New System.Drawing.Point(7, 72)
        Me.password_txt.MaxLength = 50
        Me.password_txt.Name = "password_txt"
        Me.password_txt.Size = New System.Drawing.Size(255, 20)
        Me.password_txt.TabIndex = 3
        '
        'login_lbl
        '
        Me.login_lbl.AutoSize = True
        Me.login_lbl.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.login_lbl.Location = New System.Drawing.Point(7, 9)
        Me.login_lbl.Name = "login_lbl"
        Me.login_lbl.Size = New System.Drawing.Size(66, 13)
        Me.login_lbl.TabIndex = 4
        Me.login_lbl.Text = "Login/Nick :"
        '
        'password_lbl
        '
        Me.password_lbl.AutoSize = True
        Me.password_lbl.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.password_lbl.Location = New System.Drawing.Point(7, 56)
        Me.password_lbl.Name = "password_lbl"
        Me.password_lbl.Size = New System.Drawing.Size(59, 13)
        Me.password_lbl.TabIndex = 5
        Me.password_lbl.Text = "Password :"
        '
        'size_lbl
        '
        Me.size_lbl.AutoSize = True
        Me.size_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.size_lbl.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.size_lbl.Location = New System.Drawing.Point(106, 154)
        Me.size_lbl.Name = "size_lbl"
        Me.size_lbl.Size = New System.Drawing.Size(87, 13)
        Me.size_lbl.TabIndex = 64
        Me.size_lbl.Text = "(100KB's max)"
        '
        'procurar_btn
        '
        Me.procurar_btn.Location = New System.Drawing.Point(7, 148)
        Me.procurar_btn.Name = "procurar_btn"
        Me.procurar_btn.Size = New System.Drawing.Size(93, 22)
        Me.procurar_btn.TabIndex = 63
        Me.procurar_btn.Text = "Procurar"
        Me.procurar_btn.UseVisualStyleBackColor = True
        '
        'link_txt
        '
        Me.link_txt.Location = New System.Drawing.Point(8, 122)
        Me.link_txt.Name = "link_txt"
        Me.link_txt.ReadOnly = True
        Me.link_txt.Size = New System.Drawing.Size(255, 20)
        Me.link_txt.TabIndex = 62
        '
        'avatar_lbl
        '
        Me.avatar_lbl.AutoSize = True
        Me.avatar_lbl.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.avatar_lbl.Location = New System.Drawing.Point(8, 106)
        Me.avatar_lbl.Name = "avatar_lbl"
        Me.avatar_lbl.Size = New System.Drawing.Size(44, 13)
        Me.avatar_lbl.TabIndex = 65
        Me.avatar_lbl.Text = "Avatar :"
        '
        'registar_g
        '
        Me.registar_g.BackColor = System.Drawing.Color.Transparent
        Me.registar_g.Controls.Add(Me.Label2)
        Me.registar_g.Controls.Add(Me.Label1)
        Me.registar_g.Controls.Add(Me.avatar_lbl)
        Me.registar_g.Controls.Add(Me.size_lbl)
        Me.registar_g.Controls.Add(Me.procurar_btn)
        Me.registar_g.Controls.Add(Me.link_txt)
        Me.registar_g.Controls.Add(Me.password_lbl)
        Me.registar_g.Controls.Add(Me.login_lbl)
        Me.registar_g.Controls.Add(Me.password_txt)
        Me.registar_g.Controls.Add(Me.login_txt)
        Me.registar_g.Location = New System.Drawing.Point(5, 4)
        Me.registar_g.Name = "registar_g"
        Me.registar_g.Size = New System.Drawing.Size(272, 183)
        Me.registar_g.TabIndex = 66
        Me.registar_g.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(0, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 13)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "*"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(0, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 13)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "*"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'meu_avatar_g
        '
        Me.meu_avatar_g.BackColor = System.Drawing.Color.Transparent
        Me.meu_avatar_g.Controls.Add(Me.avatar)
        Me.meu_avatar_g.Location = New System.Drawing.Point(283, 4)
        Me.meu_avatar_g.Name = "meu_avatar_g"
        Me.meu_avatar_g.Size = New System.Drawing.Size(150, 148)
        Me.meu_avatar_g.TabIndex = 67
        Me.meu_avatar_g.TabStop = False
        '
        'avatar
        '
        Me.avatar.Location = New System.Drawing.Point(4, 10)
        Me.avatar.Name = "avatar"
        Me.avatar.Size = New System.Drawing.Size(142, 133)
        Me.avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.avatar.TabIndex = 3
        Me.avatar.TabStop = False
        '
        'Registar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BackgroundImage = Global.LIM.My.Resources.Resources.fundo2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(440, 194)
        Me.Controls.Add(Me.meu_avatar_g)
        Me.Controls.Add(Me.registar_g)
        Me.Controls.Add(Me.registar_btn)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Registar"
        Me.Text = "Novo Utilizador"
        Me.registar_g.ResumeLayout(False)
        Me.registar_g.PerformLayout()
        Me.meu_avatar_g.ResumeLayout(False)
        CType(Me.avatar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents registar_btn As System.Windows.Forms.Button
    Friend WithEvents login_txt As System.Windows.Forms.TextBox
    Friend WithEvents password_txt As System.Windows.Forms.TextBox
    Friend WithEvents login_lbl As System.Windows.Forms.Label
    Friend WithEvents password_lbl As System.Windows.Forms.Label
    Friend WithEvents size_lbl As System.Windows.Forms.Label
    Friend WithEvents procurar_btn As System.Windows.Forms.Button
    Friend WithEvents link_txt As System.Windows.Forms.TextBox
    Friend WithEvents avatar_lbl As System.Windows.Forms.Label
    Friend WithEvents registar_g As System.Windows.Forms.GroupBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents meu_avatar_g As System.Windows.Forms.GroupBox
    Friend WithEvents avatar As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
