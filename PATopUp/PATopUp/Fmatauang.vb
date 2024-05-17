Imports MySql.Data.MySqlClient

Public Class Fmatauang
    Private gameId As String
    Private namaGameTerpilih As String
    Private selectedButton As Button '


    Private Sub FormMenuAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gameId = GetGameIdByGameName(namaGameTerpilih)
        TampilkanDataUang()
    End Sub

    Private Function GetMataUangIdByNamaMataUang(ByVal namaMataUang As String) As Integer
        Dim mataUangId As Integer = -1

        Dim query As String = "SELECT mata_uang_id FROM matauanggame WHERE nama_mata_uang = @nama_mata_uang"
        Dim command As MySqlCommand = New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@nama_mata_uang", namaMataUang)

        Try
            CONN.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()

            If reader.Read() Then
                mataUangId = Convert.ToInt32(reader("mata_uang_id"))
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            CONN.Close()
        End Try

        Return mataUangId
    End Function

    Private Function GetGameIdByGameName(namaGame As String) As Integer
        Try
            Dim gameId As Integer = -1
            koneksi() 
            Dim query As String = "SELECT game_id FROM game WHERE nama_game = @nama_game"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@nama_game", namaGame)
            Dim result As Object = CMD.ExecuteScalar()

            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                gameId = Convert.ToInt32(result)
            End If

            Return gameId
        Catch ex As Exception
            MessageBox.Show("Error333: " & ex.Message)
            Return -1
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Function

    Public Sub New(ByVal gameName As String)
        InitializeComponent()
        Me.gameId = gameId
        Me.namaGameTerpilih = gameName
        TampilkanDataUang()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        BukaFile.Filter = "PNG Image |*.png|JPG Image |*.jpg|GIF Image |*.gif|JPEG Image |*.jpeg"
        If BukaFile.ShowDialog() = DialogResult.OK Then
            PictureBox1.ImageLocation = BukaFile.FileName
        End If
    End Sub

    Private Sub TampilkanDataUang()
        Try
            FlowLayoutPanel1.Controls.Clear()
            koneksi() 
            Dim query As String = "SELECT * FROM matauanggame WHERE game_id = @game_id"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@game_id", gameId)
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            While reader.Read()
                Dim mataUang As String = reader("nama_mata_uang").ToString()
                Dim jumlah As Integer = Convert.ToInt32(reader("jumlah"))
                Dim hargaPerUnit As Decimal = Convert.ToDecimal(reader("harga_per_unit"))
                Dim gambarMataUang As String = reader("gambar").ToString()

                Dim button As New Button()
                button.Width = 150
                button.Height = 300
                button.ForeColor = Color.Black
                button.BackColor = ColorTranslator.FromHtml("#c4c4c4")
                button.TextAlign = ContentAlignment.BottomCenter
                button.Font = New Font(button.Font.FontFamily, 10)
                button.Tag = mataUang

                button.Text = mataUang & vbCrLf & "Jumlah: " & jumlah.ToString() & vbCrLf & "Harga per Unit: " & hargaPerUnit.ToString("C2")

                Dim pictureBox As New PictureBox()
                pictureBox.Width = button.Width
                pictureBox.Height = 200
                pictureBox.Top = 0
                pictureBox.Left = 0
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom
                pictureBox.Image = Image.FromFile(gambarMataUang)

                ' Menambahkan PictureBox ke dalam tombol
                button.Controls.Add(pictureBox)

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
        Dim namaUangTerpilih As String = clickedButton.Tag.ToString()
    End Sub

    Private Sub TambahDataMataUang()
        If String.IsNullOrEmpty(txtnamauang.Text) OrElse String.IsNullOrEmpty(txtjumlah.Text) OrElse String.IsNullOrEmpty(txtharga.Text) OrElse PictureBox1.ImageLocation Is Nothing Then
            MessageBox.Show("Semua Kolom harus diisi.")
            Return
        End If
        Try
            Dim gameId As Integer = GetGameIdByGameName(namaGameTerpilih)
            koneksi() 
            If gameId <> -1 Then

                Dim query As String = "INSERT INTO matauanggame (game_id, nama_mata_uang, jumlah, harga_per_unit, gambar) VALUES (@game_id, @nama_mata_uang, @jumlah, @harga_per_unit, @gambar)"
                CMD = New MySqlCommand(query, CONN)
                CMD.Parameters.AddWithValue("@game_id", gameId)
                CMD.Parameters.AddWithValue("@nama_mata_uang", txtnamauang.Text)
                CMD.Parameters.AddWithValue("@jumlah", Convert.ToInt32(txtjumlah.Text))
                CMD.Parameters.AddWithValue("@harga_per_unit", Convert.ToDecimal(txtharga.Text))
                CMD.Parameters.AddWithValue("@gambar", PictureBox1.ImageLocation)
                Dim rowsAffected As Integer = CMD.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    MessageBox.Show("Data mata uang berhasil ditambahkan!")
                    TampilkanDataUang()
                Else
                    MessageBox.Show("Data gagal ditambahkan!")
                End If
                txtnamauang.Clear()
                txtjumlah.Clear()
                txtharga.Clear()
                PictureBox1.Image = Nothing
            Else
                MessageBox.Show("Nama game tidak valid!")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Sub btntambah_Click(sender As Object, e As EventArgs) Handles btntambah.Click
        TambahDataMataUang()
    End Sub

    Private Sub btnubah_Click(sender As Object, e As EventArgs) Handles btnubah.Click
        If selectedButton IsNot Nothing Then
            Dim namaUangTerpilih As String = selectedButton.Tag.ToString()
            Dim gameName As String = namaGameTerpilih
            Dim FubahUang As New FubahUang(namaUangTerpilih, gameName)
            FubahUang.Show()
            Me.Hide()

        Else
            MessageBox.Show("Pilih game terlebih dahulu.")
        End If
    End Sub

    Private Sub btnhapus_Click(sender As Object, e As EventArgs) Handles btnhapus.Click
        If selectedButton IsNot Nothing Then
            Dim namaUangTerpilih As String = selectedButton.Tag.ToString()
            If MessageBox.Show("Anda yakin ingin menghapus uang " & namaUangTerpilih & "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                DeleteUang(namaUangTerpilih)
            End If
        Else
            MessageBox.Show("Pilih game terlebih dahulu.")
        End If
        If selectedButton IsNot Nothing Then
            selectedButton.Focus()
        End If
    End Sub

    Private Sub DeleteUang(mataUang As String)
        Dim query As String = "DELETE FROM matauanggame WHERE mata_uang_id = @mata_uang_id"
        Dim command As MySqlCommand = New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@nama_mata_uang", mataUang)
        command.Parameters.AddWithValue("@mata_uang_id", GetMataUangIdByNamaMataUang(mataUang))

        Try
            CONN.Open()
            command.ExecuteNonQuery()
            MessageBox.Show("Game berhasil dihapus dari database.")
            TampilkanDataUang()
            For Each btn As Button In FlowLayoutPanel1.Controls
                If btn.Text = mataUang Then
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
        Dim Form2 As New Form2()
        Form2.Show()
        Me.Close()
    End Sub

    Private Sub txtjumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtjumlah.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        Dim textLength As Integer = txtjumlah.Text.Length

        If textLength >= 8 AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtharga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtharga.KeyPress
      If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        Dim textLength As Integer = txtharga.Text.Length
        If textLength >= 8 AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtnamauang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnamauang.KeyPress
        If Not Char.IsLetter(e.KeyChar) And Not Char.IsControl(e.KeyChar) And Not e.KeyChar = " " Then
            e.Handled = True
        End If
    End Sub


End Class