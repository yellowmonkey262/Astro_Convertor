using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmUnal : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
    }

    //insert into tblunallocated(lid, trndate, amount, description, allocatedBuilding, allocatedCode)
    //select id, Date, Amount, Description, '' as allocatedBuilding, '' as allocatedCode from tblledgertransactions where id in (10662,11721)
    protected void btnBack_Click(object sender, EventArgs e) {
        Response.Redirect("frmUnallocated.aspx", true);
    }

    protected void btnAdd_Click(object sender, EventArgs e) {
        String recID = txtRec.Text;
        if (recID != "") {
            String sql = " insert into tblunallocated(lid, trndate, amount, description, allocatedBuilding, allocatedCode)";
            sql += " select id, Date, Amount, Description, '' as allocatedBuilding, '' as allocatedCode from tblledgertransactions where id = " + recID;
            String error = utils.executeQuery(sql, null);
            if (error != "") {
                lblAdd1.Text = error;
            } else {
                lblAdd1.Text = "Entry added";
            }
        }
    }

    protected void btnAdd2_Click(object sender, EventArgs e) {
        String date = txtADate.Text;
        String amt = txtAAmt.Text;
        String desc = txtADesc.Text;
        if (date != "" && amt != "" && desc != "") {
            String sql = "INSERT INTO tblLedgerTransactions(Date, Amount, Description, Allocate) VALUES(";
            sql += "'" + date + "', " + amt + ", '" + desc + "', '1')";
            String e1 = utils.executeQuery(sql, null);
            if (e1 == "") {
                sql = String.Format("SELECT id FROM tblLedgerTransactions WHERE Date = '{0}' AND Amount = {1} AND Description = '{2}'", date, amt, desc);
                DataSet ds = utils.getData(sql, null);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                    DataRow dr = ds.Tables[0].Rows[0];
                    String id = dr["id"].ToString();
                    sql = " insert into tblunallocated(lid, trndate, amount, description, allocatedBuilding, allocatedCode)";
                    sql += " select id, Date, Amount, Description, '' as allocatedBuilding, '' as allocatedCode from tblledgertransactions where id = " + id;
                    String error = utils.executeQuery(sql, null);
                    if (error != "") {
                        lblAdd2.Text = error;
                    } else {
                        lblAdd2.Text = "Entry added";
                    }
                } else {
                    lblAdd2.Text = "No records found";
                }
            } else {
                lblAdd2.Text = e1;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e) {
        String date = txtSDate.Text;
        String amt = txtSAmt.Text;
        String desc = txtSDesc.Text;

        if (date != "" || amt != "" || desc != "") {
            String sql = "select id, Date, Amount, Description, '' as allocatedBuilding, '' as allocatedCode from tblledgertransactions";
            int andCount = 0;
            if (date != "") {
                sql += " WHERE Date = '" + date + "'";
                andCount++;
            }
            if (amt != "") {
                sql += (andCount > 0 ? " AND " : " WHERE ");
                sql += " Amount = " + amt;
                andCount++;
            }
            if (desc != "") {
                sql += (andCount > 0 ? " AND " : " WHERE ");
                sql += " Description LIKE '%" + desc + "%'";
                andCount++;
            }
            DataSet ds = utils.getData(sql, null);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
    }
}