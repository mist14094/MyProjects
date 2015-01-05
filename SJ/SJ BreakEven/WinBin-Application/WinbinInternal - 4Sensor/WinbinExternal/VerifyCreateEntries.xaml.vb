Public Class VerifyCreateEntries

    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Public Sub New(availpts As Integer, availentries As Integer, freq As Integer, logo As BitmapImage)

        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 2, 0)
        dispatcherTimer.Start()
        ' This call is required by the designer.
        InitializeComponent()

        Dim totalspend As Integer = availentries * freq

        txtPoints.Text = availpts.ToString

        txtMessage.Text = "You can redeem " + totalspend.ToString + " points right now and create " + availentries.ToString + " entries in the WinBin!" + Environment.NewLine + Environment.NewLine + "Would you like to redeem your points now?"

        Image1.Source = logo
    End Sub

    Public Sub Dispose()
        txtPoints.Text = Nothing
        txtMessage.Text = Nothing
        Image1.Source = Nothing
    End Sub
    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            dispatcherTimer.Stop()
            Me.DialogResult = False
            Me.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub yes_Click(sender As Object, e As EventArgs) Handles btnYes.MouseLeftButtonUp
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub no_Click(sender As Object, e As EventArgs) Handles btnNo.MouseLeftButtonUp
        Me.DialogResult = False
        Me.Close()
    End Sub
End Class
