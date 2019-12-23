using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;
using log4net;
using System.Data.SqlClient;
using System.Configuration;

public partial class KY_Test_KY_test_page : BaseClass
{
    private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //create a object db_context class for database connecton and database related operation
    db_context dbContext = new db_context();
    int q_no;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Session.Count > 0)
        {
            try
            {
                if (Session["uid"] != null)
                {
                    Hcid.Value = Session["uid"].ToString();

                    if (HQno.Value == "")
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {

                            string strmax = "select isnull (max(q_no),0) from tblKYCandAnswers where c_id=" + Session["uid"].ToString() + " and batid=3";
                            SqlCommand cmd = new SqlCommand(strmax, con);
                            con.Open();
                            int maxQue = Convert.ToInt32(cmd.ExecuteScalar());
                            // int maxQue = Convert.ToInt32(dbContext.ExecScal(strmax));
                            maxQue++;
                            HQno.Value = maxQue.ToString();
                        }
                    }

                    if (Convert.ToInt32(HQno.Value) >= 90)
                    {
                        // generateKYTestFactor();

                        if (dbContext.generateKYTestFactor(Convert.ToInt32(Hcid.Value), 3, 9))
                        {
                            Response.Redirect("KY_test_complete.aspx", false);
                        }
                        else
                        {
                            // "Incomplete";
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }

            load_questions();
        }
    }

    private void load_questions()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (Session["myapplication.language"] == "" || Session["myapplication.language"] == null)
                { Hlang.Value = Session["myapplication.language"].ToString(); }
                else { Hlang.Value = Session["myapplication.language"].ToString(); }


                lblTestNo.Text = "1";//Test No.

                if (Thread.CurrentThread.CurrentUICulture.ToString() == "mr")
                {
                    lblQuestionNo.Text = HQno.Value.ToString() + " पासून " + Convert.ToInt32(Convert.ToInt32(HQno.Value) + 9).ToString() + " पर्यंत";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "hi")
                {
                    lblQuestionNo.Text = HQno.Value.ToString() + " से " + Convert.ToInt32(Convert.ToInt32(HQno.Value) + 9).ToString() + " तक";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "gu")
                {
                    lblQuestionNo.Text = HQno.Value.ToString() + " થી " + (Convert.ToInt32(HQno.Value.ToString()) + 9).ToString() + " સુધી";
                }
                else if (Thread.CurrentThread.CurrentUICulture.ToString() == "en")
                {
                    lblQuestionNo.Text = "From " + HQno.Value.ToString() + " To " + Convert.ToInt32(Convert.ToInt32(HQno.Value) + 9).ToString();
                }


                Hlang.Value.ToString();
                int lng = 1;
                if (Hlang.Value.ToString() == "en")
                    lng = 1;
                else if (Hlang.Value.ToString() == "hi")
                    lng = 2;
                else if (Hlang.Value.ToString() == "mr")
                    lng = 3;
                else if (Hlang.Value.ToString() == "gu")
                    lng = 4;
                else
                    lng = 1;

                string str = "select qno,factor_no,questext,op1,op2,op3,opblue,opred from tblKYQuestions " +
                       "where qno >= " + HQno.Value.ToString() + " AND qno <= (" + HQno.Value.ToString() + " + 9) and langid =" + lng;



                // DataSet ds = dbContext.ExecDataSet(str);
                SqlDataAdapter da = new SqlDataAdapter(str, con);
                DataSet ds = new DataSet();
                da.Fill(ds);


                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    rblOptions.Items.Clear();
                    RadioButtonList2.Items.Clear();
                    RadioButtonList1.Items.Clear();
                    RadioButtonList3.Items.Clear();
                    RadioButtonList4.Items.Clear();
                    RadioButtonList5.Items.Clear();
                    RadioButtonList6.Items.Clear();
                    RadioButtonList7.Items.Clear();
                    RadioButtonList8.Items.Clear();
                    RadioButtonList9.Items.Clear();

                    lblQuestionname.Text = ds.Tables[0].Rows[0][2].ToString();
                    rblOptions.Items.Add(new ListItem(ds.Tables[0].Rows[0][3].ToString(), "1"));
                    rblOptions.Items.Add(new ListItem(ds.Tables[0].Rows[0][4].ToString(), "2"));
                    rblOptions.Items.Add(new ListItem(ds.Tables[0].Rows[0][5].ToString(), "3"));

                    Label2.Text = ds.Tables[0].Rows[2][2].ToString();
                    RadioButtonList2.Items.Add(new ListItem(ds.Tables[0].Rows[1][3].ToString(), "1"));
                    RadioButtonList2.Items.Add(new ListItem(ds.Tables[0].Rows[1][4].ToString(), "2"));
                    RadioButtonList2.Items.Add(new ListItem(ds.Tables[0].Rows[1][5].ToString(), "3"));

                    Label5.Text = ds.Tables[0].Rows[1][2].ToString();
                    RadioButtonList1.Items.Add(new ListItem(ds.Tables[0].Rows[2][3].ToString(), "1"));
                    RadioButtonList1.Items.Add(new ListItem(ds.Tables[0].Rows[2][4].ToString(), "2"));
                    RadioButtonList1.Items.Add(new ListItem(ds.Tables[0].Rows[2][5].ToString(), "3"));

                    Label8.Text = ds.Tables[0].Rows[3][2].ToString();
                    RadioButtonList3.Items.Add(new ListItem(ds.Tables[0].Rows[3][3].ToString(), "1"));
                    RadioButtonList3.Items.Add(new ListItem(ds.Tables[0].Rows[3][4].ToString(), "2"));
                    RadioButtonList3.Items.Add(new ListItem(ds.Tables[0].Rows[3][5].ToString(), "3"));

                    Label11.Text = ds.Tables[0].Rows[4][2].ToString();
                    RadioButtonList4.Items.Add(new ListItem(ds.Tables[0].Rows[4][3].ToString(), "1"));
                    RadioButtonList4.Items.Add(new ListItem(ds.Tables[0].Rows[4][4].ToString(), "2"));
                    RadioButtonList4.Items.Add(new ListItem(ds.Tables[0].Rows[4][5].ToString(), "3"));

                    // -----------------------------------------------------------------------------

                    Label14.Text = ds.Tables[0].Rows[5][2].ToString();
                    RadioButtonList5.Items.Add(new ListItem(ds.Tables[0].Rows[5][3].ToString(), "1"));
                    RadioButtonList5.Items.Add(new ListItem(ds.Tables[0].Rows[5][4].ToString(), "2"));
                    RadioButtonList5.Items.Add(new ListItem(ds.Tables[0].Rows[5][5].ToString(), "3"));

                    Label17.Text = ds.Tables[0].Rows[6][2].ToString();
                    RadioButtonList6.Items.Add(new ListItem(ds.Tables[0].Rows[6][3].ToString(), "1"));
                    RadioButtonList6.Items.Add(new ListItem(ds.Tables[0].Rows[6][4].ToString(), "2"));
                    RadioButtonList6.Items.Add(new ListItem(ds.Tables[0].Rows[6][5].ToString(), "3"));

                    Label20.Text = ds.Tables[0].Rows[7][2].ToString();
                    RadioButtonList7.Items.Add(new ListItem(ds.Tables[0].Rows[7][3].ToString(), "1"));
                    RadioButtonList7.Items.Add(new ListItem(ds.Tables[0].Rows[7][4].ToString(), "2"));
                    RadioButtonList7.Items.Add(new ListItem(ds.Tables[0].Rows[7][5].ToString(), "3"));

                    Label23.Text = ds.Tables[0].Rows[8][2].ToString();
                    RadioButtonList8.Items.Add(new ListItem(ds.Tables[0].Rows[8][3].ToString(), "1"));
                    RadioButtonList8.Items.Add(new ListItem(ds.Tables[0].Rows[8][4].ToString(), "2"));
                    RadioButtonList8.Items.Add(new ListItem(ds.Tables[0].Rows[8][5].ToString(), "3"));

                    Label26.Text = ds.Tables[0].Rows[9][2].ToString();
                    RadioButtonList9.Items.Add(new ListItem(ds.Tables[0].Rows[9][3].ToString(), "1"));
                    RadioButtonList9.Items.Add(new ListItem(ds.Tables[0].Rows[9][4].ToString(), "2"));
                    RadioButtonList9.Items.Add(new ListItem(ds.Tables[0].Rows[9][5].ToString(), "3"));


                }
                ds.Clear();
                ds.Dispose();
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex);
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
        }
    }

    protected void btnGiveTest_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    q_no = Convert.ToInt32(HQno.Value.ToString());

                    int q_no2 = q_no + 1;
                    int q_no3 = q_no + 2;
                    int q_no4 = q_no + 3;
                    int q_no5 = q_no + 4;

                    int q_no6 = q_no + 5;
                    int q_no7 = q_no + 6;
                    int q_no8 = q_no + 7;
                    int q_no9 = q_no + 8;
                    int q_no10 = q_no + 9;




                    Hlang.Value.ToString();
                    int lng = 1;
                    if (Hlang.Value.ToString() == "en")
                        lng = 1;
                    else if (Hlang.Value.ToString() == "hi")
                        lng = 2;
                    else if (Hlang.Value.ToString() == "mr")
                        lng = 3;
                    else if (Hlang.Value.ToString() == "gu")
                        lng = 4;
                    else
                        lng = 1;



                    string str = "select qno,factor_no,questext,op1,op2,op3,opblue,opred from tblKYQuestions " +
                           "where qno >= " + HQno.Value.ToString() + " AND qno <= (" + HQno.Value.ToString() + " + 9) and langid =" + lng;

                    //DataSet ds = dbContext.ExecDataSet(str);
                    SqlDataAdapter da = new SqlDataAdapter(str, con);

                    DataSet ds = new DataSet();
                    //DataAdapter Object Fill DataSet
                    da.Fill(ds);


                    #region selected_option

                    int selected_option1 = 0, selected_option2 = 0, selected_option3 = 0, selected_option4 = 0, selected_option5 = 0;

                    selected_option1 = Convert.ToInt32(rblOptions.SelectedValue);

                    selected_option2 = Convert.ToInt32(RadioButtonList1.SelectedValue);

                    selected_option3 = Convert.ToInt32(RadioButtonList2.SelectedValue);

                    selected_option4 = Convert.ToInt32(RadioButtonList3.SelectedValue);

                    selected_option5 = Convert.ToInt32(RadioButtonList4.SelectedValue);


                    int selected_option6 = 0, selected_option7 = 0, selected_option8 = 0, selected_option9 = 0, selected_option10 = 0;

                    selected_option6 = Convert.ToInt32(RadioButtonList5.SelectedValue);

                    selected_option7 = Convert.ToInt32(RadioButtonList6.SelectedValue);

                    selected_option8 = Convert.ToInt32(RadioButtonList7.SelectedValue);

                    selected_option9 = Convert.ToInt32(RadioButtonList8.SelectedValue);

                    selected_option10 = Convert.ToInt32(RadioButtonList9.SelectedValue);

                    int factor_no1 = 0, factor_no2 = 0, factor_no3 = 0, factor_no4 = 0, factor_no5 = 0;


                    factor_no1 = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());

                    factor_no2 = Convert.ToInt32(ds.Tables[0].Rows[1][1].ToString());

                    factor_no3 = Convert.ToInt32(ds.Tables[0].Rows[2][1].ToString());

                    factor_no4 = Convert.ToInt32(ds.Tables[0].Rows[3][1].ToString());

                    factor_no5 = Convert.ToInt32(ds.Tables[0].Rows[4][1].ToString());


                    int factor_no6 = 0, factor_no7 = 0, factor_no8 = 0, factor_no9 = 0, factor_no10 = 0;


                    factor_no6 = Convert.ToInt32(ds.Tables[0].Rows[5][1].ToString());

                    factor_no7 = Convert.ToInt32(ds.Tables[0].Rows[6][1].ToString());

                    factor_no8 = Convert.ToInt32(ds.Tables[0].Rows[7][1].ToString());

                    factor_no9 = Convert.ToInt32(ds.Tables[0].Rows[8][1].ToString());

                    factor_no10 = Convert.ToInt32(ds.Tables[0].Rows[9][1].ToString());


                    int opt_blue1 = 0, opt_blue2 = 0, opt_blue3 = 0, opt_blue4 = 0, opt_blue5 = 0;


                    opt_blue1 = Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString());

                    opt_blue2 = Convert.ToInt32(ds.Tables[0].Rows[1][6].ToString());

                    opt_blue3 = Convert.ToInt32(ds.Tables[0].Rows[2][6].ToString());

                    opt_blue4 = Convert.ToInt32(ds.Tables[0].Rows[3][6].ToString());

                    opt_blue5 = Convert.ToInt32(ds.Tables[0].Rows[4][6].ToString());

                    int opt_blue6 = 0, opt_blue7 = 0, opt_blue8 = 0, opt_blue9 = 0, opt_blue10 = 0;


                    opt_blue6 = Convert.ToInt32(ds.Tables[0].Rows[5][6].ToString());

                    opt_blue7 = Convert.ToInt32(ds.Tables[0].Rows[6][6].ToString());

                    opt_blue8 = Convert.ToInt32(ds.Tables[0].Rows[7][6].ToString());

                    opt_blue9 = Convert.ToInt32(ds.Tables[0].Rows[8][6].ToString());

                    opt_blue10 = Convert.ToInt32(ds.Tables[0].Rows[9][6].ToString());


                    int opt_red1 = 0, opt_red2 = 0, opt_red3 = 0, opt_red4 = 0, opt_red5 = 0;


                    opt_red1 = Convert.ToInt32(ds.Tables[0].Rows[0][7].ToString());

                    opt_red2 = Convert.ToInt32(ds.Tables[0].Rows[1][7].ToString());

                    opt_red3 = Convert.ToInt32(ds.Tables[0].Rows[2][7].ToString());

                    opt_red4 = Convert.ToInt32(ds.Tables[0].Rows[3][7].ToString());

                    opt_red5 = Convert.ToInt32(ds.Tables[0].Rows[4][7].ToString());


                    int opt_red6 = 0, opt_red7 = 0, opt_red8 = 0, opt_red9 = 0, opt_red10 = 0;


                    opt_red6 = Convert.ToInt32(ds.Tables[0].Rows[5][7].ToString());

                    opt_red7 = Convert.ToInt32(ds.Tables[0].Rows[6][7].ToString());

                    opt_red8 = Convert.ToInt32(ds.Tables[0].Rows[7][7].ToString());

                    opt_red9 = Convert.ToInt32(ds.Tables[0].Rows[8][7].ToString());

                    opt_red10 = Convert.ToInt32(ds.Tables[0].Rows[9][7].ToString());


                    #endregion

                    int QMarks = 0;

                    str = "SELECT count([c_id]) FROM tblKYCandAnswers where c_id = " + Hcid.Value.ToString() + " and (q_no BETWEEN " + q_no + " AND " + q_no10 + ")  and batid=3";
                    //int i = Convert.ToInt32(dbContext.ExecScal(str));
                    SqlCommand cmd = new SqlCommand(str, con);
                    con.Open();
                    int i = Convert.ToInt32(cmd.ExecuteScalar());


                    if (i == 0)
                    {
                        ////Interest Test Answer Table
                        DataTable answertbl = new DataTable();
                        answertbl.Columns.Add("c_id", typeof(int));
                        answertbl.Columns.Add("q_no", typeof(int));
                        answertbl.Columns.Add("factor_no", typeof(int));
                        answertbl.Columns.Add("marks", typeof(int));
                        answertbl.Columns.Add("batid", typeof(int));


                        if (selected_option1 == opt_blue1)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option1 == opt_red1)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }
                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no, factor_no1.ToString(), QMarks, 3);


                        if (selected_option2 == opt_blue2)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option2 == opt_red2)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }

                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no2, factor_no2.ToString(), QMarks, 3);

                        if (selected_option3 == opt_blue3)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option3 == opt_red3)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }

                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no3, factor_no3.ToString(), QMarks, 3);

                        if (selected_option4 == opt_blue4)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option4 == opt_red4)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }

                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no4, factor_no4.ToString(), QMarks, 3);

                        if (selected_option5 == opt_blue5)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option5 == opt_red5)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }
                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no5, factor_no5.ToString(), QMarks, 3);

                        if (selected_option6 == opt_blue6)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option6 == opt_red6)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }
                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no6, factor_no6.ToString(), QMarks, 3);

                        if (selected_option7 == opt_blue7)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option7 == opt_red7)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }
                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no7, factor_no7.ToString(), QMarks, 3);

                        if (selected_option8 == opt_blue8)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option8 == opt_red8)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }
                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no8, factor_no8.ToString(), QMarks, 3);

                        if (selected_option9 == opt_blue9)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option9 == opt_red9)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }
                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no9, factor_no9.ToString(), QMarks, 3);
                        if (selected_option10 == opt_blue10)//opt_blue
                        {
                            QMarks = 2;
                        }
                        else if (selected_option10 == opt_red10)//opt_red
                        {
                            QMarks = 1;
                        }
                        else
                        {
                            QMarks = 0;
                        }
                        answertbl.Rows.Add(Hcid.Value.ToString(), q_no10, factor_no10.ToString(), QMarks, 3);


                        // con.Open();
                        using (var bulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
                        {
                            foreach (DataColumn dc in answertbl.Columns)
                            {
                                bulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                            }
                            bulkcopy.BulkCopyTimeout = 100000;
                            bulkcopy.DestinationTableName = "tblKYCandAnswers";
                            bulkcopy.WriteToServer(answertbl);
                        }
                    }

                    else
                    {

                    }

                    q_no = q_no + 10;
                    HQno.Value = q_no.ToString();

                    if (q_no > 90)//test completed
                    {
                        if (dbContext.generateKYTestFactor(Convert.ToInt32(Hcid.Value), 3, 9))
                        {
                            Response.Redirect("KY_test_complete.aspx", false);
                        }
                        else
                        {
                            //  factorStatus = "Incomplete";
                        }

                    }
                    else
                    {
                        //System.Threading.Thread.Sleep(5000);

                        load_questions();

                        //checknetcon();
                        //Session["q_no"] = q_no;

                        Session["uid"] = Hcid.Value.ToString();
                        // Response.Redirect("KY_test_page.aspx", false);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "closeWindow", "Confirmation();", true);
            }
        }
    }
}
