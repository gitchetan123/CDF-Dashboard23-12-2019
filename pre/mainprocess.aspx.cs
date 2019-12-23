using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace RazorpaySampleApp
{
    public partial class mainprocess : System.Web.UI.Page
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        int productId = 7;
        //create a object Db_context class for database connecton and database related operation
        db_context dc = new db_context();
        data_context datacontext = new data_context();
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Session["uid"].ToString();
            string paymentId = Request.Form["razorpay_payment_id"];

            //Test Acc Details
            //string key = "rzp_test_ERVr7EOp6kqmYn";
            //string secret = "4vindSSP6XFvOHwRybpx1yPP";

            //Original Acc Details
            string key = ConfigurationManager.AppSettings["razorKey"].ToString();
            string secret = ConfigurationManager.AppSettings["razorSecret"].ToString();

            RazorpayClient client = new RazorpayClient(key, secret);

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", paymentId);
            attributes.Add("razorpay_order_id", Request.Form["razorpay_order_id"]);
            attributes.Add("razorpay_signature", Request.Form["razorpay_signature"]);
            //Response.Write("Payment Successful");
            Utils.verifyPaymentSignature(attributes);

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
            string razorstring1 = JsonConvert.SerializeObject(payment);
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))

                {
                    ProductMaster product = GetProduct(productId); //razorPayment.prodid

                    //Capture a payment

                    int amount = Convert.ToInt32(product.price);

                    if (amount > 0 && paymentId != null)
                    {
                        //Insert user's payment details in tblPayment table
                        string strcmd = "INSERT INTO tblPayment(uId,amount,payDate,txnId,prodId,productInfo,status,payuString,paymentgateway) VALUES (@uId,@amount,@payDate,@txnId,@prodId,@productInfo,@status,@payuString,@paymentgateway)";

                        SqlCommand cmd = new SqlCommand(strcmd, con);

                        cmd.Parameters.AddWithValue("@uId", uid);

                        cmd.Parameters.AddWithValue("@amount", amount);

                        cmd.Parameters.AddWithValue("@payDate", DateTime.Now);

                        cmd.Parameters.AddWithValue("@txnId", paymentId);

                        cmd.Parameters.AddWithValue("@prodId", productId);

                        cmd.Parameters.AddWithValue("@productInfo", product.prodName);

                        cmd.Parameters.AddWithValue("@status", "Success");

                        cmd.Parameters.AddWithValue("@payuString", razorstring1);

                        cmd.Parameters.AddWithValue("@paymentgateway", "Razorpay");

                        con.Open();

                        int intEffectedRows = cmd.ExecuteNonQuery();
                        Response.Redirect("payment-success.aspx", false);


                        string strdata = "select fname,contactNo, email FROM tblUserMaster where uId = '" + uid + "'";
                        cmd = new SqlCommand(strdata, con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            //call to Templete file for email body
                            string body = this.PopulateBody(dr["fname"].ToString(), amount.ToString());

                            //Send email
                            var task = new Thread(() => datacontext.sendemail(dr["email"].ToString(), null, null, ConfigurationManager.AppSettings["CDFPaymentEmailTemplateSubject"], body));
                            task.Start();

                            string SMSText = ConfigurationManager.AppSettings["CDFPaymentSmsTemplate"].ToString();
                            SMSText = SMSText.Replace("{CDF}", dr["fname"].ToString());
                            SMSText = SMSText.Replace("{}", amount.ToString());
                            dc.sendSms(dr["contactNo"].ToString(), SMSText);


                        }
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }


        private string PopulateBody(string userName,string amount)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["CDFPaymentEmailTemplatePath"])))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", userName);
                body = body.Replace("{amount}", amount);              

                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ProductMaster GetProduct(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Select product details from tblProductMaster table
                    SqlCommand cmd = new SqlCommand("select * from tblProductMaster where status='DEACTIVE' and pId=" + productId, con);

                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)

                    {
                        sdr.Read();

                        ProductMaster product = new ProductMaster();

                        product.pId = Convert.ToInt32(sdr["pId"]);

                        product.prodName = sdr["prodName"].ToString();

                        product.shortDescription = sdr["shortDescription"].ToString();

                        product.description = sdr["description"].ToString();

                        product.price = Convert.ToInt32(sdr["price"]);

                        product.prodImg = ConfigurationManager.AppSettings["Productimgurl"] + sdr["mobImg"].ToString();

                        product.prodDetailImg = ConfigurationManager.AppSettings["Productimgurl"] + sdr["prodImg"].ToString();

                        product.tagline = sdr["tagline"].ToString();

                        product.mobImg = null;// ImageToByteArray(ConfigurationManager.AppSettings["imgurl"] + sdr["mobImg"].ToString());

                        product.createDate = Convert.ToDateTime(sdr["createDate"].ToString());

                        sdr.Close();

                        return product;
                    }
                    else

                        return null;
                }
            }
            catch (Exception ex)

            {
                Log.Error("" + ex);
                return null;

            }
        }
    }

}