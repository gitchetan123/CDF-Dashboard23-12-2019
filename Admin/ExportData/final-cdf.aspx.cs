using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;


public partial class Admin_ExportData_final_cdf : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string file_name = null;
    int row_count;
    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();
    filter_query filter = new filter_query();
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.SelectedIndex = -1;
        if (!IsPostBack)
        {
            string StrQuery4 = "select id,cdfLevel from tblCDFLevel";
            dbContext.BindDropDownlist(StrQuery4, ref ddl_cdfLevel);

            lbl_msg.Visible = false;
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();
            try
            {
                string StrQuery2 = "select Distinct B.id,B.name,A.cityid from tblUserMaster as A  Inner Join tblCitiesMaster as B on A.cityid = B.id order by B.name";
                dbContext.BindDropDownlist(StrQuery2, ref ddl_city);

                string StrQueryExe = "select id,exeName from tblExecutive where status ='ACTIVE'";
                dbContext.BindDropDownlist(StrQueryExe, ref ddl_ename);

                //string StrQueryBatch = " select id,batchName from tblTrainingBatch order by date desc";
                //dbContext.BindDropDownlist(StrQueryBatch, ref ddl_batch);

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

    protected void ddl_cdfLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StrQuery3 = "Select id, batchName From tblTrainingBatch where cdfLevel='" + ddl_cdfLevel.SelectedValue + "' order by id desc";
        dbContext.BindDropDownlist(StrQuery3, ref ddl_batch);
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



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //Response.Write(e.CommandArgument.ToString());
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string id = commandArgs[0];
            string name = commandArgs[1];
            string email = commandArgs[2];

            if (name == "DEACTIVE" && email == "DEACTIVE")
            {
                //update User status 
                string queryUpdateStatus = "update tblUserMaster set userStatus ='DEACTIVE' where uId='" + id + "'";
                int j = dbContext.ExecNonQuery(queryUpdateStatus);
            }
            else if (name == "ACTIVE" && email == "ACTIVE")
            {
                //update User status 
                string queryUpdateStatus = "update tblUserMaster set userStatus ='ACTIVE' where uId='" + id + "'";
                int j = dbContext.ExecNonQuery(queryUpdateStatus);
            }
            else
            {

            }
            GridView1.DataSource = BindGridView();
            GridView1.DataBind();
        }
        catch (IndexOutOfRangeException)
        {
        }

        catch (Exception ex)
        {
            Log.Error("" + ex);
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
            string strcmd = "SELECT A.uId as id,fname,lname,dob,A.regDateTime,C.name as city,a.status,userStatus, ISNULL(p.teststatus, 'Incomplete') AS Teststatus, s.TotalPayment, ISNULL(B.shadowSession, '0'),B.childTestStatus,B.childSessionStatus,B.spouseTestStatus,A.address,B.fieldOfWork,B.modeOfWork,B.industrySector " +
             "FROM(select u.uId, ISNULL(SUM(amount),0) as TotalPayment from tblPayment as p " +
             "Right Outer Join tblUserMaster as u on p.uId = u.uId and userSource='DHEYA-CDF' " +
             "group by u.uId,u.userTypeId having u.userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s " +
             "Left Outer Join(select * from tblUserMaster where userSource='DHEYA-CDF' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

             if (ddl_userStatus.SelectedValue != "Select")
            {
                if (ddl_userStatus.SelectedValue == "0")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text + "' ";
                }
                else if (ddl_userStatus.SelectedValue == "1")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text + "' ";
                }
                else if (ddl_userStatus.SelectedValue == "2")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text + "' ";
                }
                else if (ddl_userStatus.SelectedValue == "3")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text + "' ";
                }
            }

            strcmd +=  " ) AS A on A.uId = s.uId " +
             "LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId " +
             "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
             "LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId  " +
             "LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 where cdfApproved='APPROVED' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

            //if text box txt_name is not empty then like operator will be find data with avlible text name
            if (txt_name.Text != "")
            {
                strcmd += " AND (fname like '%" + txt_name.Text.Trim() + "%'  or lname like '%" + txt_name.Text.Trim() + "%' or email like '%" + txt_name.Text.Trim() + "%' or dheyaEmail like '%" + txt_name.Text.Trim() + "%' or contactNo like '%" + txt_name.Text.Trim() + "%') ";
            }          
                       
            //if dropdown ddl_testCompStatus is not empty then like operator will be find data with available test Complete Status
            if (ddl_cdfLevel.SelectedValue != "--Select--")
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

            if (ddlShadowSession.SelectedValue != "Select")
            {   
                if (ddlShadowSession.SelectedValue == "0")
                {
                    strcmd += " AND (B.shadowSession ='" + ddlShadowSession.SelectedValue + "' OR B.shadowSession is Null) ";
                }
                else     
                {
                    strcmd += " AND B.shadowSession ='" + ddlShadowSession.SelectedValue + "'";
                }
            }

            if (ddlChildTestStatus.SelectedValue != "Select")
            {
                if (ddlChildTestStatus.SelectedValue == "0")
                {
                    strcmd += " AND (B.childTestStatus ='" + ddlChildTestStatus.SelectedValue + "' OR B.childTestStatus is Null) ";
                }
                else
                {
                    strcmd += " AND B.childTestStatus ='" + ddlChildTestStatus.SelectedValue + "'";
                }
            }

            if (ddlChildSessionStatus.SelectedValue != "Select")
            {
                if (ddlChildSessionStatus.SelectedValue == "0")
                {
                    strcmd += " AND (B.childSessionStatus ='" + ddlChildSessionStatus.SelectedValue + "' OR B.childSessionStatus is Null) ";
                }
                else
                {
                    strcmd += " AND B.childSessionStatus ='" + ddlChildSessionStatus.SelectedValue + "'";
                }
            }

            if (ddlSpouseTestStatus.SelectedValue != "Select")
            {
                if (ddlSpouseTestStatus.SelectedValue == "0")
                {
                    strcmd += " AND (B.spouseTestStatus ='" + ddlSpouseTestStatus.SelectedValue + "' OR B.spouseTestStatus is Null) ";
                }
                else
                {
                    strcmd += " AND B.spouseTestStatus ='" + ddlSpouseTestStatus.SelectedValue + "'";
                }
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

            if (txt_location.Text != "")
            {
                strcmd += " AND (address like '%" + txt_location.Text.Trim() + "%') ";
            }

            if(txt_DOB.Text!="")
            {
                strcmd += " AND DATEPART(d, dob) = DATEPART(d, '" + dbContext.DateConvert(txt_DOB.Text+"/2018") + "') AND DATEPART(m, dob) = DATEPART(m, '" + dbContext.DateConvert(txt_DOB.Text+"/2018") + "')";
            }


            //data is order by desc
            strcmd += " group by A.uId,fname,lname,dob,A.regDateTime,C.name,A.status,userStatus, p.teststatus,s.TotalPayment,B.shadowSession,B.childTestStatus,B.childSessionStatus,B.spouseTestStatus,A.address,B.fieldOfWork,B.modeOfWork,B.industrySector order by A.uId desc ";

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
            ddl_cdfLevel.SelectedIndex = 0;
            //ddl_refundStatus.SelectedIndex = 0;
            //ddl_rating.SelectedIndex = 0;
            txt_from.Text = "";
            txt_to.Text = "";
            //ddlChildSessionStatus.SelectedIndex = 0;
            //ddlChildTestStatus.SelectedIndex = 0;
            //ddlShadowSession.SelectedIndex = 0;
            //ddlSpouseTestStatus.SelectedIndex = 0;
            txt_DOB.Text = "";

        }
        catch (Exception ex)
        {
            Log.Error(ex);

        }

    }

    private DataTable BindGridView_filter()
    {
        //  "SELECT A.uId as id,a.status,userStatus, ISNULL(p.teststatus, 'Incomplete') AS Teststatus, s.TotalPayment, ISNULL(B.shadowSession, '0'),B.childTestStatus,B.childSessionStatus,B.spouseTestStatus,A.address,B.fieldOfWork,B.modeOfWork,B.industrySector " +
        try
        {
            file_name = "final_cdf ";
            string strcmd = "SELECT  ";

            if (chk_fname.Checked == true) { strcmd += "fname as 'First Name', "; }
            if (chk_lname.Checked == true) { strcmd += "lname as 'Last Name', "; }

            if (chk_email.Checked == true) { strcmd += "email as Email, "; }
            if (chk_dheyaEmail.Checked == true) { strcmd += "dheyaEmail, "; }
            if (chk_contact.Checked == true) { strcmd += "contactno as Contact, "; }
        

            if (chk_dob.Checked == true) { strcmd += "dob as 'Birth Date', "; }
            if (chk_regDate.Checked == true) { strcmd += "A.regDateTime as 'Registration Date', "; }
            if (chk_city.Checked == true) { strcmd += "C.name as city , "; }
            if (chk_status.Checked == true) { strcmd += "a.status as 'Work Status', "; }
            if (chk_userStatus.Checked == true) { strcmd += "userStatus as 'Access Status',  "; }
            if (chk_testStatus.Checked == true) { strcmd += " ISNULL(p.teststatus, 'Incomplete') AS Teststatus, "; }
            if (chk_shadowSession.Checked == true) { strcmd += "ISNULL(B.shadowSession, '0'), "; }
            if (chk_childTestStatus.Checked == true) { strcmd += "B.childTestStatus, "; }
            if (chk_childSessionStatus.Checked == true) { strcmd += "B.childSessionStatus, "; }
            if (chk_spouseTestStatus.Checked == true) { strcmd += "B.spouseTestStatus, "; }
            if (chk_address.Checked == true) { strcmd += "A.address as Address, "; }
            if (chk_fieldOfWork.Checked == true) { strcmd += "B.fieldOfWork ,"; }
            if (chk_modeOfWork.Checked == true) { strcmd += "B.modeOfWork, "; }
            if (chk_modeOfWork.Checked == true) { strcmd += "B.industrySector,  "; }
            if (chk_pin.Checked == true) { strcmd += "A.pincode, "; }
            if (chk_longitude.Checked == true) { strcmd += "B.latitude,  "; }
            if (chk_latitude.Checked == true) { strcmd += "B.longitude, "; }

            strcmd += " '' FROM(select u.uId, ISNULL(SUM(amount),0) as TotalPayment from tblPayment as p " +
             "Right Outer Join tblUserMaster as u on p.uId = u.uId and userSource='DHEYA-CDF' " +
             "group by u.uId,u.userTypeId having u.userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s " +
             "Left Outer Join(select * from tblUserMaster where userSource='DHEYA-CDF' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

            if (ddl_userStatus.SelectedValue != "Select")
            {
                if (ddl_userStatus.SelectedValue == "0")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text +"' ";
                    file_name += "userStatus=" + ddl_userStatus.SelectedItem.Text + "&";
                }
                else if(ddl_userStatus.SelectedValue == "1")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text + "' ";
                    file_name += "userStatus=" + ddl_userStatus.SelectedItem.Text + "&";
                }
                else if (ddl_userStatus.SelectedValue == "2")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text + "' ";
                    file_name += "userStatus=" + ddl_userStatus.SelectedItem.Text + "&";
                }
                else if (ddl_userStatus.SelectedValue == "3")
                {
                    strcmd += " and userStatus = '" + ddl_userStatus.SelectedItem.Text + "' ";
                    file_name += "userStatus=" + ddl_userStatus.SelectedItem.Text + "&";
                }
            }

            strcmd += " ) AS A on A.uId = s.uId LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId " +
             "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
             "LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId  " +
             "LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 where cdfApproved='APPROVED' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

            //if text box txt_name is not empty then like operator will be find data with avlible text name
            if (txt_name.Text != "")
            {
                strcmd += " AND (fname like '%" + txt_name.Text.Trim() + "%'  or lname like '%" + txt_name.Text.Trim() + "%' or email like '%" + txt_name.Text.Trim() + "%' or dheyaEmail like '%" + txt_name.Text.Trim() + "%' or contactNo like '%" + txt_name.Text.Trim() + "%') ";
            }

            //if dropdown ddl_testCompStatus is not empty then like operator will be find data with available test Complete Status
            if (ddl_cdfLevel.SelectedValue != "--Select--")
            {
                strcmd += " AND cdfLevel like '%" + ddl_cdfLevel.SelectedValue + "%' ";
                file_name += "cdfLevel=" + ddl_cdfLevel.SelectedValue + "&";
            }

            //if text box txt_city is not empty then like operator will be find data with available text city
            if (ddl_city.SelectedIndex > 0)
            {
                strcmd += " AND A.cityid= " + ddl_city.SelectedValue;
                file_name += "city=" + ddl_city.SelectedItem.Text + "&";
            }

            //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
            if (ddl_ename.SelectedIndex > 0)
            {
                strcmd += " AND R.executiveId =" + ddl_ename.SelectedValue;
                file_name += "executive=" + ddl_ename.SelectedItem.Text + "&";
            }

            //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
            if (ddl_refundStatus.SelectedValue != "Select")
            {
                strcmd += " AND B.refundStatus ='" + ddl_refundStatus.SelectedValue + "'";
                file_name += "refundStatus=" + ddl_refundStatus.SelectedItem.Text + "&";
            }

            //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
            if (ddl_rating.SelectedValue != "Select")
            {
                strcmd += " AND cdfrating ='" + ddl_rating.SelectedValue + "'";
                file_name += "cdfrating=" + ddl_rating.SelectedItem.Text + "&";
            }

            if (ddl_batch.SelectedIndex > 0)
            {
                strcmd += " AND B.batchId =" + ddl_batch.SelectedValue;
                file_name += "batchId=" + ddl_batch.SelectedItem.Text + "&";
            }

            if (ddlShadowSession.SelectedValue != "Select")
            {
                if (ddlShadowSession.SelectedValue == "0")
                {
                    strcmd += " AND (B.shadowSession ='" + ddlShadowSession.SelectedValue + "' OR B.shadowSession is Null) ";
                    file_name += "shadowSession=" + ddlShadowSession.SelectedItem.Text + "&";
                }
                else
                {
                    strcmd += " AND B.shadowSession ='" + ddlShadowSession.SelectedValue + "'";
                    file_name += "shadowSession=" + ddlShadowSession.SelectedItem.Text + "&";
                }
            }

            if (ddlChildTestStatus.SelectedValue != "Select")
            {
                if (ddlChildTestStatus.SelectedValue == "0")
                {
                    strcmd += " AND (B.childTestStatus ='" + ddlChildTestStatus.SelectedValue + "' OR B.childTestStatus is Null) ";
                    file_name += "childTestStatus=" + ddlChildTestStatus.SelectedItem.Text + "&";
                }
                else
                {
                    strcmd += " AND B.childTestStatus ='" + ddlChildTestStatus.SelectedValue + "'";
                    file_name += "childTestStatus=" + ddlChildTestStatus.SelectedItem.Text + "&";
                }
            }

            if (ddlChildSessionStatus.SelectedValue != "Select")
            {
                if (ddlChildSessionStatus.SelectedValue == "0")
                {
                    strcmd += " AND (B.childSessionStatus ='" + ddlChildSessionStatus.SelectedValue + "' OR B.childSessionStatus is Null) ";
                    file_name += "childSessionStatus=" + ddlChildSessionStatus.SelectedItem.Text + "&";
                }
                else
                {
                    strcmd += " AND B.childSessionStatus ='" + ddlChildSessionStatus.SelectedValue + "'";
                    file_name += "childSessionStatus=" + ddlChildSessionStatus.SelectedItem.Text + "&";
                }
            }

            if (ddlSpouseTestStatus.SelectedValue != "Select")
            {
                if (ddlSpouseTestStatus.SelectedValue == "0")
                {
                    strcmd += " AND (B.spouseTestStatus ='" + ddlSpouseTestStatus.SelectedValue + "' OR B.spouseTestStatus is Null) ";
                    file_name += "spouseTestStatus=" + ddlSpouseTestStatus.SelectedItem.Text + "&";
                }
                else
                {
                    strcmd += " AND B.spouseTestStatus ='" + ddlSpouseTestStatus.SelectedValue + "'";
                    file_name += "spouseTestStatus=" + ddlSpouseTestStatus.SelectedItem.Text + "&";
                }
            }

            //if both date are not empty then where condition will find data between date 
            if (txt_from.Text != "" && txt_to.Text != "")
            {
                strcmd += " AND (regDateTime BETWEEN '" + dbContext.DateConvert(txt_from.Text) + "' AND '" + dbContext.DateConvert(txt_to.Text) + "')";
                file_name += "regDateTime between=" + dbContext.DateConvert(txt_from.Text) + "";
                file_name += "AND" + dbContext.DateConvert(txt_to.Text) + "&";
            }

            if (txt_location.Text != "")
            {
                strcmd += " AND (address like '%" + txt_location.Text.Trim() + "%') ";
                file_name += "address=" + txt_location.Text.Trim() + "&";
            }

            if (txt_DOB.Text != "")
            {
                strcmd += " AND DATEPART(d, dob) = DATEPART(d, '" + dbContext.DateConvert(txt_DOB.Text + "/2018") + "') AND DATEPART(m, dob) = DATEPART(m, '" + dbContext.DateConvert(txt_DOB.Text + "/2018") + "')";
                file_name += "DOB = DATEPART(d, dob) = DATEPART(d, '" + dbContext.DateConvert(txt_DOB.Text + "/2018") + "') AND DATEPART(m, dob) = DATEPART(m, '" + dbContext.DateConvert(txt_DOB.Text + "/2018") + "')&";
            }

            file_name += " Data";
            //data is order by desc
            strcmd += " group by email, dheyaEmail,contactno, A.pincode , B.latitude, B.longitude, A.uId,fname,lname,dob,A.regDateTime,C.name,A.status,userStatus, p.teststatus,s.TotalPayment,B.shadowSession,B.childTestStatus,B.childSessionStatus,B.spouseTestStatus,A.address,B.fieldOfWork,B.modeOfWork,B.industrySector order by A.uId desc ";

            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            row_count = ds.Tables[0].Rows.Count;
            lbl_rowcount.Text = "Total - " + row_count.ToString();

            ViewState["STRING"] = strcmd;
            ViewState["FileName"] = file_name;
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
    private void ExportToExcel_function()
    {
        try
        {
            file_name = ViewState["FileName"].ToString();
            string strcmd = ViewState["STRING"].ToString();
            DataSet ds = dbContext.ExecDataSet(strcmd);
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
                 "attachment;filename="+ file_name + ".xls");
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
    protected void ExportToExcel(object sender, EventArgs e)
    {
        BindGridView_filter();
        //Save_export_data_tracking();
        ExportToExcel_function();
        
    }

    protected void chk_all_click(object sender, EventArgs e)
    {
        if (chk_all.Checked == true)
        {
            chk_fname.Checked = true;
            chk_lname.Checked = true;
            chk_dob.Checked = true;
            chk_contact.Checked = true;
            chk_email.Checked = true;
            chk_regDate.Checked = true;
            chk_city.Checked = true;
            chk_status.Checked = true;
            chk_userStatus.Checked = true;
            chk_testStatus.Checked = true;
            chk_address.Checked = true;
            chk_level.Checked = true;
            chk_Batch.Checked = true;
            chk_fieldOfWork.Checked = true;
            chk_modeOfWork.Checked = true;
            chk_industrySector.Checked = true;
            chk_shadowSession.Checked = true;
            chk_childTestStatus.Checked = true;
            chk_childSessionStatus.Checked = true;
            chk_spouseTestStatus.Checked = true;
        }
        else
        {
            chk_fname.Checked = false;
            chk_lname.Checked = false;
            chk_dob.Checked = false;
            chk_contact.Checked = false;
            chk_email.Checked = false;
            chk_regDate.Checked = false;
            chk_city.Checked = false;
            chk_status.Checked = false;
            chk_userStatus.Checked = false;
            chk_testStatus.Checked = false;
            chk_address.Checked = false;
            chk_level.Checked = false;
            chk_Batch.Checked = false;
            chk_fieldOfWork.Checked = false;
            chk_modeOfWork.Checked = false;
            chk_industrySector.Checked = false;
            chk_shadowSession.Checked = false;
            chk_childTestStatus.Checked = false;
            chk_childSessionStatus.Checked = false;
            chk_spouseTestStatus.Checked = false;
        }
    }

    // bellow code is in the function ExportToExcel_function() , which was removed.

    //DataSet ds = new DataSet();
    //string query = "sp_filter_data";
    //string fromDate = dbContext.DateConvert(txt_from.Text);
    //string toDate = dbContext.DateConvert(txt_to.Text);
    //if (ddl_cdfLevel.SelectedValue != "Select")
    //{
    //    int status = 1;
    //    ds = dbContext.ExecDataSet_byLevel(query, status, ddl_cdfLevel.SelectedValue);
    //    file_name = "Final_CDF_byLevel";
    //}
    //else if (ddl_city.SelectedIndex > 0)
    //{
    //    int status = 2;
    //    ds = dbContext.ExecDataSet_byCity(query, status, ddl_city.SelectedValue);
    //    file_name = "Final_CDF_byCity";
    //}
    //else if (ddl_cdfLevel.SelectedValue != "Select" && ddl_city.SelectedIndex > 0)
    //{
    //    int status = 12;
    //    ds = dbContext.ExecDataSet_byLevelAndCity(query, status, ddl_cdfLevel.SelectedValue, ddl_city.SelectedValue);
    //    file_name = "Final_CDF_byLevelAndCity";
    //}
    //else
    //{
    //    int status = 100;
    //    ds = dbContext.ExecDataSet_All(query, status);
    //    file_name = "Final_CDF_Details";
    //}
}