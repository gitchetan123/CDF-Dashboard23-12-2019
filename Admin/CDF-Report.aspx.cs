using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CDF_Report : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    db_context dc = new db_context();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["adminuser_name"] != null)
            {
                div_Error.Visible = false;

                lbl_rowcount.Visible = false;


                BindGridView();
            }
            else
            {
                Response.Redirect("~/Admin/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
        }
    }
    private void BindGridView()
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string strcmd = "Select Distinct ud.batchId,tb.batchName,COUNT(ud.batchId) as BatchCount,COUNT(ud2.childTestStatus) as ChildTest,COUNT(ud1.childSessionStatus) as ChildSession, " +
               "COUNT(ud3.spouseTestStatus) as SpouseTest,COUNT(um1.uId) as ActiveCDF,COUNT(um2.uId) as DeactiveCDF,ISNULL(A1.Level1, 0) as Level1,isnull(A2.Level2, 0) as Level2,ISNULL(A3.Level3, 0) as Level3,ISNULL(A4.Level4, 0) as Level4,COUNT(ud4.id) as RefundCount,ISNULL(SUM(ud4.refundAmount),0) as RefAmountCount from tblUserDetails as ud " +
               "left outer Join tblTrainingBatch as tb on ud.batchId = tb.id " +
               "left outer Join tblUserMaster as um on ud.uId = um.uId " +
               "left outer join tblUserDetails as ud1 on um.uId = ud1.uId and ud1.childSessionStatus = 1 " +
               "left outer join tblUserDetails as ud2 on um.uId = ud2.uId and ud2.childTestStatus = 1 " +
               "left outer join tblUserDetails as ud3 on um.uId = ud3.uId and ud3.spouseTestStatus = 1 " +
               "left outer join tblUserDetails as ud4 on um.uId=ud4.uId and ud4.refundStatus ='Yes' " +
               "left outer Join tblUserMaster as um1 on ud.uId = um1.uId and um1.userStatus = 'ACTIVE' and um1.userTypeId = 2 and um1.userSource = 'DHEYA-CDF' and um1.cdfApproved = 'APPROVED' " +
               "left outer Join tblUserMaster as um2 on ud.uId = um2.uId and um2.userStatus = 'DEACTIVE'  and um2.userTypeId = 2 and um2.userSource = 'DHEYA-CDF' and um2.cdfApproved = 'APPROVED' " +
               "left outer Join(SELECT ud.batchId, count(um.uId) as Level1 from tblUserMaster as um inner join tblUserDetails as ud on ud.uId = um.uId " +
               "and um.cdfLevel = 1 and userTypeId = 2 and cdfApproved = 'APPROVED' group by ud.batchId) as A1 on tb.id = A1.batchId " +
               "left outer Join(SELECT ud.batchId, count(um.uId) as Level2 from tblUserMaster as um inner join tblUserDetails as ud on ud.uId = um.uId " +
               "and um.cdfLevel = 2 and userTypeId = 2 and cdfApproved = 'APPROVED' group by ud.batchId) as A2 on tb.id = A2.batchId " +
               "left outer Join(SELECT ud.batchId, count(um.uId) as Level3 from tblUserMaster as um inner join tblUserDetails as ud on ud.uId = um.uId " +
               "and um.cdfLevel = 3 and userTypeId = 2 and cdfApproved = 'APPROVED' group by ud.batchId) as A3 on tb.id = A3.batchId " +
               "left outer Join(SELECT ud.batchId, count(um.uId) as Level4 from tblUserMaster as um inner join tblUserDetails as ud on ud.uId = um.uId " +
               "and um.cdfLevel = 4 and userTypeId = 2 and cdfApproved = 'APPROVED' group by ud.batchId) as A4 on tb.id = A4.batchId " +
               "group by ud.batchId,tb.batchName,A1.Level1,A2.Level2,A3.Level3,A4.Level4 order by batchId  ";

                SqlDataAdapter da = new SqlDataAdapter(strcmd, con);
                //create a dataset object and fill it 
                DataSet ds = new DataSet();
                da.Fill(ds);

                grid_CDFReport.DataSource = ds;
                grid_CDFReport.DataBind();
                lbl_rowcount.Visible = true;
                div_Error.Visible = false;
                lbl_rowcount.Text = "Total Records - " + ds.Tables[0].Rows.Count.ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Error("" + ex);
            div_Error.Visible = true;
            div_Error.InnerText = "Something went wrong. Please try again......";

        }
    }

    protected void grid_CDFReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid_CDFReport.PageIndex = e.NewPageIndex;
        BindGridView();
    }

}