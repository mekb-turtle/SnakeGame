Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices

Public Module RenderUtils
    Private Function RoundedRect(bounds As Rectangle, diameter As Integer) As GraphicsPath
        diameter = Math.Min(Math.Min(bounds.Width, bounds.Height), diameter)

        Dim size_ As New Size(diameter, diameter)
        Dim arc As New Rectangle(bounds.Location, size_)
        Dim path As New GraphicsPath()

        If diameter = 0 Then
            path.AddRectangle(bounds)
            Return path
        End If

        ' top left arc
        path.AddArc(arc, 180, 90)

        ' top right arc
        arc.X = bounds.Right - diameter
        path.AddArc(arc, 270, 90)

        ' bottom right arc
        arc.Y = bounds.Bottom - diameter
        path.AddArc(arc, 0, 90)

        ' bottom left arc
        arc.X = bounds.Left
        path.AddArc(arc, 90, 90)

        path.CloseFigure()
        Return path
    End Function

    <Extension()>
    Public Sub DrawRoundedRectangle(graphics_ As Graphics, pen_ As Pen, bounds As Rectangle, cornerRadius As Integer)
        If graphics_ Is Nothing Then
            Throw New ArgumentNullException("graphics")
        End If
        If pen_ Is Nothing Then
            Throw New ArgumentNullException("pen")
        End If

        Using path As GraphicsPath = RoundedRect(bounds, cornerRadius)
            graphics_.DrawPath(pen_, path)
        End Using
    End Sub

    <Extension()>
    Public Sub FillRoundedRectangle(graphics_ As Graphics, brush_ As Brush, bounds As Rectangle, cornerRadius As Integer)
        If graphics_ Is Nothing Then
            Throw New ArgumentNullException("graphics")
        End If
        If brush_ Is Nothing Then
            Throw New ArgumentNullException("pen")
        End If

        Using path As GraphicsPath = RoundedRect(bounds, cornerRadius)
            graphics_.FillPath(brush_, path)
        End Using
    End Sub
End Module
