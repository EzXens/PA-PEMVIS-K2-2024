Imports MySql.Data.MySqlClient

Public Class Form2
    Private selectedButton As Button 
    Private Sub FormMenuAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilkanDataGame()
    End Sub
    Private namaGameTerpilih As String

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
                button.TextAlign = ContentAlignment.BottomCenter
                button.BackgroundImageLayout = ImageLayout.Zoom
                button.BackColor = Color.White
                button.BackgroundImage = Image.FromFile(gameImage)
                button.Tag = gameName
                Dim label As New Label()
                label.Text = gameName
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
        Catch ex As Exception
            MessageBox.Show("Error1111: " & ex.Message)
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



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If selectedButton IsNot Nothing Then
            Dim namaGameTerpilih As String = selectedButton.Tag.ToString()
            Dim Fmatauang As New Fmatauang(namaGameTerpilih)
            Fmatauang.Show()
            Me.Close()
        Else
            MessageBox.Show("Pilih Game terlebih dahulu.")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim FMenuAdmin As New FMenuAdmin()
        FMenuAdmin.Show()
        Me.Close()
    End Sub
End Class