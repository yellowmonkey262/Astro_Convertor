using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmUnallocated : System.Web.UI.Page {
    private Utilities utils = new Utilities();
    private String al = "";

    protected void Page_Load(object sender, EventArgs e) {
        String which = Request.QueryString["acc"];
        try {
            al = Request.QueryString["al"];
        } catch {
            al = "false";
        }
        allowAccess();
        GridView5.Columns[6].Visible = false;
    }

    private void Page_Error(object sender, EventArgs e) {
        Server.ClearError();
        Server.Transfer("~/frmUnallocated.aspx?al=true", true);
    }

    private void allowAccess() {
        if (al == "false") {
            Label12.Visible = false;
            lblTotal.Visible = false;
            btnExportUnall.Visible = false;
            btnAdd.Visible = false;
            lblError.Visible = false;
            GridView5.Visible = false;
            Label13.Visible = false;
            btnExportUnall0.Visible = false;
            GridView6.Visible = false;
        }
    }

    #region Menu Buttons

    protected void imgImport_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmHome.aspx?floc=import", true);
    }

    protected void imgAllocate_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmHome.aspx?floc=allocate", true);
    }

    protected void imgBuildings_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmBuildings.aspx", true);
    }

    protected void imgExport_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmExport.aspx", true);
    }

    protected void imgTrust_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmAccounts.aspx?acc=trust", true);
    }

    protected void imgUnAllocated_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmAccounts.aspx?acc=unalloc", true);
    }

    protected void imgAllocated_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmAccounts.aspx?acc=alloc", true);
    }

    protected void imgHome_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmHome.aspx", true);
    }

    protected void imgLogout_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/Default.aspx", true);
    }

    #endregion Menu Buttons

    private String GetOldReference(String lid) {
        String query = "SELECT reference FROM tblUnallocated WHERE lid = '" + lid + "'";
        DataSet ds = utils.getData(query, null);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
            return ds.Tables[0].Rows[0]["reference"].ToString();
        } else {
            return String.Empty;
        }
    }

    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e) {
        int rowIdx = int.Parse(e.CommandArgument.ToString());
        GridViewRow gvr = GridView5.Rows[rowIdx];
        int id = 0;
        int.TryParse(gvr.Cells[1].Text, out id);
        String lid = gvr.Cells[2].Text;
        if (e.CommandName == "allocate") {
            try {
                String building = (gvr.FindControl("cmbBuilding") as DropDownList).SelectedValue;
                String reference = (gvr.FindControl("txtRef") as TextBox).Text;
                DateTime trnDate = DateTime.Now;
                String amt = (gvr.FindControl("txtAmount") as TextBox).Text;
                String description = gvr.Cells[4].Text;
                int period = int.Parse((gvr.FindControl("cmbPeriod") as DropDownList).SelectedValue);
                DateTime dt = DateTime.Parse(gvr.Cells[3].Text);
                String b2 = (building == "UNA" ? "" : building);

                String oldReference = description;

                if (period != 0 && reference != "") {
                    String sql = "IF EXISTS (SELECT * FROM tblUnallocated WHERE lid = " + lid + ")";
                    sql += " UPDATE tblUnallocated SET amount = '" + amt + "', allocatedDate = '" + trnDate.ToString("yyyy/MM/dd") + "', allocated = 'True', allocatedCode = '" + b2 + "', ";
                    sql += " reference = '" + reference + "', period = " + period + " WHERE (lid = " + lid + ")";
                    sql += " ELSE ";
                    sql += " INSERT INTO tblUnallocated(lid, trnDate, amount, description, reference, period, allocatedDate, allocatedCode, trustpost)";
                    sql += " VALUES(" + lid + ", '" + gvr.Cells[3].Text + "', " + amt.ToString() + ", '" + description + "', '" + reference + "', " + period.ToString();
                    sql += ", '" + trnDate + "', '" + b2 + "', 'False');";
                    sql += " UPDATE tblLedgerTransactions SET allocate = 'True' WHERE id = " + lid;
                    //lblError.Text = sql;
                    String sqlReply = utils.executeQuery(sql, null);
                    if (sqlReply == "" && b2 != "") {
                        getBuilding(lid, trnDate.ToString("dd/MM/yyyy"), amt, description, reference, period.ToString(), building, true, dt, oldReference);
                    } else {
                        lblError.Text = sqlReply;
                    }
                } else {
                    lblError.Text = "----";
                }
            } catch (Exception ex) {
                lblError.Text = ex.Message;
            }
        } else if (e.CommandName == "Remove") {
            int counter = 0;
            DataSet ds = utils.getData("SELECT count(*) as dupes FROM tblUnallocated WHERE lid = " + lid, null);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
                counter = int.Parse(ds.Tables[0].Rows[0]["dupes"].ToString());
            }
            String DeleteCommand = "DELETE FROM tblUnallocated WHERE id = " + id.ToString() + ";";
            if (counter < 2) { DeleteCommand += " DELETE FROM tblLedgerTransactions WHERE id = " + lid; }
            utils.executeQuery(DeleteCommand, null);
            SqlDataSource5.Select(new DataSourceSelectArguments());
            SqlDataSource5.DataBind();
            GridView5.DataBind();
        }
    }

    private void getBuilding(String lid, String trnDate, String amount, String description, String reference, String period, String code, bool alloc, DateTime dt, String oldReference) {
        String sql = "SELECT Building, Code, AccNumber, Contra, DataPath FROM tblBuildings WHERE (Code = '" + code + "') ";
        DataSet ds = utils.getData(sql, null);
        if (!alloc) { trnDate = dt.ToString("dd/MM/yyyy"); }
        if ((ds != null) && (ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0)) {
            DataRow dr = ds.Tables[0].Rows[0];
            String sql2 = "INSERT INTO tblExport (lid, trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period, una)";
            sql2 += " VALUES('" + lid + "', '" + trnDate + "', '" + amount + "', '" + dr["Building"].ToString() + "', '" + code + "', '" + description + "', '" + reference;
            sql2 += "', '" + dr["AccNumber"].ToString() + "', '" + dr["Contra"].ToString() + "', '" + dr["DataPath"].ToString() + "', '" + period + "', '" + alloc.ToString() + "');";

            sql2 += "INSERT INTO tblMatch(statementRef, astroRef) VALUES('" + oldReference + "','" + reference + "')";
            //lblError.Text = sql2;
            String sqlReply = utils.executeQuery(sql2, null);
            if (sqlReply == "") {
                SqlDataSource5.Select(new DataSourceSelectArguments());
                SqlDataSource5.DataBind();
                GridView5.DataBind();
                SqlDataSource1.Select(new DataSourceSelectArguments());
                SqlDataSource1.DataBind();
                GridView6.DataBind();
            } else {
                lblError.Text = sqlReply;
            }
        }
    }

    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e) {
        //gvr.Cells[5].Text;
        double gAmt = 0;
        foreach (GridViewRow gvr in GridView5.Rows) {
            String amount = (gvr.FindControl("txtAmount") as TextBox).Text;
            double amt = double.Parse(amount);
            gAmt += amt;
        }
        lblTotal.Text = gAmt.ToString();
    }

    protected void btnExportUnall_Click(object sender, EventArgs e) {
        ExportToExcel(1);
    }

    protected void btnExportUnall0_Click(object sender, EventArgs e) {
        ExportToExcel(2);
    }

    protected void ExportToExcel(int gridNo) {
        GridView gv = null;
        String fName = "";
        switch (gridNo) {
            case 1:
                gv = GridView5;
                gv.Columns[0].Visible = false;
                gv.Columns[5].Visible = false;
                gv.Columns[6].Visible = true;
                gv.Columns[7].Visible = false;
                gv.Columns[8].Visible = false;
                gv.Columns[9].Visible = false;
                gv.Columns[10].Visible = false;
                fName = "Unallocated";
                break;

            case 2:
                gv = GridView6;
                fName = "Allocations";
                break;
        }
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=" + fName + ".xls");
        Response.Charset = "";
        this.EnableViewState = false;

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
        gv.RenderControl(htw);
        String htmlText = sw.ToString();
        Response.Write(htmlText);
        Response.End();
        if (gridNo == 1) {
            gv.Columns[0].Visible = true;
            gv.Columns[5].Visible = true;
            gv.Columns[6].Visible = false;
            gv.Columns[7].Visible = true;
            gv.Columns[8].Visible = true;
            gv.Columns[9].Visible = true;
            gv.Columns[10].Visible = true;
        }
    }

    public override void VerifyRenderingInServerForm(Control control) {
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e) {
        int rowIdx = int.Parse(e.CommandArgument.ToString());
        GridViewRow gvr = GridView1.Rows[rowIdx];
        int id = 0;
        int.TryParse(gvr.Cells[1].Text, out id);
        String lid = gvr.Cells[2].Text;
        if (e.CommandName == "allocate") {
            try {
                String building = (gvr.FindControl("cmbBuilding") as DropDownList).SelectedValue;
                String reference = (gvr.FindControl("txtRef") as TextBox).Text;
                DateTime trnDate = DateTime.Now;
                String amt = gvr.Cells[5].Text;
                String description = gvr.Cells[4].Text;
                int period = int.Parse((gvr.FindControl("cmbPeriod") as DropDownList).SelectedValue);
                bool alloc = false;
                DateTime dt = DateTime.Parse(gvr.Cells[3].Text);
                String build = building;
                String b2 = (building == "UNA" ? "" : building);
                String oldReference = description;
                if (period != 0 && reference != "") {
                    String sql = "IF EXISTS (SELECT * FROM tblUnallocated WHERE lid = " + lid + ")";
                    sql += " UPDATE tblUnallocated SET amount = '" + amt + "', building = '" + building + "', allocatedDate = '" + trnDate.ToString("yyyy/MM/dd") + "', allocated = 'True', allocatedCode = '" + b2 + "', reference = '" + reference + "', period = " + period + " WHERE (lid = " + lid + ")";
                    sql += " ELSE ";
                    sql += " INSERT INTO tblUnallocated(lid, trnDate, building, amount, description, reference, period, allocatedDate, allocatedCode, trustpost)";
                    sql += " VALUES(" + lid + ", '" + gvr.Cells[3].Text + "', '" + building + "', " + amt.ToString() + ", '" + description + "', '" + reference + "', " + period.ToString();
                    sql += ", '" + trnDate + "', '" + b2 + "', 'False');";
                    sql += " UPDATE tblLedgerTransactions SET allocate = 'True' WHERE id = " + lid;
                    String sqlReply = utils.executeQuery(sql, null);
                    if (sqlReply == "") {
                        getBuilding(lid, trnDate.ToString("dd/MM/yyyy"), amt, description, reference, period.ToString(), building, alloc, dt, oldReference);
                    } else {
                        lblError.Text = sqlReply;
                    }
                }
            } catch { }
        } else if (e.CommandName == "Remove") {
            String DeleteCommand = "DELETE FROM tblUnallocated WHERE id = " + id.ToString() + "; DELETE FROM tblLedgerTransactions WHERE id = " + lid;
            utils.executeQuery(DeleteCommand, null);
            SqlDataSource5.Select(new DataSourceSelectArguments());
            SqlDataSource5.DataBind();
            GridView1.DataBind();
        }
        SqlDataSource6.Select(new DataSourceSelectArguments());
        GridView1.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e) {
        Response.Redirect("frmUnal.aspx", true);
    }
}