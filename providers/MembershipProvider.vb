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
Public Class MembershipProvider
    Inherits System.Web.Security.MembershipProvider

#Region "Fields"
    Private _AppName As String
    Private _CommandTimeout As Integer
    Private _EnablePasswordReset As Boolean
    Private _EnablePasswordRetrieval As Boolean
    Private _MaxInvalidPasswordAttempts As Integer
    Private _MinRequiredNonalphanumericCharacters As Integer
    Private _MinRequiredPasswordLength As Integer
    Private _PasswordAttemptWindow As Integer
    Private _PasswordFormat As Web.Security.MembershipPasswordFormat
    Private _PasswordStrengthRegularExpression As String
    Private _RequiresQuestionAndAnswer As Boolean
    Private _RequiresUniqueEmail As Boolean
    Private _SchemaVersionCheck As Integer
    Private _sqlConnectionString As String
    Private Const PASSWORD_SIZE As Integer = 14
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
    Public Overrides ReadOnly Property EnablePasswordReset() As Boolean
        Get
            Return Me._EnablePasswordReset
        End Get
    End Property
    Public Overrides ReadOnly Property EnablePasswordRetrieval() As Boolean
        Get
            Return Me._EnablePasswordRetrieval
        End Get
    End Property

    Public Overrides ReadOnly Property MaxInvalidPasswordAttempts() As Integer
        Get
            Return Me._MaxInvalidPasswordAttempts
        End Get
    End Property

    Public Overrides ReadOnly Property MinRequiredNonAlphanumericCharacters() As Integer
        Get
            Return Me._MinRequiredNonalphanumericCharacters
        End Get
    End Property
    Public Overrides ReadOnly Property MinRequiredPasswordLength() As Integer
        Get
            Return Me._MinRequiredPasswordLength
        End Get
    End Property

    Public Overrides ReadOnly Property PasswordAttemptWindow() As Integer
        Get
            Return Me._PasswordAttemptWindow
        End Get
    End Property
    Public Overrides ReadOnly Property PasswordFormat() As Web.Security.MembershipPasswordFormat
        Get
            Return Me._PasswordFormat
        End Get
    End Property
    Public Overrides ReadOnly Property PasswordStrengthRegularExpression() As String
        Get
            Return Me._PasswordStrengthRegularExpression
        End Get
    End Property
    Public Overrides ReadOnly Property RequiresQuestionAndAnswer() As Boolean
        Get
            Return Me._RequiresQuestionAndAnswer
        End Get
    End Property
    Public Overrides ReadOnly Property RequiresUniqueEmail() As Boolean
        Get
            Return Me._RequiresUniqueEmail
        End Get
    End Property
#End Region

#Region "Other"
    Private Sub CheckSchemaVersion(ByVal connection As SqlClient.SqlConnection)
        Dim features As String() = New String() {"Common", "Membership"}
        Dim version As String = "1"
        providers.SecUtility.CheckSchemaVersion(Me, connection, features, version, (Me._SchemaVersionCheck))
    End Sub
    Private Function GetExceptionText(ByVal status As Integer) As String
        Dim name As String
        Select Case status
            Case 0 : Return String.Empty
            Case 1 : name = "Membership UserNotFound" : Exit Select
            Case 2 : name = "Membership WrongPassword" : Exit Select
            Case 3 : name = "Membership WrongAnswer" : Exit Select
            Case 4 : name = "Membership InvalidPassword" : Exit Select
            Case 5 : name = "Membership InvalidQuestion" : Exit Select
            Case 6 : name = "Membership InvalidAnswer" : Exit Select
            Case 7 : name = "Membership InvalidEmail" : Exit Select
            Case &H63 : name = "Membership AccountLockOut" : Exit Select
            Case Else : name = "Provider Error" : Exit Select
        End Select
        Return name
    End Function
    Private Function IsStatusDueToBadPassword(ByVal status As Integer) As Boolean
        Return (((status >= 2) AndAlso (status <= 6)) OrElse (status = &H63))
    End Function
    Friend Function EncodePassword(ByVal pass As String, ByVal passwordFormat As Integer, ByVal salt As String) As String
        Dim encodedpass As String = Nothing
        Select Case passwordFormat
            Case 0
                encodedpass = pass
            Case 2
                encodedpass = Convert.ToBase64String(Encoding.Unicode.GetBytes(pass))
            Case 1
                Dim buffer1 As Byte() = Encoding.Unicode.GetBytes(pass)
                Dim buffer2 As Byte() = Convert.FromBase64String(salt)
                Dim buffer3 As Byte() = New Byte((buffer2.Length + buffer1.Length) - 1) {}
                Dim buffer4 As Byte() = Nothing
                Buffer.BlockCopy(buffer2, 0, buffer3, 0, buffer2.Length)
                Buffer.BlockCopy(buffer1, 0, buffer3, buffer2.Length, buffer1.Length)
                Dim algorithm1 As HashAlgorithm = HashAlgorithm.Create(Membership.HashAlgorithmType)
                If algorithm1 Is Nothing Then
                    Throw New ProviderException("Invalid hash algorithm type")
                End If
                buffer4 = algorithm1.ComputeHash(buffer3)
                encodedpass = Convert.ToBase64String(buffer4)
        End Select
        Return encodedpass
    End Function
    Private Function RoundToSeconds(ByVal dt As DateTime) As DateTime
        Return New DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second)
    End Function
    Private Function GenerateSalt() As String
        Dim buffer1 As Byte() = New Byte(&H10 - 1) {}
        Dim gen As New System.Security.Cryptography.RNGCryptoServiceProvider
        gen.GetBytes(buffer1)
        Return Convert.ToBase64String(buffer1)
    End Function
    Public Overridable Function GeneratePassword() As String
        Return Membership.GeneratePassword(IIf((Me.MinRequiredPasswordLength < 14), 14, Me.MinRequiredPasswordLength), Me.MinRequiredNonAlphanumericCharacters)
    End Function
    Private Function GetEncodedPasswordAnswer(ByVal username As String, ByVal passwordAnswer As String) As String
        Dim status As Integer
        Dim passwordFormat As Integer
        Dim failedPasswordAttemptCount As Integer
        Dim failedPasswordAnswerAttemptCount As Integer
        Dim password As String = ""
        Dim passwordSalt As String = ""
        Dim isApproved As Boolean
        Dim lastLoginDate As DateTime
        Dim lastActivityDate As DateTime
        If (Not passwordAnswer Is Nothing) Then
            passwordAnswer = passwordAnswer.Trim
        End If
        If String.IsNullOrEmpty(passwordAnswer) Then
            Return passwordAnswer
        End If
        Me.GetPasswordWithFormat(username, False, status, password, passwordFormat, passwordSalt, failedPasswordAttemptCount, failedPasswordAnswerAttemptCount, isApproved, lastLoginDate, lastActivityDate)
        If (status <> 0) Then
            Throw New ProviderException(Me.GetExceptionText(status))
        End If
        Return Me.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), passwordFormat, passwordSalt)
    End Function
    Private Function GetNullableString(ByVal reader As SqlClient.SqlDataReader, ByVal col As Integer) As String
        If Not reader.IsDBNull(col) Then
            Return reader.GetString(col)
        End If
        Return Nothing
    End Function

    Private Function UnEncodePassword(ByVal encodedPassword As String, ByVal passFormat As Integer) As String
        Dim password As String = encodedPassword
        Select Case passFormat
            Case 0
            Case 2
                password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)))
            Case 1
                Throw New ProviderException("Cannot unencode a hashed password.")
            Case Else
                Throw New ProviderException("Unsupported password format.")
        End Select

        Return password
    End Function
