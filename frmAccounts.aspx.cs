using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAccounts : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
        String which = Request.QueryString["acc"];
        pnlImports.Visible = false;
        pnlRental.Visible = false;
        pnlAllocated.Visible = false;
        switch (which) {
            case "trust":
                pnlImports.Visible = true;
                pnlRental.Visible = true;
                break;

            case "unalloc":
                Response.Redirect("~/frmPassword.aspx", true);
                break;

            case "alloc":
                pnlAllocated.Visible = true;
                break;
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

    private string[] getBuildingStuff(String code) {
        String[] buildingStuff = new String[5];
        String sql = "SELECT Building, Code, AccNumber, DataPath, Contra FROM tblBuildings WHERE Code = '" + code + "'";
        DataSet ds = utils.getData(sql, null);
        if (ds != null) {
            try {
                buildingStuff[0] = ds.Tables[0].Rows[0]["Building"].ToString();
                buildingStuff[1] = ds.Tables[0].Rows[0]["Code"].ToString();
                buildingStuff[2] = ds.Tables[0].Rows[0]["AccNumber"].ToString();
                buildingStuff[3] = ds.Tables[0].Rows[0]["DataPath"].ToString();
                buildingStuff[4] = ds.Tables[0].Rows[0]["Contra"].ToString();
            } catch { }
        }
        return buildingStuff;
    }

    protected void grdAllocated_RowUpdated(object sender, GridViewUpdatedEventArgs e) {
        String loadExport1 = "UPDATE d SET d.building = b.id FROM tblDevision d JOIN tblBuildings b ON d.building = b.code or d.Building = b.building;";
        utils.executeQuery(loadExport1, null);
        String code = e.NewValues["code"].ToString();
        String id = e.Keys["id"].ToString();
        String[] buildingStuff = getBuildingStuff(code);
        String sql = "UPDATE tblExport SET building = '" + buildingStuff[0] + "', accnumber = '" + buildingStuff[2];
        sql += "', contra = '" + buildingStuff[4] + "', datapath = '" + buildingStuff[3] + "' WHERE id = " + id + ";";
        if (code == "UNA") {
            sql += " INSERT INTO tblUnallocated(lid, trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period) ";
            sql += " SELECT lid, trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period FROM tblExport WHERE id = " + id + " AND lid NOT IN (SELECT lid FROM tblUnallocated);";
            sql += " UPDATE tblLedgerTransactions SET allocate = 'False' WHERE id = (SELECT lid FROM tblExport WHERE id = " + id + ");";
        }
        utils.executeQuery(sql, null);
        SqlDataSource2.DataBind();
        grdAllocated.DataBind();
        String str = " update div set div.Building = exo.building, div.AccNumber = exo.accnumber, div.Reference = exo.reference";
        str += " from tblDevision as div inner join tblExport exo on div.lid = exo.lid where exo.id = " + id;
        utils.executeQuery(str, null);
        utils.executeQuery(loadExport1, null);
    }

    private String[] getAccounts(int allVal) {
        String sql = "SELECT crAccount, crContra FROM tblRentalAccounts WHERE id = " + allVal.ToString();
        DataSet dsAcc = utils.getData(sql, null);
        if (dsAcc != null) {
            String acc = dsAcc.Tables[0].Rows[0]["crAccount"].ToString();
            String contra = dsAcc.Tables[0].Rows[0]["crContra"].ToString();
            String[] accs = new String[] { acc, contra };
            return accs;
        } else {
            return null;
        }
    }

    protected void Button1_Command(object sender, CommandEventArgs e) {
        Button btn = sender as Button;
        bool isUna = (btn.ID == "Button2" ? true : false);
        GridViewRow gvr = btn.Parent.Parent as GridViewRow;
        String id = gvr.Cells[1].Text;
        String date = gvr.Cells[2].Text;
        if (isUna) { date = DateTime.Now.ToString("dd/MM/yyyy"); }
        String description = gvr.Cells[3].Text;
        String amount = gvr.Cells[4].Text;
        String code = (gvr.FindControl("cmbBuilding") as DropDownList).SelectedValue;
        String reference = (gvr.FindControl("txtReference") as TextBox).Text;
        String period = (gvr.FindControl("cmbPeriod") as DropDownList).SelectedValue;
        String[] buildingStuff = getBuildingStuff(code);
        String insertSql = "INSERT INTO tblExport(lid, trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period, una)";
        insertSql += " VALUES (" + id + ", '" + date + "', '" + amount + "', '" + buildingStuff[0] + "', '" + code + "', '" + description;
        insertSql += "', '" + reference + "', '" + buildingStuff[2] + "', '" + buildingStuff[4] + "', '" + buildingStuff[3] + "', '" + period + "', '" + isUna.ToString() + "');";

        if (code == "UNA") {
            String unaSql = " IF NOT EXISTS(SELECT * FROM tblUnallocated WHERE lid = " + id + ")";
            unaSql += " INSERT INTO tblUnallocated(lid, trnDate, amount, description, reference, period, allocatedDate, allocatedCode, trustpost)";
            unaSql += " VALUES(" + id + ", '" + date + "', " + amount + ", '" + description + "', '" + reference + "', " + period.ToString() + ", '" + date + "', '" + code + "', 'False');";
            utils.executeQuery(unaSql, null);
        }
        insertSql += " IF NOT EXISTS (SELECT * FROM tblDevision WHERE lid = " + id + ")";
        insertSql += " BEGIN";
        insertSql += " INSERT INTO tblDevision (Date, Description, Amount, Balance, FromAccNumber, AccDescription, StatementNr, Allocate, Reference, Building, AccNumber, Period, lid)";
        insertSql += " VALUES('" + date + "', '" + description + "', " + amount + ", 0, '', '', '', 1, '" + reference + "', '" + code + "', '" + buildingStuff[2] + "', " + period + ", " + id + ")";
        insertSql += " END ";
        insertSql += " ELSE ";
        insertSql += " BEGIN";
        insertSql += " update div set div.Building = exo.building, div.AccNumber = exo.accnumber, div.Reference = exo.reference";
        insertSql += " from tblDevision as div inner join tblExport exo on div.lid = exo.lid where exo.lid = " + id;
        insertSql += " END ;";
        insertSql += " UPDATE d SET d.building = b.id FROM tblDevision d JOIN tblBuildings b ON d.building = b.code or d.Building = b.building;";
        if (isUna) { insertSql += " DELETE FROM tblUnallocated WHERE lid = " + id + ";"; }
        insertSql += "UPDATE tblLedgerTransactions SET Allocate = 1 WHERE id = " + id;
        utils.executeQuery(insertSql, null);
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e) {
    }

    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e) {
    }
}