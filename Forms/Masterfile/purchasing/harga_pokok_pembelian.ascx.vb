Imports System.Configuration
Imports System.Data

Partial Class Forms_Masterfile_purchasing_harga_pokok_pembelian
    Inherits System.Web.UI.UserControl

    Dim reader As SqlClient.SqlDataReader
    Dim sqlcom As String

    Sub clearproduk()
        Me.tb_id_produk.Text = 0
        Me.lbl_nama_produk.Text = "------"
        Me.link_popup_produk.Visible = True
    End Sub

    Sub bindproduk()
        sqlcom = "select product_item.nama_beli, product_category.name as kategori, product_sub_category.name as sub_kategori,"
        sqlcom = sqlcom + " unit.name as satuan, convert(char, product_item.qty_conversion) + '/' + packaging.name as packaging"
        sqlcom = sqlcom + " from product_item"
        sqlcom = sqlcom + " inner join product_category on product_category.id = product_item.id_category"
        sqlcom = sqlcom + " inner join product_sub_category on product_sub_category.id_sub_category = product_item.id_sub_category"
        sqlcom = sqlcom + " inner join measurement_unit unit on unit.id = product_item.id_measurement_conversion"
        sqlcom = sqlcom + " inner join measurement_unit packaging on packaging.id = product_item.id_measurement"
        sqlcom = sqlcom + " where product_item.id = " & Me.tb_id_produk.Text
        reader = connection.koneksi.SelectRecord(sqlcom)
        reader.Read()
        If reader.HasRows Then
            Me.lbl_nama_produk.Text = reader.Item("nama_beli").ToString
            Me.lbl_kategori_produk.Text = reader.Item("kategori").ToString
            Me.lbl_sub_category.Text = reader.Item("sub_kategori").ToString
            Me.lbl_satuan.Text = reader.Item("satuan").ToString
            Me.lbl_packaging.Text = reader.Item("packaging").ToString
            Me.loadgrid()
        End If
        reader.Close()
        connection.koneksi.CloseKoneksi()
    End Sub

    Sub loadgrid()
        Try
            connection.koneksi.Openkoneksi()
            Dim con As New SqlClient.SqlConnection(connection.koneksi.con.ConnectionString)

            sqlcom = "select transaction_period.name as nama_periode, isnull(harga_pokok_pembelian_produk.hpp,0) as hpp"
            sqlcom = sqlcom + " from harga_pokok_pembelian_produk"
            sqlcom = sqlcom + " inner join transaction_period on transaction_period.id = harga_pokok_pembelian_produk.id_transaction_period"
            sqlcom = sqlcom + " where harga_pokok_pembelian_produk.id_produk = " & Me.tb_id_produk.Text
            sqlcom = sqlcom + " order by harga_pokok_pembelian_produk.seq desc"

            Dim da As New SqlClient.SqlDataAdapter(sqlcom, con)
            Dim ds As New DataSet()

            Using con
                con.Open()
                da.Fill(ds, "harga_pokok_pembelian_produk")
                Me.dg_data.DataSource = ds.Tables("harga_pokok_pembelian_produk").DefaultView

                If ds.Tables("harga_pokok_pembelian_produk").Rows.Count > 0 Then
                    If ds.Tables("harga_pokok_pembelian_produk").Rows.Count > 10 Then
                        Me.dg_data.AllowPaging = True
                        Me.dg_data.PagerStyle.Mode = PagerMode.NumericPages
                        Me.dg_data.PagerStyle.Position = PagerPosition.Top
                        Me.dg_data.PagerStyle.HorizontalAlign = HorizontalAlign.Right
                        Me.dg_data.PageSize = 10
                    Else
                        Me.dg_data.AllowPaging = False
                    End If
                    Me.dg_data.DataBind()
                    Me.dg_data.Visible = True
                Else
                    Me.dg_data.Visible = False
                End If
            End Using
            connection.koneksi.CloseKoneksi()
        Catch ex As Exception
            Me.lbl_msg.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Me.clearproduk()
            Me.tb_id_produk.Attributes.Add("style", "display: none;")
            Me.link_refresh_produk.Attributes.Add("style", "display: none;")
            Me.link_popup_produk.Attributes.Add("onclick", "popup_produk_item('" & Me.tb_id_produk.ClientID & "','" & Me.link_refresh_produk.UniqueID & "')")
        End If
    End Sub

    Protected Sub btn_close_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.Click
        Response.Redirect("~/blank.aspx")
    End Sub

    Protected Sub link_refresh_produk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles link_refresh_produk.Click
        Me.bindproduk()
    End Sub
End Class
