using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class executive_ExecutiveHome : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["executiveName"] != null)
            {
                string constr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    DataTable dt2 = new DataTable();
                    string queryExeRepCount = "SELECT distinct ex.exeName as exeName, isnull(vr.TotalTestSent,0)as TotalTestSent, isnull(ups.TestComplete,0) as TestComplete,isnull(pm.PaymentComplete,0) as PaymentComplete, CONVERT(decimal,isnull((PaymentComplete*100.00/TestComplete),0)) as ConversionRate " +
                    "FROM tblExecutive as ex " +
                    "LEFT OUTER JOIN(select executiveId, count(email) as TotalTestSent from tblVerifyRegistration group by executiveId) as vr on ex.id = vr.executiveId " +
                    "left outer join tblVerifyRegistration as v on ex.id = v.executiveId " +
                    "left outer join tblUserMaster as u on v.email = u.email " +
                    "left outer join(select executiveId, COUNT(upm.uId) as TestComplete from tblVerifyRegistration as vrs inner join tblUserMaster as u on vrs.email = u.email " +
                    "inner join tblUserProductMaster as upm on u.uId = upm.uId group by vrs.executiveId, upm.teststatus having upm.teststatus = 'Complete') as ups on ex.id = ups.executiveId " +
                    "left outer join(select executiveId, COUNT(p.uId) as PaymentComplete from tblVerifyRegistration as vrs inner join tblUserMaster as u on vrs.email = u.email " +
                    "inner join tblPayment as p on u.uId = p.uId group by vrs.executiveId) as pm on ex.id = pm.executiveId where ex.exeEmail = '" + Session["executiveEmail"].ToString() + "'";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(queryExeRepCount, con);
                    SqlDataReader dr1 = cmd.ExecuteReader();
                    //Check if table has rows for required query
                    if (dr1.HasRows)
                    {
                        dr1.Read();
                        test_sent.Text = Convert.ToString(dr1["TotalTestSent"]);
                        test_completed.Text = Convert.ToString(dr1["TestComplete"]);
                        payment_recieved.Text = Convert.ToString(dr1["PaymentComplete"]);
                        conversion_rate.Text = Convert.ToString(dr1["ConversionRate"]);
                        //Int32 s = dr1.GetInt32(0);
                        //leads_count.Text = " " + s;
                        //session_count.Text = " " + s * 0;
                        //revenue_count.Text = "" + s * 0;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Admin/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}