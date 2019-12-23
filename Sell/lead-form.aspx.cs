using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Text;
using log4net;

public partial class Sell_lead_form : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    db_context dbContext = new db_context();
    int j = 0;
    int i = 0;
    string uId = null, userSource = null;
    SqlCommand cmd = null;
    data_context datacontext = new data_context();
    string referedBy=null;
    int lead_source = 6;
    int isLead = 0, IsContact = 0, IsCase = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["email"] != null)
        {
            referedBy = Session["email"].ToString();
            if (!IsPostBack)
            {
                GridView1.DataSource = BindGridView();
                GridView1.DataBind();
            }
        }
        else
        {
            Response.Redirect("login.aspx", false);
        }

    }

    #region Submit Form Lead
    protected void btn_submit_lead_Click(object sender, EventArgs e)
    {
        push_to_crm();
    }
    #endregion Submit Form Lead

    #region Submit Form Payment
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.txt_firstname.Text = "";
        this.txt_firstname.Text = "";
        this.txt_email.Text = "";
        this.txt_contact.Text = "";
        this.txt_city.Text = "";
        //this.txt_pin.Text = "";

        this.ddl_product.SelectedValue = "Select an Option";
        this.ddl_leadcategory.SelectedValue = "Select an Option";
    }
    protected void btn_payment_Click(object sender, EventArgs e)
    {
        submit_form();
        send_pay_link();
        push_to_crm();
    }
    private void submit_form()
    {
        try
        {
            Session["Product"] = ddl_product.SelectedItem.Text;

            IsCase = 1;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd2 = null;
                con.Open();
                if (ddl_leadcategory.SelectedValue == "5" || ddl_leadcategory.SelectedValue == "4" || ddl_leadcategory.SelectedValue == "6")
                {
                    cmd2 = new SqlCommand("select count(uId) from tblUserMaster where userTypeId in (4,5,6) and email='" + txt_email.Text + "'", con);
                }
                if (ddl_leadcategory.SelectedValue == "2")
                {
                    cmd2 = new SqlCommand("select count(uId) from tblUserMaster where  userTypeId=2 and email='" + txt_email.Text + "'", con);
                }
                if (ddl_leadcategory.SelectedValue == "12")
                {
                    cmd2 = new SqlCommand("select count(uId) from tblUserMaster where  userTypeId=12 and email='" + txt_email.Text + "'", con);
                }

                cmd2.CommandType = System.Data.CommandType.Text;
                int c = Convert.ToInt32(cmd2.ExecuteScalar());
                if (c == 0)
                {
                    SqlCommand cmd1 = new SqlCommand("dsp_Insert_PreUser_tblUserMaster", con);
                    cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@userTypeId", Convert.ToInt32(ddl_leadcategory.SelectedValue));
                    cmd1.Parameters.AddWithValue("@fname", txt_firstname.Text);
                    cmd1.Parameters.AddWithValue("@email", txt_email.Text);
                    cmd1.Parameters.AddWithValue("@contactNo", txt_contact.Text);
                    cmd1.Parameters.AddWithValue("@userStatus", "INACTIVE");
                    if (ddl_leadcategory.SelectedValue == "4" || ddl_leadcategory.SelectedValue == "5")
                    { userSource = "DHEYA-DASHBOARD"; }
                    if (ddl_leadcategory.SelectedValue == "6")
                    { userSource = "DHEYA-Professional"; }
                    if (ddl_leadcategory.SelectedValue == "12")
                    { userSource = "Business Partnet-Career Product Advisor"; }
                    cmd1.Parameters.AddWithValue("@userSource", userSource);
                    // Get Max Id (last created uId)
                    cmd1.Parameters.Add("@id", SqlDbType.VarChar, 50);
                    cmd1.Parameters["@id"].Direction = ParameterDirection.Output;

                    SqlParameter parm = new SqlParameter("@id", SqlDbType.Int);
                    parm.Direction = ParameterDirection.ReturnValue;
                    cmd1.Parameters.Add(parm);


                    int j = cmd1.ExecuteNonQuery();
                    if (j > 0)
                    {
                        int id = Convert.ToInt32(parm.Value);
                        uId = cmd1.Parameters["@id"].Value.ToString();
                    }
                    else
                    {
                        Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
                    }

                    SqlCommand cmd = new SqlCommand("dsp_insert_referraldetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", txt_firstname.Text);
                    cmd.Parameters.AddWithValue("@Email", txt_email.Text);
                    cmd.Parameters.AddWithValue("@City", txt_city.Text);
                    cmd.Parameters.AddWithValue("@Contact", txt_contact.Text);
                    cmd.Parameters.AddWithValue("@Program", ddl_product.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@IsLead", isLead);
                    cmd.Parameters.AddWithValue("@IsContact", IsContact);
                    cmd.Parameters.AddWithValue("@IsCase", IsCase);
                    cmd.Parameters.AddWithValue("@PinCode", "");
                    cmd.Parameters.AddWithValue("@LeadCategory", ddl_leadcategory.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@LastName", txt_lastname.Text);
                    cmd.Parameters.AddWithValue("@ReferByEmail", Session["email"].ToString());
                    cmd.Parameters.AddWithValue("@referAs", ddl_leadcategory.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Status", "");
                    cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(Session["product_amount"]));
                    cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(ddl_product.SelectedValue));
                    cmd.Parameters.AddWithValue("@uId", Convert.ToInt32(uId));
                    cmd.Parameters.AddWithValue("@Discription", "");
                    i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        string str2 = "INSERT INTO tblRelation (uId,referral_uId) VALUES ('" + Convert.ToInt32(uId) + "','" + Convert.ToInt32(Session["uid"]) + "')";
                        cmd = new SqlCommand(str2, con);
                        int m = cmd.ExecuteNonQuery();

                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerHtml = "Sell registered successfully.";
                    }
                    else
                    {
                        Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
                    }

                    GridView1.DataSource = BindGridView();
                    GridView1.DataBind();
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerHtml = "email already exist";

                }
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
        }
    }
    private void send_pay_link()
    {
        if (Session["email"] != null)
        {
            if (IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    connection.Open();
                    int count = 0;
                    int ProductId = Convert.ToInt32(ddl_product.SelectedValue);
                    //int uId = Convert.ToInt32(ViewState["ID"]);
                    SqlTransaction transaction = null;
                    try
                    {
                        
                        string str = "select count(id) from tblCustomPayment_New where uid='" + Convert.ToInt32(uId) + "' and status='ACTIVE'";
                        SqlCommand cmd1 = new SqlCommand(str, connection);
                        int countPayment = Convert.ToInt32(cmd1.ExecuteScalar());
                        if (countPayment == 0)
                        {
                            string strcmd1 = "insert into tblCustomPayment_New (uid,userTypeId,email, CDFEmail, Product_Id, Amount, Created_By, Created_Date, Status, Approve, Discount) values (@uid,@userTypeId,@email, @CDFEmail, @Product_Id, @Amount, @Created_By, @Created_Date, 'ACTIVE', @Approve, @Discount)";
                            SqlCommand cmd = new SqlCommand(strcmd1, connection);
                            cmd.Parameters.AddWithValue("@uid", uId);
                            cmd.Parameters.AddWithValue("@userTypeId", Convert.ToInt32(ddl_leadcategory.SelectedValue));
                            cmd.Parameters.AddWithValue("@email", txt_email.Text);
                            cmd.Parameters.AddWithValue("@CDFEmail", Session["email"].ToString());
                            cmd.Parameters.AddWithValue("@Product_Id", ProductId);
                            cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(Session["product_amount"]));
                            cmd.Parameters.AddWithValue("@Created_By", Session["email"].ToString());
                            cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Approve", 1);
                            cmd.Parameters.AddWithValue("@Discount", Convert.ToInt32(ViewState["discount"]));
                            count = cmd.ExecuteNonQuery();

                            if (count > 0)
                            {
                                string strdata = "select fname,contactNo, email FROM tblUserMaster where uId = '" + uId + "'";
                                cmd = new SqlCommand(strdata, connection);
                                SqlDataReader dr = cmd.ExecuteReader();
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    //call to Templete file for email body
                                    string StudentRegistrationPagePath = "";

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
                                    string body = this.PopulateBody(txt_firstname.Text, StudentRegistrationPagePath);

                                    //Send email
                                    var task = new Thread(() => datacontext.SendEmail1(txt_email.Text, ConfigurationManager.AppSettings["StudentCustomPaymentEmailTemplateSubject"], body));
                                    task.Start();

                                    //string SMSText1 = ConfigurationManager.AppSettings["StudentCustomPaymentSMS"].ToString();
                                    //SMSText1 = SMSText1.Replace("{Name}", "" + dr["fname"].ToString());
                                    //string SMSText = SMSText1.Replace("{uId}", "" + uId);
                                    //fw.sendSms(dr["contactNo"].ToString(), SMSText);

                                }
                                //BindGridView1();
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-success";
                                div_msg.InnerHtml = "Custom payment created successfully for the user, And further details sent to   " + dr["fname"].ToString() + " with amount of "+ Convert.ToInt32(Session["product_amount"]) + " via email.";
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
                        //Log.Error(ex);
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
    #endregion Submit Form Payment

    #region Push_To_CRM
    private void push_to_crm()
    {
        var task = new Thread(run);
        task.Start();
    }
    private void run()
    {
        
        //Candidate details for CRM 
        string canddetails = "first_name=" + txt_firstname.Text.Replace("'", "").ToUpper().Trim().ToString() + "&last_name=" + txt_lastname.Text.Replace("'", "").ToUpper().Trim().ToString() + "&email1=" + txt_email.Text.Replace("'", "").Replace("/", "") + "&phone_mobile=" + txt_contact.Text.Trim().ToString()
        + "&city_c=" + txt_city.Text.Replace("'", "").ToUpper().Trim().ToString()
        + "&refered_by="+ referedBy.Replace("'", "").ToLower().Trim().ToString() +"&lead_category_c=6&lead_source="+ lead_source + "&Submit=Submit&campaign_id=cac88316-2e67-04b2-1d81-588ee871ba61&redirect_url="+ ConfigurationManager.AppSettings["ReferralSuccessPath"] + "&assigned_user_id=1&moduleDir=Leads";

        pushtoCRM(ConfigurationManager.AppSettings["CRMDataPushlink"].ToString(), canddetails);
    }
    private void pushtoCRM(string strURL, string strRequest)
    {
        try
        {
            HttpWebResponse objHttpWebResponse = null;
            UTF8Encoding encoding;
            string strResponse = "";

            HttpWebRequest objHttpWebRequest;
            objHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
            objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            objHttpWebRequest.PreAuthenticate = true;
            objHttpWebRequest.Method = "POST";

            //Prepare the request stream
            if (strRequest != null && strRequest != string.Empty)
            {
                encoding = new UTF8Encoding();
                Stream objStream = objHttpWebRequest.GetRequestStream();
                Byte[] Buffer = encoding.GetBytes(strRequest);
                // Post the request
                objStream.Write(Buffer, 0, Buffer.Length);
                objStream.Close();
            }
            objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
            encoding = new UTF8Encoding();
            StreamReader objStreamReader =
                new StreamReader(objHttpWebResponse.GetResponseStream(), encoding);
            strResponse = objStreamReader.ReadToEnd();
            objHttpWebResponse.Close();
            objHttpWebRequest = null;
            // return strResponse;

            Console.WriteLine("Data Pushed successfully.");

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }

    }
    #endregion Push_To_CRM


    #region Grid View
    protected DataTable BindGridView()
    {
        try
        {
            string strcmd = "SELECT [FirstName], [LastName] ,[City],[Contact],[Program],[Email],[PinCode]," +
                "CASE  WHEN IsLead = 1 AND IsCase = 0 AND IsContact = 0 Then 'Lead' WHEN IsLead = 0 AND IsCase = 1 AND IsContact = 0 Then 'Case' " +
                "WHEN IsLead = 0 AND IsCase = 0 AND IsContact = 1 Then 'Contact' END As 'Category'," +
                "[LeadCategory],[ReferByEmail] From [dbo].[tblReferralDetail] where ReferByEmail='" + Session["email"].ToString() + "' ";

            DataSet ds = dbContext.ExecDataSet(strcmd);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = BindGridView();
        GridView1.DataBind();
    }
    #endregion Grid View


    #region Get Detail on Fild value changed
    [WebMethod]
    public static string Get_Product_Amount()
    {
        int amount = 0;
        try
        {
            string prod_Id = System.Web.HttpContext.Current.Session["PID"].ToString();
            string strcon = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();

            using (SqlConnection con = new SqlConnection(strcon))
            {
                SqlCommand cmd = new SqlCommand("Select price from tblUserProductRelation where pId = '" + prod_Id + "'", con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        amount = Convert.ToInt32(dr["price"]);
                        System.Web.HttpContext.Current.Session["price"] = amount;
                    }
                }
                return amount.ToString();
                //return comment.ToString();
            }

        }
        catch (Exception ex)
        {
            return amount.ToString();
            throw ex;
        }
    }
    protected void ddl_product_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddl_product.SelectedItem.Text == "Dheya Discover (5999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                Session["PID"] = ddl_product.SelectedValue;
            }
        }
        if (ddl_product.SelectedItem.Text == "Dheya Compass(9999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Dheya Nevigator (19999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();

            }
        }
        if (ddl_product.SelectedItem.Text == "Parent Workshop")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Early Career (15000) - [suitable for age 25-30]")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Mid Career (20000) - [suitable for age above 35]")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "CDF Training (25000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "CPA (15000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Career Center (25000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Career Cafe (300000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
            }
        }
    }
    protected void ddl_leadcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_leadcategory.SelectedValue == "5")
        {
            string StrQueryExe = "select * from tblUserProductRelation where Student=1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "4")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Parent = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "6")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Professional = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "12")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Business = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
        }
        if (ddl_leadcategory.SelectedValue == "School")
        {
            Response.Redirect("referral1.aspx", false);
        }
        if (ddl_leadcategory.SelectedValue == "College")
        {
            Response.Redirect("referral1.aspx", false);
        }
        if (ddl_leadcategory.SelectedValue == "Corporate")
        {
            Response.Redirect("referral1.aspx", false);
        }
    }
    protected void rblfull_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 0;
        int per_amount = (Convert.ToInt32(Session["price"]) * 100) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl5_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 5;
        int per_amount = (Convert.ToInt32(Session["price"]) * 95) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl10_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 10;
        int per_amount = (Convert.ToInt32(Session["price"]) * 90) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl15_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 15;
        int per_amount = (Convert.ToInt32(Session["price"]) * 85) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    protected void rbl20_CheckedChanged(object sender, EventArgs e)
    {
        ViewState["discount"] = 20;
        int per_amount = (Convert.ToInt32(Session["price"]) * 80) / (100);
        txt_amount.Text = per_amount.ToString();
        ViewState["amount"] = txt_amount.Text;
    }
    #endregion Get Detail on Fild value changed
}