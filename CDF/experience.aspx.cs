using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class CDF_Experience_ : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dc = new db_context();
    void clear_data()
    {
        hf_id.Value = "";
        txt_company.Text = "";
        txt_position.Text = "";
        txt_jst.Text = "";
        txt_jet.Text = "";
        txt_location.Text = "";
        txt_des.Text = "";
        btn_save.Enabled = true;
        btn_update.Enabled = false;
        btn_delete.Enabled = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clear_data();
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
       // DateTime dt1,dt2;
        if (IsValid)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    string strcmd = "insert into tblExperience (cdf_company,cdf_position,cdf_job_start_date,cdf_job_end_date,cdf_location,cdf_position_discription,uId)values ";
                    strcmd += "('" + txt_company.Text + "','" + txt_position.Text + "',";
                  
                    if (txt_jst.Text == "")
                    {
                        strcmd += "null";

                    }
                    else
                    {
                        string date1 = dc.DateConvert(txt_jst.Text);
                        //dt1 = Convert.ToDateTime(date1);                                     
                        strcmd += "'" + date1 + "'";
                    }
                    strcmd += ",";
                    if (txt_jet.Text == "")
                    {
                        strcmd += "null";

                    }
                    else
                    {
                        string date2 = dc.DateConvert(txt_jet.Text);
                        //dt2 = Convert.ToDateTime(date2);                      
                        strcmd += "'" + date2 + "'";
                    }

                    strcmd += ",'" + txt_location.Text + "','" + txt_des.Text + "','" + Convert.ToInt32(Session["uid"]) + "')";

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    clear_data();
                    grid_exp.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {

        if (IsValid)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    if (txt_jst.Text == "")
                    {
                        txt_jst.Text = "null";

                    }
                    else
                    {                        
                        string date1 = dc.DateConvert(txt_jst.Text);
                        //DateTime.ParseExact(txt_jst.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                        txt_jst.Text = "'" + date1 + "'";
                    }


                    if (txt_jet.Text == "")
                    {
                        txt_jet.Text = "null";
                    }
                    else
                    {
                        string date2 = dc.DateConvert(txt_jet.Text);  //DateTime.ParseExact(txt_jet.Text.Trim(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                        txt_jet.Text = "'" + date2 + "'";
                    }

                    string strcmd = "update tblExperience set cdf_company='" + txt_company.Text + "',cdf_position='" + txt_position.Text + "',cdf_job_start_date=" + txt_jst.Text + ",cdf_job_end_date=" + txt_jet.Text + ",cdf_location='" + txt_location.Text + "',cdf_position_discription='" + txt_des.Text + "' where id='" + hf_id.Value + "'";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();

                    // update Masters in tblUserMaster table
                    strcmd = "";
                    strcmd = "update tblUserMaster set profileUpdateApproval = 0,profileDisplayApproval = 0,dateModified = (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30'))  where uId='" + Session["uid"] + "'";
                    int j = dc.ExecNonQuery(strcmd);
                    clear_data();
                    grid_exp.DataBind();
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
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string strcmd = "delete from tblExperience where id='" + hf_id.Value + "'";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    clear_data();
                    grid_exp.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        clear_data();
    }

    protected void grid_exp_SelectedIndexChanged(object sender, EventArgs e)
    {
        hf_id.Value = grid_exp.SelectedValue.ToString();
        txt_company.Text = grid_exp.SelectedRow.Cells[2].Text;
        txt_position.Text = grid_exp.SelectedRow.Cells[3].Text;
        txt_jst.Text = grid_exp.SelectedRow.Cells[4].Text != "&nbsp;" ? grid_exp.SelectedRow.Cells[4].Text : txt_jst.Text = "";
        txt_jet.Text = grid_exp.SelectedRow.Cells[5].Text != "&nbsp;" ? grid_exp.SelectedRow.Cells[5].Text : txt_jet.Text = "";
        txt_location.Text = grid_exp.SelectedRow.Cells[6].Text;
        txt_des.Text = grid_exp.SelectedRow.Cells[7].Text != "&nbsp;" ? grid_exp.SelectedRow.Cells[7].Text : txt_des.Text = "";
        btn_save.Enabled = false;
        btn_update.Enabled = true;
        btn_delete.Enabled = true;
    }
}