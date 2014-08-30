Imports System.Data.SqlClient

Public Class voip

    Private recebe_var As String
    Private login_var As String
    Private id_chamada As Integer
    Private file_id As Integer

    Private chamada_timeout As Integer = 0
    Private no_timeout As Boolean = False

    Private check_call As Boolean = False
    Private play_done As Boolean = True

    Private block_while_save As Boolean = False
    Private avoid_small_clicks As Integer = 0
    Private save_done As Boolean = True
    Private key_push As Boolean = False

    Private mute_id As Boolean = False
    Private establecida As Boolean = False

    Dim path As String

    Public Declare Function GetAsyncKeyState Lib "user32.dll" (ByVal vKey As Int32) As UShort
    Public Declare Function RecordSound Lib "winmm.dll" Alias "mciSendStringA" (ByVal Command As String, ByVal ReturnString As String, ByVal ReturnLength As Integer, ByVal hwndCallback As Integer) As Integer


#Region "Form Load"

    Private Sub voip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bar_quality.Value = 10
        pb_mute.Image = My.Resources.audio
        login_var = main.login_txt.Text

        Try
            Me.BackgroundImage = Image.FromFile(appPath + "\Skin\voip_bg.png")
        Catch ex As Exception
        End Try

        If main.nv_chamada = False Then 'Se for aberto pa iniciar uma nova chamada
            'Prepara form
            recebe_var = main.amigo_c_txt.Text
            Me.Text = "A Chamar..."

            lbl_me_info.Text = login_var
            pb_img1.Image = My.Resources.img_yescall
            lbl_other_info.Text = recebe_var
            pb_img2.Image = My.Resources.img_nocall

            main.nv_chamada = True 'Indica que esta com uma chamada, nao faz verificaçao no main

            btn_stop.Enabled = True
            btn_accept.Enabled = False
            btn_cancel.Enabled = False

            Call start_call()
        ElseIf main.nv_chamada = True Then 'Se for aberto pelo main
            'Prepara form
            no_timeout = True 'A receber, nao tem timeout
            btn_stop.Enabled = False

            'Detecta qual e a chamada
            Dim visto_v As String = "Espera"
            Dim command5 As New SqlCommand("SELECT id, de FROM voip WHERE para=@para AND visto=@visto", connection)
            command5.Parameters.AddWithValue("@para", login_var)
            command5.Parameters.AddWithValue("@visto", visto_v)
            connection.Open()
            Dim reader As SqlDataReader = command5.ExecuteReader
            reader.Read()
            id_chamada = reader("id")
            recebe_var = reader("de")

            reader.Close()
            connection.Close()

            Me.Text = recebe_var & " esta a chamar..."
            lbl_me_info.Text = login_var
            pb_img1.Image = My.Resources.img_nocall
            lbl_other_info.Text = recebe_var
            pb_img2.Image = My.Resources.img_yescall

            My.Computer.Audio.Play(My.Resources.calling, AudioPlayMode.BackgroundLoop)

            'Inicia a actualizaçao do estado
            Timer1.Enabled = True
            Timer1.Interval = 100
        End If
    End Sub

    Private Sub voip_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Computer.Audio.Stop()

        Dim visto_v As String = "Terminada"
        Dim command3 As New SqlCommand("UPDATE voip SET visto=@visto WHERE ID=@ID AND visto='Espera' OR ID=@ID AND visto='Em chamada'", connection)
        command3.Parameters.AddWithValue("@ID", id_chamada)
        command3.Parameters.AddWithValue("@visto", visto_v)
        connection.Open()
        command3.ExecuteNonQuery()
        connection.Close()

        Timer1.Stop()
        Timer2.Stop()
        Timer3.Stop()

        'Apaga todas as possiveis chamadas
        Dim visto_v1 As String = "Em chamada"
        Dim visto_v2 As String = "Espera"
        Dim command7 As New SqlCommand("UPDATE voip SET visto=@visto WHERE para=@para AND visto=@visto1 OR para=@para AND visto=@visto2 OR de=@de AND visto=@visto1 OR de=@de AND visto=@visto2", connection_voip)
        command7.Parameters.AddWithValue("@de", login_var)
        command7.Parameters.AddWithValue("@para", login_var)
        command7.Parameters.AddWithValue("@visto", visto_v)
        command7.Parameters.AddWithValue("@visto1", visto_v1)
        command7.Parameters.AddWithValue("@visto2", visto_v2)
        connection_voip.Open()
        command7.ExecuteNonQuery()
        connection_voip.Close()

        'Apaga os restos
        Dim command4 As New SqlCommand("Delete FROM voip_transfer WHERE ID_voip=@ID_voip AND de=@de", connection_voip)
        command4.Parameters.AddWithValue("@ID_voip", id_chamada)
        command4.Parameters.AddWithValue("@de", login_var)
        connection_voip.Open()
        command4.ExecuteNonQuery()
        connection_voip.Close()
        connection_voip_send.Close()
        connection_voip_recv.Close()

        main.nv_chamada = False
        Me.Dispose()
    End Sub
