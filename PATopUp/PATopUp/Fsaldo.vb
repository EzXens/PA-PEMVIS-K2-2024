Imports MySql.Data.MySqlClient

Public Class Fsaldo
    Private saldo As Decimal
    Private userId As Integer
    Private Const MaksimumSaldo As Decimal = 50000000

    Public Sub New(userId As Integer)
        InitializeComponent()
        Me.userId = userId
    End Sub

    Private Sub BtnCon_Click(sender As Object, e As EventArgs) Handles btnCon.Click
      
        Dim tambahanSaldo As Decimal
        If Decimal.TryParse(txtsaldo.Text, tambahanSaldo) Then

            If tambahanSaldo <= 0 Then
                MessageBox.Show("Jumlah saldo harus lebih dari 0.")
                Return
            End If

            Dim saldoBaru As Decimal = saldo + tambahanSaldo
            If saldoBaru > MaksimumSaldo Then
                MessageBox.Show("Saldo tidak boleh melebihi 50 juta.")
                Return
            End If
            saldo = saldoBaru
            SimpanSaldoKeDatabase()
            lblSaldo.Text = "Saldo Anda: " & saldo.ToString("C2")
            MessageBox.Show("Saldo berhasil ditambahkan.")
            txtsaldo.Text = ""

        Else
            MessageBox.Show("Masukkan jumlah saldo yang valid.")
        End If
    End Sub

    Private Sub Fsaldo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AmbilSaldoDariDatabase()
        Me.BackColor = ColorTranslator.FromHtml("#253543")
    End Sub

    Private Sub AmbilSaldoDariDatabase()
        Try
            koneksi()
            Dim query As String = "SELECT saldo FROM user WHERE user_id = @user_id"
            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@user_id", userId)

            Dim saldoObj As Object = command.ExecuteScalar()
            If saldoObj IsNot Nothing AndAlso Decimal.TryParse(saldoObj.ToString(), saldo) Then
                lblSaldo.Text = "Saldo Anda: " & saldo.ToString("C2")
                lblSaldo.ForeColor = Color.White
            Else
                MessageBox.Show("Saldo tidak ditemukan atau tidak valid.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally

            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Sub SimpanSaldoKeDatabase()
        Try

            koneksi()

            Dim query As String = "UPDATE user SET saldo = @saldo WHERE user_id = @user_id"
            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@saldo", saldo)
            command.Parameters.AddWithValue("@user_id", userId)
            command.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Public Sub RefreshSaldo()
        AmbilSaldoDariDatabase()
    End Sub

    Private Sub txtsaldo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsaldo.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

        Dim textLength As Integer = txtsaldo.Text.Length

        If textLength >= 8 AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub
End Class
