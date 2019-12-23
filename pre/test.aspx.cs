

//********************************************************************************************************
//PageName        : Brochure 
//Description     : This page is Create authcode and provide username and password and auto register 
//AddedBy         : Nitish                   AddedOn   : 18/02/2017
//Reason          : *****
//********************************************************************************************************

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

public partial class test : System.Web.UI.Page
{
    //create a object Db_context class for database connecton and database related operation
    db_context dc = new db_context();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    int prodid = 7;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check sessions of user
            if (Session["uid"] != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Check if gender, dob and cityid null or not not in tblUserMaster table
                    SqlCommand cmd = new SqlCommand("select count(uid) from tblUserMaster where gender is null and dob is null and cityid is null and uid=" + Session["uid"].ToString(), connection);
                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        div_msg.Visible = false;
                    }
                    else
                    {
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerText = "Successfully registered for test.";
                    }
                    //Check if user has completed test or not
                    string query1 = "SELECT COUNT(id) FROM tblUserProductMaster where uId='" + Session["uid"].ToString() + "' and prodid = " + 7 + " and teststatus = 'Complete'";
                    cmd = new SqlCommand(query1, connection);
                    int count1 = Convert.ToInt32((cmd.ExecuteScalar()));
                    if (count1 == 0)
                    {
                        div_TakeTest.Visible = true;
                    }
                    else
                    {
                        div_TakeTest.Visible = false;
                        div_msg.Visible = true;
                        div_msg.Attributes["class"] = "alert alert-success";
                        div_msg.InnerText = "You already completed your test";
                    }
                }
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }

    }

    protected void btn_test_Click(object sender, EventArgs e)
    {
        if (Session["uid"] != null)
        {
            try
            {
                string pid = "" + 7;
                string batid = "" + 3;
                //Check if user's gender, dob and cityid null or not in 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select count(uid) from tblUserMaster where gender is null and dob is null and cityid is null and uid=" + Session["uid"].ToString(), connection);
                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        Response.Redirect("RegUpdate.aspx", false);
                    }
                    else
                    {
                        //Check uid and product from tblUserProductMaster table
                        cmd = new SqlCommand("select count(id) from tblUserProductMaster where Uid=" + Session["uid"].ToString() + " and prodid=" + pid + "", connection);
                        int id = Convert.ToInt32(cmd.ExecuteScalar());
                        if (id == 0)
                        {
                            if (GenerateAuthcode(Convert.ToInt32(Session["uid"]), prodid))
                                Response.Redirect("~/cdf-test/Test/assessment.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("~/cdf-test/Test/assessment.aspx", false);
                        }
                    }
                    cmd.Dispose();
                }
                Session["batid"] = batid;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    private bool GenerateAuthcode(int uId, int prodid)
    {
        try
        {
            string authcode = GenerateRandomString(4) + GenerateRandomNo() + DateTime.Now.Millisecond.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //Insert prodid, authcode, date and status in tblAuthcode table
                string strcmd = "INSERT INTO tblAuthcode(prodid,authcode,date,status) VALUES (@prodid,@authcode,@date,@status)";

                SqlCommand cmd = new SqlCommand(strcmd, con);

                cmd.Parameters.AddWithValue("@prodid", prodid);

                cmd.Parameters.AddWithValue("@authcode", authcode);

                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                cmd.Parameters.AddWithValue("@status", "APR");

                con.Open();

                int intEffectedRows = cmd.ExecuteNonQuery();

                if (intEffectedRows > 0)

                {
                    //Insert uId,prodid,authcode,Status and dateofpurchase in tblUserProductMaster table
                    string strcmd1 = "INSERT INTO tblUserProductMaster(uId,prodid,authcode,Status,dateofpurchase) VALUES (@uId,@prodid,@authcode,@Status,@dateofpurchase)";

                    cmd = new SqlCommand(strcmd1, con);

                    cmd.Parameters.AddWithValue("@uId", uId);

                    cmd.Parameters.AddWithValue("@prodid", prodid);

                    cmd.Parameters.AddWithValue("@authcode", authcode);

                    cmd.Parameters.AddWithValue("@Status", "APR");

                    cmd.Parameters.AddWithValue("@dateofpurchase", DateTime.Now);

                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }
    }

    private string GenerateRandomString(int size)

    {
        try
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            StringBuilder builder = new StringBuilder();

            char ch;

            for (int i = 0; i < size; i++)

            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));

                builder.Append(ch);
            }

            return builder.ToString().ToUpper();
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);

            return null;
        }
    }

    //Generate Random number
    private int GenerateRandomNo()
    {
        Random _rdm = new Random();

        return _rdm.Next(1000, 9999);
    }
}