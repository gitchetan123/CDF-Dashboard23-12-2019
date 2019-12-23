using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for SellService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class SellService : System.Web.Services.WebService
{
    //private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    data_context datacontext = new data_context();
    SqlConnection con;
    public string strcon = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();
    public SellService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<string> GetExecutiveNames(string exeName)
    {
        try
        {
            List<string> exeNames = new List<string>();
            using (SqlConnection con = new SqlConnection(strcon))
            {
                string s_query = "sp_get_executive_names";
                con.Open();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(s_query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@exeName", exeName);
                dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    exeNames.Add(dr["exeName"].ToString());
                }
            }
            return exeNames;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
