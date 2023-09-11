Public Module SnakeGameUtils
    Public Enum Direction
        None
        Left
        Up
        Right
        Down
    End Enum

    Public Function GetOppositeDirection(a As Direction) As Direction
        If a = Direction.Left Then Return Direction.Right
        If a = Direction.Up Then Return Direction.Down
        If a = Direction.Right Then Return Direction.Left
        If a = Direction.Down Then Return Direction.Up
        Return Direction.None
    End Function

    Public Function IsOppositeDirection(a As Direction, b As Direction) As Boolean
        If a = Direction.Left And b = Direction.Right Then
            Return True
        ElseIf a = Direction.Up And b = Direction.Down Then
            Return True
        ElseIf a = Direction.Right And b = Direction.Left Then
            Return True
        ElseIf a = Direction.Down And b = Direction.Up Then
            Return True
        End If
        Return False
    End Function

    Public Function IsDirectionSameAxis(a As Direction, b As Direction) As Boolean
        Return a = b Or IsOppositeDirection(a, b)
    End Function

    Public Class Position
        Implements IEquatable(Of Position)

        Public X As Integer = 0
        Public Y As Integer = 0

        Public Sub New(X As Integer, Y As Integer)
            Me.X = X
            Me.Y = Y
        End Sub

        Public Sub New(Position As Position)
            X = Position.X
            Y = Position.Y
        End Sub

        Public Sub New()

        End Sub

        Public Shared Function FromDirection(Dir As Direction)
            Select Case Dir
                Case Direction.Up
                    Return New Position(0, -1)
                Case Direction.Right
                    Return New Position(1, 0)
                Case Direction.Down
                    Return New Position(0, 1)
                Case Direction.Left
                    Return New Position(-1, 0)
            End Select
            Return New Position(0, 0)
        End Function

        ' operators do not work well with mono on Linux
        Public Function Add(B As Position) As Position
            Return New Position(X + B.X, Y + B.Y)
        End Function
        Public Function Subtract(B As Position) As Position
            Return New Position(X - B.X, Y - B.Y)
        End Function
        Public Function Multiply(B As Position) As Position
            Return New Position(X * B.X, Y * B.Y)
        End Function
        Public Function Divide(B As Position) As Position
            Return New Position(X / B.X, Y / B.Y)
        End Function

        Public Function Add(B As Direction) As Position
            Dim B_ As Position = FromDirection(B)
            Return New Position(X + B_.X, Y + B_.Y)
        End Function
        Public Function Subtract(B As Direction) As Position
            Dim B_ As Position = FromDirection(B)
            Return New Position(X - B_.X, Y - B_.Y)
        End Function

        Public Function Add(B As Integer) As Position
            Return New Position(X + B, Y + B)
        End Function
        Public Function Subtract(B As Integer) As Position
            Return New Position(X - B, Y - B)
        End Function
        Public Function Multiply(B As Integer) As Position
            Return New Position(X * B, Y * B)
        End Function
        Public Function Divide(B As Integer) As Position
            Return New Position(X / B, Y / B)
        End Function

        Public Function Equals(other As Position) As Boolean Implements IEquatable(Of Position).Equals
            If other Is Nothing Then
                Return False
            End If

            Return X = other.X And Y = other.Y
        End Function
    End Class
End Module
