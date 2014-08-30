Imports System.Data.SqlClient

Public Class main

    Public nv_pedido As Boolean = False
    Public nv_download As Boolean = False
    Public nv_chamada As Boolean = False

    Private w_state_max As Boolean = False
    Private nv_online As Integer
    Private close_form As Boolean = False
    Private opt_open As Boolean = False
    Private desconectado As Boolean = False
    Private array_id As Boolean
    Private img_ref As Integer
    Private m_Location As Point
    Private count_off As Integer
    Private count_on As Integer
    Private list_state As New ImageList()
    Private list_conv As New ImageList()
    Private auto_open_wn As Boolean = True

    Private muda_estado As Boolean = False
    Private estados(1000) As String
    Private my_estado As String

    'Click taskbar
    Private Const GWL_STYLE = (-16)
    Private Const WS_MAXIMIZEBOX = &H10000
    Private Const WS_MINIMIZEBOX = &H20000
    Private Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
    Private Declare Function SetWindowLong Lib "user32.dll" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

    'shadow
    Private Const GCL_STYLE As Long = -26
    Private Const CS_DROPSHADOW As Long = &H20000
    Private Declare Function GetClassLong Lib "user32.dll" Alias "GetClassLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
    Private Declare Function SetClassLong Lib "user32.dll" Alias "SetClassLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

    Private Sub DropShadow(ByVal hwnd As Long)
        SetClassLong(hwnd, GCL_STYLE, GetClassLong(hwnd, GCL_STYLE) Or CS_DROPSHADOW)
    End Sub

#Region "Form Load"

    Private Sub main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Preparar form
        'Adiciona o min/max com click na taskbar
        Dim lStyle As Long = GetWindowLong(Handle, GWL_STYLE) Or WS_MAXIMIZEBOX Or WS_MINIMIZEBOX
        SetWindowLong(Handle, GWL_STYLE, lStyle)
        'adiciona uma shadow
        DropShadow(Me.Handle)

        'load skins
        Try
            Me.BackgroundImage = Image.FromFile(appPath + "\Skin\main_bg.png")
        Catch ex As Exception
        End Try

        Try
            px_minimizar.Image = Image.FromFile(appPath + "\Skin\nl1.png")
        Catch ex As Exception
        End Try

        Try
            px_maximizar.Image = Image.FromFile(appPath + "\Skin\nl2.png")
        Catch ex As Exception
        End Try

        Try
            px_fechar.Image = Image.FromFile(appPath + "\Skin\nl3.png")
        Catch ex As Exception
        End Try

        'Premite drag and drop
        avatar_1_img.AllowDrop = True
        sms_txt.AllowDrop = True
        sms_rec_txt.AllowDrop = True

        'Nome
        Me.Text = "LIM - " & login_txt.Text
        lbl_tit.Text = "LIM - " & login_txt.Text

        '------------------

        'Tamanho
        Dim estadoi As String
        Dim last_w As Integer
        Dim last_h As Integer
        doc = XDocument.Load(appPath + "\config.xml")
        Dim qList = (From xe In doc.Descendants.Elements("Resolucao") _
        Select New With { _
        .estado = xe.<estado>.Value, _
        .tamanho_w = xe.<tamanho_w>.Value, _
        .tamanho_h = xe.<tamanho_h>.Value _
        }).First
        estadoi = qList.estado
        last_w = qList.tamanho_w
        last_h = qList.tamanho_h
        If estadoi = "full" Then
            w_state_max = True
            Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
            Me.Size = New System.Drawing.Size(workingRectangle.Width, workingRectangle.Height)
            Me.Location = New System.Drawing.Point(0, 0)
        ElseIf estadoi = "non-full" Then
            w_state_max = False
            If last_w > Screen.PrimaryScreen.WorkingArea.Width Or last_h > Screen.PrimaryScreen.WorkingArea.Height Then
                Me.Size = New System.Drawing.Size(720, 385)
                Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
                Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
            Else
                Me.Size = New System.Drawing.Size(last_w, last_h)
                Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
                Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
            End If
        End If

        'Aplica o estado de entrada
        Dim qList2 = (From xe In doc.Descendants.Elements("Opcoes") _
        Select New With { _
        .auto_estado = xe.<auto_estado>.Value, _
        .open = xe.<open>.Value _
        }).First
        auto_estado_id = qList2.auto_estado
        auto_open_id = qList2.open

        If auto_estado_id = "Online" Then
            Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
            command2.Parameters.AddWithValue("@nome", login_txt.Text)
            command2.Parameters.AddWithValue("@estado", "Online")
            connection.Open()
            command2.ExecuteNonQuery()
            connection.Close()
            my_estado = "Online"
            Me.NotifyIcon1.Icon = My.Resources.icon
        ElseIf auto_estado_id = "Ocupado" Then
            Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
            command2.Parameters.AddWithValue("@nome", login_txt.Text)
            command2.Parameters.AddWithValue("@estado", "Ocupado")
            connection.Open()
            command2.ExecuteNonQuery()
            connection.Close()
            my_estado = "Ocupado"
            Me.NotifyIcon1.Icon = My.Resources.busy1
        ElseIf auto_estado_id = "Ausente" Then
            Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
            command2.Parameters.AddWithValue("@nome", login_txt.Text)
            command2.Parameters.AddWithValue("@estado", "Ausente")
            connection.Open()
            command2.ExecuteNonQuery()
            connection.Close()
            my_estado = "Ausente"
            Me.NotifyIcon1.Icon = My.Resources.away1
        ElseIf auto_estado_id = "Volto Já" Then
            Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
            command2.Parameters.AddWithValue("@nome", login_txt.Text)
            command2.Parameters.AddWithValue("@estado", "VoltoJa")
            connection.Open()
            command2.ExecuteNonQuery()
            connection.Close()
            my_estado = "VoltoJa"
            Me.NotifyIcon1.Icon = My.Resources.brb1
        End If

        '------------------

        'Aplica todas as opcoes
        main_form_id = True
        apply_content()

        'Load das List
        'Contactos
        list_state.ImageSize = New Drawing.Size(20, 20)
        list_state.ColorDepth = ColorDepth.Depth32Bit

        list_state.Images.Add(My.Resources.online)
        list_state.Images.Add(My.Resources.offline)
        list_state.Images.Add(My.Resources.busy)
        list_state.Images.Add(My.Resources.away)
        list_state.Images.Add(My.Resources.brb)

        'Conversas
        list_conv.ImageSize = New Drawing.Size(20, 20)
        list_conv.ColorDepth = ColorDepth.Depth32Bit

        list_conv.Images.Add(My.Resources.nv_conversa)
        list_conv.Images.Add(My.Resources.estados)

        Try
            'load contactos adiados
            Dim ped_id As Boolean = False
            Dim command9 As New SqlCommand("SELECT * FROM amigos WHERE amigos_1=@nome OR amigos_2=@nome", connection)
            command9.Parameters.AddWithValue("@nome", login_txt.Text)
            connection.Open()
            Dim reader As SqlDataReader = command9.ExecuteReader()
            While reader.Read()
                If reader("amigos_1") <> login_txt.Text Then
                    If reader("pedido") = "Wait" Then
                        If reader("req") <> login_txt.Text Then
                            pedidos.ListBox1.Items.Add(reader("amigos_1"))
                            ped_id = True
                        End If
                    End If
                ElseIf reader("amigos_2") <> login_txt.Text Then
                    If reader("pedido") = "Wait" Then
                        If reader("req") <> login_txt.Text Then
                            pedidos.ListBox1.Items.Add(reader("amigos_2"))
                            ped_id = True
                        End If
                    End If
                End If
            End While
            If ped_id = True Then
                nv_pedido = True
                pedidos.amigo_lbl.Visible = False
                pedidos.txt_lbl.Visible = False
                pedidos.mais_tarde_btn.Visible = False
                pedidos.ListBox1.Visible = True
                pedidos.adiados_txt.Visible = True
                pedidos.ControlBox = True
                pedidos.Show()
            End If
            reader.Close()
            connection.Close()
        Catch
            connection.Close()
        End Try

        Try
            'carrega avatar (proprio)
            Dim command As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome ", connection)
            command.Parameters.AddWithValue("@nome", login_txt.Text)
            connection.Open()
            Dim picture As System.Drawing.Image = Nothing
            Dim pictureData As Byte() = DirectCast(command.ExecuteScalar(), Byte())
            Dim stream As New IO.MemoryStream(pictureData)
            connection.Close()
            picture = System.Drawing.Image.FromStream(stream)
            avatar_1_img.Image = picture
        Catch
            connection.Close()
        End Try

        Try
            'Edita estado para online
            Dim online_v As String = "Online"
            Dim command5 As New SqlCommand("UPDATE t_login SET online=@online WHERE nome=@nome ", connection)
            command5.Parameters.AddWithValue("@nome", login_txt.Text)
            command5.Parameters.AddWithValue("@online", online_v)
            connection.Open()
            command5.ExecuteNonQuery()
            connection.Close()
        Catch
            connection.Close()
        End Try

        Try
            'Saca o ultimo estado
            Dim command2 As New SqlCommand("SELECT estado FROM t_login WHERE nome=@nome", connection)
            command2.Parameters.AddWithValue("@nome", login_txt.Text)
            connection.Open()
            Dim reader As SqlDataReader = command2.ExecuteReader()
            reader.Read()
            my_estado = reader("estado")
            reader.Close()
            connection.Close()
        Catch
            connection.Close()
        End Try

        'Utiliza o estado
        If my_estado = "Online" Then
            Me.NotifyIcon1.Icon = My.Resources.icon
        ElseIf my_estado = "Ausente" Then
            Me.NotifyIcon1.Icon = My.Resources.away1
        ElseIf my_estado = "Ocupado" Then
            Me.NotifyIcon1.Icon = My.Resources.busy1
        ElseIf my_estado = "VoltoJa" Then
            Me.NotifyIcon1.Icon = My.Resources.brb1
        End If

        'Tray
        NotifyIcon1.Text = "LIM - " & login_txt.Text
        NotifyIcon1.Visible = True
        sms_txt.Focus()
        auto_login = False

        'inicia a actualizaçao
        Timer1.Enabled = True
        Timer1.Interval = 100

        ''Round Rectangle Form
        'Me.Height = 300
        'Me.Width = 400
        'Dim p As New Drawing2D.GraphicsPath()
        'p.StartFigure()
        'p.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        'p.AddLine(40, 0, Me.Width - 40, 0)
        'p.AddArc(New Rectangle(Me.Width - 40, 0, 40, 40), -90, 90)
        'p.AddLine(Me.Width, 40, Me.Width, Me.Height - 40)
        'p.AddArc(New Rectangle(Me.Width - 40, Me.Height - 40, 40, 40), 0, 90)
        'p.AddLine(Me.Width - 40, Me.Height, 40, Me.Height)
        'p.AddArc(New Rectangle(0, Me.Height - 40, 40, 40), 90, 90)
        'p.CloseFigure()
        'Me.Region = New Region(p)
    End Sub

#End Region

#Region "Ao fechar"

    Private Sub main_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        NotifyIcon1.Visible = False
        Try
            pedidos.Dispose()
            receberfile.Dispose()
            opt.Dispose()
            voip.Dispose()
            transferencias.Dispose()
            'altera estado para Offline antes de sair, se nao tiver sido desconectado
            If desconectado = False Then
                Dim online_v As String = "Offline"
                Dim command5 As New SqlCommand("UPDATE t_login SET online=@online WHERE nome=@nome ", connection)
                command5.Parameters.AddWithValue("@nome", login_txt.Text)
                command5.Parameters.AddWithValue("@online", online_v)
                connection.Open()
                command5.ExecuteNonQuery()
                connection.Close()
                Me.Dispose()
            End If
        Catch
            connection.Close()
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

#End Region

