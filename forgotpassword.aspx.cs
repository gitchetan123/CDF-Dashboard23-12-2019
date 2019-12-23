using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Web.UI.HtmlControls;

public partial class forgotpassword : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //create a object db_context class for database connecton and database related operation
    db_context dbContext = new db_context();    
    data_context dataContext = new data_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            div_msg.Visible = false;
            resetsuccess.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //check is valid page property 
        if (IsValid)
        {
            try
            {
                //add to emailid sting varible to user input email
                string emailid = txt_emailid.Text.Trim();
                if (emailid != "")
                {
                    //check email id is avaible to datebase and get uid
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                    {
                        connection.Open();
                        string Str = "select uId,fname,contactNo from tblUserMaster where (email= '" + emailid + "' or dheyaEmail= '" + emailid + "') and userTypeId='" + ConfigurationManager.AppSettings["userTypeId"] + "'";
                        SqlCommand cmd1 = new SqlCommand(Str, connection);
                        SqlDataReader dr = cmd1.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            int token = Convert.ToInt32(dataContext.GenerateRandomNo(1000, 9999) + "" + dr.GetValue(0));
                            string encrypttoken = dataContext.Dataencrypt(token.ToString());
                            if (!encrypttoken.Equals(null))
                            {
                                //create to reset link
                                string resetlink = ConfigurationManager.AppSettings["ResetPasswordURL"] + encrypttoken;

                                string body = this.PrepareBody(resetlink);
                                if (!body.Equals(null))
                                {
                                    //inset data in tblForgotPassword 
                                    string strcmd = "INSERT INTO tblForgotPassword(uId,token,status,date) VALUES (@c_id,@token,@status,@date)";
                                    SqlCommand cmd = new SqlCommand(strcmd);
                                    cmd.Parameters.AddWithValue("@c_id", dr.GetValue(0));
                                    cmd.Parameters.AddWithValue("@token", token);
                                    cmd.Parameters.AddWithValue("@status", "ACTIVE");
                                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                                    int i = dbContext.ExecNonQuerypara(cmd);
                                    if (i > 0)
                                    {                                       
                                        //if data save to database then sent email  
                                        var task = new Thread(() => dataContext.sendemail(emailid, null, null, ConfigurationManager.AppSettings["ResetPasswordEmailSubject"], body));
                                        task.Start();                                       

                                        if (dr["fname"].ToString() != "" && dr["contactNo"].ToString() != "")
                                        {
                                            string SMSText = ConfigurationManager.AppSettings["ResetPasswordSmsTemplate"].ToString();
                                            SMSText = SMSText.Replace("{CDF}", dr["fname"].ToString());
                                            dataContext.sendSms(dr["contactNo"].ToString(), SMSText);
                                        }
                                    }
                                    resetsuccess.Visible = true;
                                    resetbox.Visible = false;
                                    HtmlMeta meta = new HtmlMeta();
                                    meta.HttpEquiv = "Refresh";
                                    meta.Content = "5;url=login.aspx";
                                    this.Page.Controls.Add(meta);
                                }


                            }
                            else
                            {
                                //tokan is null then print error msg
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerText = "Something went worng. Please try again.";

                            }
                        }
                        else
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "No account matches " + emailid;
                        }
                    }
                }
                else
                {
                    //user email id is invalid
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Please enter valid email address";
                }

            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something went worng. Please try again.";
            }
        }

    }

    private string PrepareBody(string pwdresetlink)
    {
        //create email body
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["ResetPasswordEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", "Sir / Madam");
            body = body.Replace("{passwordresetlink}", pwdresetlink);
            return body;
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return null;
        }
    }
 
}