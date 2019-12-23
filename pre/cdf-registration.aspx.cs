using log4net;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading;


//********************************************************************************************************
//PageName        : Reg
//Description     : This page is veryfiy email id and fill user details with attach resume copy
//AddedBy         : Nitish                   AddedOn   : 
//UpdatedBy       : Nitish                   UpdatedOn : 
//Reason          : update on requre fields
//********************************************************************************************************

public partial class cdf_Registration : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    //create a object dataContext class for data related method .  
    data_context dataContext = new data_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["email"] != null)
            {
                txt_email.Text = Session["email"].ToString();
            }
            else
            {
                Response.Redirect("~/pre/email-verification.aspx", false);
            }
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                if (chkTerms.Checked)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                    {
                        connection.Open();
                        // check table tblUserMaster email id is not exist 
                        string str = "select uId from tblUserMaster where email ='" + txt_email.Text + "'";
                        SqlCommand cmd1 = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd1.ExecuteReader();

                        //  If user's data not present in database then user will redirect to Reg.aspx page
                        if (!dr.HasRows)
                        {
                            dr.Close();
                            SqlCommand command = connection.CreateCommand();
                            SqlTransaction transaction;

                            // Start a local transaction.
                            transaction = connection.BeginTransaction("RegisterTransaction");

                            // Must assign both transaction object and connection
                            // to Command object for a pending local transaction
                            command.Connection = connection;
                            command.Transaction = transaction;

                            try
                            {
                                str = "";

                                // Insert user's data into tblUserMaster table
                                str = "INSERT INTO tblUserMaster (userTypeId ,fname ,lname,contactNo,email,password,regDateTime,status,userStatus,userSource) VALUES "
                                + "('" + 2 + "','" + txt_fname.Text.Trim().ToString() + "','" + txt_lname.Text.Trim().ToString() + "' "
                                + ", '" + txt_contact.Text.Trim().ToString() + "', '" + Session["email"].ToString() + "',"
                                + "'" + txt_password.Text + "','" + DateTime.Now + "','Reg_Complete','ACTIVE','DHEYA-CDF')";

                                command.CommandText = str;
                                int i = command.ExecuteNonQuery();
                                if (i > 0)
                                {
                                    str = "";

                                    // Get uId from tblUserMaster
                                    str = "select uId from tblUserMaster where email='" + Session["email"].ToString() + "'";
                                    command.CommandText = str;
                                    int uid = Convert.ToInt32(command.ExecuteScalar().ToString());
                                    if (uid > 0)
                                    {
                                        str = "";
                                        //Insert remaining user's data into tblUserDetails table
                                        str = "INSERT INTO tblUserDetails(uId) VALUES" + "(" + uid + ")";
                                        command.CommandText = str;
                                        int j = command.ExecuteNonQuery();
                                        if (j > 0)
                                        {
                                            // Attempt to commit the transaction.
                                            transaction.Commit();
                                            // Check user's Session
                                            Session["email"] = txt_email.Text;
                                            Session["uid"] = uid;
                                            Session["userName"] = txt_fname.Text + " " + txt_lname.Text;
                                            Session["status"] = "Reg_Complete";
                                            Session["dheyaEmail"] = "No";
                                            // redirect to Dashboard my product page
                                            Response.Redirect("~/pre/home.aspx", false);
                                            Session["formalImg"] = "~/images/Avatar.png";

                                            //create email body
                                            string body = this.PrepareBody(txt_fname.Text);
                                            if (!body.Equals(null))
                                            {
                                                //sent email
                                                var task = new Thread(() => dataContext.sendemail(txt_email.Text.Trim(), null, null, ConfigurationManager.AppSettings["CDFRegistrationCompleteSubject"].ToString(), body));
                                                task.Start();
                                            }

                                            //send sms
                                            string SMSText = ConfigurationManager.AppSettings["CDFRegistrationCompleteSmsTemplate"].ToString();
                                            dataContext.sendSms(txt_contact.Text.Trim().ToString(), SMSText);

                                        }
                                        else
                                        {
                                            div_msg.Visible = true;
                                            div_msg.Attributes["class"] = "alert alert-danger";
                                            div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";
                                        }
                                    }
                                }
                                else
                                {
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-danger";
                                    div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";
                                }
                            }

                            catch (Exception ex)
                            {

                                Log.Error(ex);
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";

                                // Attempt to roll back the transaction.
                                try
                                {
                                    transaction.Rollback();
                                }
                                catch (Exception ex2)
                                {

                                    Log.Error(ex2);
                                    // This catch block will handle any errors that may have occurred
                                    // on the server that would cause the rollback to fail, such as
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-danger";
                                    div_msg.InnerText = "Rollback Error";
                                }
                            }
                        }
                        else
                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "Email Address already exist";
                        }
                    }
                }
                else
                {
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerText = "Please accept Terms and Conditions to continue";
                }
            }

            //If user's data already present in database then user will redirect to NDA.aspx page
            else
            {
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Your emailid is already registered...";
                div_msg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        }

    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        div_msg.Visible = false;
        clear();
    }

    //Clear all field 
    public void clear()
    {
        txt_fname.Text = "";
        txt_password.Text = "";
        txt_conpassword.Text = "";
        txt_lname.Text = "";
        txt_contact.Text = "";
        //txt_email.Text = "";
    }

    private string PrepareBody(string name)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CDFRegistrationCompleteTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", name);
            body = body.Replace("{CDFDashboardLink}", ConfigurationManager.AppSettings["DashboardLink"]);
            return body;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

}