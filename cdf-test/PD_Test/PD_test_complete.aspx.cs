using System;
using System.Web.UI;
using log4net;
using System.Configuration;

public partial class PD_Test_PD_test_complete : BaseClass
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["uid"] != null)
                {
                    lblTestNo.Text = "2";
                    int c_id = Convert.ToInt32(Session["uid"].ToString());
                    int batid = Convert.ToInt32(Session["batid"].ToString());
                    if (dbContext.AllTestCompleteKYAndPD(c_id, batid))
                    {
                      
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }
        }
    }


  
}