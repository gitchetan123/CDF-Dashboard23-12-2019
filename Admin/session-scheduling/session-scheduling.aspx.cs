using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;


//********************************************************************************************************
//PageName        : Session Scheduling
//Description     : This page is Session Scheduling by admin for cdf and student
//AddedBy         : **                   AddedOn   : 
//UpdatedBy       : **                   UpdatedOn : 
//Reason          : **
//********************************************************************************************************

public partial class admin_sessionScheduling_sessionScheduling : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    //create a object Db_context class for database connecton and database related operation
    db_context dbContext = new db_context();

    //create a object dataContext class for data related method .  
    data_context dataContext = new data_context();

    int c_id = 535;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            // Get user in using QueryString 
            // c_id = Convert.ToInt32(Request.QueryString["id"]);

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            //Print Error msg when query string is not correct format 
            // lbl_msg.Text = ex.Message;
        }


        if (!IsPostBack)
        {
            // c_id = Convert.ToInt32(Request.QueryString["id"]);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string strcmd = "";
                    strcmd = "select fname+' ' + lname from tblUserMaster where uid = " + c_id + "";
                    SqlCommand cmd3 = new SqlCommand(strcmd, connection);
                    txt_fname.Text = cmd3.ExecuteScalar().ToString().ToUpper();


                    strcmd = "select pid, prodName from tblProductMaster where Status='ACTIVE'";
                    dbContext.BindDropDownlist(strcmd, ref ddl_product);


                    string StrQuery2 = "select id,name from tblStatesMaster where countryId='" + 101 + "' ORDER BY name";
                    dbContext.BindDropDownlist(StrQuery2, ref ddl_state);
                    ddl_city.Items.Clear();
                    ddl_city.Items.Insert(0, "--Select--");

                    strcmd = "select uid,fname+' '+lname as name from tblUserMaster where cdfApproved='APPROVED' and userTypeId = 2 and userSource='DHEYA-CDF' and userStatus='ACTIVE'";
                    dbContext.BindDropDownlist(strcmd, ref ddl_ShadowCdf);


                    //SqlDataAdapter da = new SqlDataAdapter(strcmd, connection);
                    //DataSet ds = new DataSet();
                    //da.Fill(ds);
                    //ddl.DataSource = ds;
                    //ddl.DataTextField = ds.Tables[0].Columns[1].ToString();
                    //ddl.DataValueField = ds.Tables[0].Columns[0].ToString();
                    //ddl.DataBind();
                    //ddl.Items.Insert(0, "--Select--");
                    //ds.Tables.Clear();
                    //ds.Dispose();
                    //da.Dispose();

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
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (IsValid)
        //    {
        //        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
        //        {
        //            connection.Open();
        //            // check table tblUserMaster email id is not exist 
        //            string str = "select uId from tblUserMaster where email ='" + txt_email.Text + "'";
        //            SqlCommand cmd1 = new SqlCommand(str, connection);
        //            SqlDataReader dr = cmd1.ExecuteReader();

        //            //  If user's data not present in database then user will redirect to Reg.aspx page
        //            if (!dr.HasRows)
        //            {
        //                dr.Close();
        //                SqlCommand command = connection.CreateCommand();
        //                SqlTransaction transaction;

        //                // Start a local transaction.
        //                transaction = connection.BeginTransaction("RegisterTransaction");

        //                // Must assign both transaction object and connection
        //                // to Command object for a pending local transaction
        //                command.Connection = connection;
        //                command.Transaction = transaction;

        //                try
        //                {
        //                    str = "";

        //                    // Insert user's data into tblUserMaster table
        //                    str = "INSERT INTO tblUserMaster (userTypeId ,fname ,lname,contactNo,email,password,regDateTime,status,userStatus,userSource) VALUES "
        //                    + "('" + 2 + "','" + txt_fname.Text.Trim().ToString() + "','" + txt_lname.Text.Trim().ToString() + "' "
        //                    + ", '" + txt_contact.Text.Trim().ToString() + "', '" + Session["email"].ToString() + "',"
        //                    + "'" + txt_password.Text + "','" + DateTime.Now + "','Reg_Complete','ACTIVE','DHEYA-CDF')";

        //                    command.CommandText = str;
        //                    int i = command.ExecuteNonQuery();
        //                    if (i > 0)
        //                    {
        //                        str = "";

        //                        // Get uId from tblUserMaster
        //                        str = "select uId from tblUserMaster where email='" + Session["email"].ToString() + "'";
        //                        command.CommandText = str;
        //                        int uid = Convert.ToInt32(command.ExecuteScalar().ToString());
        //                        if (uid > 0)
        //                        {
        //                            str = "";
        //                            //Insert remaining user's data into tblUserDetails table
        //                            str = "INSERT INTO tblUserDetails(uId) VALUES" + "(" + uid + ")";
        //                            command.CommandText = str;
        //                            int j = command.ExecuteNonQuery();
        //                            if (j > 0)
        //                            {
        //                                // Attempt to commit the transaction.
        //                                transaction.Commit();
        //                                // Check user's Session
        //                                Session["email"] = txt_email.Text;
        //                                Session["uid"] = uid;
        //                                Session["userName"] = txt_fname.Text + " " + txt_lname.Text;
        //                                Session["status"] = "Reg_Complete";
        //                                Session["dheyaEmail"] = "No";
        //                                // redirect to Dashboard my product page
        //                                Response.Redirect("~/pre/home.aspx", false);
        //                                Session["formalImg"] = "~/images/Avatar.png";

        //                                //create email body
        //                                string body = this.PrepareBody(txt_fname.Text);
        //                                if (!body.Equals(null))
        //                                {
        //                                    //sent email
        //                                    var task = new Thread(() => dataContext.sendemail(txt_email.Text.Trim(), null, null, ConfigurationManager.AppSettings["CDFRegistrationCompleteSubject"].ToString(), body));
        //                                    task.Start();
        //                                }

        //                                //send sms
        //                                string SMSText = ConfigurationManager.AppSettings["CDFRegistrationCompleteSmsTemplate"].ToString();
        //                                dataContext.sendSms(txt_contact.Text.Trim().ToString(), SMSText);

        //                            }
        //                            else
        //                            {
        //                                div_msg.Visible = true;
        //                                div_msg.Attributes["class"] = "alert alert-danger";
        //                                div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        div_msg.Visible = true;
        //                        div_msg.Attributes["class"] = "alert alert-danger";
        //                        div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";
        //                    }
        //                }

        //                catch (Exception ex)
        //                {

        //                    Log.Error(ex);
        //                    div_msg.Visible = true;
        //                    div_msg.Attributes["class"] = "alert alert-danger";
        //                    div_msg.InnerText = "Requirement Form Not Successfully Submitted.....Please try again.";

        //                    // Attempt to roll back the transaction.
        //                    try
        //                    {
        //                        transaction.Rollback();
        //                    }
        //                    catch (Exception ex2)
        //                    {

        //                        Log.Error(ex2);
        //                        // This catch block will handle any errors that may have occurred
        //                        // on the server that would cause the rollback to fail, such as
        //                        div_msg.Visible = true;
        //                        div_msg.Attributes["class"] = "alert alert-danger";
        //                        div_msg.InnerText = "Rollback Error";
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                div_msg.Visible = true;
        //                div_msg.Attributes["class"] = "alert alert-danger";
        //                div_msg.InnerText = "Email Address already exist";
        //            }
        //        }
        //    }
        //    //If user's data already present in database then user will redirect to NDA.aspx page
        //    else
        //    {
        //        div_msg.Attributes["class"] = "alert alert-danger";
        //        div_msg.InnerText = "Your emailid is already registered...";
        //        div_msg.Visible = true;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Log.Error(ex);
        //    div_msg.Visible = true;
        //    div_msg.Attributes["class"] = "alert alert-danger";
        //    div_msg.InnerText = "Something wrong on form loading. Please Try again.";
        //}

    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        //div_msg.Visible = false;
        //clear();
    }

    //Clear all field 
    //public void clear()
    //{
    //    txt_fname.Text = "";
    //    txt_password.Text = "";
    //    txt_conpassword.Text = "";
    //    txt_lname.Text = "";
    //    txt_contact.Text = "";
    //    //txt_email.Text = "";
    //}

    //private string PrepareBody(string name)
    //{
    //    try
    //    {
    //        string body = string.Empty;
    //        using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CDFRegistrationCompleteTemplatePath"])))
    //        {
    //            body = reader.ReadToEnd();
    //        }
    //        body = body.Replace("{UserName}", name);
    //        body = body.Replace("{CDFDashboardLink}", ConfigurationManager.AppSettings["DashboardLink"]);
    //        return body;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error(ex.ToString());
    //        return null;
    //    }
    //}

    protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //The city DropDownList contents 
            string StrQuery = "select id, name from tblCitiesMaster where stateId='" + ddl_state.SelectedValue + "' ORDER BY name";
            dbContext.BindDropDownlist(StrQuery, ref ddl_city);
        }
        catch (Exception)
        {
            ddl_city.Items.Clear();
            ddl_city.Items.Insert(0, "--Select--");
        }
    }

    protected void ddl_CDFLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cdfLevel = ddl_CDFLevel.SelectedIndex == 0 ? 0 : Convert.ToInt32(ddl_CDFLevel.SelectedValue);

        string strcmd = "select uid,fname+' '+lname as name from tblUserMaster where cdfApproved='APPROVED' and userTypeId = 2 and userSource='DHEYA-CDF' and userStatus='ACTIVE' and  cdfLevel >=" + cdfLevel + "";

        dbContext.BindDropDownlist(strcmd, ref ddl_cdf);

    }
}