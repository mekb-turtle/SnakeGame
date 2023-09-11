Imports System.Numerics
Imports Snake.SnakeGameUtils

Public Class SnakeGame
    Public SnakeCells As New List(Of Position)
    Public SnakeDirection As Direction = Direction.None
    Public SnakeLength As Integer = 1
    Public SnakePosition As New Position
    Public GameSize As Position
    Public SnakeAlive As Boolean = True
    Public SnakeWin As Boolean = False
    Public ApplePosition As Position
    Public SnakeSteps As Integer

    Public rng As New Random(Guid.NewGuid().GetHashCode())

    Public Sub New(X As Integer, Y As Integer)
        GameSize = New Position(X, Y)
        Restart()
        MoveApple()
    End Sub

    Public Sub SetSize(X As Integer, Y As Integer)
        GameSize.X = X
        GameSize.Y = Y
        Restart()
        MoveApple()
    End Sub

    Private Sub CenterSnakePosition()
        SnakePosition.X = (GameSize.X - 1) / 2
        SnakePosition.Y = (GameSize.Y - 1) / 2
    End Sub

    Public Sub Restart()
        SnakeDirection = Direction.None
        CenterSnakePosition()
        SnakeLength = 1
        SnakeAlive = True
        SnakeWin = False
        SnakeCells.Clear()
        SnakeCells.Add(SnakePosition)
        SnakeSteps = 0
    End Sub

    Public Function MoveApple() As Boolean
        Dim Cells As New List(Of Position)
        For x = 0 To GameSize.X - 1
            For y = 0 To GameSize.Y - 1
                For Each cell In SnakeCells
                    If cell.X = x And cell.Y = y Then
                        GoTo skipAddRandomPosition
                    End If
                Next
                Cells.Add(New Position(x, y))
skipAddRandomPosition:
            Next
        Next

        If Cells.Count = 0 Then
            Return False
        End If

        ApplePosition = Cells(rng.Next(Cells.Count))
        Return True
    End Function

    Public Function OutOfBounds(pos As Position) As Boolean
        Return pos.X < 0 Or pos.Y < 0 Or pos.X >= GameSize.X Or pos.Y >= GameSize.Y
    End Function

    Public Sub Advance()
        If Not SnakeAlive Then
            Return
        End If

        If SnakeDirection = Direction.None Then
            Return
        End If

        SnakeSteps += 1

        SnakePosition = SnakePosition.Add(SnakeDirection)

        If OutOfBounds(SnakePosition) Then
            SnakeAlive = False
            Return
        End If

        Dim crash As Boolean = False
        Dim apple As Boolean = False
        For i = If(SnakeLength > SnakeCells.Count, 0, 1) To SnakeCells.Count - 1
            If SnakeCells(i).Equals(SnakePosition) Then
                crash = True
            End If
        Next

        SnakeCells.Add(SnakePosition)

        If SnakePosition.Equals(ApplePosition) And Not crash Then
            SnakeLength += 1
            apple = True
        End If

        While SnakeCells.Count > SnakeLength
            SnakeCells.RemoveAt(0)
        End While

        If apple Then
            If Not MoveApple() Then
                SnakeAlive = False
                SnakeWin = True
            End If
        End If

        If crash Then
            SnakeAlive = False
        End If
    End Sub
End Class
