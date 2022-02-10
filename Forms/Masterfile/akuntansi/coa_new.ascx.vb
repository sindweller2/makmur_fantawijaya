Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_accounting_coa_new
    Inherits System.Web.UI.UserControl

    Dim tradingClass As tradingClass = New tradingClass()

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub ClearForm()
        Me.dd_jenis_coa.SelectedIndex = -1
        Me.tb_InaName.Text = Nothing
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Dim no As Integer = 0
        Dim AccountNo As String = Nothing

        If Me.dd_jenis_coa.SelectedIndex = 1 Then
            no = Me.tradingClass.PiutangKaryawanMaxAccountNo() + 1
            AccountNo = "11.03.04." & no
            sqlcom = Nothing
            sqlcom = "INSERT INTO [COA_list] ([AccountNo],[LAccount],[ParentAcc],[LParent],[InaName],[AccType],[IsControl],[MinAmount],[MaxAmount],[DefAmount],[IsActive],[Position],[CurrType],[is_neraca],[saldo_awal])"
            sqlcom = sqlcom + " VALUES ('" & AccountNo & "','5','11.03.04','4','PIUTANG KARYAWAN " & Me.tb_InaName.Text.ToUpper.Trim & "','11','N','0','0','0','Y','D','L','N','0')"
            connection.koneksi.InsertRecord(sqlcom)
        ElseIf Me.dd_jenis_coa.SelectedIndex = 2 Then
            no = Me.tradingClass.HutangLainLainMaxAccountNo() + 1
            AccountNo = "21.03." & no
            sqlcom = Nothing
            sqlcom = "INSERT INTO [COA_list] ([AccountNo],[LAccount],[ParentAcc],[LParent],[InaName],[AccType],[IsControl],[MinAmount],[MaxAmount],[DefAmount],[IsActive],[Position],[CurrType],[is_neraca],[saldo_awal],[CurrencyCode])"
            sqlcom = sqlcom + " VALUES ('" & AccountNo & "','4','21.03','3','HUTANG LAIN-LAIN " & Me.tb_InaName.Text.ToUpper.Trim & "','21','N','0','0','0','Y','C','L','N','0','RP')"
            connection.koneksi.InsertRecord(sqlcom)
        End If

        Me.ClearForm()
        Me.tradingClass.Alert("Data sudah disimpan", Me.Page)
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/coa_list.aspx")
    End Sub
End Class
