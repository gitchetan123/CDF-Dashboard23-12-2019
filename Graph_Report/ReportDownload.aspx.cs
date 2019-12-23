using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using log4net;

public partial class ESAT_ReportDownload : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    int c_id;
    MobileDAL dal = new MobileDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string c_id = Convert.ToInt32(Session["c_id"]).ToString(); 
           // string c_id = Request.QueryString["c_id"].ToString();
            string snm = dal.EncryptData(c_id);
            Response.Redirect("CorpReport.aspx?snm=" + snm,true);
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            div_msg.Visible = true;
            div_msg.Attributes["class"] = "alert alert-danger";
            div_msg.InnerText = "Something went wrong. Please try again.";     
        }
    }
}