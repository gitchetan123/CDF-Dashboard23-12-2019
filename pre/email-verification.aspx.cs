using log4net;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

public partial class pre_email_verification : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    data_context datacontext = new data_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            div_msg3.Visible = false;
        }
    }

    protected void btn_verify_Click(object sender, EventArgs e)
    {
        try
        {
            string s = txt_email.Text;
            if (s.Contains("@dheya"))
            {
                div_msg3.Attributes["class"] = "alert alert-danger";
                div_msg3.InnerText = "Dheya emailid is not allowed";
                div_msg3.Visible = true;
            }
            else
            {
                string connectionstring = ConfigurationManager.ConnectionStrings["DBConnection"].ToString();
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    // check table tblUserMaster email id exist or not 
                    string str = "select uId,fname,email from tblUserMaster where email ='" + txt_email.Text + "'";
                    SqlCommand cmd = new SqlCommand(str, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();

                        //Print msg if already register 
                        div_msg3.Attributes["class"] = "alert alert-danger";
                        div_msg3.InnerText = "You have already registered.";
                        div_msg3.Visible = true;
                    }
                    else
                    {
                        dr.Close();
                        string query_verfiedUser = "Select executiveId,email from tblverifyRegistration where status ='ACTIVE' and email = '" + txt_email.Text + "'";
                        cmd = new SqlCommand(query_verfiedUser, con);

                        SqlDataReader sdr = cmd.ExecuteReader();

                        if (sdr.HasRows)
                        {
                            sdr.Read();

                            div_msg3.Visible = false;
                            Session["email"] = txt_email.Text;
                            Response.Redirect("cdf-registration.aspx", false); // added by dhananjay korde 29/05/2018 (insted of below comment)
                            ////Generate OTP Random number in 1000 to 9999                      
                            //int i = datacontext.GenerateRandomNo(1000, 9999);

                            ////call to Templete file for email body
                            //string body = this.PopulateBody("Sir / Madam", i.ToString());

                            ////Send email
                            //var task = new Thread(() => datacontext.sendemail(txt_email.Text, null, null, ConfigurationManager.AppSettings["OTPEmailSubject"], body));
                            //task.Start();


                            ////add otp save to view state
                            //ViewState["OTP"] = i;
                            //txt_email.Visible = false;
                            //btn_verify.Visible = false;
                            //txt_otp.Visible = true;
                            //btn_otp.Visible = true;


                            //div_msg2.Visible = true;
                            //change_email.Visible = true;
                            //div_msg2.Attributes["class"] = "alert alert-success";
                            //div_msg2.InnerText = "OTP has been sent successfully in your email id, Please check your email.";
                        }
                        else
                        {
                            div_msg3.Attributes["class"] = "alert alert-danger";
                            div_msg3.InnerText = "You are not authorized to register, for any further information please visit https://www.dheya.com  and feel free to contact us for any queries to assist you better at customer support at phone numbers: +91 99 23 400 555 | 020-24223655 / 65007555 or write us at care@dheya.com.";
                            div_msg3.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void btn_otp_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_otp.Text.ToString() == ViewState["OTP"].ToString())
            {
                div_msg2.Visible = true;
                div_msg2.Attributes["class"] = "alert alert-success";
                div_msg2.InnerText = "OTP is corrcet";
                //Response.Write("OTP is corrcet");
                txt_otp.Visible = false;
                btn_otp.Visible = false;
                //div_con2.Visible = true;
                div_con1.Visible = false;

                Session["email"] = txt_email.Text;
                Response.Redirect("cdf-registration.aspx", false);
            }
            else
            {
                div_msg2.Attributes["class"] = "alert alert-danger";
                div_msg2.InnerText = "OTP is not correct.";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg3.Attributes["class"] = "alert alert-danger";
            div_msg3.InnerText = "Something went wrong. Please try again.";
        }

    }

    private string PopulateBody(string userName, string OPT)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["OTPEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{OTP}", OPT);

            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void change_email_Click(object sender, EventArgs e)
    {
        Response.Redirect("email-verification.aspx", false);
    }
}