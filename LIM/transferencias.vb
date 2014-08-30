Imports System.Data.SqlClient

Public Class transferencias

    Private CaminhoFicheiro As String
    Private get_nome As String
    Private get_extension As String
    Private get_size As Integer
    Private total_size As String

    Private id_vr As String = ""
    Private de_vr As String
    Private nome_vr As String = ""
    Private extencao_vr As String = ""
    Private tamanho_vr As String = ""
    Private nv_nome As String = ""

    Private premissoes_v As String = "Publico"

#Region "Form Load"

    Private Sub transferencias_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        actualiza_dados()

        radio_up1.Text = main.login_txt.Text
        radio_up1.Checked = True
    End Sub
#End Region

#Region "Opçoes"
    Private Sub radio_up1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radio_up1.CheckedChanged
        lbl_uploader.Visible = True
        lbl_uploader.Text = radio_up1.Text
    End Sub

    Private Sub radio_up2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radio_up2.CheckedChanged
        lbl_uploader.Visible = True
        lbl_uploader.Text = radio_up2.Text
    End Sub

    Private Sub btn_procura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_procura.Click
        OpenFileDialog1.Title = "Seleccione o Ficheiro"
        OpenFileDialog1.FileName = "Nome do Ficheiro"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Filter = "Todos os Ficheiros (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        lbl_nome.Visible = True
        lbl_tamanho.Visible = True

        CaminhoFicheiro = OpenFileDialog1.FileName.ToString()

        get_nome = System.IO.Path.GetFileName(CaminhoFicheiro).ToString
        get_extension = System.IO.Path.GetExtension(CaminhoFicheiro).ToLower.ToString
        get_size = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoFicheiro).Length() / 1024000))
        total_size = ""

        'verifica tamanho (MB)
        If get_size > 50 Then
            btn_enviar.Enabled = False
            txt_caminho.Text = ""
            lbl_nome.Text = ""
            lbl_tamanho.Text = ""
            lbl_aviso.Visible = True
            lbl_aviso.ForeColor = Color.Red
            lbl_aviso.Text = "O ficheiro não pode ter um tamanho superior a 50MB."
            Exit Sub
        End If

        'Verifica se o nome ja existe
        Dim command As New SqlCommand("SELECT nome FROM transferencias WHERE nome=@nome AND para=@para AND premissoes=@premissoes AND visto=@visto", connection)
        command.Parameters.AddWithValue("@nome", get_nome)
        command.Parameters.AddWithValue("@para", premissoes_v)
        command.Parameters.AddWithValue("@premissoes", premissoes_v)
        command.Parameters.AddWithValue("@visto", premissoes_v)
        connection.Open()
        Dim reader As SqlDataReader = command.ExecuteReader()
        Try
            reader.Read()
            Dim nome_var = reader("nome")
            btn_enviar.Enabled = False
            txt_caminho.Text = ""
            lbl_nome.Text = ""
            lbl_tamanho.Text = ""
            lbl_aviso.Visible = True
            lbl_aviso.ForeColor = Color.Red
            lbl_aviso.Text = "Um ficheiro com o mesmo nome foi encontrado."
            reader.Close()
            connection.Close()
            Exit Sub
        Catch
            reader.Close()
            connection.Close()
        End Try

        'Transforma o tamanho
        If get_size = 1 Or get_size = 0 Then
            get_size = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoFicheiro).Length() / 1024))
            total_size = get_size & " KB"
            If get_size = 1 Or get_size = 0 Then
                get_size = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoFicheiro).Length()))
                total_size = get_size & " bytes"
            End If
        Else
            total_size = get_size & " MB"
        End If

        lbl_nome.Text = get_nome
        lbl_tamanho.Text = total_size
        txt_caminho.Text = CaminhoFicheiro

        lbl_aviso.Visible = True
        lbl_aviso.ForeColor = Color.YellowGreen
        lbl_aviso.Text = "O ficheiro está pronto a ser enviado !"

        btn_enviar.Enabled = True
    End Sub
#End Region

