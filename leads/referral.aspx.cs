using System;
using System.IO;
using System.Web.UI;

public partial class Candidate_referral : System.Web.UI.Page
{

    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        StringWriter Html = new StringWriter();
        HtmlTextWriter Render = new HtmlTextWriter(Html);
        base.Render(Render);
        writer.Write(Html.ToString()
        .Replace("name=\"ctl00$ContentPlaceHolder1$redirect_url", "name=\"redirect_url")
        .Replace("id=\"ctl00_ContentPlaceHolder1_redirect_url", "id=\"redirect_url"));
    }

    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (Session["uid"] != null && Session["dheyaEmail"] != null)
            {
                hf_cdf.Value = Session["dheyaEmail"].ToString();
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
        catch(Exception)
        {
            Response.Redirect("~/login.aspx");
        }
    }
}