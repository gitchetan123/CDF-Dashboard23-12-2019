using System;
using System.Threading;
using System.Web.UI;
using log4net;

public partial class Candidatemaster : System.Web.UI.MasterPage
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
            {
                //Session.Clear();
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Session["myapplication.language"] != null)
                    {
                        if (Session["myapplication.language"].ToString() == "en")
                        {
                            language.SelectedIndex = 0;
                        }
                        if (Session["myapplication.language"].ToString() == "hi")
                        {
                            language.SelectedIndex = 1;
                        }
                        if (Session["myapplication.language"].ToString() == "mr")
                        {
                            language.SelectedIndex = 2;
                        }
                        if (Session["myapplication.language"].ToString() == "gu")
                        {
                            language.SelectedIndex = 3;
                        }
                    }
                    else
                    {
                        language.SelectedIndex = 0;
                        Session["myapplication.language"] = "en";
                    }
                }
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
            Session.Clear();
            Response.Redirect("~/Default.aspx");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
        }

    }
    protected void language_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["myapplication.language"] = language.SelectedValue.ToString();
        Response.Redirect(Request.Url.ToString());

    }
}
