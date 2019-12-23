using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Search_SearchSubco_Institute : System.Web.UI.Page
{
    dal clsdal = new dal();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //check the user type 
    //        if (Session.Count > 0 && Session["user_type"].ToString().Equals("Admin"))
    //            this.Page.MasterPageFile = "~/AdminMaster.master";
    //        else if (Session.Count > 0 && Session["user_type"].ToString().Equals("USER"))
    //            this.Page.MasterPageFile = "~/UserMaster.master";
    //        else
    //            this.Page.MasterPageFile = "~/StaffMasterPage.master";
    //    }
    //    catch (Exception ex)
    //    {
    //        this.Page.MasterPageFile = "~/AdminMaster.master";
    //    }

    //}
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
                div_msg.Visible = false;
                load_career();
                load_course();
                load_subcourse();
                addcity_state();
                load_institute("SELECT distinct A.inst_id,inst_name FROM tbl_institute_master as A inner join tbl_institute_subco_details as B on A.inst_id=B.inst_id inner join tbl_course_subcourse_bridge as C on B.subco_id=C.subco_id and B.specialization=C.specialization where A.status='Approved' order by inst_name");

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
    void load_institute(string sqlqry)
    {
        string value = "--Select--";
        try
        {
            if (!ddl_instname.SelectedValue.ToString().Equals("--Select--"))
            {
                value = ddl_instname.SelectedValue;
            }
            ddl_instname.Items.Clear();
            DataSet ds1 = clsdal.ExecDataSet(sqlqry);
            ddl_instname.DataSource = ds1.Tables[0];
            ddl_instname.DataTextField = ds1.Tables[0].Columns[1].ToString();
            ddl_instname.DataValueField = ds1.Tables[0].Columns[0].ToString();
            ddl_instname.DataBind();

            ddl_instname.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            ddl_instname.SelectedValue = value;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on load institute. Please Try again." + ex;
        }


    }
    void load_career()
    {
        try
        {
            ddl_career.Items.Clear();
            string query1 = "select distinct ca_id,basic_info1 from tbl_career_master order by basic_info1 ASC";
            DataSet ds1 = clsdal.ExecDataSet(query1);
            ddl_career.DataSource = ds1.Tables[0];
            ddl_career.DataTextField = ds1.Tables[0].Columns[1].ToString();
            ddl_career.DataValueField = ds1.Tables[0].Columns[0].ToString();
            ddl_career.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on load career. Please Try again.";
        }
        ddl_career.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        ddl_career.SelectedIndex = 0;

    }
    void load_course()
    {
        try
        {
            ddl_course.Items.Clear();
            //string strcmd = "select distinct A.co_id, A.course_name FROM tbl_newcourse_master as A  where A.status='Approved' order by A.course_name";
            string strcmd = "select distinct A.co_id, A.course_name FROM tbl_newcourse_master as A  where A.status='Approved' order by A.course_name";
            DataSet ds2 = new DataSet();
            ds2 = clsdal.ExecDataSet(strcmd);
            ddl_course.DataSource = ds2.Tables[0];
            ddl_course.DataTextField = ds2.Tables[0].Columns[1].ToString();
            ddl_course.DataValueField = ds2.Tables[0].Columns[0].ToString();
            ddl_course.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on load course. Please Try again.";
        }
        ddl_course.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        ddl_course.SelectedIndex = 0;

    }
    void load_subcourse()
    {
        try
        {
            ddl_subconame.Items.Clear();
            string strcmd = "select distinct A.subco_id, A.subco_name FROM tbl_subcourse_master as A where A.status='Approved' and A.status='Approved' order by A.subco_name";
            DataSet ds3 = new DataSet();
            ds3 = clsdal.ExecDataSet(strcmd);
            ddl_subconame.DataSource = ds3.Tables[0];
            ddl_subconame.DataTextField = ds3.Tables[0].Columns[1].ToString();
            ddl_subconame.DataValueField = ds3.Tables[0].Columns[0].ToString();
            ddl_subconame.DataBind();

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on load sub course. Please Try again.";
        }
        ddl_subconame.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        ddl_subconame.SelectedIndex = 0;
        ddl_specialization.Items.Insert(0, new ListItem("--Select--", "--Select--"));
        ddl_specialization.SelectedIndex = 0;

    }
    void select_course()
    {
        try
        {
            ddl_course.Items.Clear();
            if (!ddl_career.SelectedValue.ToString().Equals("--Select--"))
            {
                string strcmd = "select distinct A.co_id, A.course_name FROM tbl_newcourse_master as A inner join tbl_career_course_bridge as B on A.co_id=B.co_id where B.ca_id=" + ddl_career.SelectedValue + " and status='Approved' order by A.course_name";
                DataSet ds2 = new DataSet();
                ds2 = clsdal.ExecDataSet(strcmd);
                ddl_course.DataSource = ds2.Tables[0];
                ddl_course.DataTextField = ds2.Tables[0].Columns[1].ToString();
                ddl_course.DataValueField = ds2.Tables[0].Columns[0].ToString();
                ddl_course.DataBind();
            }
            ddl_course.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            ddl_course.SelectedIndex = 0;


        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on select course. Please Try again.";
        }
    }
    void select_subcourse(string strcmd)
    {
        string value = "--Select--";
        try
        {
            if (!ddl_subconame.SelectedValue.ToString().Equals("--Select--"))
            {
                value = ddl_subconame.SelectedValue;
            }
            ddl_subconame.Items.Clear();
            if (!ddl_course.SelectedValue.ToString().Equals("--Select--"))
            {
                DataSet ds2 = new DataSet();
                ds2 = clsdal.ExecDataSet(strcmd);
                ddl_subconame.DataSource = ds2.Tables[0];
                ddl_subconame.DataTextField = ds2.Tables[0].Columns[1].ToString();
                ddl_subconame.DataValueField = ds2.Tables[0].Columns[0].ToString();
                ddl_subconame.DataBind();
            }
            if (!ddl_instname.SelectedValue.ToString().Equals("--Select--"))
            {
                DataSet ds2 = new DataSet();
                ds2 = clsdal.ExecDataSet(strcmd);
                ddl_subconame.DataSource = ds2.Tables[0];
                ddl_subconame.DataTextField = ds2.Tables[0].Columns[1].ToString();
                ddl_subconame.DataValueField = ds2.Tables[0].Columns[0].ToString();
                ddl_subconame.DataBind();
            }
            ddl_subconame.Items.Insert(0, new ListItem("--Select--", "--Select--"));

            ddl_subconame.SelectedValue = value;


        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on select subcourse. Please Try again.";
        }
    }
    void select_specialization()
    {
        try
        {
            ddl_specialization.Items.Clear();
            if (!ddl_subconame.SelectedValue.ToString().Equals("--Select--"))
            {
                string strcmd = "select distinct A.specialization FROM tbl_institute_subco_details as A inner join tbl_course_subcourse_bridge as B on A.subco_id=B.subco_id and A.specialization=B.specialization inner join tbl_newcourse_master as C on C.co_id=B.co_id where A.subco_id=" + ddl_subconame.SelectedValue;
                if (!ddl_course.SelectedValue.ToString().Equals("--Select--"))
                {
                    strcmd += " and B.co_id='" + ddl_course.SelectedValue + "'";
                }
                DataSet ds2 = new DataSet();
                ds2 = clsdal.ExecDataSet(strcmd);
                ddl_specialization.DataSource = ds2.Tables[0];
                ddl_specialization.DataTextField = ds2.Tables[0].Columns[0].ToString();
                ddl_specialization.DataValueField = ds2.Tables[0].Columns[0].ToString();
                ddl_specialization.DataBind();
            }
            ddl_specialization.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            ddl_specialization.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on select specialization. Please Try again.";
        }
    }
    void addcity_state()
    {
        try
        {
            ddl_state.Items.Clear();
            string[,] strdata = new string[3, 3];
            strdata[0, 0] = "@status";
            strdata[0, 1] = "Int";
            strdata[0, 2] = "1";
            strdata[1, 0] = "@state";
            strdata[1, 1] = "VarChar";
            strdata[1, 2] = "";
            strdata[2, 0] = "@city";
            strdata[2, 1] = "VarChar";
            strdata[2, 2] = "";
            DataSet ds_1 = clsdal.getDataSet("select_city_state_simsr_mt", strdata);
            ddl_state.DataSource = ds_1.Tables[0];
            ddl_state.DataTextField = ds_1.Tables[0].Columns[0].ToString();
            ddl_state.DataValueField = ds_1.Tables[0].Columns[0].ToString();
            ddl_state.DataBind();
            ddl_state.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            ddl_state.SelectedIndex = 0;

            ddl_city.Items.Clear();
            string[,] strdata1 = new string[3, 3];
            strdata1[0, 0] = "@status";
            strdata1[0, 1] = "Int";
            strdata1[0, 2] = "2";
            strdata1[1, 0] = "@state";
            strdata1[1, 1] = "VarChar";
            strdata1[1, 2] = "";
            strdata1[2, 0] = "@city";
            strdata1[2, 1] = "VarChar";
            strdata1[2, 2] = "";
            DataSet ds_2 = clsdal.getDataSet("select_city_state_simsr_mt", strdata1);
            ddl_city.DataSource = ds_2.Tables[0];
            ddl_city.DataTextField = ds_2.Tables[0].Columns[2].ToString();
            ddl_city.DataValueField = ds_2.Tables[0].Columns[2].ToString();
            ddl_city.DataBind();
            ddl_city.Items.Insert(0, new ListItem("--Select--", "--Select--"));
            ddl_city.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on load City & State. Please Try again.";
        }
    }
    protected void ddl_career_SelectedIndexChanged(object sender, EventArgs e)
    {
        select_course();
    }
    protected void ddl_course_SelectedIndexChanged(object sender, EventArgs e)
    {
        select_subcourse("select distinct A.subco_id, A.subco_name FROM tbl_subcourse_master as A inner join tbl_course_subcourse_bridge as B on A.subco_id=B.subco_id where B.co_id=" + ddl_course.SelectedValue + " and A.status='Approved' and B.status='Approved' order by A.subco_name");
    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                binddata();

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong. Please Try again.";
            }

        }
    }

    private void binddata()
    {
        try
        {

            div_msg.Visible = false;
            string strcmd = "";
            strcmd = "select distinct A.inst_id,B.inst_name,B.city,A.specialization,E.subco_name FROM tbl_institute_subco_details as A inner join tbl_institute_master as B on A.inst_id=B.inst_id inner join tbl_course_subcourse_bridge as C on A.subco_id=C.subco_id  and C.specialization=A.specialization inner join tbl_newcourse_master as D on C.co_id=D.co_id inner join tbl_subcourse_master as E on E.subco_id=A.subco_id  where B.status='Approved'";

            if (!ddl_course.SelectedValue.ToString().Equals("--Select--"))
            {
                strcmd += " and D.co_id='" + ddl_course.SelectedValue + "'";
            }
            if (!ddl_subconame.SelectedValue.ToString().Equals("--Select--"))
            {
                strcmd += " and A.subco_id='" + ddl_subconame.SelectedValue + "'";
            }
            if (!ddl_specialization.SelectedValue.ToString().Equals("--Select--"))
            {
                strcmd += " and A.specialization='" + ddl_specialization.SelectedValue + "'";
            }
            if (!ddl_state.SelectedValue.ToString().Equals("--Select--"))
            {
                strcmd += " and B.state='" + ddl_state.SelectedValue + "'";
            }
            if (!ddl_city.SelectedValue.ToString().Equals("--Select--"))
            {
                strcmd += " and B.city='" + ddl_city.SelectedValue + "'";
            }
            if (!ddl_instname.SelectedValue.ToString().Equals("--Select--"))
            {
                strcmd += " and A.inst_id ='" + ddl_instname.SelectedValue + "'";
            }
            DataSet ds = clsdal.ExecDataSet(strcmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.Columns[1].Visible = true;
                GridView1.Columns[2].Visible = true;
                GridView1.Columns[3].Visible = true;
                GridView1.Columns[4].Visible = true;
                GridView1.Columns[5].Visible = true;
                GridView1.DataBind();
                GridView1.Columns[1].Visible = false;
                GridView1.Columns[2].Visible = false;
                GridView1.Columns[3].Visible = false;
                GridView1.Columns[4].Visible = false;
                GridView1.Columns[5].Visible = false;
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "No Institute Found. Please Search again.";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong. Please Try again.";

        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[2].Text.Length > 45)
            {
                ((HyperLink)(e.Row.FindControl("Instname"))).Text = e.Row.Cells[2].Text.Substring(0, 45) + "...";
            }
            else
            {
                ((HyperLink)(e.Row.FindControl("Instname"))).Text = e.Row.Cells[2].Text;
            }
            ((HyperLink)(e.Row.FindControl("Instname"))).NavigateUrl = "institutedetails.aspx?id=" + e.Row.Cells[1].Text + "&spe=" + e.Row.Cells[4].Text + "&sid=" + ddl_subconame.SelectedValue + "";
            ((Label)(e.Row.FindControl("lblCity"))).Text = e.Row.Cells[3].Text;
            ((Label)(e.Row.FindControl("lblspecialization"))).Text = e.Row.Cells[4].Text;
            ((Label)(e.Row.FindControl("lblsubconame"))).Text = e.Row.Cells[5].Text;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        binddata();
    }
    protected void ddl_subconame_SelectedIndexChanged(object sender, EventArgs e)
    {
        select_specialization();
        if (!ddl_subconame.SelectedValue.ToString().Equals("--Select--"))
        {
            load_institute("SELECT distinct A.inst_id,inst_name FROM tbl_institute_master as A inner join tbl_institute_subco_details as B on A.inst_id=B.inst_id inner join tbl_course_subcourse_bridge as C on B.subco_id=C.subco_id and B.specialization=C.specialization where A.status='Approved' and B.subco_id=" + ddl_subconame.SelectedValue + " order by A.inst_name");
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        div_msg.Visible = false;
        load_career();
        load_course();
        load_subcourse();
        addcity_state();
        ddl_instname.SelectedIndex = 0;
        load_institute("SELECT distinct A.inst_id,inst_name FROM tbl_institute_master as A inner join tbl_institute_subco_details as B on A.inst_id=B.inst_id inner join tbl_course_subcourse_bridge as C on B.subco_id=C.subco_id and B.specialization=C.specialization where A.status='Approved' order by inst_name");

    }
    protected void ddl_instname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddl_instname.SelectedValue.ToString().Equals("--Select--"))
        {
            select_subcourse("select distinct A.subco_id, A.subco_name FROM tbl_subcourse_master as A inner join tbl_institute_subco_details as B on A.subco_id=B.subco_id where A.status='Approved' and B.inst_id=" + ddl_instname.SelectedValue + " order by A.subco_name");
        }
    }
}