#Region "Enviar SMS"

    Private Sub enviar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles enviar_btn.Click
        Dim max_id_var As Long
        Dim amigo_idf As String = "Nao"
        If amigo_c_txt.Text = "" Then
            MsgBox("Seleccione um contacto da lista.", MsgBoxStyle.Information, "Informação")
        ElseIf amigo_c_txt.Text = "Amigo" Then
            MsgBox("Seleccione um contacto da lista.", MsgBoxStyle.Information, "Informação")
        Else
            Try
                'verifica a ligaçao
                Dim command6 As New SqlCommand("SELECT pedido FROM amigos WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                command6.Parameters.AddWithValue("@nome_1", login_txt.Text)
                command6.Parameters.AddWithValue("@nome_2", amigo_c_txt.Text)
                connection.Open()
                Dim reader6 As SqlDataReader = command6.ExecuteReader()
                While reader6.Read()
                    Dim read_pedido_v As String = reader6("pedido")
                    If read_pedido_v = "Sim" Then
                        amigo_idf = "Sim"
                    Else
                        MsgBox("Não foi possível enviar a mensagem.", MsgBoxStyle.Exclamation, "Informação")
                        connection.Close()
                        Exit Sub
                    End If
                End While
                reader6.Close()
                connection.Close()

                If amigo_idf = "Sim" Then
                    'Saca ID
                    Dim command3 As New SqlCommand("SELECT MAX(ID) as max_id FROM sms", connection)
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

                    'Data do server sql
                    Dim command As New SqlCommand("SELECT GETDATE() as Date", connection)
                    Dim data As String = ""
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    reader.Read()
                    data = reader("Date")
                    connection.Close()

                    'escreve nova sms
                    Dim command1 As New SqlCommand("INSERT INTO sms (id, de, para, mensagem, visto, data) VALUES (@id, @de, @para, @mensagem, @visto, @data)", connection)
                    Dim visto_v As String = "Nao"
                    Dim sms_v As String = "<tipo>" & letra_tipo & "</tipo><tamanho>" & letra_tamanho & "</tamanho><cor>" & letra_cor & "</cor>" & sms_txt.Text
                    max_id_var = max_id_var + 1
                    command1.Parameters.AddWithValue("@de", login_txt.Text)
                    command1.Parameters.AddWithValue("@para", amigo_c_txt.Text)
                    command1.Parameters.AddWithValue("@mensagem", sms_v)
                    command1.Parameters.AddWithValue("@visto", visto_v)
                    command1.Parameters.AddWithValue("@id", max_id_var)
                    command1.Parameters.AddWithValue("@data", data)
                    connection.Open()
                    command1.ExecuteNonQuery()
                    connection.Close()

                    'Mostra logo a sms que foi escrita (pois ele so vai ler, se ouver novas sms)
                    sms_rec_txt.SelectionFont = New Font(letra_tipo, letra_tamanho, FontStyle.Regular)

                    'Caso seja hexadecimal
                    Dim cor_tipo As String = InStr(letra_cor, "#") '1
                    If cor_tipo = 1 Then
                        Dim RGBcolor = HexToColor(letra_cor)
                        sms_rec_txt.SelectionColor = RGBcolor
                    Else
                        sms_rec_txt.SelectionColor = Color.FromName(letra_cor)
                    End If

                    If ver_data_id = "1" Then
                        sms_rec_txt.AppendText(vbNewLine & login_txt.Text & " (" & data & ")" & " diz: " & vbNewLine & "    " & sms_txt.Text & vbNewLine)
                    ElseIf ver_data_id = "0" Then
                        sms_rec_txt.AppendText(vbNewLine & login_txt.Text & " diz: " & vbNewLine & "    " & sms_txt.Text & vbNewLine)
                    End If

                    'Scrol para o fim
                    sms_rec_txt.Select(sms_rec_txt.Text.Length, 0)
                    sms_rec_txt.ScrollToCaret()

                    sms_txt.Text = ""
                End If
            Catch
                connection.Close()
                MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
            End Try
        End If
    End Sub

#End Region

#Region "Actualiza"

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'minimiza no auto iniciar
        If auto_open_wn = True Then
            auto_open_wn = False
            If auto_open_id = 0 Then
                Me.WindowState = FormWindowState.Minimized
                Me.Hide()
                tray_abrir.Text = "&Abrir"
            End If
        End If

        Try
            'Escuta novas sms do contacto currente
            Dim command5 As New SqlCommand("SELECT count(visto) as sms_vistas FROM sms WHERE para=@para AND de=@de AND Visto='Nao'", connection_act)
            command5.Parameters.AddWithValue("@de", amigo_c_txt.Text)
            command5.Parameters.AddWithValue("@para", login_txt.Text)
            Dim nova_sms_id As Boolean = False
            connection_act.Open()
            Dim reader_3_act As SqlDataReader = command5.ExecuteReader()
            reader_3_act.Read()
            Try
                If reader_3_act("sms_vistas") >= 1 Then
                    nova_sms_id = True
                Else
                    nova_sms_id = False
                End If
            Catch
                connection_act.Close()
            End Try
            reader_3_act.Close()
            connection_act.Close()

            'load da nova sms (utiliza o texto que ja la esta, que foi la posto pelo click na listbox)
            If nova_sms_id = True Then
                Dim var1_v As String = ""
                Dim var2_v As String = ""
                Dim command As New SqlCommand("SELECT de, data, mensagem FROM sms WHERE para=@para AND de=@de AND Visto='Nao'", connection_act)
                command.Parameters.AddWithValue("@de", amigo_c_txt.Text)
                command.Parameters.AddWithValue("@para", login_txt.Text)
                connection_act.Open()
                Dim reader_act As SqlDataReader = command.ExecuteReader()
                Dim source As String = ""
                Dim sms_final As String = ""

                Dim tipo As String = ""
                Dim tipo_1 As String = ""
                Dim tipo_2 As String = ""
                Dim tamanho As Integer
                Dim tamanho_1 As String = ""
                Dim tamanho_2 As String = ""
                Dim cor As String = ""
                Dim cor_1 As String = ""
                Dim cor_2 As String = ""

                While reader_act.Read()
                    source = reader_act("mensagem")

                    tipo_1 = InStr(source, "<tipo>") '6
                    tipo_2 = InStr(source, "</tipo>") '7
                    tamanho_1 = InStr(source, "<tamanho>") '9
                    tamanho_2 = InStr(source, "</tamanho>") '10
                    cor_1 = InStr(source, "<cor>") '5
                    cor_2 = InStr(source, "</cor>") '6

                    If tipo_1 = 1 And tipo_2 > tipo_1 Then
                        tipo = Mid(source, tipo_1 + 6, tipo_2 - 7)
                    End If
                    If tamanho_1 = tipo_2 + 7 And tamanho_2 > tamanho_1 Then
                        tamanho = Mid(source, tamanho_1 + 9, (tamanho_2 - 10) - (tipo_2 + 6))
                    End If
                    If cor_1 = tamanho_2 + 10 And cor_2 > cor_1 Then
                        cor = Mid(source, cor_1 + 5, (cor_2 - 6) - (tamanho_2 + 9))
                    End If

                    sms_final = Mid(source, cor_2 + 6)
                    sms_rec_txt.SelectionFont = New Font(tipo, tamanho, FontStyle.Regular)

                    'Caso seja hexadecimal
                    Dim cor_tipo As String = InStr(cor, "#") '1
                    If cor_tipo = 1 Then
                        Dim RGBcolor = HexToColor(cor)
                        sms_rec_txt.SelectionColor = RGBcolor
                    Else
                        sms_rec_txt.SelectionColor = Color.FromName(cor)
                    End If

                    If ver_data_id = "1" Then
                        sms_rec_txt.AppendText(vbNewLine & reader_act("de") & " (" & reader_act("data") & ")" & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                    ElseIf ver_data_id = "0" Then
                        sms_rec_txt.AppendText(vbNewLine & reader_act("de") & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                    End If
                End While

                'Scrol para o fim
                sms_rec_txt.Select(sms_rec_txt.Text.Length, 0)
                sms_rec_txt.ScrollToCaret()

                reader_act.Close()
                connection_act.Close()

                'escreve que a sms foi vista (assim nao chega a aparecer nas "novas sms")
                Dim visto_v As String = "Sim"
                Dim command3 As New SqlCommand("UPDATE sms SET visto=@visto WHERE para=@para AND de=@de AND visto='Nao'", connection_act)
                command3.Parameters.AddWithValue("@de", amigo_c_txt.Text)
                command3.Parameters.AddWithValue("@para", login_txt.Text)
                command3.Parameters.AddWithValue("@visto", visto_v)
                connection_act.Open()
                command3.ExecuteNonQuery()
                connection_act.Close()

                'Se receber uma nova sms e estiver minimizado, flash
                If Me.WindowState = FormWindowState.Minimized Then
                    FlashWindow(Me.Handle.ToInt32, True)
                    If Me.Visible = False And my_estado <> "Ocupado" Then
                        Me.NotifyIcon1.Icon = My.Resources.nw_sms
                    End If
                End If
            End If
        Catch
            connection_act.Close()
            MsgBox(Err.Description)
        End Try

        'Sms recebidos com load
        Try
            Dim command6 As New SqlCommand("SELECT DISTINCT de FROM sms WHERE para=@para AND visto='Nao'", connection_act)
            command6.Parameters.AddWithValue("@para", login_txt.Text)
            connection_act.Open()
            Dim reader6_act As SqlDataReader = command6.ExecuteReader()
            Dim array_recebidos(1000) As String
            Dim array_recebidos_n(1000) As String
            Dim roda As Integer = 0
            Dim roda_n As Integer = 0
            Dim e_amigo As Boolean = False
            While reader6_act.Read()
                array_recebidos(roda) = reader6_act("de")
                roda = roda + 1
            End While
            'Verifica se ainda sao amigos, para mostrar a nova conversa
            For i As Integer = 0 To roda - 1
                For i2 As Integer = 0 To list_contactos.Items.Count - 1
                    If array_recebidos(i) = list_contactos.Items.Item(i2).Text Then
                        array_recebidos_n(roda_n) = array_recebidos(i)
                        roda_n = roda_n + 1
                    End If
                Next
            Next
            'Adiciona o que esta no array das novas sms, ignora os que nao sao amigos
            If roda_n <> list_n_conversas.Items.Count Then
                list_n_conversas.Items.Clear()
                For i As Integer = 0 To roda_n - 1
                    If array_recebidos_n(i) <> amigo_c_txt.Text Then 'so adicionar se for diferente da conversa currente
                        list_n_conversas.Items.Add(array_recebidos_n(i))
                        list_n_conversas.Items.Item(i).ImageIndex = 0
                    End If
                Next
                list_n_conversas.SmallImageList = list_conv
            End If
            Dim nv_online_agr As Integer = list_n_conversas.Items.Count
            If nv_online_agr <> nv_online Then 'para actualizar so o numero de sms, se nao for maior...
                If nv_online_agr > nv_online Then 'para fazer o flash so se for maior...
                    If Me.WindowState = FormWindowState.Minimized Then
                        FlashWindow(Me.Handle.ToInt32, True)
                        If Me.Visible = False And my_estado <> "Ocupado" Then
                            Me.NotifyIcon1.Icon = My.Resources.nw_sms
                        End If
                    End If
                End If
                conv_g.Text = "Novas Mensagens - " & list_n_conversas.Items.Count
                nv_online = list_n_conversas.Items.Count
            End If
            reader6_act.Close()
            connection_act.Close()
        Catch
            connection_act.Close()
        End Try

        'escuta e da load dos pedidos, amigos, Online/Offline
        Try
            Dim array_amigos_1(1000) As String
            Dim array_amigos_2(1000) As String
            Dim array_online(1000) As String
            Dim array_offline(1000) As String
            Dim contador_amigos_1 As Integer = 0
            Dim contador_amigos_2 As Integer = 0
            Dim num_max As Integer = 0
            Dim contador_on As Integer = 0
            Dim contador_off As Integer = 0
            Dim contador_array As Integer = 0
            Dim id_pedido As Boolean = False
            array_id = False
            'Select de todos os amigos
            Dim command9 As New SqlCommand("SELECT * FROM amigos WHERE amigos_1=@nome OR amigos_2=@nome", connection_act)
            command9.Parameters.AddWithValue("@nome", login_txt.Text)
            connection_act.Open()
            Dim reader2_act As SqlDataReader = command9.ExecuteReader()
            While reader2_act.Read()
                If reader2_act("amigos_1") <> login_txt.Text Then
                    If reader2_act("pedido") = "Sim" Then
                        array_amigos_1(contador_amigos_1) = reader2_act("amigos_1")
                        contador_amigos_1 = contador_amigos_1 + 1
                        num_max = num_max + 1
                    ElseIf reader2_act("pedido") = "Nao" Then
                        If nv_pedido = False Then
                            If reader2_act("req") <> login_txt.Text Then
                                pedidos.amigo_lbl.Text = reader2_act("amigos_1")
                                id_pedido = True
                            End If
                        End If
                    End If
                ElseIf reader2_act("amigos_2") <> login_txt.Text Then
                    If reader2_act("pedido") = "Sim" Then
                        array_amigos_2(contador_amigos_2) = reader2_act("amigos_2")
                        contador_amigos_2 = contador_amigos_2 + 1
                        num_max = num_max + 1
                    ElseIf reader2_act("pedido") = "Nao" Then
                        If nv_pedido = False Then
                            If reader2_act("req") <> login_txt.Text Then
                                pedidos.amigo_lbl.Text = reader2_act("amigos_2")
                                id_pedido = True
                            End If
                        End If
                    End If
                End If
            End While
            If id_pedido = True Then
                nv_pedido = True
                pedidos.Show()
            End If
            reader2_act.Close()
            connection_act.Close()
            'Select de todos os amigos, e divide dos que estao on/off
            Dim command4 As New SqlCommand("SELECT nome, online From t_login", connection_act)
            connection_act.Open()
            Dim reader4_act As SqlDataReader = command4.ExecuteReader()
            While reader4_act.Read()
                contador_array = 0
                For i As Integer = 0 To num_max
                    If array_amigos_1(contador_array) = reader4_act("nome") Then
                        If reader4_act("online") = "Online" Then
                            array_online(contador_on) = array_amigos_1(contador_array)
                            contador_on = contador_on + 1
                        ElseIf reader4_act("online") = "Offline" Then
                            array_offline(contador_off) = array_amigos_1(contador_array)
                            contador_off = contador_off + 1
                        End If
                    ElseIf array_amigos_2(contador_array) = reader4_act("nome") Then
                        If reader4_act("online") = "Online" Then
                            array_online(contador_on) = array_amigos_2(contador_array)
                            contador_on = contador_on + 1
                        ElseIf reader4_act("online") = "Offline" Then
                            array_offline(contador_off) = array_amigos_2(contador_array)
                            contador_off = contador_off + 1
                        End If
                    End If
                    contador_array = contador_array + 1
                Next
            End While
            reader4_act.Close()
            connection_act.Close()
            'redim para o tamanho actual
            contador_on = contador_on - 1
            contador_off = contador_off - 1
            ReDim Preserve array_online(contador_on)
            ReDim Preserve array_offline(contador_off)
            'Comparador
            If array_online.Length <> count_on Then
                array_id = True
            ElseIf array_offline.Length <> count_off Then
                array_id = True
            End If
            'adiciona se mudar
            If array_id = True Then
                num_max = 0
                list_contactos.Items.Clear()
                count_on = array_online.Length
                count_off = array_offline.Length

                For i As Integer = 0 To contador_on
                    list_contactos.Items.Add(array_online(i))
                    num_max = num_max + 1
                Next

                For i As Integer = 0 To contador_off
                    list_contactos.Items.Add(array_offline(i))
                    list_contactos.Items.Item(num_max).ImageIndex = 1
                    num_max = num_max + 1
                Next
                amigos_g.Text = "Amigos - " & list_contactos.Items.Count
                list_contactos.SmallImageList = list_state
                muda_estado = True 'Obriga a actualizar o estado.
            End If
        Catch
            connection_act.Close()
        End Try

        'carrega avatar +- 30 em 30 sec(outro)
        img_ref = img_ref + 1
        If amigo_c_txt.Text = "" Then
            avatar_2_img.Image = Nothing
        ElseIf amigo_c_txt.Text = "Amigo" Then
            avatar_2_img.Image = Nothing
        ElseIf img_ref = 250 Then
            img_ref = 0
            Try
                Dim command2 As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome_load", connection_act)
                command2.Parameters.AddWithValue("@nome_load", amigo_c_txt.Text)
                connection_act.Open()
                Dim picture2 As System.Drawing.Image = Nothing
                Dim pictureData2 As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
                Dim stream2 As New IO.MemoryStream(pictureData2)
                connection_act.Close()
                picture2 = System.Drawing.Image.FromStream(stream2)
                avatar_2_img.Image = picture2
            Catch
                connection_act.Close()
            End Try
        End If

        'actualiza a lbl "modo offline", so se sofreu uma actualizaçao
        Try
            If array_id = True Then
                'Para saber se essa conversa esta on/off
                Dim roda_id As Boolean = False
                For i As Integer = 0 To list_contactos.Items.Count - 1
                    If list_contactos.Items.Item(i).Text = amigo_c_txt.Text Then
                        If list_contactos.Items.Item(i).ImageIndex = 1 Then
                            roda_id = True
                        End If
                    End If
                Next

                If roda_id = True Then
                    conversa_g.Text = "Modo Offline"
                ElseIf roda_id = False Then
                    conversa_g.Text = ""
                End If

                'Verifica se foi bloqueado, entao remove o txt da conversa, avatar e cv currente
                Dim conversa_id As Boolean = False

                For i As Integer = 0 To list_contactos.Items.Count - 1
                    If list_contactos.Items.Item(i).Text = amigo_c_txt.Text Then
                        conversa_id = True
                    End If
                Next

                If conversa_id = False Then
                    sms_rec_txt.Text = ""
                    amigo_c_txt.Text = ""
                    avatar_2_img.Image = Nothing
                End If
            End If
        Catch
            'Apenas para nao dar erro
        End Try


        'Verifica estado da conta
        Try
            Dim command7 As New SqlCommand("SELECT nome, online FROM t_login WHERE nome=@nome", connection_act)
            command7.Parameters.AddWithValue("@nome", login_txt.Text)
            connection_act.Open()
            Dim reader_act3 As SqlDataReader = command7.ExecuteReader()
            reader_act3.Read()
            If reader_act3("online") = "Offline" Then
                Timer1.Enabled = False
                terminar_sessao = True 'para nao auto iniciar
                desconectado = True 'para nao mudar o estado da conta ao sair
                Login.aviso_lbl.Visible = True
                Login.aviso_lbl.Text = "A sua conta foi desconectada !"
                NotifyIcon1.Visible = False
                close_form = True
                Login.Show()
                Me.Close()
            End If
            reader_act3.Close()
            connection_act.Close()
        Catch
            connection_act.Close()
        End Try

        'Leitura dos Downloads
        If nv_download = False Then
            Try
                Dim visto_v As String = "Nao"
                Dim premissoes_v As String = "Privado"
                Dim command5 As New SqlCommand("SELECT id FROM transferencias WHERE para=@para AND visto=@visto AND premissoes=@premissoes", connection_act)
                command5.Parameters.AddWithValue("@para", login_txt.Text)
                command5.Parameters.AddWithValue("@visto", visto_v)
                command5.Parameters.AddWithValue("@premissoes", premissoes_v)
                connection_act.Open()
                Dim reader_act7 As SqlDataReader = command5.ExecuteReader
                reader_act7.Read()
                Try
                    'Serve apenas para char o outro form, se ouver um novo ficheiro.
                    Dim teste = reader_act7("id")
                    nv_download = True
                    receberfile.Show()
                    reader_act7.Close()
                    connection_act.Close()
                Catch
                    reader_act7.Close()
                    connection_act.Close()
                End Try
            Catch
                connection_act.Close()
            End Try
        End If

        'Leitura das chamadas
        If nv_chamada = False Then
            Try
                Dim visto_v As String = "Espera"
                Dim premissoes_v As String = "Privado"
                Dim command5 As New SqlCommand("SELECT id, de FROM voip WHERE para=@para AND visto=@visto", connection_act)
                command5.Parameters.AddWithValue("@para", login_txt.Text)
                command5.Parameters.AddWithValue("@visto", visto_v)
                connection_act.Open()
                Dim reader_act7 As SqlDataReader = command5.ExecuteReader
                reader_act7.Read()
                Try
                    'Charmar o outro form
                    Dim teste = reader_act7("id")
                    nv_chamada = True
                    voip.Show()
                    reader_act7.Close()
                    connection_act.Close()
                Catch
                    reader_act7.Close()
                    connection_act.Close()
                End Try
            Catch
                connection_act.Close()
            End Try
        End If

        'Actualiza os estados
        Try
            Dim array_amigos(1000) As String
            Dim array_estados(1000) As String
            Dim contador As Integer = 0

            'Adiciona todos os amigos ao array
            For i As Integer = 0 To list_contactos.Items.Count - 1
                array_amigos(i) = list_contactos.Items.Item(i).Text
            Next

            'Verifica o estado de cada amigo, e coloca no array
            Dim command40 As New SqlCommand("SELECT nome, estado From t_login", connection_act)
            connection_act.Open()
            Dim reader40_act As SqlDataReader = command40.ExecuteReader()
            While reader40_act.Read()
                contador = 0
                For i As Integer = 0 To list_contactos.Items.Count - 1
                    If array_amigos(contador) = reader40_act("nome") Then
                        If reader40_act("estado") = "Online" Then
                            array_estados(contador) = "Online"
                        ElseIf reader40_act("estado") = "Ocupado" Then
                            array_estados(contador) = "Ocupado"
                        ElseIf reader40_act("estado") = "Ausente" Then
                            array_estados(contador) = "Ausente"
                        ElseIf reader40_act("estado") = "VoltoJa" Then
                            array_estados(contador) = "VoltoJa"
                        End If
                    End If
                    contador = contador + 1
                Next
            End While
            reader40_act.Close()
            connection_act.Close()

            'redim para o tamanho actual, para poupar ram
            ReDim Preserve array_estados(list_contactos.Items.Count - 1)
            ReDim Preserve estados(list_contactos.Items.Count - 1)

            'Compara os arrays (geral com o acima)
            If muda_estado = False Then
                For i As Integer = 0 To list_contactos.Items.Count - 1
                    If estados(i) <> array_estados(i) Then
                        muda_estado = True
                    End If
                Next
            End If

            'Se diferente, actualiza os estados
            If muda_estado = True Then
                Array.Clear(estados, 0, estados.Length)
                For i As Integer = 0 To list_contactos.Items.Count - 1
                    estados(i) = array_estados(i)
                Next

                For i As Integer = 0 To list_contactos.Items.Count - 1
                    If list_contactos.Items.Item(i).ImageIndex <> 1 Then 'O estado offline e atribuido em cima
                        If estados(i) = "Online" Then
                            list_contactos.Items.Item(i).ImageIndex = 0
                        ElseIf estados(i) = "Ocupado" Then
                            list_contactos.Items.Item(i).ImageIndex = 2
                        ElseIf estados(i) = "Ausente" Then
                            list_contactos.Items.Item(i).ImageIndex = 3
                        ElseIf estados(i) = "VoltoJa" Then
                            list_contactos.Items.Item(i).ImageIndex = 4
                        End If
                    End If
                Next
                list_contactos.SmallImageList = list_state
                muda_estado = False
            End If
        Catch
            connection_act.Close()
        End Try
    End Sub

#End Region

#Region "List Contactos"

    Private Sub list_contactos_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles list_contactos.GotFocus
        sms_txt.Focus()
    End Sub

    Private Sub list_contactos_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles list_contactos.LostFocus
        list_n_conversas.SelectedItems.Clear()
    End Sub

    Private Sub list_contactos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles list_contactos.SelectedIndexChanged
        Try
            If amigo_c_txt.Text <> list_contactos.SelectedItems(0).Text Then

                sms_rec_txt.Text = ""

                If ver_historico_id = "1" Then
                    lbl_load.Visible = True
                    sms_rec_txt.Visible = False
                    lbl_load.Refresh()
                End If

                amigo_c_txt.Text = list_contactos.SelectedItems(0).Text

                'Para saber se essa conversa esta on/off
                Dim roda_id As Boolean = False
                For i As Integer = 0 To list_contactos.Items.Count - 1
                    If list_contactos.Items.Item(i).Text = amigo_c_txt.Text Then
                        If list_contactos.Items.Item(i).ImageIndex = 1 Then
                            roda_id = True
                        End If
                    End If
                Next

                If roda_id = True Then
                    conversa_g.Text = "Modo Offline"
                ElseIf roda_id = False Then
                    conversa_g.Text = ""
                End If

                'escreve que a sms foi vista, escrever em todos os campos deste contacto, onde a sms e = a "Nao"
                Dim visto_v As String = "Sim"
                Dim command3 As New SqlCommand("UPDATE sms SET visto=@visto WHERE para=@para AND de=@de AND visto='Nao'", connection)
                command3.Parameters.AddWithValue("@de", amigo_c_txt.Text)
                command3.Parameters.AddWithValue("@para", login_txt.Text)
                command3.Parameters.AddWithValue("@visto", visto_v)
                connection.Open()
                command3.ExecuteNonQuery()
                connection.Close()

                'carrega avatar no click, pois ele so vai ser actualizado de 1 em 1 min
                Dim command2 As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome_load", connection)
                command2.Parameters.AddWithValue("@nome_load", amigo_c_txt.Text)
                connection.Open()
                Dim picture2 As System.Drawing.Image = Nothing
                Dim pictureData2 As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
                Dim stream2 As New IO.MemoryStream(pictureData2)
                connection.Close()
                picture2 = System.Drawing.Image.FromStream(stream2)
                avatar_2_img.Image = picture2

                'Esta em ultimo lugar, pois se ouver overflow da caixa, ele processa os dados anteriores
                'Primeiro load da conversa, e um load "geral" da conversa, pois os outros(enviar e actualizar) vao apenas actualizar a caixa, com o que ja la esta.
                If ver_historico_id = "1" Then
                    Dim var1_v As String = ""
                    Dim var2_v As String = ""
                    Dim sms_v As String = ""
                    Dim command As New SqlCommand("SELECT de, mensagem, data FROM sms WHERE para=@para AND de=@de OR para=@de AND de=@para ORDER BY id", connection)
                    command.Parameters.AddWithValue("@de", amigo_c_txt.Text)
                    command.Parameters.AddWithValue("@para", login_txt.Text)
                    connection.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()

                    Dim source As String = ""
                    Dim sms_final As String = ""

                    Dim tipo As String = ""
                    Dim tipo_1 As String = ""
                    Dim tipo_2 As String = ""
                    Dim tamanho As Integer
                    Dim tamanho_1 As String = ""
                    Dim tamanho_2 As String = ""
                    Dim cor As String = ""
                    Dim cor_1 As String = ""
                    Dim cor_2 As String = ""

                    While reader.Read()
                        source = reader("mensagem")

                        tipo_1 = InStr(source, "<tipo>") '6
                        tipo_2 = InStr(source, "</tipo>") '7
                        tamanho_1 = InStr(source, "<tamanho>") '9
                        tamanho_2 = InStr(source, "</tamanho>") '10
                        cor_1 = InStr(source, "<cor>") '5
                        cor_2 = InStr(source, "</cor>") '6

                        If tipo_1 = 1 And tipo_2 > tipo_1 Then
                            tipo = Mid(source, tipo_1 + 6, tipo_2 - 7)
                        End If
                        If tamanho_1 = tipo_2 + 7 And tamanho_2 > tamanho_1 Then
                            tamanho = Mid(source, tamanho_1 + 9, (tamanho_2 - 10) - (tipo_2 + 6))
                        End If
                        If cor_1 = tamanho_2 + 10 And cor_2 > cor_1 Then
                            cor = Mid(source, cor_1 + 5, (cor_2 - 6) - (tamanho_2 + 9))
                        End If

                        sms_final = Mid(source, cor_2 + 6)
                        sms_rec_txt.SelectionFont = New Font(tipo, tamanho, FontStyle.Regular)

                        'Caso seja hexadecimal
                        Dim cor_tipo As String = InStr(cor, "#") '1
                        If cor_tipo = 1 Then
                            Dim RGBcolor = HexToColor(cor)
                            sms_rec_txt.SelectionColor = RGBcolor
                        Else
                            sms_rec_txt.SelectionColor = Color.FromName(cor)
                        End If

                        If ver_data_id = "1" Then
                            sms_rec_txt.AppendText(vbNewLine & reader("de") & " (" & reader("data") & ")" & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                        ElseIf ver_data_id = "0" Then
                            sms_rec_txt.AppendText(vbNewLine & reader("de") & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                        End If
                    End While

                    'Scrol para o fim
                    sms_rec_txt.Select(sms_rec_txt.Text.Length, 0)
                    sms_rec_txt.ScrollToCaret()

                    reader.Close()
                    connection.Close()

                    lbl_load.Visible = False
                    sms_rec_txt.Visible = True
                End If
            End If
        Catch
            connection.Close()
        End Try
    End Sub

#End Region

#Region "ListBox Conversas"

    Private Sub list_n_conversas_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles list_n_conversas.GotFocus
        sms_txt.Focus()
    End Sub

    Private Sub list_n_conversas_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles list_n_conversas.LostFocus
        list_contactos.SelectedItems.Clear()
    End Sub

    Private Sub list_n_conversas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles list_n_conversas.SelectedIndexChanged
        Try
            sms_rec_txt.Text = ""

            If ver_historico_id = "1" Then
                lbl_load.Visible = True
                sms_rec_txt.Visible = False
                lbl_load.Refresh()
            End If

            amigo_c_txt.Text = list_n_conversas.SelectedItems(0).Text

            'Para saber se essa conversa esta on/off
            Dim roda_id As Boolean = False
            For i As Integer = 0 To list_contactos.Items.Count - 1
                If list_contactos.Items.Item(i).Text = amigo_c_txt.Text Then
                    If list_contactos.Items.Item(i).ImageIndex = 1 Then
                        roda_id = True
                    End If
                End If
            Next

            If roda_id = True Then
                conversa_g.Text = "Modo Offline"
            ElseIf roda_id = False Then
                conversa_g.Text = ""
            End If

            'carrega avatar no click, pois ele so vai ser actualizado de 1 em 1 min
            Dim command2 As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome_load", connection)
            command2.Parameters.AddWithValue("@nome_load", amigo_c_txt.Text)
            connection.Open()
            Dim picture2 As System.Drawing.Image = Nothing
            Dim pictureData2 As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
            Dim stream2 As New IO.MemoryStream(pictureData2)
            connection.Close()
            picture2 = System.Drawing.Image.FromStream(stream2)
            avatar_2_img.Image = picture2

            If ver_historico_id = "1" Then
                'Esta em ultimo lugar, pois se ouver overflow da caixa, ele processa os dados anteriores
                'Primeiro load da conversa, e um load "geral" da conversa, pois os outros(enviar e actualizar) vao apenas actualizar a caixa, com o que ja la esta.
                Dim var1_v As String = ""
                Dim var2_v As String = ""
                Dim command As New SqlCommand("SELECT de, mensagem, data FROM sms WHERE para=@para AND de=@de OR para=@de AND de=@para ORDER BY id", connection)
                command.Parameters.AddWithValue("@de", amigo_c_txt.Text)
                command.Parameters.AddWithValue("@para", login_txt.Text)
                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()

                Dim source As String = ""
                Dim sms_final As String = ""

                Dim tipo As String = ""
                Dim tipo_1 As String = ""
                Dim tipo_2 As String = ""
                Dim tamanho As Integer
                Dim tamanho_1 As String = ""
                Dim tamanho_2 As String = ""
                Dim cor As String = ""
                Dim cor_1 As String = ""
                Dim cor_2 As String = ""

                While reader.Read()
                    source = reader("mensagem")

                    tipo_1 = InStr(source, "<tipo>") '6
                    tipo_2 = InStr(source, "</tipo>") '7
                    tamanho_1 = InStr(source, "<tamanho>") '9
                    tamanho_2 = InStr(source, "</tamanho>") '10
                    cor_1 = InStr(source, "<cor>") '5
                    cor_2 = InStr(source, "</cor>") '6

                    If tipo_1 = 1 And tipo_2 > tipo_1 Then
                        tipo = Mid(source, tipo_1 + 6, tipo_2 - 7)
                    End If
                    If tamanho_1 = tipo_2 + 7 And tamanho_2 > tamanho_1 Then
                        tamanho = Mid(source, tamanho_1 + 9, (tamanho_2 - 10) - (tipo_2 + 6))
                    End If
                    If cor_1 = tamanho_2 + 10 And cor_2 > cor_1 Then
                        cor = Mid(source, cor_1 + 5, (cor_2 - 6) - (tamanho_2 + 9))
                    End If

                    sms_final = Mid(source, cor_2 + 6)
                    sms_rec_txt.SelectionFont = New Font(tipo, tamanho, FontStyle.Regular)

                    'Caso seja hexadecimal
                    Dim cor_tipo As String = InStr(cor, "#") '1
                    If cor_tipo = 1 Then
                        Dim RGBcolor = HexToColor(cor)
                        sms_rec_txt.SelectionColor = RGBcolor
                    Else
                        sms_rec_txt.SelectionColor = Color.FromName(cor)
                    End If

                    If ver_data_id = "1" Then
                        sms_rec_txt.AppendText(vbNewLine & reader("de") & " (" & reader("data") & ")" & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                    ElseIf ver_data_id = "0" Then
                        sms_rec_txt.AppendText(vbNewLine & reader("de") & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                    End If
                End While

                'Scrol para o fim
                sms_rec_txt.Select(sms_rec_txt.Text.Length, 0)
                sms_rec_txt.ScrollToCaret()

                reader.Close()
                connection.Close()

                lbl_load.Visible = False
                sms_rec_txt.Visible = True

            ElseIf ver_historico_id = "0" Then ' para aprecerem as novas sms, caso nao tenho o historico visivel

                Dim var1_v As String = ""
                Dim var2_v As String = ""
                Dim command As New SqlCommand("SELECT de, mensagem, data FROM sms WHERE para=@para AND de=@de AND visto='Nao' ORDER BY id", connection)
                command.Parameters.AddWithValue("@de", amigo_c_txt.Text)
                command.Parameters.AddWithValue("@para", login_txt.Text)
                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()

                Dim source As String = ""
                Dim sms_final As String = ""

                Dim tipo As String = ""
                Dim tipo_1 As String = ""
                Dim tipo_2 As String = ""
                Dim tamanho As Integer
                Dim tamanho_1 As String = ""
                Dim tamanho_2 As String = ""
                Dim cor As String = ""
                Dim cor_1 As String = ""
                Dim cor_2 As String = ""

                While reader.Read()
                    source = reader("mensagem")

                    tipo_1 = InStr(source, "<tipo>") '6
                    tipo_2 = InStr(source, "</tipo>") '7
                    tamanho_1 = InStr(source, "<tamanho>") '9
                    tamanho_2 = InStr(source, "</tamanho>") '10
                    cor_1 = InStr(source, "<cor>") '5
                    cor_2 = InStr(source, "</cor>") '6

                    If tipo_1 = 1 And tipo_2 > tipo_1 Then
                        tipo = Mid(source, tipo_1 + 6, tipo_2 - 7)
                    End If
                    If tamanho_1 = tipo_2 + 7 And tamanho_2 > tamanho_1 Then
                        tamanho = Mid(source, tamanho_1 + 9, (tamanho_2 - 10) - (tipo_2 + 6))
                    End If
                    If cor_1 = tamanho_2 + 10 And cor_2 > cor_1 Then
                        cor = Mid(source, cor_1 + 5, (cor_2 - 6) - (tamanho_2 + 9))
                    End If

                    sms_final = Mid(source, cor_2 + 6)
                    sms_rec_txt.SelectionFont = New Font(tipo, tamanho, FontStyle.Regular)

                    'Caso seja hexadecimal
                    Dim cor_tipo As String = InStr(cor, "#") '1
                    If cor_tipo = 1 Then
                        Dim RGBcolor = HexToColor(cor)
                        sms_rec_txt.SelectionColor = RGBcolor
                    Else
                        sms_rec_txt.SelectionColor = Color.FromName(cor)
                    End If

                    If ver_data_id = "1" Then
                        sms_rec_txt.AppendText(vbNewLine & reader("de") & " (" & reader("data") & ")" & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                    ElseIf ver_data_id = "0" Then
                        sms_rec_txt.AppendText(vbNewLine & reader("de") & " diz: " & vbNewLine & "    " & sms_final & vbNewLine)
                    End If
                End While

                'Scrol para o fim
                sms_rec_txt.Select(sms_rec_txt.Text.Length, 0)
                sms_rec_txt.ScrollToCaret()

                reader.Close()
                connection.Close()
            End If

            'escreve que a sms foi vista, escrever em todos os campos deste contacto, onde a sms e = a "Nao"
            Dim visto_v As String = "Sim"
            Dim command3 As New SqlCommand("UPDATE sms SET visto=@visto WHERE para=@para AND de=@de AND visto='Nao'", connection)
            command3.Parameters.AddWithValue("@de", amigo_c_txt.Text)
            command3.Parameters.AddWithValue("@para", login_txt.Text)
            command3.Parameters.AddWithValue("@visto", visto_v)
            connection.Open()
            command3.ExecuteNonQuery()
            connection.Close()

        Catch
            connection.Close()
        End Try
    End Sub
#End Region

#Region "Tray"

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        If Me.Visible Then
            Me.WindowState = FormWindowState.Minimized
            Me.Hide()
            tray_abrir.Text = "&Abrir"
        Else
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            tray_abrir.Text = "&Esconder"
            Me.Focus()
            Me.TopMost = True
            Me.TopMost = False
        End If
    End Sub

    Private Sub main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not close_form Then
            e.Cancel = True
            Me.WindowState = FormWindowState.Minimized
            Me.Hide()
            tray_abrir.Text = "&Abrir"
        End If
    End Sub

    Private Sub tray_sair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tray_sair.Click
        close_form = True
        Me.Close()
    End Sub

    Private Sub tray_terminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tray_terminar.Click
        close_form = True
        terminar_sessao = True
        Login.Show()
        Me.Close()
    End Sub

    Private Sub tray_abrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tray_abrir.Click
        If Me.Visible Then
            Me.WindowState = FormWindowState.Minimized
            Me.Hide()
            tray_abrir.Text = "&Abrir"
        Else
            Me.Show()
            Me.WindowState = FormWindowState.Normal
            tray_abrir.Text = "&Esconder"
            Me.Focus()
            Me.TopMost = True
            Me.TopMost = False
        End If
    End Sub

    Private Sub OnlineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineToolStripMenuItem.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "Online")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "Online"
        Me.NotifyIcon1.Icon = My.Resources.icon
    End Sub

    Private Sub AusenteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AusenteToolStripMenuItem.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "Ausente")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "Ausente"
        Me.NotifyIcon1.Icon = My.Resources.away1
    End Sub

    Private Sub OcupadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OcupadoToolStripMenuItem.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "Ocupado")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "Ocupado"
        Me.NotifyIcon1.Icon = My.Resources.busy1
    End Sub

    Private Sub VoltoJáToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoltoJáToolStripMenuItem.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "VoltoJa")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "VoltoJa"
        Me.NotifyIcon1.Icon = My.Resources.brb1
    End Sub

#End Region

#Region "TextBox"

    'SMS REC
    Private Sub sms_rec_txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles sms_rec_txt.KeyPress
        e.Handled = True
        sms_txt.Focus()
    End Sub

    Private Sub sms_rec_txt_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles sms_rec_txt.LinkClicked
        'Abrir com o default browser o link
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub

    'SMS
    Private Sub sms_txt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sms_txt.TextChanged
        If sms_txt.Text = "" Then
            enviar_btn.Enabled = False
        Else
            enviar_btn.Enabled = True
        End If
    End Sub

    Private Sub sms_txt_LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles sms_txt.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub

#End Region

#Region "Menus - Botao Direito"

#Region "SMS Rec"
    Private Sub rsms_citar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rsms_citar.Click
        If sms_txt.TextLength <= 500 Then
            If sms_rec_txt.SelectedText.Length <= 500 Then
                sms_txt.Text = sms_txt.Text + sms_rec_txt.SelectedText
            End If
        End If
    End Sub

    Private Sub rsms_copiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rsms_copiar.Click
        sms_rec_txt.Copy()
    End Sub

    Private Sub rsms_selec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rsms_selec.Click
        sms_rec_txt.Focus()
        sms_rec_txt.SelectAll()
    End Sub
#End Region

#Region "SMS"
    Private Sub sms_cortar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sms_cortar.Click
        sms_txt.Cut()
    End Sub

    Private Sub sms_copiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sms_copiar.Click
        sms_txt.Copy()
    End Sub

    Private Sub sms_colar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sms_colar.Click
        If Clipboard.ContainsText Then
            If Clipboard.GetText.Length <= 500 Then
                sms_txt.Paste()
            End If
        End If
    End Sub

    Private Sub sms_eliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sms_eliminar.Click
        sms_txt.SelectedText = ""
    End Sub

    Private Sub sms_selec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sms_selec.Click
        sms_txt.Focus()
        sms_txt.SelectAll()
    End Sub
#End Region

#Region "Imagem"
    Private Sub ObterImagem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ObterImagem.Click
        Try
            Dim path As String = ""
            Dim img_nome As String = ""
            Me.FolderBrowserDialog1.ShowNewFolderButton = True
            If Me.FolderBrowserDialog1.ShowDialog = DialogResult.Cancel Then
                Exit Sub
            Else
                path = FolderBrowserDialog1.SelectedPath
            End If
            FolderBrowserDialog1.Dispose()
            img_nome = InputBox("Escreva um nome para a imagem", "Guardar Imagem")
            If img_nome <> "" Then
                path = path & "\" & img_nome & ".png"
                Dim img = New Bitmap(avatar_2_img.Image)
                img.Save(path, System.Drawing.Imaging.ImageFormat.Png)
                MsgBox("A imagem foi criada com sucesso!", MsgBoxStyle.Information, "Informação")
            End If
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Opçoes"
    Private Sub sms_rec_menu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles sms_rec_menu.Opening
        If sms_rec_txt.Text = "" Then
            rsms_citar.Enabled = False
            rsms_copiar.Enabled = False
            rsms_selec.Enabled = False
        ElseIf sms_rec_txt.SelectedText <> "" Then
            rsms_citar.Enabled = True
            rsms_copiar.Enabled = True
            rsms_selec.Enabled = True
        ElseIf sms_rec_txt.Text <> "" Then
            rsms_citar.Enabled = False
            rsms_copiar.Enabled = False
            rsms_selec.Enabled = True
        End If
    End Sub

    Private Sub sms_menu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles sms_menu.Opening
        If sms_txt.Text = "" Then
            sms_cortar.Enabled = False
            sms_copiar.Enabled = False
            sms_colar.Enabled = True
            sms_eliminar.Enabled = False
            sms_selec.Enabled = False
        ElseIf sms_txt.SelectedText <> "" Then
            sms_cortar.Enabled = True
            sms_copiar.Enabled = True
            sms_colar.Enabled = True
            sms_eliminar.Enabled = True
            sms_selec.Enabled = True
        ElseIf sms_txt.Text <> "" Then
            sms_cortar.Enabled = False
            sms_copiar.Enabled = False
            sms_colar.Enabled = True
            sms_eliminar.Enabled = False
            sms_selec.Enabled = True
        End If
    End Sub

    Private Sub imagem_menu_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles imagem_menu.Opening
        If amigo_c_txt.Text = "" Then
            ObterImagem.Enabled = False
        ElseIf amigo_c_txt.Text = "Amigo" Then
            ObterImagem.Enabled = False
        Else
            ObterImagem.Enabled = True
        End If
    End Sub
#End Region

#End Region

#Region "Menus"

#Region "Adicionar User"
    Private Sub mn_adicionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_adicionar.Click
hell:
        Dim existe_v As String = "Nao"
        Dim amigo_v As String = "Nao"
        Dim amigo_block_v As String = "Nao"
        Dim amigo_lv2 As String = "Nao"
        Try
            Dim nv_amigo As String = InputBox("Escreva o nome do contacto a adicionar.", "Adicionar Amigo")

            If nv_amigo = "" Then
            ElseIf nv_amigo.Length > 12 Then
                MsgBox("Os nicks têm limite de 12 caracteres.", MsgBoxStyle.Exclamation, "Informação")
            Else
                'verifica se o amigo existe
                Dim command As New SqlCommand("SELECT nome, count(nome) as quantos FROM t_login WHERE nome=@nome GROUP BY nome", connection)
                command.Parameters.AddWithValue("@nome", nv_amigo)
                connection.Open()
                Dim reader As SqlDataReader = command.ExecuteReader()
                Try 'Tem o try, para aparecer a msgbox. Com o While apenas nao da "erro".
                    reader.Read()
                    nv_amigo = reader("nome")
                    If reader("quantos") = 1 Then
                        If nv_amigo = login_txt.Text Then
                            MsgBox("Esta a tentar adicionar a propria conta.", MsgBoxStyle.Exclamation, "Informação")
                            connection.Close()
                            GoTo hell
                        Else
                            existe_v = "Sim"
                        End If
                    End If
                Catch
                    MsgBox("Esse utilizador nao existe.", MsgBoxStyle.Exclamation, "Informação")
                    connection.Close()
                    GoTo hell
                End Try
                reader.Close()
                connection.Close()

                'verifica se os amigos estao ja estao "ligados".
                If existe_v = "Sim" Then
                    Dim command7 As New SqlCommand("SELECT count(amigos_1) as quantos FROM amigos WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                    command7.Parameters.AddWithValue("@nome_1", login_txt.Text)
                    command7.Parameters.AddWithValue("@nome_2", nv_amigo)
                    connection.Open()
                    Dim reader3 As SqlDataReader = command7.ExecuteReader()
                    While reader3.Read()
                        If reader3("quantos") = 1 Then
                            amigo_lv2 = "Sim"
                        Else
                            amigo_v = "Sim"
                        End If
                    End While
                    reader3.Close()
                    connection.Close()
                End If

                'Opçoes, se ja estiverem ligados
                If existe_v = "Sim" Then
                    If amigo_lv2 = "Sim" Then
                        Dim command6 As New SqlCommand("SELECT pedido FROM amigos WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                        command6.Parameters.AddWithValue("@nome_1", login_txt.Text)
                        command6.Parameters.AddWithValue("@nome_2", nv_amigo)
                        connection.Open()
                        Dim reader6 As SqlDataReader = command6.ExecuteReader()
                        While reader6.Read()
                            Dim read_pedido_v As String = reader6("pedido")
                            If read_pedido_v = "Block" Then
                                Dim a As String = ""
                                a = MsgBox("A sua e a ligação com '" & nv_amigo & "' está bloqueada." & vbNewLine & "Deseja fazer novamente o pedido de contacto?", MsgBoxStyle.YesNo, "Informação")
                                If a = vbNo Then
                                    connection.Close()
                                    Exit Sub
                                End If
                                amigo_block_v = "Sim"
                            ElseIf read_pedido_v = "Nao" Then
                                MsgBox("'" & nv_amigo & "' ja foi notificado do seu pedido.", MsgBoxStyle.Exclamation, "Informação")
                                connection.Close()
                                Exit Sub
                            ElseIf read_pedido_v = "Wait" Then
                                MsgBox("'" & nv_amigo & "' ja foi notificado do seu pedido.", MsgBoxStyle.Exclamation, "Informação")
                                connection.Close()
                                Exit Sub
                            ElseIf read_pedido_v = "Sim" Then
                                MsgBox("'" & nv_amigo & "' ja está na sua lista de amigos.", MsgBoxStyle.Exclamation, "Informação")
                                connection.Close()
                                Exit Sub
                            End If
                        End While
                        reader6.Close()
                        connection.Close()
                    End If
                End If

                'actualiza contacto bloqueado.
                If existe_v = "Sim" Then
                    If amigo_block_v = "Sim" Then
                        Dim command2 As New SqlCommand("UPDATE amigos SET pedido=@pedido, req=@req WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                        Dim pedido_v As String = "Nao"
                        command2.Parameters.AddWithValue("@nome_1", login_txt.Text)
                        command2.Parameters.AddWithValue("@nome_2", nv_amigo)
                        command2.Parameters.AddWithValue("@pedido", pedido_v)
                        command2.Parameters.AddWithValue("@req", login_txt.Text)
                        connection.Open()
                        command2.ExecuteNonQuery()
                        connection.Close()
                    End If
                End If

                'adiciona novo amigo
                If existe_v = "Sim" Then
                    If amigo_v = "Sim" Then
                        Dim a As String = ""
                        a = MsgBox("Tem mesmo a certeza que quer adicionar" & " '" & nv_amigo & "' ?", MsgBoxStyle.YesNo, "Informação")
                        If a = vbNo Then
                            connection.Close()
                            Exit Sub
                        End If
                        Dim command1 As New SqlCommand("INSERT INTO amigos (amigos_1, amigos_2, pedido, req) VALUES (@nome_1, @nome_2, @pedido, @req)", connection)
                        Dim pedido_v As String = "Nao"
                        command1.Parameters.AddWithValue("@nome_1", login_txt.Text)
                        command1.Parameters.AddWithValue("@nome_2", nv_amigo)
                        command1.Parameters.AddWithValue("@pedido", pedido_v)
                        command1.Parameters.AddWithValue("@req", login_txt.Text)
                        connection.Open()
                        command1.ExecuteNonQuery()
                        connection.Close()
                    End If
                End If
            End If
        Catch
            connection.Close()
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Remover User"
    Private Sub mn_bloquear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_bloquear.Click
        Dim existe_v As String = "Nao"
        Dim amigo_v As String = "Nao"
        Try
            If amigo_c_txt.Text = "" Or amigo_c_txt.Text = "Amigo" Then
                MsgBox("Seleccione o utilizador a bloquear.", MsgBoxStyle.Exclamation, "Informação")
            Else
                'verifica a ligaçao
                Dim command6 As New SqlCommand("SELECT pedido FROM amigos WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                command6.Parameters.AddWithValue("@nome_1", login_txt.Text)
                command6.Parameters.AddWithValue("@nome_2", amigo_c_txt.Text)
                connection.Open()
                Dim reader6 As SqlDataReader = command6.ExecuteReader()
                While reader6.Read()
                    Dim read_pedido_v As String = reader6("pedido")
                    If read_pedido_v = "Block" Then
                        MsgBox("'" & amigo_c_txt.Text & "' ja esta bloqueado", MsgBoxStyle.Information, "Block")
                        connection.Close()
                        Exit Sub
                    ElseIf read_pedido_v = "Sim" Then
                        amigo_v = "Sim"
                    End If
                End While
                reader6.Close()
                connection.Close()

                'block amigo
                If amigo_v = "Sim" Then
                    Dim a As String
                    a = MsgBox("Tem mesmo a certeza que quer bloquear" & " '" & amigo_c_txt.Text & "' ?", MsgBoxStyle.YesNo, "Informação")
                    If a = vbNo Then
                        connection.Close()
                        Exit Sub
                    End If
                    Dim command2 As New SqlCommand("UPDATE amigos SET pedido=@pedido WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                    Dim pedido_v As String = "Block"
                    command2.Parameters.AddWithValue("@nome_1", login_txt.Text)
                    command2.Parameters.AddWithValue("@nome_2", amigo_c_txt.Text)
                    command2.Parameters.AddWithValue("@pedido", pedido_v)
                    connection.Open()
                    command2.ExecuteNonQuery()
                    connection.Close()
                    sms_rec_txt.Text = ""
                    amigo_c_txt.Text = ""
                End If
            End If
        Catch
            connection.Close()
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Mudar Avatar"
    Private Sub mn_alterar_av_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_alterar_av.Click
        'janela de seleccionar imagem
        OpenFileDialog1.Title = "Seleccione o Avatar"
        OpenFileDialog1.FileName = "Nome do Avatar"
        OpenFileDialog1.RestoreDirectory = True
        OpenFileDialog1.Filter = "Ficheiros de Imagem (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|Todos os Ficheiros (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Try
            'janela de seleccionar imagem, ao carregar OK
            Dim link_v As String = OpenFileDialog1.FileName.ToString()

            'verifica tamanho da imagem (KB)
            Dim get_size As Integer = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(link_v).Length() / 1024))
            If get_size > 100 Then
                link_v = ""
                MsgBox("A imagem nao pode ser maior 100KB !", MsgBoxStyle.Exclamation, "Informação")
                Exit Sub
            End If

            ''verifica tamanho da imagem (AltxLarg)
            'Using img As Image = Image.FromFile(link_v)
            '    If img.Height <= 150 AndAlso img.Width <= 150 Then
            '        stream_link.Close()
            '    Else
            '        link_v = ""
            '        stream_link.Close()
            '        connection.Close()
            '        MsgBox("A imagem nao pode ser maior que 150px por 150px!", MsgBoxStyle.Exclamation, "Erro")
            '        Exit Sub
            '    End If
            'End Using

            'reescreve avatar
            Dim command As New SqlCommand("UPDATE avatar SET imagem=@imagem WHERE nome=@nome_save", connection)
            Using picture As Image = Image.FromFile(link_v)
                Using stream As New IO.MemoryStream
                    picture.Save(stream, Imaging.ImageFormat.Png)
                    command.Parameters.Add("@imagem", SqlDbType.Image).Value = stream.GetBuffer()
                End Using
            End Using
            command.Parameters.AddWithValue("@nome_save", login_txt.Text)
            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()

            'carrega novo avatar (proprio)
            Dim command2 As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome_load", connection)
            command2.Parameters.AddWithValue("@nome_load", login_txt.Text)
            connection.Open()
            Dim picture2 As System.Drawing.Image = Nothing
            Dim pictureData2 As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
            Dim stream2 As New IO.MemoryStream(pictureData2)
            connection.Close()
            picture2 = System.Drawing.Image.FromStream(stream2)
            avatar_1_img.Image = picture2
        Catch
            connection.Close()
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Envio de Ficheiros"

    Private Sub mn_ficheiros_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_ficheiros.Click
        If amigo_c_txt.Text = "" Then
            MsgBox("Seleccione um contacto da lista.", MsgBoxStyle.Information, "Informação")
        ElseIf amigo_c_txt.Text = "Amigo" Then
            MsgBox("Seleccione um contacto da lista.", MsgBoxStyle.Information, "Informação")
        Else
            'janela de seleccionar
            OpenFileDialog2.Title = "Seleccione o Ficheiro"
            OpenFileDialog2.FileName = "Nome do Ficheiro"
            OpenFileDialog2.RestoreDirectory = True
            OpenFileDialog2.Filter = "Todos os Ficheiros (*.*)|*.*"
            OpenFileDialog2.ShowDialog()
        End If
    End Sub

    Private Sub OpenFileDialog2_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog2.FileOk
        Try
            'janela de seleccionar imagem, ao carregar OK
            Dim CaminhoImagem As String = OpenFileDialog2.FileName.ToString()

            Dim get_nome As String = System.IO.Path.GetFileName(CaminhoImagem).ToString
            Dim get_extension As String = System.IO.Path.GetExtension(CaminhoImagem).ToLower.ToString
            Dim get_size As Integer = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length() / 1024000))
            Dim total_size As String = ""

            'verifica tamanho (MB)
            If get_size > 250 Then
                MsgBox("O ficheiro nao pode ser maior 250MB !", MsgBoxStyle.Exclamation, "Informação")
                Exit Sub
            End If

            'Abre o ecran de load

            'Transforma o tamanho
            If get_size = 1 Or get_size = 0 Then
                get_size = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length() / 1024))
                total_size = get_size & " KB"
                If get_size = 1 Or get_size = 0 Then
                    get_size = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length()))
                    total_size = get_size & " bytes"
                End If
            Else
                total_size = get_size & " MB"
            End If

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
            Dim Command As New SqlCommand("INSERT INTO ficheiros_priv (ID, ficheiro) VALUES (@ID, @ficheiro)", connection)
            Command.Parameters.AddWithValue("@ID", max_id_var)
            Dim FicheiroUp As IO.FileStream = IO.File.OpenRead(CaminhoImagem)
            Dim FileStream As New IO.BinaryReader(FicheiroUp)
            Dim BufferSize As Long = My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length()
            Command.Parameters.Add("@ficheiro", SqlDbType.VarBinary).Value = FileStream.ReadBytes(BufferSize)
            FicheiroUp.Flush()
            FicheiroUp.Close()
            FileStream.Close()
            connection.Open()
            Command.ExecuteNonQuery()
            connection.Close()

            'Escreve o registo da transferencia
            Dim visto_v As String = "Nao"
            Dim premissoes_v As String = "Privado"
            Dim Command2 As New SqlCommand("INSERT INTO transferencias (ID, de, para, visto, nome, extencao, tamanho, data, premissoes) VALUES (@ID, @de, @para, @visto, @nome, @extencao, @tamanho, @data, @premissoes)", connection)
            Command2.Parameters.AddWithValue("@ID", max_id_var)
            Command2.Parameters.AddWithValue("@de", login_txt.Text)
            Command2.Parameters.AddWithValue("@para", amigo_c_txt.Text)
            Command2.Parameters.AddWithValue("@visto", visto_v)
            Command2.Parameters.AddWithValue("@nome", get_nome)
            Command2.Parameters.AddWithValue("@extencao", get_extension)
            Command2.Parameters.AddWithValue("@tamanho", total_size)
            Command2.Parameters.AddWithValue("@data", data)
            Command2.Parameters.AddWithValue("@premissoes", premissoes_v)
            connection.Open()
            Command2.ExecuteNonQuery()
            connection.Close()

            MsgBox("O ficheiro foi enviado com sucesso!", MsgBoxStyle.Information, "Informação")
        Catch
            connection.Close()
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Estados"
    Private Sub OnlineToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnlineToolStripMenuItem1.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "Online")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "Online"
        Me.NotifyIcon1.Icon = My.Resources.icon
    End Sub

    Private Sub AusenteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AusenteToolStripMenuItem1.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "Ausente")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "Ausente"
        Me.NotifyIcon1.Icon = My.Resources.away1
    End Sub

    Private Sub OcupadoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OcupadoToolStripMenuItem1.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "Ocupado")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "Ocupado"
        Me.NotifyIcon1.Icon = My.Resources.busy1
    End Sub

    Private Sub VoltoJáToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoltoJáToolStripMenuItem1.Click
        Dim command2 As New SqlCommand("UPDATE t_login SET estado=@estado WHERE nome=@nome", connection)
        command2.Parameters.AddWithValue("@nome", login_txt.Text)
        command2.Parameters.AddWithValue("@estado", "VoltoJa")
        connection.Open()
        command2.ExecuteNonQuery()
        connection.Close()
        my_estado = "VoltoJa"
        Me.NotifyIcon1.Icon = My.Resources.brb1
    End Sub
