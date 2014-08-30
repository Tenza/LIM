Imports System.Data.SqlClient
Imports Microsoft.Win32
Imports System.Globalization

Module Functions

    Public connection As SqlConnection
    Public connection_act As SqlConnection
    Public connection_voip As SqlConnection
    Public connection_voip_send As SqlConnection
    Public connection_voip_recv As SqlConnection

    Public doc As New XDocument
    Public appPath As String = Application.StartupPath

    Public aparecia_geral_id As Integer
    Public aparecia_avatar_id As Integer
    Public aparecia_conversa_id As Integer
    Public aparecia_amigos_id As Integer
    Public aparecia_cor_id As String

    Public autorun_id As Integer
    Public auto_open_id As Integer
    Public iniciar_conta_id As Integer
    Public ver_data_id As Integer
    Public ver_historico_id As Integer
    Public auto_conta_id As String
    Public auto_estado_id As String

    Public letra_tipo As String
    Public letra_tamanho As Integer
    Public letra_cor As String

    Public terminar_sessao As Boolean = False
    Public auto_login As Boolean = False

    Public main_form_id As Boolean = False
    Public testar_aparencia As Boolean


    Function get_SHA512_Hash(ByVal strToHash As String) As String
        Dim sha512Obj As New Security.Cryptography.SHA512Managed
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)
        bytesToHash = sha512Obj.ComputeHash(bytesToHash)
        Dim strResult As String = ""
        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next
        Return strResult
    End Function

    Function HexToColor(ByVal hexColor As String) As Color
        If hexColor.IndexOf("#"c) <> -1 Then
            hexColor = hexColor.Replace("#", "")
        End If
        Dim red As Integer = 0
        Dim green As Integer = 0
        Dim blue As Integer = 0
        If hexColor.Length = 6 Then
            red = Integer.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier)
            green = Integer.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier)
            blue = Integer.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier)
        ElseIf hexColor.Length = 3 Then
            red = Integer.Parse(hexColor(0).ToString() + hexColor(0).ToString(), NumberStyles.AllowHexSpecifier)
            green = Integer.Parse(hexColor(1).ToString() + hexColor(1).ToString(), NumberStyles.AllowHexSpecifier)
            blue = Integer.Parse(hexColor(2).ToString() + hexColor(2).ToString(), NumberStyles.AllowHexSpecifier)
        End If
        Return Color.FromArgb(red, green, blue)
    End Function

    Sub cnn()
        Try
            'Le a cnn
            doc = XDocument.Load(appPath + "\config.xml")
            Dim tycnn = (From xe In doc.Descendants.Elements("Cnntype") _
                    Select New With {.type = xe.<type>.Value}).First

            'Data Source=(LocalDB)\v11.0;AttachDbFilename="C:\Users\Filipe\Desktop\Projecto de Aptidão Tecnológica\LIM\bin\Debug\LIM_db.mdf";Integrated Security=True;Connect Timeout=30

            If tycnn.type = "Local" Then
                Dim qlist = (From xe In doc.Descendants.Elements("Local") _
                        Select New With {.filepath = xe.<filepath>.Value}).First
                Dim cnn1 As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=" & qlist.filepath & ";Integrated Security=True;Connect Timeout=30;")
                connection = cnn1
                Dim cnn2 As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=" & qlist.filepath & ";Integrated Security=True;Connect Timeout=30;")
                connection_act = cnn2
                Dim cnn3 As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=" & qlist.filepath & ";Integrated Security=True;Connect Timeout=30;")
                connection_voip = cnn3
                Dim cnn4 As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=" & qlist.filepath & ";Integrated Security=True;Connect Timeout=30;")
                connection_voip_send = cnn4
                Dim cnn5 As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=" & qlist.filepath & ";Integrated Security=True;Connect Timeout=30;")
                connection_voip_recv = cnn5
            ElseIf tycnn.type = "Lan" Then
                Dim qList = (From xe In doc.Descendants.Elements("Lan") _
                    Select New With { _
                    .filepath = xe.<filepath>.Value, _
                    .datasource = xe.<datasource>.Value, _
                    .usersql = xe.<usersql>.Value, _
                    .passsql = xe.<passsql>.Value _
                    }).First
                Dim cnn1 As New SqlConnection("Data Source=" & qList.datasource & "\SQLEXPRESS;Initial Catalog=" & qList.filepath & ";User Id=" & qList.usersql & ";Password=" & qList.passsql)
                connection = cnn1
                Dim cnn2 As New SqlConnection("Data Source=" & qList.datasource & "\SQLEXPRESS;Initial Catalog=" & qList.filepath & ";User Id=" & qList.usersql & ";Password=" & qList.passsql)
                connection_act = cnn2
                Dim cnn3 As New SqlConnection("Data Source=" & qList.datasource & "\SQLEXPRESS;Initial Catalog=" & qList.filepath & ";User Id=" & qList.usersql & ";Password=" & qList.passsql)
                connection_voip = cnn3
                Dim cnn4 As New SqlConnection("Data Source=" & qList.datasource & "\SQLEXPRESS;Initial Catalog=" & qList.filepath & ";User Id=" & qList.usersql & ";Password=" & qList.passsql)
                connection_voip_send = cnn4
                Dim cnn5 As New SqlConnection("Data Source=" & qList.datasource & "\SQLEXPRESS;Initial Catalog=" & qList.filepath & ";User Id=" & qList.usersql & ";Password=" & qList.passsql)
                connection_voip_recv = cnn5
            End If
        Catch

        End Try
    End Sub

    Sub apply_content()
        'Actualiza as opçoes
        'Se vier do form load do main OU do cancelar para ler o conteudo de origem e aplicar
        If main_form_id = True Then
            doc = XDocument.Load(appPath + "\config.xml")
            Dim qList = (From xe In doc.Descendants.Elements("Aparencia") _
            Select New With { _
            .geral = xe.<geral>.Value, _
            .avatar = xe.<avatar>.Value, _
            .conversa = xe.<conversa>.Value, _
            .amigos = xe.<amigos>.Value, _
            .cor = xe.<cor>.Value _
            }).First
            aparecia_geral_id = qList.geral
            aparecia_avatar_id = qList.avatar
            aparecia_conversa_id = qList.conversa
            aparecia_amigos_id = qList.amigos
            aparecia_cor_id = qList.cor

            'Tipo de letra
            Dim qList2 = (From xe In doc.Descendants.Elements("Letra") _
            Select New With { _
            .tipo = xe.<tipo>.Value, _
            .tamanho = xe.<tamanho>.Value, _
            .cor = xe.<cor>.Value _
            }).First

            'Protecçoes, antes de aplicar
            If qList2.tipo = "Arial" Or qList2.tipo = "Lucida" Or qList2.tipo = "Microsoft Sans Serif" Or qList2.tipo = "Courier New" Or qList2.tipo = "Times New Roman" Then
                letra_tipo = qList2.tipo
            Else
                letra_tipo = "Microsoft Sans Serif"
            End If
            If qList2.tamanho = "8" Or qList2.tamanho = "9" Or qList2.tamanho = "10" Or qList2.tamanho = "11" Or qList2.tamanho = "12" Then
                letra_tamanho = qList2.tamanho
            Else
                letra_tamanho = "8"
            End If
            letra_cor = qList2.cor

            'Aplica a box de escrita
            Dim cor_tipo2 As String = InStr(letra_cor, "#") '1
            If cor_tipo2 = 1 Then
                Dim RGBcolor = HexToColor(letra_cor)
                main.sms_txt.ForeColor = RGBcolor
            Else
                main.sms_txt.ForeColor = Color.FromName(letra_cor)
            End If
            main.sms_txt.Font = New Font(letra_tipo, letra_tamanho, FontStyle.Regular)

            'Define o historico e data
            Dim qList3 = (From xe In doc.Descendants.Elements("Opcoes") _
            Select New With { _
            .ver_data = xe.<ver_data>.Value, _
            .ver_historico = xe.<ver_historico>.Value _
            }).First
            ver_data_id = qList3.ver_data
            ver_historico_id = qList3.ver_historico

            'Protecçoes, assim aparece sempre
            If ver_historico_id <> 0 And ver_historico_id <> 1 Then
                ver_historico_id = 1
            End If

            If ver_data_id <> 0 And ver_data_id <> 1 Then
                ver_data_id = 1
            End If

            main_form_id = False

        ElseIf main_form_id = False Then
            If testar_aparencia = False Then
                apply_opcoes()
            End If
        End If

        main.WindowState = FormWindowState.Normal
        main.Hide()
        Dim current_size As System.Drawing.Size = main.Size

        main.Size = New System.Drawing.Size(720, 385)

        If aparecia_geral_id = 1 Then
            'avatar, conversa, amigos (default)
            main.p_avatar.Location = New Point(3, 26)
            main.p_avatar.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left
            main.p_conv.Location = New Point(160, 26)
            main.p_conv.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.p_amigos.Location = New Point(579, 27)
            main.p_amigos.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
        ElseIf aparecia_geral_id = 2 Then
            'conversa,amigos,avatar
            main.p_conv.Location = New Point(5, 26)
            main.p_conv.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.p_amigos.Location = New Point(424, 27)
            main.p_amigos.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
            main.p_avatar.Location = New Point(563, 26)
            main.p_avatar.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
        ElseIf aparecia_geral_id = 3 Then
            'amigos,avatar,conversa
            main.p_amigos.Location = New Point(5, 27)
            main.p_amigos.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left
            main.p_avatar.Location = New Point(143, 26)
            main.p_avatar.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left
            main.p_conv.Location = New Point(300, 26)
            main.p_conv.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
        ElseIf aparecia_geral_id = 4 Then
            'amigos, conversa, avatar
            main.p_amigos.Location = New Point(5, 27)
            main.p_amigos.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left
            main.p_conv.Location = New Point(146, 26)
            main.p_conv.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.p_avatar.Location = New Point(563, 26)
            main.p_avatar.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
        ElseIf aparecia_geral_id = 5 Then
            'conversa,avatar,amigos
            main.p_conv.Location = New Point(5, 26)
            main.p_conv.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.p_avatar.Location = New Point(422, 26)
            main.p_avatar.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
            main.p_amigos.Location = New Point(579, 27)
            main.p_amigos.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right
        ElseIf aparecia_geral_id = 6 Then
            'avatar, amigos, conversa
            main.p_avatar.Location = New Point(3, 26)
            main.p_avatar.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left
            main.p_amigos.Location = New Point(159, 27)
            main.p_amigos.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left
            main.p_conv.Location = New Point(300, 26)
            main.p_conv.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
        End If

        If aparecia_avatar_id = 1 Then
            main.meu_avatar_g.Location = New Point(2, 152)
            main.meu_avatar_g.Anchor = AnchorStyles.Bottom + AnchorStyles.Left
            main.outro_avatar_g.Location = New Point(2, 1)
            main.outro_avatar_g.Anchor = AnchorStyles.Top + AnchorStyles.Left
        ElseIf aparecia_avatar_id = 0 Then
            main.meu_avatar_g.Location = New Point(2, 1)
            main.meu_avatar_g.Anchor = AnchorStyles.Top + AnchorStyles.Left
            main.outro_avatar_g.Location = New Point(2, 152)
            main.outro_avatar_g.Anchor = AnchorStyles.Bottom + AnchorStyles.Left
        End If

        If aparecia_conversa_id = 1 Then
            main.lbl_load.Location = New Point(171, 173)
            main.lbl_load.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.sms_rec_txt.Location = New Point(5, 83)
            main.sms_rec_txt.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.sms_txt.Location = New Point(5, 15)
            main.sms_txt.Anchor = AnchorStyles.Top + AnchorStyles.Right + AnchorStyles.Left
        ElseIf aparecia_conversa_id = 0 Then
            main.lbl_load.Location = New Point(171, 139)
            main.lbl_load.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.sms_rec_txt.Location = New Point(5, 15)
            main.sms_rec_txt.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.sms_txt.Location = New Point(5, 285)
            main.sms_txt.Anchor = AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
        End If

        If aparecia_amigos_id = 1 Then
            main.amigos_g.Location = New Point(0, 1)
            main.amigos_g.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.conv_g.Location = New Point(0, 223)
            main.conv_g.Anchor = AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
        ElseIf aparecia_amigos_id = 2 Then
            main.conv_g.Location = New Point(0, 1)
            main.conv_g.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
            main.amigos_g.Location = New Point(0, 136)
            main.amigos_g.Anchor = AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
        ElseIf aparecia_amigos_id = 3 Then
            main.amigos_g.Location = New Point(0, 1)
            main.amigos_g.Anchor = AnchorStyles.Top + AnchorStyles.Right + AnchorStyles.Left
            main.conv_g.Location = New Point(0, 223)
            main.conv_g.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
        ElseIf aparecia_amigos_id = 0 Then
            main.conv_g.Location = New Point(0, 1)
            main.conv_g.Anchor = AnchorStyles.Top + AnchorStyles.Right + AnchorStyles.Left
            main.amigos_g.Location = New Point(0, 136)
            main.amigos_g.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Right + AnchorStyles.Left
        End If

        Dim cor_tipo As String = InStr(aparecia_cor_id, "#") '1
        If cor_tipo = 1 Then
            Dim RGBcolor = HexToColor(aparecia_cor_id)
            main.lbl_tit.ForeColor = RGBcolor
            main.mn_menu.ForeColor = RGBcolor
            main.mn_amigos.ForeColor = RGBcolor
            main.mn_opcoes.ForeColor = RGBcolor
            main.conv_g.ForeColor = RGBcolor
            main.amigos_g.ForeColor = RGBcolor
        Else
            main.lbl_tit.ForeColor = Color.FromName(aparecia_cor_id)
            main.mn_menu.ForeColor = Color.FromName(aparecia_cor_id)
            main.mn_amigos.ForeColor = Color.FromName(aparecia_cor_id)
            main.mn_opcoes.ForeColor = Color.FromName(aparecia_cor_id)
            main.conv_g.ForeColor = Color.FromName(aparecia_cor_id)
            main.amigos_g.ForeColor = Color.FromName(aparecia_cor_id)
        End If

        main.Size = New System.Drawing.Size(current_size)
        main.Show()
    End Sub

    Sub apply_opcoes()
        'Aplica o Autorun
        If autorun_id = 1 Then
            AddCurrentKey("LIM", System.Reflection.Assembly.GetEntryAssembly.Location)
        ElseIf autorun_id = 0 Then
            RemoveCurrentKey("LIM")
        End If

        'Aplica o tipo de letra
        If opt.cb_font.Text = "Arial" Or opt.cb_font.Text = "Lucida" Or opt.cb_font.Text = "Microsoft Sans Serif" Or opt.cb_font.Text = "Courier New" Or opt.cb_font.Text = "Times New Roman" Then
            letra_tipo = opt.cb_font.Text
        Else
            letra_tipo = "Microsoft Sans Serif"
        End If
        If opt.cb_size.Text = "8" Or opt.cb_size.Text = "9" Or opt.cb_size.Text = "10" Or opt.cb_size.Text = "11" Or opt.cb_size.Text = "12" Then
            letra_tamanho = opt.cb_size.Text
        Else
            letra_tamanho = "8"
        End If
        letra_cor = opt.lbl_cor.Text

        'Aplica o currente a box de escrita
        Dim cor_tipo As String = InStr(letra_cor, "#") '1
        If cor_tipo = 1 Then
            Dim RGBcolor = HexToColor(letra_cor)
            main.sms_txt.ForeColor = RGBcolor
        Else
            main.sms_txt.ForeColor = Color.FromName(letra_cor)
        End If
        main.sms_txt.Font = New Font(letra_tipo, letra_tamanho, FontStyle.Regular)

        'Aplica Data
        If opt.check_data.Checked = True Then
            ver_data_id = 1
        ElseIf opt.check_data.Checked = False Then
            ver_data_id = 0
        End If

        'Aplica historico
        If opt.check_historico.Checked = True Then
            ver_historico_id = 1
        ElseIf opt.check_historico.Checked = False Then
            ver_historico_id = 0
        End If
    End Sub

    Private Sub AddCurrentKey(ByVal name As String, ByVal path As String)
        'Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        'key.SetValue(name, path)
    End Sub

    Private Sub RemoveCurrentKey(ByVal name As String)
        'Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        'key.DeleteValue(name, False)
    End Sub

End Module
