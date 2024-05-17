<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Ftransaksi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Ftransaksi))
        Me.lblgame = New System.Windows.Forms.Label()
        Me.lblmatauang = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblharga1 = New System.Windows.Forms.Label()
        Me.txtuid = New System.Windows.Forms.TextBox()
        Me.UID = New System.Windows.Forms.Label()
        Me.btnkon = New System.Windows.Forms.Button()
        Me.lbljumlah = New System.Windows.Forms.Label()
        Me.lblharga = New System.Windows.Forms.Label()
        Me.dtpTanggalTransaksi = New System.Windows.Forms.DateTimePicker()
        Me.btnKembali = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblgame
        '
        Me.lblgame.AutoSize = True
        Me.lblgame.Location = New System.Drawing.Point(333, 197)
        Me.lblgame.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblgame.Name = "lblgame"
        Me.lblgame.Size = New System.Drawing.Size(0, 17)
        Me.lblgame.TabIndex = 0
        '
        'lblmatauang
        '
        Me.lblmatauang.AutoSize = True
        Me.lblmatauang.Location = New System.Drawing.Point(199, 289)
        Me.lblmatauang.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblmatauang.Name = "lblmatauang"
        Me.lblmatauang.Size = New System.Drawing.Size(0, 17)
        Me.lblmatauang.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(199, 330)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Jumlah"
        '
        'lblharga1
        '
        Me.lblharga1.AutoSize = True
        Me.lblharga1.ForeColor = System.Drawing.Color.White
        Me.lblharga1.Location = New System.Drawing.Point(199, 368)
        Me.lblharga1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblharga1.Name = "lblharga1"
        Me.lblharga1.Size = New System.Drawing.Size(47, 17)
        Me.lblharga1.TabIndex = 5
        Me.lblharga1.Text = "Harga"
        '
        'txtuid
        '
        Me.txtuid.Location = New System.Drawing.Point(273, 398)
        Me.txtuid.Margin = New System.Windows.Forms.Padding(4)
        Me.txtuid.Name = "txtuid"
        Me.txtuid.Size = New System.Drawing.Size(132, 22)
        Me.txtuid.TabIndex = 6
        '
        'UID
        '
        Me.UID.AutoSize = True
        Me.UID.ForeColor = System.Drawing.Color.White
        Me.UID.Location = New System.Drawing.Point(199, 401)
        Me.UID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.UID.Name = "UID"
        Me.UID.Size = New System.Drawing.Size(31, 17)
        Me.UID.TabIndex = 7
        Me.UID.Text = "UID"
        '
        'btnkon
        '
        Me.btnkon.Location = New System.Drawing.Point(307, 681)
        Me.btnkon.Margin = New System.Windows.Forms.Padding(4)
        Me.btnkon.Name = "btnkon"
        Me.btnkon.Size = New System.Drawing.Size(100, 28)
        Me.btnkon.TabIndex = 8
        Me.btnkon.Text = "Konfirmasi"
        Me.btnkon.UseVisualStyleBackColor = True
        '
        'lbljumlah
        '
        Me.lbljumlah.AutoSize = True
        Me.lbljumlah.Location = New System.Drawing.Point(269, 330)
        Me.lbljumlah.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbljumlah.Name = "lbljumlah"
        Me.lbljumlah.Size = New System.Drawing.Size(51, 17)
        Me.lbljumlah.TabIndex = 10
        Me.lbljumlah.Text = "Label6"
        '
        'lblharga
        '
        Me.lblharga.AutoSize = True
        Me.lblharga.Location = New System.Drawing.Point(269, 368)
        Me.lblharga.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblharga.Name = "lblharga"
        Me.lblharga.Size = New System.Drawing.Size(51, 17)
        Me.lblharga.TabIndex = 11
        Me.lblharga.Text = "Label6"
        '
        'dtpTanggalTransaksi
        '
        Me.dtpTanggalTransaksi.Enabled = False
        Me.dtpTanggalTransaksi.Location = New System.Drawing.Point(203, 471)
        Me.dtpTanggalTransaksi.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpTanggalTransaksi.Name = "dtpTanggalTransaksi"
        Me.dtpTanggalTransaksi.Size = New System.Drawing.Size(265, 22)
        Me.dtpTanggalTransaksi.TabIndex = 12
        '
        'btnKembali
        '
        Me.btnKembali.Location = New System.Drawing.Point(33, 16)
        Me.btnKembali.Margin = New System.Windows.Forms.Padding(4)
        Me.btnKembali.Name = "btnKembali"
        Me.btnKembali.Size = New System.Drawing.Size(100, 28)
        Me.btnKembali.TabIndex = 13
        Me.btnKembali.Text = "balek"
        Me.btnKembali.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(16, 287)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(139, 130)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(273, 36)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(160, 158)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Ftransaksi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 750)
        Me.Controls.Add(Me.btnKembali)
        Me.Controls.Add(Me.dtpTanggalTransaksi)
        Me.Controls.Add(Me.lblharga)
        Me.Controls.Add(Me.lbljumlah)
        Me.Controls.Add(Me.btnkon)
        Me.Controls.Add(Me.UID)
        Me.Controls.Add(Me.txtuid)
        Me.Controls.Add(Me.lblharga1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblmatauang)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblgame)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(37, Byte), Integer), CType(CType(53, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Ftransaksi"
        Me.Text = "Ftransaksi"
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblgame As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents lblmatauang As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblharga1 As System.Windows.Forms.Label
    Friend WithEvents txtuid As System.Windows.Forms.TextBox
    Friend WithEvents UID As System.Windows.Forms.Label
    Friend WithEvents btnkon As System.Windows.Forms.Button
    Friend WithEvents lbljumlah As System.Windows.Forms.Label
    Friend WithEvents lblharga As System.Windows.Forms.Label
    Friend WithEvents dtpTanggalTransaksi As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnKembali As System.Windows.Forms.Button
End Class
