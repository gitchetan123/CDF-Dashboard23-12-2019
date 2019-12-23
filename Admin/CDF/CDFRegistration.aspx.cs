using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web.UI.WebControls;

public partial class CDF_CDFRegistration : System.Web.UI.Page
{
    db_context dbContext = new db_context();
    data_context dataContext = new data_context();

    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        string strquery = "SELECT uId, fname, lname, dheyaEmail, userStatus, cdfLevel FROM tblUserMaster where userTypeId = 2 and cdfApproved='APPROVED' and userSource='DHEYA-CDF' order by uId desc";
        // bindgrid(strquery);
        //create a dataset object and fill it 
        DataSet ds2 = dbContext.ExecDataSet(strquery);
        int row_count = ds2.Tables[0].Rows.Count;
        lbl_rowcount.Text = "Total - " + row_count.ToString();
        if (!IsPostBack)
        {
            clear_data();
            div_msg.Visible = false;
        }
    }
    //private void bindgrid(string strqry)
    //{
    //    try
    //    {
    //        grid_edu.DataSource = null;
    //        string strcmd = strqry;
    //        DataSet ds = dbContext.ExecDataSet(strcmd);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            grid_edu.DataSource = ds;
    //            grid_edu.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error(ex.ToString());
    //    }

    //}

    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                connection.Open();
                int count = 0;
                try
                {
                    string strcmd1 = "SELECT count(uid) FROM tblUserMaster where dheyaEmail = '" + txt_email.Text.Trim() + "'";
                    SqlCommand cmd = new SqlCommand(strcmd1, connection);
                    count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
                if (count == 0)
                {
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
                        string strcmd = "insert into tblUserMaster(dheyaEmail,email,cdfLevel,userStatus,password,userTypeId,cdfApproved,userSource,cdfrating,profileUpdateApproval) values ('" + txt_email.Text.Trim() + "','" + txt_personalEmail.Text.Trim() + "','" + drop_level.Text + "','" + drop_status.Text + "','Dheya@123',2,'APPROVED','DHEYA-CDF','3.00',0)";

                        command.CommandText = strcmd;
                        int i = command.ExecuteNonQuery();

                        strcmd = "select uId from tblUserMaster where dheyaEmail='" + txt_email.Text.Trim().ToString() + "'";
                        command.CommandText = strcmd;
                        int uid = Convert.ToInt32(command.ExecuteScalar().ToString());

                        strcmd = "INSERT INTO tblUserDetails(uId) VALUES" + "('" + uid + "')";
                        command.CommandText = strcmd;
                        int j = command.ExecuteNonQuery();

                        if (i > 0 && uid > 0 && j > 0)
                        {
                            // Attempt to commit the transaction.
                            transaction.Commit();

                            string tocdf = txt_email.Text.Trim();

                            if (tocdf.Contains('.') && tocdf.Contains('@'))
                            {
                                //string body = this.PrepareBody(tocdf, "Dheya@123");
                                //if (!body.Equals(null))     
                                //{
                                //    var task = new Thread(() => dataContext.sendemail(tocdf, null, ConfigurationManager.AppSettings["BCCWelcomeEmailManual"].ToString(), ConfigurationManager.AppSettings["CDFAddManuallySubject"].ToString(), body));
                                //    task.Start();
                                //}

                                string body = this.PopulateBodyWelcomeEmail("Sir / Madam", txt_email.Text, "Dheya@123");

                                //var task = new Thread(() => dataContext.sendemail(txt_email.Text, null, ConfigurationManager.AppSettings["BCCWelcomeEmail"].ToString(), ConfigurationManager.AppSettings["WelcomeEmailTemplateSubject"], body));
                                // Mannual CDF Registration Welcome mail now goes to Personal Email ID.
                                var task = new Thread(() => dataContext.sendemail(txt_personalEmail.Text, null, ConfigurationManager.AppSettings["BCCWelcomeEmail"].ToString(), ConfigurationManager.AppSettings["WelcomeEmailTemplateSubject"], body));
                                task.Start();

                                if (txt_personalEmail.Text != "")
                                {
                                    var task2 = new Thread(() => dataContext.sendemail(txt_personalEmail.Text, null, null, ConfigurationManager.AppSettings["WelcomeEmailTemplateSubject"], body));
                                    task2.Start();
                                }
                            }
                        }

                        clear_data();
                        // grid_edu.DataBind();

                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerText = "CDF registered successfully.";
                    }
                    catch (Exception ex)
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Something wrong. Please Try again.";
                        Log.Error(ex);
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            Log.Error(Convert.ToString(Session["user_email"]) + "Rollback Error" + ex2);
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
                    div_msg.InnerText = "Email-Id already registered.";
                }
            }
        }
    }

    private string PopulateBodyWelcomeEmail(string userName, string userEmail, string userPassword)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["WelcomeEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            body = body.Replace("{userEmail}", userEmail);
            body = body.Replace("{userPassword}", userPassword);

            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void grid_edu_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    hf_id.Value = grid_edu.SelectedValue.ToString();
    //    txt_email.Text = grid_edu.SelectedRow.Cells[4].Text;
    //    drop_level.SelectedValue = grid_edu.SelectedRow.Cells[5].Text;
    //    drop_status.SelectedValue = grid_edu.SelectedRow.Cells[6].Text;
    //    btn_save.Enabled = false;
    //    btn_update.Enabled = true;

    //}
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        clear_data();
    }
    void clear_data()
    {
        txt_email.Text = "";
        txt_personalEmail.Text = "";
        drop_level.SelectedValue = "--Select--";
        drop_status.SelectedValue = "--Select--";
        btn_save.Enabled = true;
        //btn_update.Enabled = false;
        div_msg.Visible = false;
    }
    //protected void btn_update_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (IsValid)
    //        {
    //            string strcmd = "update tblUserMaster set dheyaEmail='" + txt_email.Text + "',cdfLevel='" + drop_level.Text + "',userStatus='" + drop_status.Text + "' where uId='" + hf_id.Value + "'";
    //            int i = dbContext.ExecNonQuery(strcmd);
    //            clear_data();
    //           // grid_edu.DataBind();
    //            div_msg.Visible = true;
    //            div_msg.Attributes["class"] = "alert alert-success";
    //            div_msg.InnerText = "CDF updateded successfully.";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        div_msg.Visible = true;
    //        div_msg.Attributes["class"] = "alert alert-danger";
    //        div_msg.InnerText = "Something wrong. Please Try again.";
    //        Log.Error(ex.ToString());
    //    }
    //}

    //protected void grid_edu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grid_edu.PageIndex = e.NewPageIndex;
    //    bindgrid("SELECT uId, fname, lname, dheyaEmail, userStatus, cdfLevel FROM tblUserMaster where userTypeId = 2  order by uId desc");
    //    grid_edu.DataBind();
    //}

    private string PrepareBody(string emailid, string otp)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CDFAddManuallyTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{dheya email-ID}", emailid);
            body = body.Replace("{CDF OTP}", otp);
            body = body.Replace("{UserName}", "Sir / Madam");
            body = body.Replace("{CDFDashboardLink}", ConfigurationManager.AppSettings["DashboardLink"]);
            return body;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }


    //protected void btn_search_Click(object sender, EventArgs e)
    //{
    //    bindgrid("SELECT uId, fname, lname, dheyaEmail, userStatus, cdfLevel FROM tblUserMaster where userTypeId = 2 and dheyaEmail like '%" + txt_email.Text.Trim() + "%' order by uId desc");

    //}
}