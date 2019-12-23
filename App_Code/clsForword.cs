using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using DheyaTestDashboard.Models;
using log4net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
//using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DheyaTestDashboard.Models;
//using System.Web.Mvc;

/// <summary>
/// Summary description for clsForword
/// </summary>
public class clsForword
{
    UserLogin userLogin = new UserLogin();
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    private readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public clsForword()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //Send mail function
    public Boolean SendEmail(string recepientEmail, string subject, string body)
    {
        try
        {
            using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["DisplayName"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
                NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                smtp.Send(mailMessage);
                return true;
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }
    }
    //Send SMS Function
    public Boolean sendSms(string mob, string msg)
    {
        string result = "";
        WebRequest request = null;
        HttpWebResponse response = null;
        try
        {
            string userid = ConfigurationManager.AppSettings["SMSUserId"].ToString();  //  "2000167436";
            string passwd = ConfigurationManager.AppSettings["SMSPassword"].ToString();  //  "xzreMXXv5";
            string url =
        "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" +
        mob + "&msg=" + msg + "&userid=" + userid + "&password=" + passwd + "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN";

            request = WebRequest.Create(url);
            response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader reader = new System.IO.StreamReader(stream, ec);
            result = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return true;
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }
    }
    //User Data push into CRM
    public void pushtoCRM(string strRequest)
    {
        try
        {
            HttpWebResponse objHttpWebResponse = null;
            UTF8Encoding encoding;
            string strResponse = "";
            string strURL = ConfigurationManager.AppSettings["CRMDataPushlink"].ToString();
            HttpWebRequest objHttpWebRequest;
            objHttpWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
            objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            objHttpWebRequest.PreAuthenticate = true;
            objHttpWebRequest.Method = "POST";

            //Prepare the request stream
            if (strRequest != null && strRequest != string.Empty)
            {
                encoding = new UTF8Encoding();
                Stream objStream = objHttpWebRequest.GetRequestStream();
                Byte[] Buffer = encoding.GetBytes(strRequest);
                // Post the request
                objStream.Write(Buffer, 0, Buffer.Length);
                objStream.Close();
            }
            objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
            encoding = new UTF8Encoding();
            StreamReader objStreamReader =
                new StreamReader(objHttpWebResponse.GetResponseStream(), encoding);
            strResponse = objStreamReader.ReadToEnd();
            objHttpWebResponse.Close();
            objHttpWebRequest = null;
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
        }

    }
    //Check user email already exists or not in database
    public Boolean EmailExist(UserLogin userLogin)
    {
        try
        {
            if (!userLogin.email.Equals("") && userLogin.email != null)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select email from tblUserMaster where email='" + userLogin.email + "'", con);
                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            return false;
        }
    }

}