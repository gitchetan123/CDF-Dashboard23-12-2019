using System;



//********************************************************************************************************
//PageName        : CreateTicket
//Description     : User can create ticket from this page 
//AddedBy         :                    AddedOn   : **/**/2017
//UpdatedBy       :                    UpdatedOn : 
//Reason          : 
//********************************************************************************************************

public partial class Ticket_CreateTicket : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null && Session["dheyaEmail"] != null)
            {
                // Session value saved in cb_cdf
                cb_cdf.Value = Session["dheyaEmail"].ToString();
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Info("Session is expire " + ex);
            Response.Redirect("login.aspx", false);
        }
    }
}