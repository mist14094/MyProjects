Public Class ViewLoyalty
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Dim term_info As TerminalInfo
    Dim sechash As String = ""
    Public Sub New(ByVal logo As BitmapImage, ByVal term As TerminalInfo, ByVal shash As String)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 2, 0)
        dispatcherTimer.Start()

        InitializeComponent()
        Image1.Source = logo
        term_info = term
        sechash = shash
        scannerInput.Focus()
        txtGenMerch.Visibility = Windows.Visibility.Hidden
        txtTob.Visibility = Windows.Visibility.Hidden

    End Sub

    Protected Sub Dispose()
        Image1.Source = Nothing
        term_info = Nothing
        sechash = Nothing
        scannerInput.Text = Nothing
        txtGenMerch.Text = Nothing
        txtGenMerchPoints.Text = Nothing
        txtTob.Text = Nothing
        txtTobPoints.Text = Nothing
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
        GC.Collect()
    End Sub

    Protected Sub cancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeftButtonUp
        Dispose()
        Me.DialogResult = True
        Me.Close()
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

    Private Sub scannerInput_LostFocus(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles scannerInput.LostFocus
        scannerInput.Focus()
    End Sub

    Private Sub scannerInput_KeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles scannerInput.KeyDown
        If e.Key = Key.Return Then
            Dim scaninput As String = scannerInput.Text.Trim().Split(New Char() {" "c, ControlChars.Tab})(0)
            If scaninput.Length > 0 Then
                LoyaltyCall(scaninput)
            End If
            scannerInput.Clear()
        End If
    End Sub

    Private Sub scannerInput_PreviewKeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles scannerInput.PreviewKeyDown
        If e.Key = Key.Tab Then
            scannerInput.Text += " "
        End If
    End Sub

    Private Sub LoyaltyCall(upc As String)
        Dim response As String = ""
        Dim resplist As New List(Of String)()

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getLoyaltyPoints.php")
            webpost.Add("macaddr", term_info.MacAddress)
            webpost.Add("sechash", sechash)
            webpost.Add("people_key", upc)
            webpost.Add("condition_key", "363")
            response = webpost.GetResponse()
        Catch ex As Exception

        End Try


        Dim elements As String() = response.Split(New Char() {";"c})

        If elements.Count() > 1 Then
            Dim points As String = elements(1)

            If points.Length > 0 Then
                txtGenMerch.Visibility = Windows.Visibility.Visible
                txtGenMerchPoints.Text = points
            Else
                txtGenMerch.Visibility = Windows.Visibility.Hidden
                txtGenMerchPoints.Text = ""
            End If
        Else
            txtGenMerch.Visibility = Windows.Visibility.Hidden
            txtGenMerchPoints.Text = ""
        End If

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getLoyaltyPoints.php")
            webpost.Add("macaddr", term_info.MacAddress)
            webpost.Add("sechash", sechash)
            webpost.Add("people_key", upc)
            webpost.Add("condition_key", "362")
            response = webpost.GetResponse()
        Catch ex As Exception

        End Try


        Dim elements2 As String() = response.Split(New Char() {";"c})

        If elements2.Count() > 1 Then
            Dim points As String = elements2(1)

            If points.Length > 0 Then
                txtTob.Visibility = Windows.Visibility.Visible
                txtTobPoints.Text = points
            Else
                txtTob.Visibility = Windows.Visibility.Hidden
                txtTobPoints.Text = ""
            End If
        Else
            txtTob.Visibility = Windows.Visibility.Hidden
            txtTobPoints.Text = ""
        End If
    End Sub
End Class
