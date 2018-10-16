using System;
using System.Web.UI;

public partial class frmUnaMenu : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
        try {
            String pwd = Request.QueryString["pwd"];
            if (pwd != "45828") {
                Response.Redirect("~/frmUnallocated.aspx?al=false", true);
            }
        } catch {
            Response.Redirect("~/frmUnallocated.aspx?al=false", true);
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

    protected void btnUna_Click(object sender, EventArgs e) {
        Response.Redirect("~/frmUnallocated.aspx?al=true", true);
    }

    protected void btnRental_Click(object sender, EventArgs e) {
        Response.Redirect("~/frmRental.aspx", true);
    }
}

//BR CASH R0   CHQS          FEE