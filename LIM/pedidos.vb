Imports System.Data.SqlClient

Public Class pedidos

#Region "Form load"

    Private Sub pedidos_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.BackgroundImage = Image.FromFile(appPath + "\Skin\pedido_bg.png")
        Catch ex As Exception
        End Try

        If ListBox1.Visible = False Then
            Try
                Dim command2 As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome_load", connection)
                command2.Parameters.AddWithValue("@nome_load", amigo_lbl.Text)
                connection.Open()
                Dim picture2 As System.Drawing.Image = Nothing
                Dim pictureData2 As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
                Dim stream2 As New IO.MemoryStream(pictureData2)
                connection.Close()
                picture2 = System.Drawing.Image.FromStream(stream2)
                avatar_2_img.Image = picture2
            Catch
                connection.Close()
            End Try
        End If
    End Sub

    Private Sub pedidos_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        main.nv_pedido = False
    End Sub
#End Region

#Region "Aceitar"
    Private Sub aceitar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles aceitar_btn.Click
        Try
            If amigo_lbl.Text = "Amigo" Then
                MsgBox("Selecione um contacto da lista", MsgBoxStyle.Information, "Informação")
            Else
                Dim pedido_v As String = "Sim"
                Dim selected_v As String = amigo_lbl.Text
                Dim command As New SqlCommand("update amigos set pedido=@pedido Where amigos_1=@selected AND amigos_2=@nome OR amigos_1=@nome AND amigos_2=@selected", connection)
                command.Parameters.AddWithValue("@nome", main.login_txt.Text)
                command.Parameters.AddWithValue("@selected", selected_v)
                command.Parameters.AddWithValue("@pedido", pedido_v)
                connection.Open()
                command.ExecuteNonQuery()
                connection.Close()
                If ListBox1.Visible = False Then
                    Me.Close()
                ElseIf ListBox1.Visible = True Then
                    ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                    avatar_2_img.Image = Nothing
                    amigo_lbl.Text = "Amigo"
                    If ListBox1.Items.Count = 0 Then
                        Me.Close()
                    End If
                End If
            End If
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Bloquear"

    Private Sub block_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles block_btn.Click
        Try
            If amigo_lbl.Text = "Amigo" Then
                MsgBox("Selecione um contacto da lista", MsgBoxStyle.Information, "Informação")
            Else
                Dim a As String
                a = MsgBox("Tem mesmo a certeza que quer bloquear" & " '" & amigo_lbl.Text & "' ?", MsgBoxStyle.YesNo, "Info")
                If a = vbNo Then
                    Exit Sub
                End If
                Dim pedido_v As String = "Block"
                Dim selected_v As String = amigo_lbl.Text
                Dim command As New SqlCommand("update amigos set pedido=@pedido Where amigos_1=@selected AND amigos_2=@nome OR amigos_1=@nome AND amigos_2=@selected", connection)
                command.Parameters.AddWithValue("@nome", main.login_txt.Text)
                command.Parameters.AddWithValue("@selected", selected_v)
                command.Parameters.AddWithValue("@pedido", pedido_v)
                connection.Open()
                command.ExecuteNonQuery()
                connection.Close()
                If ListBox1.Visible = False Then
                    Me.Close()
                ElseIf ListBox1.Visible = True Then
                    ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
                    avatar_2_img.Image = Nothing
                    amigo_lbl.Text = "Amigo"
                    If ListBox1.Items.Count = 0 Then
                        Me.Close()
                    End If
                End If
            End If
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub
#End Region

#Region "Mais Tarde"

    Private Sub mais_tarde_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mais_tarde_btn.Click
        Try
            Dim pedido_v As String = "Wait"
            Dim selected_v As String = amigo_lbl.Text
            Dim command As New SqlCommand("update amigos set pedido=@pedido Where amigos_1=@selected AND amigos_2=@nome OR amigos_1=@nome AND amigos_2=@selected", connection)
            command.Parameters.AddWithValue("@nome", main.login_txt.Text)
            command.Parameters.AddWithValue("@selected", selected_v)
            command.Parameters.AddWithValue("@pedido", pedido_v)
            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()
            Me.Close()
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

#End Region

#Region "Box click"

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        amigo_lbl.Text = ListBox1.SelectedItem
        Try
            Dim command2 As New SqlCommand("SELECT imagem FROM avatar WHERE nome=@nome_load", connection)
            command2.Parameters.AddWithValue("@nome_load", amigo_lbl.Text)
            connection.Open()
            Dim picture2 As System.Drawing.Image = Nothing
            Dim pictureData2 As Byte() = DirectCast(command2.ExecuteScalar(), Byte())
            Dim stream2 As New IO.MemoryStream(pictureData2)
            connection.Close()
            picture2 = System.Drawing.Image.FromStream(stream2)
            avatar_2_img.Image = picture2
        Catch
            connection.Close()
        End Try
    End Sub
#End Region

End Class