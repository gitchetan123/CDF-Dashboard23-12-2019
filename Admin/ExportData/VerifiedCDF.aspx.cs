using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ExportData_VerifiedCDF : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();
    string file_name = null;
    int row_count;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    private void BindGridView()
    {
        try
        {
            //Select details id in tblVerifyRegistration table
            //string strcmd = "select vr.id,email,createDate,vr.status,e.exeName from tblVerifyRegistration as vr left outer join tblExecutive as e on e.id =vr.executiveId  order by id desc";
            string strcmd = "select vr.id,vr.email,createDate,vr.status,e.exeName,ISNULL(u.teststatus,'Incomplete') as teststatus,um.status as TestApproval,ISNULL(SUM(p.amount),0) as TotalPayment from tblVerifyRegistration as vr left outer join tblExecutive as e on e.id = vr.executiveId Left Outer join tblUserMaster as um on vr.email = um.email  Left Outer Join tblPayment as p on um.uId = p.uId Left Outer join tblUserProductMaster as u on um.uId = u.uId and u.prodid = 7 group by p.amount,vr.id,vr.email,createDate,vr.status,e.exeName,u.teststatus,um.status,um.uId order by vr.id desc";
            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            lbl_rowcount.Text = "Total - " + ds.Tables[0].Rows.Count.ToString();
            ViewState["grid"] = "load_grid";
            ViewState["Bind_Grid"] = ds;
            grid_verifiedCdf.DataSource = ds;
            grid_verifiedCdf.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            //div_msg.Visible = true;
            //div_msg.Attributes["class"] = "alert alert-danger";
            //div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        Search_Grid();
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        ExportToExcel_function();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("VerifiedCDF.aspx", false);
    }
    public void Search_Grid()
    {
        string strcmd = "select vr.id,vr.email,createDate,vr.status,e.exeName,ISNULL(u.teststatus,'Incomplete') as teststatus,um.status as TestApproval,ISNULL(SUM(p.amount),0) as TotalPayment from tblVerifyRegistration as vr left outer join tblExecutive as e on e.id = vr.executiveId Left Outer join tblUserMaster as um on vr.email = um.email  Left Outer Join tblPayment as p on um.uId = p.uId Left Outer join tblUserProductMaster as u on um.uId = u.uId and u.prodid = 7  where vr.email like '%" + txt_name.Text + "%' group by p.amount,vr.id,vr.email,createDate,vr.status,e.exeName,u.teststatus,um.status,um.uId order by vr.id desc";
        DataSet ds = dbContext.ExecDataSet(strcmd);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["grid"] = "searh_grid";
                ViewState["Search_Grid"] = ds;
                lbl_rowcount.Text = "Total - " + ds.Tables[0].Rows.Count.ToString();
                grid_verifiedCdf.DataSource = ds;
                grid_verifiedCdf.DataBind();
                lbl_msg.Visible = false;
            }
            else
            {
                lbl_rowcount.Text = "Total - 0";
                grid_verifiedCdf.DataSource = null;
                grid_verifiedCdf.DataBind();
                lbl_msg.Visible = true;
                lbl_msg.Text = "Record Not Found";
            }
        }
        else
        {
            lbl_rowcount.Text = "Total - 0";
            grid_verifiedCdf.DataSource = null;
            grid_verifiedCdf.DataBind();
            lbl_msg.Visible = true;
            lbl_msg.Text = "Record Not Found";
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
    private void ExportToExcel_function()
    {
        try
        {
            DataSet ds = null;
            string grid = ViewState["grid"].ToString();
            file_name = "verified_cdf ";
            if (grid == "searh_grid")
            {
                ds = (DataSet)ViewState["Search_Grid"];
            }
            else
            {
                ds = (DataSet)ViewState["Bind_Grid"];
            }
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

    protected void grid_verifiedCdf_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string grid = ViewState["grid"].ToString();
        if (grid == "searh_grid")
        {
            grid_verifiedCdf.PageIndex = e.NewPageIndex;
            Search_Grid();
        }
        else
        {
            grid_verifiedCdf.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}