using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Search_EntranceExam : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string strcon = ConfigurationManager.ConnectionStrings["career_portalConnectionString"].ConnectionString.ToString();
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
                Bind_ddlExam();
                BIND_GRID();
                div_msg.Visible = false;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something went wrong...Please Try again.";
            }

        }
    }

    public void Bind_ddlExam()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("SP_DDL_BIND", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SP_Type", "ENTRANCE_EXAM");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlExam.DataSource = ds;
                        ddlExam.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlExam.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddlExam.DataBind();
                        div_msg.Visible = false;
                    }
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Something went wrong...Please Try again";
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please Try again";
        }
    }
    public void BIND_GRID()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("SP_GRID_BIND", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SP_Type", "ENTRANCE_EXAM");
                cmd.Parameters.AddWithValue("@Id", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvExam.DataSource = ds;
                        gvExam.DataBind();
                        ViewState["grid"] = "bind_grid";
                    }
                    else
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Record Not Found";
                    }
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Record Not Found";
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please Try again";
        }
    }

    public void SEARCH_GRID()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("SP_GRID_BIND", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SP_Type", "ENTRANCE_EXAM_by_ID");
                cmd.Parameters.AddWithValue("@Id", ddlExam.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvExam.DataSource = ds;
                        gvExam.DataBind();
                        ViewState["grid"] = "search_grid";
                    }
                    else
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Record Not Found";
                    }
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Record Not Found";
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please Try again";
        }
      
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlExam.SelectedValue != "0")
            {
                SEARCH_GRID();
                div_msg.Visible = false;
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Please select Exam from list to serach";
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong...Please Try again";
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("EntranceExam.aspx", false);
    }

    protected void gvExam_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string grid = ViewState["grid"].ToString();
        if (grid == "search_grid")
        {
            gvExam.PageIndex = e.NewPageIndex;
            SEARCH_GRID();
        }
        else
        {
            gvExam.PageIndex = e.NewPageIndex;
            BIND_GRID();
        }
    }
}