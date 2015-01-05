Imports System
Imports System.Net
Imports System.Web
Imports System.Collections
Imports System.IO

Public Class WebPostRequest
    Dim theRequest As WebRequest
    Dim theResponse As WebResponse
    Dim theQueryData As ArrayList

    Public Sub New(ByVal url As String)
        Try
            theRequest = WebRequest.Create(url)
            theRequest.Method = "POST"
            theRequest.Proxy = Nothing
            theQueryData = New ArrayList
        Catch ex As Exception

        End Try
    End Sub

    Public Sub Add(ByVal key As String, ByVal value As String)
        Try
            theQueryData.Add(String.Format("{0}={1}", key, HttpUtility.UrlEncode(value)))
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetResponse() As String
        Dim response As String = Nothing
        Try
            'set the encoding type
            theRequest.ContentType = "application/x-www-form-urlencoded"

            'Build a string containing all the parameters
            Dim Parameters As String = [String].Join("&", DirectCast(theQueryData.ToArray(GetType(String)), [String]()))
            theRequest.ContentLength = Parameters.Length

            'We write the parameters into the request
            Dim sw As New StreamWriter(theRequest.GetRequestStream())
            sw.Write(Parameters)
            sw.Close()

            'Execute the query
            theResponse = theRequest.GetResponse
            Dim sr As New StreamReader(theResponse.GetResponseStream)
            response = sr.ReadToEnd
            Return response
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetResponseStream() As Stream
        Try
            'set the encoding type
            theRequest.ContentType = "application/x-www-form-urlencoded"

            'Build a string containing all the parameters
            Dim Parameters As String = [String].Join("&", DirectCast(theQueryData.ToArray(GetType(String)), [String]()))
            theRequest.ContentLength = Parameters.Length

            'We write the parameters into the request
            Dim sw As New StreamWriter(theRequest.GetRequestStream())
            sw.Write(Parameters)
            sw.Close()

            'Execute the query
            theResponse = theRequest.GetResponse

            Dim retstream As Stream = theResponse.GetResponseStream
            Return retstream
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
