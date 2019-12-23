using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class KY_Test_KY_test_instructions : BaseClass
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

   
    //int test_no;
   // int cid;
  //  string lang;
   
    protected void Page_Load(object sender, EventArgs e)    
    {

        try
        {
            if (Session["uid"] != null)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Error("Session expire" + ex);
            Session.Clear();
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
        }
        lblTestNo.Text = "1";

       
    }
    protected void lbContinue_Click(object sender, EventArgs e)
    {
        try
        {
            //Session["q_no"] = 1;

            Response.Redirect("KY_test_page.aspx", false);
        }
        catch (Exception ex)
        {
            Log.Error("Session expire" + ex);
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
        }
    }
}