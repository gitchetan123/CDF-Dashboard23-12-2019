using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;

public partial class executiveVerifycdfRegistration : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["executiveEmail"] != null)
        {
            if (!IsPostBack)
            {
                DataTable dt = BindGridView();
                grid_Pyment.DataSource = dt;
                grid_Pyment.DataBind();
                //BindGridView();
                div_msg.Visible = false;
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string StrQueryExe = "select id,exeName,exeEmail from tblExecutive where status ='ACTIVE' and exeEmail ='" + Session["executiveEmail"].ToString() + "'";
                        // dbContext.BindDropDownlist(StrQueryExe, ref ddl_executiveName);
                        SqlDataAdapter da = new SqlDataAdapter(StrQueryExe, con);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        ddl_executiveName.DataSource = ds;
                        ddl_executiveName.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddl_executiveName.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddl_executiveName.DataBind();
                    }

                    string StrQuery2 = "select Distinct B.id,B.name,A.cityid from tblUserMaster as A  Inner Join tblCitiesMaster as B on A.cityid = B.id order by B.name";
                    dbContext.BindDropDownlist(StrQuery2, ref ddl_city);

                    string StrQueryBatch = " select id,batchName from tblTrainingBatch where date < (SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30')) order by date desc";
                    dbContext.BindDropDownlist(StrQueryBatch, ref ddl_batch);

                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    // if condition fails then user will get following message
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Something wrong on form loading. Please Try again." + ex.Message;
                }
            }
        }
        else
        {
            Response.Redirect("~/Admin/login.aspx", false);
        }
    }

    //private void BindGridView()
    //{
    //    try
    //    {
    //        if (Session["executiveEmail"] != null)
    //        {
    //            //Select details id in tblVerifyRegistration table
    //            string strcmd = "select vr.id,vr.email,createDate,vr.status,e.exeName,ISNULL(u.teststatus, 'Incomplete') as teststatus,um.status as TestApproval,ISNULL(SUM(p.amount), 0) as TotalPayment from tblVerifyRegistration as vr left outer join tblExecutive as e on e.id = vr.executiveId Left Outer join tblUserMaster as um on vr.email = um.email  Left Outer Join tblPayment as p on um.uId = p.uId Left Outer join tblUserProductMaster as u on um.uId = u.uId and u.prodid = 7 where exeEmail = '" + Session["executiveEmail"].ToString() + "' group by p.amount,vr.id,vr.email,createDate,vr.status,e.exeName,u.teststatus,um.status,um.uId order by vr.id desc";
    //            //create a dataset object and fill it 
    //            DataSet ds = dbContext.ExecDataSet(strcmd);
    //            grid_Pyment.DataSource = ds;
    //            grid_Pyment.DataBind();
    //        }
    //        else
    //        {
    //            Response.Redirect("~/Admin/login.aspx", false);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error("" + ex);
    //        div_msg.Visible = true;
    //        div_msg.Attributes["class"] = "alert alert-danger";
    //        div_msg.InnerHtml = "Something went wrong. Please try again......";
    //    }
    //}


    protected void btn_payment_Click(object sender, EventArgs e)
    {
        if (Session["executiveName"] != null)
        {
            if (IsValid)
            {

                string s = txt_email.Text;
                if (s.Contains("@dheya"))
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Dheya email id is not allowed";
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                    {
                        connection.Open();
                        int count = 0;
                        try
                        {
                            string str = "select count(uid) from tblUserMaster where email='" + txt_email.Text.Trim() + "'";
                            SqlCommand cmd = new SqlCommand(str, connection);
                            int countuid = Convert.ToInt32(cmd.ExecuteScalar());
                            if (countuid == 0)
                            {

                                str = "select count(id) from tblVerifyRegistration where email='" + txt_email.Text.Trim() + "'";
                                cmd = new SqlCommand(str, connection);
                                int countuser = Convert.ToInt32(cmd.ExecuteScalar());
                                if (countuser == 0)
                                {
                                    string strcmd1 = "insert into tblVerifyRegistration (email,executiveId,createDate,status,userType)  values(@email, @executiveId, @createDate, @status,2)";
                                    cmd = new SqlCommand(strcmd1, connection);
                                    cmd.Parameters.AddWithValue("@email", txt_email.Text.Trim());
                                    cmd.Parameters.AddWithValue("@executiveId", ddl_executiveName.Text);
                                    cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@status", "ACTIVE");
                                    count = cmd.ExecuteNonQuery();
                                    if (count > 0)
                                    {
                                        // Send SMS
                                        string SMSText = ConfigurationManager.AppSettings["CDFEmailVerificationSMS"].ToString();
                                        datacontext.sendSms(txt_contact.Text.Trim().ToString(), SMSText);
                                       
                                        // Send Email
                                        string body = this.PopulateBody(txt_email.Text.Trim());
                                        var task = new Thread(() => datacontext.SendEmail1(txt_email.Text, ConfigurationManager.AppSettings["CDFEmailVerificationSubject"], body));
                                        task.Start();

                                        BindGridView();
                                        div_msg.Visible = true;
                                        div_msg.Attributes["class"] = "alert alert-success";
                                        div_msg.InnerHtml = "User created successfully";
                                    }
                                }
                                else
                                {
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-danger";
                                    div_msg.InnerHtml = "Email id already exists.";
                                }
                            }
                            else

                            {
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerHtml = "Email id already exists in Database";
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex);
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerHtml = "Something went wrong. Please try again......";
                        }
                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/Admin/Login.aspx", false);
        }
    }

    protected void grid_Pyment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() != "Page")
        {
            string id = e.CommandArgument.ToString();
            //if (IsValid)
            //{
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                connection.Open();
                int count = 0;
                try
                {
                    string strcmd1 = "update tblVerifyRegistration set status ='DEACTIVE' where id=@id";
                    SqlCommand cmd = new SqlCommand(strcmd1, connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        BindGridView();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            //}
        }
    }

    private string PopulateBody(string email)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CDFEmailVerificationTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Email}", email);

            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grid_Pyment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Pyment.PageIndex = e.NewPageIndex;
        DataTable dt = BindGridView();
        grid_Pyment.DataSource = dt;
        grid_Pyment.DataBind();
    }
    protected void btn_advance_preview_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                grid_Pyment.DataSource = BindGridView();
                grid_Pyment.DataBind();
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
        ddl_city.SelectedIndex = 0;
        ddl_batch.SelectedIndex = 0;
        ddl_cdfAproveStatus.SelectedIndex = 0;
        ddl_cdfLevel.SelectedIndex = 0;
        ddl_testApproveStatus.SelectedIndex = 0;
        ddl_testCompStatus.SelectedIndex = 0;
        txt_from.Text = "";
        txt_to.Text = "";
        txt_name.Text = "";
    }

    private DataTable BindGridView()
    {
        try
        {
            string strcmd = "SELECT A.uId as id,fname,lname,C.name as city,A.status as TestApproval,ISNULL(pm.teststatus, 'Incomplete') AS Teststatus,vr.email as email, " +
            "s.TotalPayment as TotalPayment,vr.email,vr.status,ex.exeName as exeName,vr.createDate as createDate FROM tblExecutive as ex " +
            "LEFT OUTER JOIN tblVerifyRegistration as vr on ex.id = vr.executiveId " +
            "Left outer join(select u.uId, u.email, ISNULL(SUM(amount), 0) as TotalPayment from tblUserMaster as u " +
            "Left Outer Join tblPayment as p on u.uId = p.uId group by u.uId, u.userTypeId, u.email having u.userTypeId= '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s on vr.email = s.email " +
            "Left Outer Join tblUserMaster AS A on s.uId = A.uId " +
            "LEFT OUTER JOIN tblUserProductMaster AS pm ON A.uId = pm.uId and pm.prodid = 7 " +
            "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
            "where ex.exeEmail= '" + Session["executiveEmail"].ToString() + "'";

            //string strcmd = "SELECT A.uId as id,fname,lname,dob,A.regDateTime,C.name as city,A.status as TestApproval,ISNULL(p.teststatus, 'Incomplete') AS Teststatus,A.email, s.TotalPayment as TotalPayment,vr.email,vr.status,ex.exeName as exeName,ISNULL(p.teststatus, 'Incomplete') as teststatus,vr.createDate as createDate " +
            //"FROM(select u.uId, ISNULL(SUM(amount), 0) as TotalPayment from tblPayment as p " +
            //"Right Outer Join tblUserMaster as u on p.uId = u.uId group by u.uId, u.userTypeId having u.userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s " +
            //"Left Outer Join(select * from tblUserMaster where userTypeId= '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS A on A.uId = s.uId " +
            //"LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId " +
            //"LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
            //"LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId " +
            //"LEFT OUTER JOIN tblVerifyRegistration as vr on A.email = vr.email " +
            //"LEFT Outer join tblExecutive as ex on vr.executiveId = ex.id " +
            //"LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p .prodid = 7 where userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' and exeEmail = '" + Session["executiveEmail"].ToString() + "' ";

            //string strcmd = "SELECT A.uId as id,fname,lname,dob,A.regDateTime,C.name as city,a.status,userStatus, ISNULL(p.teststatus, 'Incomplete') AS Teststatus, s.TotalPayment " +
            // "FROM(select u.uId, ISNULL(SUM(amount),0) as TotalPayment from tblPayment as p " +
            // "Right Outer Join tblUserMaster as u on p.uId = u.uId " +
            // "group by u.uId,u.userTypeId having u.userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s " +
            // "Left Outer Join(select * from tblUserMaster where userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS A on A.uId = s.uId " +
            // "LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId " +
            // "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
            // "LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId  " +
            // "LEFT OUTER JOIN tblVerifyRegistration as vr on R.executiveId = vr.executiveId " +
            // "LEFT Outer join tblExecutive as ex on ex.id = vr.executiveId " +
            // "LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId where userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

            //if text box txt_name is not empty then like operator will be find data with avlible text name
            if (txt_name.Text != "")
            {
                strcmd += " AND (fname like '%" + txt_name.Text.Trim() + "%'  or lname like '%" + txt_name.Text.Trim() + "%' or vr.email like '%" + txt_name.Text.Trim() + "%' or A.dheyaEmail like '%" + txt_name.Text.Trim() + "%' or contactNo like '%" + txt_name.Text.Trim() + "%') ";
            }

            //if dropdown ddl_testStatus is not empty then like operator will be find data with available test approval Status
            if (ddl_testApproveStatus.SelectedValue != "Select")
            {
                if (ddl_testApproveStatus.SelectedValue == "APPROVED")
                    strcmd += " AND A.status ='" + ddl_testApproveStatus.SelectedValue + "'";
                else
                    strcmd += " AND (A.status <> 'APPROVED' or A.status IS NULL)";
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
                strcmd += " AND A.cdfLevel like '%" + ddl_cdfLevel.SelectedValue + "%' ";
            }

            //if text box txt_city is not empty then like operator will be find data with available text city
            if (ddl_city.SelectedIndex > 0)
            {
                strcmd += " AND A.cityid= " + ddl_city.SelectedValue;
            }

            //if dropdown ddl_ename is not empty then like operator will be find data with available Executive names
            //if (ddl_ename.SelectedIndex > 0)
            //{
            //    strcmd += " AND R.executiveId =" + ddl_ename.SelectedValue;
            //}

            if (ddl_batch.SelectedIndex > 0)
            {
                strcmd += " AND B.batchId =" + ddl_batch.SelectedValue;
            }

            //if dropdown ddl_cdfAproveStatus is not empty then like operator will be find data with available Executive names
            if (ddl_cdfAproveStatus.SelectedValue != "Select")
            {
                if (ddl_cdfAproveStatus.SelectedValue == "APPROVED")
                    strcmd += " AND A.cdfApproved ='" + ddl_cdfAproveStatus.SelectedValue + "'";
                else
                    strcmd += " AND (A.cdfApproved <> 'APPROVED' or A.cdfApproved IS NULL)";
            }

            //if both date are not empty then where condition will find data between date 
            if (txt_from.Text != "" && txt_to.Text != "")
            {
                strcmd += " AND (A.regDateTime BETWEEN '" + dbContext.DateConvert(txt_from.Text) + "' AND '" + dbContext.DateConvert(txt_to.Text) + "')";
            }

            //data is order by desc
            strcmd += " order by vr.id desc ";

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



    //private DataTable BindGridView(string strcmd)
    //{
    //    try
    //    {
    //        //create a dataset object and fill it 
    //        DataSet ds = dbContext.ExecDataSet(strcmd);
    //        int row_count = ds.Tables[0].Rows.Count;
    //        lbl_rowcount.Text = "Total - " + row_count.ToString();
    //        return ds.Tables[0];
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error("" + ex);
    //        lbl_msg.Visible = true;
    //        lbl_msg.Text = "Something went wrong. Please try again......";
    //        return null;
    //    }
    //}
}