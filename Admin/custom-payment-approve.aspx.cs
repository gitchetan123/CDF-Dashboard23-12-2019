using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;

public partial class customPaymentApprove : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();
    int uid;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Get user in using QueryString 
            uid = Convert.ToInt32(Request.QueryString["id"]);
            //txt_createdBy.Text = Session["adminuser_email"].ToString();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerHtml = "Something went wrong. Please try again......";
        }
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    private void BindGridView()
    {
        try
        {
            //Select details id in tblUserMaster table
            //string strcmd = "SELECT id,uid ,amount,status ,case when approve is null then 'Pending' else 'Approved' end as approve,createdDate,modifiedDate,createdBy,updatedBy  FROM tblCustomPayment order by id desc";
            string strcmd = "SELECT id, cp.uid,fname,lname, amount, email, cp.status ,case when approve is null then 'Pending' else 'Approved' end as approve, " +
            "createdDate,modifiedDate,createdBy,updatedBy FROM tblCustomPayment as cp inner join tblUserMaster as um on cp.uid = um.uId ";
             
         if (txt_email.Text != "")
            {
                strcmd += "where fname like '%" + txt_email.Text.Trim() + "%'  or lname like '%" + txt_email.Text.Trim() + "%' or email like '%" + txt_email.Text.Trim() + "%' or createdBy like '%" + txt_email.Text.Trim() + "%' ";
            }

            strcmd += " order by cp.id desc ";

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
       
    protected void grid_Pyment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "approve")
        {
           // string id = e.CommandArgument.ToString();
            if (IsValid)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    connection.Open();
                    int count = 0;
                    try
                    {
                        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                        string id = commandArgs[0];
                        string uid = commandArgs[1];


                        string strcmd1 = "update tblCustomPayment set approve =1 where id=@id";
                        SqlCommand cmd = new SqlCommand(strcmd1, connection);
                        cmd.Parameters.AddWithValue("@id", id);
                        count = cmd.ExecuteNonQuery();
                        if (count > 0)
                        {
                            BindGridView();

                            string strdata = "select fname,contactNo, email FROM tblUserMaster where uId = '" + uid + "'";
                            cmd = new SqlCommand(strdata, connection);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                dr.Read();
                                //call to Templete file for email body
                                string body = this.PopulateBody(dr["fname"].ToString());

                                if (dr["email"] != DBNull.Value)
                                {
                                    //Send email
                                    var task = new Thread(() => datacontext.sendemail(dr["email"].ToString(), null, null, ConfigurationManager.AppSettings["CustomPaymentEmailTemplateSubject"], body));
                                    task.Start();
                                }
                                else
                                {
                                    Log.Info("Advance Payment Appprove email not send because email not exist");
                                }

                                if (dr["contactNo"] != DBNull.Value)
                                {
                                    //sms
                                    string SMSText = ConfigurationManager.AppSettings["CustomPaymentSmsTemplate"].ToString();
                                    SMSText = SMSText.Replace("{CDF}", dr["fname"].ToString());
                                    datacontext.sendSms(dr["contactNo"].ToString(), SMSText);
                                }
                                else
                                {
                                    Log.Info("Advance Payment Appprove sms not send because contact No not exist");
                                }

                            }
                            //BindGridView();
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-success";
                            div_msg.InnerHtml = "Custom payment created successfully";

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

    protected void grid_Pyment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_Pyment.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
}