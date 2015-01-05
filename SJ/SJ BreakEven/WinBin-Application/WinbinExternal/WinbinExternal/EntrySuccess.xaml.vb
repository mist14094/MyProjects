Imports System.IO.Ports
Imports System.Runtime.InteropServices

Public Class EntrySuccess

    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Dim ScanTrigger As Boolean = False
    Dim TriggerCount As Integer = 0
    Dim TriggerArray As New ArrayList
    Dim TiltTrigger As Boolean = False
    Dim ScanLocation As Integer = 0
    Dim curCustomer As Customer = Nothing
    Dim curMacAddr As String = Nothing
    Dim curSecHash As String = Nothing

    Public Sub New(ByVal customer As Customer, ByVal promo As String, ByVal logo As BitmapImage, MacAddr As String, secHash As String)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick

        InitializeComponent()


        dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 6)
        dispatcherTimer.Start()
        ScanTrigger = False
        TiltTrigger = False
        TriggerCount = 0
        ScanLocation = 0
        TriggerArray.Clear()

        curMacAddr = MacAddr
        curSecHash = secHash
        curCustomer = customer

        Image1.Source = logo
        lblCustomer.Content = "Hello " + customer.FirstName + Environment.NewLine
        lblPromo.Content = Environment.NewLine + promo

    End Sub

    Private Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SourceInitialized
        Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            dispatcherTimer.Stop()
            dispatcherTimer = Nothing

            Dispose()
            Me.DialogResult = True
            Me.Close()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub Dispose()

        ScanTrigger = Nothing
        TriggerCount = Nothing
        TriggerArray = Nothing
        TiltTrigger = Nothing
        ScanLocation = Nothing
        curCustomer = Nothing
        curMacAddr = Nothing
        curSecHash = Nothing
        Image1.Source = Nothing
        lblCustomer.Content = Nothing
        lblPromo.Content = Nothing
        imgLogo.Source = Nothing
        imgLogo = Nothing
        backgroundimg.ImageSource = Nothing
        backgroundimg = Nothing
        TextContent.Text = Nothing
        TextContent = Nothing
        GC.Collect()
    End Sub

End Class
