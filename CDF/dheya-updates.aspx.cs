using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CDF_dheya_updates : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {           
            lblDheyaUpdates.Attributes.Add("readonly", "readonly");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string querydheyaUpdates = "Select uId,dheyaUpdates From tblUserDetails where uId = " + Session["uid"] + "";
                SqlCommand command = new SqlCommand(querydheyaUpdates, con);
                con.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    if (dr["dheyaUpdates"] != DBNull.Value)
                    {
                        //string lblUpdates;
                        //string lbl2 = lblDheyaUpdates.Text.ToString();
                        //lblUpdates = dr["dheyaUpdates"].ToString();
                        //lbl2 = lblUpdates;
                        lblDheyaUpdates.Text = dr["dheyaUpdates"].ToString();
                    }
                    else
                    {
                        lblDheyaUpdates.Text = "No updates found.";
                    }
                } 
                        
            }
            
        }
        catch (Exception ex)
        {
            Log.Error(ex);            
        }
    }
}