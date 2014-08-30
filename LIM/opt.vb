Imports System.Data.SqlClient
Imports System.Xml

Public Class opt

    'Click taskbar
    Private Const GWL_STYLE = (-16)
    Private Const WS_MAXIMIZEBOX = &H10000
    Private Const WS_MINIMIZEBOX = &H20000
    Private Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
    Private Declare Function SetWindowLong Lib "user32.dll" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

#Region "Form Load"
    Private Sub opt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Adiciona o min/max com click na taskbar
        Dim lStyle As Long = GetWindowLong(Handle, GWL_STYLE) Or WS_MAXIMIZEBOX Or WS_MINIMIZEBOX
        SetWindowLong(Handle, GWL_STYLE, lStyle)

        reload_combo()

        'Lê as definiçoes actuais
        doc = XDocument.Load(appPath + "\config.xml")
        main_form_id = False

        '---------------------------------

        'Aparencias
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

        If aparecia_geral_id = 1 Then
            'avatar, conversa, amigos (default)
            p_avatar.Location = New Point(3, 4)
            p_conversa.Location = New Point(126, 4)
            p_amigos.Location = New Point(389, 4)
        ElseIf aparecia_geral_id = 2 Then
            'conversa,amigos,avatar
            p_conversa.Location = New Point(3, 4)
            p_amigos.Location = New Point(266, 4)
            p_avatar.Location = New Point(389, 4)
        ElseIf aparecia_geral_id = 3 Then
            'amigos,avatar,conversa
            p_amigos.Location = New Point(3, 4)
            p_avatar.Location = New Point(126, 4)
            p_conversa.Location = New Point(249, 4)
        ElseIf aparecia_geral_id = 4 Then
            'amigos, conversa, avatar
            p_amigos.Location = New Point(3, 4)
            p_conversa.Location = New Point(126, 4)
            p_avatar.Location = New Point(389, 4)
        ElseIf aparecia_geral_id = 5 Then
            'conversa,avatar,amigos
            p_conversa.Location = New Point(3, 4)
            p_avatar.Location = New Point(266, 4)
            p_amigos.Location = New Point(389, 4)
        ElseIf aparecia_geral_id = 6 Then
            'avatar, amigos, conversa
            p_avatar.Location = New Point(3, 4)
            p_amigos.Location = New Point(126, 4)
            p_conversa.Location = New Point(249, 4)
        End If

        If aparecia_avatar_id = 1 Then
            e_pic2.Location = New Point(2, 3)
            e_pic1.Location = New Point(2, 109)
        ElseIf aparecia_avatar_id = 0 Then
            e_pic1.Location = New Point(2, 3)
            e_pic2.Location = New Point(2, 109)
        End If

        If aparecia_conversa_id = 1 Then
            e_txtp.Location = New Point(3, 3)
            e_txtg.Location = New Point(3, 55)
        ElseIf aparecia_conversa_id = 0 Then
            e_txtg.Location = New Point(3, 3)
            e_txtp.Location = New Point(3, 203)
        End If

        If aparecia_amigos_id = 1 Then
            list_novas.Size = New System.Drawing.Size(109, 56)
            list_contactos.Size = New System.Drawing.Size(109, 186)
            list_contactos.Location = New Point(3, 3)
            list_novas.Location = New Point(3, 193)
        ElseIf aparecia_amigos_id = 2 Then
            list_novas.Size = New System.Drawing.Size(109, 186)
            list_contactos.Size = New System.Drawing.Size(109, 56)
            list_novas.Location = New Point(3, 3)
            list_contactos.Location = New Point(3, 193)
        ElseIf aparecia_amigos_id = 3 Then
            list_novas.Size = New System.Drawing.Size(109, 186)
            list_contactos.Size = New System.Drawing.Size(109, 56)
            list_contactos.Location = New Point(3, 3)
            list_novas.Location = New Point(3, 65)
        ElseIf aparecia_amigos_id = 0 Then
            list_novas.Size = New System.Drawing.Size(109, 56)
            list_contactos.Size = New System.Drawing.Size(109, 186)
            list_novas.Location = New Point(3, 3)
            list_contactos.Location = New Point(3, 65)
        End If

        'Aparencia, iguala ao main
        Radio_principal.Checked = True

        e_pic1.Image = main.avatar_1_img.Image
        e_pic2.Image = main.avatar_2_img.Image
        e_txtg.Text = main.sms_rec_txt.Text
        e_txtp.Text = main.sms_txt.Text

        'Scrol para o fim
        e_txtg.Select(e_txtg.Text.Length, 0)
        e_txtg.ScrollToCaret()

        For i As Integer = 0 To main.list_n_conversas.Items.Count - 1
            list_novas.Items.Add(main.list_n_conversas.Items.Item(i).Text)
        Next
        For i As Integer = 0 To main.list_contactos.Items.Count - 1
            list_contactos.Items.Add(main.list_contactos.Items.Item(i).Text)
        Next
        p_geral.BackgroundImage = main.BackgroundImage

        'Cores ao form
        lbl_mdcor.Text = qList.cor

        Dim cor_tipo As String = InStr(aparecia_cor_id, "#") '1
        If cor_tipo = 1 Then
            Dim RGBcolor = HexToColor(aparecia_cor_id)
            pb_mdcor.BackColor = RGBcolor
        Else
            pb_mdcor.BackColor = Color.FromName(aparecia_cor_id)
        End If
        '---------------------------------

        'Opçoes
        Dim qList2 = (From xe In doc.Descendants.Elements("Opcoes") _
        Select New With { _
        .autorun = xe.<autorun>.Value, _
        .open = xe.<open>.Value, _
        .iniciar_conta = xe.<iniciar_conta>.Value, _
        .ver_data = xe.<ver_data>.Value, _
        .ver_historico = xe.<ver_historico>.Value, _
        .auto_conta = xe.<auto_conta>.Value, _
        .auto_estado = xe.<auto_estado>.Value _
        }).First
        autorun_id = qList2.autorun
        iniciar_conta_id = qList2.iniciar_conta
        ver_data_id = qList2.ver_data
        ver_historico_id = qList2.ver_historico
        auto_conta_id = qList2.auto_conta
        auto_estado_id = qList2.auto_estado
        auto_open_id = qList2.open

        If autorun_id = 1 Then
            Check_autorun.Checked = True
        ElseIf autorun_id = 0 Then
            Check_autorun.Checked = False
        End If

        If auto_open_id = 1 Then
            check_open.Checked = True
        ElseIf auto_open_id = 0 Then
            check_open.Checked = False
        End If

        If cb_contas.Items.Count > 0 Then
            If iniciar_conta_id = 1 Then
                cb_iconta.Text = auto_conta_id 'coloca o nome da conta
                check_iniciarconta.Checked = True
            ElseIf iniciar_conta_id = 0 Then
                check_iniciarconta.Checked = False
            End If
        End If

        'coloca o estado/data/historico
        If auto_estado_id <> "Online" And auto_estado_id <> "Ausente" And auto_estado_id <> "Ocupado" And auto_estado_id <> "Volto Já" And auto_estado_id <> "Utilizar o ultimo estado" Then
            cb_estado.Text = "Utilizar o ultimo estado"
        Else
            cb_estado.Text = auto_estado_id
        End If

        If ver_data_id = 1 Then
            check_data.Checked = True
        ElseIf ver_data_id = 0 Then
            check_data.Checked = False
            lbl_data.Text = 0
        End If

        If ver_historico_id = 1 Then
            check_historico.Checked = True
        ElseIf ver_historico_id = 0 Then
            check_historico.Checked = False
            lbl_historico.Text = 0
        End If

        '---------------------------------

        'Tipos de letra
        Dim qList3 = (From xe In doc.Descendants.Elements("Letra") _
        Select New With { _
        .tipo = xe.<tipo>.Value, _
        .tamanho = xe.<tamanho>.Value, _
        .cor = xe.<cor>.Value _
        }).First

        'Protecçoes, antes de aplicar ao form
        If qList3.tipo = "Arial" Or qList3.tipo = "Lucida" Or qList3.tipo = "Microsoft Sans Serif" Or qList3.tipo = "Courier New" Or qList3.tipo = "Times New Roman" Then
            cb_font.Text = qList3.tipo
        Else
            cb_font.Text = "Microsoft Sans Serif"
        End If
        If qList3.tamanho = "8" Or qList3.tamanho = "9" Or qList3.tamanho = "10" Or qList3.tamanho = "11" Or qList3.tamanho = "12" Then
            cb_size.Text = qList3.tamanho
        Else
            cb_size.Text = "8"
        End If

        lbl_cor.Text = qList3.cor

        cb_font.Items.Add("Arial")
        cb_font.Items.Add("Lucida")
        cb_font.Items.Add("Microsoft Sans Serif")
        cb_font.Items.Add("Courier New")
        cb_font.Items.Add("Times New Roman")

        cb_size.Items.Add("8")
        cb_size.Items.Add("9")
        cb_size.Items.Add("10")
        cb_size.Items.Add("11")
        cb_size.Items.Add("12")

        'Caso seja hexadecimal
        Dim cor_tipo1 As String = InStr(letra_cor, "#") '1
        If cor_tipo1 = 1 Then
            Dim RGBcolor = HexToColor(letra_cor)
            pb_cor.BackColor = RGBcolor
        Else
            pb_cor.BackColor = Color.FromName(letra_cor)
        End If

    End Sub
