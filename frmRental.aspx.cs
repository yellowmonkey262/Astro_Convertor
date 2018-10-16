using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmRental : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
        pnlUnallocated.Visible = true;
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

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e) {
        int rowIndex = int.Parse(e.CommandArgument.ToString());
        String cmd = e.CommandName;
        GridViewRow gvr = GridView4.Rows[rowIndex];
        int id = int.Parse(gvr.Cells[1].Text);
        if (cmd == "Allocate") {
            DropDownList allocation = gvr.FindControl("cmbAccount") as DropDownList;
            int allVal = int.Parse(allocation.SelectedValue);
            if (allVal != 0) {
                DateTime trnDate = DateTime.Parse(gvr.Cells[2].Text);
                String description = gvr.Cells[3].Text;
                double drVal = double.Parse(gvr.Cells[4].Text);
                double crVal = double.Parse(gvr.Cells[5].Text);
                double val = (drVal != 0 ? drVal * -1 : crVal);
                String[] accs = getAccounts(allVal);
                if (accs != null) {
                    String sql = "INSERT INTO tblRentalRecon(rentalId, trnDate, value, account, contra) VALUES(" + id.ToString();
                    sql += " , '" + trnDate.ToString() + "', " + val.ToString() + ", '" + accs[0] + "', '" + accs[1] + "')";
                    utils.executeQuery(sql, null);
                    SqlDataSource4.DataBind();
                    GridView4.DataBind();
                }
            }
        } else if (cmd == "Delete") {
        }
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
}

//BR CASH R0   CHQS          FEE