#Region "Enviar Ficheiro"
    Private Sub btn_enviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_enviar.Click
        Try
            If txt_caminho.Text = "" Then
                lbl_aviso.ForeColor = Color.Red
                lbl_aviso.Text = "Seleccione o ficheiro a enviar."
                Exit Sub
            End If


            If radio_up2.Checked = True Then
                Dim a As String = MsgBox("Ao enviar um ficheiro anonimamente, apenas o administrador poderá apagar o mesmo." & vbNewLine & "Deseja prosseguir?", MsgBoxStyle.YesNo, "Informação")
                If a = vbNo Then
                    connection.Close()
                    Exit Sub
                End If
            End If

            btn_enviar.Enabled = False

            'Saca ID
            Dim max_id_var As Long
            Dim command3 As New SqlCommand("SELECT MAX(ID) as max_id FROM transferencias", connection)
            connection.Open()
            Dim reader3 As SqlDataReader = command3.ExecuteReader()
            reader3.Read()
            Try
                max_id_var = reader3("max_id")
            Catch
                max_id_var = 0
            End Try
            reader3.Close()
            connection.Close()

            max_id_var = max_id_var + 1

            'Data do server sql
            Dim command5 As New SqlCommand("SELECT GETDATE() as Date", connection)
            Dim data As String = ""
            connection.Open()
            Dim reader As SqlDataReader = command5.ExecuteReader()
            reader.Read()
            data = reader("Date")
            connection.Close()

            'Escreve ficheiro, tem q estar em primeiro pois, so depois e q o file fica disponivel
            Dim Command As New SqlCommand("INSERT INTO ficheiros_pub (ID, ficheiro) VALUES (@ID, @ficheiro)", connection)
            Command.Parameters.AddWithValue("@ID", max_id_var)
            Dim FicheiroUp As IO.FileStream = IO.File.OpenRead(CaminhoFicheiro)
            Dim FileStream As New IO.BinaryReader(FicheiroUp)
            Dim BufferSize As Long = My.Computer.FileSystem.GetFileInfo(CaminhoFicheiro).Length()
            Command.Parameters.Add("@ficheiro", SqlDbType.VarBinary).Value = FileStream.ReadBytes(BufferSize)
            FicheiroUp.Flush()
            FicheiroUp.Close()
            FileStream.Close()
            connection.Open()
            Command.ExecuteNonQuery()
            connection.Close()

            'Escreve o registo da transferencia
            Dim premissoes_v As String = "Publico"
            Dim Command2 As New SqlCommand("INSERT INTO transferencias (ID, de, para, visto, nome, extencao, tamanho, data, premissoes) VALUES (@ID, @de, @para, @visto, @nome, @extencao, @tamanho, @data, @premissoes)", connection)
            Command2.Parameters.AddWithValue("@ID", max_id_var)
            Command2.Parameters.AddWithValue("@de", lbl_uploader.Text)
            Command2.Parameters.AddWithValue("@para", premissoes_v)
            Command2.Parameters.AddWithValue("@visto", premissoes_v)
            Command2.Parameters.AddWithValue("@nome", get_nome)
            Command2.Parameters.AddWithValue("@extencao", get_extension)
            Command2.Parameters.AddWithValue("@tamanho", total_size)
            Command2.Parameters.AddWithValue("@data", data)
            Command2.Parameters.AddWithValue("@premissoes", premissoes_v)
            connection.Open()
            Command2.ExecuteNonQuery()
            connection.Close()

            lbl_data.Visible = True
            lbl_data.Text = data

            txt_caminho.Text = ""

            lbl_aviso.ForeColor = Color.Green
            lbl_aviso.Text = "O ficheiro foi enviado com sucesso!"

            actualiza_dados()
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
            connection.Close()
        End Try
    End Sub
#End Region

