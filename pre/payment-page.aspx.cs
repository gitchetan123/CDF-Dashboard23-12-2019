using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.UI.WebControls;

public partial class pre_payment : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    public string orderId, usename, email, contact, razorkey11 = ConfigurationManager.AppSettings["razorKey"].ToString();
    public int price;
    public void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Check sessions of user
                if (Session["uid"] != null)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        bool flag = false;
                        //Check status of user
                        string querycustpay = "Select status from tblCustomPayment where uId = '" + Session["uid"].ToString() + "' and status='ACTIVE' and approve=1";
                        SqlCommand cmd = new SqlCommand(querycustpay, conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            flag = true;
                            div_custompay.Visible = true;
                            // div_payment.Visible = false;
                        }
                        else
                        {
                            div_custompay.Visible = false;
                            //div_payment.Visible = true;
                        }
                        dr.Close();

                        if (flag == true)
                        {
                            string str = "select id, amount from tblCustomPayment where status='ACTIVE' and approve=1 and uid='" + Session["uid"].ToString() + "'";
                            SqlDataAdapter da1 = new SqlDataAdapter(str, conn);
                            DataSet ds1 = new DataSet();
                            da1.Fill(ds1);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                gvCustomPay.DataSource = ds1;
                                gvCustomPay.DataBind();
                            }
                            else
                            {
                                gvCustomPay.DataSource = null;
                                gvCustomPay.DataBind();
                            }
                        }
                    }
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        // div_custompay.Visible = false;
                        //Check if user has completed test or not
                        string query_test = "SELECT COUNT(id) FROM tblUserProductMaster where uId='" + Session["uid"].ToString() + "' and prodid = " + 7 + " and teststatus = 'Complete'";
                        SqlCommand cmd;
                        cmd = new SqlCommand(query_test, conn);
                        conn.Open();
                        int count = Convert.ToInt32((cmd.ExecuteScalar()));
                        if (count == 0)
                        {
                            div_msg.Visible = true;
                            div_status.Visible = false;
                            div_payment.Visible = false;
                            div_custompay.Visible = false;
                            div_msg.Attributes["class"] = "alert alert-danger";
                            div_msg.InnerText = "Please complete your test first";
                        }
                        else
                        {
                            //Check user's status and ndaStatus           
                            string query = "Select A.ndaStatus,B.status, B.contactNo from tblUserDetails as A Inner Join tblUserMaster as B on A.uId = B.uId  where A.uId = '" + Session["uid"].ToString() + "'";
                            cmd = new SqlCommand(query, conn);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                dr.Read();
                                if (dr["status"].ToString() == "APPROVED")
                                {
                                    if (dr["ndaStatus"].ToString() == "Agree")
                                    {
                                        div_msg.Visible = false;
                                        div_status.Visible = false;
                                        div_payment.Visible = true;
                                        div_custompay.Visible = true;
                                    }
                                    else
                                    {
                                        div_msg.Visible = true;
                                        div_status.Visible = false;
                                        div_payment.Visible = false;
                                        div_custompay.Visible = false;
                                        div_msg.Attributes["class"] = "alert alert-danger";
                                        div_msg.InnerText = "Please update your profile first";
                                    }
                                    contact = dr["contactNo"].ToString();
                                    dr.Close();
                                    string query_product = "Select price from tblProductMaster where pId = 7";
                                    cmd = new SqlCommand(query_product, conn);

                                    price = Convert.ToInt32(cmd.ExecuteScalar()) * 100;
                                    //Razorpay Code
                                    Dictionary<string, object> input = new Dictionary<string, object>();
                                    input.Add("amount", price); // this amount should be same as transaction amount
                                    input.Add("currency", "INR");
                                    input.Add("receipt", "12121");
                                    input.Add("payment_capture", 1);

                                    // Rezorpay updated verson
                                    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                                    //Test Acc Details
                                    //string key = "rzp_test_ERVr7EOp6kqmYn";
                                    //string secret = "4vindSSP6XFvOHwRybpx1yPP";

                                    //Orignal Acc Details
                                    string key = ConfigurationManager.AppSettings["razorKey"].ToString();
                                    string secret = ConfigurationManager.AppSettings["razorSecret"].ToString();

                                    RazorpayClient client = new RazorpayClient(key, secret);
                                    Razorpay.Api.Order order = client.Order.Create(input);
                                    orderId = order["id"].ToString();
                                    usename = Session["userName"].ToString();
                                    email = Session["email"].ToString();
                                    //Select status and product id from tblPayment table
                                    string query_payment = "Select status,prodId from tblPayment where uId = '" + Session["uid"].ToString() + "' and prodId=7";
                                    cmd = new SqlCommand(query_payment, conn);
                                    SqlDataReader sdr = cmd.ExecuteReader();
                                    if (sdr.HasRows)
                                    {
                                        sdr.Read();
                                        grid();

                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    div_msg.Visible = true;
                                    div_msg.Attributes["class"] = "alert alert-warning";
                                    div_msg.InnerText = "Please wait until your profile get approved.";
                                    div_status.Visible = false;
                                    div_payment.Visible = false;
                                }
                            }
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
    private void grid()
    {
        try
        {
            div_status.Visible = true;
            div_payment.Visible = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query_paystatus = "Select productInfo 'Product Name',amount 'Amount',status 'Status',payDate 'Payment Date',prodId from tblPayment where uId='" + Session["uid"].ToString() + "' and prodId=7";
                SqlDataAdapter da = new SqlDataAdapter(query_paystatus, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                }
                else
                {
                    //DataTable dt = new DataTable();
                    //GridView1.DataSource = dt;
                    //GridView1.DataBind();

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);

        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        grid();
    }

    protected void gvCustomPay_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pay")
        {
            //string amount = e.CommandArgument.ToString();
            if (IsValid)
            {
                try
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string amount = commandArgs[0];
                    string id = commandArgs[1];

                    Response.Redirect("custom-payment.aspx", false);
                    Session["CustomPaymentAmount"] = amount;
                    Session["CustomPaymentid"] = id;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }
    }

    //protected void gvCustomPay_RowCommand1(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "pay")
    //    {
    //        string amount = e.CommandArgument.ToString();
    //        if (IsValid)
    //        {
    //            try
    //            {
    //                Response.Redirect("custom-payment.aspx", false);
    //                Session["Amount"] = amount;
    //            }
    //            catch (Exception ex)
    //            {
    //                Log.Error(ex);
    //            }
    //        }
    //    }
    //}

    //protected void gvCustomPay_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //}
}