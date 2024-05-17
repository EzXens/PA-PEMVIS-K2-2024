Imports MySql.Data.MySqlClient

Public Class FormMenuAdmin
    Private selectedButton As Button
    Private Sub FormMenuAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanDataGame()
    End Sub
    Private namaGameTerpilih As String

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        BukaFile.Filter = "PNG Image |*.png|JPG Image|*.jpg|GIF Image |*.gif"
        If BukaFile.ShowDialog() = DialogResult.OK Then
            PictureBox1.ImageLocation = BukaFile.FileName
        End If
    End Sub
Private Sub TampilkanDataGame()
        Try
            FlowLayoutPanel1.Controls.Clear()
            koneksi()
            Dim query As String = "SELECT * FROM game"
            CMD = New MySqlCommand(query, CONN)
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            While reader.Read()
                Dim gameName As String = reader("nama_game").ToString()
                Dim gameImage As String = reader("gambar").ToString()

                Dim button As New Button()
                button.Width = 150
                button.Height = 200
                button.TextImageRelation = TextImageRelation.ImageAboveText
                button.Font = New Font(button.Font.FontFamily, 14)
                button.Tag = gameName
                button.BackgroundImage = Image.FromFile(gameImage)
                button.BackgroundImageLayout = ImageLayout.Zoom
                Dim label As New Label()
                label.Text = gameName
                label.Font = New Font(label.Font.FontFamily, 10)
                label.TextAlign = ContentAlignment.MiddleCenter
                label.Dock = DockStyle.Bottom
                button.Controls.Add(label)
                AddHandler button.Click, AddressOf Button_Click
                FlowLayoutPanel1.AutoScroll = True
                FlowLayoutPanel1.Controls.Add(button)

            End While

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub
    Private Sub Button_Click(sender As Object, e As EventArgs)
        Dim clickedButton As Button = DirectCast(sender, Button)
        If selectedButton IsNot Nothing Then
            selectedButton.BackColor = Color.Transparent
        End If
        clickedButton.BackColor = Color.Gray
        selectedButton = clickedButton
        namaGameTerpilih = clickedButton.Tag.ToString()
    End Sub


  
    Private Sub btnubah_Click(sender As Object, e As EventArgs) Handles btnubah.Click
        If selectedButton IsNot Nothing Then
            Dim namaGameTerpilih As String = selectedButton.Tag.ToString()
            Dim Formubah As New Formubah(namaGameTerpilih)
            Formubah.Show()
            Me.Close()
        Else
            MessageBox.Show("Pilih data game terlebih dahulu.")
        End If
    End Sub

    Private Sub TambahDataGame()
        If String.IsNullOrEmpty(txtnamagame.Text) OrElse PictureBox1.Image Is Nothing Then
            MessageBox.Show("Nama game dan gambar tidak boleh kosong.")
            Return
        End If
        Try
            koneksi()
            Dim query As String = "INSERT INTO game (nama_game, gambar) VALUES (@nama_game, @gambar)"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@nama_game", txtnamagame.Text)
            CMD.Parameters.AddWithValue("@gambar", PictureBox1.ImageLocation)
            FlowLayoutPanel1.Controls.Clear()
            Dim rowsAffected As Integer = CMD.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MessageBox.Show("Data game berhasil ditambahkan!")
                TampilkanDataGame()
            Else
                MessageBox.Show("Data gagal ditambahkan!")
            End If
            txtnamagame.Clear()
            PictureBox1.Image = Nothing
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub
    Private Function GetSelectedgame() As String
        For Each btn As Button In FlowLayoutPanel1.Controls
            If btn.Focused Then
                Return btn.Tag.ToString()
            End If
        Next
        Return ""
    End Function
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        TambahDataGame()
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If selectedButton IsNot Nothing Then
            Dim namaGameTerpilih As String = selectedButton.Tag.ToString()
            If MessageBox.Show("Anda yakin ingin menghapus game " & namaGameTerpilih & "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                DeleteGame(namaGameTerpilih)
            End If
        Else
            MessageBox.Show("Pilih game terlebih dahulu.")
        End If
        If selectedButton IsNot Nothing Then
            selectedButton.Focus()
        End If
    End Sub

    Private Sub DeleteGame(gameName As String)
        Dim query As String = "DELETE FROM game WHERE nama_game = @nama_game"
        Dim command As MySqlCommand = New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@nama_game", gameName)

        Try
            CONN.Open()
            command.ExecuteNonQuery()
            MessageBox.Show("Game berhasil dihapus dari database.")
            TampilkanDataGame()
            For Each btn As Button In FlowLayoutPanel1.Controls
                If btn.Text = gameName Then
                    FlowLayoutPanel1.Controls.Remove(btn)
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            CONN.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FMenuAdmin As New FMenuAdmin()
        FMenuAdmin.Show()
        Me.Close()
    End Sub
End Class
