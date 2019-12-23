using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

//********************************************************************************************************
//PageName        : CreateTicket
//Description     : User can view ticket solution from this page 
//AddedBy         :                    AddedOn   : **/**/2017
//UpdatedBy       :                    UpdatedOn : 
//Reason          : 
//********************************************************************************************************

public partial class Ticket_ViewSolution : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string id;

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        StringWriter Html = new StringWriter();
        HtmlTextWriter Render = new HtmlTextWriter(Html);
        base.Render(Render);
        writer.Write(Html.ToString()
        .Replace("name=\"ctl00$ContentPlaceHolder1$redirect_url", "name=\"redirect_url")
        .Replace("id=\"ctl00_ContentPlaceHolder1_redirect_url", "id=\"redirect_url"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"] != null && Session["dheyaEmail"] != null)
        {
            if (!IsPostBack)
            {
                bindgrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }
    private void bindgrid()
    {
        try
        {
            if (Session["uid"] != null)
            {
                string strcmd;
                id = Request.QueryString["id"];
                string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    //select query used to show solution & modified date
                    strcmd = "select name 'Solution',date_modified 'Modified Date'  from suitecrm.aop_case_updates where case_id = '" + id + "' order by date_modified desc";
                    MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();

                    }
                    else
                    {
                        lbl_rowcount.Visible = false;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            Response.Redirect("~/Login.aspx", false);
        }
        hf_cdf.Value = id;

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {


    }
}