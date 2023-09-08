Imports System.Drawing.Drawing2D
Imports Snake.SnakeGameUtils

Public Class SnakeForm
    Dim Game As New SnakeGame(16, 16)

    Private Sub SnakeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        box.SendToBack()
        numericWidth.Minimum = 4
        numericHeight.Minimum = 4
        numericWidth.Maximum = 32
        numericHeight.Maximum = 32
        numericWidth.Value = 16
        numericHeight.Value = 16
        UpdateSize()
        SetMenuVisible(True)
    End Sub

    Private Const CellMargin As Single = 0.1

    Private Function AddNeighborCellRectangle(cell As Position, otherCell As Position, rect As Rectangle, rects As List(Of Rectangle)) As List(Of Rectangle)
        Dim marg As Single = CellMargin * renderScale

        If otherCell + Position.FromDirection(Direction.Right) = cell Then
            rects.Add(New Rectangle(
                rect.X - marg - renderScale * 0.25, rect.Y,
                renderScale * 0.75, rect.Height
            ))
        ElseIf otherCell + Position.FromDirection(Direction.Down) = cell Then
            rects.Add(New Rectangle(
                rect.X, rect.Y - marg - renderScale * 0.25,
                rect.Width, renderScale * 0.75
            ))
        ElseIf otherCell + Position.FromDirection(Direction.Left) = cell Then
            rects.Add(New Rectangle(
                rect.X - marg + renderScale * 0.5, rect.Y,
                renderScale * 0.75, rect.Height
            ))
        ElseIf otherCell + Position.FromDirection(Direction.Up) = cell Then
            rects.Add(New Rectangle(
                rect.X, rect.Y - marg + renderScale * 0.5,
                rect.Width, renderScale * 0.75
            ))
        End If

        Return rects
    End Function

    Private Function AddNeighborEndCellRectangle(cell As Position, otherCell As Position, rect As Rectangle, rects As List(Of Rectangle)) As List(Of Rectangle)
        If otherCell + Position.FromDirection(Direction.Right) = cell Then
            rects.Add(New Rectangle(
                rect.X, rect.Y,
                rect.Width * 0.5, rect.Height
            ))
        ElseIf otherCell + Position.FromDirection(Direction.Down) = cell Then
            rects.Add(New Rectangle(
                rect.X, rect.Y,
                rect.Width, rect.Height * 0.5
            ))
        ElseIf otherCell + Position.FromDirection(Direction.Left) = cell Then
            rects.Add(New Rectangle(
                rect.Width * 0.5 + rect.X, rect.Y,
                rect.Width * 0.5, rect.Height
            ))
        ElseIf otherCell + Position.FromDirection(Direction.Up) = cell Then
            rects.Add(New Rectangle(
                rect.X, rect.Height * 0.5 + rect.Y,
                rect.Width, rect.Height * 0.5
            ))
        End If

        Return rects
    End Function

    Private renderScale As Single

    Private bufferGraphics As Graphics
    Private bufferBitmap As Bitmap
    Private boxGraphics As Graphics

    Private Sub box_SizeChanged(sender As Object, e As EventArgs) Handles box.SizeChanged
        bufferGraphics?.Dispose()
        bufferBitmap?.Dispose()
        boxGraphics?.Dispose()
        bufferGraphics = Nothing
        bufferBitmap = Nothing
        boxGraphics = Nothing
    End Sub

    Private snakeColor As New SolidBrush(ColorTranslator.FromHtml("#a6e3a1"))
    Private appleColor As New SolidBrush(ColorTranslator.FromHtml("#f38ba8"))
    Private snakeDeadColor As SolidBrush = appleColor

    Private appleRoundedRadius As Single = 1
    Private snakeRoundedRadius As Single = 0.5

    Private Sub box_Paint(sender As Object, e As PaintEventArgs) Handles box.Paint
        Dim marg As Single = CellMargin * renderScale

        If bufferBitmap Is Nothing Then
            bufferBitmap = New Bitmap(box.ClientSize.Width, box.ClientSize.Height)
        Else
            boxGraphics.DrawImage(bufferBitmap, 0, 0)
        End If
        If bufferGraphics Is Nothing Then
            bufferGraphics = Graphics.FromImage(bufferBitmap)
        End If
        If boxGraphics Is Nothing Then
            boxGraphics = box.CreateGraphics()
        End If

        bufferGraphics.SmoothingMode = SmoothingMode.AntiAlias
        bufferGraphics.Clear(Color.Transparent)

        Dim appleRect As New Rectangle(
            Math.Floor(Game.ApplePosition.X * renderScale + marg),
            Math.Floor(Game.ApplePosition.Y * renderScale + marg),
            Math.Ceiling((1 - (CellMargin * 2)) * renderScale),
            Math.Ceiling((1 - (CellMargin * 2)) * renderScale)
        )
        bufferGraphics.FillRoundedRectangle(appleColor, appleRect, renderScale * appleRoundedRadius)

        For i = 0 To Game.SnakeCells.Count - 1
            Dim cell As Position = Game.SnakeCells(i)
            Dim initialRect As New Rectangle(
                    marg + cell.X * renderScale,
                    marg + cell.Y * renderScale,
                    renderScale - (CellMargin * 2) * renderScale,
                    renderScale - (CellMargin * 2) * renderScale
                )
            Dim rects As New List(Of Rectangle)

            If i > 0 Then
                rects = AddNeighborCellRectangle(cell, Game.SnakeCells(i - 1), initialRect, rects)
            ElseIf Game.SnakeCells.Count > 1 Then
                rects = AddNeighborEndCellRectangle(cell, Game.SnakeCells(1), initialRect, rects)
            End If

            If i < Game.SnakeCells.Count - 1 Then
                rects = AddNeighborCellRectangle(cell, Game.SnakeCells(i + 1), initialRect, rects)
            ElseIf Game.SnakeCells.Count > 1 Then
                rects = AddNeighborEndCellRectangle(cell, Game.SnakeCells(i - 1), initialRect, rects)
            End If

            For Each rect In rects
                bufferGraphics.FillRectangle(snakeColor, rect)
            Next

            bufferGraphics.FillRoundedRectangle(snakeColor, initialRect, renderScale * snakeRoundedRadius)

            If i = Game.SnakeCells.Count - 1 And Not Game.SnakeAlive And Not Game.SnakeWin Then
                bufferGraphics.FillRoundedRectangle(snakeDeadColor, initialRect, renderScale * snakeRoundedRadius)
            End If
        Next

        boxGraphics.Clear(box.BackColor)
        boxGraphics.DrawImage(bufferBitmap, 0, 0)
    End Sub

    Private Sub UpdateButtons()
        Dim winWidth As Single = ClientSize.Width
        Dim winHeight As Single = ClientSize.Height

        startButton.Left = (winWidth - startButton.Width) * 0.5
        startButton.Top = (winHeight - startButton.Height) * 0.5

        resumeButton.Left = (winWidth - resumeButton.Width) * 0.5
        resumeButton.Top = (winHeight - resumeButton.Height) * 0.5

        gameOverText.Left = (winWidth - gameOverText.Width) * 0.5
        gameOverText.Top = startButton.Top - (Height * 0.1) - 20

        pausedText.Left = (winWidth - pausedText.Width) * 0.5
        pausedText.Top = gameOverText.Top

        winText.Left = (winWidth - winText.Width) * 0.5
        winText.Top = gameOverText.Top

        mainPanel.Left = (winWidth - mainPanel.Width) * 0.5
        mainPanel.Top = startButton.Top + startButton.Height + (Height * 0.05)

        If Paused Then
            startButton.Top += resumeButton.Height + (Height * 0.05)
        End If
    End Sub

    Private Sub UpdateBox()
        UpdateButtons()

        Dim winWidth As Single = ClientSize.Width
        Dim winHeight As Single = ClientSize.Height

        Dim gameWidth As Single = Game.GameSize.X
        Dim gameHeight As Single = Game.GameSize.Y

        If gameWidth / gameHeight > winWidth / winHeight Then
            renderScale = winWidth / gameWidth
            box.Left = 0
            box.Top = winHeight * 0.5 - (gameHeight * renderScale * 0.5)
            box.Width = winWidth
            box.Height = renderScale * gameHeight
        Else
            renderScale = winHeight / gameHeight
            box.Left = winWidth * 0.5 - (gameWidth * renderScale * 0.5)
            box.Top = 0
            box.Width = renderScale * gameWidth
            box.Height = winHeight
        End If

        box.Invalidate()
    End Sub

    Private Sub SnakeForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        UpdateBox()
    End Sub

    Private timerTick As Boolean = False

    Private Sub gameTimer_Tick(sender As Object, e As EventArgs) Handles gameTimer.Tick
        If timerTick Then
            Return
        End If
        timerTick = True

        If Game.SnakeAlive Then
            If changeDirectionQueue.Count > 0 Then
                Game.SnakeDirection = changeDirectionQueue(0)
                changeDirectionQueue.RemoveAt(0)
            End If
        End If

        Game.Advance()
        box.Invalidate()

        UpdateScore()

        If Not Game.SnakeAlive Then
            StopGame()
        End If

        timerTick = False
    End Sub

    Private Sub UpdateScore()
        scoreText.Text = "Score: " & (Game.SnakeLength - 1)
    End Sub

    Private changeDirectionQueue As New List(Of Direction)

    Private Paused As Boolean = False

    Private Sub TogglePause()
        If Not Game.SnakeAlive Or Not firstStarted Then
            Paused = False
            StartGame()
            SetMenuVisible(False)
            Return
        End If

        If Paused Then
            changeDirectionQueue.Clear()
            Paused = False
            gameTimer.Start()
            SetMenuVisible(False)
            pauseButton.Focus()
        Else
            changeDirectionQueue.Clear()
            Paused = True
            gameTimer.Stop()
            timerTick = False
            SetMenuVisible(False)
            resumeButton.Focus()
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If HandleKeyDown(keyData) Then
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub SnakeForm_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles MyBase.PreviewKeyDown
        If HandleKeyDown(e.KeyCode) Then
            e.IsInputKey = False
        End If
    End Sub

    Private Function HandleKeyDown(keyCode As Keys) As Boolean
        Dim dir As Direction = Direction.None
        Select Case keyCode
            Case Keys.Escape, Keys.P, Keys.Enter, Keys.Space
                TogglePause()
                Return True
            Case Keys.A, Keys.F, Keys.J, Keys.Left
                dir = Direction.Left
            Case Keys.W, Keys.T, Keys.I, Keys.Up
                dir = Direction.Up
            Case Keys.D, Keys.H, Keys.L, Keys.Right
                dir = Direction.Right
            Case Keys.S, Keys.G, Keys.K, Keys.Down
                dir = Direction.Down
                Exit Select
            Case Else
                Return False
        End Select

        Return HandleMove(dir)
    End Function

    Private Function HandleMove(dir As Direction) As Boolean
        If dir = Direction.None Or algorithm Then
            Return False
        End If

        If Paused Or Not Game.SnakeAlive Or Not firstStarted Then
            TogglePause()
        End If

        If changeDirectionQueue.Count < 3 Then
            If Game.SnakeLength > 1 Then
                ' don't allow turning back, or turning in the current direction
                If changeDirectionQueue.Count = 0 Then
                    If IsDirectionSameAxis(dir, Game.SnakeDirection) Then
                        GoTo skipAddQueue
                    End If
                Else
                    If IsDirectionSameAxis(dir, changeDirectionQueue.Last()) Then
                        GoTo skipAddQueue
                    End If
                End If
            End If

            changeDirectionQueue.Add(dir)
