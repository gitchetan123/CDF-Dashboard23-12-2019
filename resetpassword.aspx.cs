using System;
using System.Data.SqlClient;
using System.Configuration;

public partial class resetpassword : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context class for database connecton and database related operation
    db_context dbContext = new db_context();
    data_context dataContext = new data_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //check post back event 
            if (!IsPostBack)
            {
                resetfail.Visible = false;
                reset.Visible = false;
                resetsuccess.Visible = false;
                string token = dataContext.Datadecrypt(Request.QueryString["tkn"].ToString().Trim());

                //check token not empty
                if (token != null)
                {
                    //get user details
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                    {
                        connection.Open();
                        string Strcity = "SELECT uId,status FROM tblForgotPassword WHERE token = " + Convert.ToInt32(token);
                        SqlCommand cmd1 = new SqlCommand(Strcity, connection);
                        SqlDataReader dr = cmd1.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            string ststus = dr.GetValue(1).ToString();
                            //check status is active
                            if (ststus.Equals("ACTIVE"))
                            {
                                //update to status deactive
                                string str = "update tblForgotPassword set status ='DEACTIVE' where token = " + Convert.ToInt32(token);
                                int i = dbContext.ExecNonQuery(str);
                                if (i > 0)
                                {
                                    reset.Visible = true;
                                    Session["uid"] = dr.GetValue(0).ToString();
                                }
                            }
                            else
                            {
                                //if password link is expired print this msg 
                                resetfail.Visible = true;
                                reset.Visible = false;
                                resetsuccess.Visible = true;
                                resetfail.InnerText = "Password reset link has been expired.";
                            }
                        }
                        else
                        {
                            //if token is not match print this msg
                            resetfail.Visible = true;
                            reset.Visible = false;
                            resetfail.InnerText = "Invalid User.";
                        }
                    }
                }
                else
                {
                    //if token is null then print this msg
                    resetfail.Visible = true;
                    reset.Visible = false;
                    resetfail.InnerText = "Something went worng. Please try again.";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            Session.Clear();
            Response.Redirect("~/login.aspx");
        }
    }
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        if (Session.Count > 0)
        {
            if (IsValid)
            {
                try
                {
                    //get session uid to uid string
                    string uid = Session["uid"].ToString();
                    if (!uid.Equals("") && uid != null)
                    {
                        string password = txt_password.Text.Trim();
                        string cpassword = txt_cpassword.Text.Trim();

                        //if both password is match then below code is executive
                        if (password.Equals(cpassword))
                        {
                            //update new password to tblUserMaster table
                            string str = "update tblUserMaster set password = '" + password + "' where uId=" + uid;
                            int i = dbContext.ExecNonQuery(str);
                            if (i > 0)
                            {
                                reset.Visible = false;
                                resetsuccess.Visible = true;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    //if eany exception happn then redirect to dafault page
                    Log.Error(ex);
                    Session.Clear();
                    Response.Redirect("~/login.aspx");
                }
            }
        }
        else
        {
            //if session is not avaible then redirect to default page            
            Session.Clear();
            Response.Redirect("~/login.aspx");

        }
    }
}