using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Search_SearchCareer : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //string strcmd, s;
    string type = null;
    db_context_career dbContext = new db_context_career();
    DataSet ds = new DataSet();

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
        if (!IsPostBack)
        {
            try
            {
                //GridView1.DataSource = "";
                //GridView1.DataBind();
                string query1 = "select distinct ca_id,basic_info1 from tbl_career_master order by basic_info1 ASC";
                DataSet ds1 = dbContext.ExecDataSet(query1);
                drop_career.DataSource = ds1.Tables[0];
                drop_career.DataTextField = ds1.Tables[0].Columns[1].ToString();
                drop_career.DataValueField = ds1.Tables[0].Columns[0].ToString();
                drop_career.DataBind();
                drop_career.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                drop_career.SelectedIndex = 0;


                for (int i = 0; i <= drop_career.Items.Count - 1; i++)
                {
                    drop_career.Items[i].Attributes.Add("Title", drop_career.Items[i].Text);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

        }
    }

    private void binddata()
    {
        try
        {
            string strcmd = "";
            strcmd = "select ca_id, basic_info1,basic_info6 from tbl_career_master where basic_info1 like '" + drop_career.SelectedItem.Text + "'order by basic_info6";
            DataSet ds = dbContext.ExecDataSet(strcmd);
            GridView1.DataSource = ds;
            GridView1.Columns[1].Visible = true;
            GridView1.Columns[2].Visible = true;
            GridView1.Columns[3].Visible = true;
            GridView1.DataBind();
            GridView1.Columns[1].Visible = false;
            GridView1.Columns[2].Visible = false;
            GridView1.Columns[3].Visible = false;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex > -1)
            {
                if (e.Row.Cells[2].Text.Length > 45)
                {
                    ((HyperLink)(e.Row.FindControl("hlCareername"))).Text = e.Row.Cells[2].Text.Substring(0, 45) + "...";
                }
                else
                {
                    ((HyperLink)(e.Row.FindControl("hlCareername"))).Text = e.Row.Cells[2].Text;
                }
            ((HyperLink)(e.Row.FindControl("hlCareername"))).NavigateUrl = "careerdetails.aspx?id=" + e.Row.Cells[1].Text;

                ((Label)(e.Row.FindControl("lblCategory"))).Text = e.Row.Cells[3].Text;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        binddata_by_FutureRelevance();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(drop_career.SelectedValue);
        binddata();
    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            //binddata();
            drop_career.Enabled = true;
            drop_career.SelectedIndex = 0;
            drop_FutureRelevance.SelectedIndex = 0;
        }
    }
    protected void drop_FutureRelevance_SelectedIndexChanged(object sender, EventArgs e)
    {
        drop_career.Enabled = false;
        drop_career.SelectedIndex = 0;
        binddata_by_FutureRelevance();
    }
    protected void drop_career_SelectedIndexChanged(object sender, EventArgs e)
    {
        binddata();
    }
    private void binddata_by_FutureRelevance()
    {
        string strcmd = "";
        strcmd = "select ca_id, basic_info1,basic_info6,Career_scope from tbl_career_master where Career_scope='" + drop_FutureRelevance.SelectedItem.Text + "' order by basic_info6";
        DataSet ds = dbContext.ExecDataSet(strcmd);
        GridView1.DataSource = ds;
        GridView1.Columns[1].Visible = true;
        GridView1.Columns[2].Visible = true;
        GridView1.Columns[3].Visible = true;
        GridView1.DataBind();
        GridView1.Columns[1].Visible = false;
        GridView1.Columns[2].Visible = false;
        GridView1.Columns[3].Visible = false;
    }
}