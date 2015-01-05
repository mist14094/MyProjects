Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Media.Animation
Imports WinbinExternal.WinbinClientWPF
Imports WinbinExternal.OutlinedText
Imports System.Windows.Media.Effects
Imports System.Media
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Security.Cryptography
Imports System.IO
Imports System.IO.Ports
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Security.Principal
Imports System.Reflection

Public Class MainWindow
    Dim contentBlocks(3) As Grid
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Protected touchStart As Point

    Dim AlreadySwiped As Boolean = True

    Dim terminal_info As New TerminalInfo
    Dim buttonpress As New SoundPlayer()

    Dim secSalt As String = "There was no more troublesome mischief, and every happy event around the house was announced by a fox's sharp bark."
    Dim secHash As String = ""
    Dim logo As New BitmapImage
    Dim fp_1 As String = Nothing
    Dim fp_2 As String = Nothing
    Dim fp_3 As String = Nothing

    Private Function IsRunAsAdministrator() As Boolean
        Dim wi = WindowsIdentity.GetCurrent()
        Dim wp = New WindowsPrincipal(wi)

        Return wp.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Public Sub New()
        If Not IsRunAsAdministrator() Then
            ' It is not possible to launch a ClickOnce app as administrator directly, so instead we launch the
            ' app as administrator in a new process.
            Dim processInfo = New ProcessStartInfo(Assembly.GetExecutingAssembly().CodeBase)

            ' The following properties run the new process as administrator
            processInfo.UseShellExecute = True
            processInfo.Verb = "runas"

            ' Start the new process
            Try
                Process.Start(processInfo)
            Catch generatedExceptionName As Exception
                ' The user did not allow the application to run as administrator
                MessageBox.Show("Sorry, this application must be run as Administrator.")
            End Try

            ' Shut down the current process
            Application.Current.Shutdown()
            ' We are running as administrator

            ' Do normal startup stuff...
        Else
            '  DispatcherTimer setup
            AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
            dispatcherTimer.Interval = New TimeSpan(0, 0, 10)
            dispatcherTimer.Start()


            ' This call is required by the designer.
            Try
                InitializeComponent()
            Catch ex As Exception
                MessageBox.Show("ERROR INITIALIZING COMPONENT")
            End Try


            ServicePointManager.ServerCertificateValidationCallback = Function(s, cert, chain, ssl) True

            Try
                getMacAddress()
                MessageBox.Show(terminal_info.MacAddress)
            Catch ex As Exception
                MessageBox.Show("ERROR SETTING MAC ADDRESS")
            End Try

            Try
                createSecHash()
            Catch ex As Exception
                MessageBox.Show("ERROR CREATING SEC HASH")
            End Try

            Try
                retrieveLogo()
            Catch ex As Exception
                MessageBox.Show("ERROR RETRIEVING LOGO")
            End Try

            Try
                getStoreNum()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                loadContent()
            Catch ex As Exception
                MessageBox.Show("ERROR LOADING CONTENT")
            End Try
            scannerInput.IsUndoEnabled = False
            scannerInput.Clear()

            scannerInput.Focus()

        End If
    End Sub

    Private Sub scannerInput_KeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles scannerInput.KeyDown
        If e.Key = Key.Return Then
            e.Handled = True

            Dim scaninput As String = scannerInput.Text.Trim().Split(New Char() {" "c, ControlChars.Tab})(0)

            If scaninput.Contains("629915") Then
                scaninput = scaninput.Replace("629915", "")
            End If

            If scaninput.Contains(";") AndAlso scaninput.Contains("?") Then
                cardScanned(scaninput.Replace(";", "").Replace("?", "").Substring(6))
            Else
                cardScanned(scaninput)
            End If

            scannerInput.Text = ""
        Else
            scannerInput.SelectionStart = scannerInput.Text.Length
            scannerInput.SelectionLength = 0
        End If
    End Sub

    Private Sub scannerInput_PreviewKeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles scannerInput.PreviewKeyDown
        If e.Key = Key.Tab Then
            scannerInput.Text += " "
        End If
    End Sub

    Private Sub scannerLostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles scannerInput.LostFocus
        scannerInput.Focus()
    End Sub
    'Retrieves the mac address for the first network card on this machine.  This will be the identifier for this terminal.
    'Output: terminal_info.MacAddress(global)
    Private Sub getMacAddress()
        Dim macaddress As String = ""
        Try
            Dim nic As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()(0)
            macaddress = nic.GetPhysicalAddress.ToString

            'Format the MAC address as: xx:xx:xx:xx:xx
            macaddress = macaddress.Insert(10, ":")
            macaddress = macaddress.Insert(8, ":")
            macaddress = macaddress.Insert(6, ":")
            macaddress = macaddress.Insert(4, ":")
            macaddress = macaddress.Insert(2, ":")
            terminal_info.MacAddress = macaddress

        Catch ex As Exception

        End Try
    End Sub

    'Creates a security hash based on our terminal's MAC address that is sent with requests as an added bit of security
    Private Sub createSecHash()
        Dim enc As New ASCIIEncoding
        Dim cryptoTransformSHA1 As New SHA1CryptoServiceProvider
        Try
            'Our salt is a static string which is concatenated to the mac address for now
            Dim encode As String = secSalt + terminal_info.MacAddress

            'Encrypt the string using SHA1
            Dim buffer() As Byte = enc.GetBytes(encode)
            Dim hash As String = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "").ToLower

            secHash = hash
        Catch ex As Exception

        End Try
    End Sub

    Private Sub retrieveLogo()
        Try
            'Send request for the logo image data
            Dim webpost As New WebPostRequest("http://winbin.com/client/getLogo.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("sechash", secHash)

            'convert the response into a stream and then into a bitmap image
            Dim response As Stream = webpost.GetResponseStream


            logo.BeginInit()
            logo.StreamSource = response
            logo.CacheOption = BitmapCacheOption.OnLoad
            logo.EndInit()

            Image1.Source = logo

        Catch ex As Exception
            MessageBox.Show("No Logo for terminal " + terminal_info.MacAddress)
        End Try
    End Sub

     Protected Sub loadContent()
        Dim response As String = ""
        Dim bi1 As New BitmapImage()
        Dim bi2 As New BitmapImage()
        Dim bi3 As New BitmapImage()
        Try

            contentBlockA.Height = 246
            contentBlockA.Width = 355
            contentBlockA.Opacity = 0.6
            Canvas.SetLeft(contentBlockA, 25)
            Canvas.SetTop(contentBlockA, 117)

            contentBlockC.Height = 246
            contentBlockC.Width = 355
            contentBlockC.Opacity = 0.6
            Canvas.SetLeft(contentBlockC, 632)
            Canvas.SetTop(contentBlockC, 117)

            Canvas.SetZIndex(contentBlockA, 98)
            Canvas.SetZIndex(contentBlockB, 99)
            Canvas.SetZIndex(contentBlockC, 97)
            Try
                Dim webpost As New WebPostRequest("http://www.winbin.com/client/getFeaturedProducts.php")
                webpost.Add("macaddr", terminal_info.MacAddress)
                webpost.Add("sechash", secHash)

                response = webpost.GetResponse()
            Catch e As Exception

            End Try

            Dim elements As String() = response.Split(New Char() {";"c})

            If elements.Count() > 5 Then
                txtATitle.Content = elements(0)
                txtBTitle.Content = elements(3)
                txtCTitle.Content = elements(6)


                bi1.BeginInit()
                bi1.UriSource = New Uri(elements(1), UriKind.RelativeOrAbsolute)
                bi1.CacheOption = BitmapCacheOption.OnLoad
                bi1.EndInit()

                fp_1 = elements(2)

                bi2.BeginInit()
                bi2.UriSource = New Uri(elements(4), UriKind.RelativeOrAbsolute)
                bi2.CacheOption = BitmapCacheOption.OnLoad
                bi2.EndInit()

                fp_2 = elements(5)

                bi3.BeginInit()
                bi3.UriSource = New Uri(elements(7), UriKind.RelativeOrAbsolute)
                bi3.CacheOption = BitmapCacheOption.OnLoad
                bi3.EndInit()

                fp_3 = elements(8)

                imgAContent.Stretch = Stretch.Fill
                imgAContent.Source = bi1

                imgBContent.Stretch = Stretch.Fill
                imgBContent.Source = bi2

                imgCContent.Stretch = Stretch.Fill
                imgCContent.Source = bi3
            End If

            contentBlocks(0) = contentBlockB
            contentBlocks(1) = contentBlockA
            contentBlocks(2) = contentBlockC

        Catch ex As Exception

        End Try
    End Sub

    '<summary>
    'Retrieves the type of promotion this terminal is currently set to, valid types are:
    'Time-Based: limited by scans per day/hour
    'Point-Based: limited by points 
    '</summary>
    '<returns>PromoInfo object containing the type and frequency of the promotion</returns>
    Private Function getPromoType() As PromoInfo
        Dim response As String = ""
        Dim promoinfo As New PromoInfo

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getPromoType.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch ex As Exception

        End Try

        Dim elements As String() = response.Split(New Char() {";"c})
        If (elements.Count = 2 And elements(0) <> "" And elements(1) <> "") Then
            promoinfo.promotype = elements(0)
            Try
                promoinfo.frequency = Int32.Parse(elements(1))
            Catch ex As Exception

            End Try
        End If

        Return promoinfo
    End Function

    ''' <summary>
    ''' Retrieves the current amount of points that this customer has accumulated.  Only used in points based promotions.
    ''' </summary>
    ''' <returns>int of number of points accumulated</returns>
    Private Function getEarnedPoints(curcust As Customer) As Integer
        Dim response As String = ""

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getEarnedPoints.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("custID", curcust.CustID)
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch
        End Try

        Dim points As Integer = 0
        Try
            points = Int32.Parse(response)
        Catch
        End Try

        Return points
    End Function

    ''' <summary>
    ''' Called when a loyalty card is scanned.  This function goes through the sequence of screens and necessary steps to 
    ''' create contest entries for customers when they scan.
    ''' </summary>
    Private Sub cardScanned(cardnumber As String)
        'First attempt to retrieve the customer associated with this card number
        'If located the Customer object's located flag will be true
        scannerInput.IsEnabled = False

        Dim currentCustomer As Customer = getCustomerInfo(cardnumber)

        If currentCustomer.located Then
            'Retrieve the current promotion information for this terminal that the customer is attempting to enter in to
            Dim promoinfo As PromoInfo = getPromoType()

            If promoinfo.promotype = "POINTS" Then
                'This is a point based promotion where customers print a number of entries based on accumulated points
                'Get the amount of earned points for this customer
                Dim earnedpoints As Integer = getEarnedPoints(currentCustomer)

                'Figure out how many entries they are allowed
                Dim allowedentries As Integer = earnedpoints / promoinfo.frequency

                ''Verify spending points and creating these entries
                Dim verify As New VerifyCreateEntries(earnedpoints, allowedentries, promoinfo.frequency, logo)

                If verify.ShowDialog() Then
                    'Create the entries in the database and retrieve the printer type we're using
                    Dim entryreturn As List(Of String) = createEntries(currentCustomer, allowedentries)
                    Dim promodesc As String = entryreturn(1)
                    Dim printertype As String = entryreturn(2)
                    'Print the entries
                    printEntries(currentCustomer, promodesc, printertype, allowedentries)

                    dispatcherTimer.Stop()
                    Dim success As New EntriesSuccess(allowedentries, currentCustomer.FirstName, promodesc, logo)
                    success.ShowDialog()
                    dispatcherTimer.Start()

                    currentCustomer.Clear()
                    scannerInput.Clear()
                    success.Close()
                    GC.Collect()
                End If
            Else
                'This is a time based promotion where customers are allowed so many entries per time interval
                Dim entryreturn As List(Of String) = createEntry(currentCustomer)

                If entryreturn(0) = "1" Then
                    Dim promodesc As String = entryreturn(1)
                    Dim printertype As String = entryreturn(2)

                    'Allow entry
                    printEntry(currentCustomer, promodesc, printertype)

                    dispatcherTimer.Stop()
                    Dim success As New EntrySuccess(currentCustomer, promodesc, logo, terminal_info.MacAddress, secHash)
                    success.ShowDialog()
                    dispatcherTimer.Start()

                    currentCustomer.Clear()
                    scannerInput.Clear()
                    success.Close()
                    success = Nothing
                    GC.Collect()
                Else
                    Dim interval As String = entryreturn(1)
                    Dim freq As String = entryreturn(2)
                    Dim intervalnum As String = entryreturn(3)

                    'No more entries allowed for today
                    dispatcherTimer.Stop()
                    Dim failure As New EntryFailure(interval, freq, intervalnum, logo)
                    failure.ShowDialog()
                    failure.Close()
                    dispatcherTimer.Start()
                    GC.Collect()
                End If
            End If
        End If
        scannerInput.IsEnabled = True
        scannerInput.Focus()

        currentCustomer.Clear()
        scannerInput.Clear()

    End Sub

    'Fills the currentCustomer object with the customer information associated with the provided card number
    Private Function getCustomerInfo(ByVal cardnumber As String) As Customer
        Dim currentCustomer As New Customer()
        Dim response As String = ""

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getCustInfoCard.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("cardnum", cardnumber)
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch
        End Try

        Dim elements As String() = response.Split(New Char() {";"c})
        If elements.Count() = 7 AndAlso elements(0) <> "" AndAlso elements(1) <> "" Then
            currentCustomer.CustID = elements(0)
            currentCustomer.CardNum = elements(1)
            currentCustomer.FirstName = elements(2)
            currentCustomer.LastName = elements(3)
            currentCustomer.MidName = elements(4)
            currentCustomer.Suffix = elements(5)
            currentCustomer.Email = elements(6)
            currentCustomer.located = True
        End If
        Return currentCustomer
    End Function

    Private Function getCustomerInfo(State As String, IDNum As String) As Customer
        Dim currentCustomer As New Customer()

        Dim response As String = ""
        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getCustInfoID.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("state", State)
            webpost.Add("idnum", IDNum)
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch
        End Try
        Dim elements As String() = response.Split(New Char() {";"c})
        If elements.Count() = 7 AndAlso elements(0) <> "" Then
            currentCustomer.CustID = elements(0)
            currentCustomer.CardNum = elements(1)
            currentCustomer.FirstName = elements(2)
            currentCustomer.LastName = elements(3)
            currentCustomer.MidName = elements(4)
            currentCustomer.Suffix = elements(5)
            currentCustomer.Email = elements(6)
            currentCustomer.located = True
        End If
        Return currentCustomer
    End Function

    Private Function createEntry(customer As Customer) As List(Of String)
        Dim response As String = ""
        Dim resplist As New List(Of String)()
        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/createEntry.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("custid", customer.CustID)
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch
        End Try
        Dim elements As String() = response.Split(New Char() {";"c})
        For i As Integer = 0 To elements.Count() - 1
            resplist.Add(elements(i))
        Next
        Return resplist
    End Function

    Private Function createEntries(customer As Customer, numentries As Integer) As List(Of String)
        Dim response As String = ""
        Dim resplist As New List(Of String)()
        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/createEntries.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("custid", customer.CustID)
            webpost.Add("entries", numentries.ToString())
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch
        End Try
        Dim elements As String() = response.Split(New Char() {";"c})
        For i As Integer = 0 To elements.Count() - 1
            resplist.Add(elements(i))
        Next
        Return resplist
    End Function

    Public Shared Function StringToByteArray(hex As [String]) As Byte()
        Dim NumberChars As Integer = hex.Length
        Dim bytes As Byte() = New Byte(NumberChars \ 2 - 1) {}
        For i As Integer = 0 To NumberChars - 1 Step 2
            bytes(i \ 2) = Convert.ToByte(hex.Substring(i, 2), 16)
        Next
        Return bytes
    End Function

    Public Function ConvertToHex(asciiString As String) As String
        Dim hex As String = ""
        For Each c As Char In asciiString
            Dim tmp As Integer = AscW(c)
            hex += [String].Format("{0:x2}", CUInt(System.Convert.ToUInt32(tmp.ToString())))
        Next
        Return hex
    End Function

    Private Sub printEntries(customer As Customer, promodesc As String, printertype As String, numentries As Integer)
        For i As Integer = 0 To numentries - 1
            printEntry(customer, promodesc, printertype)
            Thread.Sleep(1200)
        Next
    End Sub

    Private Sub printEntry(customer As Customer, promodesc As String, printertype As String)

        Dim firstinitial As String = ""
        If customer.FirstName.Length >= 1 Then
            firstinitial = customer.FirstName.Substring(0, 1)
        End If

        Dim middleinitial As String = ""
        If customer.MidName.Length >= 1 Then
            middleinitial = customer.MidName.Substring(0, 1)
        End If

        Dim lasttrimmed As String = customer.LastName
        If lasttrimmed.Length > 8 Then
            lasttrimmed = lasttrimmed.Substring(0, 8)
        End If

        If printertype = "C" Then

            Dim linefeedbytes As Byte() = StringToByteArray("0A")
            Dim pinnedlinefeed As GCHandle = GCHandle.Alloc(linefeedbytes, GCHandleType.Pinned)
            Dim linefeed As IntPtr = pinnedlinefeed.AddrOfPinnedObject()

            Dim cutbytes As Byte() = StringToByteArray("1CC034")
            Dim pinnedcut As GCHandle = GCHandle.Alloc(cutbytes, GCHandleType.Pinned)
            Dim cut As IntPtr = pinnedcut.AddrOfPinnedObject()

            Dim barcodestr As String = customer.CardNum

            Dim barcodehex As String = ConvertToHex(barcodestr)

            Dim barcodebytes1 As Byte() = StringToByteArray("1D6B07" & barcodehex & "00")
            Dim barcodebytes2 As Byte() = StringToByteArray("1D685A")
            Dim barcodebytes3 As Byte() = StringToByteArray("1D4802")

            Dim pinnedbarcode1 As GCHandle = GCHandle.Alloc(barcodebytes1, GCHandleType.Pinned)
            Dim barcode1 As IntPtr = pinnedbarcode1.AddrOfPinnedObject()
            Dim pinnedbarcode2 As GCHandle = GCHandle.Alloc(barcodebytes2, GCHandleType.Pinned)
            Dim barcode2 As IntPtr = pinnedbarcode2.AddrOfPinnedObject()
            Dim pinnedbarcode3 As GCHandle = GCHandle.Alloc(barcodebytes3, GCHandleType.Pinned)
            Dim barcode3 As IntPtr = pinnedbarcode3.AddrOfPinnedObject()


            ' Send a printer-specific to the printer.
            PrinterHelper.SendStringToPrinter("CUSTOM TG2460-H", Convert.ToString(customer.FirstName) & " " & middleinitial & " " & Convert.ToString(customer.LastName) & " " & Convert.ToString(customer.Suffix))
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", linefeed, linefeedbytes.Count())
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", barcode3, barcodebytes3.Count())
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", barcode2, barcodebytes2.Count())
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", barcode1, barcodebytes1.Count())
            PrinterHelper.SendStringToPrinter("CUSTOM TG2460-H", " ")
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", linefeed, linefeedbytes.Count())
            PrinterHelper.SendStringToPrinter("CUSTOM TG2460-H", " ")
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", linefeed, linefeedbytes.Count())
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", cut, cutbytes.Count())
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", cut, cutbytes.Count())
            PrinterHelper.SendBytesToPrinter("CUSTOM TG2460-H", cut, cutbytes.Count())
        ElseIf printertype = "N" OrElse printertype = "N2" OrElse printertype = "NS" Then
            Dim printername As String
            If printertype = "N" Then
                printername = "NII ExD NP-2511"
            Else
                printername = "NII Printer_DS"
            End If

            Dim backfeedbytes As Byte() = StringToByteArray("1B4230")
            Dim pinnedbackfeed As GCHandle = GCHandle.Alloc(backfeedbytes, GCHandleType.Pinned)
            Dim backfeed As IntPtr = pinnedbackfeed.AddrOfPinnedObject()

            Dim linesizebytes As Byte() = StringToByteArray("1B330F")
            Dim pinnedlinesize As GCHandle = GCHandle.Alloc(linesizebytes, GCHandleType.Pinned)
            Dim linesize As IntPtr = pinnedlinesize.AddrOfPinnedObject()

            Dim linefeedbytes As Byte() = StringToByteArray("1B6405")
            Dim pinnedlinefeed As GCHandle = GCHandle.Alloc(linefeedbytes, GCHandleType.Pinned)
            Dim linefeed As IntPtr = pinnedlinefeed.AddrOfPinnedObject()

            Dim cutbytes As Byte() = StringToByteArray("1B69")
            Dim pinnedcut As GCHandle = GCHandle.Alloc(cutbytes, GCHandleType.Pinned)
            Dim cut As IntPtr = pinnedcut.AddrOfPinnedObject()

            Dim backbytes As Byte() = StringToByteArray("1B4210")
            Dim pinnedback As GCHandle = GCHandle.Alloc(cutbytes, GCHandleType.Pinned)
            Dim back As IntPtr = pinnedcut.AddrOfPinnedObject()

            Dim barcodestr As String = customer.CardNum
            If barcodestr.Trim() = "" Then
                barcodestr = customer.CustID
            End If
            Dim barcodehex As String = ConvertToHex(barcodestr)

            Dim barcodebytes1 As Byte() = StringToByteArray("1D6B06" & barcodehex & "00")
            Dim barcodebytes2 As Byte() = StringToByteArray("1D685A")
            Dim barcodebytes3 As Byte() = StringToByteArray("1D4802")
            Dim pinnedbarcode1 As GCHandle = GCHandle.Alloc(barcodebytes1, GCHandleType.Pinned)
            Dim barcode1 As IntPtr = pinnedbarcode1.AddrOfPinnedObject()
            Dim pinnedbarcode2 As GCHandle = GCHandle.Alloc(barcodebytes2, GCHandleType.Pinned)
            Dim barcode2 As IntPtr = pinnedbarcode2.AddrOfPinnedObject()
            Dim pinnedbarcode3 As GCHandle = GCHandle.Alloc(barcodebytes3, GCHandleType.Pinned)
            Dim barcode3 As IntPtr = pinnedbarcode3.AddrOfPinnedObject()

            If printertype = "NS" Then
                Dim serialport As SerialPort
                serialport = New SerialPort("COM1", 115200, Parity.None, 8, StopBits.One)
                serialport.Open()

                serialport.Write(backfeedbytes, 0, backfeedbytes.Length)
                serialport.Write(linesizebytes, 0, linesizebytes.Length)
                serialport.Write(Convert.ToString(customer.FirstName) & " " & middleinitial & " " & Convert.ToString(customer.LastName) & " " & Convert.ToString(customer.Suffix))
                serialport.Write(barcodebytes3, 0, barcodebytes3.Length)
                serialport.Write(barcodebytes2, 0, barcodebytes2.Length)
                serialport.Write(barcodebytes1, 0, barcodebytes1.Length)
                serialport.Write(linefeedbytes, 0, linefeedbytes.Length)
                serialport.Write(cutbytes, 0, cutbytes.Length)

                serialport.Close()
                serialport.Close()
            Else
                ' Send a printer-specific to the printer.
                PrinterHelper.SendBytesToPrinter(printername, backfeed, backfeedbytes.Count())
                PrinterHelper.SendBytesToPrinter(printername, linesize, linesizebytes.Count())
                PrinterHelper.SendStringToPrinter(printername, Convert.ToString(customer.FirstName) & " " & middleinitial & " " & Convert.ToString(customer.LastName) & " " & Convert.ToString(customer.Suffix))
                PrinterHelper.SendBytesToPrinter(printername, barcode3, barcodebytes3.Count())
                PrinterHelper.SendBytesToPrinter(printername, barcode2, barcodebytes2.Count())
                PrinterHelper.SendBytesToPrinter(printername, barcode1, barcodebytes1.Count())
                PrinterHelper.SendBytesToPrinter(printername, linefeed, linefeedbytes.Count())
                PrinterHelper.SendBytesToPrinter(printername, cut, cutbytes.Count())
            End If
        Else
            Dim serialport As SerialPort
            serialport = New SerialPort("COM1", 115200, Parity.None, 8, StopBits.One)
            serialport.Open()

            'Due to C# strings mangling them, we have to store the command bytes in byte arrays and send them seperately
            Select Case printertype

                Case "I"
                    Dim i_feed As Byte() = {Convert.ToByte(13), Convert.ToByte(10)}
                    Dim i_cmd1 As Byte() = {Convert.ToByte(27), Convert.ToByte("d"c), Convert.ToByte(10), Convert.ToByte(27), Convert.ToByte("v"c), Convert.ToByte(27), _
                     Convert.ToByte("j"c), Convert.ToByte(200), Convert.ToByte(27), Convert.ToByte("j"c), Convert.ToByte(200), Convert.ToByte(27), _
                     Convert.ToByte("j"c), Convert.ToByte(200)}
                    Dim i_cmd2 As Byte() = {Convert.ToByte(27), Convert.ToByte("]"c), Convert.ToByte(27), Convert.ToByte("j"c), Convert.ToByte(200)}

                    Dim i_contestheader As String = "I          Contest  Entry"
                    Dim i_name As String = "***Name:  " & Convert.ToString(customer.FirstName) & " " & middleinitial & " " & Convert.ToString(customer.LastName) & " " & Convert.ToString(customer.Suffix)
                    Dim i_cardcust As String = "***Card #:" & Convert.ToString(customer.CardNum) & " " & " Cust #:" & Convert.ToString(customer.CustID)
                    Dim i_promo As String = "***Promo: " & promodesc
                    Dim i_date As String = "***Date:  " & DateTime.Today.ToString("MM/dd/yyyy") & "  WEB"

                    serialport.Write(i_contestheader)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_name)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cardcust)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_promo)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_date)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd1, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    serialport.Write(i_feed, 0, i_feed.Length)
                    serialport.Write(i_cmd2, 0, i_cmd1.Length)
                    Exit Select

                Case "S"
                    Dim s_cmd1 As Byte() = {Convert.ToByte(27), Convert.ToByte("&"c), Convert.ToByte("P"c), Convert.ToByte(45), Convert.ToByte(100), Convert.ToByte(27), _
                     Convert.ToByte("h"c), Convert.ToByte(1), Convert.ToByte(27), Convert.ToByte("w"c), Convert.ToByte(1)}
                    Dim s_cmd2 As Byte() = {Convert.ToByte(27), Convert.ToByte("h"c), Convert.ToByte(0), Convert.ToByte(27), Convert.ToByte("w"c), Convert.ToByte(0)}
                    Dim s_cmd3 As Byte() = {Convert.ToByte(27), Convert.ToByte(30), Convert.ToByte(27), Convert.ToByte(12), Convert.ToByte(15), Convert.ToByte(5)}

                    Dim s_contestheader As String = "    Contest Entry" & vbCr & vbLf & vbCr & vbLf & vbCr & vbLf
                    Dim s_info As String = "***Name:  " & Convert.ToString(customer.FirstName) & " " & middleinitial & " " & Convert.ToString(customer.LastName) & " " & Convert.ToString(customer.Suffix) & vbCr & vbLf & vbCr & vbLf & "***Card #:" & Convert.ToString(customer.CardNum) & " " & " Cust #:" & Convert.ToString(customer.CustID) & vbCr & vbLf & vbCr & vbLf & "***Promo: " & promodesc & vbCr & vbLf & vbCr & vbLf & "Date:  " & DateTime.Today.ToString("MM/dd/yyyy") & "  WEB" & vbCr & vbLf & vbCr & vbLf

                    serialport.Write(s_cmd1, 0, s_cmd1.Length)
                    serialport.Write(s_contestheader)
                    serialport.Write(s_cmd2, 0, s_cmd2.Length)
                    serialport.Write(s_info)
                    serialport.Write(s_cmd3, 0, s_cmd3.Length)
                    Exit Select

                Case "H"
                    Dim h_feed As Byte() = {Convert.ToByte(13), Convert.ToByte(10)}
                    Dim h_barcode As Byte() = {Convert.ToByte(29), Convert.ToByte("k"c), Convert.ToByte(69), Convert.ToByte(customer.CardNum.Length + 2)}
                    Dim h_rev As Byte() = {Convert.ToByte(27), Convert.ToByte(75), Convert.ToByte(100)}
                    Dim h_cut As Byte() = {Convert.ToByte(27), Convert.ToByte(205), Convert.ToByte(4), Convert.ToByte(232), Convert.ToByte(0), Convert.ToByte(0), _
                     Convert.ToByte(0), Convert.ToByte(0)}
                    Dim h_eod As Byte() = {Convert.ToByte(27), Convert.ToByte(205), Convert.ToByte(0), Convert.ToByte(113)}

                    Dim h_namedate As String = firstinitial & " " & lasttrimmed & "(" & Convert.ToString(customer.CardNum) & ") " & DateTime.Today.ToString("MM/dd/yyyy") & "W"
                    Dim h_cardnum As String = "*" & Convert.ToString(customer.CardNum) & "*"

                    serialport.Write(h_rev, 0, h_rev.Length)
                    serialport.Write(h_namedate)
                    serialport.Write(h_feed, 0, h_feed.Length)
                    serialport.Write(h_barcode, 0, h_barcode.Length)
                    serialport.Write(h_cardnum)
                    serialport.Write(h_feed, 0, h_feed.Length)
                    serialport.Write(h_feed, 0, h_feed.Length)
                    serialport.Write(h_feed, 0, h_feed.Length)
                    serialport.Write(h_feed, 0, h_feed.Length)
                    serialport.Write(h_feed, 0, h_feed.Length)
                    serialport.Write(h_cut, 0, h_cut.Length)
                    serialport.Write(h_eod, 0, h_eod.Length)
                    Exit Select

                Case "H2"
                    Dim h2_feed As Byte() = {Convert.ToByte(10), Convert.ToByte(13)}
                    Dim h2_barcode As Byte() = {Convert.ToByte(29), Convert.ToByte("k"c), Convert.ToByte(69), Convert.ToByte(customer.CardNum.Length + 2)}
                    Dim h2_cut As Byte() = {Convert.ToByte(27), Convert.ToByte(240), Convert.ToByte(6), Convert.ToByte(2), Convert.ToByte(2), Convert.ToByte(0)}

                    Dim h2_namedate As String = firstinitial & " " & lasttrimmed & "(" & Convert.ToString(customer.CardNum) & ") " & DateTime.Today.ToString("MM/dd/yyyy")
                    Dim h2_cardnum As String = "*" & Convert.ToString(customer.CardNum) & "*"


                    serialport.Write(h2_feed, 0, h2_feed.Length)
                    serialport.Write(h2_feed, 0, h2_feed.Length)
                    serialport.Write(h2_namedate)
                    serialport.Write(h2_feed, 0, h2_feed.Length)
                    serialport.Write(h2_barcode, 0, h2_barcode.Length)
                    serialport.Write(h2_cardnum)
                    serialport.Write(h2_feed, 0, h2_feed.Length)
                    serialport.Write(h2_feed, 0, h2_feed.Length)
                    serialport.Write(h2_cut, 0, h2_cut.Length)


                    Exit Select
            End Select
            serialport.Close()
        End If
    End Sub

    'Protected Sub leftBlockClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles leftPosition.MouseDown


    '    Try
    '        Animate_Grid_Center(contentBlocks(1))
    '        Animate_Grid_Right(contentBlocks(0))
    '        Animate_Grid_Left(contentBlocks(2))

    '        Canvas.SetZIndex(contentBlocks(0), 97)
    '        Canvas.SetZIndex(contentBlocks(1), 99)
    '        Canvas.SetZIndex(contentBlocks(2), 98)

    '        Dim tempCB As Grid = contentBlocks(1)

    '        contentBlocks(1) = contentBlocks(2)
    '        contentBlocks(2) = contentBlocks(0)
    '        contentBlocks(0) = tempCB

    '        dispatcherTimer.Stop()
    '        dispatcherTimer.Start()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub rightBlockClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles rightPosition.MouseDown
    '    Animate_Grid_Center(contentBlocks(2))
    '    Animate_Grid_Left(contentBlocks(0))
    '    Animate_Grid_Right(contentBlocks(1))

    '    Canvas.SetZIndex(contentBlocks(0), 98)
    '    Canvas.SetZIndex(contentBlocks(1), 97)
    '    Canvas.SetZIndex(contentBlocks(2), 99)

    '    Dim tempCB As Grid = contentBlocks(2)
    '    contentBlocks(2) = contentBlocks(1)
    '    contentBlocks(1) = contentBlocks(0)
    '    contentBlocks(0) = tempCB

    '    dispatcherTimer.Stop()
    '    dispatcherTimer.Start()
    'End Sub

    'grd.BeginAnimation(Grid.OpacityProperty, fade)

    'Swipe Procedures

    'Begin Swipe (touch screen)
    Private Sub touchDown_Event(ByVal sender As Object, ByVal e As MouseEventArgs) Handles leftPosition.PreviewMouseLeftButtonDown
        AlreadySwiped = False
        touchStart = e.GetPosition(leftPosition)
    End Sub

    'End Swipe (lift finger from screen)
    Private Sub touchMove_Event(ByVal sender As Object, ByVal e As MouseEventArgs) Handles leftPosition.PreviewMouseLeftButtonUp
        If Not AlreadySwiped Then
            Dim Touch As Point = e.GetPosition(leftPosition)

            If (touchStart <> Nothing And Touch.X > (touchStart.X + 15)) Then

                Animate_Grid_Center(contentBlocks(1))
                Animate_Grid_Right(contentBlocks(0))
                Animate_Grid_Left(contentBlocks(2))

                Canvas.SetZIndex(contentBlocks(0), 97)
                Canvas.SetZIndex(contentBlocks(1), 99)
                Canvas.SetZIndex(contentBlocks(2), 98)

                Dim tempCB As Grid = contentBlocks(1)

                contentBlocks(1) = contentBlocks(2)
                contentBlocks(2) = contentBlocks(0)
                contentBlocks(0) = tempCB

                dispatcherTimer.Stop()
                dispatcherTimer.Start()

                AlreadySwiped = True

            ElseIf (touchStart <> Nothing And Touch.X < (touchStart.X - 15)) Then
                Animate_Grid_Center(contentBlocks(2))
                Animate_Grid_Left(contentBlocks(0))
                Animate_Grid_Right(contentBlocks(1))

                Canvas.SetZIndex(contentBlocks(0), 98)
                Canvas.SetZIndex(contentBlocks(1), 97)
                Canvas.SetZIndex(contentBlocks(2), 99)

                Dim tempCB As Grid = contentBlocks(2)
                contentBlocks(2) = contentBlocks(1)
                contentBlocks(1) = contentBlocks(0)
                contentBlocks(0) = tempCB

                dispatcherTimer.Stop()
                dispatcherTimer.Start()

                AlreadySwiped = True
            ElseIf (touchStart.X > 290 And Touch.X < 715) Then
                If Canvas.GetZIndex(contentBlockA) = 99 Then
                    Dim displayDesc As New ProductDesc(fp_1)
                    displayDesc.ShowDialog()
                    displayDesc.Close()
                ElseIf Canvas.GetZIndex(contentBlockB) = 99 Then
                    Dim displayDesc As New ProductDesc(fp_2)
                    displayDesc.ShowDialog()
                    displayDesc.Close()
                ElseIf Canvas.GetZIndex(contentBlockC) = 99 Then
                    Dim displayDesc As New ProductDesc(fp_3)
                    displayDesc.ShowDialog()
                    displayDesc.Close()
                End If
            End If
        End If
        touchStart = Nothing
        e.Handled = True
    End Sub

    Private Sub Animate_Grid_Center(ByVal grd As Grid)
        Dim left As New DoubleAnimation
        Dim top As New DoubleAnimation
        Dim width As New DoubleAnimation
        Dim height As New DoubleAnimation
        Dim opacity As New DoubleAnimation
        Dim zindex As New DoubleAnimation

        Try
            left.From = Canvas.GetLeft(grd)
            left.To = 295

            left.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            top.From = Canvas.GetTop(grd)
            top.To = 150

            top.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            width.From = grd.Width
            width.To = 421

            width.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            height.From = grd.Height
            height.To = 292

            height.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            opacity.From = grd.Opacity
            opacity.To = 1

            opacity.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            zindex.From = Canvas.GetZIndex(grd)
            zindex.To = 99

            zindex.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            grd.BeginAnimation(Canvas.LeftProperty, left)
            grd.BeginAnimation(Canvas.TopProperty, top)
            grd.BeginAnimation(Grid.WidthProperty, width)
            grd.BeginAnimation(Grid.HeightProperty, height)
            grd.BeginAnimation(Grid.OpacityProperty, opacity)
            grd.BeginAnimation(Canvas.ZIndexProperty, zindex)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Animate_Grid_Right(ByVal grd As Grid)
        Dim left As New DoubleAnimation
        Dim top As New DoubleAnimation
        Dim width As New DoubleAnimation
        Dim height As New DoubleAnimation
        Dim opacity As New DoubleAnimation
        Dim zindex As New DoubleAnimation

        Try
            left.From = Canvas.GetLeft(grd)
            left.To = 632
            left.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            top.From = Canvas.GetTop(grd)
            top.To = 117

            top.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            width.From = grd.Width
            width.To = 355

            width.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            height.From = grd.Height
            height.To = 246

            height.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            opacity.From = grd.Opacity
            opacity.To = 0.6

            opacity.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            zindex.From = Canvas.GetZIndex(grd)
            zindex.To = 97

            zindex.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            grd.BeginAnimation(Canvas.LeftProperty, left)
            grd.BeginAnimation(Canvas.TopProperty, top)
            grd.BeginAnimation(Grid.WidthProperty, width)
            grd.BeginAnimation(Grid.HeightProperty, height)
            grd.BeginAnimation(Grid.OpacityProperty, opacity)
            grd.BeginAnimation(Canvas.ZIndexProperty, zindex)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Animate_Grid_Left(ByVal grd As Grid)
        Dim left As New DoubleAnimation
        Dim top As New DoubleAnimation
        Dim width As New DoubleAnimation
        Dim height As New DoubleAnimation
        Dim opacity As New DoubleAnimation
        Dim zindex As New DoubleAnimation

        Try
            left.From = Canvas.GetLeft(grd)
            left.To = 25

            left.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            top.From = Canvas.GetTop(grd)
            top.To = 117

            top.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            width.From = grd.Width
            width.To = 355

            width.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            height.From = grd.Height
            height.To = 246

            height.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            opacity.From = grd.Opacity
            opacity.To = 0.6

            opacity.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            zindex.From = Canvas.GetZIndex(grd)
            zindex.To = 98

            zindex.Duration = New Duration(TimeSpan.FromSeconds(0.26))

            grd.BeginAnimation(Canvas.LeftProperty, left)
            grd.BeginAnimation(Canvas.TopProperty, top)
            grd.BeginAnimation(Grid.WidthProperty, width)
            grd.BeginAnimation(Grid.HeightProperty, height)
            grd.BeginAnimation(Grid.OpacityProperty, opacity)
            grd.BeginAnimation(Canvas.ZIndexProperty, zindex)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            Animate_Grid_Center(contentBlocks(1))
            Animate_Grid_Right(contentBlocks(0))
            Animate_Grid_Left(contentBlocks(2))

            Canvas.SetZIndex(contentBlocks(0), 97)
            Canvas.SetZIndex(contentBlocks(1), 99)
            Canvas.SetZIndex(contentBlocks(2), 98)

            Dim tempCB As Grid = contentBlocks(1)

            contentBlocks(1) = contentBlocks(2)
            contentBlocks(2) = contentBlocks(0)
            contentBlocks(0) = tempCB
            ' Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested()
        Catch ex As Exception

        End Try


    End Sub

    'Private Sub scannerInput_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs e) handles 

    Protected Sub scanLicense_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_scanLicense.MouseLeftButtonUp
        'buttonpress.PlaySync()
        registrationSequence()
    End Sub

    'Protected Sub priceCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_priceCheck.MouseLeftButtonUp
    '    Dim pricecheck As New PriceCheck(logo, terminal_info.StoreNum)
    '    pricecheck.ShowDialog()
    '    pricecheck.Close()
    'End Sub

    Protected Sub viewLoyalty_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_viewLoyalty.MouseLeftButtonUp
        dispatcherTimer.Stop()
        Dim viewloyalty As New ViewLoyalty(logo, terminal_info, secHash)
        viewloyalty.ShowDialog()
        viewloyalty.Close()
        GC.Collect()
        dispatcherTimer.Start()
    End Sub

    Private Sub registrationSequence()
        Dim currentCustomer As New Customer
        Try
            getTermRequirements()

            Dim scanlicense As New ScanLicense(logo)
            If scanlicense.ShowDialog() Then
                currentCustomer = scanlicense.curCustomer
                If currentCustomer.IDNum <> "" Then
                    Dim searchedCustomer As Customer = getCustomerInfo(currentCustomer.State, currentCustomer.IDNum)

                    'Check if this license has a registered customer associated with it in the winbin database
                    If searchedCustomer.located Then
                        currentCustomer = searchedCustomer

                        Dim entryreturn As List(Of String) = createEntry(currentCustomer)

                        If entryreturn(0) = "1" Then
                            Dim promodesc As String = entryreturn(1)
                            Dim printertype As String = entryreturn(2)

                            'Allow entry
                            printEntry(searchedCustomer, promodesc, printertype)
                            Dim success As New EntrySuccess(searchedCustomer, promodesc, logo, terminal_info.MacAddress, secHash)
                            success.ShowDialog()

                            currentCustomer.Clear()
                            scannerInput.Clear()
                            success.Close()
                            success = Nothing
                            GC.Collect()
                        Else
                            Dim interval As String = entryreturn(1)
                            Dim freq As String = entryreturn(2)
                            Dim intervalnum As String = entryreturn(3)

                            'No more entries allowed for today
                            Dim failure As New EntryFailure(interval, freq, intervalnum, logo)
                            failure.ShowDialog()
                            failure.Close()
                            GC.Collect()
                        End If
                    Else
                        'License not found, create a new customer
                        'First verify scanned information is correct
                        Dim verify As New VerifyLicenseInfo(currentCustomer, logo)
                        If verify.ShowDialog() Then
                            'Prompt for phone
                            Dim enterphone As New EnterPhone(terminal_info, logo)
                            Dim result As Boolean = enterphone.ShowDialog()

                            'OK means the phone number was entered, NO means the user opted to skip entering a phone number
                            If result Then

                                currentCustomer.Phone = enterphone.number

                                Dim enteremail As New EnterEmail(terminal_info, logo)

                                result = enteremail.ShowDialog()
                                If result Then

                                    currentCustomer.Email = enteremail.txtEmail.Text

                                    createNewCustomer(currentCustomer)
                                End If
                                enteremail.Close()
                            End If
                            enterphone.Close()
                        Else
                            Dim infowrong As New InfoWrong(logo)
                            infowrong.ShowDialog()
                            infowrong.Close()
                        End If

                    End If
                Else
                    Dim infowrong As New InfoWrong(logo)
                    infowrong.ShowDialog()
                    infowrong.Close()
                End If
            End If
            scanlicense.Close()
            GC.Collect()
            dispatcherTimer.Start()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub createNewCustomer(curCustomer As Customer)
        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/createNewCustomer.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("cardnum", curCustomer.CardNum)
            webpost.Add("firstname", curCustomer.FirstName)
            webpost.Add("midname", curCustomer.MidName)
            webpost.Add("lastname", curCustomer.LastName)
            webpost.Add("suffix", curCustomer.Suffix)
            webpost.Add("dob", curCustomer.DOB.ToShortDateString())
            webpost.Add("address1", curCustomer.Address1)
            webpost.Add("address2", curCustomer.Address2)
            webpost.Add("city", curCustomer.City)
            webpost.Add("state", curCustomer.State)
            webpost.Add("zip", curCustomer.Zip)
            webpost.Add("idnum", curCustomer.IDNum)
            webpost.Add("eyecolor", curCustomer.EyeColor)
            webpost.Add("gender", curCustomer.Gender)
            webpost.Add("height", curCustomer.Height)
            webpost.Add("phone", curCustomer.Phone)
            webpost.Add("email", curCustomer.Email)
            webpost.Add("sechash", secHash)

            Dim response As String = webpost.GetResponse()
            curCustomer.CardNum = response
            curCustomer.CustID = response

            Dim entryreturn As List(Of String) = createEntry(curCustomer)
            If entryreturn(0) = "1" Then
                Dim promodesc As String = entryreturn(1)
                Dim printertype As String = entryreturn(2)
                printEntry(curCustomer, promodesc, printertype)

                dispatcherTimer.Stop()
                Dim regsuccess As New EntrySuccess(curCustomer, promodesc, logo, terminal_info.MacAddress, secHash)
                regsuccess.ShowDialog()
                regsuccess.Close()
                regsuccess = Nothing
                dispatcherTimer.Start()
                GC.Collect()
            End If
        Catch
        End Try


    End Sub

    Private Sub getStoreNum()
        Dim response As String = ""
        Dim replist As New List(Of String)()

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getStoreNumber.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch ex As Exception

        End Try

        If response.Length > 0 Then
            Dim storeNum As String = response

            terminal_info.StoreNum = storeNum

        Else
            terminal_info.StoreNum = "9"
        End If

    End Sub

    Private Sub getTermRequirements()
        Dim response As String = ""
        Dim resplist As New List(Of String)()

        Try
            Dim webpost As New WebPostRequest("http://winbin.com/client/getTerminalReqs.php")
            webpost.Add("macaddr", terminal_info.MacAddress)
            webpost.Add("sechash", secHash)
            response = webpost.GetResponse()
        Catch ex As Exception

        End Try


        Dim elements As String() = response.Split(New Char() {";"c})

        If elements.Count() > 1 Then
            Dim emailflag As String = elements(0)
            Dim phoneflag As String = elements(1)
            If emailflag = "1" Then
                terminal_info.ReqEmail = True
            Else
                terminal_info.ReqEmail = False
            End If

            If phoneflag = "1" Then
                terminal_info.ReqPhone = True
            Else
                terminal_info.ReqPhone = False
            End If
        End If
    End Sub
End Class
