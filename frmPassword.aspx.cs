using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmPassword : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {
    }

    //                Response.Redirect("~/frmUnaMenu.aspx", true);

    protected void btnContinue_Click(object sender, EventArgs e) {
        String pwd = txtPassword.Text;
        Response.Redirect("frmUnaMenu.aspx?pwd=" + pwd, true);
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
}