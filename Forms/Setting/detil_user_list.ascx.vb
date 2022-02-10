Imports System.Configuration
Imports System.Data

Partial Class Forms_Setting_detil_user_list
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property vid_user() As Integer
        Get
            Dim o As Object = Request.QueryString("vid_user")
            If String.IsNullOrEmpty(o) = False Then Return CInt(o) Else Return 0
        End Get
    End Property

    Public Property id_user() As Integer
        Get
            Dim o As Object = ViewState("id_user")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("id_user") = value
        End Set
    End Property

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearform()
        Me.tb_name.Text = ""
        Me.tb_pwd.Text = ""
        Me.tb_email.Text = ""
        Me.id_user = 0
    End Sub

    Sub bindgroup()
        sqlcom = "select code, name from user_group order by name"
        reader = connection.koneksi.SelectRecord(sqlcom)
        Me.dd_group.DataSource = reader
        Me.dd_group.DataTextField = "name"
        Me.dd_group.DataValueField = "code"
        Me.dd_group.DataBind()
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loaddata()
        If Me.vid_user <> 0 Then
            Me.id_user = Me.vid_user
        End If

        sqlcom = "select nama_pegawai, username, pwd, email, code_group, status"
        sqlcom = sqlcom + " from user_list"
        sqlcom = sqlcom + " where code = " & Me.id_user
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.tb_pwd.Enabled = False
            Me.tb_nama_pegawai.Text = reader.Item("nama_pegawai").ToString
            Me.tb_name.Text = reader.Item("username").ToString
            Me.tb_pwd.Text = common.Functions.FormatDecryptToEncrypt(1, reader.Item("pwd").ToString)
            Me.tb_email.Text = reader.Item("email").ToString
            Me.dd_group.SelectedValue = reader.Item("code_group").ToString
            Me.dd_status.SelectedValue = reader.Item("status").ToString
        Else
            Me.tb_pwd.Enabled = True
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearform()
            Me.bindgroup()
            Me.loaddata()
        End If    
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            If String.IsNullOrEmpty(Me.tb_nama_pegawai.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama pegawai terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_name.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi nama user terlebih dahulu"
            ElseIf String.IsNullOrEmpty(Me.tb_pwd.Text) Then
                Me.lbl_msg.Text = "Silahkan mengisi password terlebih dahulu"
            Else
                If Me.id_user = 0 Then
                    'Dim vpwd As String = common.Functions.FormatDecryptToEncrypt(1, Me.tb_pwd.Text)
                    Dim vpwd As String = Me.tb_pwd.Text
                    'code sudah auto sequence
                    sqlcom = "insert into user_list(nama_pegawai, username, pwd, email, code_group, status)"
                    sqlcom = sqlcom + " values('" & Me.tb_nama_pegawai.Text & "','" & Me.tb_name.Text & "','" & vpwd & "','" & Me.tb_email.Text & "'"
                    sqlcom = sqlcom + "," & Me.dd_group.SelectedValue & ",'" & Me.dd_status.SelectedValue & "')"
                    connection.koneksi.InsertRecord(sqlcom)
                    Me.lbl_msg.Text = "Data sudah disimpan"
                Else
                    sqlcom = "update user_list"
                    sqlcom = sqlcom + " set nama_pegawai = '" & Me.tb_nama_pegawai.Text & "',"
                    sqlcom = sqlcom + " username = '" & Me.tb_name.Text & "',"
                    sqlcom = sqlcom + " email = '" & Me.tb_email.Text & "',"
                    sqlcom = sqlcom + " code_group = " & Me.dd_group.SelectedValue & ","
                    sqlcom = sqlcom + " status = '" & Me.dd_status.SelectedValue & "'"
                    sqlcom = sqlcom + " where code = " & Me.id_user
                    connection.koneksi.UpdateRecord(sqlcom)



                    Me.lbl_msg.Text = "Data sudah diupdate"
                End If
                Me.loaddata()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/st_user_list.aspx")
    End Sub
End Class
