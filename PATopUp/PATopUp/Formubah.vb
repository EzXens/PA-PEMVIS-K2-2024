Imports MySql.Data.MySqlClient
Public Class Formubah

    Private namaGameTerpilih As String

    Public Sub New(ByVal gameName As String)
        InitializeComponent()
        Me.namaGameTerpilih = gameName
        LoadgameData()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        BukaFile.Filter = "PNG Image |*.png|JPG Image|*.jpg|GIF Image |*.gif"
        If BukaFile.ShowDialog() = DialogResult.OK Then
            PictureBox1.ImageLocation = BukaFile.FileName
        End If
    End Sub

    Private Sub LoadgameData()
        Dim query As String = "SELECT * FROM game WHERE nama_game = @nama_game"
        Dim command As MySqlCommand = New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@nama_game", namaGameTerpilih)

        Try
            CONN.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()

            If reader.Read() Then
                txtnamagame.Text = reader("nama_game").ToString()
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            CONN.Close()
        End Try
    End Sub
    Private Sub SaveDataGame()

        If String.IsNullOrEmpty(txtnamagame.Text) OrElse PictureBox1.ImageLocation Is Nothing Then
            MessageBox.Show("Nama game dan gambar tidak boleh kosong.")
            Return
        End If
        Dim query As String = "UPDATE game SET nama_game = @nama_game, gambar = @gambar WHERE nama_game = @OldName"
        Dim command As MySqlCommand = New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@nama_game", txtnamagame.Text)
        command.Parameters.AddWithValue("@gambar", PictureBox1.ImageLocation)
        command.Parameters.AddWithValue("@OldName", namaGameTerpilih)

        Try
            CONN.Open()
            command.ExecuteNonQuery()
            MessageBox.Show("Data Game berhasil diperbarui.")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            CONN.Close()
        End Try
    End Sub

    Private Sub btnubah_Click(sender As Object, e As EventArgs) Handles btnubah.Click
        SaveDataGame()
        FormMenuAdmin.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FormMenuAdmin As New FormMenuAdmin()
        FormMenuAdmin.Show()
        Me.Close()
    End Sub

    Private Sub Formubah_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