#End Region

#Region "Iniciar nova chamada"
    Sub start_call()
        If recebe_var = "" Then
            MsgBox("Seleccione um contacto da lista", MsgBoxStyle.Information, "Informação")
            main.nv_chamada = False
            Me.Dispose()
        ElseIf recebe_var = "Amigo" Then
            MsgBox("Seleccione um contacto da lista", MsgBoxStyle.Information, "Informação")
            main.nv_chamada = False
            Me.Dispose()
        Else
            'verifica se esta online o amigo
            Dim online_true As Boolean = False
            Dim command As New SqlCommand("SELECT online FROM t_login WHERE nome=@nome", connection)
            command.Parameters.AddWithValue("@nome", recebe_var)
            connection.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()
            reader.Read()
            If reader("online") = "Online" Then
                reader.Close()
                connection.Close()
                online_true = True
            Else
                reader.Close()
                connection.Close()
                MsgBox(recebe_var & " esta offline", MsgBoxStyle.Information, "Informação")
                main.nv_chamada = False
                Me.Dispose()
                Exit Sub
            End If

            'verifica se está com outra chamada
            Dim inicia_chamada As Boolean = False
            If online_true = True Then
                Dim visto_v1 As String = "Em chamada"
                Dim visto_v2 As String = "Espera"
                Dim command2 As New SqlCommand("SELECT id FROM voip WHERE para=@para AND visto=@visto1 OR para=@para AND visto=@visto2 OR de=@de AND visto=@visto1 OR de=@de AND visto=@visto2", connection)
                command2.Parameters.AddWithValue("@para", recebe_var)
                command2.Parameters.AddWithValue("@de", recebe_var)
                command2.Parameters.AddWithValue("@visto1", visto_v1)
                command2.Parameters.AddWithValue("@visto2", visto_v2)
                connection.Open()
                Dim reader2 As SqlDataReader = command2.ExecuteReader()
                reader2.Read()
                Try
                    Dim teste = reader2("id")
                    reader2.Close()
                    connection.Close()
                    MsgBox(recebe_var & " esta em outra chamada.", MsgBoxStyle.Information, "Informação")
                    main.nv_chamada = False
                    Me.Dispose()
                    Exit Sub
                Catch ex As Exception
                    reader2.Close()
                    connection.Close()
                    inicia_chamada = True
                End Try
            End If

            'efectua o registo
            If inicia_chamada = True Then
                'Saca ID
                Dim max_id_var As Long
                Dim command7 As New SqlCommand("SELECT MAX(ID) as max_id FROM voip", connection)
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
                id_chamada = max_id_var 'Para o timer, so verificar esta chamada.

                'Data do server sql
                Dim command5 As New SqlCommand("SELECT GETDATE() as Date", connection)
                Dim data As String = ""
                connection.Open()
                Dim reader4 As SqlDataReader = command5.ExecuteReader()
                reader4.Read()
                data = reader4("Date")
                reader4.Close()
                connection.Close()

                'Escreve o registo da chamada
                Dim visto_v As String = "Espera"
                Dim Command1 As New SqlCommand("INSERT INTO voip (ID, de, para, visto, data) VALUES (@ID, @de, @para, @visto, @data)", connection)
                Command1.Parameters.AddWithValue("@ID", max_id_var)
                Command1.Parameters.AddWithValue("@de", login_var)
                Command1.Parameters.AddWithValue("@para", recebe_var)
                Command1.Parameters.AddWithValue("@visto", visto_v)
                Command1.Parameters.AddWithValue("@data", data)
                connection.Open()
                Command1.ExecuteNonQuery()
                connection.Close()

                'Inicia a actualizaçao do estado
                Timer1.Enabled = True
                Timer1.Interval = 100
            End If
        End If
    End Sub