#End Region
#Region "Methods"
    Public Function Getcode_pegawai(ByVal code_user As Integer) As Long
        Dim cons As New sqlclient.SqlConnection(Me._sqlConnectionString)
        Dim cmd As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetCodePegawai", cons)
        cmd.Parameters.Add("@code_user", SqlDbType.Int).Value = code_user
        Dim dr As SqlClient.SqlDataReader = Nothing
        cmd.CommandType = CommandType.StoredProcedure
        Try
            cons.Open()
            dr = cmd.ExecuteReader(CommandBehavior.SingleRow)
            Dim eid As Long = 0
            If dr.Read Then
                eid = Convert.ToInt64(dr("code"))
            End If
            Return eid
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then
                dr.Close()
            End If
            cons.Dispose()
        End Try
    End Function

    Public Function Getcode_language(ByVal code_user As Integer) As String
        Dim cons As New sqlclient.SqlConnection(Me._sqlConnectionString)
        Dim cmd As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetCodeLanguage", cons)
        cmd.Parameters.Add("@code_user", SqlDbType.Int).Value = code_user
        Dim dr As SqlClient.SqlDataReader = Nothing
        cmd.CommandType = CommandType.StoredProcedure
        Try
            cons.Open()
            dr = cmd.ExecuteReader(CommandBehavior.SingleRow)
            Dim code_language As String = ""
            If dr.Read Then
                code_language = Convert.ToString(dr("code_language"))
            End If
            Return code_language
        Finally
            If Not dr Is Nothing AndAlso Not dr.IsClosed Then
                dr.Close()
            End If
            cons.Dispose()
        End Try
    End Function

    Public Overrides Function ChangePassword(ByVal username As String, ByVal oldPassword As String, ByVal newPassword As String) As Boolean
        Dim passwordFormat As Integer
        Providers.SecUtility.CheckParameter((username), True, True, True, &H100, "username")
        Providers.SecUtility.CheckParameter((oldPassword), True, True, False, &H80, "oldPassword")
        Providers.SecUtility.CheckParameter((newPassword), True, True, False, &H80, "newPassword")
        Dim salt As String = Nothing
        If Not Me.CheckPassword(username, oldPassword, False, False, salt, passwordFormat) Then
            Return False
        End If
        If (newPassword.Length < Me.MinRequiredPasswordLength) Then
            Throw New ArgumentException("Password too short")
        End If
        Dim num3 As Integer = 0
        Dim i As Integer
        For i = 0 To newPassword.Length - 1
            If Not Char.IsLetterOrDigit(newPassword, i) Then
                num3 += 1
            End If
        Next i
        If (num3 < Me.MinRequiredNonAlphanumericCharacters) Then
            Throw New ArgumentException("Password need more non alpha numeric chars")
        End If
        If ((Me.PasswordStrengthRegularExpression.Length > 0) AndAlso Not Regex.IsMatch(newPassword, Me.PasswordStrengthRegularExpression)) Then
            Throw New ArgumentException("Password does not match regular expression")
        End If
        Dim objValue As String = Me.EncodePassword(newPassword, passwordFormat, salt)
        If (objValue.Length > &H80) Then
            Throw New ArgumentException("Membership password too long")
        End If
        Dim e As New ValidatePasswordEventArgs(username, newPassword, False)
        Me.OnValidatingPassword(e)
        If e.Cancel Then
            If (Not e.FailureInformation Is Nothing) Then
                Throw e.FailureInformation
            End If
            Throw New ArgumentException("Membership Custom Password Validation Failure")
        End If
        Try
            Dim cons As sqlclient.SqlConnection = Nothing
            Try
                cons = New sqlclient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim cmd As New SqlClient.SqlCommand("dbo.aspnet_user_list_SetPassword", cons)
                cmd.CommandTimeout = Me.CommandTimeout
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@NewPassword", SqlDbType.VarChar, objValue))
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordSalt", SqlDbType.VarChar, salt))
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordFormat", SqlDbType.Int, passwordFormat))
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                cmd.Parameters.Add(parameter)
                cmd.ExecuteNonQuery()
                Dim status As Integer = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                If (status <> 0) Then
                    Dim message As String = Me.GetExceptionText(status)
                    If Me.IsStatusDueToBadPassword(status) Then
                        Throw New MembershipPasswordException(message)
                    End If
                    Throw New ProviderException(message)
                End If
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return True
    End Function

    Public Overrides Function ChangePasswordQuestionAndAnswer(ByVal username As String, ByVal password As String, ByVal newPasswordQuestion As String, ByVal newPasswordAnswer As String) As Boolean
        Dim salt As String = ""
        Dim passwordFormat As Integer
        Dim param As String = ""
        Dim flag As Boolean
        Providers.SecUtility.CheckParameter((username), True, True, True, &H100, "username")
        Providers.SecUtility.CheckParameter((password), True, True, False, &H80, "password")
        If Not Me.CheckPassword(username, password, False, False, salt, passwordFormat) Then
            Return False
        End If
        Providers.SecUtility.CheckParameter((newPasswordQuestion), Me.RequiresQuestionAndAnswer, Me.RequiresQuestionAndAnswer, False, &H100, "newPasswordQuestion")
        If (Not newPasswordAnswer Is Nothing) Then
            newPasswordAnswer = newPasswordAnswer.Trim
        End If
        Providers.SecUtility.CheckParameter((newPasswordAnswer), Me.RequiresQuestionAndAnswer, Me.RequiresQuestionAndAnswer, False, &H80, "newPasswordAnswer")
        If Not String.IsNullOrEmpty(newPasswordAnswer) Then
            param = Me.EncodePassword(newPasswordAnswer.ToLower(CultureInfo.InvariantCulture), passwordFormat, salt)
        Else
            param = newPasswordAnswer
        End If
        Providers.SecUtility.CheckParameter((param), Me.RequiresQuestionAndAnswer, Me.RequiresQuestionAndAnswer, False, &H80, "newPasswordAnswer")
        Try
            Dim cons As sqlclient.SqlConnection = Nothing
            Try
                cons = New sqlclient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim cmd As New SqlClient.SqlCommand("dbo.aspnet_user_list_ChangePasswordQuestionAndAnswer", cons)
                cmd.CommandTimeout = Me.CommandTimeout
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@NewPasswordQuestion", SqlDbType.VarChar, newPasswordQuestion))
                cmd.Parameters.Add(Providers.SecUtility.CreateInputParam("@NewPasswordAnswer", SqlDbType.VarChar, param))
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                cmd.Parameters.Add(parameter)
                cmd.ExecuteNonQuery()
                Dim status As Integer = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                If (status <> 0) Then
                    Throw New ProviderException(Me.GetExceptionText(status))
                End If
                flag = (status = 0)
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return flag
    End Function

    Private Function CheckPassword(ByVal username As String, ByVal password As String, ByVal updateLastLoginActivityDate As Boolean, ByVal failIfNotApproved As Boolean) As Boolean
        Dim salt As String = ""
        Dim passwordFormat As Integer
        Return Me.CheckPassword(username, password, updateLastLoginActivityDate, failIfNotApproved, salt, passwordFormat)
    End Function

    Private Function CheckPassword(ByVal vusername As String, ByVal vpwd As String, ByVal updateLastLoginActivityDate As Boolean, ByVal failIfNotApproved As Boolean, <Out()> ByRef salt As String, <Out()> ByRef passwordFormat As Integer) As Boolean
        Try
            Dim reader As Data.SqlClient.SqlDataReader
            'Dim vpwd As String
            vpwd = common.Functions.FormatDecryptToEncrypt(1, vpwd)
            Dim sqlcom As String
            sqlcom = "select code from user_list where username = '" & vusername & "' and pwd = '" & vpwd & "' and status = 1"

            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                'While reader.Read
                HttpContext.Current.Session("UserID") = reader.Item("code")
                reader.Close()
                connection.koneksi.CloseKoneksi()
                Return True
                'End While
            Else
                Return False
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception

        End Try
    End Function

    Public Overrides Function CreateUser(ByVal username As String, ByVal password As String, ByVal email As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, ByVal isApproved As Boolean, ByVal providerUserKey As Object, <Out()> ByRef status As MembershipCreateStatus) As MembershipUser
        Dim param As String
        Dim user As MembershipUser
        If Not Providers.SecUtility.ValidateParameter((password), True, True, False, &H80) Then
            status = MembershipCreateStatus.InvalidPassword
            Return Nothing
        End If
        Dim salt As String = Me.GenerateSalt
        Dim objValue As String = Me.EncodePassword(password, CInt(Me._PasswordFormat), salt)
        If (objValue.Length > &H80) Then
            status = MembershipCreateStatus.InvalidPassword
            Return Nothing
        End If
        If (Not passwordAnswer Is Nothing) Then
            passwordAnswer = passwordAnswer.Trim
        End If
        If Not String.IsNullOrEmpty(passwordAnswer) Then
            If (passwordAnswer.Length > &H80) Then
                status = MembershipCreateStatus.InvalidAnswer
                Return Nothing
            End If
            param = Me.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), CInt(Me._PasswordFormat), salt)
        Else
            param = passwordAnswer
        End If
        If Not Providers.SecUtility.ValidateParameter((param), Me.RequiresQuestionAndAnswer, True, False, &H80) Then
            status = MembershipCreateStatus.InvalidAnswer
            Return Nothing
        End If
        If Not Providers.SecUtility.ValidateParameter((username), True, True, True, &H100) Then
            status = MembershipCreateStatus.InvalidUserName
            Return Nothing
        End If
        If Not Providers.SecUtility.ValidateParameter((email), Me.RequiresUniqueEmail, Me.RequiresUniqueEmail, False, &H100) Then
            status = MembershipCreateStatus.InvalidEmail
            Return Nothing
        End If
        If Not Providers.SecUtility.ValidateParameter((passwordQuestion), Me.RequiresQuestionAndAnswer, True, False, &H100) Then
            status = MembershipCreateStatus.InvalidQuestion
            Return Nothing
        End If
        'If ((Not providerUserKey Is Nothing) AndAlso Not TypeOf providerUserKey Is Guid) Then
        '    status = MembershipCreateStatus.InvalidProviderUserKey
        '    Return Nothing
        'End If
        If (password.Length < Me.MinRequiredPasswordLength) Then
            status = MembershipCreateStatus.InvalidPassword
            Return Nothing
        End If
        Dim num As Integer = 0
        Dim i As Integer
        For i = 0 To password.Length - 1
            If Not Char.IsLetterOrDigit(password, i) Then
                num += 1
            End If
        Next i
        If (num < Me.MinRequiredNonAlphanumericCharacters) Then
            status = MembershipCreateStatus.InvalidPassword
            Return Nothing
        End If
        If ((Me.PasswordStrengthRegularExpression.Length > 0) AndAlso Not Regex.IsMatch(password, Me.PasswordStrengthRegularExpression)) Then
            status = MembershipCreateStatus.InvalidPassword
            Return Nothing
        End If
        Dim e As New ValidatePasswordEventArgs(username, password, True)
        Me.OnValidatingPassword(e)
        If e.Cancel Then
            status = MembershipCreateStatus.InvalidPassword
            Return Nothing
        End If
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim time As DateTime = Me.RoundToSeconds(DateTime.UtcNow)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_CreateUser", cons)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@Password", SqlDbType.VarChar, objValue))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordSalt", SqlDbType.VarChar, salt))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@Email", SqlDbType.VarChar, email))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordQuestion", SqlDbType.VarChar, passwordQuestion))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordAnswer", SqlDbType.VarChar, param))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@IsApproved", SqlDbType.Bit, isApproved))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UniqueEmail", SqlDbType.Int, IIf(Me.RequiresUniqueEmail, 1, 0)))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordFormat", SqlDbType.Int, CInt(Me.PasswordFormat)))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, time))
                Dim parameter As SqlClient.SqlParameter = providers.SecUtility.CreateInputParam("@code", SqlDbType.Int, providerUserKey)
                parameter.Direction = ParameterDirection.InputOutput
                command.Parameters.Add(parameter)
                parameter = New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                command.ExecuteNonQuery()
                Dim num3 As Integer = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                If ((num3 < 0) OrElse (num3 > 11)) Then
                    num3 = 11
                End If
                status = DirectCast(num3, MembershipCreateStatus)
                If (num3 <> 0) Then
                    Return Nothing
                End If
                providerUserKey = CInt(command.Parameters.Item("@code").Value.ToString)
                time = time.ToLocalTime
                user = New MembershipUser(Me.Name, username, providerUserKey, email, passwordQuestion, Nothing, isApproved, False, time, time, time, time, New DateTime(&H6DA, 1, 1))
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return user
    End Function

    Public Overrides Function DeleteUser(ByVal username As String, ByVal deleteAllRelatedData As Boolean) As Boolean
        Dim flag As Boolean
        Providers.SecUtility.CheckParameter((username), True, True, True, &H100, "username")
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_DeleteUser", cons)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                If deleteAllRelatedData Then
                    command.Parameters.Add(Providers.SecUtility.CreateInputParam("@TablesToDeleteFrom", SqlDbType.Int, 15))
                Else
                    command.Parameters.Add(Providers.SecUtility.CreateInputParam("@TablesToDeleteFrom", SqlDbType.Int, 1))
                End If
                Dim parameter As New SqlClient.SqlParameter("@NumTablesDeletedFrom", SqlDbType.Int)
                parameter.Direction = ParameterDirection.Output
                command.Parameters.Add(parameter)
                command.ExecuteNonQuery()
                Dim num As Integer = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                flag = (num > 0)
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return flag
    End Function

    Public Overrides Function FindUsersByEmail(ByVal emailToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As MembershipUserCollection
        Dim users2 As MembershipUserCollection
        Providers.SecUtility.CheckParameter((emailToMatch), False, False, False, &H100, "emailToMatch")
        If (pageIndex < 0) Then
            Throw New ArgumentException("PageIndex_bad", "pageIndex")
        End If
        If (pageSize < 1) Then
            Throw New ArgumentException("PageSize bad", "pageSize")
        End If
        Dim num As Long = (((pageIndex * pageSize) + pageSize) - 1)
        If (num > &H7FFFFFFF) Then
            Throw New ArgumentException("PageIndex PageSize bad", "pageIndex and pageSize")
        End If
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            totalRecords = 0
            Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
            parameter.Direction = ParameterDirection.ReturnValue
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_FindUsersByEmail", cons)
                Dim users As New MembershipUserCollection
                Dim reader As SqlClient.SqlDataReader = Nothing
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@EmailToMatch", SqlDbType.VarChar, emailToMatch))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PageSize", SqlDbType.Int, pageSize))
                command.Parameters.Add(parameter)
                Try
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess)
                    Do While reader.Read
                        Dim name As String = Me.GetNullableString(reader, 0)
                        Dim email As String = Me.GetNullableString(reader, 1)
                        Dim passwordQuestion As String = Me.GetNullableString(reader, 2)
                        Dim comment As String = Me.GetNullableString(reader, 3)
                        Dim isApproved As Boolean = reader.GetBoolean(4)
                        Dim creationDate As DateTime = reader.GetDateTime(5).ToLocalTime
                        Dim lastLoginDate As DateTime = reader.GetDateTime(6).ToLocalTime
                        Dim lastActivityDate As DateTime = reader.GetDateTime(7).ToLocalTime
                        Dim lastPasswordChangedDate As DateTime = reader.GetDateTime(8).ToLocalTime
                        Dim providerUserKey As Integer = reader.GetInt32(9)
                        Dim isLockedOut As Boolean = reader.GetBoolean(10)
                        Dim lastLockoutDate As DateTime = reader.GetDateTime(11).ToLocalTime
                        users.Add(New MembershipUser(Me.Name, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate))
                    Loop
                    users2 = users
                Finally
                    If (Not reader Is Nothing) Then
                        reader.Close()
                    End If
                    If ((Not parameter.Value Is Nothing) AndAlso TypeOf parameter.Value Is Integer) Then
                        totalRecords = CInt(parameter.Value)
                    End If
                End Try
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return users2
    End Function

    Public Overrides Function FindUsersByName(ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As MembershipUserCollection
        Dim users2 As MembershipUserCollection
        Providers.SecUtility.CheckParameter((usernameToMatch), True, True, False, &H100, "usernameToMatch")
        If (pageIndex < 0) Then
            Throw New ArgumentException("PageIndex bad", "pageIndex")
        End If
        If (pageSize < 1) Then
            Throw New ArgumentException("PageSize bad", "pageSize")
        End If
        Dim num As Long = (((pageIndex * pageSize) + pageSize) - 1)
        If (num > &H7FFFFFFF) Then
            Throw New ArgumentException("PageIndex PageSize bad", "pageIndex and pageSize")
        End If
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            totalRecords = 0
            Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
            parameter.Direction = ParameterDirection.ReturnValue
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_FindUsersByName", cons)
                Dim users As New MembershipUserCollection
                Dim reader As SqlClient.SqlDataReader = Nothing
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserNameToMatch", SqlDbType.VarChar, usernameToMatch))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PageSize", SqlDbType.Int, pageSize))
                command.Parameters.Add(parameter)
                Try
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess)
                    Do While reader.Read
                        Dim name As String = Me.GetNullableString(reader, 0)
                        Dim email As String = Me.GetNullableString(reader, 1)
                        Dim passwordQuestion As String = Me.GetNullableString(reader, 2)
                        Dim comment As String = Me.GetNullableString(reader, 3)
                        Dim isApproved As Boolean = reader.GetBoolean(4)
                        Dim creationDate As DateTime = reader.GetDateTime(5).ToLocalTime
                        Dim lastLoginDate As DateTime = reader.GetDateTime(6).ToLocalTime
                        Dim lastActivityDate As DateTime = reader.GetDateTime(7).ToLocalTime
                        Dim lastPasswordChangedDate As DateTime = reader.GetDateTime(8).ToLocalTime
                        Dim providerUserKey As Integer = reader.GetInt32(9)
                        Dim isLockedOut As Boolean = reader.GetBoolean(10)
                        Dim lastLockoutDate As DateTime = reader.GetDateTime(11).ToLocalTime
                        users.Add(New MembershipUser(Me.Name, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate))
                    Loop
                    users2 = users
                Finally
                    If (Not reader Is Nothing) Then
                        reader.Close()
                    End If
                    If ((Not parameter.Value Is Nothing) AndAlso TypeOf parameter.Value Is Integer) Then
                        totalRecords = CInt(parameter.Value)
                    End If
                End Try
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return users2
    End Function

    Public Overrides Function GetAllUsers(ByVal pageIndex As Integer, ByVal pageSize As Integer, <Out()> ByRef totalRecords As Integer) As MembershipUserCollection
        If (pageIndex < 0) Then
            Throw New ArgumentException("PageIndex bad", "pageIndex")
        End If
        If (pageSize < 1) Then
            Throw New ArgumentException("PageSize bad", "pageSize")
        End If
        Dim num As Long = (((pageIndex * pageSize) + pageSize) - 1)
        If (num > &H7FFFFFFF) Then
            Throw New ArgumentException("PageIndex PageSize bad", "pageIndex and pageSize")
        End If
        Dim users As New MembershipUserCollection
        totalRecords = 0
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Try
                cons = New sqlclient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetAllUsers", cons)
                Dim reader As SqlClient.SqlDataReader = Nothing
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@ApplicationName", SqlDbType.VarChar, Me.ApplicationName))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PageIndex", SqlDbType.Int, pageIndex))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PageSize", SqlDbType.Int, pageSize))
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                Try
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess)
                    Do While reader.Read
                        Dim name As String = Me.GetNullableString(reader, 0)
                        Dim email As String = Me.GetNullableString(reader, 1)
                        Dim passwordQuestion As String = Me.GetNullableString(reader, 2)
                        Dim comment As String = Me.GetNullableString(reader, 3)
                        Dim isApproved As Boolean = reader.GetBoolean(4)
                        Dim creationDate As DateTime = reader.GetDateTime(5).ToLocalTime
                        Dim lastLoginDate As DateTime = reader.GetDateTime(6).ToLocalTime
                        Dim lastActivityDate As DateTime = reader.GetDateTime(7).ToLocalTime
                        Dim lastPasswordChangedDate As DateTime = reader.GetDateTime(8).ToLocalTime
                        Dim providerUserKey As Integer = reader.GetInt32(9)
                        Dim isLockedOut As Boolean = reader.GetBoolean(10)
                        Dim lastLockoutDate As DateTime = reader.GetDateTime(11).ToLocalTime
                        users.Add(New MembershipUser(Me.Name, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate))
                    Loop
                Finally
                    If (Not reader Is Nothing) Then
                        reader.Close()
                    End If
                    If ((Not parameter.Value Is Nothing) AndAlso TypeOf parameter.Value Is Integer) Then
                        totalRecords = CInt(parameter.Value)
                    End If
                End Try
                Return users
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return users
    End Function

    Public Overrides Function GetNumberOfUsersOnline() As Integer
        Dim num2 As Integer
        Try
            Dim cons As sqlclient.SqlConnection = Nothing
            Try
                cons = New sqlclient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetNumberOfUsersOnline", cons)
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@MinutesSinceLastInActive", SqlDbType.Int, Membership.UserIsOnlineTimeWindow))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                command.ExecuteNonQuery()
                Dim num As Integer = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                num2 = num
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return num2
    End Function
    Public Overrides Function GetPassword(ByVal username As String, ByVal passwordAnswer As String) As String
        If Not Me.EnablePasswordRetrieval Then
            Throw New NotSupportedException("Membership asswordRetrieval not supported")
        End If
        Providers.SecUtility.CheckParameter((username), True, True, True, &H100, "username")
        Dim param As String = Me.GetEncodedPasswordAnswer(username, passwordAnswer)
        Providers.SecUtility.CheckParameter((param), Me.RequiresQuestionAndAnswer, Me.RequiresQuestionAndAnswer, False, &H80, "passwordAnswer")
        Dim passwordFormat As Integer = 0
        Dim status As Integer = 0
        Dim pass As String = Me.GetPasswordFromDB(username, param, Me.RequiresQuestionAndAnswer, passwordFormat, status)
        If (Not pass Is Nothing) Then
            Return Me.UnEncodePassword(pass, passwordFormat)
        End If
        Dim message As String = Me.GetExceptionText(status)
        If Me.IsStatusDueToBadPassword(status) Then
            Throw New MembershipPasswordException(message)
        End If
        Throw New ProviderException(message)
    End Function

    Private Function GetPasswordFromDB(ByVal username As String, ByVal passwordAnswer As String, ByVal requiresQuestionAndAnswer As Boolean, <Out()> ByRef passwordFormat As Integer, <Out()> ByRef status As Integer) As String
        Dim text2 As String
        Try
            Dim cons As sqlclient.SqlConnection = Nothing
            Dim reader As SqlClient.SqlDataReader = Nothing
            Dim parameter As SqlClient.SqlParameter = Nothing
            Try
                cons = New sqlclient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetPassword", cons)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, Me.MaxInvalidPasswordAttempts))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, Me.PasswordAttemptWindow))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                If requiresQuestionAndAnswer Then
                    command.Parameters.Add(Providers.SecUtility.CreateInputParam("@PasswordAnswer", SqlDbType.VarChar, passwordAnswer))
                End If
                parameter = New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                reader = command.ExecuteReader(CommandBehavior.SingleRow)
                Dim text As String = Nothing
                status = -1
                If reader.Read Then
                    [text] = reader.GetString(0)
                    passwordFormat = reader.GetInt32(1)
                Else
                    [text] = Nothing
                    passwordFormat = 0
                End If
                text2 = [text]
            Finally
                If (Not reader Is Nothing) Then
                    reader.Close()
                    reader = Nothing
                    status = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                End If
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return text2
    End Function

    Private Sub GetPasswordWithFormat(ByVal username As String, ByVal updateLastLoginActivityDate As Boolean, <Out()> ByRef status As Integer, <Out()> ByRef password As String, <Out()> ByRef passwordFormat As Integer, <Out()> ByRef passwordSalt As String, <Out()> ByRef failedPasswordAttemptCount As Integer, <Out()> ByRef failedPasswordAnswerAttemptCount As Integer, <Out()> ByRef isApproved As Boolean, <Out()> ByRef lastLoginDate As DateTime, <Out()> ByRef lastActivityDate As DateTime)
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Dim reader As SqlClient.SqlDataReader = Nothing
            Dim parameter As SqlClient.SqlParameter = Nothing
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("aspnet_user_list_GetPasswordWithFormat", cons)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UpdateLastLoginActivityDate", SqlDbType.Bit, updateLastLoginActivityDate))
                command.Parameters.Add(Providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                parameter = New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                reader = command.ExecuteReader(CommandBehavior.SingleRow)
                status = -1
                If reader.Read Then
                    password = reader.GetString(0)
                    passwordFormat = reader.GetInt32(1)
                    passwordSalt = reader.GetString(2)
                    failedPasswordAttemptCount = reader.GetInt32(3)
                    failedPasswordAnswerAttemptCount = reader.GetInt32(4)
                    isApproved = reader.GetBoolean(5)
                    lastLoginDate = reader.GetDateTime(6)
                    lastActivityDate = reader.GetDateTime(7)
                Else
                    password = Nothing
                    passwordFormat = 0
                    passwordSalt = Nothing
                    failedPasswordAttemptCount = 0
                    failedPasswordAnswerAttemptCount = 0
                    isApproved = False
                    lastLoginDate = DateTime.UtcNow
                    lastActivityDate = DateTime.UtcNow
                End If
            Finally
                If (Not reader Is Nothing) Then
                    reader.Close()
                    reader = Nothing
                    status = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                End If
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
    End Sub
    Public Overrides Function GetUser(ByVal providerUserKey As Object, ByVal userIsOnline As Boolean) As MembershipUser
        If (providerUserKey Is Nothing) Then
            Throw New ArgumentNullException("providerUserKey")
        End If
        'If Not TypeOf providerUserKey Is Guid Then
        '    Throw New ArgumentException("Membership InvalidProviderUserKey", "providerUserKey")
        'End If
        Dim reader As SqlClient.SqlDataReader = Nothing
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetUserBycode", cons)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@code", SqlDbType.Int, providerUserKey))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@UpdateLastActivity", SqlDbType.Bit, userIsOnline))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                reader = command.ExecuteReader
                If reader.Read Then
                    Dim email As String = Me.GetNullableString(reader, 0)
                    Dim passwordQuestion As String = Me.GetNullableString(reader, 1)
                    Dim comment As String = Me.GetNullableString(reader, 2)
                    Dim isApproved As Boolean = reader.GetBoolean(3)
                    Dim creationDate As DateTime = reader.GetDateTime(4).ToLocalTime
                    Dim lastLoginDate As DateTime = reader.GetDateTime(5).ToLocalTime
                    Dim lastActivityDate As DateTime = reader.GetDateTime(6).ToLocalTime
                    Dim lastPasswordChangedDate As DateTime = reader.GetDateTime(7).ToLocalTime
                    Dim name As String = Me.GetNullableString(reader, 8)
                    Dim isLockedOut As Boolean = reader.GetBoolean(9)
                    Return New MembershipUser(Me.Name, name, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, reader.GetDateTime(10).ToLocalTime)
                End If
            Finally
                If (Not reader Is Nothing) Then
                    reader.Close()
                    reader = Nothing
                End If
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return Nothing
    End Function

    Public Overrides Function GetUser(ByVal username As String, ByVal userIsOnline As Boolean) As MembershipUser
        providers.SecUtility.CheckParameter((username), True, False, True, &H100, "username")
        Dim dr As SqlClient.SqlDataReader = Nothing
        Try
            dr = connection.koneksi.SelectRecord("select * from pegawai where username = '" & username & "'")
            If dr.HasRows Then
                Return Nothing
            End If

            'Dim cons As sqlclient.SqlConnection = Nothing
            'Try
            '    cons = New sqlclient.SqlConnection(Me._sqlConnectionString)
            '    cons.Open()
            '    'Me.CheckSchemaVersion(cons)
            '    Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetUserByName", cons)
            '    command.CommandTimeout = Me.CommandTimeout
            '    command.CommandType = CommandType.StoredProcedure
            '    command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
            '    command.Parameters.Add(Providers.SecUtility.CreateInputParam("@UpdateLastActivity", SqlDbType.Bit, userIsOnline))
            '    command.Parameters.Add(Providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
            '    Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
            '    parameter.Direction = ParameterDirection.ReturnValue
            '    command.Parameters.Add(parameter)
            '    dr = command.ExecuteReader
            '    If dr.Read Then
            '        Dim email As String = Me.GetNullableString(dr, 0)
            '        Dim passwordQuestion As String = Me.GetNullableString(dr, 1)
            '        Dim comment As String = Me.GetNullableString(dr, 2)
            '        Dim isApproved As Boolean = dr.GetBoolean(3)
            '        Dim creationDate As DateTime = dr.GetDateTime(4).ToLocalTime
            '        Dim lastLoginDate As DateTime = dr.GetDateTime(5).ToLocalTime
            '        Dim lastActivityDate As DateTime = dr.GetDateTime(6).ToLocalTime
            '        Dim lastPasswordChangedDate As DateTime = dr.GetDateTime(7).ToLocalTime
            '        Dim providerUserKey As Integer = dr.GetInt32(8)
            '        Dim isLockedOut As Boolean = dr.GetBoolean(9)
            'Return New MembershipUser(Me.Name, username, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, dr.GetDateTime(10).ToLocalTime)
            '    End If
            'Finally
            '    If Not dr Is Nothing AndAlso Not dr.IsClosed Then
            '        dr.Close()
            '    End If
            '    If (Not cons Is Nothing) Then
            '        cons.Close()
            '        cons = Nothing
            '    End If
            'End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return Nothing
    End Function

    Public Overrides Function GetUserNameByEmail(ByVal email As String) As String
        Dim text2 As String
        providers.SecUtility.CheckParameter((email), False, False, False, &H100, "email")
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_GetUserByEmail", cons)
                Dim nullableString As String = Nothing
                Dim reader As SqlClient.SqlDataReader = Nothing
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@Email", SqlDbType.VarChar, email))
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                Try
                    reader = command.ExecuteReader(CommandBehavior.SequentialAccess)
                    If reader.Read Then
                        nullableString = Me.GetNullableString(reader, 0)
                        If (Me.RequiresUniqueEmail AndAlso reader.Read) Then
                            Throw New ProviderException("Membership_more_than_one_user_with_email")
                        End If
                    End If
                Finally
                    If (Not reader Is Nothing) Then
                        reader.Close()
                    End If
                End Try
                text2 = nullableString
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return text2
    End Function
    Public Overrides Function UnlockUser(ByVal username As String) As Boolean
        providers.SecUtility.CheckParameter((username), True, True, True, &H100, "username")
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_UnlockUser", cons)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                command.ExecuteNonQuery()
                If (IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1) = 0) Then
                    Return True
                End If
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
        Return False
    End Function

    Public Overrides Function ResetPassword(ByVal username As String, ByVal passwordAnswer As String) As String
        Dim passwordSalt As String = ""
        Dim passwordFormat As Integer
        Dim password As String = ""
        Dim status As Integer
        Dim failedPasswordAttemptCount As Integer
        Dim failedPasswordAnswerAttemptCount As Integer
        Dim isApproved As Boolean
        Dim lastLoginDate As DateTime
        Dim lastActivityDate As DateTime
        If Not Me.EnablePasswordReset Then
            Throw New NotSupportedException("Not_configured_to_support_password_resets")
        End If
        providers.SecUtility.CheckParameter((username), True, True, True, &H100, "username")
        Me.GetPasswordWithFormat(username, False, status, password, passwordFormat, passwordSalt, failedPasswordAttemptCount, failedPasswordAnswerAttemptCount, isApproved, lastLoginDate, lastActivityDate)
        If (status = 0) Then
            Dim param As String
            Dim text6 As String
            If (Not passwordAnswer Is Nothing) Then
                passwordAnswer = passwordAnswer.Trim
            End If
            If Not String.IsNullOrEmpty(passwordAnswer) Then
                param = Me.EncodePassword(passwordAnswer.ToLower(CultureInfo.InvariantCulture), passwordFormat, passwordSalt)
            Else
                param = passwordAnswer
            End If
            providers.SecUtility.CheckParameter((param), Me.RequiresQuestionAndAnswer, Me.RequiresQuestionAndAnswer, False, &H80, "passwordAnswer")
            Dim text4 As String = Me.GeneratePassword
            Dim e As New ValidatePasswordEventArgs(username, text4, False)
            Me.OnValidatingPassword(e)
            If e.Cancel Then
                If (Not e.FailureInformation Is Nothing) Then
                    Throw e.FailureInformation
                End If
                Throw New ProviderException("Membership_Custom_Password_Validation_Failure")
            End If
            Try
                Dim cons As SqlClient.SqlConnection = Nothing
                Try
                    cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                    cons.Open()
                    'Me.CheckSchemaVersion(cons)
                    Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_ResetPassword", cons)
                    command.CommandTimeout = Me.CommandTimeout
                    command.CommandType = CommandType.StoredProcedure
                    command.Parameters.Add(providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, username))
                    command.Parameters.Add(providers.SecUtility.CreateInputParam("@NewPassword", SqlDbType.VarChar, Me.EncodePassword(text4, passwordFormat, passwordSalt)))
                    command.Parameters.Add(providers.SecUtility.CreateInputParam("@MaxInvalidPasswordAttempts", SqlDbType.Int, Me.MaxInvalidPasswordAttempts))
                    command.Parameters.Add(providers.SecUtility.CreateInputParam("@PasswordAttemptWindow", SqlDbType.Int, Me.PasswordAttemptWindow))
                    command.Parameters.Add(providers.SecUtility.CreateInputParam("@PasswordSalt", SqlDbType.VarChar, passwordSalt))
                    command.Parameters.Add(providers.SecUtility.CreateInputParam("@PasswordFormat", SqlDbType.Int, passwordFormat))
                    command.Parameters.Add(providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                    If Me.RequiresQuestionAndAnswer Then
                        command.Parameters.Add(providers.SecUtility.CreateInputParam("@PasswordAnswer", SqlDbType.VarChar, param))
                    End If
                    Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                    parameter.Direction = ParameterDirection.ReturnValue
                    command.Parameters.Add(parameter)
                    command.ExecuteNonQuery()
                    status = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                    If (status <> 0) Then
                        Dim message As String = Me.GetExceptionText(status)
                        If Me.IsStatusDueToBadPassword(status) Then
                            Throw New MembershipPasswordException(message)
                        End If
                        Throw New ProviderException(message)
                    End If
                    text6 = text4
                Finally
                    If (Not cons Is Nothing) Then
                        cons.Close()
                        cons = Nothing
                    End If
                End Try
            Catch obj1 As Exception
                Throw
            End Try
            Return text6
        End If
        If Me.IsStatusDueToBadPassword(status) Then
            Throw New MembershipPasswordException(Me.GetExceptionText(status))
        End If
        Throw New ProviderException(Me.GetExceptionText(status))
    End Function

    Public Overrides Sub UpdateUser(ByVal user As MembershipUser)
        If (user Is Nothing) Then
            Throw New ArgumentNullException("user")
        End If
        providers.SecUtility.CheckParameter((user.UserName), True, True, True, &H100, "UserName")
        Dim param As String = user.Email
        providers.SecUtility.CheckParameter((param), Me.RequiresUniqueEmail, Me.RequiresUniqueEmail, False, &H100, "Email")
        user.Email = param
        Try
            Dim cons As SqlClient.SqlConnection = Nothing
            Try
                cons = New SqlClient.SqlConnection(Me._sqlConnectionString)
                cons.Open()
                'Me.CheckSchemaVersion(cons)
                Dim command As New SqlClient.SqlCommand("dbo.aspnet_user_list_UpdateUser", cons)
                command.CommandTimeout = Me.CommandTimeout
                command.CommandType = CommandType.StoredProcedure
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@ApplicationName", SqlDbType.VarChar, Me.ApplicationName))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@UserName", SqlDbType.VarChar, user.UserName))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@Email", SqlDbType.VarChar, user.Email))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@Comment", SqlDbType.VarChar, user.Comment))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@IsApproved", SqlDbType.Bit, IIf(user.IsApproved, 1, 0)))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@LastLoginDate", SqlDbType.DateTime, user.LastLoginDate.ToUniversalTime))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@LastActivityDate", SqlDbType.DateTime, user.LastActivityDate.ToUniversalTime))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@UniqueEmail", SqlDbType.Int, IIf(Me.RequiresUniqueEmail, 1, 0)))
                command.Parameters.Add(providers.SecUtility.CreateInputParam("@CurrentTimeUtc", SqlDbType.DateTime, DateTime.UtcNow))
                Dim parameter As New SqlClient.SqlParameter("@ReturnValue", SqlDbType.Int)
                parameter.Direction = ParameterDirection.ReturnValue
                command.Parameters.Add(parameter)
                command.ExecuteNonQuery()
                Dim status As Integer = IIf((Not parameter.Value Is Nothing), CInt(parameter.Value), -1)
                If (status <> 0) Then
                    Throw New ProviderException(Me.GetExceptionText(status))
                End If
            Finally
                If (Not cons Is Nothing) Then
                    cons.Close()
                    cons = Nothing
                End If
            End Try
        Catch obj1 As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Misc"
    Public Overrides Function ValidateUser(ByVal username As String, ByVal password As String) As Boolean
        If ((Providers.SecUtility.ValidateParameter((username), True, True, True, &H100) AndAlso Providers.SecUtility.ValidateParameter((password), True, True, False, &H80)) AndAlso Me.CheckPassword(username, password, True, True)) Then
            'PerfCounters.IncrementCounter(AppPerfCounter.MEMBER_SUCCESS)
            'WebBaseEvent.RaiseSystemEvent(Nothing, &HFA2, username)
            Return True
        Else
            'PerfCounters.IncrementCounter(AppPerfCounter.MEMBER_FAIL)
            'System.Web.Management.WebBaseEvent.RaiseSystemEvent(Nothing, &HFA6, username)
            Return False
        End If
    End Function


