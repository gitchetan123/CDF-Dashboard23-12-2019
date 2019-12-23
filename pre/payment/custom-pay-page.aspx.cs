using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public partial class pre_payment_custom_pay_page : System.Web.UI.Page
{
    //create a object Db_context class for database connecton and database related operation
    db_context dc = new db_context();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string orderId, usename, email, contact, razorkey11 = ConfigurationManager.AppSettings["razorKey"].ToString();
    public int price;

    public void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            //Check sessions of user
            if (Session["uid"] != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //Select id, status, amount and contact number of user from tblCustomPayment and tblUserMaster tables
                    string query = "Select A.id,B.fname, B.email,A.status,A.amount, B.contactNo from tblCustomPayment as A Inner Join tblUserMaster as B on A.uId = B.uId where A.uId = '" + Session["uid"].ToString() + "' and A.status = 'ACTIVE'";
                    // SqlDataReader dr = conn.ExecDataReader(query);
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    //Check if table has rows for required query
                    if (dr.HasRows)
                    {
                        dr.Read();
                        //div_payment.Visible = true;
                        // Response.Redirect("~/pre/payment.aspx", false);

                        //Session["CustPayId"] = dr["id"].ToString();
                        Session["userName"] = dr["fname"].ToString();
                        Session["email"] = dr["email"].ToString();
                        contact = dr["contactNo"].ToString();
                        //price = (Convert.ToInt32(dr["amount"])) * 100;

                        price = (Convert.ToInt32(Session["CustomPaymentAmount"].ToString())) * 100;

                        dr.Close();
                        //string query_product = "Select price from tblProductMaster where pId = 7";
                        //cmd = new SqlCommand(query_product, conn);

                        //price = Convert.ToInt32(cmd.ExecuteScalar()) * 100;

                        //Razorpay Code
                        Dictionary<string, object> input = new Dictionary<string, object>();
                        input.Add("amount", price); // this amount should be same as transaction amount
                        input.Add("currency", "INR");
                        input.Add("receipt", "12121");
                        input.Add("payment_capture", 1);

                        //Test Acc Details
                        //string key = "rzp_test_ERVr7EOp6kqmYn";
                        //string secret = "4vindSSP6XFvOHwRybpx1yPP";
                        string key = ConfigurationManager.AppSettings["razorKey"].ToString();
                        string secret = ConfigurationManager.AppSettings["razorSecret"].ToString();

                        //Orignal Acc Details
                        //string key = "rzp_live_pAKEJ7HxaRkA5W";
                        //string secret = "4jKCObLVsGxZ0NaSHJyV348U";

                        RazorpayClient client = new RazorpayClient(key, secret);
                        Razorpay.Api.Order order = client.Order.Create(input);
                        orderId = order["id"].ToString();
                        usename = Session["userName"].ToString();
                        email = Session["email"].ToString();
                        Session["amount"] = price;

                        //Select status, prodId coulmns from tblPayment table
                        string query_payment = "Select status,prodId from tblPayment where uId = '" + Session["uid"].ToString() + "' and prodId=7";
                        cmd = new SqlCommand(query_payment, conn);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            // grid();

                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}