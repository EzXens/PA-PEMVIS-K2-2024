Imports MySql.Data.MySqlClient

Public Class FMenuAdmin

    Private Sub FMenuAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanTransaksi()
    End Sub
    Private Sub TampilkanTransaksi()
        Try
            koneksi()
            Dim query As String = "SELECT transaksi_id, game_id, mata_uang_id, user_id, uid, tanggal_transaksi, status FROM transaksi WHERE status = 'Pending'"
            Dim adapter As New MySqlDataAdapter(query, CONN)
            Dim table As New DataTable()
            adapter.Fill(table)
            DataGridView1.DataSource = table
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Sub btnAcc_Click(sender As Object, e As EventArgs) Handles btnAcc.Click
        Dim selectedRow As DataGridViewRow = DataGridView1.CurrentRow
        If selectedRow IsNot Nothing Then
            Dim result As DialogResult = MessageBox.Show("Apakah Anda yakin ingin ACC transaksi ini?", "Konfirmasi ACC", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                Dim transaksi_id As Integer = Convert.ToInt32(selectedRow.Cells("transaksi_id").Value)
                UpdateStatusTransaksi(transaksi_id)
            End If
        Else
            MessageBox.Show("Tidak ada Pemesanan Yang Perlu Di ACC")
        End If
    End Sub


    Private Sub UpdateStatusTransaksi(transaksi_id As Integer)
        Try
            koneksi()
            Dim queryTransaksi As String = "SELECT user_id, mata_uang_id, jumlah_topup FROM transaksi WHERE transaksi_id = @transaksi_id"
            Dim commandTransaksi As New MySqlCommand(queryTransaksi, CONN)
            commandTransaksi.Parameters.AddWithValue("@transaksi_id", transaksi_id)
            Dim reader As MySqlDataReader = commandTransaksi.ExecuteReader()

            Dim userId As Integer
            Dim mata_uang_id As Integer
            Dim jumlah_topup As Decimal

            If reader.Read() Then
                userId = Convert.ToInt32(reader("user_id"))
                mata_uang_id = Convert.ToInt32(reader("mata_uang_id"))
                jumlah_topup = Convert.ToDecimal(reader("jumlah_topup"))
            Else
                MessageBox.Show("Transaksi tidak ditemukan.")
                reader.Close()
                Return
            End If
            reader.Close()
            Dim queryUpdateStatus As String = "UPDATE transaksi SET status = 'Berhasil' WHERE transaksi_id = @transaksi_id"
            Dim commandUpdateStatus As New MySqlCommand(queryUpdateStatus, CONN)
            commandUpdateStatus.Parameters.AddWithValue("@transaksi_id", transaksi_id)
            commandUpdateStatus.ExecuteNonQuery()
            Dim queryUpdateSaldo As String = "UPDATE user SET saldo = saldo - @jumlah_topup WHERE user_id = @user_id"
            Dim commandUpdateSaldo As New MySqlCommand(queryUpdateSaldo, CONN)
            commandUpdateSaldo.Parameters.AddWithValue("@jumlah_topup", jumlah_topup)
            commandUpdateSaldo.Parameters.AddWithValue("@user_id", userId)
            commandUpdateSaldo.ExecuteNonQuery()

            MessageBox.Show("Status transaksi berhasil diperbarui menjadi 'Berhasil' dan saldo pengguna berkurang.")
            TampilkanTransaksi()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Sub PToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PToolStripMenuItem.Click
        FormMenuAdmin.Show()
        Me.Hide()
    End Sub

    Private Sub DataMataUangGameToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DataMataUangGameToolStripMenuItem1.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Flogin.Show()
        Me.Hide()
    End Sub

End Class
