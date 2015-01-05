Public Class PriceCheck

    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Dim storeNumber As String = Nothing

    Public Sub New(ByVal logo As BitmapImage, ByVal storeNum As String)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 2, 0)
        dispatcherTimer.Start()

        InitializeComponent()
        Image1.Source = logo
        storeNumber = storeNum
        scannerInput.Focus()
    End Sub

    Protected Sub Dispose()
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
        storeNumber = Nothing
        Image1.Source = Nothing
        txtProductName.Text = Nothing
        txtPrice.Text = Nothing
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
                PriceCheck(scaninput)
            End If
            scannerInput.Clear()
        End If
    End Sub

    Private Sub scannerInput_PreviewKeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles scannerInput.PreviewKeyDown
        If e.Key = Key.Tab Then
            scannerInput.Text += " "
        End If
    End Sub

    Private Sub PriceCheck(upc As String)
        Dim response As String = ""
        Dim resplist As New List(Of String)()

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getPriceCheck.php")
            webpost.Add("upc", upc)
            webpost.Add("storenum", storeNumber)
            response = webpost.GetResponse()
        Catch ex As Exception

        End Try


        Dim elements As String() = response.Split(New Char() {";"c})

        If elements.Count() > 1 Then
            Dim productDesc As String = elements(0)
            Dim productPrice As String = elements(1)

            If productDesc.Length > 0 And productPrice.Length > 0 Then
                txtProductName.Text = productDesc
                txtPrice.Text = "$" + productPrice
            ElseIf productPrice.Length > 0 Then
                txtProductName.Text = ""
                txtPrice.Text = "$" + productPrice
            Else
                txtPrice.Text = ""
                txtProductName.Text = "Sorry, item not found"
            End If
            
        End If
    End Sub
End Class
