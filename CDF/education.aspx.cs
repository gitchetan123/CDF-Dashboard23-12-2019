using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class CDF_CDF_Edu : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    db_context dbContext = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clear_data();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {   
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Insert user's education details in tblEducation
                    string strcmd = "insert into tblEducation (cdf_college,cdf_degree,cdf_description,cdf_grade,uId)values ";
                    strcmd += "('" + txt_college.Text + "','" + txt_degree.Text + "','" + txt_des.Text + "','" + txt_grade.Text + "','" + Convert.ToInt32(Session["uid"]) + "')";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    clear_data();
                    grid_edu.DataBind();
                    //Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }

    }
    protected void grid_edu_SelectedIndexChanged(object sender, EventArgs e)
    {
        hf_id.Value = grid_edu.SelectedValue.ToString();
        txt_college.Text = grid_edu.SelectedRow.Cells[2].Text;
        txt_degree.Text = grid_edu.SelectedRow.Cells[3].Text;
        if (grid_edu.SelectedRow.Cells[4].Text != "" && grid_edu.SelectedRow.Cells[4].Text != "&nbsp;")
        {
            txt_des.Text = grid_edu.SelectedRow.Cells[4].Text;
        }
        else
        {
            txt_des.Text = "";
        }
        if (grid_edu.SelectedRow.Cells[5].Text != "" && grid_edu.SelectedRow.Cells[5].Text != "&nbsp;")
        {
            txt_grade.Text = grid_edu.SelectedRow.Cells[5].Text;
        }
        else
        {
            txt_grade.Text = "";
        }
        btn_save.Enabled = false;
        btn_update.Enabled = true;
        btn_delete.Enabled = true;
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        clear_data();
    }
    void clear_data()
    {
        hf_id.Value = "";
        txt_college.Text = "";
        txt_degree.Text = "";
        txt_des.Text = "";
        txt_grade.Text = "";
        btn_save.Enabled = true;
        btn_update.Enabled = false;
        btn_delete.Enabled = false;
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Update user's education details in tblEducation
                    string strcmd = "update tblEducation set cdf_college='" + txt_college.Text + "',cdf_description='" + txt_des.Text + "',cdf_degree='" + txt_degree.Text + "',cdf_grade='" + txt_grade.Text + "' where id='" + hf_id.Value + "'";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    // update Masters in tblUserMaster table
                    strcmd = "";
                    strcmd = "update tblUserMaster set profileUpdateApproval = 0,profileDisplayApproval = 0,dateModified = '" + DateTime.Now + "'  where uId='" + Session["uid"] + "'";
                    int j = dbContext.ExecNonQuery(strcmd);
                    clear_data();
                    grid_edu.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Delete user's education details in tblEducation
                    string strcmd = "delete from tblEducation where id='" + hf_id.Value + "'";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    clear_data();
                    grid_edu.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }
}