#End Region

#Region "Aceita a chamada"
    Private Sub btn_accept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_accept.Click
        If txt_hotkey.Text <> "" Then
            Dim visto_v As String = "Em chamada"
            Dim command3 As New SqlCommand("UPDATE voip SET visto=@visto WHERE ID=@ID AND visto='Espera'", connection)
            command3.Parameters.AddWithValue("@ID", id_chamada)
            command3.Parameters.AddWithValue("@visto", visto_v)
            connection.Open()
            command3.ExecuteNonQuery()
            connection.Close()
        Else
            txt_hotkey.BackColor = Color.LightPink
        End If
    End Sub
#End Region

#Region "Cancelar a chamada"
    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        My.Computer.Audio.Stop()
        Dim visto_v As String = "Cancelada"
        Dim command3 As New SqlCommand("UPDATE voip SET visto=@visto WHERE ID=@ID AND visto='Espera'", connection)
        command3.Parameters.AddWithValue("@ID", id_chamada)
        command3.Parameters.AddWithValue("@visto", visto_v)
        connection.Open()
        command3.ExecuteNonQuery()
        connection.Close()
        'Para nao aparecer a msgbox, q esta acima
        Timer1.Enabled = False
        main.nv_chamada = False
        Me.Dispose()
    End Sub
#End Region

#Region "Terminar chamada"
    Private Sub btn_stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_stop.Click
        Dim visto_v As String = "Terminada"
        Dim command3 As New SqlCommand("UPDATE voip SET visto=@visto WHERE ID=@ID AND visto='Espera' OR ID=@ID AND visto='Em chamada'", connection)
        command3.Parameters.AddWithValue("@ID", id_chamada)
        command3.Parameters.AddWithValue("@visto", visto_v)
        connection.Open()
        command3.ExecuteNonQuery()
        connection.Close()
    End Sub
#End Region

#Region "Sound Record"

