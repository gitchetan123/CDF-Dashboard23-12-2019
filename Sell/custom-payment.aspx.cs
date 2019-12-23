using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;

public partial class Sale_custom_payment : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();
    clsForword fw = new clsForword();
    int referral_id;
    int uId;
    int amount;
    int amount_30Perc;
    int ProductId;
    int discount = 0;
    int userTypeId = 0;
    string email = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_msg.Visible = false;
            // Get user in using QueryString 
            if (Request.QueryString["uId"] == "")
            { uId = 0; }
            else
            { uId = Convert.ToInt32(Request.QueryString["uId"]); }
            if (Request.QueryString["userTypeId"] == "")
            { userTypeId = 0; }
            else
            { userTypeId = Convert.ToInt32(Request.QueryString["userTypeId"]); }

            amount = Convert.ToInt32(Request.QueryString["amount"]);
            ViewState["amount"] = amount;
            ProductId = Convert.ToInt32(Request.QueryString["ProductId"]);
            email = Request.QueryString["emailId"].ToString();
        }
        catch (Exception)
        {

        }

        if (Session["email"] != null)
        {
            if (!IsPostBack)
            {
                rblfull.Checked = true;
                txt_amount.Text = amount.ToString();
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }
    private void BindGridView()
    {
        try
        {
            //Select details id in tblUserMaster table
            string strcmd = "SELECT id,uid ,Amount,Status ,case when Approve is null then 'Pending' else 'Approved' end as approve,Created_Date,Modify_Date,Created_By,Updated_By  FROM tblCustomPayment_New where uid=" + uId + " order by id desc";
            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);

            grid_Pyment.DataSource = ds;
            grid_Pyment.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
    }

    protected void rblfull_CheckedChanged(object sender, EventArgs e)
    {
        txt_amount.Text = Request.QueryString["amount"].ToString();
        ViewState["discount"] = 0;
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl5_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 5;
        int per_amount = Convert.ToInt32((((amount) * (5)) / 100).ToString());
        int fin_amount = amount - per_amount;
        txt_amount.Text = fin_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl10_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 10;
        int per_amount = Convert.ToInt32((((amount) * (10)) / 100).ToString());
        int fin_amount = amount - per_amount;
        txt_amount.Text = fin_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl15_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 15;
        int per_amount = Convert.ToInt32((((amount) * (15)) / 100).ToString());
        int fin_amount = amount - per_amount;
        txt_amount.Text = fin_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl20_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 20;
        int per_amount = Convert.ToInt32((((amount) * (20)) / 100).ToString());
        int fin_amount = amount - per_amount;
        txt_amount.Text = fin_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
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
                    SqlTransaction transaction = null;
                    try
                    {
                        string str = "select count(id) from tblCustomPayment_New where uid='" + uId + "' and status='ACTIVE'";
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
                            cmd.Parameters.AddWithValue("@userTypeId", userTypeId);
                            cmd.Parameters.AddWithValue("@email", email);
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
                                BindGridView();
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-success";
                                div_msg.InnerHtml = "Custom payment created successfully";
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
            Response.Redirect("~/Admin/Login.aspx", false);
        }
    }
    protected void grid_Pyment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CustPayment")
        {

            string id = e.CommandArgument.ToString();
            if (IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    connection.Open();
                    int count = 0;
                    try
                    {
                        string strcmd1 = "update tblCustomPayment_New set Status = 'ACTIVE' and approve = '1' where id=@id";
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
            }
        }
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
}