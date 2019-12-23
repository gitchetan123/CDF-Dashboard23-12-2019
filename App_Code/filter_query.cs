using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for filter_query
/// </summary>
public class  filter_query
{
    public filter_query()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string q1()
    {
        string strcmd1 = "SELECT fname,lname, email, dheyaEmail, contactNo , C.name as City, cdfLevel " +
             "FROM(select u.uId, ISNULL(SUM(amount),0) as TotalPayment from tblPayment as p " +
             "Right Outer Join tblUserMaster as u on p.uId = u.uId and userSource='DHEYA-CDF' " +
             "group by u.uId,u.userTypeId having u.userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS s " +
             "Left Outer Join(select * from tblUserMaster where userSource='DHEYA-CDF' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "') AS A on A.uId = s.uId " +
             "LEFT OUTER JOIN tblUserDetails AS B ON A.uId = B.uId " +
             "LEFT OUTER JOIN tblCitiesMaster AS C ON A.cityid = C.id " +
             "LEFT OUTER JOIN tblRelation AS R ON A.uId = R.uId  " +
             "LEFT OUTER JOIN tblUserProductMaster AS p ON A.uId = p.uId and p.prodid = 7 where cdfApproved='APPROVED' and userTypeId = '" + ConfigurationManager.AppSettings["userTypeId"] + "' ";

        return strcmd1;
    }
}