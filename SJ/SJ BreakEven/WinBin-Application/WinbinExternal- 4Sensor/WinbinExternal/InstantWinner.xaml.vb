Public Class InstantWinner
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Public Sub New(ByVal customer As Customer, ByVal Scanner As Integer, ByVal logo As BitmapImage, MacAddr As String, secHash As String)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        InitializeComponent()

        dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 4, 500)
        dispatcherTimer.Start()

        Image1.Source = logo
        lblCustomer.Content = "Hello " + customer.FirstName + Environment.NewLine
        thePlayer.Source = New Uri("C:\winner.mp3", UriKind.Absolute)
        thePlayer.Play()
        Dim response As String = ""
        Dim webpost As New WebPostRequest("http://winbin.com/client/addInstantWinnerEntry.php")
        webpost.Add("macaddr", MacAddr)
        webpost.Add("custID", customer.CustID)
        webpost.Add("sechash", secHash)
        webpost.Add("scanner", Scanner)

        response = webpost.GetResponse()

        lblPromo.Content = Environment.NewLine + response

    End Sub

    Protected Sub Dispose()
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
        Image1.Source = Nothing
        lblCustomer.Content = Nothing
        lblPromo.Content = Nothing
        thePlayer.Close()
        thePlayer.Clock = Nothing
        thePlayer = Nothing
        lblCustomer = Nothing
        lblPromo = Nothing
        imgLogo.Source = Nothing
        imgLogo = Nothing
        backgroundimg.ImageSource = Nothing
        backgroundimg = Nothing
        TextContent.Text = Nothing
        TextContent = Nothing

        GC.Collect()
    End Sub

    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            Dispose()

            Me.DialogResult = True

            Me.Close()

        Catch ex As Exception

        End Try
    End Sub
End Class
