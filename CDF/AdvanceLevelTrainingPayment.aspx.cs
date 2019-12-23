using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CDF_AdvanceLevelTrainingPayment : System.Web.UI.Page
{
    db_context dc = new db_context();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string orderId, usename, email, contact, razorkey11 = ConfigurationManager.AppSettings["razorKey"].ToString();
    public int price;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Check sessions of user
            if (Session["uid"] != null)
            {
                string query = null;
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string Amount = Session["AdvanceLevelTrainigPayAmt"].ToString();
                    string Pid = Session["AdvanceLevelTrainigPid"].ToString();
                    if (Amount == "3000")
                    {
                        query = "Select contactNo from tblUserMaster where uId = '" + Session["uid"].ToString() + "' ";


                        // SqlDataReader dr = conn.ExecDataReader(query);
                        SqlCommand cmd = new SqlCommand(query, conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            div_payment.Visible = true;
                            contact = dr["contactNo"].ToString();
                            //price = (Convert.ToInt32(dr["amount"])) * 100;

                            price = (Convert.ToInt32(Amount)) * 100;

                            dr.Close();

                            //Razorpay Code

                            //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                            string key = ConfigurationManager.AppSettings["razorKey"].ToString();
                            string secret = ConfigurationManager.AppSettings["razorSecret"].ToString();

                            Dictionary<string, object> input = new Dictionary<string, object>();
                            input.Add("amount", price); // this amount should be same as transaction amount
                            input.Add("currency", "INR");
                            input.Add("receipt", "12121");
                            input.Add("payment_capture", 1);

                            RazorpayClient client = new RazorpayClient(key, secret);
                            Razorpay.Api.Order order = client.Order.Create(input);
                            orderId = order["id"].ToString();
                            usename = Session["userName"].ToString();
                            email = Session["email"].ToString();
                            Session["amount"] = price;
                            Session["Pid"] = Pid;
                            //Select status, prodId coulmns from tblPayment table
                            //string query_payment = "Select status,prodId from tblPayment where uId = '" + Session["uid"].ToString() + "' and prodId='" + Pid + "'";
                            //cmd = new SqlCommand(query_payment, conn);
                            //SqlDataReader sdr = cmd.ExecuteReader();
                            //if (sdr.HasRows)
                            //{
                            //    while (sdr.Read())
                            //    {
                            //        string status = sdr["Success"].ToString();
                            //        Session["status"] = status;
                            //    }
                            //}
                            //else
                            //{

                            //}
                        }
                    }
                    else
                    {
                        div_payment.Visible = false;
                        Response.Redirect("~/home1.aspx", false);
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
            Response.Redirect("~/login.aspx", false);
        }
    }
}