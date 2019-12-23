using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

public partial class login : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["checkpageload"] = null;
            div_msg.Visible = false;
            this.Form.DefaultButton = "btn_login";

            if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            {
                txt_username.Text = Request.Cookies["UserName"].Value;
                txt_password.Attributes["value"] = Request.Cookies["Password"].Value;
            }
        }
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                //login query
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                string connStr = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
               // string connectionString = ConfigurationManager.ConnectionStrings["career_ConnectionStringNew"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string str = "select M.IsAllowReport , M.uid, email, userStatus, cdfLevel,M.cdfApproved,D.preStatus,M.status, ISNULL(fname, 'No') as fname,lname,ISNULL(dheyaEmail, 'No') as dheyaEmail, ISNULL(formalImg, 'No') as formalImg,  " +
                    " ISNULL(casualImg, 'No') as casualImg,contactNo  from tblUserMaster as M left outer join tblUserDetails as D on M.uId=D.uId " +
                    " where userTypeId = 2 and(email = @userId or dheyaEmail = @userId) and " +
                    " password = @password COLLATE Latin1_General_CS_AS and M.userSource = 'DHEYA-CDF'";

                    SqlCommand cmd = new SqlCommand(str, con);
                    cmd.Parameters.AddWithValue("@userId", txt_username.Text);
                    cmd.Parameters.AddWithValue("@password", txt_password.Text);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    bool flag = false;
                    if (dr.HasRows)
                    {
                        //Remember username and password code
                        if (chkRememberMe.Checked)
                        {
                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                        }
                        else
                        {
                            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);

                        }
                        Response.Cookies["UserName"].Value = txt_username.Text.Trim();
                        Response.Cookies["Password"].Value = txt_password.Text.Trim();

                        dr.Read();
                        {
                            if (Convert.ToString(dr["userStatus"]) == "ACTIVE")
                            {
                                //User Sessions    
                                Session["type"] = null;
                                Session["uid"] = dr["uid"].ToString();
                                Session["userName"] = dr["fname"].ToString() + " " + dr["lname"].ToString();
                                Session["cdfLevel"] = dr["cdfLevel"].ToString();
                                Session["dheyaEmail"] = dr["dheyaEmail"].ToString();
                                Session["email"] = dr["email"].ToString();
                                Session["status"] = dr["status"].ToString();
                                Session["IsAllowReport"] = dr["IsAllowReport"].ToString();
                                //Session["profileStatus"] = dr["profileStatus"].ToString();
                                //    Session["logintype"] = "nongoogle";
                                if (dr["formalImg"].Equals("No"))
                                    Session["formalImg"] = "~/images/Avatar.png";
                                else
                                    Session["formalImg"] = "~/doc/formalImg/" + dr["formalImg"].ToString();

                                if (dr["casualImg"].Equals("No"))
                                    Session["casualImg"] = "~/images/Avatar.png";
                                else
                                    Session["casualImg"] = "~/doc/images/" + dr["casualImg"].ToString();

                                string uname = Session["userName"].ToString();

                                if ((dr["fname"].ToString().Equals("No") || dr["status"].ToString().Equals("NotLogin")) && txt_password.Text.Equals("Dheya@123"))
                                {
                                    // Response.Redirect("changePassword.aspx", false);
                                    Response.Redirect("manual-registration.aspx", false);
                                }
                                else if (dr["dheyaEmail"].ToString()=="No")
                                {
                                    Response.Redirect("pre/home.aspx", false);
                                }
                                else if (dr["dheyaEmail"].ToString() != "No")
                                {
                                    flag = true;
                                    Response.Redirect("home1.aspx", false);   
                                }
                               
                                else
                                {
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-danger";
                                    div_msg.InnerText = "Something Wrong .... Please Try Again.";
                                }
                            }
                            else
                            {
                                div_msg.Visible = true;
                                div_msg.Attributes["class"] = "alert alert-danger";
                                div_msg.InnerText = "Your Registration is not Confirmed... Please confirm your registration first.";
                            }
                        }
                        dr.Close();
                    }
                    else
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-danger";
                        div_msg.InnerText = "Wrong Username or Password....Please Try Again.";
                    }

                    if (flag == true)
                    {
                        string str2 = "insert into tblLog (uId,log_type,log_time) values ( '" + Convert.ToInt32(Session["uid"]) + "','in','" + DateTime.Now + "')";
                        SqlCommand cmd2 = new SqlCommand(str2, con);
                        int i = cmd2.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }

    protected void forgotpassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("forgotpassword.aspx", false);
    }

}