#End Region

#Region "Aparencia"

#Region "Muda Principal"
    Private Sub btn_esq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_esq.Click
        'Loc  / Size
        '3, 4 / 117, 254
        '126, 4 / 257, 254
        '389, 4 / 117, 254

        If p_amigos.Location = New Point(3, 4) Then
            If p_avatar.Location = New Point(126, 4) Then
                If p_conversa.Location = New Point(249, 4) Then
                    'avatar, conversa, amigos (default)
                    p_avatar.Location = New Point(3, 4)
                    p_conversa.Location = New Point(126, 4)
                    p_amigos.Location = New Point(389, 4)
                    aparecia_geral_id = "1"
                    Exit Sub
                End If
            End If
        End If

        If p_avatar.Location = New Point(3, 4) Then
            If p_conversa.Location = New Point(126, 4) Then
                If p_amigos.Location = New Point(389, 4) Then
                    'conversa,amigos,avatar
                    p_conversa.Location = New Point(3, 4)
                    p_amigos.Location = New Point(266, 4)
                    p_avatar.Location = New Point(389, 4)
                    aparecia_geral_id = "2"
                    Exit Sub
                End If
            End If
        End If

        If p_conversa.Location = New Point(3, 4) Then
            If p_amigos.Location = New Point(266, 4) Then
                If p_avatar.Location = New Point(389, 4) Then
                    'amigos,avatar,conversa
                    p_amigos.Location = New Point(3, 4)
                    p_avatar.Location = New Point(126, 4)
                    p_conversa.Location = New Point(249, 4)
                    aparecia_geral_id = "3"
                    Exit Sub
                End If
            End If
        End If

        '-----------------------------------------------------------------

        If p_avatar.Location = New Point(3, 4) Then
            If p_amigos.Location = New Point(126, 4) Then
                If p_conversa.Location = New Point(249, 4) Then
                    'amigos, conversa, avatar
                    p_amigos.Location = New Point(3, 4)
                    p_conversa.Location = New Point(126, 4)
                    p_avatar.Location = New Point(389, 4)
                    aparecia_geral_id = "4"
                    Exit Sub
                End If
            End If
        End If

        If p_amigos.Location = New Point(3, 4) Then
            If p_conversa.Location = New Point(126, 4) Then
                If p_avatar.Location = New Point(389, 4) Then
                    'conversa,avatar,amigos
                    p_conversa.Location = New Point(3, 4)
                    p_avatar.Location = New Point(266, 4)
                    p_amigos.Location = New Point(389, 4)
                    aparecia_geral_id = "5"
                    Exit Sub
                End If
            End If
        End If

        If p_conversa.Location = New Point(3, 4) Then
            If p_avatar.Location = New Point(266, 4) Then
                If p_amigos.Location = New Point(389, 4) Then
                    'avatar, amigos, conversa
                    p_avatar.Location = New Point(3, 4)
                    p_amigos.Location = New Point(126, 4)
                    p_conversa.Location = New Point(249, 4)
                    aparecia_geral_id = "6"
                    Exit Sub
                End If
            End If
        End If

    End Sub

    Private Sub btn_troca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_troca.Click
        If p_amigos.Location = New Point(3, 4) Then
            If p_conversa.Location = New Point(126, 4) Then
                If p_avatar.Location = New Point(389, 4) Then
                    'avatar, conversa, amigos (default)
                    p_avatar.Location = New Point(3, 4)
                    p_conversa.Location = New Point(126, 4)
                    p_amigos.Location = New Point(389, 4)
                    aparecia_geral_id = "1"
                    Exit Sub
                End If
            End If
        End If

        If p_avatar.Location = New Point(3, 4) Then
            If p_conversa.Location = New Point(126, 4) Then
                If p_amigos.Location = New Point(389, 4) Then
                    'amigos, conversa, avatar
                    p_amigos.Location = New Point(3, 4)
                    p_conversa.Location = New Point(126, 4)
                    p_avatar.Location = New Point(389, 4)
                    aparecia_geral_id = "4"
                    Exit Sub
                End If
            End If
        End If

        '-----------------------------------------------------------------

        If p_conversa.Location = New Point(3, 4) Then
            If p_avatar.Location = New Point(266, 4) Then
                If p_amigos.Location = New Point(389, 4) Then
                    'conversa,amigos,avatar
                    p_conversa.Location = New Point(3, 4)
                    p_amigos.Location = New Point(266, 4)
                    p_avatar.Location = New Point(389, 4)
                    aparecia_geral_id = "2"
                    Exit Sub
                End If
            End If
        End If

        If p_conversa.Location = New Point(3, 4) Then
            If p_amigos.Location = New Point(266, 4) Then
                If p_avatar.Location = New Point(389, 4) Then
                    'conversa,avatar,amigos
                    p_conversa.Location = New Point(3, 4)
                    p_avatar.Location = New Point(266, 4)
                    p_amigos.Location = New Point(389, 4)
                    aparecia_geral_id = "5"
                    Exit Sub
                End If
            End If
        End If

        '-----------------------------------------------------------------

        If p_avatar.Location = New Point(3, 4) Then
            If p_amigos.Location = New Point(126, 4) Then
                If p_conversa.Location = New Point(249, 4) Then
                    'amigos,avatar,conversa
                    p_amigos.Location = New Point(3, 4)
                    p_avatar.Location = New Point(126, 4)
                    p_conversa.Location = New Point(249, 4)
                    aparecia_geral_id = "3"
                    Exit Sub
                End If
            End If
        End If

        If p_amigos.Location = New Point(3, 4) Then
            If p_avatar.Location = New Point(126, 4) Then
                If p_conversa.Location = New Point(249, 4) Then
                    'avatar, amigos, conversa
                    p_avatar.Location = New Point(3, 4)
                    p_amigos.Location = New Point(126, 4)
                    p_conversa.Location = New Point(249, 4)
                    aparecia_geral_id = "6"
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub btn_dir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_dir.Click
        If p_conversa.Location = New Point(3, 4) Then
            If p_amigos.Location = New Point(266, 4) Then
                If p_avatar.Location = New Point(389, 4) Then
                    'avatar, conversa, amigos (default)
                    p_avatar.Location = New Point(3, 4)
                    p_conversa.Location = New Point(126, 4)
                    p_amigos.Location = New Point(389, 4)
                    aparecia_geral_id = "1"
                    Exit Sub
                End If
            End If
        End If

        If p_amigos.Location = New Point(3, 4) Then
            If p_avatar.Location = New Point(126, 4) Then
                If p_conversa.Location = New Point(249, 4) Then
                    'conversa,amigos,avatar
                    p_conversa.Location = New Point(3, 4)
                    p_amigos.Location = New Point(266, 4)
                    p_avatar.Location = New Point(389, 4)
                    aparecia_geral_id = "2"
                    Exit Sub
                End If
            End If
        End If

        If p_avatar.Location = New Point(3, 4) Then
            If p_conversa.Location = New Point(126, 4) Then
                If p_amigos.Location = New Point(389, 4) Then
                    'amigos,avatar,conversa
                    p_amigos.Location = New Point(3, 4)
                    p_avatar.Location = New Point(126, 4)
                    p_conversa.Location = New Point(249, 4)
                    aparecia_geral_id = "3"
                    Exit Sub
                End If
            End If
        End If

        '-----------------------------------------------------------------

        If p_conversa.Location = New Point(3, 4) Then
            If p_avatar.Location = New Point(266, 4) Then
                If p_amigos.Location = New Point(389, 4) Then
                    'amigos, conversa, avatar
                    p_amigos.Location = New Point(3, 4)
                    p_conversa.Location = New Point(126, 4)
                    p_avatar.Location = New Point(389, 4)
                    aparecia_geral_id = "4"
                    Exit Sub
                End If
            End If
        End If

        If p_avatar.Location = New Point(3, 4) Then
            If p_amigos.Location = New Point(126, 4) Then
                If p_conversa.Location = New Point(249, 4) Then
                    'conversa,avatar,amigos
                    p_conversa.Location = New Point(3, 4)
                    p_avatar.Location = New Point(266, 4)
                    p_amigos.Location = New Point(389, 4)
                    aparecia_geral_id = "5"
                    Exit Sub
                End If
            End If
        End If

        If p_amigos.Location = New Point(3, 4) Then
            If p_conversa.Location = New Point(126, 4) Then
                If p_avatar.Location = New Point(389, 4) Then
                    'avatar, amigos, conversa
                    p_avatar.Location = New Point(3, 4)
                    p_amigos.Location = New Point(126, 4)
                    p_conversa.Location = New Point(249, 4)
                    aparecia_geral_id = "6"
                    Exit Sub
                End If
            End If
        End If
    End Sub