#End Region
#Region "Initialize"
    Public Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
        'HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Low, "Feature_not_supported_at_this_level")
        If (config Is Nothing) Then
            Throw New ArgumentNullException("config")
        End If
        If String.IsNullOrEmpty(name) Then
            name = "MembershipProvider"
        End If
        If String.IsNullOrEmpty(config.Item("description")) Then
            config.Remove("description")
            config.Add("description", "MembershipProvider description")
        End If
        MyBase.Initialize(name, config)
        Me._SchemaVersionCheck = 0
        Me._EnablePasswordRetrieval = Providers.SecUtility.GetBooleanValue(config, "enablePasswordRetrieval", False)
        Me._EnablePasswordReset = Providers.SecUtility.GetBooleanValue(config, "enablePasswordReset", True)
        Me._RequiresQuestionAndAnswer = Providers.SecUtility.GetBooleanValue(config, "requiresQuestionAndAnswer", False)
        Me._RequiresUniqueEmail = Providers.SecUtility.GetBooleanValue(config, "requiresUniqueEmail", True)
        Me._MaxInvalidPasswordAttempts = Providers.SecUtility.GetIntValue(config, "maxInvalidPasswordAttempts", 5, False, 0)
        Me._PasswordAttemptWindow = Providers.SecUtility.GetIntValue(config, "passwordAttemptWindow", 10, False, 0)
        Me._MinRequiredPasswordLength = Providers.SecUtility.GetIntValue(config, "minRequiredPasswordLength", 7, False, &H80)
        Me._MinRequiredNonalphanumericCharacters = Providers.SecUtility.GetIntValue(config, "minRequiredNonalphanumericCharacters", 1, True, &H80)
        Me._PasswordStrengthRegularExpression = config.Item("passwordStrengthRegularExpression")
        If (Not Me._PasswordStrengthRegularExpression Is Nothing) Then
            Me._PasswordStrengthRegularExpression = Me._PasswordStrengthRegularExpression.Trim
            If (Me._PasswordStrengthRegularExpression.Length = 0) Then
                GoTo Label_016C
            End If
            Try
                Dim r As New Regex(Me._PasswordStrengthRegularExpression)
                GoTo Label_016C
            Catch exception As ArgumentException
                Throw New ProviderException(exception.Message, exception)
            End Try
        End If
        Me._PasswordStrengthRegularExpression = String.Empty
