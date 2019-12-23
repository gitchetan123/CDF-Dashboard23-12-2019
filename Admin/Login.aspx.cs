using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class Login1 : System.Web.UI.Page
{

    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            div_msg.Visible = false;
        }
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //string strcmd = "select * from tblAdminUsers where name = @uname and password = @pass";
                    string strcmd = "select * from tblAdminUsers where email = @uname and password = @pass";
                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    cmd.Parameters.AddWithValue("@uname", txt_Ausername.Text);
                    cmd.Parameters.AddWithValue("@pass", txt_Apassword.Text);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (dr.GetString(6) == "APR")
                        {
                            Session["adminuser_name"] = dr.GetString(1);
                            Session["adminuser_email"] = dr.GetString(4);
                            Session["adminuser_id"] = dr.GetInt32(0);
                            Session["type"] = dr.GetString(3);
                            Response.Redirect("AdminHome.aspx", false);
                        }
                        else
                        {
                            div_msg.Visible = true;
                            div_msg.InnerText = "Your Registration is not Confirmed... Please confirm your registration first.";
                        }
                    }
                    else
                    {
                        // div_msg.Visible = true;
                        //div_msg.InnerText = "Wrong Username or Password.... Try Again.";

                        dr.Close();
                        string strexe = "select * from tblExecutive where exeEmail = @uname and password = @pass";
                        SqlCommand cmdexe = new SqlCommand(strexe, con);
                        cmdexe.Parameters.AddWithValue("@uname", txt_Ausername.Text);
                        cmdexe.Parameters.AddWithValue("@pass", txt_Apassword.Text);
                        SqlDataReader drexe = cmdexe.ExecuteReader();
                        if (drexe.HasRows)
                        {
                            drexe.Read();
                            if (drexe["status"].ToString() == "ACTIVE")
                            {
                                Session["executiveName"] = drexe["exeName"].ToString();
                                Session["executiveEmail"] = drexe["exeEmail"].ToString();
                                Response.Redirect("~/Admin/Executive/executive-home.aspx", false);
                            }
                            else
                            {
                                div_msg.Visible = true;
                                div_msg.InnerText = "Your Registration is not Confirmed... Please confirm your registration first.";
                            }
                        }
                        else
                        {
                            div_msg.Visible = true;
                            div_msg.InnerText = "Wrong Username or Password.... Try Again.";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            Response.Redirect("Default.aspx");
        }
    }





}