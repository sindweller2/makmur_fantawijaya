
Partial Class header
    Inherits System.Web.UI.Page
    Dim sqlcom As String
    Dim reader As Data.SqlClient.SqlDataReader

    Public Property vmax() As Integer
        Get
            Dim o As Object = ViewState("vmax")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vmax") = value
        End Set
    End Property

    Public Property vmax_history_kas() As Integer
        Get
            Dim o As Object = ViewState("vmax_history_kas")
            If Not o Is Nothing Then Return CInt(o) Else Return 0
        End Get
        Set(ByVal value As Integer)
            ViewState("vmax_history_kas") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If HttpContext.Current.Session("UserID").ToString = "" Then
                Dim url As String = "default.aspx"
                Response.Write("<script>" & vbCrLf)
                Response.Write("window.open('" & url & "');" & vbCrLf)
                Response.Write("window.close();" & vbCrLf)
                Response.Write("</script>")
            Else
                Me.username()
                Me.closing()
            End If
            'Me.SetTime()           
        End If
    End Sub

    Private Sub username()
        Try
            sqlcom = "select user_list.username"
            sqlcom = sqlcom + " from user_list"
            sqlcom = sqlcom + " where code = " & HttpContext.Current.Session("UserId")
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                Me.lbl_username.Text = "Username : " & reader.Item("username")
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Sub max_seq_history_kas()
        Try
            Dim readermax As Data.SqlClient.SqlDataReader
            sqlcom = "select isnull(max(seq),0) + 1 as vmax from history_kas"
            readermax = connection.koneksi.SelectRecord(sqlcom)
            readermax.Read()
            If readermax.HasRows Then
                Me.vmax_history_kas = readermax.Item("vmax").ToString
            End If
            readermax.Close()
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Private Sub closing()
        Try
            Dim id_periode_terakhir As Integer = 0            
            Dim bulan_terakhir As Integer = 0
            Dim tahun_terakhir As Integer = 0
            Dim nama_periode_sekarang As String = Nothing
            Dim id_periode_sekarang As Integer = 0

            Dim vtgl As String = Month(Now.Date).ToString.PadLeft(2, "0") & "/" & Day(Now.Date).ToString.PadLeft(2, "0") & "/" & Year(Now.Date).ToString

            'periode terakhir yang sudah close
            sqlcom = "select id as id_periode, bulan, tahun"
            sqlcom = sqlcom + " from transaction_period"
            sqlcom = sqlcom + " where id = (select min(x.id) from transaction_period x"
            sqlcom = sqlcom + " where x.is_closing = 'B')"
            reader = connection.koneksi.SelectRecord(sqlcom)
            reader.Read()
            If reader.HasRows Then
                id_periode_terakhir = Decimal.ToDouble(reader.Item("id_periode").ToString)                
                bulan_terakhir = reader.Item("bulan").ToString
                tahun_terakhir = reader.Item("tahun").ToString
            End If
            reader.Close()
            connection.koneksi.CloseKoneksi()

            If bulan_terakhir <> Decimal.ToDouble(Month(Now.Date.ToString)) Then
                sqlcom = "update transaction_period"
                sqlcom = sqlcom + " set is_closing = 'S'"
                sqlcom = sqlcom + " where bulan = " & bulan_terakhir
                sqlcom = sqlcom + " and tahun = " & tahun_terakhir
                connection.koneksi.UpdateRecord(sqlcom)

                'periode paling awal yang belum close
                sqlcom = "select id as id_periode_sekarang, name as nama_periode_sekarang"
                sqlcom = sqlcom + " from transaction_period"
                sqlcom = sqlcom + " where id = (select min(x.id) from transaction_period x"
                sqlcom = sqlcom + " where x.is_closing = 'B')"
                reader = connection.koneksi.SelectRecord(sqlcom)
                reader.Read()
                If reader.HasRows Then
                    id_periode_sekarang = Decimal.ToDouble(reader.Item("id_periode_sekarang").ToString)
                    nama_periode_sekarang = reader.Item("nama_periode_sekarang").ToString
                End If
                reader.Close()
                connection.koneksi.CloseKoneksi()

                'insert history kas
                sqlcom = "select id as id_cash_bank, isnull(saldo_akhir,0) as saldo_akhir"
                sqlcom = sqlcom + " from bank_account"
                reader = connection.koneksi.SelectRecord(sqlcom)
                Do While reader.Read
                    Me.max_seq_history_kas()
                    sqlcom = "insert into history_kas(id_transaction_period, id_cash_bank, tanggal, keterangan, nilai_debet, nilai_kredit, seq)"
                    sqlcom = sqlcom + " values(" & id_periode_sekarang & "," & reader.Item("id_cash_bank").ToString & ",'" & vtgl & "'"
                    sqlcom = sqlcom + ",'Saldo awal bulan " & nama_periode_sekarang & "'"
                    sqlcom = sqlcom + "," & Decimal.ToDouble(reader.Item("saldo_akhir").ToString) & ",0," & Me.vmax_history_kas & ")"
                    connection.koneksi.InsertRecord(sqlcom)
                Loop
                reader.Close()
                connection.koneksi.CloseKoneksi()
            End If
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub
End Class
