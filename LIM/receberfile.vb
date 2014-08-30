Imports System.Data.SqlClient

Public Class receberfile

    Dim id_vr As String = ""
    Dim nome_vr As String = ""
    Dim extencao_vr As String = ""
    Dim nv_nome As String = ""

#Region "Form Load"

    Private Sub receberfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.BackgroundImage = Image.FromFile(appPath + "\Skin\receber_bg.png")
        Catch ex As Exception
        End Try

        If main.nv_download = True Then
            Dim visto_v As String = "Nao"
            Dim premissoes_v As String = "Privado"

            Dim command3 As New SqlCommand("SELECT ID, de, nome, extencao, tamanho, data FROM transferencias WHERE para=@para AND visto=@visto AND premissoes=@premissoes", connection)
            command3.Parameters.AddWithValue("@para", main.login_txt.Text)
            command3.Parameters.AddWithValue("@visto", visto_v)
            command3.Parameters.AddWithValue("@premissoes", premissoes_v)
            connection.Open()
            Dim reader As SqlDataReader = command3.ExecuteReader()
            Try
                reader.Read()

                'Saca a informaçao
                id_vr = reader("ID")
                nome_vr = reader("nome")
                extencao_vr = reader("extencao")
                Dim de_vr = reader("de")
                Dim tamanho_vr = reader("tamanho")
                Dim data_vr = reader("data")

                lbl_envio.Text = de_vr
                lbl_nomef.Text = nome_vr
                lbl_tamanho.Text = tamanho_vr
                lbl_data.Text = data_vr
            Catch
                reader.Close()
                connection.Close()
            End Try

            extencao_vr = extencao_vr.ToLower

            If extencao_vr = ".avi" Then
                px_img.Image = My.Resources.avi
            ElseIf extencao_vr = ".bmp" Then
                px_img.Image = My.Resources.bmp
            ElseIf extencao_vr = ".divx" Then
                px_img.Image = My.Resources.divx
            ElseIf extencao_vr = ".dll" Then
                px_img.Image = My.Resources.dll
            ElseIf extencao_vr = ".xlsx" Or extencao_vr = ".xlsm" Or extencao_vr = ".xlsb" Or extencao_vr = ".xls" Or extencao_vr = ".xltx" Or extencao_vr = ".xltm" Or extencao_vr = ".xlt" Then
                px_img.Image = My.Resources.excel
            ElseIf extencao_vr = ".html" Or extencao_vr = ".htm" Or extencao_vr = ".mht" Or extencao_vr = ".mhtml" Then
                px_img.Image = My.Resources.firefox
            ElseIf extencao_vr = ".flv" Or extencao_vr = ".swf" Or extencao_vr = ".fla" Then
                px_img.Image = My.Resources.flv
            ElseIf extencao_vr = ".iso" Then
                px_img.Image = My.Resources.iso
            ElseIf extencao_vr = ".jpeg" Or extencao_vr = ".jpg" Then
                px_img.Image = My.Resources.jpeg
            ElseIf extencao_vr = ".max" Then
                px_img.Image = My.Resources.max
            ElseIf extencao_vr = ".mov" Then
                px_img.Image = My.Resources.mov
            ElseIf extencao_vr = ".mp3" Then
                px_img.Image = My.Resources.mp3
            ElseIf extencao_vr = ".mp4" Then
                px_img.Image = My.Resources.mp4
            ElseIf extencao_vr = ".mpeg" Then
                px_img.Image = My.Resources.mpeg
            ElseIf extencao_vr = ".one" Or extencao_vr = ".onepkg" Then
                px_img.Image = My.Resources.onenote
            ElseIf extencao_vr = ".pdf" Then
                px_img.Image = My.Resources.pdf
            ElseIf extencao_vr = ".pptx" Or extencao_vr = ".pptm" Or extencao_vr = ".ppt" Or extencao_vr = ".potx" Or extencao_vr = ".potm" Or extencao_vr = ".ppsx" Or extencao_vr = ".pps" Or extencao_vr = ".ppsx" Then
                px_img.Image = My.Resources.powerpoint
            ElseIf extencao_vr = ".psd" Then
                px_img.Image = My.Resources.psd
            ElseIf extencao_vr = ".png" Then
                px_img.Image = My.Resources.png
            ElseIf extencao_vr = ".pub" Then
                px_img.Image = My.Resources.publisher
            ElseIf extencao_vr = ".rar" Then
                px_img.Image = My.Resources.rar
            ElseIf extencao_vr = ".rm" Or extencao_vr = ".rmvb" Then
                px_img.Image = My.Resources.rm
            ElseIf extencao_vr = ".rtf" Then
                px_img.Image = My.Resources.rtf
            ElseIf extencao_vr = ".torrent" Then
                px_img.Image = My.Resources.torrent
            ElseIf extencao_vr = ".txt" Then
                px_img.Image = My.Resources.txt
            ElseIf extencao_vr = ".vsd" Or extencao_vr = ".vss" Or extencao_vr = ".vst" Or extencao_vr = ".vdx" Or extencao_vr = ".vsx" Or extencao_vr = ".vtx" Then
                px_img.Image = My.Resources.visio
            ElseIf extencao_vr = ".vob" Or extencao_vr = ".srt" Then ' aha uma nova agr
                px_img.Image = My.Resources.vob
            ElseIf extencao_vr = ".wav" Then
                px_img.Image = My.Resources.wav
            ElseIf extencao_vr = ".wma" Then
                px_img.Image = My.Resources.wma
            ElseIf extencao_vr = ".doc" Or extencao_vr = ".docx" Or extencao_vr = ".docm" Or extencao_vr = ".dotx" Or extencao_vr = ".dotm" Or extencao_vr = ".dot" Then
                px_img.Image = My.Resources.word
            ElseIf extencao_vr = ".zip" Then
                px_img.Image = My.Resources.zip
            ElseIf extencao_vr = ".gif" Then
                px_img.Image = My.Resources.photo
            ElseIf extencao_vr = ".mkv" Then
                px_img.Image = My.Resources.movie
            ElseIf extencao_vr = ".m3u" Or extencao_vr = ".midi" Then
                px_img.Image = My.Resources.music
            Else
                px_img.Image = My.Resources.file
            End If
            reader.Close()
            connection.Close()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub receberfile_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        main.nv_download = False
    End Sub