#Region "Sound Quality"
    Dim kHz, Bits, Channels As Integer
    Dim BlockAlign As Short
    Dim BytesPerSec As Integer

    Private Sub GetSoundFormat()
        If _SoundFormat = SoundFormats.Mono_6kbps_8_Bit Then
            kHz = 6000 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Mono_8kbps_8_Bit Then
            kHz = 8000 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Mono_11kbps_8_Bit Then
            kHz = 11025 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Mono_16kbps_8_Bit Then
            kHz = 16000 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Mono_22kbps_8_Bit Then
            kHz = 22050 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Mono_32kbps_8_Bit Then
            kHz = 32000 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Mono_44kbps_8_Bit Then
            kHz = 44100 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Mono_48kbps_8_Bit Then
            kHz = 48000 : Bits = 8 : Channels = 1
        ElseIf _SoundFormat = SoundFormats.Stereo_24kbps_16_Bit Then
            kHz = 6000 : Bits = 16 : Channels = 2
        ElseIf _SoundFormat = SoundFormats.Stereo_32kbps_16_Bit Then
            kHz = 8000 : Bits = 16 : Channels = 2
        ElseIf _SoundFormat = SoundFormats.Stereo_44kbps_16_Bit Then
            kHz = 11025 : Bits = 16 : Channels = 2
        ElseIf _SoundFormat = SoundFormats.Stereo_64kbps_16_Bit Then
            kHz = 16000 : Bits = 16 : Channels = 2
        ElseIf _SoundFormat = SoundFormats.Stereo_88kbps_16_Bit Then
            kHz = 22050 : Bits = 16 : Channels = 2
        ElseIf _SoundFormat = SoundFormats.Stereo_128kbps_16_Bit Then
            kHz = 32000 : Bits = 16 : Channels = 2
        ElseIf _SoundFormat = SoundFormats.Stereo_176kbps_16_Bit Then
            kHz = 44100 : Bits = 16 : Channels = 2
        ElseIf _SoundFormat = SoundFormats.Stereo_192kbps_16_Bit Then
            kHz = 48000 : Bits = 16 : Channels = 2
        End If
        BlockAlign = Channels * Bits / 8
        BytesPerSec = kHz * BlockAlign
    End Sub

    Public Enum SoundFormats
        Mono_6kbps_8_Bit
        Mono_8kbps_8_Bit
        Mono_11kbps_8_Bit
        Mono_16kbps_8_Bit
        Mono_22kbps_8_Bit
        Mono_32kbps_8_Bit
        Mono_44kbps_8_Bit
        Mono_48kbps_8_Bit
        Stereo_24kbps_16_Bit
        Stereo_32kbps_16_Bit
        Stereo_44kbps_16_Bit
        Stereo_64kbps_16_Bit
        Stereo_88kbps_16_Bit
        Stereo_128kbps_16_Bit
        Stereo_176kbps_16_Bit
        Stereo_192kbps_16_Bit
    End Enum

    Private _SoundFormat As SoundFormats
    Public Property SoundFormat() As SoundFormats
        Get
            SoundFormat = _SoundFormat
        End Get
        Set(ByVal Value As SoundFormats)
            _SoundFormat = Value
        End Set
    End Property


    Private Sub bar_quality_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles bar_quality.ValueChanged
        If bar_quality.Value = 0 Then
            SoundFormat = SoundFormats.Mono_6kbps_8_Bit
            lbl_quality.Text = "Mono - 48kbps - 6kHz"
        ElseIf bar_quality.Value = 1 Then
            SoundFormat = SoundFormats.Mono_8kbps_8_Bit
            lbl_quality.Text = "Mono - 64kbps - 8kHz"
        ElseIf bar_quality.Value = 2 Then
            SoundFormat = SoundFormats.Mono_11kbps_8_Bit
            lbl_quality.Text = "Mono - 88kbps - 11kHz"
        ElseIf bar_quality.Value = 3 Then
            SoundFormat = SoundFormats.Mono_16kbps_8_Bit
            lbl_quality.Text = "Mono - 128kbps - 16kHz"
        ElseIf bar_quality.Value = 4 Then
            SoundFormat = SoundFormats.Mono_22kbps_8_Bit
            lbl_quality.Text = "Mono - 176kbps - 22kHz"
        ElseIf bar_quality.Value = 5 Then
            SoundFormat = SoundFormats.Mono_32kbps_8_Bit
            lbl_quality.Text = "Mono - 256kbps - 32kHz"
        ElseIf bar_quality.Value = 6 Then
            SoundFormat = SoundFormats.Mono_44kbps_8_Bit
            lbl_quality.Text = "Mono - 352kbps - 44kHz"
        ElseIf bar_quality.Value = 7 Then
            SoundFormat = SoundFormats.Mono_48kbps_8_Bit
            lbl_quality.Text = "Mono - 384kbps - 48kHz"
        ElseIf bar_quality.Value = 8 Then
            SoundFormat = SoundFormats.Stereo_24kbps_16_Bit
            lbl_quality.Text = "Stereo - 176kbps - 6kHz"
        ElseIf bar_quality.Value = 9 Then
            SoundFormat = SoundFormats.Stereo_32kbps_16_Bit
            lbl_quality.Text = "Stereo - 256kbps - 8kHz"
        ElseIf bar_quality.Value = 10 Then
            SoundFormat = SoundFormats.Stereo_44kbps_16_Bit
            lbl_quality.Text = "Stereo - 352kbps - 11kHz"
        ElseIf bar_quality.Value = 11 Then
            SoundFormat = SoundFormats.Stereo_64kbps_16_Bit
            lbl_quality.Text = "Stereo - 512kbps - 16kHz"
        ElseIf bar_quality.Value = 12 Then
            SoundFormat = SoundFormats.Stereo_88kbps_16_Bit
            lbl_quality.Text = "Stereo - 704kbps - 22kHz"
        ElseIf bar_quality.Value = 13 Then
            SoundFormat = SoundFormats.Stereo_128kbps_16_Bit
            lbl_quality.Text = "Stereo - 1024kbps - 32kHz"
        ElseIf bar_quality.Value = 14 Then
            SoundFormat = SoundFormats.Stereo_176kbps_16_Bit
            lbl_quality.Text = "Stereo - 1408kbps - 44kHz"
        ElseIf bar_quality.Value = 15 Then
            SoundFormat = SoundFormats.Stereo_192kbps_16_Bit
            lbl_quality.Text = "Stereo - 1536kbps - 48kHz"
        End If
    End Sub
#End Region

#Region "State / Name"
    Public Enum MyState
        Idle
        Recording
        Paused
    End Enum

    Private xState As MyState
    Public ReadOnly Property State() As MyState
        Get
            State = xState
        End Get
    End Property

    Private FName As String
    Public Property FileName() As String
        Get
            FileName = FName
        End Get
        Set(ByVal Value As String)
            FName = Value
        End Set
    End Property
