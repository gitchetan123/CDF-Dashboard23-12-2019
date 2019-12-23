using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Web.UI.WebControls;

public partial class verifycdfregistration : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            BindGridView();
            div_msg.Visible = false;
            try
            {
                string StrQueryExe = "select id,exeName from tblExecutive where status ='ACTIVE' and forUserType=2";
                dbContext.BindDropDownlist(StrQueryExe, ref ddl_executiveName);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                // if condition fails then user will get following message
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong on form loading. Please Try again." + ex.Message;
            }
        }
    }

    private void BindGridView()
    {
        try
        {
            //Select details id in tblVerifyRegistration table
            //string strcmd = "select vr.id,email,createDate,vr.status,e.exeName from tblVerifyRegistration as vr left outer join tblExecutive as e on e.id =vr.executiveId  order by id desc";
            string strcmd = "select vr.id,vr.email,createDate,vr.status,e.exeName,ISNULL(u.teststatus,'Incomplete') as teststatus,um.status as TestApproval,ISNULL(SUM(p.amount),0) as TotalPayment from tblVerifyRegistration as vr left outer join tblExecutive as e on e.id = vr.executiveId Left Outer join tblUserMaster as um on vr.email = um.email  Left Outer Join tblPayment as p on um.uId = p.uId Left Outer join tblUserProductMaster as u on um.uId = u.uId and u.prodid = 7 group by p.amount,vr.id,vr.email,createDate,vr.status,e.exeName,u.teststatus,um.status,um.uId order by vr.id desc";
            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            grid_verifiedCdf.DataSource = ds;
            grid_verifiedCdf.DataBind();
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

            string s = txt_email.Text;
            if (s.Contains("@dheya"))
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Dheya emailid is not allowed";
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {
                    connection.Open();
                    int count = 0;
                    try
                    {

                        string str = "select count(uid) from tblUserMaster where email='" + txt_email.Text + "'";
                        SqlCommand cmd = new SqlCommand(str, connection);
                        int countuid = Convert.ToInt32(cmd.ExecuteScalar());
                        if (countuid == 0)
                        {

                            str = "select count(id) from tblVerifyRegistration where email='" + txt_email.Text + "'";
                            cmd = new SqlCommand(str, connection);
                            int countuser = Convert.ToInt32(cmd.ExecuteScalar());
                            if (countuser == 0)
                            {
                                string strcmd1 = "insert into tblVerifyRegistration (email,executiveId,createDate,status,userType)  values(@email, @executiveId, @createDate, @status,2)";
                                cmd = new SqlCommand(strcmd1, connection);
                                cmd.Parameters.AddWithValue("@email", txt_email.Text.Trim());
                                cmd.Parameters.AddWithValue("@executiveId", ddl_executiveName.Text);
                                cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@status", "ACTIVE");
                                count = cmd.ExecuteNonQuery();
                                if (count > 0)
                                {
                                    // Send SMS
                                    string SMSText = ConfigurationManager.AppSettings["CDFEmailVerificationSMS"].ToString();
                                    datacontext.sendSms(txt_contact.Text.Trim().ToString(), SMSText);

                                    // Send Email
                                    string body = this.PopulateBody(txt_email.Text.Trim());
                                    var task = new Thread(() => datacontext.SendEmail1(txt_email.Text, ConfigurationManager.AppSettings["CDFEmailVerificationSubject"], body));
                                    task.Start();

                                    BindGridView();
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-success";
                                    div_msg.InnerHtml = "User created successfully";
                                }
                            }
                            else
                            {
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerHtml = "Email id already exists.";
                            }
                        }
                        else

                        {
                            div_msg.Visible = true;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerHtml = "Email id already exists in Database";
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
    }

    protected void grid_Pyment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() != "Page")
        {
            string id = e.CommandArgument.ToString();
            //if (IsValid)
            //{
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                connection.Open();
                int count = 0;
                try
                {
                    string strcmd1 = "update tblVerifyRegistration set status ='DEACTIVE' where id=@id";
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

    private string PopulateBody(string email)
    {
        try
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CDFEmailVerificationTemplatePath"])))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Email}", email);

            return body;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void grid_verifiedCdf_PageIndexChanging(object sender, GridViewPageEventArgs e)

    {
        grid_verifiedCdf.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}