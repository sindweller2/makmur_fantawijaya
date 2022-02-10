Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Configuration.Provider

''' <summary>
''' Merupakan Class dari Email
''' </summary>
Public Class CommonEmails
    Private _SubjectEmails As String
    Private _ToEmails As String
    Private _FromEmails As String
    Private _BodyEmails As String
    Private _SMTPServer As String

#Region "Property"
    ''' <summary>
    ''' Subject Untuk Email
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Public Property subjectEmail() As String
        Get
            Return _SubjectEmails
        End Get
        Set(ByVal value As String)
            _SubjectEmails = value
        End Set
    End Property

    ''' <summary>
    ''' To Untuk Email
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ToEmails() As String
        Get
            Return _ToEmails
        End Get
        Set(ByVal value As String)
            _ToEmails = value
        End Set
    End Property


    ''' <summary>
    ''' From Untuk Email
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FromEmails() As String
        Get
            Return _FromEmails
        End Get
        Set(ByVal value As String)
            _FromEmails = value
        End Set
    End Property

    ''' <summary>
    ''' Body Email
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BodyEmails() As String
        Get
            Return _BodyEmails
        End Get
        Set(ByVal value As String)
            _BodyEmails = value
        End Set
    End Property


    Private Property SMTPServer() As String
        Get
            If _SMTPServer = Nothing Then
                Return System.Configuration.ConfigurationManager.AppSettings("SMTPServer").ToString
            Else
                Return _SMTPServer
            End If
        End Get
        Set(ByVal value As String)
            _SMTPServer = value
        End Set
    End Property

   
#End Region

    Public Function SendEmail() As Boolean
        Try
            Me.SendEmailFormat(FromEmails, ToEmails, subjectEmail, BodyEmails)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    
    Private Sub SendEmailFormat(ByVal _fromemail As String, ByVal _toemail As String, ByVal _subject As String, ByVal _body As String)
        Dim myemail As New System.Net.Mail.MailMessage(_fromemail, _toemail, _subject, _body)
        myemail.IsBodyHtml = True
        Dim smtpclient As New Net.Mail.SmtpClient(SMTPServer)
        smtpclient.Send(myemail)
    End Sub
End Class
