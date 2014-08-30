Imports System.Data.SqlClient

Public Class connect

    Private reader_id As Boolean = False

#Region "Form Load"
    Private Sub connect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        pass_sql.PasswordChar = "●"
        If System.IO.File.Exists(appPath + "\Config.xml") = False Then
            'Se nao existir, le o conteudo e actualiza
            read_content()
            cnn()
        Else
            read_content()
        End If
    End Sub

    Private Sub connect_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Login.Focus()
    End Sub
#End Region

#Region "Combo"

    Private Sub tc_combo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ty_combo.KeyPress
        e.Handled = True
    End Sub

    Private Sub ty_combo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ty_combo.SelectedIndexChanged
        If ty_combo.Text = "Local" Then
            path_bd.Enabled = True
            pc_nome.Enabled = False
            pc_nome.Text = ""
            user_sql.Enabled = False
            user_sql.Text = ""
            pass_sql.Enabled = False
            pass_sql.Text = ""
        ElseIf ty_combo.Text = "Lan" Then
            path_bd.Enabled = True
            pc_nome.Enabled = True
            user_sql.Enabled = True
            pass_sql.Enabled = True
        End If
        read_content()
    End Sub

#End Region

#Region "Guardar / Testar"

    Private Sub guardar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles guardar_btn.Click
        Try
            If ty_combo.Text = "Local" Then
                If path_bd.Text = "" Then
                    MsgBox("Todos os campos sao obrigatórios", MsgBoxStyle.Exclamation, "Informação")
                    Exit Sub
                End If
            ElseIf ty_combo.Text = "Lan" Then
                If path_bd.Text = "" Or pc_nome.Text = "" Or user_sql.Text = "" Or pass_sql.Text = "" Then
                    MsgBox("Todos os campos sao obrigatórios", MsgBoxStyle.Exclamation, "Informação")
                    Exit Sub
                End If
            End If
            save_content()
            MsgBox("A conexão alterada com sucesso!", MsgBoxStyle.Information, "Informação")
            Me.Close()
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Private Sub testar_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles testar_btn.Click
        Try
            If ty_combo.Text = "Local" Then
                If path_bd.Text = "" Then
                    MsgBox("Todos os campos sao obrigatórios", MsgBoxStyle.Exclamation, "Informação")
                    Exit Sub
                End If
                'Data Source=(LocalDB)\v11.0;AttachDbFilename="C:\Users\Filipe\Desktop\Projecto de Aptidão Tecnológica\LIM\bin\Debug\LIM_db.mdf";Integrated Security=True;Connect Timeout=30
                Dim connection2 As New SqlConnection("Data Source=(LocalDB)\v11.0;AttachDbFilename=" & path_bd.Text & ";Integrated Security=True;Connect Timeout=30;")
                connection2.Open()
                connection2.Close()
            ElseIf ty_combo.Text = "Lan" Then
                If pc_nome.Text = "" Or path_bd.Text = "" Or user_sql.Text = "" Or pass_sql.Text = "" Then
                    MsgBox("Todos os campos sao obrigatórios", MsgBoxStyle.Exclamation, "Informação")
                    Exit Sub
                End If
                Dim connection2 As New SqlConnection("Data Source=" & pc_nome.Text & "\SQLEXPRESS;Initial Catalog=" & path_bd.Text & ";User Id=" & user_sql.Text & ";Password=" & pass_sql.Text)
                connection2.Open()
                connection2.Close()
            End If
            MsgBox("Conexão efectuada com sucesso!", MsgBoxStyle.Information, "Informação")
        Catch
            MsgBox(Err.Description, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

#End Region

#Region "Actualizar"

    Private Sub read_content()
        Try
            'Le o conteudo
            doc = XDocument.Load(appPath + "\config.xml")

            'para ler vindo do formload
            If reader_id = False Then
                reader_id = True
                Dim tycnn = (From xe In doc.Descendants.Elements("Cnntype") _
                Select New With {.type = xe.<type>.Value}).First
                ty_combo.Text = tycnn.type
            End If

            If ty_combo.Text = "Local" Then
                Dim qlist = (From xe In doc.Descendants.Elements("Local") _
                    Select New With {.filepath = xe.<filepath>.Value}).First
                path_bd.Text = qlist.filepath
            ElseIf ty_combo.Text = "Lan" Then
                Dim qList = (From xe In doc.Descendants.Elements("Lan") _
                    Select New With { _
                .filepath = xe.<filepath>.Value, _
                .datasource = xe.<datasource>.Value, _
                .usersql = xe.<usersql>.Value, _
                .passsql = xe.<passsql>.Value _
                }).First
                path_bd.Text = qList.filepath
                pc_nome.Text = qList.datasource
                user_sql.Text = qList.usersql
                pass_sql.Text = qList.passsql
            End If
        Catch

        End Try
    End Sub

    Private Sub save_content()
        Try
            'Grava o conteudo, e actualiza
            doc = XDocument.Load(appPath + "\config.xml")
            If ty_combo.Text = "Local" Then
                path_bd.Text.ToUpper()
                Dim editnode As XElement = doc.Descendants.Elements("Local").First
                Dim editnode2 As XElement = doc.Descendants.Elements("Cnntype").First
                editnode.<filepath>.Value = path_bd.Text
                editnode2.<type>.Value = ty_combo.Text
            ElseIf ty_combo.Text = "Lan" Then
                Dim editnode As XElement = doc.Descendants.Elements("Lan").First
                Dim editnode2 As XElement = doc.Descendants.Elements("Cnntype").First
                editnode.<filepath>.Value = path_bd.Text
                editnode.<datasource>.Value = pc_nome.Text
                editnode.<usersql>.Value = user_sql.Text
                editnode.<passsql>.Value = pass_sql.Text
                editnode2.<type>.Value = ty_combo.Text
            End If
            doc.Save(appPath + "\config.xml")
            cnn()
        Catch

        End Try
    End Sub
#End Region

End Class