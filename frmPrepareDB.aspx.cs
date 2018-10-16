using System;

public partial class frmPrepareDB : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
    }

    protected void btnYes_Click(object sender, EventArgs e) {
        utils.executeQuery("DELETE FROM tblLedgerTransactions", null);
        utils.executeQuery("DELETE FROM tblPost", null);
        Response.Redirect("~/frmHome.aspx", true);
    }

    protected void btnNo_Click(object sender, EventArgs e) {
        Response.Redirect("~/frmHome.aspx", true);
    }
}