#End Region

#Region "Aceitar"

    Private Sub btn_aceitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_aceitar.Click
        Try
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
            Dim command2 As New SqlCommand("SELECT ficheiro FROM ficheiros_priv WHERE ID=@ID", connection)
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

            'Apaga o ficheiro
            Dim command4 As New SqlCommand("Delete FROM ficheiros_priv WHERE ID=@ID", connection)
            command4.Parameters.AddWithValue("@ID", id_vr)
            connection.Open()
            command4.ExecuteNonQuery()
            connection.Close()

            'Muda o Visto, apenas para ficar nos logs
            Dim visto_v As String = "Aceite"
            Dim premissoes_v As String = "Privado"

            Dim command3 As New SqlCommand("UPDATE transferencias SET visto=@visto WHERE ID=@ID AND para=@para AND premissoes=@premissoes AND visto='Nao'", connection)
            command3.Parameters.AddWithValue("@ID", id_vr)
            command3.Parameters.AddWithValue("@para", main.login_txt.Text)
            command3.Parameters.AddWithValue("@premissoes", premissoes_v)
            command3.Parameters.AddWithValue("@visto", visto_v)
            connection.Open()
            command3.ExecuteNonQuery()
            connection.Close()

            Me.Close()
            MsgBox("Ficheiro recebido com sucesso!", MsgBoxStyle.Information, "Informação")
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
            connection.Close()
        End Try
    End Sub
#End Region

#Region "Cancelar"

    Private Sub btn_cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancelar.Click
        Try
            Dim a As String
            a = MsgBox("Tem mesmo a certeza que quer cancelar?", MsgBoxStyle.YesNo, "Informação")
            If a = vbNo Then
                connection.Close()
                Exit Sub
            End If

            'Apaga o ficheiro
            Dim command4 As New SqlCommand("Delete FROM ficheiros_priv WHERE ID=@ID", connection)
            command4.Parameters.AddWithValue("@ID", id_vr)
            connection.Open()
            command4.ExecuteNonQuery()
            connection.Close()

            'Muda o Visto, apenas para ficar nos logs
            Dim visto_v As String = "Cancelado"
            Dim premissoes_v As String = "Privado"

            Dim command3 As New SqlCommand("UPDATE transferencias SET visto=@visto WHERE ID=@ID AND para=@para AND premissoes=@premissoes AND visto='Nao'", connection)
            command3.Parameters.AddWithValue("@ID", id_vr)
            command3.Parameters.AddWithValue("@para", main.login_txt.Text)
            command3.Parameters.AddWithValue("@premissoes", premissoes_v)
            command3.Parameters.AddWithValue("@visto", visto_v)
            connection.Open()
            command3.ExecuteNonQuery()
            connection.Close()

            Me.Close()
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
            connection.Close()
        End Try
    End Sub

#End Region

End Class