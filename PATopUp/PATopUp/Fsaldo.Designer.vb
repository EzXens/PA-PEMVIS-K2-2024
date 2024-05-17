<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Fsaldo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Fsaldo))
        Me.btncon = New System.Windows.Forms.Button()
        Me.txtsaldo = New System.Windows.Forms.TextBox()
        Me.lblSaldo = New System.Windows.Forms.Label()
        Me.lblNamaUser = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btncon
        '
        Me.btncon.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btncon.Location = New System.Drawing.Point(320, 183)
        Me.btncon.Margin = New System.Windows.Forms.Padding(4)
        Me.btncon.Name = "btncon"
        Me.btncon.Size = New System.Drawing.Size(100, 32)
        Me.btncon.TabIndex = 22
        Me.btncon.Text = "OK"
        Me.btncon.UseVisualStyleBackColor = False
        '
        'txtsaldo
        '
        Me.txtsaldo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.25!)
        Me.txtsaldo.Location = New System.Drawing.Point(15, 183)
        Me.txtsaldo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtsaldo.Name = "txtsaldo"
        Me.txtsaldo.Size = New System.Drawing.Size(253, 31)
        Me.txtsaldo.TabIndex = 21
        '
        'lblSaldo
        '
        Me.lblSaldo.AutoSize = True
        Me.lblSaldo.Location = New System.Drawing.Point(127, 62)
        Me.lblSaldo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSaldo.Name = "lblSaldo"
        Me.lblSaldo.Size = New System.Drawing.Size(0, 17)
        Me.lblSaldo.TabIndex = 23
        '
        'lblNamaUser
        '
        Me.lblNamaUser.AutoSize = True
        Me.lblNamaUser.Font = New System.Drawing.Font("Poppins", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNamaUser.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblNamaUser.Location = New System.Drawing.Point(12, 143)
        Me.lblNamaUser.Name = "lblNamaUser"
        Me.lblNamaUser.Size = New System.Drawing.Size(255, 36)
        Me.lblNamaUser.TabIndex = 29
        Me.lblNamaUser.Text = "Silahkan Isi Saldo Anda"
        '
        'Fsaldo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 400)
        Me.Controls.Add(Me.lblNamaUser)
        Me.Controls.Add(Me.lblSaldo)
        Me.Controls.Add(Me.btncon)
        Me.Controls.Add(Me.txtsaldo)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Fsaldo"
        Me.Text = "Fsaldo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btncon As System.Windows.Forms.Button
    Friend WithEvents txtsaldo As System.Windows.Forms.TextBox
    Friend WithEvents lblSaldo As System.Windows.Forms.Label
    Friend WithEvents lblNamaUser As System.Windows.Forms.Label
End Class
