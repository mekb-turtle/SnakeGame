Public Class GraphicsControl
    Inherits PictureBox

    Public Sub New()
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub
End Class
