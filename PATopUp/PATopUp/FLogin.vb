Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Security.Cryptography
Imports System.Text

Public Class Flogin

    Private Property Color As Color

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        If username = "admin123" AndAlso password = "12345" Then
            Dim menuAdmin As New FMenuAdmin()
            menuAdmin.Show()
            Me.Hide()
            Return
        End If

        koneksi()
        Dim hashedPassword As String = GetHash(password)
        Dim query As String = "SELECT user_id FROM user WHERE username = @username AND password = @password"
        Dim command As New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@username", username)
        command.Parameters.AddWithValue("@password", hashedPassword)

        Try
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                Dim userId As Integer = reader("user_id")
                Dim data_game As New data_game(userId)
                data_game.Show()
                Me.Hide()
            Else
                MessageBox.Show("Username atau password salah.")
            End If
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Function GetHash(input As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(input))
            Dim builder As New StringBuilder()
            For Each t As Byte In bytes
                builder.Append(t.ToString("x2"))
            Next

            Return builder.ToString()
        End Using
    End Function

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Fregistrasi.Show()
        Me.Hide()
    End Sub

    Private Sub Flogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub Flogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = ColorTranslator.FromHtml("#253543")
        AddHandler btnLogin.Paint, AddressOf Button_Paint
        AddHandler btnRegister.Paint, AddressOf Button_Paint
    End Sub

    Private Sub Button_Paint(sender As Object, e As PaintEventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim g As Graphics = e.Graphics
        Dim color As Color = ColorTranslator.FromHtml("#2e95d3")
        g.Clear(color)
        Dim sf As New StringFormat() With {
            .Alignment = StringAlignment.Center,
            .LineAlignment = StringAlignment.Center
        }
        g.DrawString(btn.Text, btn.Font, Brushes.White, btn.ClientRectangle, sf)
    End Sub

    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True

            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            btnLogin.PerformClick()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
