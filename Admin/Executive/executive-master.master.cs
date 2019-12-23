using System;

public partial class Executivemaster : System.Web.UI.MasterPage
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["executiveName"] != null)
            {
                    lbl_username.Text = Session["executiveName"].ToString();
                    lbl_username2.Text = Session["executiveName"].ToString();
                    Image1.ImageUrl = "~/images/Avatar.png";
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