#End Region

#Region "Funcs"
    Public Function StartRecord() As Boolean
        Try
            Call GetSoundFormat()
            Dim i As Integer
            i = RecordSound("open new type waveaudio alias capture", vbNullString, 0, 0)
            i = RecordSound("set capture samplespersec " & kHz & " channels " & Channels & " bitspersample " & Bits & " alignment " & BlockAlign & " bytespersec " & BytesPerSec, vbNullString, 0, 0)
            i = RecordSound("record capture", vbNullString, 0, 0)
            xState = MyState.Recording
            StartRecord = True
            Exit Function
        Catch ex As Exception
            StartRecord = False
        End Try
    End Function

    Public Function StopRecord() As Boolean
        Dim i As Integer
        Try
            i = RecordSound("save capture " & FName, vbNullString, 0, 0)
            i = RecordSound("close capture", vbNullString, 0, 0)
            xState = MyState.Idle
            StopRecord = True
            Exit Function
        Catch ex As Exception
            StopRecord = False
            i = RecordSound("close capture", vbNullString, 0, 0)
        End Try
    End Function

    'Closing Recording But Not Saved
    Public Sub CloseRecord()
        Dim i As Integer
        i = RecordSound("close capture", vbNullString, 0, 0)
        xState = MyState.Idle
    End Sub

#End Region

#End Region

#Region "Call Checker"
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If check_call = False Then
            Dim command As New SqlCommand("SELECT visto FROM voip WHERE id=@id", connection_voip)
            command.Parameters.AddWithValue("@id", id_chamada)
            connection_voip.Open()
            Dim reader_voip As SqlDataReader = command.ExecuteReader()
            reader_voip.Read()
            Dim chamada_opt = reader_voip("visto")
            reader_voip.Close()
            connection_voip.Close()
            If chamada_opt = "Em chamada" Then
                My.Computer.Audio.Stop()

                btn_stop.Enabled = True
                btn_accept.Enabled = False
                btn_cancel.Enabled = False

                'Establece a chamada
                Dim FileToDelete As String = appPath + "\Sound.wav"
                Dim FileToDelete2 As String = appPath + "\Voice.wav"

                If System.IO.File.Exists(FileToDelete) = True Then
                    System.IO.File.Delete(FileToDelete)
                End If
                If System.IO.File.Exists(FileToDelete2) = True Then
                    System.IO.File.Delete(FileToDelete2)
                End If

                FileName = appPath + "\Voice.wav"
                path = appPath + "\Sound.wav"
                Me.Text = "Chamada establecida com " & recebe_var

                pb_img1.Image = My.Resources.img_away
                pb_img2.Image = My.Resources.img_away
                txt_hotkey.Enabled = False

                check_call = True
                establecida = True
                mute_id = False
                pb_mute.Image = My.Resources.audio

                Timer2.Enabled = True
                Timer3.Enabled = True
            ElseIf chamada_opt = "Cancelada" Then
                Timer1.Enabled = False
                main.nv_chamada = False
                Me.Dispose()
                MsgBox(recebe_var & " cancelou chamada.", MsgBoxStyle.Information, "Informação")
            ElseIf chamada_timeout = 250 Then
                Timer1.Enabled = False
                Dim visto_v As String = "Terminada"
                Dim command1 As New SqlCommand("UPDATE voip SET visto=@visto WHERE ID=@ID AND visto='Espera'", connection_voip)
                command1.Parameters.AddWithValue("@ID", id_chamada)
                command1.Parameters.AddWithValue("@visto", visto_v)
                connection_voip.Open()
                command1.ExecuteNonQuery()
                connection_voip.Close()
                main.nv_chamada = False
                Me.Dispose()
                MsgBox(recebe_var & " nao atendeu a chamada.", MsgBoxStyle.Information, "Informação")
            ElseIf chamada_opt = "Terminada" Then
                My.Computer.Audio.Stop()
                Timer1.Enabled = False
                main.nv_chamada = False
                Me.Dispose()
            Else
                If no_timeout = False Then
                    chamada_timeout = chamada_timeout + 1
                End If
            End If
        ElseIf check_call = True Then
            Dim chamada_opt_2 As String = ""
            Try
                Dim command2 As New SqlCommand("SELECT visto FROM voip WHERE id=@id", connection_voip)
                command2.Parameters.AddWithValue("@id", id_chamada)
                connection_voip.Open()
                Dim reader_voip_2 As SqlDataReader = command2.ExecuteReader()
                reader_voip_2.Read()
                chamada_opt_2 = reader_voip_2("visto")
                reader_voip_2.Close()
                connection_voip.Close()
            Catch ex As Exception
                connection_voip.Close()
            End Try

            If chamada_opt_2 = "Terminada" Then
                Timer1.Stop()
                Timer2.Stop()
                Timer3.Stop()

                'Apaga todas as possiveis chamadas
                Dim visto_v As String = "Terminada"
                Dim visto_v1 As String = "Em chamada"
                Dim visto_v2 As String = "Espera"
                Dim command3 As New SqlCommand("UPDATE voip SET visto=@visto WHERE para=@para AND visto=@visto1 OR para=@para AND visto=@visto2 OR de=@de AND visto=@visto1 OR de=@de AND visto=@visto2", connection_voip)
                command3.Parameters.AddWithValue("@de", login_var)
                command3.Parameters.AddWithValue("@para", login_var)
                command3.Parameters.AddWithValue("@visto", visto_v)
                command3.Parameters.AddWithValue("@visto1", visto_v1)
                command3.Parameters.AddWithValue("@visto2", visto_v2)
                connection_voip.Open()
                command3.ExecuteNonQuery()
                connection_voip.Close()

                'Apaga os restos
                Dim command4 As New SqlCommand("Delete FROM voip_transfer WHERE ID_voip=@ID_voip AND de=@de", connection_voip)
                command4.Parameters.AddWithValue("@ID_voip", id_chamada)
                command4.Parameters.AddWithValue("@de", login_var)
                connection_voip.Open()
                command4.ExecuteNonQuery()
                connection_voip.Close()
                connection_voip_send.Close()
                connection_voip_recv.Close()

                main.nv_chamada = False
                Me.Dispose()
            End If
        End If
    End Sub