#Region "Receber Ficheiro"
    Private Sub ListV_pub_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListV_pub.SelectedIndexChanged
        Try
            If btn_download.Enabled = False Then
                btn_download.Enabled = True
            End If

            If ListV_pub.SelectedItems.Item(0).SubItems(0).Text = main.login_txt.Text Then
                btn_eliminar.Enabled = True
            Else
                btn_eliminar.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btn_download_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_download.Click

        Try
            ListV_pub.Select()

            Dim command3 As New SqlCommand("SELECT ID, de, nome, extencao, tamanho, data FROM transferencias WHERE de=@de AND nome=@nome AND premissoes=@premissoes AND visto=@visto", connection)
            command3.Parameters.AddWithValue("@de", ListV_pub.SelectedItems.Item(0).SubItems(0).Text)
            command3.Parameters.AddWithValue("@nome", ListV_pub.SelectedItems.Item(0).SubItems(1).Text)
            command3.Parameters.AddWithValue("@premissoes", premissoes_v)
            command3.Parameters.AddWithValue("@visto", premissoes_v)

            connection.Open()
            id_vr = ""
            Dim reader As SqlDataReader = command3.ExecuteReader()
            Try
                reader.Read()

                'Saca a informaçao
                id_vr = reader("ID")
                de_vr = reader("de")
                nome_vr = reader("nome")
                extencao_vr = reader("extencao")
                tamanho_vr = reader("tamanho")
            Catch
                reader.Close()
                connection.Close()
            End Try
            reader.Close()
            connection.Close()

            'Verifica se o ficheiro existe
            If id_vr = "" Then
                MsgBox("Ficheiro já não está na base de dados.", MsgBoxStyle.Information, "Informação")
                actualiza_dados()
                Exit Sub
            End If

            'Pede pela localizaçao do para guardar o ficheiro
            Dim path As String = ""
            Dim path_original As String = ""
            If Me.FolderBrowserDialog1.ShowDialog = DialogResult.Cancel Then
                Exit Sub
            Else
                FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop
                FolderBrowserDialog1.ShowNewFolderButton = True

                path_original = FolderBrowserDialog1.SelectedPath
            End If
            FolderBrowserDialog1.Dispose()

            'Saca o ficheiro e grava
            path = path_original & "\" & nome_vr
            Dim command2 As New SqlCommand("SELECT ficheiro FROM ficheiros_pub WHERE ID=@ID", connection)
            command2.Parameters.AddWithValue("@ID", id_vr)
            connection.Open()

            Try
hell:
                Dim FicheiroNovo As New IO.FileStream(path, IO.FileMode.CreateNew, IO.FileAccess.Write)
                Dim DataStream As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
                Dim FileStream As New IO.MemoryStream(DataStream)
                FileStream.WriteTo(FicheiroNovo)
                FicheiroNovo.Flush()
                FicheiroNovo.Close()
                FileStream.Flush()
                FileStream.Close()
                connection.Close()
            Catch
                nv_nome = ""
                nv_nome = InputBox("O ficheiro ja existe!" & vbNewLine & "Escreva um nome para o ficheiro", "Ficheiro ja existente")
                If nv_nome = "" Then
                    connection.Close()
                    Exit Sub
                Else
                    path = path_original & "\" & nv_nome & extencao_vr
                    GoTo hell
                End If
            End Try

            'Saca ID
            Dim max_id_var As Long
            Dim command7 As New SqlCommand("SELECT MAX(ID) as max_id FROM transferencias", connection)
            connection.Open()
            Dim reader3 As SqlDataReader = command7.ExecuteReader()
            reader3.Read()
            Try
                max_id_var = reader3("max_id")
            Catch
                max_id_var = 0
            End Try
            reader3.Close()
            connection.Close()

            max_id_var = max_id_var + 1

            'Data do server sql
            Dim command5 As New SqlCommand("SELECT GETDATE() as Date", connection)
            Dim data As String = ""
            connection.Open()
            Dim reader4 As SqlDataReader = command5.ExecuteReader()
            reader4.Read()
            data = reader4("Date")
            connection.Close()

            Dim nada As String = ""
            'Escreve o registo da transferencia
            Dim Command1 As New SqlCommand("INSERT INTO transferencias (ID, de, para, visto, nome, extencao, tamanho, data, premissoes) VALUES (@ID, @de, @para, @visto, @nome, @extencao, @tamanho, @data, @premissoes)", connection)
            Command1.Parameters.AddWithValue("@ID", max_id_var)
            Command1.Parameters.AddWithValue("@de", de_vr)
            Command1.Parameters.AddWithValue("@para", main.login_txt.Text)
            Command1.Parameters.AddWithValue("@visto", nada)
            Command1.Parameters.AddWithValue("@nome", nome_vr)
            Command1.Parameters.AddWithValue("@extencao", extencao_vr)
            Command1.Parameters.AddWithValue("@tamanho", tamanho_vr)
            Command1.Parameters.AddWithValue("@data", data)
            Command1.Parameters.AddWithValue("@premissoes", premissoes_v)
            connection.Open()
            Command1.ExecuteNonQuery()
            connection.Close()

            btn_download.Enabled = False
            btn_eliminar.Enabled = False

            actualiza_dados()
            MsgBox("Ficheiro recebido com sucesso!", MsgBoxStyle.Information, "Informação")
        Catch
            'MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
            connection.Close()
        End Try
    End Sub
