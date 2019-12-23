using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class Candidate_Previewleads : System.Web.UI.Page
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
                //Show preview leads of respective user
                string strcmd = "select l.first_name 'First Name',l.last_name 'Last Name',l.phone_mobile 'Contact Number', ea.email_address as 'Email Address',l.date_entered 'Date',cust.city_c 'City',l.lead_source 'Lead Source', l.status 'Lead Status', cust.lead_category_c 'Lead Category',l.lead_source_description 'Description',CONCAT(u.first_name, ' ', u.last_name) as Assigned_User,u.phone_work as Number FROM suitecrm.leads as l Left Outer Join suitecrm.leads_cstm cust on l.id = cust.id_c Left Outer Join suitecrm.email_addr_bean_rel eabl  ON l.id = eabl.bean_id  AND eabl.deleted=0 Left Outer Join suitecrm.email_addresses ea ON (eabl.email_address_id = ea.id ) and ea.deleted=0 Left Outer Join suitecrm.users as u on l.assigned_user_id = u.id AND u.deleted=0 where l.refered_by='" + Session["dheyaEmail"].ToString() + "' and l.deleted=0";

                if (txt_name.Text != "")
                {
                    strcmd += " AND l.first_name like '%" + txt_name.Text + "%'";
                }
                strcmd += " order by l.date_entered desc";
                MySqlDataAdapter da = new MySqlDataAdapter(strcmd, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    lbl_rowcount.Visible = true;
                    div_Error.Visible = false;
                    lbl_rowcount.Text = "Total Leads - " + ds.Tables[0].Rows.Count.ToString();
                }
                else
                {
                    lbl_rowcount.Visible = false;
                    div_Error.Visible = true;
                    div_Error.InnerText = "There are no records found on selected Referral Name...... ";
                    GridView1.DataSource = null;
                    GridView1.DataBind();
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
}