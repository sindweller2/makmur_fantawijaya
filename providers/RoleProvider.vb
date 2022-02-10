Imports System.Web
Imports System.Web.HttpRuntime
Imports System.Web.Configuration
Imports System.Configuration.Provider
Imports System.Collections.Specialized
Imports System.Web.Security
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Cryptography
Imports System.Runtime.InteropServices
Imports System.Web.Management
Imports System.Globalization

Public Class RoleProvider
    Inherits System.Web.Security.RoleProvider

#Region "Fields"
    Private _AppName As String
    Private _CommandTimeout As Integer
    Private _SchemaVersionCheck As Integer
    Private _sqlConnectionString As String
#End Region
#Region "Properties"
    Public Overrides Property ApplicationName() As String
        Get
            Return Me._AppName
        End Get
        Set(ByVal value As String)
            If String.IsNullOrEmpty(value) Then
                Throw New ArgumentException("Property Null or Empty")
            End If
            If (value.Length > &H100) Then
                Throw New ProviderException("Provider application name too long")
            End If
            Me._AppName = value
        End Set
    End Property

    Private ReadOnly Property CommandTimeout() As Integer
        Get
            Return Me._CommandTimeout
        End Get
    End Property
#End Region

#Region "Useless Method"
    Public Overrides Sub AddUsersToRoles(ByVal usernames() As String, ByVal roleNames() As String)
        'Providers.SecUtility.CheckArrayParameter((roleNames), True, True, True, &H100, "roleNames")
        'Providers.SecUtility.CheckArrayParameter((usernames), True, True, True, &H100, "usernames")
        'Dim flag As Boolean = False
        'Try
        '    Dim connection As SqlClient.SqlConnection = Nothing
        '    Try
        '        connection = New SqlClient.SqlConnection(Me._sqlConnectionString)
        '        Dim length As Integer = usernames.Length
        '        Do While (length > 0)
        '            Dim index As Integer
        '            Dim text As String = usernames((usernames.Length - length))
        '            length -= 1
        '            For index = (usernames.Length - length) To usernames.Length - 1
        '                If ((([text].Length + usernames(index).Length) + 1) >= &HFA0) Then
        '                    Exit For
        '                End If
        '                [text] = ([text] & "," & usernames(index))
        '                length -= 1
        '            Next index
        '            Dim num3 As Integer = roleNames.Length
        '            Do While (num3 > 0)
        '                Dim text2 As String = roleNames((roleNames.Length - num3))
        '                num3 -= 1
        '                For index = (roleNames.Length - num3) To roleNames.Length - 1
        '                    If (((text2.Length + roleNames(index).Length) + 1) >= &HFA0) Then
        '                        Exit For
        '                    End If
        '                    text2 = (text2 & "," & roleNames(index))
        '                    num3 -= 1
        '                Next index
        '                If (Not flag AndAlso ((length > 0) OrElse (num3 > 0))) Then
        '                New SqlClient.SqlCommand("BEGIN TRANSACTION", connection.Connection).ExecuteNonQuery
        '                    flag = True
        '                End If
        '                Me.AddUsersToRolesCore(connection, [text], text2)
        '            Loop
        '        Loop
        '        If flag Then
        '            'New SqlClient.SqlCommand("COMMIT TRANSACTION", connection.Connection).ExecuteNonQuery
        '            flag = False
        '        End If
        '    Catch obj1 As Object
        '        If flag Then
        '            Try
        '            New SqlClient.SqlCommand("ROLLBACK TRANSACTION", connection.Connection).ExecuteNonQuery
        '            Catch obj2 As Object
        '            End Try
        '            flag = False
        '        End If
        '        Throw
        '    Finally
        '        If (Not connection Is Nothing) Then
        '            connection.Close()
        '            connection = Nothing
        '        End If
        '    End Try
        'Catch obj3 As Object
        '    Throw
        'End Try
    End Sub
    Private Sub AddUsersToRolesCore(ByVal conn As SqlClient.SqlConnection, ByVal usernames As String, ByVal roleNames As String)
        'Dim cmd As New SqlClient.SqlCommand("dbo.aspnet_UsersInRoles_AddUsersToRoles", conn)
        'Dim reader As SqlClient.SqlDataReader = Nothing
        'Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
        'Dim text As String = String.Empty
        'Dim text2 As String = String.Empty
        'cmd.CommandType = CommandType.StoredProcedure
        'parameter.Direction = ParameterDirection.ReturnValue
        'cmd.Parameters.Add(parameter)
        'cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@RoleNames", sqldbtype.VarChar, roleNames))
        'cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserNames", sqldbtype.VarChar, usernames))
        'cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@CurrentTimeUtc", oleDbType.Date, DateTime.UtcNow))
        'Try
        '    reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
        '    If reader.Read Then
        '        If (reader.FieldCount > 0) Then
        '            [text] = reader.GetString(0)
        '        End If
        '        If (reader.FieldCount > 1) Then
        '            text2 = reader.GetString(1)
        '        End If
        '    End If
        'Finally
        '    If (Not reader Is Nothing) Then
        '        reader.Close()
        '    End If
        'End Try
        'Select Case Providers.SecUtility.GetReturnValue(cmd)
        '    Case 0
        '        Return
        '    Case 1
        '        Throw New ProviderException("Provider this user not found")
        '    Case 2
        '        Throw New ProviderException("Provider role not found")
        '    Case 3
        '        Throw New ProviderException("Provider this user already in role")
        'End Select
        'Throw New ProviderException("Provider unknown failure")
    End Sub
    Public Overrides Sub CreateRole(ByVal roleName As String)

    End Sub
    Public Overrides Function DeleteRole(ByVal roleName As String, ByVal throwOnPopulatedRole As Boolean) As Boolean

    End Function
    Public Overrides Sub RemoveUsersFromRoles(ByVal usernames() As String, ByVal roleNames() As String)

    End Sub
