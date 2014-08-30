Imports System.Data.SqlClient

Public Class Registar

#Region "Registo"
    Private Sub procurar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles procurar_btn.Click
        OpenFileDialog1.Title = "Seleccione o Avatar"
        OpenFileDialog1.FileName = "Nome do Avatar"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Filter = "Ficheiros de Imagem (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|Todos os Ficheiros (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        'janela de seleccionar imagem, ao carregar OK
        link_txt.Text = OpenFileDialog1.FileName.ToString()

        'verifica tamanho da imagem (KB)
        Dim get_size As Integer = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(link_txt.Text).Length() / 1024))
        If get_size > 100 Then
            link_txt.Text = ""
            MsgBox("A imagem nao pode ser maior 100KB !", MsgBoxStyle.Exclamation, "Informação")
            Exit Sub
        Else
            avatar.Image = Image.FromFile(link_txt.Text)
        End If

        ''verifica tamanho da imagem (AltxLarg)
        'Using img As Image = Image.FromFile(link_txt.Text)
        '    If img.Height <= 150 AndAlso img.Width <= 150 Then
        '        stream_link.Close()
        '    Else
        '        link_txt.Text = ""
        '        stream_link.Close()
        '        MsgBox("A imagem nao pode ser maior que 150px por 150px!", MsgBoxStyle.Exclamation, "Erro")
        '    End If
        'End Using

    End Sub

    Private Sub registar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles registar_btn.Click
        Try
            If login_txt.Text = "" Then
                MsgBox("Escreva o seu Login/Nick", MsgBoxStyle.Exclamation, "Informação")
                login_txt.Focus()
            ElseIf password_txt.Text = "" Then
                MsgBox("Escreva uma Password", MsgBoxStyle.Exclamation, "Informação")
                password_txt.Focus()
            Else

                'verifica dupe
                Dim command As New SqlCommand("SELECT count(nome) as quantos FROM t_login WHERE nome=@nome", connection)
                command.Parameters.AddWithValue("@nome", login_txt.Text)
                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()
                While reader.Read() 'Com o While apenas nao da o erro
                    If reader("quantos") = 1 Then
                        MsgBox("Esse login ja existe!", MsgBoxStyle.Exclamation, "Informação")
                        connection.Close()
                        Exit Sub
                    End If
                End While
                reader.Close()
                connection.Close()

                'encrypt pwd
                Dim sha512_password = get_SHA512_Hash(password_txt.Text)
                'adiciona o novo login
                Dim online_v As String = "Offline"
                Dim estado_v As String = "Online"
                Dim command1 As New SqlCommand("INSERT INTO t_login (nome, password, online, estado) VALUES (@nome_lg, @password, @online, @estado)", connection)
                command1.Parameters.AddWithValue("@nome_lg", login_txt.Text)
                command1.Parameters.AddWithValue("@password", sha512_password)
                command1.Parameters.AddWithValue("@online", online_v)
                command1.Parameters.AddWithValue("@estado", estado_v)
                connection.Open()
                command1.ExecuteNonQuery()
                connection.Close()

                'escreve imagem na db
                Dim command2 As New SqlCommand("INSERT INTO avatar (nome, imagem) VALUES (@nome_img, @imagem)", connection)
                If link_txt.Text = "" Then
                    Using picture As Image = My.Resources.avatar
                        Using stream As New IO.MemoryStream
                            picture.Save(stream, Imaging.ImageFormat.Png)
                            command2.Parameters.Add("@imagem", SqlDbType.Image).Value = stream.GetBuffer()
                        End Using
                    End Using
                Else
                    Using picture As Image = Image.FromFile(link_txt.Text)
                        Using stream As New IO.MemoryStream
                            picture.Save(stream, Imaging.ImageFormat.Png)
                            command2.Parameters.Add("@imagem", SqlDbType.Image).Value = stream.GetBuffer()
                        End Using
                    End Using
                End If
                command2.Parameters.AddWithValue("@nome_img", login_txt.Text)
                connection.Open()
                command2.ExecuteNonQuery()
                connection.Close()

                MsgBox("Registado com sucesso!", MsgBoxStyle.Information, "Informação")

                If Login.login_txt.Text = "" And Login.password_txt.Text = "" Then
                    Login.login_txt.Text = login_txt.Text
                    Login.password_txt.Text = password_txt.Text
                End If

                Me.Close()

                Login.BringToFront()

            End If
        Catch
            connection.Close()
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Private Sub Registar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        password_txt.PasswordChar = "●"
        login_txt.Select()
    End Sub

    Private Sub login_txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles login_txt.KeyPress
        If e.KeyChar = Space(1) Then
            e.Handled = True
        End If
    End Sub
#End Region

End Class