#End Region

#Region "Outros"
    'menu, fechar/sair/sobre
    Private Sub mn_voltar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_voltar.Click
        close_form = True
        terminar_sessao = True
        Login.Show()
        Me.Close()
    End Sub

    Private Sub mn_sair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_sair.Click
        close_form = True
        Me.Close()
    End Sub

    Private Sub mn_style_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_style.Click
        opt.Show()
    End Sub

    Private Sub mn_transferencias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_transferencias.Click
        transferencias.Show()
    End Sub

    Private Sub mn_chamada_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_chamada.Click
        If nv_chamada = False Then
            If amigo_c_txt.Text = "" Then
                MsgBox("Seleccione um contacto da lista ", MsgBoxStyle.Information, "Informação")
            ElseIf amigo_c_txt.Text = "Amigo" Then
                MsgBox("Seleccione um contacto da lista ", MsgBoxStyle.Information, "Informação")
            Else
                'Para saber se essa conversa esta on/off
                Dim roda_id As Boolean = False
                For i As Integer = 0 To list_contactos.Items.Count - 1
                    If list_contactos.Items.Item(i).Text = amigo_c_txt.Text Then
                        If list_contactos.Items.Item(i).ImageIndex = 1 Then
                            roda_id = True
                        End If
                    End If
                Next

                If roda_id = True Then
                    MsgBox("Para realizar uma chamada, o contacto tem que estar online", MsgBoxStyle.Information, "Informação")
                Else
                    voip.Show()
                End If
            End If
        Else
            MsgBox("So pode efectuar uma chamada de cada vez.", MsgBoxStyle.Information, "Informação")
        End If
    End Sub
