<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class receberfile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(receberfile))
        Me.btn_aceitar = New System.Windows.Forms.Button
        Me.btn_cancelar = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbl_nomef = New System.Windows.Forms.Label
        Me.lbl_tamanho = New System.Windows.Forms.Label
        Me.lbl_data = New System.Windows.Forms.Label
        Me.lbl_envio = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.px_img = New System.Windows.Forms.PictureBox
        CType(Me.px_img, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_aceitar
        '
        Me.btn_aceitar.BackColor = System.Drawing.Color.Transparent
        Me.btn_aceitar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_aceitar.ForeColor = System.Drawing.Color.Green
        Me.btn_aceitar.Location = New System.Drawing.Point(368, 123)
        Me.btn_aceitar.Name = "btn_aceitar"
        Me.btn_aceitar.Size = New System.Drawing.Size(82, 24)
        Me.btn_aceitar.TabIndex = 0
        Me.btn_aceitar.Text = "Aceitar"
        Me.btn_aceitar.UseVisualStyleBackColor = False
        '
        'btn_cancelar
        '
        Me.btn_cancelar.BackColor = System.Drawing.Color.Transparent
        Me.btn_cancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_cancelar.ForeColor = System.Drawing.Color.Red
        Me.btn_cancelar.Location = New System.Drawing.Point(368, 151)
        Me.btn_cancelar.Name = "btn_cancelar"
        Me.btn_cancelar.Size = New System.Drawing.Size(82, 24)
        Me.btn_cancelar.TabIndex = 1
        Me.btn_cancelar.Text = "Cancelar"
        Me.btn_cancelar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label1.Location = New System.Drawing.Point(104, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(179, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Ficheiro a receber:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label2.Location = New System.Drawing.Point(104, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 17)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Tamanho:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label3.Location = New System.Drawing.Point(104, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Data do envio:"
        '
        'lbl_nomef
        '
        Me.lbl_nomef.AutoSize = True
        Me.lbl_nomef.BackColor = System.Drawing.Color.Transparent
        Me.lbl_nomef.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_nomef.Location = New System.Drawing.Point(104, 69)
        Me.lbl_nomef.Name = "lbl_nomef"
        Me.lbl_nomef.Size = New System.Drawing.Size(61, 13)
        Me.lbl_nomef.TabIndex = 5
        Me.lbl_nomef.Text = "lbl_nomef"
        '
        'lbl_tamanho
        '
        Me.lbl_tamanho.AutoSize = True
        Me.lbl_tamanho.BackColor = System.Drawing.Color.Transparent
        Me.lbl_tamanho.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_tamanho.Location = New System.Drawing.Point(104, 115)
        Me.lbl_tamanho.Name = "lbl_tamanho"
        Me.lbl_tamanho.Size = New System.Drawing.Size(75, 13)
        Me.lbl_tamanho.TabIndex = 6
        Me.lbl_tamanho.Text = "lbl_tamanho"
        '
        'lbl_data
        '
        Me.lbl_data.AutoSize = True
        Me.lbl_data.BackColor = System.Drawing.Color.Transparent
        Me.lbl_data.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_data.Location = New System.Drawing.Point(104, 161)
        Me.lbl_data.Name = "lbl_data"
        Me.lbl_data.Size = New System.Drawing.Size(52, 13)
        Me.lbl_data.TabIndex = 7
        Me.lbl_data.Text = "lbl_data"
        '
        'lbl_envio
        '
        Me.lbl_envio.AutoSize = True
        Me.lbl_envio.BackColor = System.Drawing.Color.Transparent
        Me.lbl_envio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_envio.Location = New System.Drawing.Point(104, 23)
        Me.lbl_envio.Name = "lbl_envio"
        Me.lbl_envio.Size = New System.Drawing.Size(58, 13)
        Me.lbl_envio.TabIndex = 9
        Me.lbl_envio.Text = "lbl_envio"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.Desktop
        Me.Label5.Location = New System.Drawing.Point(104, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Envio de: "
        '
        'px_img
        '
        Me.px_img.BackColor = System.Drawing.Color.Transparent
        Me.px_img.Location = New System.Drawing.Point(-12, 25)
        Me.px_img.Name = "px_img"
        Me.px_img.Size = New System.Drawing.Size(128, 128)
        Me.px_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.px_img.TabIndex = 12
        Me.px_img.TabStop = False
        '
        'receberfile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(458, 183)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btn_cancelar)
        Me.Controls.Add(Me.lbl_envio)
        Me.Controls.Add(Me.btn_aceitar)
        Me.Controls.Add(Me.lbl_nomef)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl_tamanho)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbl_data)
        Me.Controls.Add(Me.px_img)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "receberfile"
        Me.Text = "Receber ficheiro"
        Me.TopMost = True
        CType(Me.px_img, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_aceitar As System.Windows.Forms.Button
    Friend WithEvents btn_cancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbl_nomef As System.Windows.Forms.Label
    Friend WithEvents lbl_tamanho As System.Windows.Forms.Label
    Friend WithEvents lbl_data As System.Windows.Forms.Label
    Friend WithEvents lbl_envio As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents px_img As System.Windows.Forms.PictureBox
End Class
