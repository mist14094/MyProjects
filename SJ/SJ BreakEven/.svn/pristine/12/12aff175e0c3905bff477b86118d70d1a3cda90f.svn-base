Imports System.IO.Ports
Imports System.Runtime.InteropServices

Public Class EntrySuccess
    Dim WithEvents phidgetIFK As Phidgets.InterfaceKit
    Dim WithEvents phidgetIFK1 As Phidgets.InterfaceKit
    Dim WithEvents phidgetIFK2 As Phidgets.InterfaceKit
    Dim WithEvents phidgetIFK3 As Phidgets.InterfaceKit
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

        dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 8)
        dispatcherTimer.Start()
        ScanTrigger = False
        TiltTrigger = False
        TriggerCount = 0
        ScanLocation = 0
        Try
            TriggerArray.Clear()

            curMacAddr = MacAddr
            curSecHash = secHash
            curCustomer = customer

            Image1.Source = logo
            lblCustomer.Content = "Hello " + customer.FirstName + Environment.NewLine
            lblPromo.Content = Environment.NewLine + promo
        Catch e As Exception

        End Try

    End Sub

    Private Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SourceInitialized
        Try
            phidgetIFK = New Phidgets.InterfaceKit
            phidgetIFK1 = New Phidgets.InterfaceKit
            phidgetIFK2 = New Phidgets.InterfaceKit
            phidgetIFK3 = New Phidgets.InterfaceKit
            phidgetIFK.open(336806)
            phidgetIFK1.open(275782)
            phidgetIFK2.open(327159)
            phidgetIFK3.open(327282)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            dispatcherTimer.Stop()
            dispatcherTimer = Nothing


            phidgetIFK.close()
            phidgetIFK = Nothing
            phidgetIFK1.close()
            phidgetIFK1 = Nothing
            phidgetIFK2.close()
            phidgetIFK2 = Nothing
            phidgetIFK3.close()
            phidgetIFK3 = Nothing

            If ScanTrigger And ScanLocation <> 0 Then
                If Not TiltTrigger Then
                    Dim winner As New InstantWinner(curCustomer, ScanLocation, Image1.Source, curMacAddr, curSecHash)
                    curCustomer.Clear()
                    TriggerArray.Clear()
                    winner.ShowDialog()
                    winner.Close()
                    winner = Nothing
                Else
                    'Blacklist Customer
                End If
            End If

            Dispose()
            Me.DialogResult = True
            Me.Close()

        Catch ex As Exception
            Dispose()
            Me.DialogResult = True
            Me.Close()
        End Try
    End Sub

    Public Sub Dispose()
        Try

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

        Catch ex As Exception

        End Try
    End Sub

    Private Sub phidgetIFK_SensorChange(ByVal sender As Object, ByVal e As Phidgets.Events.SensorChangeEventArgs) Handles phidgetIFK.SensorChange
        Try

       
            If (Not TriggerArray.Contains(e.Index)) And e.Value < 950 Then
                Console.WriteLine(e.Index.ToString + " has value of " + e.Value.ToString)
                TriggerArray.Add(e.Index)
            End If

            If Not ScanTrigger And e.Value < 960 Then
                ScanTrigger = True
                Try
                    If e.Value < 960 Then
                        Select Case e.Index
                            Case 0
                                ScanLocation = 1
                            Case 1
                                ScanLocation = 1
                            Case 2
                                ScanLocation = 1
                            Case 3
                                ScanLocation = 1
                            Case 4
                                ScanLocation = 2
                            Case 5
                                ScanLocation = 2
                            Case 6
                                ScanLocation = 2
                            Case 7
                                ScanLocation = 2
                        End Select

                        dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 0, 250)
                    End If
                Catch ex As Exception

                End Try
            ElseIf phidgetIFK.sensors(e.Index).Value < 960 Then
                If TriggerArray.Count >= 5 Then
                    TiltTrigger = True
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub phidgetIFK1_SensorChange(ByVal sender As Object, ByVal e As Phidgets.Events.SensorChangeEventArgs) Handles phidgetIFK1.SensorChange
        Try

            If (Not TriggerArray.Contains(e.Index)) And e.Value < 950 Then
                Console.WriteLine(e.Index.ToString + " has value of " + e.Value.ToString)
                TriggerArray.Add(e.Index)
            End If

            If Not ScanTrigger And e.Value < 960 Then
                ScanTrigger = True
                Try
                    If e.Value < 960 Then
                        Select Case e.Index
                            Case 0
                                ScanLocation = 3
                            Case 1
                                ScanLocation = 3
                            Case 2
                                ScanLocation = 3
                            Case 3
                                ScanLocation = 3
                            Case 4
                                ScanLocation = 4
                            Case 5
                                ScanLocation = 4
                            Case 6
                                ScanLocation = 4
                            Case 7
                                ScanLocation = 4
                        End Select

                        dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 0, 250)
                    End If
                Catch ex As Exception

                End Try
            ElseIf phidgetIFK1.sensors(e.Index).Value < 960 Then
                If TriggerArray.Count >= 5 Then
                    TiltTrigger = True
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub phidgetIFK2_SensorChange(ByVal sender As Object, ByVal e As Phidgets.Events.SensorChangeEventArgs) Handles phidgetIFK2.SensorChange
        Try

            If (Not TriggerArray.Contains(e.Index)) And e.Value < 950 Then
                Console.WriteLine(e.Index.ToString + " has value of " + e.Value.ToString)
                TriggerArray.Add(e.Index)
            End If

            If Not ScanTrigger And e.Value < 960 Then
                ScanTrigger = True
                Try
                    If e.Value < 960 Then
                        Select Case e.Index
                            Case 0
                                ScanLocation = 5
                            Case 1
                                ScanLocation = 5
                            Case 2
                                ScanLocation = 5
                            Case 3
                                ScanLocation = 5
                            Case 4
                                ScanLocation = 6
                            Case 5
                                ScanLocation = 6
                            Case 6
                                ScanLocation = 6
                            Case 7
                                ScanLocation = 6
                        End Select

                        dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 0, 250)
                    End If
                Catch ex As Exception

                End Try
            ElseIf phidgetIFK2.sensors(e.Index).Value < 960 Then
                If TriggerArray.Count >= 5 Then
                    TiltTrigger = True
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub phidgetIFK3_SensorChange(ByVal sender As Object, ByVal e As Phidgets.Events.SensorChangeEventArgs) Handles phidgetIFK3.SensorChange
        Try

            If (Not TriggerArray.Contains(e.Index)) And e.Value < 950 Then
                Console.WriteLine(e.Index.ToString + " has value of " + e.Value.ToString)
                TriggerArray.Add(e.Index)
            End If

            If Not ScanTrigger And e.Value < 960 Then
                ScanTrigger = True
                Try
                    If e.Value < 960 Then
                        Select Case e.Index
                            Case 0
                                ScanLocation = 7
                            Case 1
                                ScanLocation = 7
                            Case 2
                                ScanLocation = 7
                            Case 3
                                ScanLocation = 7
                            Case 4
                                ScanLocation = 8
                            Case 5
                                ScanLocation = 8
                            Case 6
                                ScanLocation = 8
                            Case 7
                                ScanLocation = 8
                        End Select

                        dispatcherTimer.Interval = New TimeSpan(0, 0, 0, 0, 250)
                    End If
                Catch ex As Exception

                End Try
            ElseIf phidgetIFK3.sensors(e.Index).Value < 960 Then
                If TriggerArray.Count >= 5 Then
                    TiltTrigger = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub phidgetIFK_Attach(ByVal sender As Object, ByVal e As Phidgets.Events.AttachEventArgs) Handles phidgetIFK.Attach
        Try

            Dim i As Integer
            For i = 0 To phidgetIFK.sensors.Count - 1
                phidgetIFK.sensors(i).Sensitivity = 0
                phidgetIFK.ratiometric = False
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub phidgetIFK1_Attach(ByVal sender As Object, ByVal e As Phidgets.Events.AttachEventArgs) Handles phidgetIFK1.Attach
        Try

            Dim i As Integer
            For i = 0 To phidgetIFK1.sensors.Count - 1
                phidgetIFK1.sensors(i).Sensitivity = 0
                phidgetIFK1.ratiometric = False
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub phidgetIFK2_Attach(ByVal sender As Object, ByVal e As Phidgets.Events.AttachEventArgs) Handles phidgetIFK2.Attach
        Try

            Dim i As Integer
            For i = 0 To phidgetIFK2.sensors.Count - 1
                phidgetIFK2.sensors(i).Sensitivity = 0
                phidgetIFK2.ratiometric = False
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub phidgetIFK3_Attach(ByVal sender As Object, ByVal e As Phidgets.Events.AttachEventArgs) Handles phidgetIFK3.Attach
        Try

            Dim i As Integer
            For i = 0 To phidgetIFK3.sensors.Count - 1
                phidgetIFK3.sensors(i).Sensitivity = 0
                phidgetIFK3.ratiometric = False
            Next

        Catch ex As Exception

        End Try
    End Sub
End Class
