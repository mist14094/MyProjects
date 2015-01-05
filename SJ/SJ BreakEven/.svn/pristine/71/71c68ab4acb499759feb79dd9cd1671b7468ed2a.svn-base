Public Class EntryFailure
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()
    Public Sub New(ByVal interval As String, ByVal freq As String, ByVal intervalnum As String, ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 0, 9)
        dispatcherTimer.Start()

        InitializeComponent()
        Dim amount As String = ""
        Dim intervaltext As String = ""
        If (freq = "1") Then
            amount = "once"
        Else
            amount = freq + " times"
        End If

        If intervalnum = "1" Then
            intervaltext = interval
        Else
            intervaltext = intervalnum + " " + interval + "S"
        End If
        FailureMessage.Text = "Sorry, you may only enter this" + Environment.NewLine + "promotion " + amount + " every " + intervaltext.ToLower + "." + Environment.NewLine + "Please try again soon!"

        Image1.Source = logo

    End Sub

    Public Sub Dispose()
        TextContent.Text = Nothing
        Image1.Source = Nothing
        FailureMessage.Text = Nothing
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
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
