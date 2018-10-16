using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class upload : System.Web.UI.Page {

    protected void Page_Load(object sender, EventArgs e) {
    }

    protected void btnUpload_Click(object sender, EventArgs e) {
        try {
            String uploadLoc = "C:\\inetpub\\BankFiles\\";
            if (!Directory.Exists(uploadLoc)) { Directory.CreateDirectory(uploadLoc); }
            if (FileUpload1.HasFile) {
                String filename = FileUpload1.FileName.ToUpper();
                if (filename.EndsWith(".CSV")) {
                    FileUpload1.SaveAs(uploadLoc + FileUpload1.FileName);
                    if (File.Exists(uploadLoc + FileUpload1.FileName)) {
                        lblResult.Text = "Upload succeeded";
                    }
                } else {
                    lblResult.Text = "Upload failed.  Incorrect format.  Please note that the file must be in csv format.";
                }
            } else {
                lblResult.Text = "Upload failed.  No file selected.";
            }
        } catch (Exception ex) {
            lblResult.Text = "Upload failed: " + ex.Message;
        }
    }
}