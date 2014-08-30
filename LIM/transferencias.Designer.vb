<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class transferencias
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(transferencias))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabControl3 = New System.Windows.Forms.TabControl
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.ListV_historico_down = New System.Windows.Forms.ListView
        Me.Enviado_por = New System.Windows.Forms.ColumnHeader
        Me.Nome_do_Ficheiro = New System.Windows.Forms.ColumnHeader
        Me.Estado = New System.Windows.Forms.ColumnHeader
        Me.Tamanho = New System.Windows.Forms.ColumnHeader
        Me.Data = New System.Windows.Forms.ColumnHeader
        Me.Tipo = New System.Windows.Forms.ColumnHeader
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.ListV_historico_up = New System.Windows.Forms.ListView
        Me.Enviado_para1 = New System.Windows.Forms.ColumnHeader
        Me.Nome_do_Ficheiro1 = New System.Windows.Forms.ColumnHeader
        Me.Estado1 = New System.Windows.Forms.ColumnHeader
        Me.Tamanho1 = New System.Windows.Forms.ColumnHeader
        Me.Data1 = New System.Windows.Forms.ColumnHeader
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.listV_recebidas = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.TabPage8 = New System.Windows.Forms.TabPage
        Me.listV_efectuadas = New System.Windows.Forms.ListView
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.btn_eliminar = New System.Windows.Forms.Button
        Me.ListV_pub = New System.Windows.Forms.ListView
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.btn_download = New System.Windows.Forms.Button
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lbl_aviso = New System.Windows.Forms.Label
        Me.lbl_nome = New System.Windows.Forms.Label
        Me.lbl_tamanho = New System.Windows.Forms.Label
        Me.lbl_data = New System.Windows.Forms.Label
        Me.lbl_uploader = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btn_procura = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.radio_up1 = New System.Windows.Forms.RadioButton
        Me.txt_caminho = New System.Windows.Forms.TextBox
        Me.radio_up2 = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.btn_enviar = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(694, 432)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TabControl3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(686, 406)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Histórico de transferências e chamadas"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.TabPage5)
        Me.TabControl3.Controls.Add(Me.TabPage6)
        Me.TabControl3.Controls.Add(Me.TabPage7)
        Me.TabControl3.Controls.Add(Me.TabPage8)
        Me.TabControl3.Location = New System.Drawing.Point(0, 0)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(690, 410)
        Me.TabControl3.TabIndex = 2
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.ListV_historico_down)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(682, 384)
        Me.TabPage5.TabIndex = 0
        Me.TabPage5.Text = "Download"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'ListV_historico_down
        '
        Me.ListV_historico_down.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Enviado_por, Me.Nome_do_Ficheiro, Me.Estado, Me.Tamanho, Me.Data, Me.Tipo})
        Me.ListV_historico_down.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListV_historico_down.FullRowSelect = True
        Me.ListV_historico_down.GridLines = True
        Me.ListV_historico_down.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListV_historico_down.Location = New System.Drawing.Point(3, 3)
        Me.ListV_historico_down.Name = "ListV_historico_down"
        Me.ListV_historico_down.Size = New System.Drawing.Size(676, 378)
        Me.ListV_historico_down.TabIndex = 1
        Me.ListV_historico_down.UseCompatibleStateImageBehavior = False
        Me.ListV_historico_down.View = System.Windows.Forms.View.Details
        '
        'Enviado_por
        '
        Me.Enviado_por.Text = "Enviado por"
        Me.Enviado_por.Width = 143
        '
        'Nome_do_Ficheiro
        '
        Me.Nome_do_Ficheiro.Text = "Nome do Ficheiro"
        Me.Nome_do_Ficheiro.Width = 96
        '
        'Estado
        '
        Me.Estado.Text = "Estado"
        Me.Estado.Width = 66
        '
        'Tamanho
        '
        Me.Tamanho.Text = "Tamanho"
        Me.Tamanho.Width = 65
        '
        'Data
        '
        Me.Data.Text = "Data"
        Me.Data.Width = 42
        '
        'Tipo
        '
        Me.Tipo.Text = "Tipo"
        Me.Tipo.Width = 68
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.ListV_historico_up)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(682, 384)
        Me.TabPage6.TabIndex = 1
        Me.TabPage6.Text = "Upload"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'ListV_historico_up
        '
        Me.ListV_historico_up.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Enviado_para1, Me.Nome_do_Ficheiro1, Me.Estado1, Me.Tamanho1, Me.Data1})
        Me.ListV_historico_up.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListV_historico_up.FullRowSelect = True
        Me.ListV_historico_up.GridLines = True
        Me.ListV_historico_up.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListV_historico_up.Location = New System.Drawing.Point(3, 3)
        Me.ListV_historico_up.Name = "ListV_historico_up"
        Me.ListV_historico_up.Size = New System.Drawing.Size(676, 378)
        Me.ListV_historico_up.TabIndex = 2
        Me.ListV_historico_up.UseCompatibleStateImageBehavior = False
        Me.ListV_historico_up.View = System.Windows.Forms.View.Details
        '
        'Enviado_para1
        '
        Me.Enviado_para1.Text = "Enviado para"
        Me.Enviado_para1.Width = 143
        '
        'Nome_do_Ficheiro1
        '
        Me.Nome_do_Ficheiro1.Text = "Nome do Ficheiro"
        Me.Nome_do_Ficheiro1.Width = 96
        '
        'Estado1
        '
        Me.Estado1.Text = "Estado"
        Me.Estado1.Width = 66
        '
        'Tamanho1
        '
        Me.Tamanho1.Text = "Tamanho"
        Me.Tamanho1.Width = 65
        '
        'Data1
        '
        Me.Data1.Text = "Data"
        Me.Data1.Width = 42
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.listV_recebidas)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(682, 384)
        Me.TabPage7.TabIndex = 2
        Me.TabPage7.Text = "Recebidas"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'listV_recebidas
        '
        Me.listV_recebidas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader3, Me.ColumnHeader9})
        Me.listV_recebidas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listV_recebidas.FullRowSelect = True
        Me.listV_recebidas.GridLines = True
        Me.listV_recebidas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.listV_recebidas.Location = New System.Drawing.Point(3, 3)
        Me.listV_recebidas.Name = "listV_recebidas"
        Me.listV_recebidas.Size = New System.Drawing.Size(676, 378)
        Me.listV_recebidas.TabIndex = 2
        Me.listV_recebidas.UseCompatibleStateImageBehavior = False
        Me.listV_recebidas.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "De"
        Me.ColumnHeader1.Width = 143
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Estado"
        Me.ColumnHeader3.Width = 102
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Data"
        Me.ColumnHeader9.Width = 42
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.listV_efectuadas)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(682, 384)
        Me.TabPage8.TabIndex = 0
        Me.TabPage8.Text = "Efectuadas"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'listV_efectuadas
        '
        Me.listV_efectuadas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader4, Me.ColumnHeader10})
        Me.listV_efectuadas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listV_efectuadas.FullRowSelect = True
        Me.listV_efectuadas.GridLines = True
        Me.listV_efectuadas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.listV_efectuadas.Location = New System.Drawing.Point(0, 0)
        Me.listV_efectuadas.Name = "listV_efectuadas"
        Me.listV_efectuadas.Size = New System.Drawing.Size(682, 384)
        Me.listV_efectuadas.TabIndex = 3
        Me.listV_efectuadas.UseCompatibleStateImageBehavior = False
        Me.listV_efectuadas.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Para"
        Me.ColumnHeader2.Width = 143
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Estado"
        Me.ColumnHeader4.Width = 102
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Data"
        Me.ColumnHeader10.Width = 42
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TabControl2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(686, 406)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Ficheiros públicos"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(690, 410)
        Me.TabControl2.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btn_eliminar)
        Me.TabPage3.Controls.Add(Me.ListV_pub)
        Me.TabPage3.Controls.Add(Me.btn_download)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(682, 384)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Ficheiros Públicos"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btn_eliminar
        '
        Me.btn_eliminar.Enabled = False
        Me.btn_eliminar.Location = New System.Drawing.Point(595, 354)
        Me.btn_eliminar.Name = "btn_eliminar"
        Me.btn_eliminar.Size = New System.Drawing.Size(75, 24)
        Me.btn_eliminar.TabIndex = 7
        Me.btn_eliminar.Text = "Eliminar"
        Me.btn_eliminar.UseVisualStyleBackColor = True
        '
        'ListV_pub
        '
        Me.ListV_pub.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8})
        Me.ListV_pub.FullRowSelect = True
        Me.ListV_pub.GridLines = True
        Me.ListV_pub.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListV_pub.HideSelection = False
        Me.ListV_pub.Location = New System.Drawing.Point(-1, 0)
        Me.ListV_pub.Name = "ListV_pub"
        Me.ListV_pub.Size = New System.Drawing.Size(680, 348)
        Me.ListV_pub.TabIndex = 6
        Me.ListV_pub.UseCompatibleStateImageBehavior = False
        Me.ListV_pub.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Enviado por"
        Me.ColumnHeader5.Width = 144
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Nome do Ficheiro"
        Me.ColumnHeader6.Width = 96
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Tamanho"
        Me.ColumnHeader7.Width = 65
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Data"
        Me.ColumnHeader8.Width = 42
        '
        'btn_download
        '
        Me.btn_download.Enabled = False
        Me.btn_download.Location = New System.Drawing.Point(514, 354)
        Me.btn_download.Name = "btn_download"
        Me.btn_download.Size = New System.Drawing.Size(75, 24)
        Me.btn_download.TabIndex = 5
        Me.btn_download.Text = "Download"
        Me.btn_download.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox2)
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.Controls.Add(Me.btn_enviar)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(682, 384)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Upload"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lbl_aviso)
        Me.GroupBox2.Controls.Add(Me.lbl_nome)
        Me.GroupBox2.Controls.Add(Me.lbl_tamanho)
        Me.GroupBox2.Controls.Add(Me.lbl_data)
        Me.GroupBox2.Controls.Add(Me.lbl_uploader)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 162)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(672, 169)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Informaçao do ficheiro"
        '
        'lbl_aviso
        '
        Me.lbl_aviso.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_aviso.Location = New System.Drawing.Point(355, 135)
        Me.lbl_aviso.Name = "lbl_aviso"
        Me.lbl_aviso.Size = New System.Drawing.Size(311, 22)
        Me.lbl_aviso.TabIndex = 1
        Me.lbl_aviso.Text = "lbl_aviso"
        Me.lbl_aviso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbl_aviso.Visible = False
        '
        'lbl_nome
        '
        Me.lbl_nome.AutoSize = True
        Me.lbl_nome.Location = New System.Drawing.Point(160, 65)
        Me.lbl_nome.Name = "lbl_nome"
        Me.lbl_nome.Size = New System.Drawing.Size(49, 13)
        Me.lbl_nome.TabIndex = 14
        Me.lbl_nome.Text = "lbl_nome"
        Me.lbl_nome.Visible = False
        '
        'lbl_tamanho
        '
        Me.lbl_tamanho.AutoSize = True
        Me.lbl_tamanho.Location = New System.Drawing.Point(113, 100)
        Me.lbl_tamanho.Name = "lbl_tamanho"
        Me.lbl_tamanho.Size = New System.Drawing.Size(64, 13)
        Me.lbl_tamanho.TabIndex = 13
        Me.lbl_tamanho.Text = "lbl_tamanho"
        Me.lbl_tamanho.Visible = False
        '
        'lbl_data
        '
        Me.lbl_data.AutoSize = True
        Me.lbl_data.Location = New System.Drawing.Point(88, 135)
        Me.lbl_data.Name = "lbl_data"
        Me.lbl_data.Size = New System.Drawing.Size(44, 13)
        Me.lbl_data.TabIndex = 12
        Me.lbl_data.Text = "lbl_data"
        Me.lbl_data.Visible = False
        '
        'lbl_uploader
        '
        Me.lbl_uploader.AutoSize = True
        Me.lbl_uploader.Location = New System.Drawing.Point(112, 30)
        Me.lbl_uploader.Name = "lbl_uploader"
        Me.lbl_uploader.Size = New System.Drawing.Size(64, 13)
        Me.lbl_uploader.TabIndex = 8
        Me.lbl_uploader.Text = "lbl_uploader"
        Me.lbl_uploader.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(40, 100)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Tamanho :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(40, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Nome do Ficheiro :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(40, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Data :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(40, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Uploader :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_procura)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.radio_up1)
        Me.GroupBox1.Controls.Add(Me.txt_caminho)
        Me.GroupBox1.Controls.Add(Me.radio_up2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(670, 150)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Upload"
        '
        'btn_procura
        '
        Me.btn_procura.Location = New System.Drawing.Point(421, 89)
        Me.btn_procura.Name = "btn_procura"
        Me.btn_procura.Size = New System.Drawing.Size(75, 23)
        Me.btn_procura.TabIndex = 5
        Me.btn_procura.Text = "Procurar"
        Me.btn_procura.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(83, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Uploader: "
        '
        'radio_up1
        '
        Me.radio_up1.AutoSize = True
        Me.radio_up1.Location = New System.Drawing.Point(108, 72)
        Me.radio_up1.Name = "radio_up1"
        Me.radio_up1.Size = New System.Drawing.Size(90, 17)
        Me.radio_up1.TabIndex = 1
        Me.radio_up1.TabStop = True
        Me.radio_up1.Text = "RadioButton1"
        Me.radio_up1.UseVisualStyleBackColor = True
        '
        'txt_caminho
        '
        Me.txt_caminho.Enabled = False
        Me.txt_caminho.Location = New System.Drawing.Point(421, 63)
        Me.txt_caminho.Name = "txt_caminho"
        Me.txt_caminho.Size = New System.Drawing.Size(224, 20)
        Me.txt_caminho.TabIndex = 4
        '
        'radio_up2
        '
        Me.radio_up2.AutoSize = True
        Me.radio_up2.Location = New System.Drawing.Point(108, 95)
        Me.radio_up2.Name = "radio_up2"
        Me.radio_up2.Size = New System.Drawing.Size(66, 17)
        Me.radio_up2.TabIndex = 2
        Me.radio_up2.TabStop = True
        Me.radio_up2.Text = "Anónimo"
        Me.radio_up2.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(418, 33)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Ficheiro: "
        '
        'btn_enviar
        '
        Me.btn_enviar.Enabled = False
        Me.btn_enviar.Location = New System.Drawing.Point(222, 344)
        Me.btn_enviar.Name = "btn_enviar"
        Me.btn_enviar.Size = New System.Drawing.Size(224, 23)
        Me.btn_enviar.TabIndex = 6
        Me.btn_enviar.Text = "Enviar ficheiro para a rede pública"
        Me.btn_enviar.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'transferencias
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 428)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "transferencias"
        Me.Text = "Transferências"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabControl3.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ListV_historico_down As System.Windows.Forms.ListView
    Friend WithEvents Enviado_por As System.Windows.Forms.ColumnHeader
    Friend WithEvents Nome_do_Ficheiro As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tamanho As System.Windows.Forms.ColumnHeader
    Friend WithEvents Data As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ListV_pub As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btn_download As System.Windows.Forms.Button
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Tipo As System.Windows.Forms.ColumnHeader
    Friend WithEvents radio_up2 As System.Windows.Forms.RadioButton
    Friend WithEvents radio_up1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_enviar As System.Windows.Forms.Button
    Friend WithEvents btn_procura As System.Windows.Forms.Button
    Friend WithEvents txt_caminho As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_nome As System.Windows.Forms.Label
    Friend WithEvents lbl_tamanho As System.Windows.Forms.Label
    Friend WithEvents lbl_data As System.Windows.Forms.Label
    Friend WithEvents lbl_uploader As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lbl_aviso As System.Windows.Forms.Label
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents ListV_historico_up As System.Windows.Forms.ListView
    Friend WithEvents Enviado_para1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Nome_do_Ficheiro1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Tamanho1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Data1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btn_eliminar As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Estado As System.Windows.Forms.ColumnHeader
    Friend WithEvents Estado1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents listV_recebidas As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listV_efectuadas As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
End Class