#End Region

#End Region

#Region "Outros"

#Region "Window Move"
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        m_Location = e.Location
        Timer1.Enabled = False
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        Timer1.Enabled = True
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If Not e.Button = Windows.Forms.MouseButtons.Left Then Exit Sub
        Dim Delta As Size
        Delta.Width = e.X - m_Location.X
        Delta.Height = e.Y - m_Location.Y
        Me.Location = Point.Add(Me.Location, Delta)
    End Sub

    Private Sub lbl_tit_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbl_tit.MouseDown
        m_Location = e.Location
        Timer1.Enabled = False
    End Sub

    Private Sub lbl_tit_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbl_tit.MouseUp
        Timer1.Enabled = True
    End Sub

    Private Sub lbl_tit_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbl_tit.MouseMove
        If Not e.Button = Windows.Forms.MouseButtons.Left Then Exit Sub
        Dim Delta As Size
        Delta.Width = e.X - m_Location.X
        Delta.Height = e.Y - m_Location.Y
        Me.Location = Point.Add(Me.Location, Delta)
    End Sub
#End Region

#Region "ControlBox"
    Private Sub px_minimizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles px_minimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub px_minimizar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles px_minimizar.MouseDown
        Try
            px_minimizar.Image = Image.FromFile(appPath + "\Skin\min2.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_minimizar_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles px_minimizar.MouseEnter
        Try
            px_minimizar.Image = Image.FromFile(appPath + "\Skin\min1.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_minimizar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles px_minimizar.MouseLeave
        Try
            px_minimizar.Image = Image.FromFile(appPath + "\Skin\nl1.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_maximizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles px_maximizar.Click
        Call w_max()
    End Sub

    Private Sub px_maximizar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles px_maximizar.MouseDown
        Try
            px_maximizar.Image = Image.FromFile(appPath + "\Skin\max2.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_maximizar_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles px_maximizar.MouseEnter
        Try
            px_maximizar.Image = Image.FromFile(appPath + "\Skin\max1.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_maximizar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles px_maximizar.MouseLeave
        Try
            px_maximizar.Image = Image.FromFile(appPath + "\Skin\nl2.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_fechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles px_fechar.Click
        Me.Close()
    End Sub

    Private Sub px_fechar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles px_fechar.MouseDown
        Try
            px_fechar.Image = Image.FromFile(appPath + "\Skin\cl2.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_fechar_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles px_fechar.MouseEnter
        Try
            px_fechar.Image = Image.FromFile(appPath + "\Skin\cl1.png")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub px_fechar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles px_fechar.MouseLeave
        Try
            px_fechar.Image = Image.FromFile(appPath + "\Skin\nl3.png")
        Catch ex As Exception

        End Try
    End Sub


    Private Sub lbl_tit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbl_tit.DoubleClick
        Call w_max()
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        Call w_max()
    End Sub

    'Func para maximizar
    Private Sub w_max()
        If w_state_max = False Then
            w_state_max = True

            'Grava o estado e o tamanho nao minimizado
            Dim current_w As Integer = Me.Width
            Dim current_h As Integer = Me.Height
            doc = XDocument.Load(appPath + "\config.xml")
            Dim editnode As XElement = doc.Descendants.Elements("Resolucao").First
            editnode.<estado>.Value = "full"
            editnode.<tamanho_w>.Value = current_w
            editnode.<tamanho_h>.Value = current_h
            doc.Save(appPath + "\config.xml")

            'Aplica o Fullscreen
            Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
            Me.Size = New System.Drawing.Size(workingRectangle.Width, workingRectangle.Height)
            Me.Location = New System.Drawing.Point(0, 0)

        ElseIf w_state_max = True Then
            w_state_max = False

            'Grava o estado(nao minimizado) 
            doc = XDocument.Load(appPath + "\config.xml")
            Dim editnode As XElement = doc.Descendants.Elements("Resolucao").First
            editnode.<estado>.Value = "non-full"
            doc.Save(appPath + "\config.xml")

            'Saca o ultimo tamanho, para depois o utilizar
            Dim last_w As Integer
            Dim last_h As Integer
            doc = XDocument.Load(appPath + "\config.xml")
            Dim qList = (From xe In doc.Descendants.Elements("Resolucao") _
            Select New With { _
            .tamanho_w = xe.<tamanho_w>.Value, _
            .tamanho_h = xe.<tamanho_h>.Value _
            }).First
            last_w = qList.tamanho_w
            last_h = qList.tamanho_h

            'Aplica o estado, e protege caso seja maior
            If last_w > Screen.PrimaryScreen.WorkingArea.Width Or last_h > Screen.PrimaryScreen.WorkingArea.Height Then
                Me.Size = New System.Drawing.Size(720, 385)
                Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
                Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
            Else
                Me.Size = New System.Drawing.Size(last_w, last_h)
                Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
                Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
            End If
        End If
        'Scrol para o fim
        sms_rec_txt.Select(sms_rec_txt.Text.Length, 0)
        sms_rec_txt.ScrollToCaret()
    End Sub
#End Region

#Region "Funcoes"
    Private Sub main_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            If my_estado = "Online" Then
                Me.NotifyIcon1.Icon = My.Resources.icon
            ElseIf my_estado = "Ausente" Then
                Me.NotifyIcon1.Icon = My.Resources.away1
            ElseIf my_estado = "Ocupado" Then
                Me.NotifyIcon1.Icon = My.Resources.busy1
            ElseIf my_estado = "VoltoJa" Then
                Me.NotifyIcon1.Icon = My.Resources.brb1
            End If
        End If
    End Sub

    'Func para piscar a janela
    Private Declare Function FlashWindow Lib "user32" Alias "FlashWindow" (ByVal wHandle As Int32, ByVal invertStates As Boolean) As Int32
#End Region

#End Region

#Region "Form Resize"

    Private Const HTCAPTION As Integer = 2
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const WM_NCHITTEST As Integer = &H84

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_NCHITTEST Then
            Dim pt As New Point(m.LParam.ToInt32)
            pt = Me.PointToClient(pt)
            If w_state_max = False Then
                If pt.X < 5 AndAlso pt.Y < 5 Then
                    m.Result = New IntPtr(HTTOPLEFT)
                ElseIf pt.X > (Me.Width - 5) AndAlso pt.Y < 5 Then
                    m.Result = New IntPtr(HTTOPRIGHT)
                ElseIf pt.Y < 5 Then
                    m.Result = New IntPtr(HTTOP)
                ElseIf pt.X < 5 AndAlso pt.Y > (Me.Height - 5) Then
                    m.Result = New IntPtr(HTBOTTOMLEFT)
                ElseIf pt.X > (Me.Width - 5) AndAlso pt.Y > (Me.Height - 5) Then
                    m.Result = New IntPtr(HTBOTTOMRIGHT)
                ElseIf pt.Y > (Me.Height - 5) Then
                    m.Result = New IntPtr(HTBOTTOM)
                ElseIf pt.X < 5 Then
                    m.Result = New IntPtr(HTLEFT)
                ElseIf pt.X > (Me.Width - 5) Then
                    m.Result = New IntPtr(HTRIGHT)
                Else
                    MyBase.WndProc(m)
                End If
            End If
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Private Sub main_ResizeBegin(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeBegin
        p_amigos.Visible = False
        p_avatar.Visible = False
        p_conv.Visible = False
        p_controlbox.Visible = False
    End Sub

    Private Sub main_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        p_amigos.Visible = True
        p_avatar.Visible = True
        p_conv.Visible = True
        p_controlbox.Visible = True

        'Grava o tamanho
        If w_state_max = False Then
            Dim current_w As Integer = Me.Width
            Dim current_h As Integer = Me.Height
            doc = XDocument.Load(appPath + "\config.xml")
            Dim editnode As XElement = doc.Descendants.Elements("Resolucao").First
            editnode.<tamanho_w>.Value = current_w
            editnode.<tamanho_h>.Value = current_h
            doc.Save(appPath + "\config.xml")
        End If
    End Sub

#End Region

#Region "Drag and Drop"

#Region "Avatar"
    Private Sub avatar_1_img_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles avatar_1_img.DragDrop
        Dim NomeDaImagem As String = System.IO.Path.GetFileName(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString)
        Dim CaminhoImagem As String = System.IO.Path.GetFullPath(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString)
        Try
            'verifica tamanho da imagem (KB)
            Dim get_size As Integer = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length() / 1024))
            If get_size > 100 Then
                MsgBox("A imagem nao pode ser maior 100KB !", MsgBoxStyle.Exclamation, "Informação")
                Exit Sub
            End If

            'reescreve avatar
            Dim command As New SqlCommand("UPDATE avatar SET imagem=@imagem WHERE nome=@nome_save", connection)
            Using picture As Image = Image.FromFile(CaminhoImagem)
                Using stream As New IO.MemoryStream
                    picture.Save(stream, Imaging.ImageFormat.Png)
                    command.Parameters.Add("@imagem", SqlDbType.Image).Value = stream.GetBuffer()
                End Using
            End Using
            command.Parameters.AddWithValue("@nome_save", login_txt.Text)
            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()

            'carrega novo avatar (proprio)
            Dim command2 As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome_load", connection)
            command2.Parameters.AddWithValue("@nome_load", login_txt.Text)
            connection.Open()
            Dim picture2 As System.Drawing.Image = Nothing
            Dim pictureData2 As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
            Dim stream2 As New IO.MemoryStream(pictureData2)
            connection.Close()
            picture2 = System.Drawing.Image.FromStream(stream2)
            avatar_1_img.Image = picture2
        Catch ex As Exception
            connection.Close()
            MsgBox("Erro ao mostrar a imagem, o ficheiro " & NomeDaImagem & " pode estar corrompido!", MsgBoxStyle.Exclamation, "Erro")
        End Try
    End Sub

    Private Sub avatar_1_img_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles avatar_1_img.DragEnter
        Dim ExtencaoDoFicheiro As String = System.IO.Path.GetExtension(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString).ToString.ToLower 'Vai buscar a extenção do ficheiro a ser dropado
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) And _
        ExtencaoDoFicheiro = ".jpg" Or _
        ExtencaoDoFicheiro = ".bmp" Or _
        ExtencaoDoFicheiro = ".png" Or _
        ExtencaoDoFicheiro = ".gif" Then 'Vê se está algum ficheiro a ser arrastado e vê se a extensão se confirma.
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
#End Region

#Region "Ficheiro"

    Dim txt_file As Boolean
    Private Sub sms_txt_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles sms_txt.DragEnter
        txt_file = False
        Dim ExtencaoDoFicheiro As String = System.IO.Path.GetExtension(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString).ToString.ToLower 'Vai buscar a extenção do ficheiro a ser dropado
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) And _
        ExtencaoDoFicheiro = ".txt" Or _
        ExtencaoDoFicheiro = ".xml" Or _
        ExtencaoDoFicheiro = ".php" Or _
        ExtencaoDoFicheiro = ".css" Then 'Vê se está algum ficheiro a ser arrastado e vê se a extensão.
            txt_file = True
        End If
        e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub sms_txt_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles sms_txt.DragDrop
        If amigo_c_txt.Text = "" Then
            MsgBox("Seleccione um contacto da lista.", MsgBoxStyle.Information, "Informação")
        ElseIf amigo_c_txt.Text = "Amigo" Then
            MsgBox("Seleccione um contacto da lista.", MsgBoxStyle.Information, "Informação")
        Else
            If txt_file = True Then
                Dim a = MsgBox("Foi detectado um ficheiro de texto, pretende copiar o texto para caixa ?", MsgBoxStyle.YesNo, "Ficheiro de texto")
                If a = MsgBoxResult.Yes Then
                    Dim NomeFicheiro As String = System.IO.Path.GetFileName(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString)
                    Try
                        Dim LocalDoFicheiro As String = CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString
                        sms_txt.Text = My.Computer.FileSystem.ReadAllText(LocalDoFicheiro, System.Text.Encoding.Default)
                    Catch ex As Exception
                        MsgBox("Erro ao mostrar o documento, o ficheiro " & NomeFicheiro & " pode estar corrompido!", MsgBoxStyle.Exclamation, "Erro")
                    End Try
                    Exit Sub
                ElseIf a = MsgBoxResult.No Then
                    txt_file = False
                End If
            End If

            If txt_file = False Then
                Try
                    Dim CaminhoImagem As String = System.IO.Path.GetFullPath(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString)

                    'Vars
                    Dim get_nome As String = System.IO.Path.GetFileName(CaminhoImagem).ToString
                    Dim get_extension As String = System.IO.Path.GetExtension(CaminhoImagem).ToLower.ToString
                    Dim get_size As Integer = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length() / 1024000))
                    Dim total_size As String = ""

                    'verifica tamanho (MB)
                    If get_size > 250 Then
                        MsgBox("O ficheiro nao pode ser maior 250MB !", MsgBoxStyle.Exclamation, "Informação")
                        Exit Sub
                    End If

                    'Abre o ecran de load

                    'Transforma o tamanho
                    If get_size = 1 Or get_size = 0 Then
                        get_size = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length() / 1024))
                        total_size = get_size & " KB"
                        If get_size = 1 Or get_size = 0 Then
                            get_size = CStr(Math.Round(My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length()))
                            total_size = get_size & " bytes"
                        End If
                    Else
                        total_size = get_size & " MB"
                    End If

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
                    Dim Command As New SqlCommand("INSERT INTO ficheiros_priv (ID, ficheiro) VALUES (@ID, @ficheiro)", connection)
                    Command.Parameters.AddWithValue("@ID", max_id_var)
                    Dim FicheiroUp As IO.FileStream = IO.File.OpenRead(CaminhoImagem)
                    Dim FileStream As New IO.BinaryReader(FicheiroUp)
                    Dim BufferSize As Long = My.Computer.FileSystem.GetFileInfo(CaminhoImagem).Length()
                    Command.Parameters.Add("@ficheiro", SqlDbType.VarBinary).Value = FileStream.ReadBytes(BufferSize)
                    FicheiroUp.Flush()
                    FicheiroUp.Close()
                    FileStream.Close()
                    connection.Open()
                    Command.ExecuteNonQuery()
                    connection.Close()

                    'Escreve o registo da transferencia
                    Dim visto_v As String = "Nao"
                    Dim premissoes_v As String = "Privado"
                    Dim Command2 As New SqlCommand("INSERT INTO transferencias (ID, de, para, visto, nome, extencao, tamanho, data, premissoes) VALUES (@ID, @de, @para, @visto, @nome, @extencao, @tamanho, @data, @premissoes)", connection)
                    Command2.Parameters.AddWithValue("@ID", max_id_var)
                    Command2.Parameters.AddWithValue("@de", login_txt.Text)
                    Command2.Parameters.AddWithValue("@para", amigo_c_txt.Text)
                    Command2.Parameters.AddWithValue("@visto", visto_v)
                    Command2.Parameters.AddWithValue("@nome", get_nome)
                    Command2.Parameters.AddWithValue("@extencao", get_extension)
                    Command2.Parameters.AddWithValue("@tamanho", total_size)
                    Command2.Parameters.AddWithValue("@data", data)
                    Command2.Parameters.AddWithValue("@premissoes", premissoes_v)
                    connection.Open()
                    Command2.ExecuteNonQuery()
                    connection.Close()

                    MsgBox("O ficheiro foi enviado com sucesso!", MsgBoxStyle.Information, "Informação")
                Catch
                    connection.Close()
                    MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
                End Try
            End If
        End If
    End Sub
