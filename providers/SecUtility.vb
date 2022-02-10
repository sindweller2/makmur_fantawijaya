Imports System.Configuration.Provider
Imports System.Data.SqlClient
Imports System.Collections.Specialized

Public Class SecUtility
    Public Shared Sub CheckArrayParameter(ByRef param As String(), ByVal checkForNull As Boolean, ByVal checkIfEmpty As Boolean, ByVal checkForCommas As Boolean, ByVal maxSize As Integer, ByVal paramName As String)
        If (param Is Nothing) Then
            Throw New ArgumentNullException(paramName)
        End If
        If (param.Length < 1) Then
            Throw New ArgumentException("Parameter array empty", paramName)
        End If
        Dim hashtable As New Hashtable(param.Length)
        Dim i As Integer = (param.Length - 1)
        Do While (i >= 0)
            CheckParameter((param(i)), checkForNull, checkIfEmpty, checkForCommas, maxSize, (paramName & "[ " & i.ToString(Globalization.CultureInfo.InvariantCulture) & " ]"))
            If hashtable.Contains(param(i)) Then
                Throw New ArgumentException("Parameter duplicate array element", paramName)
            End If
            hashtable.Add(param(i), param(i))
            i -= 1
        Loop
    End Sub
    Public Shared Sub CheckParameter(ByRef param As String, ByVal checkForNull As Boolean, ByVal checkIfEmpty As Boolean, ByVal checkForCommas As Boolean, ByVal maxSize As Integer, ByVal paramName As String)
        If (param Is Nothing) Then
            If checkForNull Then
                Throw New ArgumentNullException(paramName)
            End If
        Else
            param = param.Trim
            If (checkIfEmpty AndAlso (param.Length < 1)) Then
                Throw New ArgumentException("Parameter can not be empty", paramName)
            End If
            If ((maxSize > 0) AndAlso (param.Length > maxSize)) Then
                Throw New ArgumentException("Parameter too long", paramName)
            End If
            If (checkForCommas AndAlso param.Contains(",")) Then
                Throw New ArgumentException("Parameter can not contain comma", paramName)
            End If
        End If
    End Sub
    Public Shared Sub CheckPasswordParameter(ByRef param As String, ByVal maxSize As Integer, ByVal paramName As String)
        If (param Is Nothing) Then
            Throw New ArgumentNullException(paramName)
        End If
        If (param.Length < 1) Then
            Throw New ArgumentException("Parameter can not be empty", paramName)
        End If
        If ((maxSize > 0) AndAlso (param.Length > maxSize)) Then
            Throw New ArgumentException("Parameter too long", paramName)
        End If
    End Sub

    Public Shared Sub CheckSchemaVersion(ByVal provider As ProviderBase, ByVal connection As SqlClient.SqlConnection, ByVal features As String(), ByVal version As String, ByRef schemaVersionCheck As Integer)
        If (connection Is Nothing) Then
            Throw New ArgumentNullException("connection")
        End If
        If (features Is Nothing) Then
            Throw New ArgumentNullException("features")
        End If
        If (version Is Nothing) Then
            Throw New ArgumentNullException("version")
        End If
        If (schemaVersionCheck = -1) Then
            Throw New ProviderException("Provider Schema Version Not Match")
        End If
        If (schemaVersionCheck = 0) Then
            SyncLock provider
                If (schemaVersionCheck = -1) Then
                    Throw New ProviderException("Provider Schema Version Not Match")
                End If
                If (schemaVersionCheck = 0) Then
                    Dim command As SqlClient.SqlCommand = Nothing
                    Dim parameter As SqlClient.SqlParameter = Nothing
                    Dim text As String
                    For Each text In features
                        command = New SqlClient.SqlCommand("dbo.aspnet_CheckSchemaVersion", connection)
                        command.CommandType = CommandType.StoredProcedure
                        parameter = New SqlClient.SqlParameter("@Feature", [text])
                        command.Parameters.Add(parameter)
                        parameter = New SqlClient.SqlParameter("@CompatibleSchemaVersion", version)
                        command.Parameters.Add(parameter)
                        parameter = New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                        parameter.Direction = ParameterDirection.ReturnValue
                        command.Parameters.Add(parameter)
                        command.ExecuteNonQuery()
                        If (IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1) <> 0) Then
                            schemaVersionCheck = -1
                            Throw New ProviderException("Provider Schema Version Not Match")
                        End If
                    Next
                    schemaVersionCheck = 1
                End If
            End SyncLock
        End If
    End Sub
    Public Shared Function GetBooleanValue(ByVal config As NameValueCollection, ByVal valueName As String, ByVal defaultValue As Boolean) As Boolean
        Dim result As Boolean
        Dim text As String = config.Item(valueName)
        If ([text] Is Nothing) Then
            Return defaultValue
        End If
        If Not Boolean.TryParse([text], result) Then
            Throw New ProviderException("Value must be boolean")
        End If
        Return result
    End Function

    Public Shared Function GetIntValue(ByVal config As NameValueCollection, ByVal valueName As String, ByVal defaultValue As Integer, ByVal zeroAllowed As Boolean, ByVal maxValueAllowed As Integer) As Integer
        Dim result As Integer
        Dim s As String = config.Item(valueName)
        If (s Is Nothing) Then
            Return defaultValue
        End If
        If Not Integer.TryParse(s, result) Then
            If zeroAllowed Then
                Throw New ProviderException("Value must be non negative integer")
            End If
            Throw New ProviderException("Value must be positive integer")
        End If
        If (zeroAllowed AndAlso (result < 0)) Then
            Throw New ProviderException("Value must be non negative integer")
        End If
        If (Not zeroAllowed AndAlso (result <= 0)) Then
            Throw New ProviderException("Value must be positive integer")
        End If
        If ((maxValueAllowed > 0) AndAlso (result > maxValueAllowed)) Then
            Throw New ProviderException("Value too big")
        End If
        Return result
    End Function

    Public Shared Function CreateInputParam(ByVal paramName As String, ByVal dbType As SqlDbType, ByVal objValue As Object) As SqlClient.SqlParameter
        Dim parameter As New SqlClient.SqlParameter(paramName, dbType)
        If (objValue Is Nothing) Then
            parameter.IsNullable = True
            parameter.Value = DBNull.Value
            Return parameter
        End If
        parameter.Value = objValue
        Return parameter
    End Function

    Public Shared Function GetReturnValue(ByVal cmd As SqlClient.SqlCommand) As Integer
        Dim parameter As SqlClient.SqlParameter
        For Each parameter In cmd.Parameters
            If (((parameter.Direction = ParameterDirection.ReturnValue) AndAlso (Not parameter.Value Is Nothing)) AndAlso TypeOf parameter.Value Is Integer) Then
                Return CInt(parameter.Value)
            End If
        Next
        Return -1
    End Function
    Public Shared Function ValidateParameter(ByRef param As String, ByVal checkForNull As Boolean, ByVal checkIfEmpty As Boolean, ByVal checkForCommas As Boolean, ByVal maxSize As Integer) As Boolean
        If (param Is Nothing) Then
            Return Not checkForNull
        End If
        param = param.Trim
        Return (((Not checkIfEmpty OrElse (param.Length >= 1)) AndAlso ((maxSize <= 0) OrElse (param.Length <= maxSize))) AndAlso (Not checkForCommas OrElse Not param.Contains(",")))
    End Function

    Public Shared Function ValidatePasswordParameter(ByRef param As String, ByVal maxSize As Integer) As Boolean
        If (param Is Nothing) Then
            Return False
        End If
        If (param.Length < 1) Then
            Return False
        End If
        If ((maxSize > 0) AndAlso (param.Length > maxSize)) Then
            Return False
        End If
        Return True
    End Function
End Class
