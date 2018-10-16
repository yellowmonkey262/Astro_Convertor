using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmBuildings : System.Web.UI.Page {
    private Utilities utils = new Utilities();
    private List<SqlParameter> insertParameters = new List<SqlParameter>();

    protected void Page_Load(object sender, EventArgs e) {
        if (!this.IsCallback && !this.IsPostBack) {
            pnlBuildingCodes.Visible = true;
            pnlRentals.Visible = false;
            pnlBankCharges.Visible = false;
            pnlJournals.Visible = false;
        }
        DataSet dsJnl = utils.getData("SELECT * FROM tblBankCharges", null);
        LoadServiceFee();
    }

    #region Menu Buttons

    protected void imgImport_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmHome.aspx?floc=import", true);
    }

    protected void imgAllocate_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmHome.aspx?floc=allocate", true);
    }

    protected void imgBuildings_Click(object sender, ImageClickEventArgs e) {
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

    #endregion Menu Buttons

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "InsertNew") {
            insertParameters.Clear();
            TextBox buildName = GridView1.FooterRow.FindControl("txtNewBuild") as TextBox;
            TextBox buildCode = GridView1.FooterRow.FindControl("txtNewCode") as TextBox;
            TextBox acc = GridView1.FooterRow.FindControl("txtNewAcc") as TextBox;
            TextBox bank = GridView1.FooterRow.FindControl("txtNewBank") as TextBox;
            TextBox period = GridView1.FooterRow.FindControl("txtNewPeriod") as TextBox;
            TextBox path = GridView1.FooterRow.FindControl("txtNewPath") as TextBox;
            TextBox payments = GridView1.FooterRow.FindControl("txtNewPayment") as TextBox;
            TextBox receipts = GridView1.FooterRow.FindControl("txtNewReceipt") as TextBox;

            TextBox pm = GridView1.FooterRow.FindControl("txtNewPM") as TextBox;
            TextBox bankName = GridView1.FooterRow.FindControl("txtNewBankName") as TextBox;
            TextBox accName = GridView1.FooterRow.FindControl("txtNewAccName") as TextBox;
            TextBox accNumber = GridView1.FooterRow.FindControl("txtNewAccNumber") as TextBox;
            TextBox branch = GridView1.FooterRow.FindControl("txtNewBranch") as TextBox;

            SqlParameter name = new SqlParameter("@Building", SqlDbType.NVarChar, 255);
            name.Direction = ParameterDirection.Input;
            name.Value = buildName.Text;
            SqlParameter Code = new SqlParameter("@Code", SqlDbType.NVarChar, 255);
            Code.Direction = ParameterDirection.Input;
            Code.Value = buildCode.Text;
            SqlParameter AccNumber = new SqlParameter("@AccNumber", SqlDbType.NVarChar, 255);
            AccNumber.Direction = ParameterDirection.Input;
            AccNumber.Value = acc.Text;
            SqlParameter Contra = new SqlParameter("@Contra", SqlDbType.NVarChar, 255);
            Contra.Direction = ParameterDirection.Input;
            Contra.Value = bank.Text;
            SqlParameter Period = new SqlParameter("@Period", SqlDbType.NVarChar, 255);
            Period.Direction = ParameterDirection.Input;
            Period.Value = period.Text;
            SqlParameter DataPath = new SqlParameter("@DataPath", SqlDbType.NVarChar, 255);
            DataPath.Direction = ParameterDirection.Input;
            DataPath.Value = path.Text;

            SqlParameter payment = new SqlParameter("@payments", SqlDbType.Int);
            payment.Direction = ParameterDirection.Input;
            payment.Value = payments.Text;
            SqlParameter receipt = new SqlParameter("@receipts", SqlDbType.Int);
            receipt.Direction = ParameterDirection.Input;
            receipt.Value = receipts.Text;

            SqlParameter PM = new SqlParameter("@pm", SqlDbType.NVarChar, 255);
            PM.Direction = ParameterDirection.Input;
            PM.Value = pm.Text;
            SqlParameter BankName = new SqlParameter("@bankName", SqlDbType.NVarChar, 255);
            BankName.Direction = ParameterDirection.Input;
            BankName.Value = bankName.Text;
            SqlParameter AccName = new SqlParameter("@accName", SqlDbType.NVarChar, 255);
            AccName.Direction = ParameterDirection.Input;
            AccName.Value = accName.Text;
            SqlParameter BankAccNumber = new SqlParameter("@bankAccNumber", SqlDbType.NVarChar, 255);
            BankAccNumber.Direction = ParameterDirection.Input;
            BankAccNumber.Value = accNumber.Text;
            SqlParameter Branch = new SqlParameter("@branch", SqlDbType.NVarChar, 255);
            Branch.Direction = ParameterDirection.Input;
            Branch.Value = branch.Text;

            insertParameters.Add(name);
            insertParameters.Add(Code);
            insertParameters.Add(AccNumber);
            insertParameters.Add(Contra);
            insertParameters.Add(Period);
            insertParameters.Add(DataPath);
            insertParameters.Add(payment);
            insertParameters.Add(receipt);

            insertParameters.Add(PM);
            insertParameters.Add(BankName);
            insertParameters.Add(AccName);
            insertParameters.Add(BankAccNumber);
            insertParameters.Add(Branch);

            dsBuildings.Insert();
        } else if (e.CommandName == "NoInsert") {
            insertParameters.Clear();
            TextBox buildName = GridView1.Controls[0].Controls[0].FindControl("noBuild") as TextBox;
            TextBox buildCode = GridView1.Controls[0].Controls[0].FindControl("noCode") as TextBox;

            SqlParameter name = new SqlParameter("@Building", SqlDbType.NVarChar, 255);
            name.Direction = ParameterDirection.Input;
            name.Value = buildName.Text;
            SqlParameter Code = new SqlParameter("@Code", SqlDbType.NVarChar, 255);
            Code.Direction = ParameterDirection.Input;
            Code.Value = buildCode.Text;
            SqlParameter AccNumber = new SqlParameter("@AccNumber", SqlDbType.NVarChar, 255);
            AccNumber.Direction = ParameterDirection.Input;
            AccNumber.Value = "";
            SqlParameter Contra = new SqlParameter("@Contra", SqlDbType.NVarChar, 255);
            Contra.Direction = ParameterDirection.Input;
            Contra.Value = "";
            SqlParameter Period = new SqlParameter("@Period", SqlDbType.NVarChar, 255);
            Period.Direction = ParameterDirection.Input;
            Period.Value = "";
            SqlParameter DataPath = new SqlParameter("@DataPath", SqlDbType.NVarChar, 255);
            DataPath.Direction = ParameterDirection.Input;
            DataPath.Value = "";

            insertParameters.Add(name);
            insertParameters.Add(Code);
            insertParameters.Add(AccNumber);
            insertParameters.Add(Contra);
            insertParameters.Add(Period);
            insertParameters.Add(DataPath);
            dsBuildings.Insert();
        }
    }

    protected void dsBuildings_Inserting(object sender, SqlDataSourceCommandEventArgs e) {
        e.Command.Parameters.Clear();
        foreach (SqlParameter p in insertParameters) {
            e.Command.Parameters.Add(p);
        }
    }

    protected void btnTrustCodes_Click(object sender, EventArgs e) {
        pnlBuildingCodes.Visible = true;
        pnlRentals.Visible = false;
        pnlJournals.Visible = false;
        pnlBankCharges.Visible = false;
    }

    protected void btnRentalCodes_Click(object sender, EventArgs e) {
        pnlBuildingCodes.Visible = false;
        pnlRentals.Visible = true;
        pnlBankCharges.Visible = false;
        pnlJournals.Visible = false;
    }

    protected void btnBankFees_Click(object sender, EventArgs e) {
        pnlBuildingCodes.Visible = false;
        pnlRentals.Visible = false;
        pnlJournals.Visible = false;
        pnlBankCharges.Visible = true;
    }

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e) {
        if (e.CommandName == "InsertRent") {
            insertParameters.Clear();
            TextBox description = GridView4.FooterRow.FindControl("txtDescrip") as TextBox;
            TextBox account = GridView4.FooterRow.FindControl("txtAcc") as TextBox;
            TextBox contra = GridView4.FooterRow.FindControl("txtContr") as TextBox;

            SqlParameter desc = new SqlParameter("@description", SqlDbType.NVarChar, 255);
            desc.Direction = ParameterDirection.Input;
            desc.Value = description.Text;
            SqlParameter acc = new SqlParameter("@crAccount", SqlDbType.NVarChar, 255);
            acc.Direction = ParameterDirection.Input;
            acc.Value = account.Text;
            SqlParameter con = new SqlParameter("@crContra", SqlDbType.NVarChar, 255);
            con.Direction = ParameterDirection.Input;
            con.Value = contra.Text;

            insertParameters.Add(desc);
            insertParameters.Add(acc);
            insertParameters.Add(con);
            SqlDataSource1.Insert();
        }
    }

    protected void SqlDataSource1_Inserting(object sender, SqlDataSourceCommandEventArgs e) {
        SqlDataSource1.InsertParameters.Clear();
        foreach (SqlParameter p in insertParameters) {
            e.Command.Parameters.Add(p);
        }
    }

    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e) {
    }

    protected void insertRent_Click(object sender, EventArgs e) {
        insertParameters.Clear();
        TextBox description = GridView4.FooterRow.FindControl("txtDescrip") as TextBox;
        TextBox account = GridView4.FooterRow.FindControl("txtAcc") as TextBox;
        TextBox contra = GridView4.FooterRow.FindControl("txtContr") as TextBox;

        SqlParameter desc = new SqlParameter("@description", SqlDbType.NVarChar, 255);
        desc.Direction = ParameterDirection.Input;
        desc.Value = description.Text;
        SqlParameter acc = new SqlParameter("@crAccount", SqlDbType.NVarChar, 255);
        acc.Direction = ParameterDirection.Input;
        acc.Value = account.Text;
        SqlParameter con = new SqlParameter("@crContra", SqlDbType.NVarChar, 255);
        con.Direction = ParameterDirection.Input;
        con.Value = contra.Text;

        insertParameters.Add(desc);
        insertParameters.Add(acc);
        insertParameters.Add(con);
        SqlDataSource1.Insert();
    }

    protected void btnJournal_Click(object sender, EventArgs e) {
        pnlBuildingCodes.Visible = false;
        pnlRentals.Visible = false;
        pnlJournals.Visible = true;
        pnlBankCharges.Visible = false;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e) {
        foreach (GridViewRow gvr in grdJournal.Rows) {
            CheckBox thisCheck = (gvr.FindControl("chkJournal") as CheckBox);
            thisCheck.Checked = chkAll.Checked;
        }
    }

    protected void btnApplyJournal_Click(object sender, EventArgs e) {
        String period = cmbPeriod.SelectedValue;
        String description = txtDescription.Text;
        String reference = txtReference.Text;
        String amt = double.Parse(txtAmount.Text).ToString();
        int checkedBoxes = 0;
        String str = "";
        foreach (GridViewRow gvr in grdJournal.Rows) {
            str = "";
            if ((gvr.FindControl("chkJournal") as CheckBox).Checked) {
                checkedBoxes += 1;
                try {
                    String build = (gvr.Cells[1].Controls[1] as Label).Text;
                    String bCode = (gvr.Cells[2].Controls[1] as Label).Text;
                    String[] bdets = GetBuildingDetails(bCode);
                    if (bdets != null) {
                        String accNumber = bdets[0];
                        String contra = bdets[1];
                        String dPath = bdets[2];
                        DateTime thisDate = DateTime.Now;
                        String dateInsert = thisDate.Year.ToString() + "/" + (thisDate.Month < 10 ? "0" + thisDate.Month.ToString() : thisDate.Month.ToString()) + "/" + (thisDate.Day < 10 ? "0" + thisDate.Day.ToString() : thisDate.Day.ToString());
                        Dictionary<String, Object> sqlParms = new Dictionary<string, object>();
                        sqlParms.Add("@build", build);
                        sqlParms.Add("@bCode", bCode);
                        sqlParms.Add("@trnDate", DateTime.Now.ToString("yyyy/MM/dd"));
                        sqlParms.Add("@amt", amt);
                        sqlParms.Add("@description", description);
                        sqlParms.Add("@reference", reference);
                        sqlParms.Add("@accNumber", accNumber);
                        sqlParms.Add("@contra", contra);
                        sqlParms.Add("@dPath", dPath);
                        sqlParms.Add("@period", period);

                        str = " IF NOT EXISTS (SELECT * FROM tblJournal WHERE building = @build AND code = @bCode) ";
                        str += "INSERT INTO tblJournal (trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period, post) VALUES ";
                        str += " (@trnDate, @amt, @build,@bCode, @description, @reference, @accNumber, @contra, @dPath, @period, 'False')";
                        str += " ELSE ";
                        str += "UPDATE tblJournal SET trnDate = @trnDate, amount = @amt, accnumber = @accNumber, contra = @contra, description = @description, reference = @reference, ";
                        str += " datapath = @dPath, period = @period, post = 'False' WHERE building = @build AND code = @bCode";
                        str += " ";
                        String error = utils.executeQuery(str, sqlParms);
                        if (error != "") {
                            lblJnlStatus.Text = error;
                        }
                    }
                } catch (Exception ex) {
                    lblJnlStatus.Text += ex.Message;
                }
            }
        }
        runExport();
    }

    private void runExport() {
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
            lblJnlStatus.Text += "Journal Succeeded";
        } catch (Exception ex) {
            lblJnlStatus.Text += ex.Message;
        }
    }

    private String[] GetBuildingDetails(String Code) {
        String str = "SELECT AccNumber, Contra, DataPath FROM tblBuildings WHERE Code = '" + Code + "'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                String acc = idDS.Tables[0].Rows[0]["AccNumber"].ToString().Trim();
                String contra = idDS.Tables[0].Rows[0]["Contra"].ToString().Trim();
                String dpath = idDS.Tables[0].Rows[0]["DataPath"].ToString().Trim();
                String[] dets = new String[] { acc, contra, dpath };
                return dets;
            } catch {
                return null;
            }
        } else {
            return null;
        }
    }

    protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e) {
        LoadServiceFee();
    }

    private void LoadServiceFee() {
        String str = "SELECT CashDeposit FROM tblBankCharges";
        DataSet ds = utils.getData(str, null);
        String sf = ds.Tables[0].Rows[0]["CashDeposit"].ToString();
        txtAmount.Text = sf;
    }
}