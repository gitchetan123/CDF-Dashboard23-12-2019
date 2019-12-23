using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data.SqlClient;

//********************************************************************************************
//PageName        : Details   
//Description     : This page is display User list and export
//AddedBy         : Bahubali                   AddedOn   : **/**/2017
//UpdatedBy       :                            UpdatedOn : 
//Reason          : 
//*******************************************************************************************

public partial class candidatelist : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        if (!IsPostBack)
        {
            lbl_msg.Visible = false;
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();
            try
            {
                string StrQuery2 = "select Distinct B.id,B.name,A.cityid from tblUserMaster as A  Inner Join tblCitiesMaster as B on A.cityid = B.id order by B.name";
                dbContext.BindDropDownlist(StrQuery2, ref ddl_city);

                string StrQueryExe = "select id,exeName from tblExecutive where status ='ACTIVE'";
                dbContext.BindDropDownlist(StrQueryExe, ref ddl_ename);

                string StrQueryBatch = " select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) order by date desc";
                dbContext.BindDropDownlist(StrQueryBatch, ref ddl_batch);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                // if condition fails then user will get following message
                //div_msg.Visible = true;
                //div_msg.Attributes["class"] = "alert alert-danger";
                //div_msg.InnerText = "Something wrong on form loading. Please Try again." + ex.Message;
            }
        }
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    //protected void btn_export_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //       string strcmd = "SELECT A.uId as id,A.dheyaemail,fname,lname,dob,B.qualification,B.designation,B.oraganisation,A.regDateTime,C.name as city,a.status,userStatus, ISNULL(p.teststatus, 'Incomplete') AS Teststatus, s.TotalPayment " +
    //         "FROM(select u.uId, ISNULL(SUM(amount),0) as TotalPayment from tblPayment as p " +
    //         "Right Outer Join tblUserMaster as u on p.uId = u.uId " +
    //         "group by u.uId,u.userTypeId having u.userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s " +
    //         "Left Outer Join(select * from tblUserMaster where userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS A on A.uId = s.uId " +
    //         "LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId " +
    //         "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
    //         "LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId  " +
    //         "LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 where cdfApproved is null and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

    //        //if text box txt_name is not empty then like operator will be find data with avlible text name
    //        if (txt_name.Text != "")
    //        {
    //            strcmd += " AND (fname like '%" + txt_name.Text.Trim() + "%'  or lname like '%" + txt_name.Text.Trim() + "%' or email like '%" + txt_name.Text.Trim() + "%' or dheyaEmail like '%" + txt_name.Text.Trim() + "%' or contactNo like '%" + txt_name.Text.Trim() + "%') ";
    //        }

    //        //if dropdown ddl_testStatus is not empty then like operator will be find data with available test approval Status
    //        if (ddl_testApproveStatus.SelectedValue != "Select")
    //        {
    //            if (ddl_testApproveStatus.SelectedValue == "APPROVED")
    //                strcmd += " AND a.status ='" + ddl_testApproveStatus.SelectedValue + "'";
    //            else
    //                strcmd += " AND (a.status <> 'APPROVED' or a.status IS NULL)";
    //        }

    //        //if dropdown ddl_testCompStatus is not empty then like operator will be find data with available test Complete Status
    //        if (ddl_testCompStatus.SelectedValue != "Select")
    //        {
    //            if (ddl_testCompStatus.SelectedValue == "Incomplete")
    //            {
    //                strcmd += " AND Teststatus is null ";
    //            }
    //            else
    //            {
    //                strcmd += " AND Teststatus like '%" + ddl_testCompStatus.SelectedValue + "%' ";
    //            }
    //        }

    //        //if dropdown ddl_testCompStatus is not empty then like operator will be find data with available test Complete Status
    //        if (ddl_cdfLevel.SelectedValue != "Select")
    //        {
    //            strcmd += " AND cdfLevel like '%" + ddl_cdfLevel.SelectedValue + "%' ";
    //        }

    //        //if text box txt_city is not empty then like operator will be find data with available text city
    //        if (ddl_city.SelectedIndex > 0)
    //        {
    //            strcmd += " AND A.cityid= " + ddl_city.SelectedValue;
    //        }

    //        //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
    //        if (ddl_ename.SelectedIndex > 0)
    //        {
    //            strcmd += " AND R.executiveId =" + ddl_ename.SelectedValue;
    //        }

    //        //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
    //        if (ddl_refundStatus.SelectedValue != "Select")
    //        {
    //            strcmd += " AND B.refundStatus ='" + ddl_refundStatus.SelectedValue + "'";
    //        }

    //        //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
    //        if (ddl_rating.SelectedValue != "Select")
    //        {
    //            strcmd += " AND cdfrating ='" + ddl_rating.SelectedValue + "'";
    //        }

    //        if (ddl_batch.SelectedIndex > 0)
    //        {
    //            strcmd += " AND B.batchId =" + ddl_batch.SelectedValue;
    //        }

    //        //if dropdown ddl_cdfAproveStatus is not empty then like operator will be find data with available Executive names
    //        if (ddl_cdfAproveStatus.SelectedValue != "Select")
    //        {
    //            if (ddl_cdfAproveStatus.SelectedValue == "APPROVED")
    //                strcmd += " AND cdfApproved ='" + ddl_cdfAproveStatus.SelectedValue + "'";
    //            else
    //                strcmd += " AND (cdfApproved <> 'APPROVED' or cdfApproved IS NULL)";
    //        }

    //        //if both date are not empty then where condition will find data between date 
    //        if (txt_from.Text != "" && txt_to.Text != "")
    //        {
    //            strcmd += " AND (regDateTime BETWEEN '" + dbContext.DateConvert(txt_from.Text) + "' AND '" + dbContext.DateConvert(txt_to.Text) + "')";
    //        }

    //        //data is order by desc
    //        strcmd += "group by A.uId,A.dheyaemail,fname,lname,dob,B.qualification,B.designation,B.oraganisation,A.regDateTime,C.name,A.status,userStatus, p.teststatus,s.TotalPayment order by A.uId desc ";


    //        DataSet ds = dbContext.ExecDataSet(strcmd);

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            //Create a dummy GridView
    //            GridView G1 = new GridView();
    //            G1.AllowPaging = false;
    //            G1.DataSource = ds.Tables[0];
    //            G1.DataBind();

    //            Response.Clear();
    //            Response.Buffer = true;
    //            Response.AddHeader("content-disposition",
    //             "attachment;filename=DataTable.xls");
    //            Response.Charset = "";
    //            Response.ContentType = "application/vnd.ms-excel";
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter hw = new HtmlTextWriter(sw);

    //            for (int i = 0; i < G1.Rows.Count; i++)
    //            {
    //                //Apply text style to each Row
    //                G1.Rows[i].Attributes.Add("class", "textmode");
    //            }
    //            G1.RenderControl(hw);

    //            //style to format numbers to string
    //            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
    //            Response.Write(style);
    //            Response.Output.Write(sw.ToString());
    //            Response.Flush();
    //            Response.End();
    //        }
    //        else
    //        {
    //            lbl_msg.Visible = true;
    //            lbl_msg.Text = "No data found for export. Please try again......";
    //        }
    //    }

    //    catch (System.Threading.ThreadAbortException)
    //    { }

    //    catch (Exception ex)
    //    {
    //        Log.Error("" + ex);
    //        lbl_msg.Visible = true;
    //        lbl_msg.Text = "Something went wrong in export data. Please try again......";
    //    }
    //}

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

        //try
        //{
        //    //Response.Write(e.CommandArgument.ToString());
        //    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        //    string id = commandArgs[0];
        //    string name = commandArgs[1];
        //    string email = commandArgs[2];

        //    if (name == "DEACTIVE" && email == "DEACTIVE")
        //    {
        //        //update User status 
        //        string queryUpdateStatus = "update tblUserMaster set userStatus ='DEACTIVE' where uId='" + id + "'";
        //        int j = dbContext.ExecNonQuery(queryUpdateStatus);
        //    }
        //    else if (name == "ACTIVE" && email == "ACTIVE")
        //    {
        //        //update User status 
        //        string queryUpdateStatus = "update tblUserMaster set userStatus ='ACTIVE' where uId='" + id + "'";
        //        int j = dbContext.ExecNonQuery(queryUpdateStatus);
        //    }
        //    else
        //    {

        //    }
        //    GridView1.DataSource = BindGridView();
        //    GridView1.DataBind();
        //}
        //catch (IndexOutOfRangeException)
        //{
        //}

        //catch (Exception ex)
        //{
        //    Log.Error("" + ex);
        //}
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
            string strcmd = "SELECT A.uId as id,fname,lname,dob,A.regDateTime,C.name as city,a.status,userStatus, ISNULL(p.teststatus, 'Incomplete') AS Teststatus, s.TotalPayment " +
             "FROM(select u.uId, ISNULL(SUM(amount),0) as TotalPayment from tblPayment as p " +
             "Right Outer Join tblUserMaster as u on p.uId = u.uId and userSource='DHEYA-CDF' " +
             "group by u.uId,u.userTypeId having u.userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s " +
             "Left Outer Join(select * from tblUserMaster where userSource='DHEYA-CDF' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS A on A.uId = s.uId " +
             "LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId " +
             "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
             "LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId  " +
             "LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 where cdfApproved is null and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

            //if text box txt_name is not empty then like operator will be find data with avlible text name
            if (txt_name.Text != "")
            {
                strcmd += " AND (fname like '%" + txt_name.Text.Trim() + "%'  or lname like '%" + txt_name.Text.Trim() + "%' or email like '%" + txt_name.Text.Trim() + "%' or dheyaEmail like '%" + txt_name.Text.Trim() + "%' or contactNo like '%" + txt_name.Text.Trim() + "%') ";
            }

            //if dropdown ddl_testStatus is not empty then like operator will be find data with available test approval Status
            if (ddl_testApproveStatus.SelectedValue != "Select")
            {
                if (ddl_testApproveStatus.SelectedValue == "APPROVED")
                    strcmd += " AND a.status ='" + ddl_testApproveStatus.SelectedValue + "'";
                else
                    strcmd += " AND (a.status <> 'APPROVED' or a.status IS NULL)";
            }

            //if dropdown ddl_testCompStatus is not empty then like operator will be find data with available test Complete Status
            if (ddl_testCompStatus.SelectedValue != "Select")
            {
                if (ddl_testCompStatus.SelectedValue == "Incomplete")
                {
                    strcmd += " AND Teststatus is null ";
                }
                else
                {
                    strcmd += " AND Teststatus like '%" + ddl_testCompStatus.SelectedValue + "%' ";
                }
            }

            //if dropdown ddl_testCompStatus is not empty then like operator will be find data with available test Complete Status
            if (ddl_cdfLevel.SelectedValue != "Select")
            {
                strcmd += " AND cdfLevel like '%" + ddl_cdfLevel.SelectedValue + "%' ";
            }

            //if text box txt_city is not empty then like operator will be find data with available text city
            if (ddl_city.SelectedIndex > 0)
            {
                strcmd += " AND A.cityid= " + ddl_city.SelectedValue;
            }

            //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
            if (ddl_ename.SelectedIndex > 0)
            {
                strcmd += " AND R.executiveId =" + ddl_ename.SelectedValue;
            }

            //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
            if (ddl_refundStatus.SelectedValue != "Select")
            {
                strcmd += " AND B.refundStatus ='" + ddl_refundStatus.SelectedValue + "'";
            }

            //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
            if (ddl_rating.SelectedValue != "Select")
            {
                strcmd += " AND cdfrating ='" + ddl_rating.SelectedValue + "'";
            }

            if (ddl_batch.SelectedIndex > 0)
            {
                strcmd += " AND B.batchId =" + ddl_batch.SelectedValue;
            }

            //if dropdown ddl_cdfAproveStatus is not empty then like operator will be find data with available Executive names
            //if (ddl_cdfAproveStatus.SelectedValue != "Select")
            //{
            //    if (ddl_cdfAproveStatus.SelectedValue == "APPROVED")
            //        strcmd += " AND cdfApproved ='" + ddl_cdfAproveStatus.SelectedValue + "'";
            //    else
            //        strcmd += " AND (cdfApproved <> 'APPROVED' or cdfApproved IS NULL)";
            //}

            //if both date are not empty then where condition will find data between date 
            if (txt_from.Text != "" && txt_to.Text != "")
            {
                strcmd += " AND (regDateTime BETWEEN '" + dbContext.DateConvert(txt_from.Text) + "' AND '" + dbContext.DateConvert(txt_to.Text) + "')";
            }

            //data is order by desc
            strcmd += "group by A.uId,fname,lname,dob,A.regDateTime,C.name,A.status,userStatus, p.teststatus,s.TotalPayment order by A.uId desc ";

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
            ddl_city.SelectedIndex = 0;
            ddl_ename.SelectedIndex = 0;
            ddl_batch.SelectedIndex = 0;
           // ddl_cdfAproveStatus.SelectedIndex = 0;
            ddl_cdfLevel.SelectedIndex = 0;
            ddl_refundStatus.SelectedIndex = 0;
            ddl_testApproveStatus.SelectedIndex = 0;
            ddl_testCompStatus.SelectedIndex = 0;
            ddl_rating.SelectedIndex = 0;
            txt_from.Text = "";
            txt_to.Text = "";

        }
        catch (Exception ex)
        {
            Log.Error(ex);
          
        }

    }
}