using System;

public partial class CDFMaster : System.Web.UI.MasterPage
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["adminuser_name"] != null)
            {
                //string type = Session["type"].ToString();
                lbl_username.Text = Session["adminuser_name"].ToString();
                lbl_username2.Text = Session["adminuser_name"].ToString();
                Image1.ImageUrl = "~/images/Avatar.png";
                if (Session["type"].ToString() == "Research")
                {
                    Home.Visible = true;
                    CareerTools.Visible = true;
                    CareerSearch.Visible = true;
                    CDFDetails.Visible = false;
                    other.Visible = true;
                    ExecutiveList.Visible = false;
                    Resources.Visible = true;
                    AddDocument.Visible = true;
                    Reports.Visible = false;
                }
                if(Session["type"].ToString() == "SuperAdmin") 
                {
                    AdminUsersList.Visible = true;
                    Export.Visible = true;
                }
                if (Session["type"].ToString() == "Admin")
                {
                    Export.Visible = true;
                }
                if (Session["type"].ToString() == "Staff")
                {
                    ExecutiveList.Visible = false;
                }
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
