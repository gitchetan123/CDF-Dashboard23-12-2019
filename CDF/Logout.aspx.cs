using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
   
    db_context dbContext = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null)
            {
                string strcmd = "insert into tblLog (uId,log_type,log_time) values ( '" + Convert.ToInt32(Session["uid"]) + "','out','" + DateTime.Now + "')";
                int i = dbContext.ExecNonQuery(strcmd);
            }

            if (Session["logintype"].ToString().Equals("nongoogle"))
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("~/login.aspx", false);
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://www.dheya.com/cdf-dashboard/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);      
            Response.Redirect("~/login.aspx", false);
        }
       
    }
}