skipAddQueue:
        End If
        Return True
    End Function

    Private firstStarted As Boolean = False
    Private algorithm As Boolean = False

    Private Sub SetMenuVisible(menu As Boolean)
        resumeButton.Visible = Paused
        resumeButton.Enabled = Paused
        pausedText.Visible = Paused
        startButton.Visible = Paused Or menu
        startButton.Enabled = startButton.Visible
        gameOverText.Visible = menu And Not Game.SnakeWin And firstStarted
        winText.Visible = menu And Game.SnakeWin And firstStarted
        mainPanel.Visible = menu
        numericWidth.Enabled = menu
        numericHeight.Enabled = menu
        pauseButton.Visible = Not menu And Not Paused
        pauseButton.Enabled = pauseButton.Visible
        scoreText.Visible = Not menu
        algorithmCheckbox.Visible = menu
        algorithmCheckbox.Enabled = menu

        If menu Then
            startButton.Focus()
        Else
            UpdateScore()
        End If
        UpdateButtons()
    End Sub

    Private Sub StopGame()
        changeDirectionQueue.Clear()
        gameTimer.Stop()
        timerTick = False
        SetMenuVisible(True)
        Paused = False
    End Sub

    Private Sub UpdateSize()
        Game.SetSize(numericWidth.Value, numericHeight.Value)
        UpdateBox()
    End Sub

    Private Sub StartGame()
        changeDirectionQueue.Clear()

        Paused = False

        UpdateBox()
        If firstStarted Then
            Game.Restart()
            Game.MoveApple()
        End If

        firstStarted = True

        gameTimer.Start()

        SetMenuVisible(False)

        box.Invalidate()

        startButton.Text = "Restart"

        pauseButton.Focus()
    End Sub

    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        StartGame()
    End Sub

    Private Sub numericWidth_ValueChanged(sender As Object, e As EventArgs) Handles numericWidth.ValueChanged, numericHeight.ValueChanged
        UpdateSize()
    End Sub

    Private Sub resumeButton_Click(sender As Object, e As EventArgs) Handles resumeButton.Click
        TogglePause()
    End Sub

    Private Sub pauseButton_Click(sender As Object, e As EventArgs) Handles pauseButton.Click
        TogglePause()
    End Sub

    Private Sub resumeButton_KeyDown(sender As Object, e As KeyEventArgs) Handles resumeButton.KeyDown
        HandleKeyDown(e.KeyCode)
        e.SuppressKeyPress = True
    End Sub

    Private Sub startButton_KeyDown(sender As Object, e As KeyEventArgs) Handles startButton.KeyDown
        HandleKeyDown(e.KeyCode)
        e.SuppressKeyPress = True
    End Sub

    Private Sub pauseButton_KeyDown(sender As Object, e As KeyEventArgs) Handles pauseButton.KeyDown
        HandleKeyDown(e.KeyCode)
        e.SuppressKeyPress = True
    End Sub

    Private Sub algorithmCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles algorithmCheckbox.CheckedChanged
        algorithm = True
    End Sub
End Class
