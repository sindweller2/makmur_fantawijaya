using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Forms_login_LoginControl : System.Web.UI.UserControl
{

    public tradingClass tradingClass = new tradingClass();
    public System.Data.SqlClient.SqlConnection SqlConnection;
    public System.Data.SqlClient.SqlCommand SqlCommand;
    public System.Data.SqlClient.SqlDataAdapter SqlDataAdapter;
    public String Query;

    public System.Data.DataTable CheckLogin()
    {
        try
        {
            this.Query = "SELECT * FROM user_list WHERE username = '" + this.lgn.UserName.ToString() + "' AND pwd = '" + this.lgn.Password.ToString() + "'";
            this.SqlConnection = new System.Data.SqlClient.SqlConnection(tradingClass.SQLServerConnectionString());
            this.SqlConnection.Open();
            this.SqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter(this.Query, this.SqlConnection);
            this.SqlDataAdapter.SelectCommand.CommandTimeout = 3600;
            System.Data.DataTable DataTable = new System.Data.DataTable();
            this.SqlDataAdapter.Fill(DataTable);
            this.SqlConnection.Close();
            return DataTable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.CheckLogin().Rows.Count == 1)
            {
                Session["Authenticate"] = "Yes";
                tradingClass.SessionUser(this.CheckLogin());
            }
        }
        catch (Exception ex)
        {
            tradingClass.Alert(ex.Message, this.Page);
        }
    }
}
