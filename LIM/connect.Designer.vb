<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class connect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(connect))
        Me.path_bd = New System.Windows.Forms.TextBox
        Me.pc_nome = New System.Windows.Forms.TextBox
        Me.user_sql = New System.Windows.Forms.TextBox
        Me.pass_sql = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ty_combo = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.guardar_btn = New System.Windows.Forms.Button
        Me.tips = New System.Windows.Forms.ToolTip(Me.components)
        Me.testar_btn = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'path_bd
        '
        Me.path_bd.Location = New System.Drawing.Point(9, 64)
        Me.path_bd.Name = "path_bd"
        Me.path_bd.Size = New System.Drawing.Size(227, 20)
        Me.path_bd.TabIndex = 0
        Me.tips.SetToolTip(Me.path_bd, resources.GetString("path_bd.ToolTip"))
        '
        'pc_nome
        '
        Me.pc_nome.Location = New System.Drawing.Point(9, 103)
        Me.pc_nome.Name = "pc_nome"
        Me.pc_nome.Size = New System.Drawing.Size(227, 20)
        Me.pc_nome.TabIndex = 1
        Me.tips.SetToolTip(Me.pc_nome, resources.GetString("pc_nome.ToolTip"))
        '
        'user_sql
        '
        Me.user_sql.Location = New System.Drawing.Point(9, 142)
        Me.user_sql.Name = "user_sql"
        Me.user_sql.Size = New System.Drawing.Size(227, 20)
        Me.user_sql.TabIndex = 2
        Me.tips.SetToolTip(Me.user_sql, "Escreva aqui o nome do utilizador que registou no SQL Server." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "PS: Atenção as per" & _
                "missões deste utilizador !")
        '
        'pass_sql
        '
        Me.pass_sql.Location = New System.Drawing.Point(9, 181)
        Me.pass_sql.Name = "pass_sql"
        Me.pass_sql.Size = New System.Drawing.Size(227, 20)
        Me.pass_sql.TabIndex = 3
        Me.tips.SetToolTip(Me.pass_sql, "Coloque aqui a password do SQL Server " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "que esta associada ao o utilizador que es" & _
                "creveu acima.")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label1.Location = New System.Drawing.Point(9, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Nome/Localização da BD :"
        '
        'ty_combo
        '
        Me.ty_combo.FormattingEnabled = True
        Me.ty_combo.Items.AddRange(New Object() {"Lan", "Local"})
        Me.ty_combo.Location = New System.Drawing.Point(9, 24)
        Me.ty_combo.Name = "ty_combo"
        Me.ty_combo.Size = New System.Drawing.Size(227, 21)
        Me.ty_combo.TabIndex = 6
        Me.ty_combo.Text = "Lan"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(9, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Tipo de conexão :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(6, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(135, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Nome do computador :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.Location = New System.Drawing.Point(9, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Utilizador SQL :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(9, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(97, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Password SQL :"
        '
        'guardar_btn
        '
        Me.guardar_btn.BackColor = System.Drawing.Color.Transparent
        Me.guardar_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.guardar_btn.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.guardar_btn.Location = New System.Drawing.Point(9, 207)
        Me.guardar_btn.Name = "guardar_btn"
        Me.guardar_btn.Size = New System.Drawing.Size(103, 25)
        Me.guardar_btn.TabIndex = 4
        Me.guardar_btn.Text = "Guardar"
        Me.guardar_btn.UseVisualStyleBackColor = False
        '
        'tips
        '
        Me.tips.AutomaticDelay = 100
        Me.tips.AutoPopDelay = 10000
        Me.tips.BackColor = System.Drawing.SystemColors.Menu
        Me.tips.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.tips.InitialDelay = 100
        Me.tips.IsBalloon = True
        Me.tips.ReshowDelay = 10
        Me.tips.ShowAlways = True
        Me.tips.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.tips.ToolTipTitle = "Info:"
        '
        'testar_btn
        '
        Me.testar_btn.BackColor = System.Drawing.Color.Transparent
        Me.testar_btn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.testar_btn.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.testar_btn.Location = New System.Drawing.Point(133, 208)
        Me.testar_btn.Name = "testar_btn"
        Me.testar_btn.Size = New System.Drawing.Size(103, 25)
        Me.testar_btn.TabIndex = 5
        Me.testar_btn.Text = "Testar Conexão"
        Me.testar_btn.UseVisualStyleBackColor = False
        '
        'connect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = Global.LIM.My.Resources.Resources.fundo3
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(248, 240)
        Me.Controls.Add(Me.testar_btn)
        Me.Controls.Add(Me.guardar_btn)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ty_combo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pass_sql)
        Me.Controls.Add(Me.user_sql)
        Me.Controls.Add(Me.pc_nome)
        Me.Controls.Add(Me.path_bd)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "connect"
        Me.Text = "Conexão a Base de Dados"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents path_bd As System.Windows.Forms.TextBox
    Friend WithEvents pc_nome As System.Windows.Forms.TextBox
    Friend WithEvents user_sql As System.Windows.Forms.TextBox
    Friend WithEvents pass_sql As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ty_combo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents guardar_btn As System.Windows.Forms.Button
    Friend WithEvents tips As System.Windows.Forms.ToolTip
    Friend WithEvents testar_btn As System.Windows.Forms.Button
End Class
