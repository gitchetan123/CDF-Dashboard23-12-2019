using System;
using System.Configuration;
using System.Web.UI;

public partial class CDF_update_profile_pic : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dbContext = new db_context();  

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string email = Session["dheyaEmail"].ToString();
            if (file_image.PostedFile.ContentLength < 2097152) // 2 MB 1024*KB of file size
            {
                //formal_Image Upload code with rename in user emailid+_formal_+id
                string img = file_image.FileName;
                img = img.Substring(img.LastIndexOf('.'));
                string imgfile = email + "_formal_" + Session["uid"].ToString() + img;
                file_image.PostedFile.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imageFormalPath"].ToString() + imgfile));

               
                // update data in table tblUserDetails for CDF-PostRegistration
                string str = "update tblUserDetails set formalImg = '" + imgfile.ToString() + "' where uId = '" + Session["uid"].ToString() + "'";
                int i = dbContext.ExecNonQuery(str);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-success";
                div_msg.InnerText = "Successfully uploaded your image";
            }
            else
            {
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerText = "Selected image file size is more than 1 MB. So, please upload it again with less than 1 MB size.";
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
}