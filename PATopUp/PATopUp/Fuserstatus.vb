Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing


Public Class Fuserstatus
    Private userId As Integer
    Private dataTable As DataTable
    Private selectedTransactionId As Integer
    Public Sub New(userId As Integer)
        InitializeComponent()
        Me.userId = userId
    End Sub

    Private Sub Fuserstatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler PrintDocument1.PrintPage, AddressOf Me.printDocument1_PrintPage
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        PrintDocument1.DefaultPageSettings.Landscape = False

        AddHandler PrintDocument1.PrintPage, AddressOf Me.printDocument1_PrintPage
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1

        TampilkanStatus()
        Me.BackColor = ColorTranslator.FromHtml("#253543")
    End Sub
    Private Sub TampilkanStatus()
        dataTable = New DataTable()
        Try
            koneksi()
            Dim command As New MySqlCommand("SELECT t.transaksi_id, g.nama_game, m.nama_mata_uang, t.jumlah_topup, t.tanggal_transaksi, t.uid, t.status FROM transaksi t JOIN game g ON t.game_id = g.game_id JOIN matauanggame m ON t.mata_uang_id = m.mata_uang_id WHERE t.user_id = @user_id", CONN)
            command.Parameters.AddWithValue("@user_id", userId)
            Dim adapter As New MySqlDataAdapter(command)
            adapter.Fill(dataTable)

            Dim yPosition As Integer = 10
            If dataTable.Rows.Count = 0 Then
                Dim lblNoTransaction As New Label()
                lblNoTransaction.Text = "Belum ada riwayat transaksi"
                lblNoTransaction.Location = New Point(10, yPosition)
                lblNoTransaction.AutoSize = True
                lblNoTransaction.ForeColor = Color.White
                lblNoTransaction.Font = New Font("Arial", 12, FontStyle.Bold)
                Me.Controls.Add(lblNoTransaction)
            Else
                For Each row As DataRow In dataTable.Rows
                    Dim jumlahTopup As Decimal = Convert.ToDecimal(row("jumlah_topup"))
                    Dim jumlahTopupRp As String = jumlahTopup.ToString("C2")

                    Dim lblInfo As New Label()
                    lblInfo.Text = "Game: " & row("nama_game").ToString() &
                                   " | Mata Uang: " & row("nama_mata_uang").ToString() &
                                   " | Jumlah Topup: " & jumlahTopupRp &
                                   " | Tanggal: " & row("tanggal_transaksi").ToString() &
                                   " | UID: " & row("uid").ToString() &
                                   " | Status: " & row("status").ToString()
                    lblInfo.Location = New Point(10, yPosition)
                    lblInfo.AutoSize = True
                    lblInfo.ForeColor = Color.White
                    lblInfo.Font = New Font("Arial", 8, FontStyle.Bold)
                    Me.Controls.Add(lblInfo)

                    Dim line As New Label()
                    line.BorderStyle = BorderStyle.Fixed3D
                    line.Size = New Size(Me.ClientSize.Width + 90, 5)
                    line.Location = New Point(10, yPosition + 30)
                    Me.Controls.Add(line)

                    Dim btnCetak As New Button()
                    btnCetak.Text = "Lihat"
                    btnCetak.Tag = row("transaksi_id")
                    btnCetak.Location = New Point(850, yPosition)
                    btnCetak.FlatStyle = FlatStyle.Flat
                    btnCetak.BackColor = Color.DarkBlue
                    btnCetak.ForeColor = Color.White
                    btnCetak.Font = New Font("Arial", 11, FontStyle.Bold)
                    AddHandler btnCetak.Click, AddressOf BtnCetak_Click
                    Me.Controls.Add(btnCetak)

                    Dim btnCetakPDF As New Button()
                    btnCetakPDF.Text = "Cetak"
                    btnCetakPDF.Tag = row("transaksi_id")
                    btnCetakPDF.Location = New Point(930, yPosition)
                    btnCetakPDF.FlatStyle = FlatStyle.Flat
                    btnCetakPDF.BackColor = Color.DarkGreen
                    btnCetakPDF.ForeColor = Color.White
                    btnCetakPDF.Font = New Font("Arial", 11, FontStyle.Bold)
                    AddHandler btnCetakPDF.Click, AddressOf BtnCetakPDF_Click
                    Me.Controls.Add(btnCetakPDF)

                    yPosition += 60
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try
    End Sub



    Private Sub printDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Dim centermargin As Integer = PrintDocument1.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PrintDocument1.DefaultPageSettings.PaperSize.Width

        Dim left As New StringFormat
        Dim center As New StringFormat

        left.Alignment = StringAlignment.Near
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "================================================================"

        Dim NamaToko As String = "ARAD GAMING STORE"
        Dim Alamat As String = "Jl. Perjuangan 1"
        Dim Kota As String = "Samarinda"

        Dim xPos As Single = e.MarginBounds.Left
        Dim yPos As Single = e.MarginBounds.Top

        Dim fontJudul As New Font("Arial", 10, FontStyle.Bold)
        Dim fontInfo As New Font("Arial", 8, FontStyle.Regular)

        Dim image As Image = image.FromFile(CurDir() & "/b.png")

        For Each row As DataRow In dataTable.Rows

            Dim jumlahTopup As Decimal = Convert.ToDecimal(row("jumlah_topup"))
            Dim jumlahTopupRp As String = jumlahTopup.ToString("C2")

            If row("transaksi_id").ToString() = selectedTransactionId.ToString() Then
                Dim game As String = "Game : " & row("nama_game").ToString()
                Dim mataUang As String = "Mata Uang : " & row("nama_mata_uang").ToString()

                Dim jumlahTopupDecimal As Decimal = Convert.ToDecimal(row("jumlah_topup"))
                Dim jumlahTopupFormatted As String = jumlahTopupDecimal.ToString("C2")
                Dim jumlahTopups As String = "Harga Topup : " & jumlahTopupFormatted

                Dim tanggal As String = "Tanggal : " & row("tanggal_transaksi").ToString()
                Dim uid As String = "UID yang Diproses : " & row("uid").ToString()
                Dim status As String = "Status : " & row("status").ToString()



                e.Graphics.DrawString(NamaToko, fontJudul, Brushes.Black, centermargin, 30, center)
                e.Graphics.DrawString(Alamat, fontInfo, Brushes.Black, centermargin, 60, center)
                e.Graphics.DrawString(Kota, fontInfo, Brushes.Black, centermargin, 90, center)
                e.Graphics.DrawString(line, fontInfo, Brushes.Black, centermargin, 120, center)
                e.Graphics.DrawString(tanggal, fontInfo, Brushes.Black, 10, 150)
                e.Graphics.DrawString(uid, fontInfo, Brushes.Black, 10, 180)
                e.Graphics.DrawString("Admin : ARAD ", fontInfo, Brushes.Black, 10, 210)
                e.Graphics.DrawString(line, fontInfo, Brushes.Black, centermargin, 240, center)
                e.Graphics.DrawString(game, fontInfo, Brushes.Black, 10, 270)
                e.Graphics.DrawString(mataUang, fontInfo, Brushes.Black, 10, 300)
                e.Graphics.DrawString(jumlahTopups, fontInfo, Brushes.Black, 10, 330)
                e.Graphics.DrawString(status, fontInfo, Brushes.Black, 10, 360)
                e.Graphics.DrawString(line, fontInfo, Brushes.Black, centermargin, 390, center)

                Dim imageWidth As Single = image.Width * ((e.MarginBounds.Bottom - e.MarginBounds.Top - 420) / image.Height)
                Dim imageXPosition As Single = e.MarginBounds.Left + (e.MarginBounds.Width - imageWidth) / 2
                e.Graphics.DrawImage(image, imageXPosition, 515, imageWidth, e.MarginBounds.Bottom - e.MarginBounds.Top - 420)

            End If
        Next
        e.HasMorePages = False
    End Sub


    Private Sub BtnCetak_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        selectedTransactionId = Convert.ToInt32(btn.Tag)

        Dim paperSize As New Printing.PaperSize("Custom", 250, 500)
        PrintDocument1.DefaultPageSettings.PaperSize = paperSize

        Dim printPreviewDialog As New PrintPreviewDialog()
        printPreviewDialog.Document = Me.PrintDocument1

        printPreviewDialog.ShowDialog()

    End Sub
    Private Sub BtnCetakPDF_Click(sender As Object, e As EventArgs)

        Dim btn As Button = CType(sender, Button)
        selectedTransactionId = Convert.ToInt32(btn.Tag)

        Dim printDialog As New PrintDialog()
        printDialog.Document = PrintDocument1

        If printDialog.ShowDialog() = DialogResult.OK Then

            Dim paperSize As New Printing.PaperSize("Custom", 250, 500)
            PrintDocument1.DefaultPageSettings.PaperSize = paperSize

            PrintDocument1.Print()
        End If
    End Sub


    Private Sub BtnKembali_Click(sender As Object, e As EventArgs) Handles BtnKembali.Click
        Dim formDataGame As New data_game(userId)
        formDataGame.Show()
        Me.Close()
    End Sub

End Class
