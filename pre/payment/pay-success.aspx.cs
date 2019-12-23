using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pre_payment_pay_success : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["CustomPaymentAmount"].ToString() == "33500" || Session["CustomPaymentAmount"].ToString() != "15000")
            {
                div_note.Visible = false;
            }
            if (Session["CustomPaymentAmount"].ToString() == "15000")
            {
                div_note.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}