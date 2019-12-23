using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.UI.WebControls;

public partial class pre_payment_page1 : System.Web.UI.Page
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
                        string querycustpay = "Select status from tblCustomPayment where uId = '" + Session["uid"].ToString() + "' and status in ('ACTIVE') and approve=1";
                        SqlCommand cmd = new SqlCommand(querycustpay, conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            flag = true;
                            Full_with_Discount();
                            Advance_fix();
                            Balance_payment();
                            //div_custompay.Visible = true;
                            // div_payment.Visible = false;
                        }
                        else
                        {
                            Full_with_Discount();
                            Advance_fix();
                            Balance_payment_default();
                            //div_custompay.Visible = false;
                            //div_payment.Visible = true;
                        }
                        dr.Close();

                        // Select status and product id from tblPayment table
                        string query_payment = "Select status,prodId,sum(amount) as amount from tblPayment where uId = '" + Session["uid"].ToString() + "' and prodId=7 group by status,prodId,uid ";
                        cmd = new SqlCommand(query_payment, conn);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            if (sdr["amount"].ToString() == "33500")
                            {
                                Panel_Balance.Enabled = false;
                                Panel_full.Enabled = false;
                                Panel_Advance.Enabled = false;
                                div_custompay.Visible = false;
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
                                div_custompay.Visible = false;
                            }
                            grid();
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

    public void Full_with_Discount()
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
        }
    }
    public void Advance_fix()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            // Advance Fix Payment
            string str_advance_fix = "select pId as id, (price-20000) as amount from tblProductMaster where pId = 7";
            SqlDataAdapter da1_advance_fix = new SqlDataAdapter(str_advance_fix, conn);
            DataSet ds1_advance_fix = new DataSet();
            da1_advance_fix.Fill(ds1_advance_fix);

            if (ds1_advance_fix.Tables[0].Rows.Count > 0)
            {
                gvCustomPay_Advance_fix.DataSource = ds1_advance_fix;
                gvCustomPay_Advance_fix.DataBind();
            }
            else
            {
                gvCustomPay_Advance_fix.DataSource = null;
                gvCustomPay_Advance_fix.DataBind();
            }
        }
    }
    public void Balance_payment_default()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            // Balance Payment
            string str_balance = " select pId as id, (price-15000) as amount from tblProductMaster where pId = 7 ";
            SqlDataAdapter da1_balance = new SqlDataAdapter(str_balance, conn);
            DataSet ds1_balance = new DataSet();
            da1_balance.Fill(ds1_balance);

            if (ds1_balance.Tables[0].Rows.Count > 0)
            {
                gvCustomPay_Balance.DataSource = ds1_balance;
                gvCustomPay_Balance.DataBind();
            }
            else
            {
                gvCustomPay_Balance.DataSource = null;
                gvCustomPay_Balance.DataBind();
            }
        }
    }
    public void Balance_payment()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            // Balance Payment
            string str_balance = " select id, (amount-15000) as amount from tblCustomPayment where uid = '"+ Session["uid"].ToString()+ "' And status='ACTIVE'";
            SqlDataAdapter da1_balance = new SqlDataAdapter(str_balance, conn);
            DataSet ds1_balance = new DataSet();
            da1_balance.Fill(ds1_balance);

            if (ds1_balance.Tables[0].Rows.Count > 0)
            {
                gvCustomPay_Balance.DataSource = ds1_balance;
                gvCustomPay_Balance.DataBind();
            }
            else
            {
                gvCustomPay_Balance.DataSource = null;
                gvCustomPay_Balance.DataBind();
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

                    Response.Redirect("~/pre/custom-payment.aspx", false);
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