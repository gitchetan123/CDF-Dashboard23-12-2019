using System;
using System.Data.SqlClient;

public partial class Profile_Change_password : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context  class for database related method.
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"] != null && Session["dheyaEmail"] != null)
        {
            if (!IsPostBack)
            {
                div_Error.Visible = false;
                div_success.Visible = false;

                try
                {
                    //Check user's password in tblUserMaster table
                    string strcmd = "select password from tblUserMaster where uid = '" + Session["uid"].ToString() + "'";
                    Hf_password.Value = dbContext.ExecScal(strcmd);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null && Session["dheyaEmail"] != null)
            {
                if (IsValid && tbOldPassword.Text == Hf_password.Value)
                {
                    //Update password of respective user in tblUserMaster table
                    string strcmd = "update tblUserMaster set  password=  '" + tbNewPassword.Text + "' where uid = '" + Session["uid"].ToString() + "'";
                    int i = dbContext.ExecNonQuery(strcmd);
                    div_success.Visible = true;
                    div_Error.Visible = false;
                }
                else
                {
                    div_Error.Visible = true;
                    div_success.Visible = false;
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
        }
    }
}