#End Region

#Region "Opçoes"
    Private Sub Radio_principal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Radio_principal.CheckedChanged
        Radio_avatar.Enabled = False
        Radio_texto.Enabled = False
        Radio_contactos.Enabled = False

        If Radio_contactos.Checked = True Then
            p_r3.Enabled = False
        End If

        btn_esq.Visible = True
        btn_troca.Visible = True
        btn_dir.Visible = True
        btn_mudar_seg.Visible = False

        p_avatar.BorderStyle = BorderStyle.FixedSingle
        p_conversa.BorderStyle = BorderStyle.FixedSingle
        p_amigos.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub Radio_sec_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Radio_sec.CheckedChanged
        Radio_avatar.Enabled = True
        Radio_texto.Enabled = True
        Radio_contactos.Enabled = True

        If Radio_contactos.Checked = True Then
            p_r3.Enabled = True
        End If

        btn_esq.Visible = False
        btn_troca.Visible = False
        btn_dir.Visible = False
        btn_mudar_seg.Visible = True

        If Radio_avatar.Checked = False And Radio_texto.Checked = False And Radio_contactos.Checked = False Then
            Radio_avatar.Checked = True
        ElseIf Radio_avatar.Checked = True Then
            p_avatar.BorderStyle = BorderStyle.FixedSingle
            p_conversa.BorderStyle = BorderStyle.None
            p_amigos.BorderStyle = BorderStyle.None
        ElseIf Radio_texto.Checked = True Then
            p_avatar.BorderStyle = BorderStyle.None
            p_conversa.BorderStyle = BorderStyle.FixedSingle
            p_amigos.BorderStyle = BorderStyle.None
        ElseIf Radio_contactos.Checked = True Then
            p_avatar.BorderStyle = BorderStyle.None
            p_conversa.BorderStyle = BorderStyle.None
            p_amigos.BorderStyle = BorderStyle.FixedSingle
        End If
    End Sub

    Private Sub btn_mudar_seg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_mudar_seg.Click
        If Radio_sec.Checked = True Then
            If Radio_avatar.Checked = True Then
                If e_pic1.Location = New Point(2, 3) Then
                    e_pic2.Location = New Point(2, 3)
                    e_pic1.Location = New Point(2, 109)
                    aparecia_avatar_id = 1
                ElseIf e_pic2.Location = New Point(2, 3) Then
                    e_pic1.Location = New Point(2, 3)
                    e_pic2.Location = New Point(2, 109)
                    aparecia_avatar_id = 0
                End If
            ElseIf Radio_texto.Checked = True Then
                If e_txtg.Location = New Point(3, 3) Then
                    e_txtp.Location = New Point(3, 3)
                    e_txtg.Location = New Point(3, 55)
                    aparecia_conversa_id = 1
                ElseIf e_txtp.Location = New Point(3, 3) Then
                    e_txtg.Location = New Point(3, 3)
                    e_txtp.Location = New Point(3, 203)
                    aparecia_conversa_id = 0
                End If
            ElseIf Radio_contactos.Checked = True Then
                If Radio_maior_amigos.Checked = True Then
                    list_novas.Size = New System.Drawing.Size(109, 56)
                    list_contactos.Size = New System.Drawing.Size(109, 186)
                    If list_novas.Location = New Point(3, 3) Then
                        list_contactos.Location = New Point(3, 3)
                        list_novas.Location = New Point(3, 193)
                        aparecia_amigos_id = 1
                    ElseIf list_contactos.Location = New Point(3, 3) Then
                        list_novas.Location = New Point(3, 3)
                        list_contactos.Location = New Point(3, 65)
                        aparecia_amigos_id = 0
                    End If
                ElseIf Radio_maior_conversa.Checked = True Then
                    list_novas.Size = New System.Drawing.Size(109, 186)
                    list_contactos.Size = New System.Drawing.Size(109, 56)
                    If list_novas.Location = New Point(3, 3) Then
                        list_contactos.Location = New Point(3, 3)
                        list_novas.Location = New Point(3, 65)
                        aparecia_amigos_id = 3
                    ElseIf list_contactos.Location = New Point(3, 3) Then
                        list_novas.Location = New Point(3, 3)
                        list_contactos.Location = New Point(3, 193)
                        aparecia_amigos_id = 2
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Radio_avatar_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Radio_avatar.CheckedChanged
        p_avatar.BorderStyle = BorderStyle.FixedSingle
        p_conversa.BorderStyle = BorderStyle.None
        p_amigos.BorderStyle = BorderStyle.None

        p_r3.Visible = False
    End Sub

    Private Sub Radio_texto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Radio_texto.CheckedChanged
        p_avatar.BorderStyle = BorderStyle.None
        p_conversa.BorderStyle = BorderStyle.FixedSingle
        p_amigos.BorderStyle = BorderStyle.None

        p_r3.Visible = False
    End Sub

    Private Sub Radio_contactos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Radio_contactos.CheckedChanged
        p_avatar.BorderStyle = BorderStyle.None
        p_conversa.BorderStyle = BorderStyle.None
        p_amigos.BorderStyle = BorderStyle.FixedSingle

        p_r3.Enabled = True
        p_r3.Visible = True

        If Radio_maior_amigos.Checked = False And Radio_maior_conversa.Checked = False Then
            Radio_maior_amigos.Checked = True
        End If
    End Sub

    Private Sub btn_mudar_cor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_mudar_cor.Click
        Dim cor As Color
        Dim novacor As String

        ColorDialog2.ShowDialog()

        cor = ColorDialog2.Color
        novacor = ColorTranslator.ToHtml(cor)

        'Mostra a cor caso seja hexadecimal
        Dim cor_tipo As String = InStr(novacor, "#") '1
        If cor_tipo = 1 Then
            Dim RGBcolor = HexToColor(novacor)
            pb_mdcor.BackColor = RGBcolor
        Else
            pb_mdcor.BackColor = Color.FromName(novacor)
        End If

        lbl_mdcor.Text = novacor
        aparecia_cor_id = novacor
    End Sub