#End Region

#Region "Imagem"

    Dim img_file As Boolean
    Private Sub sms_rec_txt_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles sms_rec_txt.DragEnter
        img_file = False
        Dim ExtencaoDoFicheiro As String = System.IO.Path.GetExtension(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString).ToString.ToLower 'Vai buscar a extenção do ficheiro a ser dropado
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) And _
        ExtencaoDoFicheiro = ".jpg" Or _
        ExtencaoDoFicheiro = ".bmp" Or _
        ExtencaoDoFicheiro = ".png" Or _
        ExtencaoDoFicheiro = ".gif" Then 'Vê se está algum ficheiro a ser arrastado e vê se a extensão se confirma.
            e.Effect = DragDropEffects.Copy
            img_file = True
        End If
    End Sub

    Private Sub sms_rec_txt_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles sms_rec_txt.DragDrop
        If img_file = True Then
            Dim a = MsgBox("Deseja utilizar a imagem como fundo ?", MsgBoxStyle.YesNo, "Novo Fundo")
            If a = MsgBoxResult.No Then
                Exit Sub
            End If

            'Liberta a imagem
            Dim tmp = Me.BackgroundImage
            Me.BackgroundImage = Nothing
            tmp.Dispose()

            Dim CaminhoImagem As String = System.IO.Path.GetFullPath(CType(e.Data.GetData(DataFormats.FileDrop), Array).GetValue(0).ToString)
            Dim novo_nome As String = ""

            Dim i As Integer = 1
            Dim id As Boolean = False
            'verifica os nomes ja existentes
            Do While (id = False)
                If System.IO.File.Exists(appPath + "\Skin\main_bg" & i & ".png") = False Then
                    novo_nome = "main_bg" & i & ".png"
                    My.Computer.FileSystem.RenameFile(appPath + "\Skin\main_bg.png", novo_nome)
                    My.Computer.FileSystem.CopyFile(CaminhoImagem, appPath + "\Skin\main_bg.png")
                    Me.BackgroundImage = Image.FromFile(appPath + "\Skin\main_bg.png")
                    id = True
                Else
                    i = i + 1
                End If
            Loop
        Else
            MsgBox("Apenas .jpg/.bmp/.png/.gif sao permitidos para fundo.", MsgBoxStyle.Information, "Informação")
        End If
    End Sub
