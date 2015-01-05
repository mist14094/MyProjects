Public Class EnterPhone
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Public number As String = ""

    Public Sub New(ByVal terminfo As TerminalInfo, ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 2, 0)
        dispatcherTimer.Start()
        ' This call is required by the designer.
        InitializeComponent()
        Image1.Source = logo
        txtPhone.Text = "(   )   -    "
        If Not terminfo.ReqPhone Then
            btnSkip.Visibility = Windows.Visibility.Visible
        Else
            btnSkip.Visibility = Windows.Visibility.Hidden
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub Dispose()
        btnNext.Visibility = Windows.Visibility.Hidden
        txtPhone.Text = Nothing
        Image1.Source = Nothing
        dispatcherTimer.Stop()
        dispatcherTimer = Nothing
    End Sub
    Private Sub dispatcherTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        ' Updating the Label which displays the current second
        Try
            Dispose()
            Me.DialogResult = False
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowNumber()
        txtPhone.Text = "("

        For i As Integer = 0 To 2
            If i < number.Length Then
                txtPhone.Text += number(i)
            Else
                txtPhone.Text += " "
            End If
        Next
        txtPhone.Text += ")"
        For i As Integer = 3 To 5
            If i < number.Length Then
                txtPhone.Text += number(i)
            Else
                txtPhone.Text += " "
            End If
        Next
        txtPhone.Text += "-"
        For i As Integer = 6 To 9
            If i < number.Length Then
                txtPhone.Text += number(i)
            Else
                txtPhone.Text += " "
            End If
        Next

        If number.Length = 10 Then
            btnNext.Visibility = Windows.Visibility.Visible
        Else
            btnNext.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub addnum(ByVal num As String)
        If number.Length < 10 Then
            number += num
            ShowNumber()
        End If
    End Sub

    Private Sub backspace_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackspace.Click
        If number.Length > 0 Then
            number = number.Substring(0, number.Length - 1)
            ShowNumber()
        End If
    End Sub

    Private Sub clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.MouseLeftButtonUp
        number = ""
        ShowNumber()
    End Sub

    Private Sub next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.MouseLeftButtonUp
        If number.Length = 10 Then
            Dispose()
            Me.DialogResult = True
            Me.Close()
        End If
    End Sub

    Private Sub cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeftButtonUp
        Dispose()
        Me.DialogResult = False
        Me.Close()
    End Sub

    Private Sub skip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSkip.MouseLeftButtonUp
        Dispose()
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub numberClick(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btn0.Click
        addnum(DirectCast(sender, Button).Content.ToString)
    End Sub
End Class
