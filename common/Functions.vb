Imports System.Web.Security
Imports System.Data
Imports System.Data.SqlClient
Imports FE_SymmetricNamespace
Imports System.Security.Cryptography
Public Class Functions
    Public Shared Function GetDateFormatFromString(ByVal value As String) As Date
        Try
            Dim s As String() = value.Split("/")
            Dim d As New Date(Val(s(2)), Val(s(1)), Val(s(0)))
            Return d
        Catch ex As Exception
            Return Date.MinValue
        End Try
    End Function

    Public Shared Function FormatDateTextBox(ByVal value As Date) As String
        Return Format(value, "dd/MM/yyyy")
    End Function
    Public Shared Function FormatTime(ByVal value As Date) As String
        Return Format(value, "HH:mm")
    End Function

    Public Shared Function FormatDateTime(ByVal value As Date) As String
        Return Format(value, "dd MMM yyyy HH:mm")
    End Function
    Public Shared Function FormatDate(ByVal value As Date) As String
        Return Format(value, "dd/MM/yyyy")
    End Function
    Public Shared Function FormatDateToString(ByVal value As Date) As String
        Return Format(value, GlobalVariable.DefaultDateFormat)
    End Function
    
    Public Shared Function FormatDecryptToEncrypt(ByVal hash_type As String, ByVal nilai As String) As String
        Dim gKey As String = Nothing
        Dim gstrSource As String = Nothing
        Dim gstrProcess As String = Nothing
        Dim hasil As String = Nothing

        gKey = hash_type
        gstrSource = nilai
        gstrProcess = hasil
        Dim feService As New FE_Symmetric

        Dim strTmp As String
        Dim i As Integer = 0
        strTmp = feService.EncryptData(gKey, gstrSource)
        'Result.Text = "<b>Encrypted Data Length</b> = " + strTmp.Length.ToString()
        hasil = strTmp
        Return hasil
    End Function

    Public Shared Function FormatEncryptToDecrypt(ByVal hash_type As String, ByVal hasil As String) As String
        Dim gKey As String = Nothing
        Dim gstrSource As String = Nothing
        Dim gstrProcess As String = Nothing
        Dim nilai As String = Nothing

        gKey = hash_type
        gstrSource = hasil
        gstrProcess = nilai

        Dim feService As New FE_Symmetric

        Dim strTmp As String
        strTmp = feService.DecryptData(gKey, gstrSource)
        'Result.Text = "<b>Decrypted Data Length</b> = " + strTmp.Length.ToString()
        nilai = strTmp
        Dim bpDe As New Byte()
        bpDe = strTmp.Length
        Dim a As New Text.ASCIIEncoding
        a.GetBytes(strTmp, 0, strTmp.Length)
        Dim i As Integer = 0
        Do While i < strTmp.Length
            Trace.Write(i.ToString() + "=" + strTmp(i) + "=")
            i = i + 1
        Loop
        Return nilai
    End Function
End Class
