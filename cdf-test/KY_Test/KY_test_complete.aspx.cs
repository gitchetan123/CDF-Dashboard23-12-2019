using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class KY_Test_KY_test_complete : BaseClass
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
                    lblTestNo.Text = "1";
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
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

    }
}