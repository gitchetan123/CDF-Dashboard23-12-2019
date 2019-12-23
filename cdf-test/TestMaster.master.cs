using System;
using System.Threading;
using System.Web.UI;
using log4net;

public partial class TestMaster : System.Web.UI.MasterPage
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }
            else
            {
               if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                {
                    Label1.Text = "आपले स्वागत आहे";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    Label1.Text = "आपका स्वागत है";
                }
               else if (Thread.CurrentThread.CurrentUICulture.ToString() == "gu")
               {
                   Label1.Text = "આપનું સ્વાગત છે";
               }
                else
                {
                    Label1.Text = "WELCOME";
                }
               lbl_user.Text = Session["userName"].ToString().ToUpper();
            }
            
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
        }
    }
}
