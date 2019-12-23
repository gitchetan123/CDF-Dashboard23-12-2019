using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CDF_bank_details : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                read_data();
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }
    }

    protected void btn_edit_update_Click(object sender, EventArgs e)
    {
        if (btn_edit_update.Text == "Edit")
        {
            pan.Enabled = true;
            btn_edit_update.Text = "Update";
        }
        else
        {
            if (IsValid)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string strcmd = "SELECT uId from tblUserBankDetails WHERE uId = " + Session["uid"] + "";

                        SqlCommand cmd = new SqlCommand(strcmd, con);
                        con.Open();
                        SqlDataReader dr1 = cmd.ExecuteReader();
                        
                        if (dr1.HasRows)
                        {
                            dr1.Read();
                            int cid = Convert.ToInt32(dr1["uId"]);
                            dr1.Close();
                            cmd.Dispose();

                            string StrQuery2 = "";
                            StrQuery2 = "Update tblUserBankDetails SET accountHolderName = '" + txt_accountHolderName.Text.Trim().ToString() + "',"
                               + "accountNumber = '" + txt_accountNumber.Text.Trim().ToString() + "',bankName = '" + txt_bankName.Text.Trim().ToString() + "',"
                               + "branchName = '" + txt_branchName.Text.Trim().ToString() + "',ifscNo = '" + txt_ifscNo.Text.Trim().ToString() + "'"
                               + " where uId = " + Session["uid"] + "";
                            //SqlCommand cmd1 = new SqlCommand(StrQuery2, con);
                            // con.Open();
                            int k = dbContext.ExecNonQuery(StrQuery2);
                            if (k != 0)
                            {
                                pan.Enabled = false;
                                btn_edit_update.Text = "Edit";
                            }
                        }
                        else
                            {
                                string StrQuery = "";
                                //update details in tblUserDetails table
                                StrQuery = "";
                                StrQuery = "insert into tblUserBankDetails (uId,accountHolderName,accountNumber,bankName,branchName,ifscNo) Values (" + Session["uid"] + ",'" + txt_accountHolderName.Text.Trim().ToString() + "'"
                                            + ",'" + txt_accountNumber.Text.Trim().ToString() + "','" + txt_bankName.Text.Trim().ToString() + "','" + txt_branchName.Text.Trim().ToString() + "','" + txt_ifscNo.Text.Trim().ToString() + "')";
                                int i = dbContext.ExecNonQuery(StrQuery);

                                //if update success is show msg
                                if (i != 0)
                                {
                                    pan.Enabled = false;
                                    btn_edit_update.Text = "Edit";
                                }
                            }

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalUpdate", "$('#myModalUpdate').modal();", true);
                    }
                    
                }
                catch (Exception ex)
                {
                    Log.Error("" + ex);
                }
            }
        }
    }


    void read_data()
    {
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //Get all user details 
                    string strcmd = "SELECT accountHolderName,accountNumber,bankName,branchName,ifscNo from tblUserBankDetails WHERE uId = '" + Session["uid"] + "'";

                    SqlCommand cmd = new SqlCommand(strcmd, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();

                        {
                            txt_accountHolderName.Text = dr["accountHolderName"].ToString();
                            txt_accountNumber.Text = dr["accountNumber"].ToString();
                            txt_bankName.Text = dr["bankName"].ToString();
                            txt_branchName.Text = dr["branchName"].ToString();
                            txt_ifscNo.Text = dr["ifscNo"].ToString();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
            }
        }
    }
}