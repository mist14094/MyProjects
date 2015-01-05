Public Class InfoWrong
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()
    Public Sub New(ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 0, 10)
        dispatcherTimer.Start()

        InitializeComponent()
        Image1.Source = logo

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

    Public Sub Dispose()
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
        Image1.Source = Nothing
    End Sub
End Class
