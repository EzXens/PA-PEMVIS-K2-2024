Imports MySql.Data.MySqlClient
Imports System.Drawing
Imports System.Security.Cryptography
Imports System.Text

Public Class Fregistrasi
    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim username As String = txtUsername.Text
        Dim password As String = txtPassword.Text
        If username.Length < 8 OrElse password.Length < 8 Then
            MessageBox.Show("Username dan password harus terdiri dari minimal 8 karakter.")
            Return
        End If
        If txtPassword.Text <> txtConfirmPassword.Text Then
            MessageBox.Show("Password dan konfirmasi password tidak cocok!")
            Return
        End If
        koneksi()
        If IsUsernameExists(username) Then
            MessageBox.Show("Username sudah digunakan. Silakan masukkan username lain.")
            Return
        End If

        Dim hashedPassword As String = HashPassword(password)
        Dim query As String = "INSERT INTO user (username, password) VALUES (@username, @password)"
        Dim command As New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@username", username)
        command.Parameters.AddWithValue("@password", hashedPassword)

        Try
            CONN.Open()
            Dim rowsAffected As Integer = command.ExecuteNonQuery()
            If rowsAffected > 0 Then
                MessageBox.Show("Registrasi berhasil. Anda dapat login sekarang.")
                Dim formLogin As New Flogin()

                txtPassword.Clear()
                txtUsername.Clear()
                txtConfirmPassword.Clear()

                formLogin.Show()
                Me.Hide()
            Else
                MessageBox.Show("Registrasi gagal.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Function IsUsernameExists(username As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM user WHERE username = @username"
        Dim command As New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@username", username)

        Try

            koneksi()

            Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return count > 0
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return False
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim formLogin As New Flogin()
        formLogin.Show()
        Me.Close()
    End Sub

    Private Sub Fregistrasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler btnRegister.Paint, AddressOf Button_Paint
        AddHandler Button1.Paint, AddressOf Button_Paint
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

 

    Private Sub txtConfirmPassword_TextChanged(sender As Object, e As EventArgs) Handles txtConfirmPassword.TextChanged
        If txtPassword.Text.Length < 8 Then
            lblPasswordMatch.Text = "Password minimal terdiri dari 8 karakter"
            lblPasswordMatch.ForeColor = Color.Red
            lblPasswordMatch.Font = New Font(lblPasswordMatch.Font.FontFamily, 10)
        ElseIf txtPassword.Text <> txtConfirmPassword.Text Then
            lblPasswordMatch.Text = "Tidak cocok!"
            lblPasswordMatch.ForeColor = Color.Red
        Else
            lblPasswordMatch.Text = "Cocok"
            lblPasswordMatch.ForeColor = Color.Green
        End If
    End Sub

    Private Function HashPassword(password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Dim builder As New StringBuilder()
            For Each b As Byte In bytes
                builder.Append(b.ToString("x2"))
            Next
            Return builder.ToString()
        End Using
    End Function

    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True

            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True

            txtConfirmPassword.Focus()
        End If
    End Sub

    Private Sub txtConfirmPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtConfirmPassword.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            btnRegister.PerformClick()
        End If
    End Sub

End Class
