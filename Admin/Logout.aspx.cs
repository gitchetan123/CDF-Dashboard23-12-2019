using System;

public partial class Profile_Logout : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session.Clear();
            Session.Abandon();
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
        Response.Redirect("Default.aspx", false);
    }
}