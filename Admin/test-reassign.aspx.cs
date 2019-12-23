using System;
using System.Configuration;
using System.Data.SqlClient;

public partial class Admin_test_reassign : System.Web.UI.Page
{
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
    int c_id;
    int KYtest = 9;
    int PDtest = 10;
    int batid = 3;
    int prodid = 7;
    db_context dbContext = new db_context();

    protected void Page_Load(object sender, EventArgs e)
    {
        c_id = Convert.ToInt32(dbContext.Datadecrypt(Request.QueryString["id"]));

        if (!IsPostBack)
        {
            // c_id = Convert.ToInt32(Request.QueryString["id"]);
          
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string strcmd = "";

                    strcmd = "select fname+' ' + lname from tblUserMaster where uid = " + c_id + "";
                    SqlCommand cmd3 = new SqlCommand(strcmd, connection);
                    lbl_name.Text = " &nbsp" + cmd3.ExecuteScalar().ToString().ToUpper();

                    #region KY Test                   
                    strcmd = "SELECT count(q_no) FROM tblKYCandAnswers where batid=" + batid + " and c_id ='" + c_id + "'";
                    SqlCommand cmd = new SqlCommand(strcmd, connection);
                    int ky = Convert.ToInt32(cmd.ExecuteScalar());
                    ky_completed_que.Text = ky.ToString();
                    ky_remin_que.Text = (90 - Convert.ToInt32(ky)).ToString();

                    if (ky_completed_que.Text == "90")
                    {
                        strcmd = "";
                        strcmd = "SELECT count(factor_no)  FROM tblKYCandFactors where batid=" + batid + " and c_id='" + c_id + "'";
                        cmd = new SqlCommand(strcmd, connection);

                        int kyFact = Convert.ToInt32(cmd.ExecuteScalar());
                        if (kyFact != 0)
                        {
                            ky_factor.Text = kyFact.ToString();
                            if (kyFact == 9)
                            {
                                ky_status.Text = "Complete";
                            }
                            else
                            {
                                ky_status.Text = "Factor InComplete";
                            }
                            ky_factor.Text = kyFact.ToString();
                        }
                        else
                        {
                            ky_status.Text = "InComplete";
                            ky_factor.Text = "No Factor";
                        }
                    }
                    else
                    {
                        ky_status.Text = "InComplete";
                        ky_factor.Text = "No Factor";
                    }
                    #endregion

                    #region PD Test
                    strcmd = "";
                    strcmd = "SELECT COUNT(c_id)  FROM  tblPersonalityCandAnswers where batid=" + batid + " and c_id='" + c_id + "'";
                    SqlCommand cmd2 = new SqlCommand(strcmd, connection);
                    SqlDataReader dr2 = cmd2.ExecuteReader();


                    //dr = clsdal.ExecDataReader(strcmd);
                    int pd;
                    if (dr2.HasRows)
                    {
                        dr2.Read();
                        pd = dr2.GetInt32(0);
                    }
                    else
                        pd = 0;
                    pd_completed_que.Text = pd.ToString();
                    pd_remin_que.Text = (24 - Convert.ToInt32(pd)).ToString();

                    if (pd_completed_que.Text == "24")
                    {
                        pd_status.Text = "Complete";
                    }
                    else
                    {
                        pd_status.Text = "InComplete";
                    }

                    #endregion

                    if (ky_status.Text == "Complete")
                        btn_ky.Enabled = true;
                    else
                        btn_ky.Enabled = false;

                    if (pd_status.Text == "Complete")
                        btn_pd.Enabled = true;
                    else
                        btn_pd.Enabled = false;

                    if (ky_status.Text == "Complete" && pd_status.Text == "Complete")
                        btn_all_test.Enabled = true;
                    else
                         btn_all_test.Enabled = false;


                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerHtml = "Something went wrong. Please try again......";
                }
            }
        }
    }

    protected void btn_ky_Click(object sender, EventArgs e)
    {
        reassignKYTest();
    }
    protected void btn_pd_Click(object sender, EventArgs e)
    {
        reassignPDTest();
    }

    protected void btn_all_test_Click(object sender, EventArgs e)
    {
        reassignKYTest();
        reassignPDTest();

        div_msg.Visible = true;
        div_msg.Attributes["class"] = "alert alert-success";
        div_msg.InnerHtml = "ALL Test Reassign success ......";

    }

    public void reassignKYTest()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            SqlTransaction transaction;
            transaction = connection.BeginTransaction("KYTestReassign");

            cmd.Connection = connection;
            cmd.Transaction = transaction;

            try
            {
                #region KY Test

                string strQuery = "  insert into tblTestReassign(uId, testId, batid, dateTime, remark, adminId) values " +
                " (" + c_id + ", " + KYtest + "," + batid + ",'" + DateTime.Now + "','" + "KY Test Reassign" + "', '" + Session["adminuser_email"].ToString() + "') ";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int testRe = cmd.ExecuteNonQuery();

                strQuery = " INSERT Into tblKYCandAnswersHistory  SELECT c_id,q_no,factor_no,marks,batid FROM tblKYCandAnswers WHERE batid=" + batid + " and c_id = " + c_id + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyAns = cmd.ExecuteNonQuery();

                strQuery = " INSERT Into tblKYCandFactorsHistory  SELECT c_id,factor_no,score,rating,P_rating,measure,New_P_rating,batid FROM tblKYCandFactors WHERE batid=" + batid + " and c_id = " + c_id + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyFact = cmd.ExecuteNonQuery();

                strQuery = " delete from  tblKYCandAnswers   WHERE batid=" + batid + " and c_id = " + c_id + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyAnsDel = cmd.ExecuteNonQuery();

                strQuery = " delete from  tblKYCandFactors   WHERE batid=" + batid + " and c_id = " + c_id + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyFactDel = cmd.ExecuteNonQuery();

                strQuery = " update tblUserTestMaster set testStatus = 'Reassign', factorStatus = 'Reassign' WHERE batid = " + batid + " and uId = " + c_id + " and testid = " + KYtest + " ";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyuserTest = cmd.ExecuteNonQuery();

                strQuery = "update tblUserProductMaster set testStatus='Reassign' where uId=" + c_id + " and prodid=" + prodid + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyuserProd = cmd.ExecuteNonQuery();

                transaction.Commit();

                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-success";
                div_msg.InnerHtml = "KY Test Reassign success ......";

                #endregion
            }

            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerHtml = "Something went wrong. Please try again......";
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Log.Error("Rollback Error" + ex2);
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerHtml = "Rollback error. Please try again......";
                }
            }
        }
    }

    public void reassignPDTest()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            SqlCommand cmd = connection.CreateCommand();
            SqlTransaction transaction;
            transaction = connection.BeginTransaction("PDTestReassign");

            cmd.Connection = connection;
            cmd.Transaction = transaction;

            try
            {
                #region KY Test

                string strQuery = "  insert into tblTestReassign(uId, testId, batid, dateTime, remark, adminId) values " +
                " (" + c_id + ", " + PDtest + "," + batid + ",'" + DateTime.Now + "','" + "PD Test Reassign" + "', '" + Session["adminuser_email"].ToString() + "') ";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int testRe = cmd.ExecuteNonQuery();

                strQuery = " INSERT Into tblPersonalityCandAnswersHistory  SELECT c_id,q_id,most_qno,least_qno,most_code,least_code,most_status,least_status,batid FROM tblPersonalityCandAnswers WHERE batid=" + batid + " and c_id = " + c_id + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int pdAns = cmd.ExecuteNonQuery();

                strQuery = " delete from  tblPersonalityCandAnswers  WHERE batid=" + batid + " and c_id = " + c_id + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyAnsDel = cmd.ExecuteNonQuery();

                strQuery = " update tblUserTestMaster set testStatus = 'Reassign', factorStatus = 'Reassign' WHERE batid = " + batid + " and uId = " + c_id + " and testid = " + PDtest + " ";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyuserTest = cmd.ExecuteNonQuery();

                strQuery = "update tblUserProductMaster set testStatus='Reassign' where uId=" + c_id + " and prodid=" + prodid + "";
                //cmd = new SqlCommand(strQuery, connection);
                cmd.CommandText = strQuery;
                int kyuserProd = cmd.ExecuteNonQuery();

                transaction.Commit();

                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-success";
                div_msg.InnerHtml = "PD Test Reassign success ......";

                #endregion
            }

            catch (Exception ex)
            {
                Log.Error(ex);
                div_msg.Visible = true;
                div_msg.Attributes["class"] = "alert alert-danger";
                div_msg.InnerHtml = "Something went wrong. Please try again......";
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Log.Error("Rollback Error" + ex2);
                    div_msg.Visible = true;
                    div_msg.Attributes["class"] = "alert alert-danger";
                    div_msg.InnerHtml = "Rollback error. Please try again......";
                }
            }
        }
    }

}