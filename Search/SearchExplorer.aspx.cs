using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Search_SearchExplorer : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    db_context_career dbContext = new db_context_career();
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

    }
    protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourses.SelectedItem.Value == "--Select--")
            {
                lbl1.Visible = false;
                ddlNext1.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                lbl2.Visible = false;
                ddlNext2.Visible = false;
            }
            if (ddlCourses.SelectedItem.Value == "10th")
            {
                lbl1.Visible = true;
                ddlNext1.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
                lbl2.Visible = false;
                ddlNext2.Visible = false;

                ddlNext1.Items.Clear();

                if (ddlNext1.Items.Count == 0)
                {
                    ListItem lst1 = new ListItem("--Select--", "--Select--");
                    ListItem lst = new ListItem("12th", "12th");
                    ddlNext1.Items.Add(lst1);
                    ddlNext1.Items.Add(lst);
                    lbl1.Text = "Your Next Education Will be :";
                }
            }
            if (ddlCourses.SelectedItem.Value == "12th")
            {
                lbl1.Visible = true;
                ddlNext1.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
                lbl2.Visible = false;
                ddlNext2.Visible = false;

                ddlNext1.Items.Clear();

                if (ddlNext1.Items.Count == 0)
                {
                    ListItem lst2 = new ListItem("--Select--", "--Select--");
                    ListItem lst3 = new ListItem("Science", "Science");
                    ListItem lst4 = new ListItem("Arts", "Arts");
                    ListItem lst5 = new ListItem("Commerce", "Commerce");
                    ListItem lst6 = new ListItem("Any Stream", "Any Stream");
                    ddlNext1.Items.Add(lst2);
                    ddlNext1.Items.Add(lst3);
                    ddlNext1.Items.Add(lst4);
                    ddlNext1.Items.Add(lst5);
                    ddlNext1.Items.Add(lst6);
                    lbl1.Text = "Select Stream :";
                }
            }
            if (ddlCourses.SelectedItem.Value == "Graduation")
            {
                lbl1.Visible = true;
                ddlNext1.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
                lbl2.Visible = false;
                ddlNext2.Visible = false;

                ddlNext1.Items.Clear();

                if (ddlNext1.Items.Count == 0)
                {
                    string query = "select distinct course13 from tbl_course_master where course13 != '-' order by course13 asc";
                    DataSet ds = dbContext.ExecDataSet(query);
                    ddlNext1.DataSource = ds.Tables[0];
                    ddlNext1.DataTextField = ds.Tables[0].Columns[0].ToString();
                    ddlNext1.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlNext1.DataBind();
                    ddlNext1.Items.Insert(0, new ListItem("--Select--", "--Select--"));
                    // ddlNext1.SelectedIndex = 0;

                }
                lbl1.Text = "Select Graduation :";

                for (int i = 0; i <= ddlNext1.Items.Count - 1; i++)
                {
                    ddlNext1.Items[i].Attributes.Add("Title", ddlNext1.Items[i].Text);
                }
            }

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }
    }
    protected void ddlNext1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNext1.SelectedItem.Value == "12th")
        {
            ddlNext2.Items.Clear();

            if (ddlNext2.Items.Count == 0)
            {
                ListItem lst2 = new ListItem("--Select--", "--Select--");
                ListItem lst3 = new ListItem("Science", "Science");
                ListItem lst4 = new ListItem("Arts", "Arts");
                ListItem lst5 = new ListItem("Commerce", "Commerce");
                ListItem lst6 = new ListItem("Any Stream", "Any Stream");
                ddlNext2.Items.Add(lst2);
                ddlNext2.Items.Add(lst3);
                ddlNext2.Items.Add(lst4);
                ddlNext2.Items.Add(lst5);
                ddlNext2.Items.Add(lst6);
                ddlNext2.Visible = true;
                lbl2.Visible = true;
                lbl2.Text = "Select Stream :";

            }
        }
    }
    private void bindData()
    {
        try
        {
            string strcmd = "";
            if (ddlCourses.SelectedItem.Value == "10th")
            {

                strcmd = "select co_id, course1,course6 from tbl_course_master where course7 like '" + ddlNext2.SelectedItem.Text + "'order by course1";

            }
            if (ddlCourses.SelectedItem.Value == "12th")
            {
                strcmd = "select co_id, course1,course6 from tbl_course_master where course7 like '" + ddlNext1.SelectedItem.Text + "'order by course1";

            }
            if (ddlCourses.SelectedItem.Value == "Graduation")
            {
                strcmd = "select co_id, course1,course6 from tbl_course_master where course13 like '" + ddlNext1.SelectedItem.Text + "'order by course1";

            }
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
            ((HyperLink)(e.Row.FindControl("hlCareername"))).NavigateUrl = "CourseDetail.aspx?id=" + e.Row.Cells[1].Text;

            ((Label)(e.Row.FindControl("lblCategory"))).Text = e.Row.Cells[3].Text;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindData();
    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        bindData();
    }
}