Public Class EntriesSuccess
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()



    Public Sub New(ByVal numentries As Integer, ByVal customer As String, ByVal promo As String, ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 0, 9)
        dispatcherTimer.Start()


        InitializeComponent()
        Image1.Source = logo
        lblCustomer.Content = "Hello " + customer + Environment.NewLine
        lblPromo.Content = promo
        txtMessage.Text = "You have successfully created " + numentries.ToString() + " entries in this WinBin!"
    End Sub

    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            dispatcherTimer.Stop()
            dispatcherTimer = Nothing

            Me.DialogResult = True
            Me.Close()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Dispose()
        lblCustomer.Content = ""
        lblPromo.Content = ""
        Image1.Source = Nothing
        txtMessage.Text = Nothing

    End Sub
End Class
