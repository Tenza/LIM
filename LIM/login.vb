Imports System.Data.SqlClient
Imports System.Xml

Public Class Login

#Region "Login"

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ok_btn.Click
        Dim var_login As String = ""
        Dim login_cv As Boolean = False
        Dim sha512_password As String = ""
        Try
            If login_txt.Text = "" Then
                aviso_lbl.Text = "Introduza o seu login."
                aviso_lbl.Visible = True
                auto_login = False
                login_txt.Focus()
            ElseIf password_txt.Text = "" Then
                aviso_lbl.Text = "Introduza a sua password."
                aviso_lbl.Visible = True
                auto_login = False
                password_txt.Focus()
            Else
                bola()

                If auto_login = True Then
                    sha512_password = password_txt.Text
                ElseIf auto_login = False Then
                    sha512_password = get_SHA512_Hash(password_txt.Text)
                End If

                Dim command As New SqlCommand("SELECT nome, password, count(nome) as quantos FROM t_login WHERE nome=@login AND password=@password GROUP BY nome, password", connection)
                command.Parameters.AddWithValue("@login", login_txt.Text)
                command.Parameters.AddWithValue("@password", sha512_password)
                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()
                Try
                    reader.Read()
                    var_login = reader("nome")
                    Dim var_pass = reader("password")
                    Dim var_read = reader("quantos")
                    If var_read = 1 Then
                        If var_pass = sha512_password Then
                            login_cv = True
                        End If
                    End If
                    reader.Close()
                    connection.Close()
                Catch
                    'Erro
                    reader.Close()
                    connection.Close()
                    var_login = ""
                    login_cv = False
                    aviso_lbl.Text = "Login/Password Errado !"
                    aviso_lbl.Visible = True
                    roda()
                    If auto_login = True Then
                        password_txt.Text = ""
                        auto_login = False
                    End If
                End Try
            End If

            If login_cv = True Then
                'verifica estado da conta
                Dim online_true As Boolean = False
                Dim command2 As New SqlCommand("SELECT online FROM t_login WHERE nome=@nome", connection)
                command2.Parameters.AddWithValue("@nome", var_login)
                connection.Open()
                Dim reader2 As SqlDataReader = command2.ExecuteReader()
                reader2.Read()
                If reader2("online") = "Ban" Then
                    aviso_lbl.Visible = True
                    aviso_lbl.Text = "O utilizador está banido !"
                    password_txt.Text = ""
                    auto_login = False
                    var_login = ""
                    roda()
                    reader2.Close()
                    connection.Close()
                ElseIf reader2("online") = "Online" Then
                    If auto_login = False Then
                        online_true = True
                    ElseIf auto_login = True Then
                        aviso_lbl.Visible = True
                        aviso_lbl.Text = "O utilizador ja está conectado !"
                        password_txt.Text = ""
                        auto_login = False
                        var_login = ""
                        roda()
                    End If
                    reader2.Close()
                    connection.Close()
                Else
                    reader2.Close()
                    connection.Close()
                    main.login_txt.Text = var_login
                    main.Show()
                    Registar.Close()
                    connect.Close()
                    Me.Close()
                End If

                If online_true = True Then
                    Dim a As String = ""
                    a = MsgBox("Esse user ja esta conectado!" & vbNewLine & "Deseja desconectar a conta e entrar?", MsgBoxStyle.YesNo, "Informação")
                    If a = vbYes Then
                        'altera estado para Offline e entra na conta
                        Dim online_v2 As String = "Offline"
                        Dim command5 As New SqlCommand("UPDATE t_login SET online=@online WHERE nome=@nome ", connection)
                        command5.Parameters.AddWithValue("@nome", var_login)
                        command5.Parameters.AddWithValue("@online", online_v2)
                        connection.Open()
                        command5.ExecuteNonQuery()
                        connection.Close()
                        Threading.Thread.Sleep(1000) 'para dar tempo do outro dar DC
                        main.login_txt.Text = var_login
                        main.Show()
                        Registar.Close()
                        connect.Close()
                        Me.Close()
                    ElseIf a = vbNo Then
                        connection.Close()
                        var_login = ""
                        roda()
                        Exit Sub
                    End If
                End If
            End If
        Catch
            auto_login = False
            password_txt.Text = ""
            connection.Close()
            var_login = ""
            roda()
            main.Dispose()
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Opt"
    Private Sub bola()
        roda_pb.Visible = False
        bola_pb.Visible = True
        Cursor = Cursors.AppStarting
        Me.Refresh()
    End Sub

    Private Sub roda()
        bola_pb.Visible = False
        roda_pb.Visible = True
        Cursor = Cursors.Default
        Me.Refresh()
    End Sub

    Private Sub registar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles registar_btn.Click
        Registar.Show()
    End Sub

    Private Sub login_txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles login_txt.KeyPress
        If e.KeyChar = Space(1) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        password_txt.PasswordChar = "●"

        If System.IO.File.Exists(appPath + "\Config.xml") = False Then
            Dim writer As New XmlTextWriter(appPath + "\Config.xml", System.Text.Encoding.UTF8)

            writer.WriteStartDocument(True)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 5

            writer.WriteComment("Configuração Geral")

            'Tabela do tipo de CNN
            writer.WriteStartElement("Table")
            writer.WriteStartElement("Cnntype")
            writer.WriteStartElement("type")
            writer.WriteString("Local")
            writer.WriteEndElement()
            writer.WriteEndElement()

            'Atributos da CNN Local
            writer.WriteStartElement("Local")
            writer.WriteStartElement("filepath")
            writer.WriteString("|DataDirectory|\LIM_db.mdf")
            writer.WriteEndElement()
            writer.WriteEndElement()

            'Atributos da CNN Lan
            writer.WriteStartElement("Lan")
            writer.WriteStartElement("filepath")
            writer.WriteEndElement()
            writer.WriteStartElement("datasource")
            writer.WriteEndElement()
            writer.WriteStartElement("usersql")
            writer.WriteEndElement()
            writer.WriteStartElement("passsql")
            writer.WriteEndElement()
            writer.WriteEndElement()

            'Atributos da Aparencia
            writer.WriteStartElement("Definicoes")
            writer.WriteStartElement("Aparencia")
            writer.WriteStartElement("geral")
            writer.WriteString("1")
            writer.WriteEndElement()
            writer.WriteStartElement("avatar")
            writer.WriteString("0")
            writer.WriteEndElement()
            writer.WriteStartElement("conversa")
            writer.WriteString("0")
            writer.WriteEndElement()
            writer.WriteStartElement("amigos")
            writer.WriteString("0")
            writer.WriteEndElement()
            writer.WriteStartElement("cor")
            writer.WriteString("Black")
            writer.WriteEndElement()
            writer.WriteEndElement()

            'Atributos das Opçoes
            writer.WriteStartElement("Opcoes")
            writer.WriteStartElement("autorun")
            writer.WriteString("0")
            writer.WriteEndElement()
            writer.WriteStartElement("open")
            writer.WriteString("1")
            writer.WriteEndElement()
            writer.WriteStartElement("iniciar_conta")
            writer.WriteString("0")
            writer.WriteEndElement()
            writer.WriteStartElement("ver_data")
            writer.WriteString("0")
            writer.WriteEndElement()
            writer.WriteStartElement("ver_historico")
            writer.WriteString("0")
            writer.WriteEndElement()
            writer.WriteStartElement("auto_conta")
            writer.WriteString("")
            writer.WriteEndElement()
            writer.WriteStartElement("auto_estado")
            writer.WriteString("Utilizar o ultimo estado")
            writer.WriteEndElement()
            writer.WriteEndElement()

            'Atributos da Resoluçao
            writer.WriteStartElement("Resolucao")
            writer.WriteStartElement("estado")
            writer.WriteString("non-full")
            writer.WriteEndElement()
            writer.WriteStartElement("tamanho_w")
            writer.WriteString("720")
            writer.WriteEndElement()
            writer.WriteStartElement("tamanho_h")
            writer.WriteString("385")
            writer.WriteEndElement()
            writer.WriteEndElement()

            'Atributos do Tipo de Letra
            writer.WriteStartElement("Letra")
            writer.WriteStartElement("tipo")
            writer.WriteString("Microsoft Sans Serif")
            writer.WriteEndElement()
            writer.WriteStartElement("tamanho")
            writer.WriteString("8")
            writer.WriteEndElement()
            writer.WriteStartElement("cor")
            writer.WriteString("Black")
            writer.WriteEndElement()
            writer.WriteEndElement()

            writer.WriteEndElement()

            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()

            connect.Show()
            cnn() 'aplica logo a cnn pa nao dar erro ao tentar ligar
        Else
            cnn()
        End If

        If terminar_sessao = False Then
            'Lê Opçoes
            doc = XDocument.Load(appPath + "\config.xml")
            Dim qList2 = (From xe In doc.Descendants.Elements("Opcoes") _
            Select New With { _
            .iniciar_conta = xe.<iniciar_conta>.Value, _
            .auto_conta = xe.<auto_conta>.Value, _
            .auto_estado = xe.<auto_estado>.Value _
            }).First
            iniciar_conta_id = qList2.iniciar_conta
            auto_conta_id = qList2.auto_conta
            auto_estado_id = qList2.auto_estado

            If iniciar_conta_id = 1 Then
                Try
                    auto_login = True
                    doc = XDocument.Load(appPath + "\acc.xml")
                    Dim qList = (From xe In doc.Descendants.Elements("Account") _
                              Where xe.Elements("Login").Value = auto_conta_id _
                              Select New With { _
                                       .Login = xe.<Login>.Value, _
                                       .Password = xe.<Password>.Value _
                                       }).FirstOrDefault
                    login_txt.Text = qList.Login
                    password_txt.Text = qList.Password
                    OK_Click(Nothing, Nothing)
                Catch ex As Exception
                    auto_login = False
                    If System.IO.File.Exists(appPath + "\acc.xml") = True Then
                        MsgBox("Ocorreu um erro ao iniciar o programa" & vbNewLine & "Provavelmente derivado de uma edição exterior do ficheiro .xml", MsgBoxStyle.Critical, "Erro")
                    End If
                End Try
            End If
        End If
    End Sub

    Private Sub alterar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles alterar_btn.Click
        connect.Show()
    End Sub

    'Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim command2 As New SqlCommand("Delete From amigos", connection)
    '    Dim command3 As New SqlCommand("Delete From avatar", connection)
    '    Dim command4 As New SqlCommand("Delete From sms", connection)
    '    Dim command5 As New SqlCommand("Delete From t_login", connection)
    '    connection.Open()
    '    command2.ExecuteNonQuery()
    '    command3.ExecuteNonQuery()
    '    command4.ExecuteNonQuery()
    '    command5.ExecuteNonQuery()
    '    connection.Close()
    'End Sub
#End Region

End Class
