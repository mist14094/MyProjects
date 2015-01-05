Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class Customer
    Public CustID As String = ""
    Public CardNum As String = ""
    Public FirstName As String = ""
    Public LastName As String = ""
    Public MidName As String = ""
    Public Suffix As String = ""
    Public Address1 As String = ""
    Public Address2 As String = ""
    Public City As String = ""
    Public State As String = ""
    Public Zip As String = ""
    Public IDNum As String = ""
    Public DOB As New DateTime()
    Public Gender As String = ""
    Public Height As String = ""
    Public EyeColor As String = ""
    Public Phone As String = ""
    Public Email As String = ""
    Public located As Boolean

    Public Sub Clear()
        CustID = ""
        CardNum = ""
        FirstName = ""
        LastName = ""
        MidName = ""
        Suffix = ""
        Address1 = ""
        Address2 = ""
        City = ""
        State = ""
        Zip = ""
        IDNum = ""
        DOB = New DateTime()
        Gender = ""
        Height = ""
        EyeColor = ""
        Phone = ""
        Email = ""
        located = False
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Clear()
        Finally
            MyBase.Finalize()
        End Try
    End Sub
End Class
