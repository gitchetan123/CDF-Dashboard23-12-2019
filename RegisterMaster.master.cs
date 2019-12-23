using System;

public partial class RegisterMaster : System.Web.UI.MasterPage
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dc = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["userName"]) == "No ")
            {
                logindiv.Visible = false;
                manualregdiv.Visible = true;
                regdiv.Visible = false;
                lbl_username2.Text = Session["email"].ToString();
                Image1.ImageUrl = Session["formalImg"].ToString();
            }
            else
            {
                logindiv.Visible = true;
                manualregdiv.Visible = false;
                regdiv.Visible = true;
                Image1.ImageUrl = "~/images/Avatar.png";
            }
    }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}
