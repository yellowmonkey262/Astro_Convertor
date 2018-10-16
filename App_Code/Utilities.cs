using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Utilities
/// </summary>
public class Utilities {
    private SqlConnection myConn;
    private SqlCommand myCmd;
    private SqlDataAdapter da;

    public Utilities() {
        myConn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AstrodonConnectionString"].ToString().Trim());
        myCmd = new SqlCommand();
        myCmd.Connection = myConn;
        myCmd.CommandType = CommandType.Text;
    }

    public String executeQuery(String Str, Dictionary<String, Object> sqlParams) {
        myCmd.CommandText = String.Empty;
        myCmd.CommandText = Str;
        myCmd.Parameters.Clear();
        String error = "";
        try {
            if (sqlParams != null) {
                foreach (KeyValuePair<String, Object> sqlParam in sqlParams) {
                    myCmd.Parameters.AddWithValue(sqlParam.Key, sqlParam.Value);
                }
            }
            myConn.Open();
            myCmd.ExecuteNonQuery();
        } catch (Exception ex) {
            error = ex.Message;
        } finally {
            myConn.Close();
        }
        return error;
    }

    public DataSet getData(String Str, Dictionary<String, Object> sqlParams) {
        myCmd.CommandText = String.Empty;
        myCmd.CommandText = Str;
        myCmd.Parameters.Clear();
        da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        if (sqlParams != null) {
            foreach (KeyValuePair<String, Object> sqlParam in sqlParams) {
                myCmd.Parameters.AddWithValue(sqlParam.Key, sqlParam.Value);
            }
        }
        try {
            myConn.Open();
            da.SelectCommand = myCmd;
            da.Fill(ds);
        } catch {
            ds = null;
        } finally {
            myConn.Close();
        }
        return ds;
    }
}