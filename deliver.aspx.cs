using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class deliver : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        
        DataHandler dh = new DataHandler();
        Dictionary<String, Object> sqlParms = new Dictionary<string, object>();
        String status = "OK";

        try {
            String fullQuery = Request.Url.Query;
            String sqlQuery = "INSERT INTO tblTest(Response) VALUES(@response)";
            sqlParms.Add("@response", fullQuery);
            dh.SetData(sqlQuery, sqlParms, out status);

            String pass = Request.QueryString["pass"];

            if (pass == "m3t@") {



                bool isMessageRequest = Request.Url.AbsoluteUri.IndexOf("msg_id=") >= 0 ? true : false;
                sqlParms.Clear();
                if (isMessageRequest) {
                    String msg_id = Request.QueryString["msg_id"];
                    String msisdn = Request.QueryString["msisdn"];
                    String msgstatus = Request.QueryString["status"];
                    String batch_id = Request.QueryString["batch_id"];
                    String unique_id = Request.QueryString["unique_id"];
                    String completed_time = Request.QueryString["completed_time"];
                    sqlQuery = "INSERT INTO tblSMS(msg_id, msisdn, status, batch_id, unique_id, completed_time) VALUES(@msg_id, @msisdn, @status, @batch_id, @unique_id, @completed_time)";
                    sqlParms.Add("@msg_id", msg_id);
                    sqlParms.Add("@msisdn", msisdn);
                    sqlParms.Add("@status", msgstatus);
                    sqlParms.Add("@batch_id", batch_id);
                    sqlParms.Add("@unique_id", unique_id);
                    sqlParms.Add("@completed_time", completed_time);
                    dh.SetData(sqlQuery, sqlParms, out status);


                }
            }
        } catch {
        }
        Response.Write(status);
    }
}