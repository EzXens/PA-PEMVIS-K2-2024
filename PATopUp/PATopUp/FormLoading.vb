Public Class FormLoading

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        ProgressBar1.Value += 1
        Label2.Text = ProgressBar1.Value & " %"
        If ProgressBar1.Value = ProgressBar1.Maximum Then
            Timer1.Enabled = False
            Flogin.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub FormLoading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
End Class