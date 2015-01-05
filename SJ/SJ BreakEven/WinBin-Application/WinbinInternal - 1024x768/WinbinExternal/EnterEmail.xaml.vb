Public Class EnterEmail
    Dim dispatcherTimer As New Windows.Threading.DispatcherTimer()

    Public Sub New(ByVal terminfo As TerminalInfo, ByVal logo As BitmapImage)
        AddHandler dispatcherTimer.Tick, AddressOf dispatcherTimer_Tick
        dispatcherTimer.Interval = New TimeSpan(0, 2, 0)
        dispatcherTimer.Start()
        ' This call is required by the designer.
        InitializeComponent()
        Image1.Source = logo
        txtEmail.Text = ""
        txtEmail.IsUndoEnabled = False
        If Not terminfo.ReqEmail Then
            btnSkip.Visibility = Windows.Visibility.Visible
        Else
            btnSkip.Visibility = Windows.Visibility.Hidden
        End If
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub Dispose()
        btnNext.Visibility = Windows.Visibility.Hidden
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

    Private Function CheckEmail() As Boolean
        If txtEmail.Text <> "" Then
            If txtEmail.Text.IndexOf("@") <> -1 Then
                If txtEmail.Text.IndexOf("@") = txtEmail.Text.LastIndexOf("@") Then
                    If txtEmail.Text.IndexOf(".") <> -1 Then
                        If txtEmail.Text.IndexOf("@") < txtEmail.Text.LastIndexOf(".") Then
                            Return True
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Private Sub ButtonPress(button As String)
        If txtEmail.Text.Length < 50 Then
            txtEmail.Text += button

            txtEmail.CaretIndex = txtEmail.Text.Length
            Dim rect = txtEmail.GetRectFromCharacterIndex(txtEmail.CaretIndex)
            txtEmail.ScrollToHorizontalOffset(rect.Right)
        End If

        If CheckEmail() Then
            btnNext.Visibility = Windows.Visibility.Visible
        Else
            btnNext.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub backspace_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBackspace.Click
        If txtEmail.Text.Length > 0 Then
            txtEmail.Text = txtEmail.Text.Substring(0, txtEmail.Text.Length - 1)

            If CheckEmail() Then
                btnNext.Visibility = Windows.Visibility.Visible
            Else
                btnNext.Visibility = Windows.Visibility.Hidden
            End If
        End If
    End Sub

    Private Sub clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.MouseLeftButtonUp
        txtEmail.Text = ""
        btnNext.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub next_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.MouseLeftButtonUp
        If CheckEmail() Then
            Me.DialogResult = True
            Me.Close()
        End If
    End Sub

    Private Sub skip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSkip.MouseLeftButtonUp
        txtEmail.Text = ""
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeftButtonUp
        Me.DialogResult = False
        Me.Close()
    End Sub

    Private Sub button_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click,
        btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btnQ.Click, btnW.Click, btnE.Click, btnR.Click, btnT.Click, btnY.Click, btnU.Click, btnI.Click,
        btnO.Click, btnP.Click, btnA.Click, btnS.Click, btnD.Click, btnF.Click, btnG.Click, btnH.Click, btnJ.Click, btnK.Click, btnL.Click, btnZ.Click, btnX.Click, btnC.Click, btnV.Click,
        btnB.Click, btnN.Click, btnM.Click, btnUS.Click, btnDot.Click, btnDash.Click, btnAt.Click, btnGmail.Click, btnYahoo.Click, btnHotmail.Click, btnCom.Click, btnNet.Click, btnOrg.Click

        ButtonPress(DirectCast(sender, Button).Content.ToString.ToLower)
    End Sub
End Class
