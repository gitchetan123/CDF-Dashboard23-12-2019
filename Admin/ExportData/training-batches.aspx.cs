using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class Admin_ExportData_training_batches : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string file_name = null;
    int row_count;
    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();
    filter_query filter = new filter_query();
    DataSet ds1 = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        if (!IsPostBack)
        {
            lbl_msg.Visible = false;
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();
        }
    }

    protected void ddl_cdfLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            clear();
            if (IsValid)
            {
                GridView1.DataSource = BindGridView();
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //check rows count
        if (GridView1.Rows.Count == 0)
        {
            lbl_msg.Visible = true;
            lbl_msg.Text = "There are no Records for the selected status......";
        }
        //check rows is grater than  zero
        if (GridView1.Rows.Count > 0)
        {
            lbl_msg.Visible = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (Session["SortedView"] != null)
        {
            GridView1.DataSource = Session["SortedView"];
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();
        }
    }
    

    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }

    private DataTable BindGridView()
    {
        try
        {
            file_name = "training_batches ";

            string strcmd = " select t.batchName as BatchName,t.cdfcount as TotalNumberOfPeopleInBatch,t.trainerName Trainer_Name, t.date as Date_Conducted,t.location as Location,c.name as CityName, " +
                               " t.cdfLevel from tblTrainingBatch as t join tblCitiesMaster as c on t.cityId = c.id where date <> '1990-01-01' ";
           
            //if text box txt_name is not empty then like operator will be find data with avlible text name
            if (txt_name.Text != "")
            {
                strcmd += " and t.batchName like '%" + txt_name.Text.Trim() + "%' or t.trainerName like '%" + txt_name.Text.Trim() + "%'  or t.location like '%" + txt_name.Text.Trim() + "%' or c.name like '%" + txt_name.Text.Trim() + "%' ";
                file_name += txt_name.Text + " & ";
            }
            if (ddlLevel.SelectedValue != "0")
            {
                strcmd += " and t.cdfLevel = '"+ ddlLevel.SelectedValue + "' ";
                file_name += "Level="+ ddlLevel.SelectedValue + " & ";
            }
            if (txt_count.Text != "")
            {
                strcmd += " and t.cdfcount = '"+ txt_count.Text + "' ";
                file_name +="& count="+ txt_count.Text + " & ";
            }

            //create a dataset object and fill it 
            ds1 = dbContext.ExecDataSet(strcmd);
            int row_count = ds1.Tables[0].Rows.Count;
            lbl_rowcount.Text = "Total - " + row_count.ToString();
            return ds1.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
            return null;
        }
    }


    private DataTable BindGridView(string strcmd)
    {
        try
        {
            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            int row_count = ds.Tables[0].Rows.Count;
            lbl_rowcount.Text = "Total - " + row_count.ToString();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
            return null;
        }
    }

    protected void btn_advance_preview_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                GridView1.DataSource = BindGridView();
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Error(ex);

        }

    }

    private void ExportToExcel_function()
    {
        try
        {
            BindGridView();
            file_name += " Data";
            DataSet ds = new DataSet();
            ds = ds1;
            row_count = ds.Tables[0].Rows.Count;

            if (ds.Tables[0].Rows.Count > 0)
            {
                //Create a dummy GridView
                GridView G1 = new GridView();
                G1.AllowPaging = false;
                G1.DataSource = ds.Tables[0];
                G1.DataBind();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition",
                 "attachment;filename=" + file_name + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                for (int i = 0; i < G1.Rows.Count; i++)
                {
                    //Apply text style to each Row
                    G1.Rows[i].Attributes.Add("class", "textmode");
                }
                G1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Save_export_data_tracking();
                Response.Flush();
                Response.End();


            }
            else
            {
                lbl_msg.Visible = true;
                lbl_msg.Text = "No data found for export. Please try again......";
                lbl_msg.ForeColor = System.Drawing.Color.Red;
            }
        }

        //catch (System.Threading.ThreadAbortException)
        //{ }

        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong in export data. Please try again......";
            lbl_msg.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void Save_export_data_tracking()
    {
        try
        {
            string s_query = "sp_export_data_tracking";
            int createdBy = Convert.ToInt32(Session["adminuser_id"]);
            int i = dbContext.ExecNonQuery(s_query, file_name, row_count, createdBy);

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Save_export_data_tracking();
        ExportToExcel_function();
    }
}