#End Region

#Region "Save"
    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Try
            If play_done = True Then 'fim de ouvir
                Dim get_hotkey As Long = txt_hotkey.Text
                If GetAsyncKeyState(get_hotkey) Then
                    key_push = True
                Else
                    key_push = False
                End If
                If key_push = True Then
                    avoid_small_clicks = avoid_small_clicks + 1
                    If save_done = True Then 'Se ja tiver acabado de gravar
                        save_done = False
                        pb_img1.Image = My.Resources.img_speak
                        BackgroundWorker3.RunWorkerAsync()
                    End If
                ElseIf key_push = False Then
                    If save_done = False Then
                        If block_while_save = False Then
                            If avoid_small_clicks > 6 Then
                                block_while_save = True
                                BackgroundWorker1.RunWorkerAsync()
                            Else
                                CloseRecord()
                                avoid_small_clicks = 0
                                pb_img1.Image = My.Resources.img_away
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        StartRecord()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            'para de gravar
            StopRecord()

            'Saca ID
            Dim max_id_var As Long
            Dim command As New SqlCommand("SELECT MAX(ID) as max_id FROM voip_transfer", connection_voip_send)
            connection_voip_send.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()
            reader.Read()
            Try
                max_id_var = reader("max_id")
            Catch
                max_id_var = 0
            End Try
            reader.Close()
            connection_voip_send.Close()

            max_id_var = max_id_var + 1

            'Escreve ficheiro
            Dim command1 As New SqlCommand("INSERT INTO voip_transfer (ID, ID_voip, de, para, ficheiro) VALUES (@ID, @ID_voip, @de, @para, @ficheiro)", connection_voip_send)
            command1.Parameters.AddWithValue("@ID", max_id_var)
            command1.Parameters.AddWithValue("@ID_voip", id_chamada)
            command1.Parameters.AddWithValue("@de", login_var)
            command1.Parameters.AddWithValue("@para", recebe_var)
            Dim FicheiroUp As IO.FileStream = IO.File.OpenRead(FileName)
            Dim FileStream As New IO.BinaryReader(FicheiroUp)
            Dim BufferSize As Long = My.Computer.FileSystem.GetFileInfo(FileName).Length()
            command1.Parameters.Add("@ficheiro", SqlDbType.VarBinary).Value = FileStream.ReadBytes(BufferSize)
            FicheiroUp.Close()
            FileStream.Close()
            connection_voip_send.Open()
            command1.ExecuteNonQuery()
            connection_voip_send.Close()

        Catch ex As Exception
            connection_voip_send.Close()
        End Try

        pb_img1.Image = My.Resources.img_away
        avoid_small_clicks = 0
        save_done = True
        block_while_save = False
    End Sub
