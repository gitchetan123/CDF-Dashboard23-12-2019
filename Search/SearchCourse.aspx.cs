using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Search_SearchCourse : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    // string strcmd, s; 
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
                //string query1 = "select distinct co_id,course1 from tbl_course_master order by course1 ASC";
                string query1 = "select distinct co_id,course_name from tbl_newcourse_master order by course_name ASC ";
                DataSet ds1 = dbContext.ExecDataSet(query1);
                drop_course.DataSource = ds1.Tables[0];
                drop_course.DataTextField = ds1.Tables[0].Columns[1].ToString();
                drop_course.DataValueField = ds1.Tables[0].Columns[0].ToString();
                drop_course.DataBind();
                drop_course.Items.Insert(0, new ListItem("--Select--", ""));
                drop_course.SelectedIndex = 0;


                for (int i = 0; i <= drop_course.Items.Count - 1; i++)
                {
                    drop_course.Items[i].Attributes.Add("Title", drop_course.Items[i].Text);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong on form loading. Please Try again.";
            }
        }
    }
    private void binddata()
    {
        try
        {
            string strcmd = "";
            //strcmd = "select co_id, course1,course6 from tbl_course_master where course1 like '" + drop_course.SelectedItem.Text + "'order by course1";
            strcmd = "select co_id, course_name, field_of_work  from tbl_newcourse_master where course_name like '" + drop_course.SelectedItem.Text + "'order by course_name";
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
            ((HyperLink)(e.Row.FindControl("hlCareername"))).NavigateUrl = "CourseDetail.aspx?id=" + e.Row.Cells[1].Text;

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
        binddata();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(drop_course.SelectedValue);
        binddata();
    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        binddata();
    }
}