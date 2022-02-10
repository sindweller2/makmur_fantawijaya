Public Class GlobalVariable
    Public Shared ReadOnly Property DefaultDateFormat() As String
        Get
            Return GetAppSetting("DefaultDateFormat")
        End Get
    End Property


    Private Shared Function GetAppSetting(ByVal key As String) As String
        Return System.Configuration.ConfigurationManager.AppSettings(key).ToString
    End Function
End Class
