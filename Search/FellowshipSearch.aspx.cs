using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Search_FellowshipSearch : System.Web.UI.Page
{
    dal clsdal = new dal();
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
                Bind_DDlFellowship();
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

    public void Bind_DDlFellowship()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("SP_DDL_BIND", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SP_Type", "FELLOWSHIP");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlFellowship.DataSource = ds;
                        ddlFellowship.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlFellowship.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddlFellowship.DataBind();
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
                cmd.Parameters.AddWithValue("@SP_Type", "FELLOWSHIP");
                cmd.Parameters.AddWithValue("@Id", "");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    string link = ds.Tables[0].Rows[i]["websiteLink"].ToString();
                        //    bool flag = false;
                        //    if (link.Contains("http://"))
                        //    {
                        //        flag = true;
                        //    }
                        //    if (link.Contains("https://"))
                        //    {
                        //        flag = true;
                        //    }
                        //    else
                        //    {
                        //        flag = false;
                        //    }
                        //    if (flag == false)
                        //    {
                        //        string li = "https://" + ds.Tables[0].Rows[i]["websiteLink"].ToString();
                        //        ds.Tables[0].Rows[i]["websiteLink"] = li;
                        //    }
                        //}
                        gvFellowship.DataSource = ds;
                        gvFellowship.DataBind();
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
                cmd.Parameters.AddWithValue("@SP_Type", "FELLOWSHIP_by_ID");
                cmd.Parameters.AddWithValue("@Id", ddlFellowship.SelectedValue);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvFellowship.DataSource = ds;
                        gvFellowship.DataBind();
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
            if (ddlFellowship.SelectedValue != "0")
            {
                SEARCH_GRID();
                div_msg.Visible = false;
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Please select Scholarship from list to serach";
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

    protected void gvScholarship_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string grid = ViewState["grid"].ToString();
        if (grid == "search_grid")
        {
            gvFellowship.PageIndex = e.NewPageIndex;
            SEARCH_GRID();
        }
        else
        {
            gvFellowship.PageIndex = e.NewPageIndex;
            BIND_GRID();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("FellowshipSearch.aspx", false);
    }
}