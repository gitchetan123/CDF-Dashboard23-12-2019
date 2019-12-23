using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.UI.WebControls;

public partial class pre_payment_pay_page : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    public string orderId, usename, email, contact, razorkey11 = ConfigurationManager.AppSettings["razorKey"].ToString();
    public int price;

    string DecodedData = null;

    public string DecryptID(string uId)
    {
        string Data = uId;
        System.Text.UTF8Encoding oUTF8Encoding = new System.Text.UTF8Encoding();
        System.Text.Decoder oDecoder = oUTF8Encoding.GetDecoder();
        byte[] oByte = Convert.FromBase64String(Data);
        int CharCount = oDecoder.GetCharCount(oByte, 0, oByte.Length);
        char[] DecodedChar = new char[CharCount];
        oDecoder.GetChars(oByte, 0, oByte.Length, DecodedChar, 0);
        DecodedData = new String(DecodedChar);

        return DecodedData;
    }
    public void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            try
            {
                string Data = Request.QueryString["uId"].ToString();

                DecryptID(Data);

                Session["uid"] = DecodedData;

                check_status();
                //Check sessions of user
                if (Session["uid"] != null)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        // Full Payment with discount
                        string str_full = "select pId as id, (price - 1500) as amount, price from tblProductMaster where pId = 7";
                        SqlDataAdapter da1_full = new SqlDataAdapter(str_full, conn);
                        DataSet ds1_full = new DataSet();
                        da1_full.Fill(ds1_full);

                        if (ds1_full.Tables[0].Rows.Count > 0)
                        {
                            gvCustomPay_full.DataSource = ds1_full;
                            gvCustomPay_full.DataBind();
                        }
                        else
                        {
                            gvCustomPay_full.DataSource = null;
                            gvCustomPay_full.DataBind();
                        }

                        // Advance Fix Payment
                        string str_advance_fix = "select pId as id, (price-20000) as amount from tblProductMaster where pId = 7";
                        SqlDataAdapter da1_advance_fix = new SqlDataAdapter(str_advance_fix, conn);
                        DataSet ds1_advance_fix = new DataSet();
                        da1_advance_fix.Fill(ds1_advance_fix);

                        if (ds1_advance_fix.Tables[0].Rows.Count > 0)
                        {
                            gvCustomPay_advance_fix.DataSource = ds1_advance_fix;
                            gvCustomPay_advance_fix.DataBind();
                        }
                        else
                        {
                            gvCustomPay_advance_fix.DataSource = null;
                            gvCustomPay_advance_fix.DataBind();
                        }

                        // Balance Payment
                        string str_balance = " select id, (amount-15000) as amount from tblCustomPayment where uid = '" + Session["uid"].ToString() + "' ";
                        SqlDataAdapter da1_balance = new SqlDataAdapter(str_balance, conn);
                        DataSet ds1_balance = new DataSet();
                        da1_balance.Fill(ds1_balance);

                        if (ds1_balance.Tables[0].Rows.Count > 0)
                        {
                            gvCustomPay_balance.DataSource = ds1_balance;
                            gvCustomPay_balance.DataBind();
                        }
                        else
                        {
                            gvCustomPay_balance.DataSource = null;
                            gvCustomPay_balance.DataBind();
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }

    public void check_status()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Select status and product id from tblPayment table
                string query_payment = "Select status,prodId,amount from tblPayment where uId = '" + Session["uid"].ToString() + "' and prodId=7";
                SqlCommand cmd = new SqlCommand(query_payment, connection);
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                
                if (sdr.HasRows)
                {
                    sdr.Read();
                    if (sdr["amount"].ToString() == "33500")
                    {
                        Panel_Balance.Enabled = false;
                        Panel_full.Enabled = false;
                        Panel_Advance.Enabled = false;
                    }
                    else if (sdr["amount"].ToString() == "15000")
                    {
                        Panel_full.Enabled = false;
                        Panel_Advance.Enabled = false;
                        Panel_Balance.Enabled = true;
                    }
                    else
                    {
                        Panel_Balance.Enabled = false;
                        Panel_full.Enabled = false;
                        Panel_Advance.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
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

                    Response.Redirect("~/pre/payment/custom-pay-page.aspx", false);
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
}