using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

/// <summary>
/// Summary description for DebtorFlow
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
// [System.Web.Script.Services.ScriptService]
public class DebtorFlow : System.Web.Services.WebService {
    private Utilities utils;

    public DebtorFlow() {
        utils = new Utilities();
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public bool login(String username, String password, out int userid) {
        String sqlCommand = "SELECT id FROM tblUsers WHERE username = '" + username + "' AND password = '" + password + "'";
        DataSet ds = utils.getData(sqlCommand, null);
        bool success = false;
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
            DataRow dr = ds.Tables[0].Rows[0];
            userid = (int)dr["id"];
            success = true;
        } else {
            userid = 0;
        }
        return success;
    }

    [WebMethod]
    public DataSet getBuildings(int userid) {
        String sqlCommand = "SELECT tblBuildings.id, tblBuildings.Building, tblBuildings.Code, tblBuildings.DataPath, tblBuildings.Period, tblBuildings.journals";
        sqlCommand += " FROM tblUserBuildings INNER JOIN tblBuildings ON tblUserBuildings.buildingid = tblBuildings.id";
        sqlCommand += " WHERE (tblUserBuildings.userid = " + userid.ToString() + ") ORDER BY tblBuildings.Building";
        DataSet ds = utils.getData(sqlCommand, null);
        return ds;
    }

    [WebMethod]
    public bool insertLog(int userid, int buildingid, String customerCode, DateTime trnDate, double amount, String description, out String error) {
        String sqlCommand = "INSERT INTO tblUserLog(userid, buildingid, customercode, trnDate, amount, description)";
        sqlCommand += " VALUES(@userid, @buildingid, @customercode, @trnDate, @amount, @description)";
        Dictionary<String, Object> sqlParms = new Dictionary<string, object>();
        sqlParms.Add("@userid", userid);
        sqlParms.Add("@buildingid", buildingid);
        sqlParms.Add("@customercode", customerCode);
        sqlParms.Add("@trnDate", trnDate);
        sqlParms.Add("@amount", amount);
        sqlParms.Add("@description", description);
        error = utils.executeQuery(sqlCommand, sqlParms);
        if (error == "") {
            return true;
        } else {
            return false;
        }
    }

    [WebMethod]
    public DataSet getLog(int userid, int buildingid, String customerCode, DateTime trnDate, double amount, String description) {
        String sqlCommand = "SELECT * FROM tblUserLog";
        int counter = 0;
        if (userid != 0) {
            sqlCommand += " WHERE (userid = @userid)";
            counter++;
        }
        if (buildingid != 0) {
            if (counter > 0) {
                sqlCommand += " AND ";
            } else {
                sqlCommand += " WHERE ";
            }
            sqlCommand += " (buildingid = @buildingid)";
            counter++;
        }
        if (customerCode != "") {
            if (counter > 0) {
                sqlCommand += " AND ";
            } else {
                sqlCommand += " WHERE ";
            }
            sqlCommand += " (customercode = @customercode)";
            counter++;
        }
        if (trnDate != DateTime.Parse("1900/01/01")) {
            if (counter > 0) {
                sqlCommand += " AND ";
            } else {
                sqlCommand += " WHERE ";
            }
            sqlCommand += " (trnDate = @trnDate)";
            counter++;
        }
        if (amount != 0) {
            if (counter > 0) {
                sqlCommand += " AND ";
            } else {
                sqlCommand += " WHERE ";
            }
            sqlCommand += " (amount = @amount)";
            counter++;
        }
        if (description != "") {
            if (counter > 0) {
                sqlCommand += " AND ";
            } else {
                sqlCommand += " WHERE ";
            }
            sqlCommand += " (description = @description)";
            counter++;
        }
        Dictionary<String, Object> sqlParms = new Dictionary<string, object>();
        sqlParms.Add("@userid", userid);
        sqlParms.Add("@buildingid", buildingid);
        sqlParms.Add("@customercode", customerCode);
        sqlParms.Add("@trnDate", trnDate);
        sqlParms.Add("@amount", amount);
        sqlParms.Add("@description", description);
        return utils.getData(sqlCommand, sqlParms);
    }
}