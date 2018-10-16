using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
    }

    protected void Button1_Click(object sender, EventArgs e) {
        String sqlString = TextBox1.Text;
        TextBox1.Text += utils.executeQuery(sqlString, null);
    }

    protected void Button2_Click(object sender, EventArgs e) {
        String sqlString = TextBox1.Text;
        DataSet ds = utils.getData(sqlString, null);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
}