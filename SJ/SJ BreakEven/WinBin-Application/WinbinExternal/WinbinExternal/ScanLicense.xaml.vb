Public Class ScanLicense
    Public licenseState As String = Nothing
    Public curCustomer As New Customer

    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Public Sub New(ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 2, 0)
        dispatcherTimer.Start()
        ' This call is required by the designer.


        InitializeComponent()
        scannerInput.IsUndoEnabled = False
        Image1.Source = logo
        ' Add any initialization after the InitializeComponent() call.
        scannerInput.Focus()
    End Sub

    Public Sub Dispose()
        licenseState = Nothing
        curCustomer.Clear()
    End Sub

    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            dispatcherTimer.Stop()
            dispatcherTimer = Nothing

            Me.DialogResult = False
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cancelClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeftButtonUp
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing

        Me.DialogResult = False
        Me.Close()
    End Sub

    Private Function ParseLicenseInfo(ByVal data As String) As List(Of String)
        Dim licinfo As New List(Of String)()
        Try
            Dim temp As String() = data.Trim().Split(","c)
            For i As Integer = 0 To temp.Length - 1
                licinfo.Add(temp(i))
            Next
        Catch ex As Exception

        End Try

        Return licinfo
    End Function

    Private Sub LoadLicenseInfo(data As String)
        'MessageBox.Show(data);
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing

        Dim licenseinfo As List(Of String) = ParseLicenseInfo(data)
        Try
            curCustomer.FirstName = licenseinfo(0)
            curCustomer.MidName = licenseinfo(1)
            curCustomer.LastName = licenseinfo(2)
            curCustomer.Address1 = licenseinfo(3)
            curCustomer.Address2 = licenseinfo(4)
            curCustomer.City = licenseinfo(5)
            curCustomer.State = licenseinfo(6)
            curCustomer.Zip = licenseinfo(7)
            curCustomer.IDNum = licenseinfo(8).Replace(" ", "")
            curCustomer.DOB = DateTime.Parse(licenseinfo(9))
            curCustomer.Gender = licenseinfo(10)
            curCustomer.EyeColor = licenseinfo(11)
        Catch ex As Exception

            Me.DialogResult = False
            scannerInput.Clear()
            Me.Close()
        End Try
    End Sub

    Private Sub scannerInput_LostFocus(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles scannerInput.LostFocus
        scannerInput.Focus()
    End Sub

    Private Sub scannerInput_KeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles scannerInput.KeyDown
        If e.Key = Key.Return Then
            If scannerInput.Text.Length > 20 Then
                curCustomer.Clear()
                LoadLicenseInfo(scannerInput.Text)
                Me.DialogResult = True
            End If
            scannerInput.Clear()
        End If
    End Sub
End Class
