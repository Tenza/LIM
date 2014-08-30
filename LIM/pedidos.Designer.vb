<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class pedidos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(pedidos))
        Me.aceitar_btn = New System.Windows.Forms.Button
        Me.block_btn = New System.Windows.Forms.Button
        Me.txt_lbl = New System.Windows.Forms.Label
        Me.amigo_lbl = New System.Windows.Forms.Label
        Me.mais_tarde_btn = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.avatar_2_img = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.adiados_txt = New System.Windows.Forms.Label
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.GroupBox2.SuspendLayout()
        CType(Me.avatar_2_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'aceitar_btn
        '
        Me.aceitar_btn.BackColor = System.Drawing.Color.Transparent
        Me.aceitar_btn.FlatAppearance.BorderSize = 0
        Me.aceitar_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet
        Me.aceitar_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.aceitar_btn.ForeColor = System.Drawing.Color.Green
        Me.aceitar_btn.Location = New System.Drawing.Point(182, 129)
        Me.aceitar_btn.Name = "aceitar_btn"
        Me.aceitar_btn.Size = New System.Drawing.Size(82, 24)
        Me.aceitar_btn.TabIndex = 1
        Me.aceitar_btn.Text = "Aceitar"
        Me.aceitar_btn.UseVisualStyleBackColor = False
        '
        'block_btn
        '
        Me.block_btn.BackColor = System.Drawing.Color.Transparent
        Me.block_btn.FlatAppearance.BorderSize = 0
        Me.block_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet
        Me.block_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.block_btn.ForeColor = System.Drawing.Color.Red
        Me.block_btn.Location = New System.Drawing.Point(270, 129)
        Me.block_btn.Name = "block_btn"
        Me.block_btn.Size = New System.Drawing.Size(82, 24)
        Me.block_btn.TabIndex = 3
        Me.block_btn.Text = "Bloquear"
        Me.block_btn.UseVisualStyleBackColor = False
        '
        'txt_lbl
        '
        Me.txt_lbl.AutoSize = True
        Me.txt_lbl.BackColor = System.Drawing.Color.Transparent
        Me.txt_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_lbl.Location = New System.Drawing.Point(179, 47)
        Me.txt_lbl.Name = "txt_lbl"
        Me.txt_lbl.Size = New System.Drawing.Size(260, 16)
        Me.txt_lbl.TabIndex = 4
        Me.txt_lbl.Text = "quer adicioná-lo à lista de contactos" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'amigo_lbl
        '
        Me.amigo_lbl.AutoSize = True
        Me.amigo_lbl.BackColor = System.Drawing.Color.Transparent
        Me.amigo_lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.amigo_lbl.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.amigo_lbl.Location = New System.Drawing.Point(180, 18)
        Me.amigo_lbl.Name = "amigo_lbl"
        Me.amigo_lbl.Size = New System.Drawing.Size(70, 24)
        Me.amigo_lbl.TabIndex = 5
        Me.amigo_lbl.Text = "Amigo"
        '
        'mais_tarde_btn
        '
        Me.mais_tarde_btn.BackColor = System.Drawing.Color.Transparent
        Me.mais_tarde_btn.FlatAppearance.BorderSize = 0
        Me.mais_tarde_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.BlueViolet
        Me.mais_tarde_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mais_tarde_btn.ForeColor = System.Drawing.Color.Black
        Me.mais_tarde_btn.Location = New System.Drawing.Point(358, 129)
        Me.mais_tarde_btn.Name = "mais_tarde_btn"
        Me.mais_tarde_btn.Size = New System.Drawing.Size(94, 24)
        Me.mais_tarde_btn.TabIndex = 6
        Me.mais_tarde_btn.Text = "Mais tarde"
        Me.mais_tarde_btn.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.avatar_2_img)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(150, 148)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        '
        'avatar_2_img
        '
        Me.avatar_2_img.Location = New System.Drawing.Point(4, 10)
        Me.avatar_2_img.Name = "avatar_2_img"
        Me.avatar_2_img.Size = New System.Drawing.Size(142, 133)
        Me.avatar_2_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.avatar_2_img.TabIndex = 3
        Me.avatar_2_img.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(180, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 16)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Pretende:"
        '
        'adiados_txt
        '
        Me.adiados_txt.AutoSize = True
        Me.adiados_txt.BackColor = System.Drawing.Color.Transparent
        Me.adiados_txt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.adiados_txt.Location = New System.Drawing.Point(180, 6)
        Me.adiados_txt.Name = "adiados_txt"
        Me.adiados_txt.Size = New System.Drawing.Size(168, 13)
        Me.adiados_txt.TabIndex = 15
        Me.adiados_txt.Text = "Lista de contactos adiados :"
        Me.adiados_txt.Visible = False
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(182, 21)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(260, 82)
        Me.ListBox1.TabIndex = 16
        Me.ListBox1.Visible = False
        '
        'pedidos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(459, 159)
        Me.ControlBox = False
        Me.Controls.Add(Me.amigo_lbl)
        Me.Controls.Add(Me.txt_lbl)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.adiados_txt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.mais_tarde_btn)
        Me.Controls.Add(Me.block_btn)
        Me.Controls.Add(Me.aceitar_btn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "pedidos"
        Me.Text = "Pedido de Contacto"
        Me.TopMost = True
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.avatar_2_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents aceitar_btn As System.Windows.Forms.Button
    Friend WithEvents block_btn As System.Windows.Forms.Button
    Friend WithEvents txt_lbl As System.Windows.Forms.Label
    Friend WithEvents amigo_lbl As System.Windows.Forms.Label
    Friend WithEvents mais_tarde_btn As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents avatar_2_img As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents adiados_txt As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
End Class
