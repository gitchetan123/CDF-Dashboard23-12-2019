using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;

public partial class CDFMaster : System.Web.UI.MasterPage
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string type;
    db_context dbContext = new db_context();
    // Removed Tabs : 1. abilityfilter  2. interestfilter  3. personalityfilter
    // No need to search by above filter
    // Changes on 26-11-2018
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["cdfLevel"] != null)
            {
                //string IsAllowReport = Session["IsAllowReport"].ToString();
                if (Session["IsAllowReport"].ToString()=="" )
                {
                    li_Candidate.Visible = false;
                }
                else if(Session["IsAllowReport"].ToString() == "No")
                {
                    li_Candidate.Visible = false;
                }
                else
                {
                    li_Candidate.Visible = false;
                }

                int cdflevel;

                cdflevel = Convert.ToInt32(Session["cdfLevel"]);

                if (cdflevel == 2 || cdflevel == 3 || cdflevel == 4)
                {
                    CareerSearch.Visible = true;
                    //abilityfilter.Visible = true;
                    //interestfilter.Visible = true;
                    //personalityfilter.Visible = true;
                    abilityInterestFilter.Visible = true;
                }
                else
                {
                    CareerSearch.Visible = false;
                    //abilityfilter.Visible = false;



                    //interestfilter.Visible = false;
                    //personalityfilter.Visible = false;
                    abilityInterestFilter.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["type"] == null)
            {
                type = "cdf";
            }
            else
            {
                type = "Research";
            }  

            if (type != "Research")
            {
                if (Session["uid"] != null && Session["dheyaEmail"] != null)
                {
                    if (Session["dheyaEmail"].ToString() != "No")
                    {
                        lbl_username.Text = Session["userName"].ToString();
                        lbl_username2.Text = Session["userName"].ToString();
                        //Image1.ImageUrl = Session["formalImg"].ToString();
                        Image1.ImageUrl = Session["formalImg"].ToString();
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "myScript", "ChangeImage('" + Session["formalImg"].ToString() + "');", false);

                        //get_notification();
                    }
                    else
                    {
                        Response.Redirect("~/pre/home.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("~/login.aspx", false);
                }
            }
            else
            {
                
            }
        }
        catch (Exception ex)
        {
            ex.StackTrace.ToString();
            Log.Error(ex);
        }
    }

    public void get_notification()
    {
        string i = dbContext.ExecScal("select uId, fname, lname from tblUserMaster where fname is null and lname is null and uId = "+ Session["uid"]);
        if (i=="1")
        { lbl_Info.Visible = true; }
        string j = dbContext.ExecScal("select count(*) from tblUserBankDetails where accountNumber is null and  uId = " + Session["uid"]);
        if(j=="0")
        { lbl_bank.Visible = true; }
    }
}
