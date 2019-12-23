using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;


//********************************************************************************************************
//PageName        : ViewTickets
//Description     : User can View ticket from this page 
//AddedBy         :                    AddedOn   : **/**/2017
//UpdatedBy       :                    UpdatedOn : 
//Reason          : 
//********************************************************************************************************

public partial class Ticket_ViewTickets : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"] != null && Session["dheyaEmail"] != null)
        {
            if (!IsPostBack)
            {
                div_Error.Visible = false;

                lbl_rowcount.Visible = false;

                bindgrid();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null && Session["dheyaEmail"] != null)
            {
                if (IsValid)
                {
                    bindgrid();
                }
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    private void bindgrid()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["CRM_ConnectionString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                // System will show id,state,priority,subject,status,issue,entered date,description by joining 3 tables
                string strcmd = "select id,state 'State',priority 'Priority',name 'Subject',status 'Status',cust.issue_c as 'Issue',date_entered 'Date',description 'Description' FROM suitecrm.cases left join suitecrm.cases_cstm cust on cust.id_c=suitecrm.cases.id where (cust.ticket_created_by_c='" + Session["username"].ToString() + "' or cust.ticket_created_by_c='" + Session["dheyaEmail"].ToString() + "') and suitecrm.cases.deleted=0 ";

                if (txt_subject.Text != "")
                {
                    strcmd = strcmd + " AND suitecrm.cases.name like '%" + txt_subject.Text + "%'";
                }

                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    lbl_rowcount.Visible = true;
                    lbl_rowcount.Text = "Total Tickets - " + ds.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    lbl_rowcount.Visible = false;
                    div_Error.Visible = true;
                    div_Error.InnerText = "There are no records found on selected status...... ";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_Error.Visible = true;
            div_Error.InnerText = "Something went wrong. Please try again......";
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}