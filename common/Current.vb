Imports System.Web.Security
Imports System.Data
'Imports System.Data.SqlClient
Public Class Current
    'Inherits DBConString
    Public Shared ReadOnly Property UserID() As Integer
        Get
            Dim o As Object = System.Web.HttpContext.Current.Session("UserID")
            If Not o Is Nothing Then
                Return CInt(o)
            Else
                System.Web.HttpContext.Current.Session("UserID") = CInt(Membership.GetUser.ProviderUserKey)
                Return CInt(System.Web.HttpContext.Current.Session("UserID"))
            End If
        End Get
    End Property

    Public Shared Sub ClearAllSession()
        System.Web.HttpContext.Current.Session("UserID") = Nothing
    End Sub
End Class
