using log4net;
using System;
using System.Configuration;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using System.Threading;

public partial class Sale_sell : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();
    DataTable dt;
    //int amount = 12250;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] != null)
        {
            if (!IsPostBack)
            {
                BindGridView1();
            }
            //rbl0.Checked = true;
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridView1();

        }
        catch (Exception ex)
        {

            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Attributes["class"] = "alert alert-danger";
            lbl_msg.Text = "Something went wrong. Please try again......";
        }
    }

    public void BindGridView1()
    {
        try
        {
            //string strcmd = "select UM.userTypeId, UM.uId, UM.fname + ISNULL(' ' + UM.lname, ' ') as Name , UM.contactNo, UM.email,RD.Program, UM.regDateTime,UM.userStatus, case when UM.userStatus='ACTIVE' then 'paid' else 'not paid' end as PaymentStatus, RD.Amount, RD.ProductId from tblUserMaster UM "
            //+ " left outer join tblReferralDetail RD "
            //+ " on UM.uId = RD.uId where RD.IsCase=1 and RD.ReferByEmail='"+ Session["email"].ToString()+ "' order by UM.uId DESC";
            string strcmd = "select UM.userTypeId, UM.uId, RD.FirstName as Name , "
                            + " RD.Contact, RD.Email,RD.Program, RD.CreatedDate,UM.userStatus, "
                            + " case when UM.userStatus = 'ACTIVE' then 'paid' else 'not paid' end as PaymentStatus, RD.Amount, RD.ProductId "
                            + " from tblUserMaster UM  full outer join tblReferralDetail RD on UM.uId = RD.uId "
                            + " where RD.IsCase = 1 and RD.ReferByEmail = '" + Session["email"].ToString() + "' ";
            if (txt_search.Text != "")
            {
                strcmd += " and RD.FirstName like '%" + txt_search.Text + "%' or RD.Contact like '%" + txt_search.Text + "%' or RD.Email like '%" + txt_search.Text + "%' ";
            }
            strcmd += " order by UM.uId DESC";
            //create a dataset object and fill it 
            ViewState["Grid"] = strcmd;
            DataSet ds = dbContext.ExecDataSet(strcmd);
            grid_Pyment.DataSource = ds;
            grid_Pyment.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            //div_msg.Visible = true;
            //div_msg.Attributes["class"] = "alert alert-danger";
            //div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }

    protected void btn_payment_Click(object sender, EventArgs e)
    {
        if (Session["email"] != null)
        {
            if (IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    connection.Open();
                    int count = 0;
                    int ProductId = Convert.ToInt32(ViewState["ProductId"]);
                    int uId = Convert.ToInt32(ViewState["ID"]);
                    SqlTransaction transaction = null;
                    try
                    {
                        //if (rblfull.Checked == false && rbl5.Checked == false && rbl10.Checked == false && rbl15.Checked == false && rbl20.Checked == false)
                        //{
                        //    txt_amount.Text = Session["Amount"].ToString();
                        //}
                        
                        string str = "select count(id) from tblCustomPayment_New where uid='" + Convert.ToInt32(ViewState["ID"]) + "' and status='ACTIVE'";
                        SqlCommand cmd1 = new SqlCommand(str, connection);
                        int countPayment = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (countPayment == 0)
                        {
                            //string str = "select count(id) from tblCustomPayment where uid='" + uId + "' and status='ACTIVE'";
                            //SqlCommand cmd1 = new SqlCommand(str, connection);
                            //int countPayment = Convert.ToInt32(cmd.ExecuteScalar());

                            //transaction = connection.BeginTransaction();
                            //string strcmd1 = "insert into tblCustomPayment (uid ,amount,status ,createdDate,createdBy,approve) values (@uid ,@amount,'ACTIVE',@createdDate,@createdBy,@approve)";

                            string strcmd1 = "insert into tblCustomPayment_New (uid,userTypeId,email, CDFEmail, Product_Id, Amount, Created_By, Created_Date, Status, Approve, Discount) values (@uid,@userTypeId,@email, @CDFEmail, @Product_Id, @Amount, @Created_By, @Created_Date, 'ACTIVE', @Approve, @Discount)";
                            SqlCommand cmd = new SqlCommand(strcmd1, connection);
                            cmd.Parameters.AddWithValue("@uid", uId);
                            cmd.Parameters.AddWithValue("@userTypeId", Convert.ToInt32(ViewState["UserType"]));
                            cmd.Parameters.AddWithValue("@email", ViewState["CandidateEmail"].ToString());
                            cmd.Parameters.AddWithValue("@CDFEmail", Session["email"].ToString());
                            cmd.Parameters.AddWithValue("@Product_Id", ProductId);
                            cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(txt_amount.Text));
                            cmd.Parameters.AddWithValue("@Created_By", Session["email"].ToString());
                            cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Approve", 1);
                            cmd.Parameters.AddWithValue("@Discount", Convert.ToInt32(ViewState["discount"]));
                            count = cmd.ExecuteNonQuery();

                            //cmd.Transaction = transaction;
                            //connection.Close();
                            if (count > 0)
                            {
                                string strdata = "select fname,contactNo, email FROM tblUserMaster where uId = '" + uId + "'";
                                //string strdata = "select * from  tblReferralDetail where referral_id = '"+ referral_id + "'";
                                cmd = new SqlCommand(strdata, connection);
                                SqlDataReader dr = cmd.ExecuteReader();
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    //call to Templete file for email body
                                    string StudentRegistrationPagePath = "";
                                    //string StudentRegistrationPagePathSMS = "";
                                    //if (ProductId==7)
                                    //{
                                    //    StudentRegistrationPagePath = "https://www.dheya.com/cdf-dashboard/login.aspx";
                                    //    StudentRegistrationPagePathSMS = "https://www.dheya.com/cdf-dashboard/login.aspx";
                                    //}
                                    if (ProductId == 1 || ProductId == 2 || ProductId == 3 || ProductId == 8)
                                    {
                                        StudentRegistrationPagePath = "https://www.dheya.com/cpa/new-user/registration.aspx?uid=" + uId;
                                        //StudentRegistrationPagePathSMS = "https://www.dheya.com/cpa/new-user/registration.aspx";
                                    }
                                    if (ProductId == 9 || ProductId == 10 || ProductId == 11)
                                    {
                                        StudentRegistrationPagePath = "https://www.dheya.com/cpa/CPA/Pre/cpa-registration.aspx?uid=" + uId;
                                        //StudentRegistrationPagePathSMS = "https://www.dheya.com/CPA/Pre/cpa-registration.aspx?uid=" + uId;
                                    }
                                    if (ProductId == 12 || ProductId == 13)
                                    {
                                        StudentRegistrationPagePath = "https://www.dheya.com/cpa/new-user/registration-career.aspx?uid=" + uId;
                                        //StudentRegistrationPagePathSMS = "https://www.dheya.com/cpa/new-user/registration-career.aspx?uid=" + uId;
                                    }
                                    string body = this.PopulateBody(dr["fname"].ToString(), StudentRegistrationPagePath);

                                    //Send email
                                    var task = new Thread(() => datacontext.SendEmail1(dr["email"].ToString(), ConfigurationManager.AppSettings["StudentCustomPaymentEmailTemplateSubject"], body));
                                    task.Start();

                                    //string SMSText1 = ConfigurationManager.AppSettings["StudentCustomPaymentSMS"].ToString();
                                    //SMSText1 = SMSText1.Replace("{Name}", "" + dr["fname"].ToString());
                                    //string SMSText = SMSText1.Replace("{uId}", "" + uId);
                                    //fw.sendSms(dr["contactNo"].ToString(), SMSText);

                                }
                                //BindGridView();
                                BindGridView1();
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-success";
                                div_msg.InnerHtml = "Custom payment created successfully for the user, And further details sent to   " + dr["fname"].ToString() + " via email.";
                            }
                        }
                        else
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerHtml = "Custom payment for this user currently activated, so you can not create another one";
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
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void grid_Pyment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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
        string get_user_by_id = "select UM.userTypeId, UM.uId, RD.FirstName as Name ,  RD.Contact, RD.Email, "
                                + " RD.Program, RD.CreatedDate,UM.userStatus,  case when UM.userStatus = 'ACTIVE' then 'paid' else 'not paid' end as PaymentStatus,  "
                                + " RD.Amount, RD.ProductId from tblUserMaster UM  full "
                                + " outer join tblReferralDetail RD on UM.uId = RD.uId  where RD.IsCase = 1 and "
                                + " RD.ReferByEmail = 'dhananjay.korde@gmail.com' and UM.uId = '" + id + "' "
                                + " order by UM.uId DESC";
        SqlDataReader dr = dbContext.ExecDataReader12(get_user_by_id, id);
        if (dr.HasRows)
        {
            dr.Read();
            txt_amount.Text = dr.GetValue(9).ToString();
            Session["Amount"] = txt_amount.Text;
            FullFees.Value = txt_amount.Text;
            ViewState["UserType"] = dr.GetValue(0).ToString();
            ViewState["CandidateEmail"] = dr.GetValue(4).ToString();
            ViewState["ProductId"] = dr.GetValue(10).ToString();
        }
        dr.Close();
        dr.Dispose();
    }

    private string PopulateBody(string userName, string StudentRegistrationPagePath)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["StudentCustomPaymentEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{url}", StudentRegistrationPagePath);

            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grid_Pyment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGridView1();
        //grid_Pyment.PageIndex = e.NewPageIndex;
        //DataTable dt = BindGridView();
        //grid_Pyment.DataSource = dt;
        //grid_Pyment.DataBind();
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

    private DataTable BindGridView()
    {
        try
        {
            string email = Session["executiveEmail"].ToString();

            string strcmd = "SELECT A.uId as id,fname,lname,C.name as city,A.status as TestApproval,ISNULL(pm.teststatus, 'Incomplete') AS Teststatus,vr.email as email, " +
            "s.TotalPayment as TotalPayment,vr.email,vr.status,ex.exeName as exeName,vr.createDate as createDate FROM tblExecutive as ex " +
            "LEFT OUTER JOIN tblVerifyRegistration as vr on ex.id = vr.executiveId " +
            "Left outer join(select u.uId, u.email, ISNULL(SUM(amount), 0) as TotalPayment from tblUserMaster as u " +
            "Left Outer Join tblPayment as p on u.uId = p.uId group by u.uId, u.userTypeId, u.email having u.userTypeId= '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s on vr.email = s.email " +
            "Left Outer Join tblUserMaster AS A on s.uId = A.uId " +
            "LEFT OUTER JOIN tblUserProductMaster AS pm ON A.uId = pm.uId and pm.prodid = 7 " +
            "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
            "where ex.exeEmail= '" + Session["executiveEmail"].ToString() + "'";

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

    protected void rblfull_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 0;
        int per_amount = Convert.ToInt32(FullFees.Value);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl5_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 5;
        int per_amount = (Convert.ToInt32(Session["Amount"]) * 95) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl10_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 10;
        int per_amount = (Convert.ToInt32(Session["Amount"]) * 90) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl15_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 15;
        int per_amount = (Convert.ToInt32(Session["Amount"]) * 85) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl20_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 20;
        int per_amount = (Convert.ToInt32(Session["Amount"]) * 80) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
   
}