#End Region

#End Region

#Region "Flash Window 2"

    Public Structure FLASHWINFO
        Public cbSize As Int32
        Public hwnd As IntPtr
        Public dwFlags As Int32
        Public uCount As Int32
        Public dwTimeout As Int32
    End Structure

    Private Declare Function FlashWindowEx Lib "user32.dll" (ByRef pfwi As FLASHWINFO) As Int32

    Public Const FLASHW_STOP = 0        ' Stop flashing. The system restores the window to its original state. 
    Public Const FLASHW_CAPTION = &H1   ' Flash the window caption. 
    Public Const FLASHW_TRAY = &H2      ' Flash the taskbar button.
    Public Const FLASHW_ALL = &H3       ' Flash both the window caption and taskbar button. 
    Public Const FLASHW_TIMER = &H4     ' Flash continuously, until the FLASHW_STOP flag is set. 
    Public Const FLASHW_TIMERNOFG = &HC ' Flash continuously until the window comes to the foreground. 

    Public Sub FlashIcon(ByVal Handle%, ByVal Flags%)
        Dim flash As New FLASHWINFO
        flash.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(flash) '/// size of structure in bytes
        flash.hwnd = Handle '/// Handle to the window to be flashed
        flash.dwFlags = Flags
        flash.dwTimeout = 1000 '/// speed of flashes in MilliSeconds ( can be left out )
        FlashWindowEx(flash) '/// flash the window 
    End Sub

    'FlashIcon(MyBase.Handle, FLASHW_TRAY + FLASHW_TIMERNOFG)

    '*** Scrol***
    'Public Const EM_GETLINECOUNT = &HBA
    'Public Const EM_LINESCROLL = &HB6

    'Func para o auto scroll
    'Public Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    'Quantas linhas ja estao adicionadas
    'Dim intLines As Integer = SendMessage(sms_rec_txt.Handle, EM_GETLINECOUNT, 0, 0)

    'Retira as novas linhas, para saber quanto fazer ao scroll
    'Dim intLinesToAdd As Integer = (SendMessage(sms_rec_txt.Handle, EM_GETLINECOUNT, 0, 0) - intLines)

    'Da scroll das novas linhas
    'SendMessage(sms_rec_txt.Handle, EM_LINESCROLL, 0, intLinesToAdd)