#End Region

#End Region

#Region "Opçoes"
    Private Sub cb_iconta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cb_iconta.KeyPress
        e.Handled = True
    End Sub

    Private Sub cb_estado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cb_estado.KeyPress
        e.Handled = True
    End Sub

    Private Sub Check_autorun_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Check_autorun.CheckedChanged
        If Check_autorun.Checked = True Then
            autorun_id = 1
        ElseIf Check_autorun.Checked = False Then
            autorun_id = 0
        End If
    End Sub

    Private Sub check_open_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_open.CheckedChanged
        If check_open.Checked = True Then
            auto_open_id = 1
        ElseIf check_open.Checked = False Then
            auto_open_id = 0
        End If
    End Sub

    Private Sub check_iniciarconta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_iniciarconta.CheckedChanged
        If check_iniciarconta.Checked = True Then
            If cb_iconta.Items.Count = 0 Then
                check_iniciarconta.Checked = False
                MsgBox("Tem que adicionar contas, antes do poder activar esta opção.", MsgBoxStyle.Information, "Informação")
                Exit Sub
            End If
            cb_iconta.Enabled = True
            If cb_iconta.Text = "" Then
                cb_iconta.Text = cb_contas.Items.Item(0)
            End If
            iniciar_conta_id = 1
        ElseIf check_iniciarconta.Checked = False Then
            cb_iconta.Enabled = False
            cb_iconta.Text = ""
            iniciar_conta_id = 0
        End If
    End Sub

    Private Sub check_data_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_data.CheckedChanged
        If check_data.Checked = True Then
            lbl_data.Text = 1
        ElseIf check_data.Checked = False Then
            lbl_data.Text = 0
        End If
    End Sub

    Private Sub check_historico_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles check_historico.CheckedChanged
        If check_historico.Checked = True Then
            lbl_historico.Text = 1
        ElseIf check_historico.Checked = False Then
            lbl_historico.Text = 0
        End If
    End Sub

    Private Sub cb_iconta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_iconta.SelectedIndexChanged
        auto_conta_id = cb_iconta.Text
    End Sub

    Private Sub cb_estado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_estado.SelectedIndexChanged
        auto_estado_id = cb_estado.Text
    End Sub
