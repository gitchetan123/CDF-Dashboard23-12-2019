using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;

public partial class Sale_sale_status : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGridView();

    }

    private void BindGridView()
    {
        try
        {

            string strcmd = "select ref.ReferByEmail, ref.uId, ref.FirstName + isnull(' ' + ref.LastName, '') as  FullName,ref.Contact,ref.Email, "
            + " case when ref.IsContact = 1 then 'Contact' when ref.IsLead = 1 then 'Lead' when ref.IsCase = 1 then 'Case' end as Refer_status, "
            + " case when UM.userStatus = 'ACTIVE' then 'paid' else 'not paid' end as PaymentStatus from tblReferralDetail as ref  "
            + " left outer join tblUserMaster as UM on ref.uId = UM.uId ";

            if (txt_search.Text != "")
            {
                strcmd += " where ((ref.Email like '%" + txt_search.Text.Trim() + "%') or (ref.Contact like '%" + txt_search.Text.Trim() + "%')) and ref.ReferByEmail='" + Session["email"].ToString() + "' ";
            }
            else
            {
                strcmd += " where ref.ReferByEmail='" + Session["email"].ToString() + "'";
            }
            strcmd += " order by ref.uId desc ";
            //if (rbpay.Checked) 
            //{
            //    strcmd += " where (PayStatus like '%" + rbpay.Text.Trim() + "%')";
            //}
            //if (rbunpay.Checked)
            //{
            //    strcmd += " where (PayStatus like '%" + rbunpay.Text.Trim() + "%')";
            //}

            DataTable dt = dbContext.ExecDataSet(strcmd).Tables[0];
            grid_reffStatus.DataSource = dt;
            grid_reffStatus.DataBind();
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            lbl_msg.Visible = true;

            lbl_msg.Attributes["class"] = "alert alert-danger";
            lbl_msg.Text = "Something went wrong. Please try again......";
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridView();

        }
        catch (Exception ex)
        {

            Log.Error("" + ex);
            lbl_msg.Visible = true;
            lbl_msg.Attributes["class"] = "alert alert-danger";
            lbl_msg.Text = "Something went wrong. Please try again......";
        }

    }

    protected void grid_reffStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_reffStatus.PageIndex = e.NewPageIndex;
        BindGridView();
    }
}