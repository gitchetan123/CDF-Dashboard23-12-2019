using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_news_feed : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        //only first time on below code 
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
            //Select all News Feed details 
            string query_newsFeed = "select id,title,description,dateCreated,status from tblNewsFeed where userType=2";
            DataSet ds = dbContext.ExecDataSet(query_newsFeed);
            //fill data on gridview
            grid_exe.DataSource = ds;
            grid_exe.DataBind();
            ds.Dispose();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    //clear all fields 
    void clear_data()
    {
        hf_id.Value = "";
        txt_title.Text = "";
        txt_description.Text = "";
        drop_status.SelectedValue = "--Select--";
        btn_save.Enabled = true;
        btn_update.Enabled = false;
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            //Insert a new News Feed data 
            string strcmd = "insert into tblNewsFeed (title,description,status,dateCreated,userType,createdBy)values ('" + txt_title.Text + "','" + txt_description.Text + "','" + drop_status.Text + "','"+DateTime.Now+ "',2, '" + Convert.ToString(Session["adminuser_name"]) + "')";
            int i = dbContext.ExecNonQuery(strcmd);
            clear_data();
            FillGrid();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            //update existing News Feed details
            string strcmd = "update tblNewsFeed set title='" + txt_title.Text + "',description='" + txt_description.Text + "',status='" + drop_status.Text + "' where id='" + hf_id.Value + "'";
            int i = dbContext.ExecNonQuery(strcmd);
            clear_data();
            FillGrid();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
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
        hf_id.Value = grid_exe.SelectedValue.ToString();
        txt_title.Text = grid_exe.SelectedRow.Cells[2].Text;
        txt_description.Text = grid_exe.SelectedRow.Cells[3].Text;
        drop_status.SelectedValue = grid_exe.SelectedRow.Cells[4].Text;
        btn_save.Enabled = false;
        btn_update.Enabled = true;
    }
    protected void grid_exe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //pagening change update gridview
        grid_exe.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}