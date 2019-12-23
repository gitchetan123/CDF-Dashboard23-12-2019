using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Search_entrancedetail : System.Web.UI.Page
{
    dal clsdal = new dal();
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //check the user type 
    //        if (Session.Count > 0 && Session["user_type"].ToString().Equals("Admin"))
    //            this.Page.MasterPageFile = "~/AdminMaster.master";
    //        else if (Session.Count > 0 && Session["user_type"].ToString().Equals("USER"))
    //            this.Page.MasterPageFile = "~/UserMaster.master";
    //        else
    //            this.Page.MasterPageFile = "~/StaffMasterPage.master";
    //    }
    //    catch (Exception ex)
    //    {
    //        this.Page.MasterPageFile = "~/AdminMaster.master";
    //    }

    //}
    protected void Page_PreInit(Object sender, EventArgs e)
    {
        if (Session["type"] == null)
        {
            this.MasterPageFile = "~/CDFMaster.master";
        }
        else
        {
            this.MasterPageFile = "~/Admin/admin-master.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                div_msg.Visible = false;
                string strcmd = "select * FROM tbl_entrance_master where entrance_ID= '" + Request.QueryString["id"].ToString() + "'";
                DataSet ds = clsdal.ExecDataSet(strcmd);
                lbl_entrancename.Text = ds.Tables[0].Rows[0][1].ToString();
                lbl_detail.Text = ds.Tables[0].Rows[0][2].ToString();
                lbl_req.Text = ds.Tables[0].Rows[0][3].ToString();
                lbl_fee.Text = ds.Tables[0].Rows[0][4].ToString();
                lbl_edate.Text = ds.Tables[0].Rows[0][5].ToString().Replace("01-Jan-1900 12:00:00 AM", "");
                lbl_adate.Text = ds.Tables[0].Rows[0][6].ToString().Replace("01-Jan-1900 12:00:00 AM", "");
                entrancelink.NavigateUrl = "http://" + ds.Tables[0].Rows[0][7].ToString();
                entrancelink.Text = ds.Tables[0].Rows[0][7].ToString();


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Something wrong. Please Try again.";
            }
        }
    }
}