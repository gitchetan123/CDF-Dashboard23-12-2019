using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;

public partial class custompayment : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();
    int uid;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            // Get user in using QueryString 
            uid = Convert.ToInt32(Request.QueryString["id"]);
        }
        catch (Exception)
        {
        }

        if (!IsPostBack)
        {
            try
            {
            
                txt_createdBy.Text = Session["adminuser_email"].ToString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string strcmd = "select fname+' ' + lname from tblUserMaster where uid = " + uid + "";
                    SqlCommand cmd3 = new SqlCommand(strcmd, connection);
                    lbl_name.Text = " &nbsp" + cmd3.ExecuteScalar().ToString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerHtml = "Something went wrong. Please try again......";
            }

            BindGridView();
        }
    }

    private void BindGridView()
    {
        try
        {
            
            //Select details id in tblUserMaster table
            string strcmd = "SELECT id,uid ,amount,status ,case when approve is null then 'Pending' else 'Approved' end as approve,createdDate,modifiedDate,createdBy,updatedBy  FROM tblCustomPayment where uid=" + uid + " order by id desc";
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


    protected void btn_payment_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                connection.Open();
                int count = 0;
                try
                {
                    //string str = "select count(id) from tblCustomPayment where uid='" + uid + "' and status='ACTIVE'";
                    //SqlCommand cmd = new SqlCommand(str, connection);
                    //int countPayment = Convert.ToInt32(cmd.ExecuteScalar());
                    //if (countPayment == 0)
                    //{
                        string strcmd1 = "insert into tblCustomPayment (uid ,amount,status ,createdDate,createdBy) values (@uid ,@amount,'ACTIVE',@createdDate,@createdBy)";
                    SqlCommand cmd = new SqlCommand(strcmd1, connection);
                        cmd.Parameters.AddWithValue("@uid", uid);
                        cmd.Parameters.AddWithValue("@amount", txt_amount.Text);
                        cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@createdBy", txt_createdBy.Text);
                        count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            string strdata = "select fname,contactNo, email FROM tblUserMaster where uId = '" + uid + "'";
                            cmd = new SqlCommand(strdata, connection);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                dr.Read();
                                //call to Templete file for email body
                                string body = this.PopulateBody(dr["fname"].ToString());

                                ////Send email
                                //var task = new Thread(() => datacontext.sendemail(dr["email"].ToString(), null, null, ConfigurationManager.AppSettings["CustomPaymentEmailTemplateSubject"], body));
                                //task.Start();
                                // //sms
                                //string SMSText = ConfigurationManager.AppSettings["CustomPaymentSmsTemplate"].ToString();
                                //SMSText = SMSText.Replace("{CDF}", dr["fname"].ToString());
                                //datacontext.sendSms(dr["contactNo"].ToString(), SMSText);

                            }
                            BindGridView();
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-success";
                            div_msg.InnerHtml = "Custom payment created successfully";
                        }
                    //}
                    //else
                    //{
                    //    div_msg.Visible = true;
                    //    div_msg.Attributes["class"] = "alert alert-danger";
                    //    div_msg.InnerHtml = "Custom payment for this user currently activated, so you can not create another one";
                    //}
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
                        string strcmd1 = "update tblCustomPayment set status ='DEACTIVE' where id=@id";
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

    private string PopulateBody(string userName)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CustomPaymentEmailTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);

            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}