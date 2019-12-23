using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class CDF_PreviewCDFinfo : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        if (!IsPostBack)
        {
            div_msg.Visible = false;

        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                bindgrid();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                bindgrid();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    private void bindgrid()
    {
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strcmd = "";                    
                    strcmd = "SELECT uId, fname, lname, dheyaEmail, dob, D.name as city,gender,userStatus,cdfLevel FROM tblUserMaster as M " +
                       " left outer join tblCitiesMaster as D on M.cityid = D.id   where M.userTypeId=2";
                    if (txt_fname.Text != "")
                    {
                        strcmd += " AND fname like '%" + txt_fname.Text + "%' ";
                    }
                    if (txt_email.Text != "")
                    {
                        strcmd += " AND dheyaEmail like '%" + txt_email.Text + "%' ";
                    }
                    if (txt_city.Text != "")
                    {
                        strcmd += " AND city like '%" + txt_city.Text + "%' ";
                    }

                    strcmd += "order by uId desc";

                    SqlDataAdapter da = new SqlDataAdapter(strcmd, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}