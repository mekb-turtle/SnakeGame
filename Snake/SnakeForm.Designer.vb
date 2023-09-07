<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SnakeForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SnakeForm))
        Me.box = New System.Windows.Forms.Panel()
        Me.gameTimer = New System.Windows.Forms.Timer(Me.components)
        Me.startButton = New System.Windows.Forms.Button()
        Me.gameOverText = New System.Windows.Forms.Label()
        Me.winText = New System.Windows.Forms.Label()
        Me.mainPanel = New System.Windows.Forms.Panel()
        Me.numericHeight = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numericWidth = New System.Windows.Forms.NumericUpDown()
        Me.resumeButton = New System.Windows.Forms.Button()
        Me.pausedText = New System.Windows.Forms.Label()
        Me.pauseButton = New System.Windows.Forms.Button()
        Me.mainPanel.SuspendLayout()
        CType(Me.numericHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numericWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'box
        '
        Me.box.BackColor = System.Drawing.Color.Black
        Me.box.Location = New System.Drawing.Point(69, 47)
        Me.box.Margin = New System.Windows.Forms.Padding(2)
        Me.box.Name = "box"
        Me.box.Size = New System.Drawing.Size(133, 65)
        Me.box.TabIndex = 0
        '
        'gameTimer
        '
        Me.gameTimer.Interval = 300
        '
        'startButton
        '
        Me.startButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.startButton.ForeColor = System.Drawing.Color.White
        Me.startButton.Location = New System.Drawing.Point(232, 125)
        Me.startButton.Margin = New System.Windows.Forms.Padding(2)
        Me.startButton.Name = "startButton"
        Me.startButton.Size = New System.Drawing.Size(75, 31)
        Me.startButton.TabIndex = 1
        Me.startButton.Text = "Start"
        Me.startButton.UseVisualStyleBackColor = False
        '
        'gameOverText
        '
        Me.gameOverText.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.gameOverText.ForeColor = System.Drawing.Color.Red
        Me.gameOverText.Location = New System.Drawing.Point(232, 97)
        Me.gameOverText.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.gameOverText.Name = "gameOverText"
        Me.gameOverText.Size = New System.Drawing.Size(75, 15)
        Me.gameOverText.TabIndex = 2
        Me.gameOverText.Text = "Game Over!"
        Me.gameOverText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.gameOverText.Visible = False
        '
        'winText
        '
        Me.winText.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.winText.ForeColor = System.Drawing.Color.Gold
        Me.winText.Location = New System.Drawing.Point(232, 61)
        Me.winText.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.winText.Name = "winText"
        Me.winText.Size = New System.Drawing.Size(75, 15)
        Me.winText.TabIndex = 3
        Me.winText.Text = "You Win!"
        Me.winText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.winText.Visible = False
        '
        'mainPanel
        '
        Me.mainPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.mainPanel.Controls.Add(Me.numericHeight)
        Me.mainPanel.Controls.Add(Me.Label2)
        Me.mainPanel.Controls.Add(Me.Label1)
        Me.mainPanel.Controls.Add(Me.numericWidth)
        Me.mainPanel.ForeColor = System.Drawing.Color.Transparent
        Me.mainPanel.Location = New System.Drawing.Point(209, 170)
        Me.mainPanel.Margin = New System.Windows.Forms.Padding(2)
        Me.mainPanel.Name = "mainPanel"
        Me.mainPanel.Size = New System.Drawing.Size(131, 41)
        Me.mainPanel.TabIndex = 4
        '
        'numericHeight
        '
        Me.numericHeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.numericHeight.ForeColor = System.Drawing.Color.White
        Me.numericHeight.Location = New System.Drawing.Point(91, 12)
        Me.numericHeight.Margin = New System.Windows.Forms.Padding(2)
        Me.numericHeight.Name = "numericHeight"
        Me.numericHeight.Size = New System.Drawing.Size(37, 20)
        Me.numericHeight.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(77, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "x"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(2, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Size:"
        '
        'numericWidth
        '
        Me.numericWidth.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.numericWidth.ForeColor = System.Drawing.Color.White
        Me.numericWidth.Location = New System.Drawing.Point(35, 12)
        Me.numericWidth.Margin = New System.Windows.Forms.Padding(2)
        Me.numericWidth.Name = "numericWidth"
        Me.numericWidth.Size = New System.Drawing.Size(37, 20)
        Me.numericWidth.TabIndex = 2
        '
        'resumeButton
        '
        Me.resumeButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.resumeButton.ForeColor = System.Drawing.Color.White
        Me.resumeButton.Location = New System.Drawing.Point(331, 125)
        Me.resumeButton.Margin = New System.Windows.Forms.Padding(2)
        Me.resumeButton.Name = "resumeButton"
        Me.resumeButton.Size = New System.Drawing.Size(75, 31)
        Me.resumeButton.TabIndex = 5
        Me.resumeButton.Text = "Resume"
        Me.resumeButton.UseVisualStyleBackColor = False
        Me.resumeButton.Visible = False
        '
        'pausedText
        '
        Me.pausedText.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pausedText.ForeColor = System.Drawing.Color.White
        Me.pausedText.Location = New System.Drawing.Point(331, 97)
        Me.pausedText.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.pausedText.Name = "pausedText"
        Me.pausedText.Size = New System.Drawing.Size(75, 15)
        Me.pausedText.TabIndex = 6
        Me.pausedText.Text = "Paused"
        Me.pausedText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.pausedText.Visible = False
        '
        'pauseButton
        '
        Me.pauseButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pauseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.pauseButton.ForeColor = System.Drawing.Color.White
        Me.pauseButton.Location = New System.Drawing.Point(436, 8)
        Me.pauseButton.Margin = New System.Windows.Forms.Padding(2)
        Me.pauseButton.Name = "pauseButton"
        Me.pauseButton.Size = New System.Drawing.Size(75, 31)
        Me.pauseButton.TabIndex = 7
        Me.pauseButton.Text = "Pause"
        Me.pauseButton.UseVisualStyleBackColor = False
        Me.pauseButton.Visible = False
        '
        'SnakeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(519, 256)
        Me.Controls.Add(Me.pauseButton)
        Me.Controls.Add(Me.resumeButton)
        Me.Controls.Add(Me.pausedText)
        Me.Controls.Add(Me.mainPanel)
        Me.Controls.Add(Me.startButton)
        Me.Controls.Add(Me.winText)
        Me.Controls.Add(Me.gameOverText)
        Me.Controls.Add(Me.box)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MinimumSize = New System.Drawing.Size(270, 170)
        Me.Name = "SnakeForm"
        Me.Text = "Snake"
        Me.mainPanel.ResumeLayout(False)
        Me.mainPanel.PerformLayout()
        CType(Me.numericHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numericWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents box As Panel
    Friend WithEvents gameTimer As Timer
    Friend WithEvents startButton As Button
    Friend WithEvents gameOverText As Label
    Friend WithEvents winText As Label
    Friend WithEvents mainPanel As Panel
    Friend WithEvents numericWidth As NumericUpDown
    Friend WithEvents numericHeight As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents resumeButton As Button
    Friend WithEvents pausedText As Label
    Friend WithEvents pauseButton As Button
End Class
