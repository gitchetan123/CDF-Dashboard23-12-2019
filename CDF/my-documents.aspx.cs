using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CDF_my_documents : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["uid"] != null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //
                string query_docstatus = "select idcard ,certificate ,visitingCard ,ndaCopy,childTestStatus,childSessionStatus,spouseTestStatus,shadowSession From tblUserDetails where uId='" + Session["uid"].ToString() + "'";
                SqlCommand cmd = new SqlCommand(query_docstatus, connection);
                SqlDataReader dr = cmd.ExecuteReader();
                //Check if table has rows for required query
                if (dr.HasRows)
                {

                    dr.Read();
                    string strr = Convert.ToString(dr["idcard"]);
                    if (Convert.ToString(dr["idcard"]) == "True")
                    {
                        lbl_idcard.Text = "Received";
                    }
                    else
                    {
                        lbl_idcard.Text = "Pending";
                    }
                    if (Convert.ToString(dr["certificate"]) == "True")
                    {
                        lbl_certificate.Text = "Received";
                    }
                    else
                    {
                        lbl_certificate.Text = "Pending";
                    }
                    if (Convert.ToString(dr["visitingCard"]) == "True")
                    {
                        lbl_visitingcard.Text = "Received";
                    }
                    else
                    {
                        lbl_visitingcard.Text = "Pending";
                    }
                    if (Convert.ToString(dr["ndaCopy"]) == "True")
                    {
                        lbl_ndacopy.Text = "Received";
                    }
                    else
                    {
                        lbl_ndacopy.Text = "Pending";
                    }
                    if (Convert.ToString(dr["childTestStatus"]) == "True")
                    {
                        lbl_childTest.Text = "Complete";
                    }
                    else
                    {
                        lbl_childTest.Text = "Pending";
                    }
                    if (Convert.ToString(dr["childSessionStatus"]) == "True")
                    {
                        lbl_childSession.Text = "Complete";
                    }
                    else
                    {
                        lbl_childSession.Text = "Pending";
                    }
                    if (Convert.ToString(dr["spouseTestStatus"]) == "True")
                    {
                        lbl_spouseTest.Text = "Complete";
                    }
                    else
                    {
                        lbl_spouseTest.Text = "Pending";
                    }
                    if (Convert.ToString(dr["shadowSession"]) != "")
                    {
                        lbl_shadowSessions.Text = dr["shadowSession"].ToString();
                    }
                    else
                    {
                        lbl_shadowSessions.Text = "0";
                    }
                }

            }
        }
        else
        { }
    }
}