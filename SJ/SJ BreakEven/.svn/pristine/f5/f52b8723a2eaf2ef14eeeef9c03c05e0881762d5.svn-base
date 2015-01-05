Public Class TempDown
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Dim term_info As TerminalInfo
    Dim sechash As String = ""
    Public Sub New(ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 0, 5)
        dispatcherTimer.Start()

        InitializeComponent()
        Image1.Source = logo
        scannerInput.Focus()
    End Sub

    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            scannerInput.Clear()
            scannerInput.Focus()
            If My.Computer.Network.Ping("192.168.1.5") Then
                dispatcherTimer.Stop()
                dispatcherTimer = Nothing

                Me.DialogResult = False
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
