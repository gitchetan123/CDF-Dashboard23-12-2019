using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class vs_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        data_context dc = new data_context();
        string SMSText = ConfigurationManager.AppSettings["CDFAddManuallySmsTemplate"].ToString();
        //SMSText = SMSText.Replace("{userName}", "myclap");
        //SMSText = SMSText.Replace("{Password}", "Reminder");
        //dc.sendSms("9595396050", SMSText);

       
        //SMSText = ConfigurationManager.AppSettings["CDFAddManuallySmsTemplate"].ToString();
        //SMSText = SMSText.Replace("{CDF}", "Bahubali");
        //dc.sendSms("9595396050", SMSText);

        SMSText = ConfigurationManager.AppSettings["CDFRegistrationCompleteSmsTemplate"].ToString();        
        dc.sendSms("9595396050", SMSText);




        SMSText = ConfigurationManager.AppSettings["CDFTestCompleteSmsTemplate"].ToString();
        SMSText = SMSText.Replace("{CDF}", "Bahubali");
        dc.sendSms("9595396050", SMSText);




        SMSText = ConfigurationManager.AppSettings["CDFApprovalSmsTemplate"].ToString();
        SMSText = SMSText.Replace("{CDF}", "Bahubali");
        dc.sendSms("9595396050", SMSText);






        SMSText = ConfigurationManager.AppSettings["NDASmsTemplate"].ToString();
        SMSText = SMSText.Replace("{CDF}", "Bahubali");
        dc.sendSms("9595396050", SMSText);






        SMSText = ConfigurationManager.AppSettings["ResetPasswordSmsTemplate"].ToString();
        SMSText = SMSText.Replace("{CDF}", "Bahubali");
        dc.sendSms("9595396050", SMSText);

        SMSText = ConfigurationManager.AppSettings["WelcomeEmailSmsTemplate"].ToString();
        SMSText = SMSText.Replace("{CDF}", "Bahubali");
        dc.sendSms("9595396050", SMSText);





        SMSText = ConfigurationManager.AppSettings["CDFPaymentSmsTemplate"].ToString();
        SMSText = SMSText.Replace("{CDF}", "Bahubali");
        SMSText = SMSText.Replace("{}", "1000");
        dc.sendSms("9595396050", SMSText);

        SMSText = ConfigurationManager.AppSettings["CustomPaymentSmsTemplate"].ToString();
        SMSText = SMSText.Replace("{CDF}", "Bahubali");
        dc.sendSms("9595396050", SMSText);


    }
}