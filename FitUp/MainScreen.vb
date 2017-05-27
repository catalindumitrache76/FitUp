Public Class MainScreen
    Private Sub MainScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MainScreen_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Dim myControl As Control
        myControl = sender

        SplitContainer1.SplitterDistance = myControl.Size.Width / 3
        SplitContainer2.SplitterDistance = myControl.Size.Width / 3

    End Sub

End Class
