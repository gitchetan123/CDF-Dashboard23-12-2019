using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class cashpayment : System.Web.UI.Page
{
    db_context dbContext = new db_context();
    data_context dataContext = new data_context();
    int uid;

    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    // Get user in using QueryString 
        //    uid = Convert.ToInt32(Request.QueryString["id"]);

        //    if (!IsPostBack)
        //    {
        //        BindGridView();
        //    }

        //}
        //catch (Exception ex)
        //{
        //    Log.Error(ex);
        //}

        try
        {
            uid = Convert.ToInt32(Request.QueryString["id"]);
        }
        catch (Exception)
        {  
        }
        

        if (!IsPostBack)
        {
            try
            {
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string strcmd = "select fname+' ' + lname from tblUserMaster where uid = " + uid + "";
                    SqlCommand cmd3 = new SqlCommand(strcmd, connection);
                    lbl_name.Text = " &nbsp" + cmd3.ExecuteScalar().ToString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerHtml = "Something went wrong. Please try again......";
            }

            BindGridView();
        }

    }

    private void BindGridView()
    {
        try
        {
            //Select details id in tblUserMaster table
            string strcmd = "select id,productInfo,upper(txnId) as txnId,payDate, amount, status,paymentgateway from tblPayment where uId='" + uid + "'";
            //create a dataset object and fill it 
            DataSet ds = dbContext.ExecDataSet(strcmd);

            grid_Pyment.DataSource = ds;
            grid_Pyment.DataBind();

        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_msg.Visible = true;
            div_msg.InnerHtml = "Something went wrong. Please try again......";

        }

    }

    protected void btn_payment_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
            {
                connection.Open();
                int count = 0;
                try
                {
                    string strcmd1 = "insert into tblPayment (uid,amount,payDate,txnId,prodId,productInfo,payuString,paymentgateway,status) " +
                        "values (@uid,@amount,@payDate,@txnId,@prodId,@productInfo,@payuString,@paymentgateway,'Success')";
                    SqlCommand cmd = new SqlCommand(strcmd1, connection);
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@amount", txt_amount.Text);
                    cmd.Parameters.AddWithValue("@txnId", txt_TransactionID.Text);
                    cmd.Parameters.AddWithValue("@prodId", 7);
                    cmd.Parameters.AddWithValue("@productInfo", "CDF Training");
                    cmd.Parameters.AddWithValue("@payuString", txt_details.Text + "data entered :-" + DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@paymentgateway", "Cash");

                    string dt = dataContext.DateConvert(txt_paymentDate.Text);
                    cmd.Parameters.AddWithValue("@payDate", dt);

                    count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        BindGridView();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }
    }
}