using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class Admin_Sell_sell_details : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    data_context datacontext = new data_context();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["adminuser_email"] != null)
        {
            if (!IsPostBack)
            {
                BindGridView1();
                grid_Pyment.DataBind();

                string StrQueryExe = "select id,exeName from tblExecutive where status ='ACTIVE' and forUserType=2";
                dbContext.BindDropDownlist(StrQueryExe, ref ddl_executiveName);
            }
        }
        else
        {
            Response.Redirect("~/Admin/login.aspx", false);
        }
    }

    public DataTable BindGridView1()
    {
        try
        {
            string strcmd = "select UM.userTypeId, UM.uId, RD.FirstName as Name ,  RD.Contact, RD.Email,RD.Program, RD.CreatedDate,UM.userStatus, RD.LeadCategory, "
                            +" case when P.amount is null then 'not paid' else 'paid' end as PaymentStatus, "
                            +" RD.Amount, RD.ProductId, RD.ReferByEmail, "
                            +" case when RD.IsCase = 1 then 'case' when RD.IsLead = 1 then 'lead' when RD.IsContact = 1 then 'contact' end as refType  from tblUserMaster UM "
                            +" left outer join tblPayment as P on P.uId = UM.uId "
                            +" left outer join tblReferralDetail RD on UM.uId = RD.uId "
                            +" right outer join View_UserMaster_Refrral_Detail as VRD on VRD.email = UM.email "
                            +" where(RD.IsCase = 1) ";

            if (txt_Search.Text != "")
            {
                strcmd += " and RD.Email like '%" + txt_Search.Text + "%' or RD.FirstName like '%" + txt_Search.Text + "%' or RD.Contact like '%" + txt_Search.Text + "%' ";
            }
            if (txt_Search_Ref.Text != "")
            {
                strcmd += " and RD.ReferByEmail like '%" + txt_Search_Ref.Text + "%' ";
            }
            if (ddl_Payment_Status.SelectedValue != "0")
            {
                strcmd += " and UM.userStatus= '" + ddl_Payment_Status.SelectedValue + "' ";
            }
            if (ddl_executiveName.SelectedIndex>0)
            {
                strcmd += " and RD.ReferByEmail  in (Select email FROM tblVerifyRegistration where executiveId = '"+ ddl_executiveName.SelectedValue+ "')  ";
            }
            
            strcmd += " order by UM.uId DESC ";

            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            grid_Pyment.DataSource = ds;
            int row_count = ds.Tables[0].Rows.Count;
            lbl_rowcount.Text = "Total - " + row_count.ToString();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
            return null;
        }
    }

    protected void btn_payment_Click(object sender, EventArgs e)
    {
        if (Session["adminuser_email"] != null)
        {
            if (IsValid)
            {
                string s = "";
                if (s.Contains("@dheya"))
                {
                    //div_msg.Visible = true;
                    //div_msg.Attributes["class"] = "alert alert-danger";
                    //div_msg.InnerText = "Dheya email id is not allowed";
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                    {
                        connection.Open();
                        int count = 0;
                        try
                        {
                            string str = "select count(uid) from tblUserMaster where email='" + s + "'";
                            SqlCommand cmd = new SqlCommand(str, connection);
                            int countuid = Convert.ToInt32(cmd.ExecuteScalar());
                            if (countuid == 0)
                            {

                                str = "select count(id) from tblVerifyRegistration where email='" + s + "'";
                                cmd = new SqlCommand(str, connection);
                                int countuser = Convert.ToInt32(cmd.ExecuteScalar());
                                if (countuser == 0)
                                {
                                    string strcmd1 = "insert into tblVerifyRegistration (email,executiveId,createDate,status)  values(@email, @executiveId, @createDate, @status)";
                                    cmd = new SqlCommand(strcmd1, connection);
                                    //cmd.Parameters.AddWithValue("@email", txt_email.Text.Trim());
                                    //cmd.Parameters.AddWithValue("@executiveId", ddl_executiveName.Text);
                                    cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@status", "ACTIVE");
                                    count = cmd.ExecuteNonQuery();
                                    if (count > 0)
                                    {
                                        //string strdata = "select fname,contactNo, email FROM tblUserMaster where uId = '" + uid+"'";
                                        //cmd = new SqlCommand(strdata, connection);
                                        //SqlDataReader dr = cmd.ExecuteReader();
                                        //if(dr.HasRows)
                                        //{
                                        //    dr.Read();
                                        //    //call to Templete file for email body
                                        //    string body = this.PopulateBody(dr["fname"].ToString());

                                        //    //Send email

                                        //var task = new Thread(() => );
                                        //task.Start();

                                        //    datacontext.sendemail(dr["email"].ToString(), null, null, ConfigurationManager.AppSettings["0000000000000000EmailTemplateSubject"], body);

                                        //}
                                        BindGridView1();
                                        //div_msg.Visible = true;
                                        //div_msg.Attributes["class"] = "alert alert-success";
                                        //div_msg.InnerHtml = "User created successfully";
                                    }
                                }
                                else
                                {
                                    //div_msg.Visible = true;
                                    //div_msg.Attributes["class"] = "alert alert-danger";
                                    //div_msg.InnerHtml = "Email id already exists.";
                                }
                            }
                            else

                            {
                                //div_msg.Visible = true;
                                //div_msg.Attributes["class"] = "alert alert-danger";
                                //div_msg.InnerHtml = "Email id already exists in Database";
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex);
                            //div_msg.Visible = true;
                            //div_msg.Attributes["class"] = "alert alert-danger";
                            //div_msg.InnerHtml = "Something went wrong. Please try again......";
                        }
                    }
                }
            }
        }
        else
        {
            Response.Redirect("~/Admin/Login.aspx", false);
        }
    }
    protected void grid_Pyment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() != "Page")
        {
            string id = e.CommandArgument.ToString();

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
                        BindGridView1();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

        }
    }
    
    protected void grid_Pyment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGridView1();

    }
    protected void btn_preview_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                grid_Pyment.DataSource = BindGridView1();
                grid_Pyment.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {
        ddl_executiveName.SelectedIndex = 0;
        ddl_city.SelectedIndex = 0;
        ddl_batch.SelectedIndex = 0;
        ddl_cdfAproveStatus.SelectedIndex = 0;
        ddl_cdfLevel.SelectedIndex = 0;
        ddl_testApproveStatus.SelectedIndex = 0;
        ddl_testCompStatus.SelectedIndex = 0;
        txt_from.Text = "";
        txt_to.Text = "";
        txt_name.Text = "";

        txt_Search.Text = "";
        txt_Search_Ref.Text = "";
        ddl_Payment_Status.SelectedItem.Text = "--Select--";
    }
    
    private DataTable BindGridView1(string strcmd)
    {
        try
        {
            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);
            int row_count = ds.Tables[0].Rows.Count;
            lbl_rowcount.Text = "Total - " + row_count.ToString();
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Text = "Something went wrong. Please try again......";
            return null;
        }
    }
}