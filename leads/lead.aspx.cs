using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using log4net;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Services;
using System.Diagnostics;
using System.Text;

public partial class leads_lead : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    int j = 0;
    int i = 0;
    string uId = null, userSource = null, email = null, product = null, lead_category = null, assigned_user_id = null;
    SqlCommand cmd = null;
    int isLead = 0, IsContact = 0, IsCase = 0;
    string StrQuery = "";
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Session["email"] != null)
        {
            if (!IsPostBack)
            {
                email = Session["email"].ToString();

                GridView1.DataSource = BindGridView();
                GridView1.DataBind();

                StrQuery = "select id,name from tblStatesMaster ORDER BY name";
                dbContext.BindDropDownlist(StrQuery, ref ddl_state);
            }
        }
        else
        {
            Response.Redirect("login.aspx", false);
        }
    }

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //The city DropDownList contents 
            string StrQuery = "select id, name from tblCitiesMaster where stateId='" + ddl_state.SelectedValue + "' ORDER BY name";
            dbContext.BindDropDownlist(StrQuery, ref ddl_city);

        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            //Call thread method
            var task = new Thread(run);
            task.Start();

            //Session["Product"] = ddl_product.SelectedItem.Text;
            //IsCase = 1;
            //using (SqlConnection con = new SqlConnection(connectionString))
            //{
            //    SqlCommand cmd2 = null;
            //    con.Open();
            //    if (ddl_leadcategory.SelectedValue == "5" || ddl_leadcategory.SelectedValue == "4" || ddl_leadcategory.SelectedValue == "6")
            //    {
            //        cmd2 = new SqlCommand("select count(uId) from tblUserMaster where userTypeId in (4,5,6) and email='" + txt_email.Text + "'", con);
            //    }
            //    if (ddl_leadcategory.SelectedValue == "2")
            //    {
            //        cmd2 = new SqlCommand("select count(uId) from tblUserMaster where  userTypeId=2 and email='" + txt_email.Text + "'", con);
            //    }
            //    if (ddl_leadcategory.SelectedValue == "12")
            //    {
            //        cmd2 = new SqlCommand("select count(uId) from tblUserMaster where  userTypeId=12 and email='" + txt_email.Text + "'", con);
            //    }

            //    cmd2.CommandType = System.Data.CommandType.Text;
            //    int c = Convert.ToInt32(cmd2.ExecuteScalar());
            //    if (c == 0)
            //    {
                    
            //        SqlCommand cmd1 = new SqlCommand("dsp_Insert_PreUser_tblUserMaster", con);
            //        cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd1.Parameters.AddWithValue("@userTypeId", Convert.ToInt32(ddl_leadcategory.SelectedValue));
            //        cmd1.Parameters.AddWithValue("@fname", txt_firstname.Text);
            //        cmd1.Parameters.AddWithValue("@email", txt_email.Text);
            //        cmd1.Parameters.AddWithValue("@contactNo", txt_contact.Text);
            //        cmd1.Parameters.AddWithValue("@userStatus", "INACTIVE");
            //        if (ddl_leadcategory.SelectedValue == "4" || ddl_leadcategory.SelectedValue == "5")
            //        { userSource = "DHEYA-DASHBOARD"; }
            //        if (ddl_leadcategory.SelectedValue == "6")
            //        { userSource = "DHEYA-Professional"; }
            //        if (ddl_leadcategory.SelectedValue == "12")
            //        { userSource = "Business Partnet-Career Product Advisor"; }
            //        cmd1.Parameters.AddWithValue("@userSource", userSource);
            //        // Get Max Id (last created uId)
            //        cmd1.Parameters.Add("@id", SqlDbType.VarChar, 50);
            //        cmd1.Parameters["@id"].Direction = ParameterDirection.Output;

            //        SqlParameter parm = new SqlParameter("@id", SqlDbType.Int);
            //        parm.Direction = ParameterDirection.ReturnValue;
            //        cmd1.Parameters.Add(parm);

            //        //int j = cmd1.ExecuteNonQuery();
            //        if (j > 0)
            //        {
            //            int id = Convert.ToInt32(parm.Value);
            //            uId = cmd1.Parameters["@id"].Value.ToString();
            //        }
            //        else
            //        {
            //            Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
            //        }

            //        SqlCommand cmd = new SqlCommand("dsp_insert_referraldetails", con);
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@FirstName", txt_firstname.Text);
            //        cmd.Parameters.AddWithValue("@Email", txt_email.Text);
            //        cmd.Parameters.AddWithValue("@City", ddl_city.SelectedItem.Text);
            //        cmd.Parameters.AddWithValue("@Contact", txt_contact.Text);
            //        cmd.Parameters.AddWithValue("@Program", ddl_product.SelectedItem.Text);
            //        cmd.Parameters.AddWithValue("@IsLead", isLead);
            //        cmd.Parameters.AddWithValue("@IsContact", IsContact);
            //        cmd.Parameters.AddWithValue("@IsCase", IsCase);
            //        cmd.Parameters.AddWithValue("@PinCode", txt_pin.Text);
            //        cmd.Parameters.AddWithValue("@LeadCategory", ddl_leadcategory.SelectedItem.Text);
            //        cmd.Parameters.AddWithValue("@LastName", txt_lastname.Text);
            //        cmd.Parameters.AddWithValue("@ReferByEmail", Session["email"].ToString());
            //        cmd.Parameters.AddWithValue("@referAs", ddl_leadcategory.SelectedItem.Text);
            //        cmd.Parameters.AddWithValue("@Status", "");
            //        cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(Session["product_amount"]));
            //        cmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(ddl_product.SelectedValue));
            //        cmd.Parameters.AddWithValue("@uId", Convert.ToInt32(uId));
            //        cmd.Parameters.AddWithValue("@Discription", "");
            //        //i = cmd.ExecuteNonQuery();
            //        if (i > 0)
            //        {
            //            string str2 = "INSERT INTO tblRelation (uId,referral_uId) VALUES ('" + Convert.ToInt32(uId) + "','" + Convert.ToInt32(Session["uid"]) + "')";
            //            cmd = new SqlCommand(str2, con);
            //            int m = cmd.ExecuteNonQuery();

            //            div_msg.Visible = true;
            //            div_msg.Attributes["class"] = "alert alert-success";
            //            div_msg.InnerHtml = "Sell registered successfully.";
            //            //Response.Redirect("referral_success.aspx", false);

                       
            //        }
            //        else
            //        {
            //            Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
            //        }

            //        GridView1.DataSource = BindGridView();
            //        GridView1.DataBind();
            //    }
            //    else
            //    {
            //        div_msg.Visible = true;
            //        div_msg.Attributes["class"] = "alert alert-danger";
            //        div_msg.InnerHtml = "email already exist";
            //    }
            //}
           
        }
        catch (Exception ex)
        {
            Response.Write("<script type = 'text/javascript'>alert('Registration failed');</script>");
        }
    }

    //thread method
    private void run()
    {
        try
        {
            string success_url = "~/leads/referral_success.aspx";
            string state = ddl_state.SelectedItem.Text;
            string comment = txt_comment.Text;
            string enq_for_product = ViewState["Product"].ToString();
            string email = Session["email"].ToString();
            string lead_category = ViewState["lead_category"].ToString();
            //Candidate details for CRM  // state, comment , product or enquired for 
            string canddetails = "first_name=" + txt_firstname.Text.Replace("'", "").ToUpper().Trim().ToString() + "&last_name=" + txt_lastname.Text.Replace("'", "").ToUpper().Trim().ToString() + "&email1=" + txt_email.Text.Replace("'", "").Replace("/", "") + "&phone_mobile=" + txt_contact.Text.Trim().ToString() 
            + "&city_c=" + ddl_city.SelectedItem.Text.Trim().ToString() + "&primary_address_postalcode=" + txt_pin.Text.Trim().ToString() + "&school_college_name_c=" + txt_school.Text.Replace("'", "").Replace("/", "").Trim().ToUpper().ToString()
            + "&refered_by=" +email.Replace("'", "").ToLower().Trim().ToString() + "&lead_category_c=" + lead_category.Replace("'", "").Trim().ToString() + "&lead_source=5&Submit=Submit&campaign_id=c644dc77-9354-bef2-8ebc-56d2fccc27ea&redirect_url="+ success_url + "&assigned_user_id=1&moduleDir=Leads";

            //pushtoCRM(ConfigurationManager.AppSettings["CRMDataPushlink"].ToString(), canddetails);

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }
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
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.txt_firstname.Text = "";
        this.txt_firstname.Text = "";
        this.txt_email.Text = "";
        this.txt_contact.Text = "";
        //this.txt_city.Text = "";
        this.ddl_product.SelectedValue = "Select an Option";
        this.ddl_leadcategory.SelectedValue = "Select an Option";
    }

    protected void ddl_product_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddl_product.SelectedItem.Text == "Dheya Discover (5999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Dheya Compass(9999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Dheya Nevigator (19999)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Parent Workshop")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Early Career (15000) - [suitable for age 25-30]")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Mid Career (20000) - [suitable for age above 35]")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "CDF Training (25000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "CPA (15000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Career Center (25000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
        if (ddl_product.SelectedItem.Text == "Career Cafe (300000)")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Select ProductName, price from tblUserProductRelation where pId= '" + ddl_product.SelectedValue + "'";
                // SqlDataReader dr = conn.ExecDataReader(query);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                Session["product_amount"] = dr["Price"].ToString();
                ViewState["Product"] = dr["ProductName"].ToString();
            }
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = BindGridView();
        GridView1.DataBind();
    }

    protected void ddl_leadcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_leadcategory.SelectedValue == "5")
        {
            string StrQueryExe = "select * from tblUserProductRelation where Student=1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "6";
            assigned_user_id = "sheetal";
        }
        if (ddl_leadcategory.SelectedValue == "4")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Parent = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "1";
            assigned_user_id = "pooja";
        }
        if (ddl_leadcategory.SelectedValue == "6")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Professional = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            if(ddl_product.SelectedItem.Text== "Early Career (15000) - [suitable for age bellow 35]")
            { ViewState["lead_category"] = "25";  }
            if (ddl_product.SelectedItem.Text == "Mid Career (20000) - [suitable for age above 35]")
            { ViewState["lead_category"] = "Mid_Career"; }
            assigned_user_id = "pooja";

        }
        if (ddl_leadcategory.SelectedValue == "12")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Business = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "5";
            assigned_user_id = "pooja";
        }
        if (ddl_leadcategory.SelectedValue == "School")
        {
            string StrQueryExe = "select* from tblUserProductRelation where School = 1 and pId not in (5,6,14,16) ";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "3";
            assigned_user_id = "pooja";
        }
        if (ddl_leadcategory.SelectedValue == "Institution")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Institution = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "2";
            assigned_user_id = "pooja";
        }
        if (ddl_leadcategory.SelectedValue == "Corporate")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Corporate = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "7";
            assigned_user_id = "arvind";
        }
        if (ddl_leadcategory.SelectedValue == "2")
        {
            string StrQueryExe = "select* from tblUserProductRelation where CDF = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "4";
            assigned_user_id = "arvind";
        }
        if (ddl_leadcategory.SelectedValue == "NGO")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Other = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "10";
            assigned_user_id = "arvind";
        }
        if (ddl_leadcategory.SelectedValue == "Other")
        {
            string StrQueryExe = "select* from tblUserProductRelation where Other = 1";
            dbContext.BindDropDownlist(StrQueryExe, ref ddl_product);
            ViewState["lead_category"] = "10";
            assigned_user_id = "pooja";
        }
    }
}