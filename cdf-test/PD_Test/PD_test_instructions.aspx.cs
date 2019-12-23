using System;
using System.Threading;
using System.Web.UI;
using log4net;
using System.Configuration;

public partial class PD_Test_PD_test_instructions : BaseClass
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        int cid;

        if (!IsPostBack && Session.Count > 0)
        {
            try
            {

                //if (Session["Audio"] == "true")
                //{
                //    divAudio.Visible = true;

                //    audioControl.Attributes["src"] = "../" + ConfigurationManager.AppSettings["audioFolderName"].ToString() + "/pd-test/" + Thread.CurrentThread.CurrentUICulture.ToString() + "/" + "Instruction" + ConfigurationManager.AppSettings["fileExtension"].ToString();
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "playAudio()", true);
                //}
                //else
                //{
                //    divAudio.Visible = false;

                //}
                
                if (Session["uid"].ToString() != null && Session["uid"].ToString() != "")
                    cid = Convert.ToInt32(Session["uid"].ToString());
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
                }

                lblTestNo.Text = "2";


                if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                {
                    Image4.ImageUrl = "~/images/TestImages/test_10_opt_mr.png";
                    Image1.ImageUrl = "~/images/TestImages/test_10_opt_mr_sm.png";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    Image4.ImageUrl = "~/images/TestImages/test_10_opt_hi.png";
                    Image1.ImageUrl = "~/images/TestImages/test_10_opt_hi_sm.png";
                }
                if (Thread.CurrentThread.CurrentUICulture.ToString() == "en")
                {
                    Image4.ImageUrl = "~/images/TestImages/test_10_opt_en.png";
                    Image1.ImageUrl = "~/images/TestImages/test_10_opt_en_sm.png";
                }
                if (Thread.CurrentThread.CurrentUICulture.ToString() == "gu")
                {
                    Image4.ImageUrl = "~/images/TestImages/test_10_opt_gu.png";
                    Image1.ImageUrl = "~/images/TestImages/test_10_opt_gu_sm.png";
                }
            }
            catch (Exception ex)
            {
                Log.Error("" + ex);
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }
        }
        if (Session.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
        }

    }
    protected void lbContinue_Click(object sender, EventArgs e)
    {
        Response.Redirect("PD_test_page.aspx", false);
    }
}