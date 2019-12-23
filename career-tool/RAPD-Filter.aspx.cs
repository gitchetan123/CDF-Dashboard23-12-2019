using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class career_tool_RAPD_Filter : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context_career ob = new db_context_career();
    protected void Page_PreInit(Object sender, EventArgs e)
    {
        if (Session["type"] == null)
        {
            this.MasterPageFile = "~/CDFMaster.master";
        }
        else
        {
            this.MasterPageFile = "~/Admin/admin-master.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblmsg.Text = null;
            if (!IsPostBack)
            {
                try
                {
                    // lblmsg.Visible = false;
                    // Add Occupation category
                    string sqlquery = "SELECT distinct isnull(Occupational_category, 'No Catagory') as Occupationalcategory FROM tbl_career_master order by Occupationalcategory";
                    DataSet ds = ob.ExecDataSet(sqlquery);
                    drop_occupationCategory.DataSource = ds.Tables[0];
                    drop_occupationCategory.DataTextField = ds.Tables[0].Columns[0].ToString();
                    drop_occupationCategory.DataValueField = ds.Tables[0].Columns[0].ToString();
                    drop_occupationCategory.DataBind();
                    drop_occupationCategory.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    drop_occupationCategory.SelectedIndex = 0;
                    for (int i = 0; i <= drop_occupationCategory.Items.Count - 1; i++)
                    {
                        drop_occupationCategory.Items[i].Attributes.Add("Title", drop_occupationCategory.Items[i].Text);
                    }
                    drop_occupationCategory.Items.Remove("No Catagory");




                    filter.Visible = false;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }
    protected void drop_occupationCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drop_occupationCategory.SelectedItem.Value != "--Select--")
            {
                // Add Career category
                string sqlquery = "SELECT distinct isnull(Career_category, 'No Catagory') as Careercategory FROM  tbl_career_master where Occupational_category='"+ drop_occupationCategory.SelectedValue + "'";
                DataSet  ds = ob.ExecDataSet(sqlquery);
                drop_carrerCategory.DataSource = ds.Tables[0];
                drop_carrerCategory.DataTextField = ds.Tables[0].Columns[0].ToString();
                drop_carrerCategory.DataValueField = ds.Tables[0].Columns[0].ToString();
                drop_carrerCategory.DataBind();
                drop_carrerCategory.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                drop_carrerCategory.SelectedIndex = 0;
                for (int i = 0; i <= drop_carrerCategory.Items.Count - 1; i++)
                {
                    drop_carrerCategory.Items[i].Attributes.Add("Title", drop_carrerCategory.Items[i].Text);
                }
                drop_carrerCategory.Items.Remove("No Catagory");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            filter.Visible = true;
            RAPDFilter();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }

    private void RAPDFilter()
    {
        try
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            string connectionString = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString;
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //Select career by ability from selected values by user
                string strcmd = "SELECT A.ca_id,rScore,aScore,pScore,dScore,Career_category As CareerCategory,Occupational_category As OccupationalCategory,basic_info1 As Career from tbl_career_master as A " +
               "inner join (SELECT ca_id, rScore, aScore, pScore, dScore FROM tblCareerRAPD where (rScore = '" + drop_rScore.SelectedValue + "' and aScore = '" + drop_aScore.SelectedValue + "' and pScore = '" + drop_pScore.SelectedValue + "' and dScore = '" + drop_dScore.SelectedValue + "')) " +
               "as B on A.ca_id = B.ca_id where A.ca_id>1 and Career_category is not null and Occupational_category is not null ";

                if (drop_carrerCategory.SelectedValue != "--Select--")
                {
                    strcmd += " and A.Career_category='"+drop_carrerCategory.SelectedValue+"'";
                }
                if (drop_occupationCategory.SelectedValue != "--Select--")
                {
                    strcmd += " and A.Occupational_category='" + drop_occupationCategory.SelectedValue + "'";
                }

                con.Open();
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    GridView1.DataSource = dr;
                    GridView1.DataBind();
                }
                else
                {
                    lblmsg.Text = "No careers found for this RAPD combination.";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            filter.Visible = false;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }
    }


}