#End Region

#Region "Eliminar Ficheiro"
    Private Sub btn_eliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_eliminar.Click
        Try
            ListV_pub.Select()

            Dim a As String = MsgBox("Deseja eliminar o ficheiro da rede pública?", MsgBoxStyle.YesNo, "Informação")
            If a = vbNo Then
                connection.Close()
                Exit Sub
            End If

            Dim command3 As New SqlCommand("SELECT ID FROM transferencias WHERE de=@de AND nome=@nome AND premissoes=@premissoes AND visto=@visto", connection)
            command3.Parameters.AddWithValue("@de", main.login_txt.Text)
            command3.Parameters.AddWithValue("@nome", ListV_pub.SelectedItems.Item(0).SubItems(1).Text)
            command3.Parameters.AddWithValue("@premissoes", premissoes_v)
            command3.Parameters.AddWithValue("@visto", premissoes_v)

            connection.Open()
            Dim reader As SqlDataReader = command3.ExecuteReader()
            Try
                reader.Read()
                'Saca a informaçao
                id_vr = reader("ID")
                reader.Close()
                connection.Close()
            Catch
                reader.Close()
                connection.Close()
            End Try

            'Apaga o ficheiro
            Dim command4 As New SqlCommand("Delete FROM ficheiros_pub WHERE ID=@ID", connection)
            command4.Parameters.AddWithValue("@ID", id_vr)
            connection.Open()
            command4.ExecuteNonQuery()
            connection.Close()

            'Muda o Visto, apenas para ficar nos logs
            Dim visto_v As String = "Eliminado"

            Dim command7 As New SqlCommand("UPDATE transferencias SET visto=@visto WHERE ID=@ID AND para=@para AND premissoes=@premissoes AND visto='Publico'", connection)
            command7.Parameters.AddWithValue("@ID", id_vr)
            command7.Parameters.AddWithValue("@para", premissoes_v)
            command7.Parameters.AddWithValue("@premissoes", premissoes_v)
            command7.Parameters.AddWithValue("@visto", visto_v)
            connection.Open()
            command7.ExecuteNonQuery()
            connection.Close()

            btn_download.Enabled = False
            btn_eliminar.Enabled = False

            actualiza_dados()
            MsgBox("Ficheiro eliminado com sucesso!", MsgBoxStyle.Information, "Informação")
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
            connection.Close()
        End Try
    End Sub
#End Region

