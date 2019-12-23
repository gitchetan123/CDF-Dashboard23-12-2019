//********************************************************************************************
//PageName        : Download page
//Description     : This page is work on download pdf and images
//AddedBy         : Bahubali                   AddedOn   : 11/07/2017
//UpdatedBy       :                            UpdatedOn : 
//Reason          :
//*******************************************************************************************

using log4net;
using System;
using System.IO;

public partial class Admin_download : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {       
        try
        {
            string fname = "";
            if (Request.QueryString["filename"] != null)
                fname = Request.QueryString["filename"].ToString();
            DownloadFile(fname, true);
        }
        catch (System.Threading.ThreadAbortException)
        { }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }

    //set file set file extension
    private void DownloadFile(string fname, bool forceDownload)
    {
        string path = fname; //Server.MapPath((".") + fname);
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        string type = "";
        // set known types based on file extension  
        if (ext != null)
        {
            switch (ext.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    type = "image/jpeg";
                    break;
                case ".gif":
                    type = "image/GIF";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".docx":
                case ".doc":
                case ".rtf":
                    type = "Application/msword";
                    break; 
                case ".pdf":
                    type = "Application/pdf";
                    break;
            }
        }
        if (forceDownload)
        {
            Response.AppendHeader("content-disposition",
                "attachment; filename=" + name);
        }
        if (type != "")
        Response.ContentType = type;
        Response.WriteFile(path);
        Response.End();
    }
}