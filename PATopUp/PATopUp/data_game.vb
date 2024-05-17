Imports MySql.Data.MySqlClient

Public Class data_game

    Public Property UserId As Integer

    Private Sub data_game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler btnstatus.Paint, AddressOf Button_Paint
        AddHandler Button1.Paint, AddressOf Button_Paint
        AddHandler btncari.Paint, AddressOf Button_Paint
        TampilkanNamaUser()
        TampilkanSemuaGame()
    End Sub
   Public Sub New(ByVal userId As Integer)
        InitializeComponent()
        Me.UserId = userId
    End Sub

    Public Property username As String

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


    Private Sub TampilkanSemuaGame()
        Try
            FlowLayoutPanel1.Controls.Clear()
            koneksi()
            Dim query As String = "SELECT * FROM game"
            CMD = New MySqlCommand(query, CONN)
            Dim reader As MySqlDataReader = CMD.ExecuteReader()

            Dim dataAda As Boolean = False

            While reader.Read()
                dataAda = True
                Dim namaGame As String = reader("nama_game").ToString()
                Dim gameImage As String = reader("gambar").ToString()
                Dim gameId As Integer = Convert.ToInt32(reader("game_id"))


                Dim button As New Button()
                button.Width = 150
                button.Height = 200

                button.TextImageRelation = TextImageRelation.ImageAboveText
                button.Font = New Font(button.Font.FontFamily, 14)
                button.Tag = gameId
                button.BackgroundImage = Image.FromFile(gameImage)
                button.BackColor = Color.White
                button.BackgroundImageLayout = ImageLayout.Zoom

                Dim label As New Label()
                label.Text = namaGame
                label.Font = New Font(label.Font.FontFamily, 10)
                label.TextAlign = ContentAlignment.MiddleCenter
                label.ForeColor = Color.Black
                label.Dock = DockStyle.Bottom
                button.Controls.Add(label)
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

    Private Sub BtnCari_Click(sender As Object, e As EventArgs) Handles btncari.Click

        Dim kataKunci As String = txtcari.Text.Trim()
        If Not String.IsNullOrEmpty(kataKunci) Then
            CariGame(kataKunci)
        Else
            TampilkanSemuaGame()
        End If
    End Sub

    Private Sub CariGame(kataKunci As String)
        Try
            koneksi()

            Dim query As String = "SELECT * FROM game WHERE nama_game LIKE @kataKunci"
            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@kataKunci", "%" & kataKunci & "%")
            Dim reader As MySqlDataReader = command.ExecuteReader()
            FlowLayoutPanel1.Controls.Clear()

            Dim dataAda As Boolean = False
            While reader.Read()
                dataAda = True
                Dim namaGame As String = reader("nama_game").ToString()
                Dim gameImage As String = reader("gambar").ToString()
                Dim gameId As Integer = Convert.ToInt32(reader("game_id"))


                Dim button As New Button()
                button.Width = 150
                button.Height = 200

                button.TextImageRelation = TextImageRelation.ImageAboveText
                button.Font = New Font(button.Font.FontFamily, 14)
                button.Tag = gameId
                button.BackgroundImage = Image.FromFile(gameImage)
                button.BackColor = Color.White
                button.BackgroundImageLayout = ImageLayout.Zoom

                Dim label As New Label()
                label.Text = namaGame
                label.Font = New Font(label.Font.FontFamily, 10)
                label.TextAlign = ContentAlignment.MiddleCenter
                label.ForeColor = Color.Black
                label.Dock = DockStyle.Bottom
                button.Controls.Add(label)
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
        Dim gameId As Integer = Convert.ToInt32(button.Tag)

        Dim formDataUang As New FdataUang(gameId, UserId)
        formDataUang.Show()
        Me.Hide()
    End Sub

    Private Function GetGameIdByName(namaGame As String) As Integer
        Try
            koneksi()
            Dim query As String = "SELECT game_id FROM game WHERE nama_game = @nama_game"
            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@nama_game", namaGame)
            Dim gameId As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return gameId
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            Return -1
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim formFsaldo As New Fsaldo(UserId)
        formFsaldo.Show()
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

    Private Sub btnstatus_Click(sender As Object, e As EventArgs) Handles btnstatus.Click
        Dim formUserStatus As New Fuserstatus(UserId)
        formUserStatus.Show()
        Me.Hide()
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim login As New Flogin()
        login.Show()
        Me.Close()
    End Sub

End Class
