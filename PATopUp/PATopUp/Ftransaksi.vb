Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class Ftransaksi
    Private gameId As Integer
    Private mata_uang_id As Integer
    Private userId As Integer
    Private harga_per_unit As Decimal


    Public Sub New(gameId As Integer, mata_uang_id As Integer, userId As Integer)
        InitializeComponent()
        Me.gameId = gameId
        Me.mata_uang_id = mata_uang_id
        Me.userId = userId

    End Sub

    Private Sub Ftransaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.FromArgb(37, 53, 67)
        dtpTanggalTransaksi.Value = DateTime.Now

        AmbilDataGame()
        AmbilDataMataUang()

    End Sub

    Private Sub AmbilDataGame()
        Try
            koneksi()
            Dim query As String = "SELECT nama_game, gambar FROM game WHERE game_id = @game_id"
            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@game_id", gameId)

            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                lblGame.Text = reader("nama_game").ToString()
                PictureBox1.Image = Image.FromFile(reader("gambar").ToString())
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

    Private Sub AmbilDataMataUang()
        Try
            koneksi()
            Dim query As String = "SELECT nama_mata_uang, gambar, jumlah, harga_per_unit FROM matauanggame WHERE mata_uang_id = @mata_uang_id"
            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@mata_uang_id", mata_uang_id)

            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                lblmatauang.Text = reader("nama_mata_uang").ToString()
                lblmatauang.ForeColor = Color.White
                PictureBox2.Image = Image.FromFile(reader("gambar").ToString())
                lbljumlah.Text = reader("jumlah").ToString()
                lbljumlah.ForeColor = Color.White
                Dim hargaPerUnit As Decimal = Convert.ToDecimal(reader("harga_per_unit"))
                lblharga.Text = hargaPerUnit.ToString("C2")
                lblharga.ForeColor = Color.White
                harga_per_unit = Convert.ToDecimal(reader("harga_per_unit"))
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

    Private Sub btnKon_Click(sender As Object, e As EventArgs) Handles btnkon.Click
        Dim uid As String = txtuid.Text.Trim()
        If String.IsNullOrEmpty(uid) Then
            MessageBox.Show("UID tidak boleh kosong.")
            Return
        End If

        If uid.Length < 9 Then
            MessageBox.Show("UID minimal 9 digit.")
            Return
        End If

        Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin melakukan transaksi?", "Konfirmasi Transaksi", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            SimpanTransaksi(uid)
        End If
    End Sub

    Private Sub SimpanTransaksi(uid As String)
        Try
            koneksi()
            Dim saldo As Decimal = 0
            Dim querySaldo As String = "SELECT saldo FROM user WHERE user_id = @user_id"
            Dim commandSaldo As New MySqlCommand(querySaldo, CONN)
            commandSaldo.Parameters.AddWithValue("@user_id", userId)
            saldo = Convert.ToDecimal(commandSaldo.ExecuteScalar())

            If saldo < harga_per_unit Then
                MessageBox.Show("Saldo tidak mencukupi.")
                Return
            End If

            Dim queryTransaksi As String = "INSERT INTO transaksi (game_id, mata_uang_id, user_id, uid, tanggal_transaksi, status, jumlah_topup) VALUES (@game_id, @mata_uang_id, @user_id, @uid, @tanggal_transaksi, 'Pending', @harga_per_unit)"
            Dim commandTransaksi As New MySqlCommand(queryTransaksi, CONN)
            commandTransaksi.Parameters.AddWithValue("@game_id", gameId)
            commandTransaksi.Parameters.AddWithValue("@mata_uang_id", mata_uang_id)
            commandTransaksi.Parameters.AddWithValue("@user_id", userId)
            commandTransaksi.Parameters.AddWithValue("@uid", uid)
            commandTransaksi.Parameters.AddWithValue("@harga_per_unit", harga_per_unit)
            commandTransaksi.Parameters.AddWithValue("@tanggal_transaksi", dtpTanggalTransaksi.Value)

            commandTransaksi.ExecuteNonQuery()
            MessageBox.Show("Transaksi berhasil disimpan dengan status 'Pending'.")
            Dim formDataUang As New FdataUang(gameId, userId)
            formDataUang.Show()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub


    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim formDataUang As New FdataUang(gameId, userId)
        formDataUang.Show()
        Me.Close()
    End Sub

    Private Sub txtuid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtuid.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        Dim textLength As Integer = txtuid.Text.Length
        If textLength >= 13 AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


    Private Sub txtuid_TextChanged(sender As Object, e As EventArgs) Handles txtuid.TextChanged

    End Sub
End Class
