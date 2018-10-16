using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {
    private String username = "admin";
    private String password = "n1md@";

    protected void Page_Load(object sender, EventArgs e) {
        try {
            bool logout = bool.Parse(Request.QueryString["lg"].Trim());
            if (logout) {
                Response.Cookies["LoggedIn"].Expires = DateTime.Now.AddDays(-1);
                Response.Redirect("~/Default.aspx");
            }
        } catch { }
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "no-cache");
        Response.Expires = -1;
    }

    protected void btnLogin_Click(object sender, EventArgs e) {
        if (txtUname.Text == username && txtPword.Text == password) {
            HttpCookie loggedIn = new HttpCookie("LoggedIn");
            loggedIn.Value = "True";
            Response.Cookies.Add(loggedIn);
            Response.Redirect("frmPrepareDB.aspx", true);
        } else {
            lblMessage.Text = "Incorrect username or password!";
        }
    }
}