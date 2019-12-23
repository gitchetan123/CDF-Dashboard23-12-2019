using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CDF_AdvanceLevelTraining : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    public string orderId, usename, email, contact, razorkey11 = ConfigurationManager.AppSettings["razorKey"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Check sessions of user
                if (Session["uid"] != null)
                {
                    gv_AdvanceLevel();
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
    }

    public void gv_AdvanceLevel()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = " select * from tblProductmaster where pid=19 ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        gvAdvanceLevel.DataSource = dt;
                        gvAdvanceLevel.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void gvAdvanceLevel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "pay")
            {
                string args = e.CommandArgument.ToString();
                string[] arg = args.Split(',');
                string Amount = arg[0];
                string Pid = arg[1];
                Session["AdvanceLevelTrainigPayAmt"] = Amount;
                Session["AdvanceLevelTrainigPid"] = Pid;
                Response.Redirect("~/CDF/AdvanceLevelTrainingPayment.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    protected void gvAdvanceLevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string status = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query_payment = "Select status,prodId from tblPayment where uId = '" + Session["uid"].ToString() + "' and prodId=19 ";
                SqlCommand cmd = new SqlCommand(query_payment, con);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        status = sdr["status"].ToString();
                        Session["status"] = status;
                    }
                }
                con.Close();
                if (status == "Success")
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Button btnPayemt = (Button)e.Row.FindControl("btn_pay_Advance_fix");

                        //    string status = e.Row.Cells[2].Text;
                        btnPayemt.Enabled = false;
                    }
                }
                else
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        Button btnPayemt = (Button)e.Row.FindControl("btn_pay_Advance_fix");

                        //    string status = e.Row.Cells[2].Text;
                        btnPayemt.Enabled = true;
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