Public Class SnakeGameUtils
    Public Enum Direction
        None
        Left
        Up
        Right
        Down
    End Enum

    Public Shared Function IsOppositeDirection(a As Direction, b As Direction) As Boolean
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

    Public Class Position
        Public X As Integer = 0
        Public Y As Integer = 0

        Public Sub New(X As Integer, Y As Integer)
            Me.X = X
            Me.Y = Y
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

        Public Shared Operator +(A As Position, B As Position)
            Return New Position(A.X + B.X, A.Y + B.Y)
        End Operator
        Public Shared Operator -(A As Position, B As Position)
            Return New Position(A.X - B.X, A.Y - B.Y)
        End Operator
        Public Shared Operator *(A As Position, B As Position)
            Return New Position(A.X * B.X, A.Y * B.Y)
        End Operator
        Public Shared Operator /(A As Position, B As Position)
            Return New Position(A.X / B.X, A.Y / B.Y)
        End Operator

        Public Shared Operator +(A As Position, B As Integer)
            Return New Position(A.X + B, A.Y + B)
        End Operator
        Public Shared Operator -(A As Position, B As Integer)
            Return New Position(A.X - B, A.Y - B)
        End Operator
        Public Shared Operator *(A As Position, B As Integer)
            Return New Position(A.X * B, A.Y * B)
        End Operator
        Public Shared Operator /(A As Position, B As Integer)
            Return New Position(A.X / B, A.Y / B)
        End Operator

        Public Shared Operator =(A As Position, B As Position)
            Return A.X = B.X And A.Y = B.Y
        End Operator
        Public Shared Operator <>(A As Position, B As Position)
            Return A.X <> B.X Or A.Y <> B.Y
        End Operator
    End Class
End Class
