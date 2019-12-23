using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_find_nearby_cdf : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        //GridView1.SelectedIndex = -1;
        if (!IsPostBack)
        {
            lbl_msg.Visible = false;
            //GridView1.DataSource = BindGridView();
           // GridView1.DataBind();
           
        }
    }

    protected void btn_preview_Click(object sender, EventArgs e)
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CandDetails")
        {
            string id = e.CommandArgument.ToString();
            if (IsValid)
            {
                try
                {
                    //  Response.Redirect("candidate-details.aspx?id=" + dbContext.EncryptData(id) + "", false);
                    Page.ClientScript.RegisterStartupScript(
           this.GetType(), "OpenWindow", "window.open('login.aspx','_newtab');", true);

                    //ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('Default.aspx');", true);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
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
        DataView sortedView = new DataView(BindGridView());
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

    private DataTable BindGridView()
    {
        try
        {
            string strcmd = "SELECT top 30 A.uId as id,A.fname,A.lname,A.regDateTime,C.name as city,A.userStatus,  B.latitude,B.longitude,A.address, "
                          + " (1 / 0.62137 * (3959 * acos(cos(radians('" + txtLatitude.Text + "')) * cos(radians(B.latitude)) * cos(radians(B.longitude) - radians('" + txtLongitude.Text + "')) + sin(radians('" + txtLatitude.Text + "')) * "
                          + " sin(radians(B.latitude))))) AS distance FROM tblUserMaster AS A "
                          + " LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId "
                          + "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id "
                          + " LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 "
                          + "where cdfApproved = 'APPROVED' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' and userSource = 'DHEYA-CDF' and A.userStatus = 'ACTIVE' and B.latitude <> '0' and B.longitude <> '0' ";
            
            //if text box txt_name is not empty then like operator will be find data with avlible text name
            //if (txtLatitude.Text != "")
            //{
            //    strcmd += " AND (B.latitude like '%" + txtLatitude.Text.Trim() + "%') ";
            //}

            //if (txtLongitude.Text != "")
            //{
            //    strcmd += " AND (B.longitude like '%" + txtLongitude.Text.Trim() + "%') ";
            //}


            //data is order by desc
            strcmd += "group by A.uId,A.fname,A.lname,A.regDateTime,C.name,A.userStatus, B.latitude,B.longitude,A.address order by distance asc ";

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

   

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        txtLatitude.Text = "";
        txtLongitude.Text = "";
    }

   
}