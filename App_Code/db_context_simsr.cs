using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for db_context_simsr
/// </summary>
public class db_context_simsr
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string strcon;
    data_context datacontext = new data_context();
    public db_context_simsr()
    {
        strcon = ConfigurationManager.ConnectionStrings["career_portalConnectionString_simsr"].ConnectionString.ToString();
    }


    public int ExecNonQuery(string s_query, int c_id, int userType, string fname, string lname, string email, string contact, string userStatus)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(s_query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uId", c_id);
                cmd.Parameters.AddWithValue("@userTypeId", userType);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@contactNo", contact);
                cmd.Parameters.AddWithValue("@userStatus", userStatus);
                int intEffectedRows = cmd.ExecuteNonQuery();
                return intEffectedRows;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public SqlDataReader ExecDataReader(string s_query, int c_id)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(s_query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uId", c_id);
                dr = cmd.ExecuteReader();
                dr.Read();
                return dr;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int ExecNonQuery(string s_query, int c_id, string userStatus)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(s_query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uId", c_id);
                cmd.Parameters.AddWithValue("@userStatus", userStatus);
                int intEffectedRows = cmd.ExecuteNonQuery();
                return intEffectedRows;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int ExecScal(string s_query, int uId)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(strcon))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(s_query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@uId", uId);
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                return i;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}