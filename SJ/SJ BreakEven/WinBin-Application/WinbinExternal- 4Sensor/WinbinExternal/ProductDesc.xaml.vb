Public Class ProductDesc
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Public Sub New(ByVal background As String)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 0, 30)
        dispatcherTimer.Start()

        InitializeComponent()
        Try
            Dim bi1 As New BitmapImage()
            bi1.BeginInit()
            If background.Contains(".avi") Then
                TextContent.Visibility = Windows.Visibility.Visible
                thePlayer.Source = New Uri(background, UriKind.Absolute)
                thePlayer.Play()
            Else
                TextContent.Visibility = Windows.Visibility.Hidden
                bi1.UriSource = New Uri(background, UriKind.RelativeOrAbsolute)
                bi1.CacheOption = BitmapCacheOption.None
                bi1.EndInit()

                background_image.ImageSource = bi1
            End If


        Catch ex As Exception
            Me.Close()
        End Try

    End Sub

    Protected Sub Dispose()
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
        thePlayer.Stop()
        thePlayer.Close()
        thePlayer.Clock = Nothing
        thePlayer = Nothing
        background_image.ImageSource = Nothing
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

    Protected Sub ThePlayerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles thePlayer.MouseLeftButtonUp
        Dispose()
        Me.DialogResult = True
        Me.Close()
    End Sub

    Protected Sub cancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeftButtonUp
        Dispose()

        Me.DialogResult = True
        Me.Close()
    End Sub

    Protected Sub MediaOpenedEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles thePlayer.MediaOpened
        TextContent.Visibility = Windows.Visibility.Hidden
        Dim d As Duration = Nothing
        Try
            d = thePlayer.NaturalDuration
            dispatcherTimer.Interval = d.TimeSpan
        Catch ex As Exception

        End Try
    End Sub
End Class
