Imports MySql.Data.MySqlClient

Public Class FdataUang
    Private gameId As Integer
    Private userId As Integer

    Public Sub New(ByVal gameId As Integer, ByVal userId As Integer)
        InitializeComponent()
        Me.gameId = gameId
        Me.userId = userId
        TampilkanDataUang()
    End Sub

    Private Sub FdataUang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanNamaUser()
        TampilkanDataUang()
    End Sub

    Private Sub TampilkanNamaUser()
        Try
            koneksi()
            Dim query As String = "SELECT username FROM user WHERE user_id = @UserId"
            Dim cmd As New MySqlCommand(query, CONN)
            cmd.Parameters.AddWithValue("@UserId", Me.UserId)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                lblNamaUser.Text = "Welcome, " & reader("username").ToString()
            Else
                lblNamaUser.Text = "User not found"
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

    Private Sub TampilkanDataUang()
        Try
            FlowLayoutPanel1.Controls.Clear()
            koneksi()
            Dim query As String = "SELECT * FROM matauanggame WHERE game_id = @game_id"
            CMD = New MySqlCommand(query, CONN)
            CMD.Parameters.AddWithValue("@game_id", gameId)
            Dim reader As MySqlDataReader = CMD.ExecuteReader()
            Dim dataAda As Boolean = False
            While reader.Read()
                dataAda = True
                Dim mataUangId As Integer = Convert.ToInt32(reader("mata_uang_id"))
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
                button.Tag = mataUangId

                button.Text = mataUang & vbCrLf & "Jumlah: " & jumlah.ToString() & vbCrLf & "Harga per Unit: " & hargaPerUnit.ToString("C2")

                Dim pictureBox As New PictureBox()
                pictureBox.Width = button.Width
                pictureBox.Height = 200
                pictureBox.Top = 0
                pictureBox.Left = 0
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom
                pictureBox.Image = Image.FromFile(gambarMataUang)


                button.Controls.Add(pictureBox)

                AddHandler button.Click, AddressOf Button_Click
                FlowLayoutPanel1.AutoScroll = True
                FlowLayoutPanel1.Controls.Add(button)
            End While

            reader.Close()
            If Not dataAda Then
                Dim label As New Label()
                label.Text = "Data Belum Tersedia"
                label.ForeColor = Color.White
                label.Font = New Font(label.Font.FontFamily, 12, FontStyle.Bold)
                label.TextAlign = ContentAlignment.MiddleCenter
                label.AutoSize = True

                Dim posX As Integer = (FlowLayoutPanel1.Width - label.Width) \ 2
                Dim posY As Integer = (FlowLayoutPanel1.Height - label.Height) \ 2
                label.Location = New Point(posX, posY)

                FlowLayoutPanel1.Controls.Add(label)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

    Private Sub Button_Click(sender As Object, e As EventArgs)
        Dim button As Button = DirectCast(sender, Button)
        Dim mataUangId As Integer = Convert.ToInt32(button.Tag)

        Dim formTransaksi As New Ftransaksi(gameId, mataUangId, userId)
        formTransaksi.Show()
        Me.Hide()
    End Sub
   
    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim datagame As New data_game(userId)
        datagame.Show()
        Me.Close()
    End Sub

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        Dim kataKunci As String = txtcari.Text.Trim()
        If Not String.IsNullOrEmpty(kataKunci) Then
            CariGame(kataKunci)
        Else
            TampilkanDataUang()
        End If
    End Sub

    Private Sub CariGame(kataKunci As String)
        Try

            koneksi()
            Dim query As String = "SELECT * FROM matauanggame WHERE game_id = @game_id AND nama_mata_uang LIKE @kataKunci"
            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@game_id", gameId)
            command.Parameters.AddWithValue("@kataKunci", "%" & kataKunci & "%")
            Dim reader As MySqlDataReader = command.ExecuteReader()
            FlowLayoutPanel1.Controls.Clear()
            Dim dataAda As Boolean = False
            While reader.Read()
                dataAda = True
                Dim mataUangId As Integer = Convert.ToInt32(reader("mata_uang_id"))
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
                button.Tag = mataUangId
                button.Text = mataUang & vbCrLf & "Jumlah: " & jumlah.ToString() & vbCrLf & "Harga per Unit: " & hargaPerUnit.ToString("C2")


                Dim pictureBox As New PictureBox()
                pictureBox.Width = button.Width
                pictureBox.Height = 200
                pictureBox.Top = 0
                pictureBox.Left = 0
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom
                pictureBox.Image = Image.FromFile(gambarMataUang)

                button.Controls.Add(pictureBox)

                AddHandler button.Click, AddressOf Button_Click
                FlowLayoutPanel1.AutoScroll = True
                FlowLayoutPanel1.Controls.Add(button)

            End While
            reader.Close()
            If Not dataAda Then
                Dim label As New Label()
                label.Text = "Data Belum Tersedia"
                label.ForeColor = Color.White
                label.Font = New Font(label.Font.FontFamily, 12, FontStyle.Bold)
                label.TextAlign = ContentAlignment.MiddleCenter
                label.AutoSize = True
                Dim posX As Integer = (FlowLayoutPanel1.Width - label.Width) \ 2
                Dim posY As Integer = (FlowLayoutPanel1.Height - label.Height) \ 2
                label.Location = New Point(posX, posY)
                FlowLayoutPanel1.Controls.Add(label)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub

End Class
