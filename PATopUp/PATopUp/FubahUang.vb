Imports MySql.Data.MySqlClient
Public Class FubahUang
    Private namaUangTerpilih As String
    Private gameName As String

    Public Sub New(ByVal mataUang As String, ByVal gameName As String)
        InitializeComponent()
        Me.namaUangTerpilih = mataUang
        Me.gameName = gameName
        End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SaveDataMataUang()
    End Sub

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        BukaFile.Filter = "PNG Image |*.png|JPG Image|*.jpg|GIF Image |*.gif"
        If BukaFile.ShowDialog() = DialogResult.OK Then
            PictureBox1.ImageLocation = BukaFile.FileName
        End If
    End Sub

    Private Sub LoadMataUangData()
        Dim query As String = "SELECT * FROM matauanggame WHERE nama_mata_uang = @nama_mata_uang"
        Dim command As MySqlCommand = New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@nama_mata_uang", namaUangTerpilih)

        Try
            CONN.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()

            If reader.Read() Then
                txtnamauang.Text = reader("nama_mata_uang").ToString()
                txtjumlah.Text = reader("jumlah").ToString()
                txtharga.Text = reader("harga_per_unit").ToString()
                PictureBox1.ImageLocation = reader("gambar").ToString()
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            CONN.Close()
        End Try
    End Sub

    Private Sub SaveDataMataUang()
        If String.IsNullOrEmpty(txtnamauang.Text) OrElse String.IsNullOrEmpty(txtjumlah.Text) OrElse String.IsNullOrEmpty(txtharga.Text) OrElse PictureBox1.ImageLocation Is Nothing Then
            MessageBox.Show("Semua Kolom harus diisi.")
            Return
        End If
        Dim query As String = "UPDATE matauanggame SET nama_mata_uang = @nama_mata_uang, jumlah = @jumlah, harga_per_unit = @harga_per_unit, gambar = @gambar WHERE mata_uang_id = @mata_uang_id"
        Dim command As MySqlCommand = New MySqlCommand(query, CONN)
        command.Parameters.AddWithValue("@nama_mata_uang", txtnamauang.Text)
        command.Parameters.AddWithValue("@jumlah", Convert.ToInt32(txtjumlah.Text))
        command.Parameters.AddWithValue("@harga_per_unit", Convert.ToDecimal(txtharga.Text))
        command.Parameters.AddWithValue("@gambar", PictureBox1.ImageLocation)
        command.Parameters.AddWithValue("@mata_uang_id", GetMataUangIdByNamaMataUang(namaUangTerpilih))

        Try
            CONN.Open()
            command.ExecuteNonQuery()
            MessageBox.Show("Data mata uang berhasil diperbarui.")
            Dim Fmatauang As New Fmatauang(gameName)
            Fmatauang.Show()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            CONN.Close()
        End Try
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

    Private Sub txtnamauang_TextChanged(sender As Object, e As EventArgs) Handles txtnamauang.TextChanged

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Fmatauang As New Fmatauang(gameName)
        Fmatauang.Show()
        Me.Close()
    End Sub

End Class