Imports System.Web.Configuration
Imports System.Data.OleDb
Imports System.Data

Partial Class _default
    Inherits System.Web.UI.MasterPage

    Public tradingClass As tradingClass = New tradingClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Try
            If BrowserCompatibility.IsUplevel Then
                Page.ClientTarget = "uplevel"
            End If
        Catch ex As Exception
            tradingClass.Alert(ex.Message, Me.Page)
        End Try
    End Sub
End Class

