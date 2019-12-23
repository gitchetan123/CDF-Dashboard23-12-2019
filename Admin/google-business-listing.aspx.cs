using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Web.Services;
using System.Data.SqlClient;

public partial class Admin_google_business_listing : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string file_name = null;
    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GridView1.SelectedIndex = -1;
            if (!IsPostBack)
            {
                lbl_msg.Visible = false;
                BindUserBusiness();
                //GridView1.DataSource = BindUserBusiness();
                //GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            // if condition fails then user will get following message
            lbl_msg.Visible = true;
            lbl_msg.Attributes["class"] = "alert alert-danger";
            lbl_msg.Text = "Something wrong on form loading. Please Try again." + ex.Message;
            lbl_msg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            BindUserBusiness();
            //GridView1.DataSource = BindUserBusiness();
            //GridView1.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
            lbl_msg.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btn_Reset_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        //check rows count
        if (GridView1.Rows.Count == 0)
        {
            lbl_msg.Visible = true;
            lbl_msg.Text = "There are no Records for the selected status......";
            lbl_msg.ForeColor = System.Drawing.Color.Red;
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
            BindUserBusiness();
            //GridView1.DataSource = BindUserBusiness();
            //GridView1.DataBind();
        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";

        }
        DataView sortedView = new DataView(BindUserBusiness());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        GridView1.DataSource = sortedView;
        GridView1.DataBind();
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

    private DataTable BindUserBusiness()
    {
        try
        {
            string strcmd = "select A.uId as id, CONCAT(A.fname ,'  ', A.lname) AS 'Name', A.email, A.dheyaEmail, A.contactno, C.name as city, A.address, A.regDateTime, "
                            + " CASE WHEN B.GBL IS NULL THEN '0' ELSE B.GBL END AS GBL, "
                            + " CASE WHEN B.status IS NULL THEN 'Not Done' ELSE B.status END AS Status, "
                            + " B.store_code from  tblUserMaster AS A "
                            + " LEFT outer join tblUserBusiness AS B on A.uId = B.uId "
                            + " LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id "
                            + " where A.userTypeId = '2' and A.userSource = 'DHEYA-CDF' and A.cdfApproved = 'APPROVED' ";

            if (txt_name.Text != "")
            {
                strcmd += " and  (A.fname like '%" + txt_name.Text.Trim() + "%'  or A.lname like '%" + txt_name.Text.Trim() + "%' or A.email like '%" + txt_name.Text.Trim() + "%' or A.dheyaEmail like '%" + txt_name.Text.Trim() + "%' or A.contactNo like '%" + txt_name.Text.Trim() + "%' or C.name like '%" + txt_name.Text.Trim() + "%' or A.address like '%" + txt_name.Text.Trim() + "%') ";
            }
            if (txt_storeCode.Text != "")
            {
                strcmd += " AND B.store_code like '%" + txt_storeCode.Text + "%' ";
            }
            if (ddl_GBLStatus.SelectedValue != "0" && ddl_GBLStatus.SelectedValue != "3")
            {
                strcmd += " AND B.status ='" + ddl_GBLStatus.SelectedItem.Text + "' ";
            }
            if (ddl_GBLStatus.SelectedValue == "3")
            {
                strcmd += " AND (B.status is null  or B.status ='Not Done') ";
            }
            strcmd += " order by A.uId desc";

            DataSet ds = dbContext.ExecDataSet(strcmd);

            GridView1.DataSource = ds;
            GridView1.DataBind();

            int row_count = ds.Tables[0].Rows.Count;
            lbl_rowcount.Text = "Total - " + row_count.ToString();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
            lbl_msg.ForeColor = System.Drawing.Color.Red;
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
            lbl_msg.ForeColor = System.Drawing.Color.Red;
            return null;
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
            txt_name.Text = "";
            ddl_GBLStatus.SelectedItem.Text = "--Select--";
            txt_storeCode.Text = "";
            ddl_GBLStatus1.SelectedItem.Text = "--Select--";
            chk_GBL.Checked = false;
            txt_StoreCode1.Text = "";
            text_comment.Text = "";
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            lbl_msg.ForeColor = System.Drawing.Color.Red;
        }

    }

    #region CDF Google Business Listing
    protected void btn_saveBusinessList_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_GBLStatus1.SelectedItem.Text == "--Select--")
            {
                //BindUserBusiness();
            }
            else
            {
                bool GBL = false;
                string queryUpdate = "sp_Save_CDF_Business";
                if (chk_GBL.Checked == true) { GBL = true; } else { GBL = false; }
                int createdBy = Convert.ToInt32(Session["adminuser_id"]);
                int updatedBy = Convert.ToInt32(Session["adminuser_id"]);
                int c_id = Convert.ToInt32(ViewState["ID"]);
                int i = dbContext.ExecNonQuery(queryUpdate, c_id, GBL, ddl_GBLStatus1.SelectedItem.Text, txt_StoreCode1.Text, text_comment.Text, createdBy, updatedBy);
                if (i == -1)
                {
                    BindUserBusiness();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void ddl_GBLStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_GBLStatus1.SelectedItem.Text == "Not Done")
        {
            chk_GBL.Enabled = false;
            chk_GBL.Checked = false;
            txt_StoreCode1.Text = "NA";
            text_comment.Text = "";
        }
        if (ddl_GBLStatus1.SelectedItem.Text == "Published")
        {
            chk_GBL.Enabled = false;
            chk_GBL.Checked = true;
            txt_StoreCode1.Text = "";
            text_comment.Text = "";
        }
        if (ddl_GBLStatus1.SelectedItem.Text == "Pending")
        {
            chk_GBL.Enabled = false;
            chk_GBL.Checked = false;
            txt_StoreCode1.Text = "NA";
            text_comment.Text = "";
        }
        if (ddl_GBLStatus1.SelectedItem.Text == "Not Interested")
        {
            chk_GBL.Enabled = false;
            chk_GBL.Checked = false;
            txt_StoreCode1.Text = "NA";
            text_comment.Text = "";
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        clear();
        if (e.CommandName == "View")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            ViewState["ID"] = id;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            read_data_by_id();
        }
    }
    private void read_data_by_id()
    {
        int id = Convert.ToInt32(ViewState["ID"]);
        string get_user_by_id = "select uId, GBL, status, store_code, comment from tblUserBusiness where uId=" + id;
        SqlDataReader dr = dbContext.ExecDataReader12(get_user_by_id, id);
        if (dr.HasRows)
        {
            dr.Read();
            string GBL = dr.GetValue(1).ToString();
            if (GBL == "True") { chk_GBL.Checked = true; } else { chk_GBL.Checked = false; }
            txt_StoreCode1.Text = dr.GetValue(3).ToString();
            ddl_GBLStatus1.SelectedItem.Text = dr.GetValue(2).ToString();
            text_comment.Text = dr.GetValue(4).ToString();
        }
        dr.Close();
        dr.Dispose();
    }

    #endregion CDF Google Business Listing





}