#End Region

#Region "Cria Lista"
    Private Sub mn_clista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_clista.Click
        Try
            Dim path As String = ""
            Dim txt_nome As String = ""
            Me.FolderBrowserDialog2.ShowNewFolderButton = True
            If Me.FolderBrowserDialog2.ShowDialog = DialogResult.Cancel Then
                Exit Sub
            Else
                path = FolderBrowserDialog2.SelectedPath
            End If
            FolderBrowserDialog2.Dispose()

            txt_nome = InputBox("Escreva um nome para o ficheiro", "Guardar Contactos")
            If txt_nome <> "" Then
                path = path & "\" & txt_nome & ".txt"
                Dim TextFile As New IO.StreamWriter(path)
                For i As Integer = 0 To list_contactos.Items.Count - 1
                    TextFile.WriteLine(list_contactos.Items.Item(i).Text)
                Next
                MsgBox("Lista criada com sucesso", MsgBoxStyle.Information, "Informação")
                TextFile.Close()
            End If
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Adiciona Lista"
    Private Sub mn_alista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mn_alista.Click
        OpenFileDialog3.Title = "Seleccione o Ficheiro"
        OpenFileDialog3.FileName = "Ficheiro de Texto"
        OpenFileDialog3.RestoreDirectory = True
        OpenFileDialog3.Filter = "Ficheiro de Texto (*.txt)|*.txt"
        OpenFileDialog3.ShowDialog()
    End Sub

    Private Sub OpenFileDialog3_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog3.FileOk
        Try
            Dim stream_reader As New IO.StreamReader(OpenFileDialog3.FileName.ToString())
            Dim nv_amigo As String

            Dim added As Integer = 0
            Dim pr_naoexiste As Integer = 0
            Dim pr_notificado As Integer = 0
            Dim pr_janalista As Integer = 0

            Dim total As Integer = 0

            While Not stream_reader.EndOfStream
                'Le o contacto
                nv_amigo = stream_reader.ReadLine
                total = total + 1

                Dim existe_v As String = "Nao"
                Dim amigo_v As String = "Nao"
                Dim amigo_block_v As String = "Nao"
                Dim amigo_lv2 As String = "Nao"
                Try
                    If nv_amigo = "" Then
                    ElseIf nv_amigo.Length > 12 Then
                        pr_naoexiste = pr_naoexiste + 1
                    Else
                        'verifica se o amigo existe
                        Dim command As New SqlCommand("SELECT nome, count(nome) as quantos FROM t_login WHERE nome=@nome GROUP BY nome", connection)
                        command.Parameters.AddWithValue("@nome", nv_amigo)
                        connection.Open()
                        Dim reader As SqlDataReader = command.ExecuteReader()
                        Try 'Tem o try, para aparecer a msgbox. Com o While apenas nao da "erro".
                            reader.Read()
                            nv_amigo = reader("nome")
                            If reader("quantos") = 1 Then
                                If nv_amigo = login_txt.Text Then
                                    pr_naoexiste = pr_naoexiste + 1
                                Else
                                    existe_v = "Sim"
                                End If
                            End If
                        Catch
                            pr_naoexiste = pr_naoexiste + 1
                            connection.Close()
                        End Try
                        reader.Close()
                        connection.Close()

                        'verifica se os amigos estao ja estao "ligados".
                        If existe_v = "Sim" Then
                            Dim command7 As New SqlCommand("SELECT count(amigos_1) as quantos FROM amigos WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                            command7.Parameters.AddWithValue("@nome_1", login_txt.Text)
                            command7.Parameters.AddWithValue("@nome_2", nv_amigo)
                            connection.Open()
                            Dim reader3 As SqlDataReader = command7.ExecuteReader()
                            While reader3.Read()
                                If reader3("quantos") = 1 Then
                                    amigo_lv2 = "Sim"
                                Else
                                    amigo_v = "Sim"
                                End If
                            End While
                            reader3.Close()
                            connection.Close()
                        End If

                        'Opçoes, se ja estiverem ligados
                        If existe_v = "Sim" Then
                            If amigo_lv2 = "Sim" Then
                                Dim command6 As New SqlCommand("SELECT pedido FROM amigos WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                                command6.Parameters.AddWithValue("@nome_1", login_txt.Text)
                                command6.Parameters.AddWithValue("@nome_2", nv_amigo)
                                connection.Open()
                                Dim reader6 As SqlDataReader = command6.ExecuteReader()
                                While reader6.Read()
                                    Dim read_pedido_v As String = reader6("pedido")
                                    If read_pedido_v = "Block" Then
                                        amigo_block_v = "Sim"
                                    ElseIf read_pedido_v = "Nao" Then
                                        pr_notificado = pr_notificado + 1
                                    ElseIf read_pedido_v = "Wait" Then
                                        pr_notificado = pr_notificado + 1
                                    ElseIf read_pedido_v = "Sim" Then
                                        pr_janalista = pr_janalista + 1
                                    End If
                                End While
                                reader6.Close()
                                connection.Close()
                            End If
                        End If

                        'actualiza contacto bloqueado.
                        If existe_v = "Sim" Then
                            If amigo_block_v = "Sim" Then
                                Dim command2 As New SqlCommand("UPDATE amigos SET pedido=@pedido, req=@req WHERE amigos_1=@nome_1 AND amigos_2=@nome_2 OR amigos_1=@nome_2 AND amigos_2=@nome_1", connection)
                                Dim pedido_v As String = "Nao"
                                command2.Parameters.AddWithValue("@nome_1", login_txt.Text)
                                command2.Parameters.AddWithValue("@nome_2", nv_amigo)
                                command2.Parameters.AddWithValue("@pedido", pedido_v)
                                command2.Parameters.AddWithValue("@req", login_txt.Text)
                                connection.Open()
                                command2.ExecuteNonQuery()
                                connection.Close()
                                added = added + 1
                            End If
                        End If

                        'adiciona novo amigo
                        If existe_v = "Sim" Then
                            If amigo_v = "Sim" Then
                                Dim command1 As New SqlCommand("INSERT INTO amigos (amigos_1, amigos_2, pedido, req) VALUES (@nome_1, @nome_2, @pedido, @req)", connection)
                                Dim pedido_v As String = "Nao"
                                command1.Parameters.AddWithValue("@nome_1", login_txt.Text)
                                command1.Parameters.AddWithValue("@nome_2", nv_amigo)
                                command1.Parameters.AddWithValue("@pedido", pedido_v)
                                command1.Parameters.AddWithValue("@req", login_txt.Text)
                                connection.Open()
                                command1.ExecuteNonQuery()
                                connection.Close()
                                added = added + 1
                            End If
                        End If
                    End If
                Catch
                    connection.Close()
                End Try
            End While
            stream_reader.Close()
            MsgBox("Adicionados " & added & " de " & total & vbNewLine & vbNewLine & "Nao existem: " & pr_naoexiste & vbNewLine & "Ja na lista: " & pr_janalista & vbNewLine & "Ja notificado: " & pr_notificado, MsgBoxStyle.Information, "Informação")
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

End Class