#Region "Actualiza"
    Sub actualiza_dados()

        ListV_historico_down.Items.Clear()
        ListV_historico_up.Items.Clear()
        ListV_pub.Items.Clear()

        'Mostra todos registos de downloads pessoais
        Try
            Dim command2 As New SqlCommand("SELECT de, nome, visto, tamanho, data , premissoes FROM transferencias WHERE para=@para ORDER BY id", connection)
            command2.Parameters.AddWithValue("@para", main.login_txt.Text)
            connection.Open()
            Dim reader As SqlDataReader = command2.ExecuteReader()
            While reader.Read()
                Dim uploader = reader("de")
                Dim nome = reader("nome")
                Dim estado = reader("visto")
                Dim tamanho = reader("tamanho")
                Dim data = reader("data")
                Dim premissoes = reader("premissoes")

                Dim i As ListViewItem = ListV_historico_down.Items.Add(uploader)
                i.SubItems.AddRange(New String() {nome, estado, tamanho, data, premissoes})
            End While

            Try
                ListV_historico_down.Items.Item(0).Selected = True
                ListV_historico_down.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                ListV_historico_down.Columns(3).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                ListV_historico_down.Columns(4).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
            Catch ex As Exception
                ListV_historico_down.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                ListV_historico_down.Columns(3).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                ListV_historico_down.Columns(4).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
            End Try

            reader.Close()
            connection.Close()
        Catch
            connection.Close()
        End Try

        'Mostra todos registos de uploads pessoais
        Try
            Dim command2 As New SqlCommand("SELECT para, nome, visto, tamanho, data FROM transferencias WHERE de=@de AND para<>@para AND visto<>'' ORDER BY id", connection)
            command2.Parameters.AddWithValue("@de", main.login_txt.Text)
            command2.Parameters.AddWithValue("@para", main.login_txt.Text)
            connection.Open()
            Dim reader As SqlDataReader = command2.ExecuteReader()
            While reader.Read()
                Dim downloader = reader("para")
                Dim nome = reader("nome")
                Dim estado = reader("visto")
                Dim tamanho = reader("tamanho")
                Dim data = reader("data")

                Dim i As ListViewItem = ListV_historico_up.Items.Add(downloader)
                i.SubItems.AddRange(New String() {nome, estado, tamanho, data})
            End While

            Try
                ListV_historico_up.Items.Item(0).Selected = True
                ListV_historico_up.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                ListV_historico_up.Columns(3).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                ListV_historico_up.Columns(4).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
            Catch ex As Exception
                ListV_historico_up.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                ListV_historico_up.Columns(3).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                ListV_historico_up.Columns(4).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
            End Try

            reader.Close()
            connection.Close()
        Catch
            connection.Close()
        End Try

        'Mostra todos os registos publicos para download
        Try
            Dim premissoes_v As String = "Publico"
            Dim command2 As New SqlCommand("SELECT de, nome, tamanho, data FROM transferencias WHERE premissoes=@premissoes AND para=@para AND visto=@visto ORDER BY id", connection)
            command2.Parameters.AddWithValue("@para", premissoes_v)
            command2.Parameters.AddWithValue("@premissoes", premissoes_v)
            command2.Parameters.AddWithValue("@visto", premissoes_v)
            connection.Open()
            Dim reader As SqlDataReader = command2.ExecuteReader()
            While reader.Read()
                Dim uploader = reader("de")
                Dim nome = reader("nome")
                Dim tamanho = reader("tamanho")
                Dim data = reader("data")

                Dim i As ListViewItem = ListV_pub.Items.Add(uploader)
                i.SubItems.AddRange(New String() {nome, tamanho, data})
            End While

            Try
                ListV_pub.Items.Item(0).Selected = True
                ListV_pub.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
                ListV_pub.Columns(3).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
            Catch ex As Exception
                ListV_pub.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
                ListV_pub.Columns(3).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
            End Try

            reader.Close()
            connection.Close()
        Catch
            connection.Close()
        End Try

        'Mostra todas as chamadas recebidas
        Try
            Dim command2 As New SqlCommand("SELECT de, visto, data FROM voip WHERE para=@para ORDER BY id", connection)
            command2.Parameters.AddWithValue("@para", main.login_txt.Text)
            connection.Open()
            Dim reader As SqlDataReader = command2.ExecuteReader()
            While reader.Read()
                Dim de = reader("de")
                Dim estado = reader("visto")
                Dim data = reader("data")

                Dim i As ListViewItem = listV_recebidas.Items.Add(de)
                i.SubItems.AddRange(New String() {estado, data})
            End While

            Try
                listV_recebidas.Items.Item(0).Selected = True
                listV_recebidas.Columns(2).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
            Catch ex As Exception
                listV_recebidas.Columns(2).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
            End Try

            reader.Close()
            connection.Close()
        Catch
            connection.Close()
        End Try

        'Mostra todas as chamadas efectuadas
        Try
            Dim command2 As New SqlCommand("SELECT para, visto, data FROM voip WHERE de=@de ORDER BY id", connection)
            command2.Parameters.AddWithValue("@de", main.login_txt.Text)
            connection.Open()
            Dim reader As SqlDataReader = command2.ExecuteReader()
            While reader.Read()
                Dim para = reader("para")
                Dim estado = reader("visto")
                Dim data = reader("data")

                Dim i As ListViewItem = listV_efectuadas.Items.Add(para)
                i.SubItems.AddRange(New String() {estado, data})
            End While

            Try
                listV_efectuadas.Items.Item(0).Selected = True
                listV_efectuadas.Columns(2).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
            Catch ex As Exception
                listV_efectuadas.Columns(2).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
            End Try

            reader.Close()
            connection.Close()
        Catch
            connection.Close()
        End Try
    End Sub
#End Region

End Class