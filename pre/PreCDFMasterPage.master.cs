using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pre_PreCDFMasterPage : System.Web.UI.MasterPage
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dc = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["uid"] != null && Session["dheyaEmail"] != null)
            {
                if (Session["dheyaEmail"].ToString() == "No")
                {
                    // string uname = Session["cdf"].ToString();
                    lbl_username.Text = Session["userName"].ToString();
                    lbl_username2.Text = Session["userName"].ToString();
                    //lbl_lname.Text = Session["lname"].ToString();
                    Image1.ImageUrl = Session["formalImg"].ToString();
                    //uname.ImageUrl = Session["p"].ToString();
                    //string conn = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                    //SqlConnection connection = new SqlConnection(conn);
                    //connection.Open();
                    //string query = "SELECT A.status,B.ndaStatus,C.teststatus FROM tblUserMaster as A inner join tblUserDetails as B on A.uId = B.uId left outer JOIN tblUserProductMaster as C ON A.uId = C.uId where A.uId='" + Session["uid"].ToString() + "' and C.prodid=7";
                    //SqlDataReader dr = dc.ExecDataReader(query);
                    //if (dr.HasRows)
                    //{
                    //    dr.Read();
                    //    if (dr["teststatus"].ToString() == "Complete")
                    //    {
                    //        hiddendiv.Visible = true;
                    //        hiddendiv2.Visible = false;
                    //        hiddendiv3.Visible = true;
                    //    }
                    //    else
                    //    {
                    //        hiddendiv.Visible = false;
                    //        hiddendiv2.Visible = true;
                    //        hiddendiv3.Visible = false;
                    //    }
                    //    if (dr["ndaStatus"].ToString() == "Agree")
                    //    {
                    //        hiddendiv.Visible = true;
                    //        hiddendiv2.Visible = false;
                    //        hiddendiv3.Visible = false;
                    //    }

                    //    //if (dr["teststatus"].ToString() == "Complete" || dr["ndaStatus"].ToString() == "")
                    //    //{
                    //    //    hiddendiv2.Visible = false;
                    //    //    Response.Redirect("~/pre/moreinfo.aspx", false);
                    //    //}
                    //    //else 

                    //    //else if (dr["ndaStatus"].ToString() == "")
                    //    //{
                    //    //    Response.Redirect("~/pre/moreinfo.aspx", false);
                    //    //}

                    //    //else
                    //    //{
                    //    //    hiddendiv.Visible = false;
                    //    //    hiddendiv2.Visible = false;
                    //    //}
                    //}
                }
                else
                {
                    Response.Redirect("~/home.aspx", false);
                }
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}
