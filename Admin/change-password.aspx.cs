using System;
using log4net;
using System.Configuration;
using System.Data.SqlClient;

//********************************************************************************************
//PageName        : change password   
//Description     : This page is user wand to change password
//AddedBy         : Bahubali                   AddedOn   : 24/05/2017
//UpdatedBy       :                            UpdatedOn : 
//Reason          :
//*******************************************************************************************

public partial class Profile_Change_password : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //check post back event occer 
                div_Error.Visible = false;
                div_success.Visible = false;

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_Error.Visible = true;
                div_success.Visible = false;
                Response.Redirect("Login.aspx", false);
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //get old password 
                string strcmd = "select password from tblAdminUsers where name = '" + Session["adminuser_name"].ToString() + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(strcmd, con);
                string oldPass = cmd.ExecuteScalar().ToString();

                //check old password is match or not 
                if (IsValid && tbOldPassword.Text == oldPass)
                {
                    //if old password match new password id update
                    strcmd = "UPDATE tblAdminUsers SET password = '" + tbNewPassword.Text + "' where name = '" + Session["adminuser_name"].ToString() + "'";
                    cmd = new SqlCommand(strcmd, con);
                    int i = cmd.ExecuteNonQuery();

                    div_success.Visible = true;
                    div_Error.Visible = false;

                }
                else
                {
                    //if not match old password print error msg
                    div_Error.Visible = true;
                    div_success.Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_Error.Visible = true;
            div_success.Visible = false;
            Response.Redirect("Login.aspx", false);
        }
    }
}