Label_016C:
        If (Me._MinRequiredNonalphanumericCharacters > Me._MinRequiredPasswordLength) Then
            Throw New HttpException("MinRequiredNonalphanumericCharacters can not be more than MinRequiredPasswordLength")
        End If
        Me._CommandTimeout = Providers.SecUtility.GetIntValue(config, "commandTimeout", 30, True, 0)
        'Me._AppName = config.Item("applicationName")
        If String.IsNullOrEmpty(Me._AppName) Then
            Me._AppName = "HRMS"
        End If
        If (Me._AppName.Length > &H100) Then
            Throw New ProviderException("Provider application name too long")
        End If
        Dim text As String = config.Item("passwordFormat")
        If ([text] Is Nothing) Then
            [text] = "Hashed"
        End If
        Select Case [text]
            Case "Clear"
                Me._PasswordFormat = MembershipPasswordFormat.Clear
                Exit Select
            Case "Encrypted"
                Me._PasswordFormat = MembershipPasswordFormat.Encrypted
                Exit Select
            Case "Hashed"
                Me._PasswordFormat = MembershipPasswordFormat.Hashed
                Exit Select
            Case Else
                Throw New ProviderException("Provider bad password format")
        End Select
        If ((Me.PasswordFormat = MembershipPasswordFormat.Hashed) AndAlso Me.EnablePasswordRetrieval) Then
            Throw New ProviderException("Provider can not retrieve hashed password")
        End If

        Me._sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings(config.Item("connectionStringName")).ToString
        If ((Me._sqlConnectionString Is Nothing) OrElse (Me._sqlConnectionString.Length < 1)) Then
            Throw New ProviderException("Connection string not found")
        End If
        config.Remove("connectionStringName")
        config.Remove("enablePasswordRetrieval")
        config.Remove("enablePasswordReset")
        config.Remove("requiresQuestionAndAnswer")
        config.Remove("applicationName")
        config.Remove("requiresUniqueEmail")
        config.Remove("maxInvalidPasswordAttempts")
        config.Remove("passwordAttemptWindow")
        config.Remove("commandTimeout")
        config.Remove("passwordFormat")
        config.Remove("name")
        config.Remove("minRequiredPasswordLength")
        config.Remove("minRequiredNonalphanumericCharacters")
        config.Remove("passwordStrengthRegularExpression")
        If (config.Count > 0) Then
            Dim key As String = config.GetKey(0)
            If Not String.IsNullOrEmpty(key) Then
                Throw New ProviderException("Provider unrecognized attribute")
            End If
        End If
    End Sub
#End Region
End Class