#End Region

#Region "Contas"
    Private Sub cb_contas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cb_contas.KeyPress
        e.Handled = True
    End Sub

    Private Sub btn_c_nova_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_c_nova.Click
        Try
            If btn_c_nova.Text = "Nova" Then
                cb_contas.Enabled = False
                cb_contas.Text = ""
                txt_login.Enabled = True
                txt_login.Text = ""
                txt_password.Enabled = True
                txt_password.Text = ""

                btn_c_nova.Text = "Adicionar"
                btn_c_editar.Text = "Cancelar"

                btn_c_editar.Enabled = True
                btn_c_eliminar.Enabled = False

                txt_login.Focus()
            ElseIf btn_c_nova.Text = "Adicionar" Then
                If txt_login.Text = "" Then
                    MsgBox("Introduza um Login.", MsgBoxStyle.Information, "Informação")
                    txt_login.Focus()
                    Exit Sub
                ElseIf txt_password.Text = "" Then
                    MsgBox("Introduza uma Password.", MsgBoxStyle.Information, "Informação")
                    txt_password.Focus()
                    Exit Sub
                End If

                Dim max_id As Integer = 0

                doc = XDocument.Load(appPath + "\acc.xml")

                'Saca ID
                max_id = get_max_id()
                max_id = max_id + 1

                Dim sha512_password = get_SHA512_Hash(txt_password.Text)

                'Adiciona a nova node
                Dim add_node As XElement = doc.Descendants("Table") _
                .LastOrDefault

                Dim new_node As New XElement("Account")
                new_node.Add(New XAttribute("ID", max_id))
                new_node.Add(New XElement("Login", txt_login.Text))
                new_node.Add(New XElement("Password", sha512_password))

                add_node.Element("Accounts").Add(new_node)
                doc.Save(appPath + "\acc.xml")

                reload_combo()

                'Definicoes
                cb_contas.Enabled = True
                cb_contas.Text = ""
                txt_login.Enabled = False
                txt_login.Text = ""
                txt_password.Enabled = False
                txt_password.Text = ""

                btn_c_nova.Text = "Nova"
                btn_c_editar.Text = "Editar"

                btn_c_editar.Enabled = False
                btn_c_eliminar.Enabled = False

            ElseIf btn_c_nova.Text = "Confirmar" Then
                If txt_login.Text = "" Then
                    MsgBox("Login em falta.", MsgBoxStyle.Information, "Informação")
                    txt_login.Focus()
                    Exit Sub
                ElseIf txt_password.Text = "" Then
                    MsgBox("Password em falta.", MsgBoxStyle.Information, "Informação")
                    txt_password.Focus()
                    Exit Sub
                End If

                Dim sha512_password = get_SHA512_Hash(txt_password.Text)

                doc = XDocument.Load(appPath + "\acc.xml")

                Dim editnode As XElement = doc.Descendants.Elements("Account") _
                                    .Where(Function(xe) xe.Attribute("ID").Value = cb_contas.SelectedIndex + 1) _
                                    .FirstOrDefault
                editnode.<Login>.Value = txt_login.Text
                editnode.<Password>.Value = sha512_password
                doc.Save(appPath + "\acc.xml")

                reload_combo()

                'Definicoes
                cb_contas.Enabled = True
                cb_contas.Text = ""
                txt_login.Enabled = False
                txt_login.Text = ""
                txt_password.Enabled = False
                txt_password.Text = ""

                btn_c_nova.Text = "Nova"
                btn_c_editar.Text = "Editar"

                btn_c_editar.Enabled = False
                btn_c_eliminar.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("O ficheiro .xml foi alterado exteriormente de modo incorrecto.", MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Private Sub btn_c_eliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_c_eliminar.Click
        Try
            If cb_contas.Text = cb_iconta.Text Then
                MsgBox("Não pode eliminar a conta que esta a ser usada para entrar no programa.", MsgBoxStyle.Information, "Informação")
                Exit Sub
            End If

            Dim a = MsgBox("Deseja eliminar a conta '" & cb_contas.Text & "'", MsgBoxStyle.YesNo, "Informação")
            If a = vbNo Then
                Exit Sub
            End If
            doc = XDocument.Load(appPath + "\acc.xml")

            Dim delete_node As XElement = doc.Descendants.Elements("Account") _
                                   .Where(Function(xe) xe.<Login>.Value = txt_login.Text And xe.<Password>.Value = txt_password.Text) _
                                   .FirstOrDefault

            delete_node.Remove()
            doc.Save(appPath + "\acc.xml")

            'Reescreve todos os ID
            Dim count As Integer = 1
            For Each node As XAttribute In doc.Descendants.Attributes("ID")
                node.Value = count
                doc.Save(appPath + "\acc.xml")
                count = count + 1
            Next

            reload_combo()

            'Definicoes
            cb_contas.Text = ""
            txt_login.Text = ""
            txt_password.Text = ""

            btn_c_editar.Enabled = False
            btn_c_eliminar.Enabled = False

            If cb_contas.Items.Count = 0 Then
                doc = XDocument.Load(appPath + "\config.xml")
                Dim editnode4 As XElement = doc.Descendants.Elements("Opcoes").First
                editnode4.<iniciar_conta>.Value = 0
                doc.Save(appPath + "\config.xml")
            End If
        Catch ex As Exception
            MsgBox("O ficheiro .xml foi alterado exteriormente de modo incorrecto.", MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Private Sub btn_c_editar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_c_editar.Click
        If btn_c_editar.Text = "Editar" Then
            If cb_contas.Text = cb_iconta.Text Then
                MsgBox("Não pode editar a conta que esta a ser usada para entrar no programa.", MsgBoxStyle.Information, "Informação")
                Exit Sub
            End If

            cb_contas.Enabled = False
            txt_login.Enabled = True
            txt_password.Enabled = True
            txt_password.Text = ""

            btn_c_nova.Text = "Confirmar"
            btn_c_editar.Text = "Cancelar"

            btn_c_eliminar.Enabled = False
        ElseIf btn_c_editar.Text = "Cancelar" Then
            cb_contas.Enabled = True
            cb_contas.Text = ""
            txt_login.Enabled = False
            txt_login.Text = ""
            txt_password.Enabled = False
            txt_password.Text = ""

            btn_c_nova.Text = "Nova"
            btn_c_editar.Text = "Editar"

            btn_c_editar.Enabled = False
            btn_c_eliminar.Enabled = False
        End If
    End Sub

    Private Sub cb_contas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_contas.SelectedIndexChanged
        Try
            doc = XDocument.Load(appPath + "\acc.xml")
            Dim qList = (From xe In doc.Descendants.Elements("Account") _
                      Where xe.Attribute("ID").Value = cb_contas.SelectedIndex + 1 _
                      Select New With { _
                               .Login = xe.<Login>.Value, _
                               .Password = xe.<Password>.Value _
                               }).FirstOrDefault
            txt_login.Text = qList.Login
            txt_password.Text = qList.Password


            If cb_contas.Text = "" Then
                btn_c_editar.Enabled = False
                btn_c_eliminar.Enabled = False
            Else
                btn_c_editar.Enabled = True
                btn_c_eliminar.Enabled = True
            End If
        Catch ex As Exception
            MsgBox("O ficheiro .xml foi alterado exteriormente de modo incorrecto.", MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Function get_max_id()
        Dim max_id As Integer = 0
        doc = XDocument.Load(appPath + "\acc.xml")
        For Each node As XAttribute In doc.Descendants.Attributes("ID")
            If max_id < node.Value Then
                max_id = node.Value
            End If
        Next
        Return max_id
    End Function

    Sub reload_combo()
        Try
            If System.IO.File.Exists(appPath + "\acc.xml") = False Then
                Dim writer As New XmlTextWriter(appPath + "\acc.xml", System.Text.Encoding.UTF8)

                writer.WriteStartDocument(True)
                writer.Formatting = Formatting.Indented
                writer.Indentation = 5

                'Tabela dos logins
                writer.WriteStartElement("Table")
                writer.WriteStartElement("Accounts")
                writer.WriteEndElement()
                writer.WriteEndElement()

                writer.WriteEndDocument()
                writer.Close()
            End If

            'Coloca todos os ID nas combo
            doc = XDocument.Load(appPath + "\acc.xml")
            cb_contas.Items.Clear()
            cb_iconta.Items.Clear()
            For Each node As XAttribute In doc.Descendants.Attributes("ID")
                Dim count As Integer = node.Value
                Dim qList = (From xe In doc.Descendants.Elements("Account") _
                          Where xe.Attribute("ID").Value = count _
                          Select New With { _
                                   .Login = xe.<Login>.Value _
                                   }).FirstOrDefault
                cb_contas.Items.Add(qList.Login)
                cb_iconta.Items.Add(qList.Login)
            Next
        Catch ex As Exception
            MsgBox("O ficheiro .xml foi alterado exteriormente de modo incorrecto.", MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Password"
    Private Sub btn_alterar_pw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_alterar_pw.Click
        If txt_pass_antiga.Text = "" Or txt_pass_nova.Text = "" Or txt_pass_nova_2.Text = "" Then
            lbl_aviso.ForeColor = Color.LightPink
            lbl_aviso.Text = "Todos os campos sao obrigatórios."
            Exit Sub
        End If
        If txt_pass_nova.Text <> txt_pass_nova_2.Text Then
            lbl_aviso.ForeColor = Color.LightPink
            lbl_aviso.Text = "As duas password não correspondem."
            Exit Sub
        End If

        Dim sha512_password = get_SHA512_Hash(txt_pass_antiga.Text)
        Dim txt_login As String = main.login_txt.Text
        Dim id_pass As Boolean = False
        Dim command As New SqlCommand("SELECT password FROM t_login WHERE nome=@login AND password=@password", connection)
        command.Parameters.AddWithValue("@login", txt_login)
        command.Parameters.AddWithValue("@password", sha512_password)
        connection.Open()
        Dim reader As SqlDataReader = command.ExecuteReader()
        Try
            reader.Read()
            Dim var_pass = reader("password")
            If var_pass = sha512_password Then
                id_pass = True
            End If
            reader.Close()
            connection.Close()
        Catch
            lbl_aviso.ForeColor = Color.LightPink
            lbl_aviso.Text = "A password esta incorrecta!"
            reader.Close()
            connection.Close()
        End Try

        If txt_pass_antiga.Text = txt_pass_nova.Text Or txt_pass_antiga.Text = txt_pass_nova_2.Text Then
            lbl_aviso.ForeColor = Color.LightPink
            lbl_aviso.Text = "Nao pode alterar para a mesma password."
            Exit Sub
        End If

        Dim sha512_password2 = get_SHA512_Hash(txt_pass_nova.Text)
        If id_pass = True Then
            Dim command2 As New SqlCommand("UPDATE t_login SET password=@password WHERE nome=@login", connection)
            command2.Parameters.AddWithValue("@login", txt_login)
            command2.Parameters.AddWithValue("@password", sha512_password2)
            connection.Open()
            command2.ExecuteNonQuery()
            connection.Close()

            'Alterar no xml
            Try
                If check_savepass.Checked = True Then
                    doc = XDocument.Load(appPath + "\acc.xml")

                    Dim editnode As XElement = doc.Descendants.Elements("Account") _
                                           .Where(Function(xe) xe.<Login>.Value = main.login_txt.Text And xe.<Password>.Value = sha512_password) _
                                           .FirstOrDefault
                    editnode.<Login>.Value = main.login_txt.Text
                    editnode.<Password>.Value = sha512_password2
                    doc.Save(appPath + "\acc.xml")
                End If
            Catch ex As Exception

            End Try

            txt_pass_antiga.Text = ""
            txt_pass_nova.Text = ""
            txt_pass_nova_2.Text = ""
            check_savepass.Checked = False
            lbl_aviso.ForeColor = Color.SpringGreen
            lbl_aviso.Text = "A password foi alrerada com sucesso!"
        End If
    End Sub

    Private Sub btn_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        txt_pass_antiga.Text = ""
        txt_pass_nova.Text = ""
        txt_pass_nova_2.Text = ""
        txt_pass_antiga.Focus()
    End Sub
#End Region

#Region "Tipos de Letra"
    Private Sub btn_mudacor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_mudacor.Click
        Dim cor As Color
        Dim novacor As String

        ColorDialog1.ShowDialog()

        cor = ColorDialog1.Color
        novacor = ColorTranslator.ToHtml(cor)

        'Mostra a cor caso seja hexadecimal
        Dim cor_tipo As String = InStr(novacor, "#") '1
        If cor_tipo = 1 Then
            Dim RGBcolor = HexToColor(novacor)
            pb_cor.BackColor = RGBcolor
        Else
            pb_cor.BackColor = Color.FromName(novacor)
        End If

        lbl_cor.Text = novacor
    End Sub

    Private Sub cb_font_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cb_font.KeyPress
        e.Handled = True
    End Sub

    Private Sub cb_size_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cb_size.KeyPress
        e.Handled = True
    End Sub
#End Region

#Region "Botoes Laterais"
    Private Sub pb_aparencia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_aparencia.Click
        pb_aparencia.Image = My.Resources.aparecnia2
        pb_fonte.Image = My.Resources.fonte1
        pb_opcoes.Image = My.Resources.opçoes1
        pb_contas.Image = My.Resources.contas1
        pb_password.Image = My.Resources.pass1

        lbl_tabname.Text = "Aparência"
        pb_imga.Image = My.Resources.appl_opt

        TabControl1.SelectedTab = tab_aparencia
    End Sub

    Private Sub pb_fonte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_fonte.Click
        pb_aparencia.Image = My.Resources.aparecnia1
        pb_fonte.Image = My.Resources.fonte2
        pb_opcoes.Image = My.Resources.opçoes1
        pb_contas.Image = My.Resources.contas1
        pb_password.Image = My.Resources.pass1

        lbl_tabname.Text = "Alterar fonte"
        pb_imga.Image = My.Resources.letra_opt

        TabControl1.SelectedTab = tab_fonte
    End Sub

    Private Sub pb_opcoes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_opcoes.Click
        pb_aparencia.Image = My.Resources.aparecnia1
        pb_fonte.Image = My.Resources.fonte1
        pb_opcoes.Image = My.Resources.opçoes2
        pb_contas.Image = My.Resources.contas1
        pb_password.Image = My.Resources.pass1

        lbl_tabname.Text = "Opções"
        pb_imga.Image = My.Resources.opt_opt

        TabControl1.SelectedTab = tab_opcoes
    End Sub

    Private Sub pb_contas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_contas.Click
        pb_aparencia.Image = My.Resources.aparecnia1
        pb_fonte.Image = My.Resources.fonte1
        pb_opcoes.Image = My.Resources.opçoes1
        pb_contas.Image = My.Resources.contas2
        pb_password.Image = My.Resources.pass1

        lbl_tabname.Text = "Contas guardadas"
        pb_imga.Image = My.Resources.acc_opt

        txt_password.PasswordChar = "●"
        TabControl1.SelectedTab = tab_contas
    End Sub

    Private Sub pb_password_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_password.Click
        pb_aparencia.Image = My.Resources.aparecnia1
        pb_fonte.Image = My.Resources.fonte1
        pb_opcoes.Image = My.Resources.opçoes1
        pb_contas.Image = My.Resources.contas1
        pb_password.Image = My.Resources.pass2

        lbl_tabname.Text = "Modificar password"
        pb_imga.Image = My.Resources.pass_opt

        txt_pass_antiga.PasswordChar = "●"
        txt_pass_nova.PasswordChar = "●"
        txt_pass_nova_2.PasswordChar = "●"
        txt_pass_antiga.Text = ""
        txt_pass_nova.Text = ""
        txt_pass_nova_2.Text = ""
        lbl_aviso.Text = ""
        check_savepass.Checked = False
        TabControl1.SelectedTab = tab_pass
    End Sub
#End Region

#Region "Actualiza"
    Private Sub btn_testar_aparencia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_testar_aparencia.Click
        testar_aparencia = True
        apply_content()
    End Sub

    Private Sub pb_ok_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_ok.MouseDown
        pb_ok.Image = My.Resources.btn_ok_click
    End Sub

    Private Sub pb_ok_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_ok.MouseUp
        pb_ok.Image = My.Resources.opt_ok
    End Sub

    Private Sub pb_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_ok.Click
        'Grava o conteudo, actualiza e sai
        testar_aparencia = False
        doc = XDocument.Load(appPath + "\config.xml")
        Dim editnode As XElement = doc.Descendants.Elements("Aparencia").First
        editnode.<geral>.Value = aparecia_geral_id
        editnode.<avatar>.Value = aparecia_avatar_id
        editnode.<conversa>.Value = aparecia_conversa_id
        editnode.<amigos>.Value = aparecia_amigos_id
        editnode.<cor>.Value = aparecia_cor_id

        Dim editnode2 As XElement = doc.Descendants.Elements("Opcoes").First
        editnode2.<autorun>.Value = autorun_id

        Dim editnode9 As XElement = doc.Descendants.Elements("Opcoes").First
        editnode9.<open>.Value = auto_open_id

        Dim editnode4 As XElement = doc.Descendants.Elements("Opcoes").First
        editnode4.<iniciar_conta>.Value = iniciar_conta_id

        Dim editnode7 As XElement = doc.Descendants.Elements("Opcoes").First
        editnode7.<ver_data>.Value = lbl_data.Text

        Dim editnode8 As XElement = doc.Descendants.Elements("Opcoes").First
        editnode8.<ver_historico>.Value = lbl_historico.Text

        Dim editnode5 As XElement = doc.Descendants.Elements("Opcoes").First
        editnode5.<auto_conta>.Value = auto_conta_id

        Dim editnode6 As XElement = doc.Descendants.Elements("Opcoes").First
        editnode6.<auto_estado>.Value = auto_estado_id

        Dim editnode3 As XElement = doc.Descendants.Elements("Letra").First
        editnode3.<tipo>.Value = cb_font.Text
        editnode3.<tamanho>.Value = cb_size.Text
        editnode3.<cor>.Value = lbl_cor.Text

        doc.Save(appPath + "\config.xml")
        apply_content()
        Me.Close()
    End Sub

    Private Sub pb_cancel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_cancel.MouseDown
        pb_cancel.Image = My.Resources.btn_cancel_click
    End Sub

    Private Sub pb_cancel_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_cancel.MouseUp
        pb_cancel.Image = My.Resources.opt_cancel
    End Sub

    Private Sub pb_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_cancel.Click
        'Para voltar ao original
        main_form_id = True
        apply_content()
        Me.Close()
    End Sub
#End Region

#Region "Move Window"

    Private m_Location As Point

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        m_Location = e.Location
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If Not e.Button = Windows.Forms.MouseButtons.Left Then Exit Sub
        Dim Delta As Size
        Delta.Width = e.X - m_Location.X
        Delta.Height = e.Y - m_Location.Y
        Me.Location = Point.Add(Me.Location, Delta)
    End Sub

    Private Sub lbl_tabname_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbl_tabname.MouseDown
        m_Location = e.Location
    End Sub

    Private Sub lbl_tabname_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbl_tabname.MouseMove
        If Not e.Button = Windows.Forms.MouseButtons.Left Then Exit Sub
        Dim Delta As Size
        Delta.Width = e.X - m_Location.X
        Delta.Height = e.Y - m_Location.Y
        Me.Location = Point.Add(Me.Location, Delta)
    End Sub

    Private Sub opt_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        m_Location = e.Location
    End Sub

    Private Sub opt_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If Not e.Button = Windows.Forms.MouseButtons.Left Then Exit Sub
        Dim Delta As Size
        Delta.Width = e.X - m_Location.X
        Delta.Height = e.Y - m_Location.Y
        Me.Location = Point.Add(Me.Location, Delta)
    End Sub
#End Region

End Class