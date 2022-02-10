using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public class tradingClass
{
    private SqlConnection sqlConnection = null;
    private SqlDataAdapter sqlDataAdapter = null;
    private SqlCommandBuilder sqlCommandBuilder = null;
    private DataTable dataTable = null;
    private String query = String.Empty;

    public String SQLServerConnectionString()
    {
        try
        {
            return ConfigurationManager.ConnectionStrings["trading"].ConnectionString;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Alert(String Message, Page Page)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ShowPopup", "alert('" + Message + "');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void ShowEmptyGridView(DataTable DataTable, System.Web.UI.WebControls.GridView GridView)
    {
        try
        {
            if (DataTable.Rows.Count == 0)
            {
                DataTable.Rows.Add(DataTable.NewRow());
                GridView.DataSource = DataTable;
                GridView.DataBind();
                GridView.Rows[0].Visible = false;
                GridView.Rows[0].Controls.Clear();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String DateValidated(String Date)
    {
        try
        {
            return Date.Substring(3, 2) + "/" + Date.Substring(0, 2) + "/" + Date.Substring(6, 4);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public String DateFormated(String Date)
    //{
    //    try
    //    {
    //        return Date.Substring(6, 4) + "-" + Date.Substring(0, 2) + "-" + Date.Substring(3, 2);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public Int64 PiutangKaryawanMaxAccountNo()
    {
        try
        {
            query = "select max(substring(accountno,10,99999)) from [trading].[dbo].[COA_list] where [InaName] like '%piutang karyawan%'";
            dataTable = DataTableQuery(query);
            return System.Convert.ToInt64(dataTable.Rows[0].ItemArray[0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Int64 HutangLainLainMaxAccountNo()
    {
        try
        {
            query = "select max(substring(accountno,7,99999)) from [trading].[dbo].[COA_list] where [InaName] like '%hutang lain-lain%'";
            dataTable = DataTableQuery(query);
            return System.Convert.ToInt64(dataTable.Rows[0].ItemArray[0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Int64 CustomerMaxAccountNo()
    {
        try
        {

            query = "select top 1 cast(substring(akun_piutang_dagang,10,99999)as int) from daftar_customer order by 1 desc";
            dataTable = DataTableQuery(query);
            return System.Convert.ToInt64(dataTable.Rows[0].ItemArray[0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Int64 SupplierMaxAccountNo()
    {
        try
        {

            query = "select top 1 cast(substring(akun_hutang_dagang,10,99999)as int) from daftar_supplier order by 1 desc";
            dataTable = DataTableQuery(query);
            return System.Convert.ToInt64(dataTable.Rows[0].ItemArray[0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String JurnalType(String ID)
    {
        try
        {

            query = "SELECT nama_jurnal FROM jenis_jurnal WHERE id = '" + ID.Trim() + "'";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Int64 SeqMax()
    {
        try
        {
            query = "SELECT isnull(max(seq),0) + 1 FROM akun_general_ledger";
            dataTable = DataTableQuery(query);
            return System.Convert.ToInt64(dataTable.Rows[0].ItemArray[0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Int64 IDJurnalMax()
    {
        try
        {
            query = "SELECT isnull(max(id_jurnal),0) + 1 FROM akun_jurnal";
            dataTable = DataTableQuery(query);
            return System.Convert.ToInt64(dataTable.Rows[0].ItemArray[0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Int64 IDTransaksiMax()
    {
        try
        {
            query = "SELECT isnull(max(id_transaksi),0) + 1 FROM akun_general_ledger";
            dataTable = DataTableQuery(query);
            return System.Convert.ToInt64(dataTable.Rows[0].ItemArray[0].ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String KursBulanan(String kurs, String id)
    {
        try
        {
            query = "SELECT isnull(" + kurs + ",0) FROM [trading].[dbo].[transaction_period] WHERE id = " + id.Trim();
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String COA(String filter)
    {
        try
        {
            query = "SELECT AccountNo FROM COA_list WHERE InaName like '%" + filter.Trim() + "'";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void InsertAkunJurnal(DateTime tgl_jurnal, Int32 id_jurnal, String nama_jurnal, String keterangan, Int64 id_transaction_period)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "INSERT INTO akun_jurnal (id_jurnal,tgl_jurnal,nama_jurnal,keterangan,id_transaction_period) VALUES (@id_jurnal,@tgl_jurnal,@nama_jurnal,@keterangan,@id_transaction_period)";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_jurnal", id_jurnal);
            sqlCommand.Parameters.AddWithValue("@tgl_jurnal", tgl_jurnal);
            sqlCommand.Parameters.AddWithValue("@nama_jurnal", nama_jurnal);
            sqlCommand.Parameters.AddWithValue("@keterangan", keterangan);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", id_transaction_period);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteAkunJurnal(String keterangan, Int64 id_transaction_period)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "DELETE FROM akun_jurnal WHERE keterangan = @keterangan and id_transaction_period = @id_transaction_period";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@keterangan", keterangan);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", id_transaction_period);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void InsertAkunGeneralLedger(Int64 seq, Int64 id_transaksi, DateTime tgl_transaksi, String coa_code, String coa_code_lawan, Decimal nilai_debet, Decimal nilai_kredit, String keterangan, Int64 id_transaction_period, String id_currency, Decimal kurs, Decimal nilai_debet_usd, Decimal nilai_kredit_usd, String no_voucher)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "INSERT INTO akun_general_ledger (seq,id_transaksi,tgl_transaksi,coa_code,coa_code_lawan,nilai_debet,nilai_kredit,keterangan,id_transaction_period,id_currency,kurs,nilai_debet_usd,nilai_kredit_usd,no_voucher) VALUES (@seq,@id_transaksi,@tgl_transaksi,@coa_code,@coa_code_lawan,@nilai_debet,@nilai_kredit,@keterangan,@id_transaction_period,@id_currency,@kurs,@nilai_debet_usd,@nilai_kredit_usd,@no_voucher)";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@seq", seq);
            sqlCommand.Parameters.AddWithValue("@id_transaksi", id_transaksi);
            sqlCommand.Parameters.AddWithValue("@tgl_transaksi", tgl_transaksi);
            sqlCommand.Parameters.AddWithValue("@coa_code", coa_code);
            sqlCommand.Parameters.AddWithValue("@coa_code_lawan", coa_code_lawan);
            sqlCommand.Parameters.AddWithValue("@nilai_debet", nilai_debet);
            sqlCommand.Parameters.AddWithValue("@nilai_kredit", nilai_kredit);
            sqlCommand.Parameters.AddWithValue("@keterangan", keterangan);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", id_transaction_period);
            sqlCommand.Parameters.AddWithValue("@id_currency", id_currency);
            sqlCommand.Parameters.AddWithValue("@kurs", kurs);
            sqlCommand.Parameters.AddWithValue("@nilai_debet_usd", nilai_debet_usd);
            sqlCommand.Parameters.AddWithValue("@nilai_kredit_usd", nilai_kredit_usd);
            sqlCommand.Parameters.AddWithValue("@no_voucher", no_voucher);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteAkunGeneralLedger(String keterangan, Int64 id_transaction_period)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "DELETE FROM akun_general_ledger WHERE keterangan = @keterangan and id_transaction_period = @id_transaction_period";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@keterangan", keterangan);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", id_transaction_period);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteHistoryKas(String keterangan, Int64 id_transaction_period)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "DELETE FROM history_kas WHERE keterangan = @keterangan and id_transaction_period = @id_transaction_period";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@keterangan", keterangan);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", id_transaction_period);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SessionUser(DataTable DataTable)
    {
        try
        {
            HttpContext.Current.Session["code"] = DataTable.Rows[0].ItemArray[0].ToString();
            HttpContext.Current.Session["code_group"] = DataTable.Rows[0].ItemArray[1].ToString();
            HttpContext.Current.Session["username"] = DataTable.Rows[0].ItemArray[2].ToString();
            HttpContext.Current.Session["pwd"] = DataTable.Rows[0].ItemArray[3].ToString();
            HttpContext.Current.Session["email"] = DataTable.Rows[0].ItemArray[4].ToString();
            HttpContext.Current.Session["status"] = DataTable.Rows[0].ItemArray[5].ToString();
            HttpContext.Current.Session["nama_pegawai"] = DataTable.Rows[0].ItemArray[6].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SessionSecurity()
    {
        try
        {
            if (HttpContext.Current.Session["Authenticate"] == null)
            {
                HttpContext.Current.Response.Redirect("~/login.aspx", false);
            }
            else
            {
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SessionEnd()
    {
        try
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void OpenModalBrowser(System.Web.UI.Page Page, String URL, String Width, String Height)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "OpenModalBrowser", "window.showModalDialog('" + URL + "','Expense Control System','dialogWidth:" + Width + "px;dialogHeight:" + Height + "px;');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void OpenBrowser(System.Web.UI.Page Page, String URL, String Width, String Height, String Location, String Resizable)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "OpenBrowser", "window.open('" + URL + "','','width=" + Width + ",height=" + Height + ",location=" + Location + ",resizable=" + Resizable + "')", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void PrintWebPage()
    {
        try
        {
            HttpContext.Current.Response.Write("<script>window.print();window.onfocus=function(){ window.close();}</script>");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void CloseBrowser()
    {
        try
        {
            HttpContext.Current.Response.Write("<script>window.close();</script>");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void WriteExcel(String File, DataTable DataTable, Int32 StartIndex)
    {
        try
        {
            NPOI.HSSF.UserModel.HSSFWorkbook HSSFWorkbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.Sheet sheet = HSSFWorkbook.CreateSheet();
            NPOI.SS.UserModel.Row headerRow = sheet.CreateRow(0);

            foreach (DataColumn column in DataTable.Columns)
            {
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }


            int rowIndex = StartIndex;

            foreach (DataRow row in DataTable.Rows)
            {
                NPOI.SS.UserModel.Row dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in DataTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }

            System.IO.FileStream FileStream = new System.IO.FileStream(HttpContext.Current.Server.MapPath(File), System.IO.FileMode.Create);
            HSSFWorkbook.Write(FileStream);
            FileStream.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable DataTableQuery(String SQL)
    {
        try
        {
            System.Data.SqlClient.SqlConnection SqlConnection = new System.Data.SqlClient.SqlConnection(SQLServerConnectionString());
            SqlConnection.Open();
            System.Data.SqlClient.SqlDataAdapter SqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(SQL, SqlConnection);
            System.Data.DataTable DataTable = new System.Data.DataTable();
            SqlDataAdapter.Fill(DataTable);
            SqlConnection.Close();
            return DataTable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String IDPeriodConverter(String year, String month)
    {
        try
        {
            query = "select id from transaction_period where tahun = '" + year + "' and bulan = '" + month + "'";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Boolean IDPeriodChecking(String year, Int32 startMonth, Int32 endMonth)
    {
        Boolean result = true;
        try
        {
            for (int index = startMonth; index <= endMonth; index++)
            {
                query = "select id from transaction_period where tahun = '" + year + "' and bulan = '" + index + "'";
                dataTable = DataTableQuery(query);

                if (dataTable.Rows.Count == 1)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

                dataTable.Clear();
            }

            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String DebitCreditParentNoBalance(String idPeriod, String COA)
    {
        try
        {
            query = "select isnull((select agl.nilai from coa_list cl inner join";
            query += " (select coa_list.parentacc, sum(isnull(akun_general_ledger.nilai_debet,0)) - sum(isnull(akun_general_ledger.nilai_kredit,0)) as nilai";
            query += " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code";
            query += " where akun_general_ledger.id_transaction_period = '" + idPeriod + "'";
            query += " group by coa_list.parentacc) ";
            query += " as agl on cl.accountno = agl.parentacc";
            query += " where agl.parentacc = '" + COA + "'),0)";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String DebitCreditParentBalance(String idPeriod, String COA)
    {
        try
        {
            query = "select isnull((select saldo_awal from saldo where id_transaction_period = '" + idPeriod + "' and AccountNo = '" + COA + "'),0)";
            query += " + ";
            query += " isnull((select agl.nilai from coa_list cl inner join";
            query += " (select coa_list.parentacc, sum(isnull(akun_general_ledger.nilai_debet,0)) - sum(isnull(akun_general_ledger.nilai_kredit,0)) as nilai";
            query += " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code";
            query += " where akun_general_ledger.id_transaction_period = '" + idPeriod + "'";
            query += " group by coa_list.parentacc) ";
            query += " as agl on cl.accountno = agl.parentacc";
            query += " where agl.parentacc = '" + COA + "'),0)";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String DebitCreditNoParentBalance(String idPeriod, String COA)
    {
        try
        {
            query = "select isnull((select saldo_awal from saldo where id_transaction_period = '" + idPeriod + "' and AccountNo = '" + COA + "'),0)";
            query += " + ";
            query += " (select isnull(sum(isnull(nilai_debet,0) - isnull(nilai_kredit,0)),0)";
            query += " from akun_general_ledger";
            query += " where id_transaction_period = '" + idPeriod + "'";
            query += " and coa_code = '" + COA + "')";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String CreditDebitParentNoBalance(String idPeriod, String COA)
    {
        try
        {
            query = "select isnull((select agl.nilai from coa_list cl inner join";
            query += " (select coa_list.parentacc, sum(isnull(akun_general_ledger.nilai_kredit,0)) - sum(isnull(akun_general_ledger.nilai_debet,0)) as nilai";
            query += " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code";
            query += " where akun_general_ledger.id_transaction_period = '" + idPeriod + "'";
            query += " group by coa_list.parentacc) ";
            query += " as agl on cl.accountno = agl.parentacc";
            query += " where agl.parentacc = '" + COA + "'),0)";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String CreditDebitParentBalance(String idPeriod, String COA)
    {
        try
        {
            query = "select isnull((select saldo_awal from saldo where id_transaction_period = '" + idPeriod + "' and AccountNo = '" + COA + "'),0)";
            query += " + ";
            query += " isnull((select agl.nilai from coa_list cl inner join";
            query += " (select coa_list.parentacc, sum(isnull(akun_general_ledger.nilai_kredit,0)) - sum(isnull(akun_general_ledger.nilai_debet,0)) as nilai";
            query += " from akun_general_ledger inner join coa_list on coa_list.accountno = akun_general_ledger.coa_code";
            query += " where akun_general_ledger.id_transaction_period = '" + idPeriod + "'";
            query += " group by coa_list.parentacc) ";
            query += " as agl on cl.accountno = agl.parentacc";
            query += " where agl.parentacc = '" + COA + "'),0)";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String CreditDebitNoParentBalance(String idPeriod, String COA)
    {
        try
        {
            query = "select isnull((select saldo_awal from saldo where id_transaction_period = '" + idPeriod + "' and AccountNo = '" + COA + "'),0)";
            query += " + ";
            query += " (select isnull(sum(isnull(nilai_kredit,0) - isnull(nilai_debet,0)),0)";
            query += " from akun_general_ledger";
            query += " where id_transaction_period = '" + idPeriod + "'";
            query += " and coa_code = '" + COA + "')";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String BeginningBalanceValue(String idPeriod, String COA)
    {
        try
        {
            query = "select saldo_awal from saldo where id_transaction_period = '" + idPeriod + "' and AccountNo = '" + COA + "'";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String ProfitLossValue(String idPeriod, String CategoryName)
    {
        try
        {
            query = "select " + CategoryName + " from lap_rugi_laba where id_transaction_period = '" + idPeriod + "'";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String BalanceSheetValue(String idPeriod, String CategoryName)
    {
        try
        {
            query = "select " + CategoryName + " from lap_neraca where id_transaction_period = '" + idPeriod + "'";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void InsertProfitLoss(String idPeriod)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "INSERT INTO lap_rugi_laba (id_transaction_period) VALUES (@id_transaction_period)";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", idPeriod);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void InsertBalanceSheet(String idPeriod)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "INSERT INTO lap_neraca (id_transaction_period) VALUES (@id_transaction_period)";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", idPeriod);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateProfitLoss(String idPeriod, String CategoryName, String CategoryValue)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "UPDATE lap_rugi_laba set " + CategoryName + " = @CategoryValue where id_transaction_period = @id_transaction_period";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CategoryValue", CategoryValue);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", idPeriod);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void UpdateBalanceSheet(String idPeriod, String CategoryName, String CategoryValue)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "UPDATE lap_neraca set " + CategoryName + " = @CategoryValue where id_transaction_period = @id_transaction_period";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CategoryValue", CategoryValue);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", idPeriod);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteProfitLoss(String idPeriod)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "DELETE lap_rugi_laba where id_transaction_period = @id_transaction_period";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", idPeriod);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteBalanceSheet(String idPeriod)
    {
        try
        {
            sqlConnection = new SqlConnection(SQLServerConnectionString());

            query = "DELETE lap_neraca where id_transaction_period = @id_transaction_period";

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_transaction_period", idPeriod);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public String PurchasingInventory(String idPeriod)
    {
        try
        {
            query = "select isnull((select sum(amount_stock) from inventory_stock_barang where id_transaction_period = '" + idPeriod + "'),0)";
            query += " + ";
            query += "isnull((select sum(amount_purchase) from inventory_stock_barang where id_transaction_period = '" + idPeriod + "'),0)";
            query += " - ";
            query += "isnull((select sum(amount_akhir) from inventory_stock_barang where id_transaction_period = '" + idPeriod + "'),0)";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public String StockInventory(String idPeriod)
    {
        try
        {
            query = "select isnull((select sum(amount_akhir) from inventory_stock_barang where id_transaction_period = '" + idPeriod + "'),0)";
            dataTable = DataTableQuery(query);
            return dataTable.Rows[0].ItemArray[0].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void ViewButtonUnsubmit(Button UnsubmitButton)
    {
        try
        {
            if (System.Convert.ToInt64(HttpContext.Current.Session["code"]) == 1)
            {
                UnsubmitButton.Visible = true;
            }
            else
            {
                UnsubmitButton.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}