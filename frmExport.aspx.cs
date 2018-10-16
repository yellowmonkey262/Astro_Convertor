using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmExport : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
        runExport();
    }

    private void runExport() {
        String loadExport = "update d set d.building = b.id from tblDevision d inner join tblbuildings b on d.building = b.building;";
        utils.executeQuery(loadExport, null);
        String fileDate = DateTime.Now.ToString("yyyyMMdd") + ".txt";
        String SaveName = "C:\\Inetpub\\FileImports\\ " + fileDate;
        if (!System.IO.Directory.Exists("C:\\Inetpub\\FileImports\\")) { System.IO.Directory.CreateDirectory("C:\\Inetpub\\FileImports\\"); }
        if (File.Exists(SaveName)) { File.Delete(SaveName); }
        FileStream fs = new FileStream(SaveName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        try {
            StreamWriter w = new StreamWriter(fs); // create a stream writer
            w.BaseStream.Seek(0, SeekOrigin.End); // set the file pointer to the end of file
            w.WriteLine("New File");
            w.Flush();
            w.Close();
            lblExportStatus.Text = "Export Succeeded";
        } catch (Exception ex) {
            lblExportStatus.Text = ex.Message;
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