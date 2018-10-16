using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class DataHandler {
    private String connString = ConfigurationManager.ConnectionStrings["AstrodonConnectionString"].ConnectionString;
    private SqlConnection conn;
    private SqlCommand cmd;
    private SqlDataAdapter da;
    private String msg;

    public DataHandler() {
        conn = new SqlConnection(connString);
        cmd = new SqlCommand();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = conn;
        da = new SqlDataAdapter();
    }

    public DataSet GetData(String query, Dictionary<String, Object> sqlParms, out String msg) {
        DataSet ds = new DataSet();
        cmd.Parameters.Clear();
        try {
            cmd.CommandText = query;
            if (sqlParms != null) {
                foreach (KeyValuePair<String, Object> sqlParm in sqlParms) {
                    cmd.Parameters.AddWithValue(sqlParm.Key, sqlParm.Value);
                }
            }
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            da.SelectCommand = cmd;
            da.Fill(ds);
            msg = "";
        } catch (Exception ex) {
            msg = ex.Message;
            ds = null;
        } finally {
            if (conn.State == ConnectionState.Open) { conn.Close(); }
        }
        return ds;
    }

    public int SetData(String query, Dictionary<String, Object> sqlParms, out String msg) {
        cmd.Parameters.Clear();
        int rs = 0;
        try {
            cmd.CommandText = query;
            if (sqlParms != null) {
                foreach (KeyValuePair<String, Object> sqlParm in sqlParms) {
                    cmd.Parameters.AddWithValue(sqlParm.Key, sqlParm.Value);
                }
            }
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            rs = cmd.ExecuteNonQuery();
            msg = "";
        } catch (Exception ex) {
            msg = ex.Message;
            rs = -1;
        } finally {
            if (conn.State == ConnectionState.Open) { conn.Close(); }
        }
        return rs;
    }

    public bool Authenticate(String username, String password) {
        String query = String.Format("SELECT id FROM tblUsers WHERE username = '{0}' AND password = '{1}'", username, password);
        DataSet dsUsers = GetData(query, null, out msg);
        if (dsUsers != null && dsUsers.Tables.Count > 0 && dsUsers.Tables[0].Rows.Count > 0) {
            return true;
        } else {
            return false;
        }
    }

    public int SaveOutboundMessage(int id, String building, String customer, String number, String reference, String message, bool billable, bool bulkbillable,
        DateTime sent, String sender, String astStatus, String batchID, String status, DateTime nextPolled, int pollCount, double cbal, String smsType, out String msg) {
        Dictionary<String, Object> sqlParms = new Dictionary<string, object>();
        sqlParms.Add("@id", id);
        sqlParms.Add("@building", building);
        sqlParms.Add("@customer", customer);
        sqlParms.Add("@number", number);
        sqlParms.Add("@reference", reference);
        sqlParms.Add("@message", message);
        sqlParms.Add("@billable", billable);
        sqlParms.Add("@bulkbillable", bulkbillable);
        sqlParms.Add("@sent", sent);
        sqlParms.Add("@sender", sender);
        sqlParms.Add("@astStatus", astStatus);
        sqlParms.Add("@batchID", batchID);
        sqlParms.Add("@status", "-1");
        sqlParms.Add("@cbal", cbal);
        sqlParms.Add("@smstype", smsType);
        sqlParms.Add("@nextPolled", (id == 0 ? DateTime.Now.AddMinutes(5) : nextPolled));
        sqlParms.Add("@pollCount", (id == 0 ? 0 : pollCount));

        if (id == 0) {
            String insertQuery = "INSERT INTO tblSMS(building, customer, number, reference, message, billable, bulkbillable, sent, sender, astStatus, batchID, status, nextPolled, pollCount, currentBalance, smsType) ";
            insertQuery += " VALUES(@building, @customer, @number, @reference, @message, @billable, @bulkbillable, @sent, @sender, @astStatus, @batchID, @status, @nextPolled, @pollCount, @cbal, @smstype);";
            insertQuery += " SELECT @@IDENTITY;";
            DataSet ds = GetData(insertQuery, sqlParms, out msg);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            } else {
                return -1;
            }
        } else {
            String updateQuery = "UPDATE tblSMS SET building = @building, customer = @customer, number = @number, reference = @reference, message = @message, billable = @billable, ";
            updateQuery += " bulkbillable = @bulkbillable, sent = @sent, sender = @sender, astStatus = @astStatus, batchID = @batchID, status = @status, nextPolled = @nextPolled, ";
            updateQuery += " pollCount = @pollCount, currentBalance = @cbal, smsType = @smstype WHERE id = @id";
            return SetData(updateQuery, sqlParms, out msg);
        }
    }

    public int SaveQueuedMessage(String building, String customer, double currentBalance, String smsType, String number, String reference, String message, bool billable, bool bulkbillable, DateTime sent,
        String sender, String astStatus, String batchID, String status) {
        Dictionary<String, Object> sqlParms = new Dictionary<string, object>();

        sqlParms.Add("@building", building);
        sqlParms.Add("@customer", customer);
        sqlParms.Add("@number", number);
        sqlParms.Add("@reference", reference);
        sqlParms.Add("@message", message);
        sqlParms.Add("@billable", billable);
        sqlParms.Add("@bulkbillable", bulkbillable);
        sqlParms.Add("@sent", sent);
        sqlParms.Add("@sender", sender);
        sqlParms.Add("@astStatus", astStatus);
        sqlParms.Add("@batchID", batchID);
        sqlParms.Add("@status", status);
        sqlParms.Add("@cbal", currentBalance);
        sqlParms.Add("@smstype", smsType);
        sqlParms.Add("@nextPolled", sent);
        sqlParms.Add("@pollCount", 0);

        String insertQuery = "INSERT INTO tblSMS(building, customer, number, reference, message, billable, bulkbillable, sent, sender, astStatus, batchID, status, nextPolled, pollCount, currentBalance, smsType) ";
        insertQuery += " VALUES(@building, @customer, @number, @reference, @message, @billable, @bulkbillable, @sent, @sender, @astStatus, @batchID, @status, @nextPolled, @pollCount, @cbal, @smstype);";
        insertQuery += " SELECT @@IDENTITY;";
        DataSet ds = GetData(insertQuery, sqlParms, out msg);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        } else {
            return -1;
        }
    }
}