#End Region

#Region "Play"
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Try
            If save_done = True Then
                If play_done = True Then
                    Dim command As New SqlCommand("SELECT id FROM voip_transfer WHERE ID_voip=@ID_voip AND de=@de AND para=@para ORDER BY id", connection_voip_recv)
                    command.Parameters.AddWithValue("@ID_voip", id_chamada)
                    command.Parameters.AddWithValue("@de", recebe_var)
                    command.Parameters.AddWithValue("@para", login_var)
                    connection_voip_recv.Open()
                    Dim reader As SqlDataReader = command.ExecuteReader()
                    reader.Read()
                    Try
                        file_id = reader("ID")
                    Catch ex As Exception
                        file_id = Nothing
                    End Try

                    reader.Close()
                    connection_voip_recv.Close()

                    If file_id <> Nothing Then
                        play_done = False
                        pb_img2.Image = My.Resources.img_speak
                        BackgroundWorker2.RunWorkerAsync()
                    End If
                End If
            End If
        Catch ex As Exception
            connection_voip_recv.Close()
        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            'Saca o ficheiro
            Dim command2 As New SqlCommand("SELECT ficheiro FROM voip_transfer WHERE ID=@ID", connection_voip_recv)
            command2.Parameters.AddWithValue("@ID", file_id)
            connection_voip_recv.Open()
            Dim FicheiroNovo As New IO.FileStream(path, IO.FileMode.Create, IO.FileAccess.Write)
            Dim DataStream As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
            Dim FileStream As New IO.MemoryStream(DataStream)
            FileStream.WriteTo(FicheiroNovo)
            FicheiroNovo.Flush()
            FicheiroNovo.Close()
            FileStream.Flush()
            FileStream.Close()
            connection_voip_recv.Close()

            'Apaga o ficheiro
            Dim command4 As New SqlCommand("Delete FROM voip_transfer WHERE ID=@ID", connection_voip_recv)
            command4.Parameters.AddWithValue("@ID", file_id)
            connection_voip_recv.Open()
            command4.ExecuteNonQuery()
            connection_voip_recv.Close()

            file_id = Nothing

            'Reproduz
            My.Computer.Audio.Play(appPath + "\Sound.wav", AudioPlayMode.WaitToComplete)

        Catch ex As Exception
            connection_voip_recv.Close()
        End Try

        pb_img2.Image = My.Resources.img_away
        play_done = True
    End Sub
#End Region

#Region "Nova tecla"

    Private Sub txt_hotkey_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_hotkey.KeyPress
        lbl_keytext.Text = e.KeyChar
    End Sub

    Private Sub txt_hotkey_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_hotkey.KeyUp
        txt_hotkey.BackColor = Color.White
        txt_hotkey.Text = e.KeyCode
        txt_hotkey.Enabled = False
        lbl_info_key.Text = "Tecla para falar :"

        If establecida = True Then
            Timer3.Enabled = True
        End If

    End Sub

    Private Sub btn_redefenir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_redefenir.Click
        If establecida = True Then
            Timer3.Enabled = False
        End If

        txt_hotkey.Text = ""
        lbl_keytext.Text = ""
        txt_hotkey.Enabled = True
        lbl_info_key.Text = "Defina a tecla :"
        txt_hotkey.Focus()
    End Sub
#End Region

#Region "Mute"
    Private Sub pb_mute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_mute.Click
        If establecida = False Then
            If mute_id = False Then
                mute_id = True
                pb_mute.Image = My.Resources.audio_mute
                My.Computer.Audio.Stop()
            ElseIf mute_id = True Then
                mute_id = False
                pb_mute.Image = My.Resources.audio
                My.Computer.Audio.Play(My.Resources.calling, AudioPlayMode.BackgroundLoop)
            End If
        ElseIf establecida = True Then
            If mute_id = False Then
                mute_id = True
                pb_mute.Image = My.Resources.audio_mute
                Timer2.Enabled = False
            ElseIf mute_id = True Then
                mute_id = False
                pb_mute.Image = My.Resources.audio
                Timer2.Enabled = True
            End If
        End If
    End Sub
#End Region

End Class