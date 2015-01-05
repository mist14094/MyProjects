Public Class VerifyLicenseInfo

    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()
    Public Sub New(ByVal customer As Customer, ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 2, 0)
        dispatcherTimer.Start()

        InitializeComponent()
        Image1.Source = logo

        custInfo.Text = customer.FirstName + " " + customer.MidName + " " + customer.LastName + " " + customer.Suffix + Environment.NewLine +
                        customer.Address1 + Environment.NewLine +
                        customer.Address2 + Environment.NewLine +
                        customer.City + ", " + customer.State + " " + customer.Zip

    End Sub

    Protected Sub Dispose()
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
        Image1.Source = Nothing
        custInfo.Text = Nothing
    End Sub

    Private Sub btnCorrect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCorrect.MouseLeftButtonUp
        Dispose()
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub btnIncorrect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncorrect.MouseLeftButtonUp
        Dispose()
        Me.DialogResult = False
        Me.Close()
    End Sub

    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            Dispose()
            Me.DialogResult = False
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
End Class