#End Region


#Region "Methods"
    Public Overrides Function FindUsersInRole(ByVal code_menu As String, ByVal username As String) As String()
        Dim textArray2 As String()
        providers.SecUtility.CheckParameter((code_menu), True, True, True, &H100, "code_menu")
        providers.SecUtility.CheckParameter((username), True, True, False, &H100, "username")
        Try
            Dim connection As SqlClient.SqlConnection = Nothing
            Try
                connection = New SqlClient.SqlConnection(Me._sqlConnectionString)

                Dim cmd As New SqlClient.SqlCommand("dbo.aspnet_menu_list_FindUsersInMenu", connection)
                Dim reader As SqlClient.SqlDataReader = Nothing
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                Dim strings As New StringCollection
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = Me.CommandTimeout
                parameter.Direction = ParameterDirection.ReturnValue
                cmd.Parameters.Add(parameter)
                cmd.Parameters.Add(providers.SecUtility.CreateInputParam("@code_menu", SqlDbType.VarChar, code_menu))
                cmd.Parameters.Add(providers.SecUtility.CreateInputParam("@username", SqlDbType.VarChar, username))
                Try
                    reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                    Do While reader.Read
                        strings.Add(reader.GetString(0))
                    Loop
                Catch obj1 As Exception
                    Throw
                Finally
                    If (Not reader Is Nothing) Then
                        reader.Close()
                    End If
                End Try
                If (strings.Count < 1) Then
                    Select Case providers.SecUtility.GetReturnValue(cmd)
                        Case 0
                            Return New String(0 - 1) {}
                        Case 1
                            Throw New ProviderException("Provider menu not found")
                    End Select
                    Throw New ProviderException("Provider unknown failure")
                End If
                Dim array As String() = New String(strings.Count - 1) {}
                strings.CopyTo(array, 0)
                textArray2 = array
            Finally
                If (Not connection Is Nothing) Then
                    connection.Close()
                    connection = Nothing
                End If
            End Try
        Catch obj2 As Exception
            Throw
        End Try
        Return textArray2
    End Function
    Public Overrides Function GetAllRoles() As String()
        Dim textArray2 As String()
        Try
            Dim connection As SqlClient.SqlConnection = Nothing
            Try
                Dim cmd As New SqlClient.SqlCommand("dbo.aspnet_menu_list_GetAllMenus", connection)
                Dim strings As New StringCollection
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                Dim reader As SqlClient.SqlDataReader = Nothing
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = Me.CommandTimeout
                parameter.Direction = ParameterDirection.ReturnValue
                cmd.Parameters.Add(parameter)
                Try
                    reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
                    Do While reader.Read
                        strings.Add(reader.GetString(0))
                    Loop
                Finally
                    If (Not reader Is Nothing) Then
                        reader.Close()
                    End If
                End Try
                Dim array As String() = New String(strings.Count - 1) {}
                strings.CopyTo(array, 0)
                textArray2 = array
            Finally
                connection.Dispose()
            End Try
        Catch obj2 As Exception
            Throw
        End Try
        Return textArray2

    End Function
    Public Overrides Function GetRolesForUser(ByVal username As String) As String()
        Dim cons As New SqlClient.SqlConnection(Me._sqlConnectionString)
        Dim cmd As New SqlClient.SqlCommand("aspnet_menu_list_GetMenuForUser", cons)
        cmd.CommandTimeout = Me.CommandTimeout
        cmd.Parameters.Add("@username", SqlDbType.VarChar, 256).Value = username
        cmd.CommandType = CommandType.StoredProcedure
        Dim dr As SqlClient.SqlDataReader = Nothing
        Dim textArray As String()
        Try
            cons.Open()
            Dim strings As New StringCollection
            dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
            Do While dr.Read
                strings.Add(dr.GetString(0))
            Loop
            Dim array As String() = New String(strings.Count - 1) {}
            strings.CopyTo(array, 0)
            textArray = array
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            cons.Dispose()
        End Try
        Return textArray
    End Function

    Public Overrides Function GetUsersInRole(ByVal code_menu As String) As String()
        Dim cons As New SqlClient.SqlConnection(Me._sqlConnectionString)
        Dim cmd As New SqlClient.SqlCommand("aspnet_menu_list_GetUsersInMenu", cons)
        cmd.Parameters.Add("@code_menu", SqlDbType.VarChar, 10).Value = code_menu
        cmd.CommandType = CommandType.StoredProcedure
        Dim dr As SqlClient.SqlDataReader = Nothing
        Dim textArray As String()
        Try
            cons.Open()
            Dim strings As New StringCollection
            dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess)
            Do While dr.Read
                strings.Add(dr.GetString(0))
            Loop
            Dim array As String() = New String(strings.Count - 1) {}
            strings.CopyTo(array, 0)
            textArray = array
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            cons.Dispose()
        End Try
        Return textArray
    End Function
    Public Overrides Function IsUserInRole(ByVal username As String, ByVal code_menu As String) As Boolean
        Dim cons As New SqlClient.SqlConnection(Me._sqlConnectionString)
        Dim cmd As New SqlClient.SqlCommand("aspnet_menu_list_IsUserInMenu", cons)
        cmd.Parameters.Add("@username", SqlDbType.VarChar, 256).Value = username
        cmd.Parameters.Add("@code_menu", SqlDbType.VarChar, 10).Value = code_menu
        cmd.CommandType = CommandType.StoredProcedure
        Dim dr As SqlClient.SqlDataReader = Nothing
        Try
            Dim isExists As Boolean = False
            dr = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read Then isExists = True
            Return isExists
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            cons.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' RoleExists dengan procedure aspnet_menu_list_MenuExists
    ''' tujuan dari function ini yaitu untuk mengetahui ada atau tidaknya menu
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Anwar Minarso</remarks>
    Public Overrides Function RoleExists(ByVal code_menu As String) As Boolean
        Dim cons As New SqlClient.SqlConnection(Me._sqlConnectionString)
        Dim cmd As New SqlClient.SqlCommand("aspnet_menu_list_MenuExists", cons)
        cmd.Parameters.Add("@code", SqlDbType.VarChar, 10).Value = code_menu
        cmd.CommandType = CommandType.StoredProcedure
        Dim dr As SqlClient.SqlDataReader = Nothing
        Try
            Dim isExists As Boolean = False
            dr = cmd.ExecuteReader(CommandBehavior.SingleRow)
            If dr.Read Then isExists = True
            Return isExists
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then dr.Close()
            cons.Dispose()
        End Try
    End Function
#End Region
    Public Overrides Sub Initialize(ByVal name As String, ByVal config As System.Collections.Specialized.NameValueCollection)
        'MyBase.Initialize(name, config)
        If (config Is Nothing) Then
            Throw New ArgumentNullException("config")
        End If
        If String.IsNullOrEmpty(name) Then
            name = "RoleProvider"
        End If
        If String.IsNullOrEmpty(config.Item("description")) Then
            config.Remove("description")
            config.Add("description", "RoleProvider Description")
        End If
        MyBase.Initialize(name, config)
        Me._SchemaVersionCheck = 0
        Me._CommandTimeout = Providers.SecUtility.GetIntValue(config, "commandTimeout", 30, True, 0)

        Dim specifiedConnectionString As String = config.Item("connectionStringName")
        If ((specifiedConnectionString Is Nothing) OrElse (specifiedConnectionString.Length < 1)) Then
            Throw New ProviderException("Connection name not specified")
        End If
        Me._sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings(config.Item("connectionStringName")).ToString
        If ((Me._sqlConnectionString Is Nothing) OrElse (Me._sqlConnectionString.Length < 1)) Then
            Throw New ProviderException("Connection string not found")
        End If
        Me._AppName = config.Item("applicationName")
        If String.IsNullOrEmpty(Me._AppName) Then
            Me._AppName = "SHE"
        End If
        If (Me._AppName.Length > &H100) Then
            Throw New ProviderException("Provider application name too long")
        End If
        config.Remove("connectionStringName")
        config.Remove("applicationName")
        config.Remove("commandTimeout")
        If (config.Count > 0) Then
            Dim key As String = config.GetKey(0)
            If Not String.IsNullOrEmpty(key) Then
                Throw New ProviderException("Provider unrecognized attribute")
            End If
        End If

    End Sub
End Class
