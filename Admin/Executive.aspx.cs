using System;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using System.Data.SqlClient;
using System.Configuration;


//********************************************************************************************
//PageName        : Executive page
//Description     : This page is executive reliated work (add and update)
//AddedBy         : Bahubali                   AddedOn   : **/**/2017
//UpdatedBy       :                            UpdatedOn : 
//Reason          :
//*******************************************************************************************

public partial class requirement_Admin_executive : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string strcon = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();
    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        //only first time execuite on below code 
        if (!IsPostBack)
        {
            clear_data();
            FillGrid();
        }
    }

    //This funcation will fill data in grid View 
    void FillGrid()
    {
        try
        {
            string SP_Name = "SP_SELECT_USERES", SP_Type = "EXECUTIVE_LIST";
            DataTable dt = new DataTable();

            dt = dbContext.FillGrid(SP_Name, SP_Type);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    grid_exe.DataSource = dt;
                    grid_exe.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }
    }
    //clear all fields 
    void clear_data()
    {
        hf_id.Value = "";
        txt_email.Text = "";
        txt_name.Text = "";
        drop_status.SelectedValue = "--Select--";
        btn_save.Enabled = true;
        btn_update.Enabled = false;

        div_msg.Visible = false;
        div_msg.Attributes["class"] = "";
        div_msg.InnerHtml = "";
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                string str = "select id from tblExecutive where exeEmail='" + txt_email.Text + "'";
                SqlCommand cmd = new SqlCommand(str, connection);
                connection.Open();
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                if (id == 0)
                {
                    //Insert a new Executive data 
                    string strcmd = "insert into tblExecutive (exeName,exeEmail,status,password,forUserType,dateOfReg)values ('" + txt_name.Text + "','" + txt_email.Text + "','" + drop_status.Text + "','Dheya@123',2,getdate())";
                    //int i = dbContext.ExecNonQuery(strcmd);
                    cmd = new SqlCommand(strcmd, connection);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        clear_data();
                        FillGrid();

                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerHtml = "Executive create successfully";
                    }

                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerHtml = "Email id already exists";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            //update existing Executive details
            string strcmd = "update tblExecutive set exeName='" + txt_name.Text + "',exeEmail='" + txt_email.Text + "',status='" + drop_status.Text + "' where id='" + hf_id.Value + "'";
            int i = dbContext.ExecNonQuery(strcmd);
            if (i > 0)
            {
                clear_data();
                FillGrid();
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-success";
                div_msg.InnerHtml = "Executive Update successfully";
                FillGrid();
            }

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        //call clear_data method
        clear_data();
    }
    protected void grid_exe_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fill data gridview to controls
        div_msg.Attributes["class"] = "";
        div_msg.InnerHtml = "";
        hf_id.Value = grid_exe.SelectedValue.ToString();
        txt_name.Text = grid_exe.SelectedRow.Cells[2].Text;
        txt_email.Text = grid_exe.SelectedRow.Cells[3].Text;
        drop_status.SelectedValue = grid_exe.SelectedRow.Cells[5].Text;
        btn_save.Enabled = false;
        btn_update.Enabled = true;
    }
    protected void grid_exe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //pagening change update gridview
        grid_exe.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void grid_exe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button buttonStatus = (Button)e.Row.FindControl("btnStatus");

            string status = e.Row.Cells[5].Text;
            if (status == "ACTIVE")
            {
                buttonStatus.CssClass = "btn btn-success btn-sm btn-block";
            }
            else
            {
                buttonStatus.CssClass = "btn btn-danger btn-sm btn-block";
            }
        }

    }

    protected void grid_exe_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ChkStatus")
        {
            string args = e.CommandArgument.ToString();
            string[] var = args.Split(',');
            string id = var[0];
            string status = var[1];

            string query = "";

            using (SqlConnection con = new SqlConnection(strcon))
            {
                if (status == "ACTIVE")
                {
                    query = " update tblExecutive set status = 'DEACTIVE' where id = '" + id + "' and status = 'ACTIVE' ";
                }
                if (status == "DEACTIVE")
                {
                    query = " update tblExecutive set status = 'ACTIVE' where id = '" + id + "' and status = 'DEACTIVE' ";
                }
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Response.Write("<script type='text/javascript'>alert('Status Updated Successfuly') </script>");
                }
                con.Close();

                FillGrid();
                //Response.Redirect("admin-users.aspx", false);
            }
        }
    }
}