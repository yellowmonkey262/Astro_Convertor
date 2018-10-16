using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.UI;

//using Microsoft.Office.Interop.Excel;
public partial class frmHome : System.Web.UI.Page {
    private Utilities utils = new Utilities();

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack && !Page.IsCallback) {
            hideAll();
            ListBox1.Items.Clear();
            loadImportFiles();
            getIntegratorInfo();
        }
        try {
            String reloc = Request.QueryString["floc"];
            if (reloc == "import") {
                hideAll();
                pnlImportFiles.Visible = true;
            } else if (reloc == "allocate") {
                hideAll();
                pnlAllocate.Visible = true;
            }
        } catch { }
    }

    private void hideAll() {
        pnlImportFiles.Visible = false;
        pnlAllocate.Visible = false;
    }

    #region Start Integrator

    private void getIntegratorInfo() {
        String processName = "Server";
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length == 0) {
            lblStatus.Text = "Not running";
        } else {
            lblStatus.Text = "Running";
        }
    }

    private void startIntegrator() {
        String fileName = "C:\\Program Files (x86)\\Metathought Development & Design\\Astrodon Server\\Server.exe"; //Astrodon
        //String fileName = "C:\\Program Files (x86)\\Softline Pastel\\Pastel SDK V11\\SDKVBSample.NET\\bin\\Pastel Integrator.exe" //Local
        try {
            Process myProcess = Process.Start(fileName);
        } catch { }
        getIntegratorInfo();
    }

    #endregion Start Integrator

    #region Menu Buttons

    protected void imgHome_Click(object sender, ImageClickEventArgs e) {
    }

    protected void imgLogout_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/Default.aspx", true);
    }

    protected void imgImport_Click(object sender, ImageClickEventArgs e) {
        hideAll();
        pnlImportFiles.Visible = true;
    }

    protected void imgAllocate_Click(object sender, ImageClickEventArgs e) {
        hideAll();
        pnlAllocate.Visible = true;
    }

    protected void imgTrust_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmAccounts.aspx?acc=trust", true);
    }

    protected void imgAllocated_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmAccounts.aspx?acc=alloc", true);
    }

    protected void imgUnAllocated_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmAccounts.aspx?acc=unalloc", true);
    }

    protected void imgBuildings_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmBuildings.aspx", true);
    }

    protected void imgExport_Click(object sender, ImageClickEventArgs e) {
        Response.Redirect("~/frmExport.aspx", true);
    }

    #endregion Menu Buttons

    #region Import Files

    private void loadImportFiles() {
        if (!Directory.Exists("C:\\inetpub\\BankFiles\\")) { Directory.CreateDirectory("C:\\inetpub\\BankFiles\\"); }
        DirectoryInfo di = new DirectoryInfo("C:\\inetpub\\BankFiles\\");
        FileInfo[] diar1 = di.GetFiles();

        //ListBox1.Attributes.Add("onClick", "Import()")
        ListBox1.Items.Clear();
        foreach (FileInfo dra in diar1) {
            ListBox1.Items.Add(dra.FullName);
        }
        ListBox1.Attributes.Add("onclick", "listbox_click()");
    }

    private void loadExcelFile(String fileName) {
        StreamReader objReader = new StreamReader(fileName);
        ArrayList contents = new ArrayList();
        int whichLine = 1;
        String[] lineBreaker = new String[] { "," };
        String[] lineBreaker2 = new String[] { ";" };
        int lineCount = 0;
        int importedLines = 0;
        while (!objReader.EndOfStream) {
            String strLine = objReader.ReadLine();
            lineCount++;

            String[] lineContents = strLine.Split(strLine.Contains(",") ? lineBreaker : lineBreaker2, StringSplitOptions.None);

            try {
                if (whichLine >= 7) {
                    String tranDate = cleanDate(lineContents[0]);
                    String[] dateSplitter = new String[] { "/" };
                    String[] dateBits = tranDate.Split(dateSplitter, StringSplitOptions.None);
                    DateTime trnDate = new DateTime(int.Parse(dateBits[2]), int.Parse(dateBits[1]), int.Parse(dateBits[0]));
                    String reference = lineContents[2];
                    String description = lineContents[4];
                    double debit = 0;
                    double credit = 0;
                    double cumulative = 0;
                    double.TryParse(lineContents[5], out debit);
                    double.TryParse(lineContents[6], out credit);
                    if (lineContents.Length > 7) { double.TryParse(lineContents[7], out cumulative); }
                    importedLines++;
                    bool success = importRental(trnDate, reference, description, debit, credit, cumulative);
                }
            } catch {
                continue;
            }
            whichLine++;
        }
        objReader.Close();
        if (lblImportStatus.Text == "") {
            File.Delete(fileName);
            loadImportFiles();
            lblImportStatus.Text = "Import Successful: " + lineCount.ToString() + " lines in file. " + importedLines.ToString() + " imported.";
        }
    }

    protected void btnImportFile_Click(object sender, EventArgs e) {
        int selIndex = ListBox1.SelectedIndex;
        String fileName = ListBox1.SelectedValue;
        if (fileName.ToUpper().EndsWith(".CSV")) {
            importFiles(fileName);
        }
    }

    private void importFiles(String fileName) {
        try {
            StreamReader objReader = new StreamReader(fileName);
            ArrayList contents = new ArrayList();
            int whichLine = 0;
            String[] lineBreaker = new String[] { "," };
            String accNumber = "";
            String accDescription = "";
            String statementNumber = "";
            while (!objReader.EndOfStream) {
                String strLine = objReader.ReadLine();
                if (strLine.ToLower().Contains("prepared by")) {
                    objReader.Close();
                    loadExcelFile(fileName);
                    break;
                }
                String[] lineContents = strLine.Split(lineBreaker, StringSplitOptions.RemoveEmptyEntries);
                switch (whichLine) {
                    case 0:
                        break;

                    case 1:
                        accNumber = lineContents[1];
                        break;

                    case 2:
                        accDescription = lineContents[1];
                        break;

                    case 3:
                        statementNumber = lineContents[1];
                        break;

                    default:
                        String trnDate = cleanDate(lineContents[0]);
                        String description = cleanDescription(lineContents[1]);
                        String rawAmount = lineContents[2];
                        String rawBalance = lineContents[3];
                        importMe(accNumber, accDescription, statementNumber, trnDate, description, rawAmount, rawBalance);
                        break;
                }
                whichLine++;
            }

            String str = " DELETE FROM tblLedgerTransactions WHERE Description = 'BROUGHT FORWARD' OR Description = 'CARRIED FORWARD' OR Description = 'PROVISIONAL STATEMENT' ";
            utils.executeQuery(str, null);
            objReader.Close();
            File.Delete(fileName);
            loadImportFiles();
            if (lblImportStatus.Text == "") { lblImportStatus.Text = "Statement Import Successful"; }
        } catch (Exception ex) {
            lblImportStatus.Text = ex.Message;
        }
    }

    private void importMe(String accNumber, String accDescription, String statementNr, String trnDate, String description, String amount, String balance) {
        String str = " BEGIN TRAN ";
        str += " IF NOT EXISTS (SELECT * FROM tblLedgerTransactions WHERE AccNumber = '" + accNumber + "' AND AccDescription = '" + accDescription + "' AND ";
        str += " StatementNr = '" + statementNr + "' AND Date = '" + trnDate + "' AND Description = '" + description + "' AND Amount = '" + amount;
        str += "' AND Balance = '" + balance + "') ";
        str += " BEGIN ";
        str += " INSERT INTO tblLedgerTransactions (AccNumber, AccDescription, StatementNr, Date, Description, Amount, Balance, Allocate) VALUES ";
        str += " ('" + accNumber + "', '" + accDescription + "', '" + statementNr + "', '" + trnDate + "', '" + description + "', '" + amount;
        str += "', '" + balance + "', 0) ";
        str += " END  ";
        str += " COMMIT TRAN";
        utils.executeQuery(str, null);
    }

    private bool importRental(DateTime trnDate, String reference, String description, double drValue, double crValue, double cumValue) {
        String str = "IF NOT EXISTS(SELECT * FROM tblRentals WHERE trnDate = @trnDate AND reference = @reference AND cumValue = @cumValue)";
        str += " INSERT INTO tblRentals (trnDate, reference, description, drValue, crValue, cumValue) VALUES ";
        str += " (@trnDate, @reference, @description, @drValue, @crValue, @cumValue) ";
        Dictionary<String, Object> sqlParms = new Dictionary<string, object>();
        sqlParms.Add("@trnDate", trnDate);
        sqlParms.Add("@reference", reference);
        sqlParms.Add("@description", description);
        sqlParms.Add("@drValue", drValue);
        sqlParms.Add("@crValue", crValue);
        sqlParms.Add("@cumValue", cumValue);
        String error = utils.executeQuery(str, sqlParms);
        if (error != "") {
            lblImportStatus.Text += error + "<br/>";
            return false;
        } else {
            return true;
        }
    }

    private String cleanDate(String rawDate) {
        //Begin to Format the Date for Pastel (dd/mm/yyyy)
        String Numbers = "0123456789";
        rawDate = rawDate.Replace("-", "").Replace("/", "");
        if (Numbers.Contains(rawDate.Substring(1, 1))) {
        } else {
            rawDate = "0" + rawDate;
        }
        String day = rawDate.Substring(0, 2);
        String month = "";
        int yearX = 0;
        if (!Numbers.Contains(rawDate.Substring(2, 1))) {
            month = rawDate.Substring(2, 3);
            switch (month) {
                case "Jan":
                    month = "01";
                    break;

                case "Feb":
                    month = "02";
                    break;

                case "Mar":
                    month = "03";
                    break;

                case "Apr":
                    month = "04";
                    break;

                case "May":
                    month = "05";
                    break;

                case "Jun":
                    month = "06";
                    break;

                case "Jul":
                    month = "07";
                    break;

                case "Aug":
                    month = "08";
                    break;

                case "Sep":
                    month = "09";
                    break;

                case "Oct":
                    month = "10";
                    break;

                case "Nov":
                    month = "11";
                    break;

                case "Dec":
                    month = "12";
                    break;
            }
            yearX = 5;
        } else {
            month = rawDate.Substring(2, 2);
            yearX = 4;
        }
        int yearLength = rawDate.Length - yearX;
        String year = "";
        if (yearLength == 2) {
            year = "20" + rawDate.Substring(yearX, 2);
        } else {
            year = rawDate.Substring(yearX, 4);
        }
        String cleanDate = day + "/" + month + "/" + year;
        return cleanDate;
    }

    private String cleanDescription(String rawDescription) {
        //Remove the + sign in the description because my import does not like it
        rawDescription = rawDescription.Replace("+", " ");
        //Description PRIME PROPERTIES will always go to Knightsbridge
        //Add the Code to the description then the system will pick it up during reconcile
        if (rawDescription == "PRIME PROPERTIES") { rawDescription += " KB000"; }
        try {
            if (rawDescription.Substring(0, 13) == "CASHFOCUS MDM") {
                String subNumber = rawDescription.Substring(15, 3);
                rawDescription = rawDescription.Substring(0, 13) + subNumber;
            }
        } catch { }
        Char[] illegalChars = "!@#$%^&*{}[]\"'_+<>?".ToCharArray();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (Char ch in rawDescription.ToCharArray()) {
            if (Array.IndexOf(illegalChars, ch) == -1) { sb.Append(ch); }
        }
        rawDescription = sb.ToString();
        return rawDescription;
    }

    #endregion Import Files

    protected void btnAllocate_Click(object sender, EventArgs e) {
        Match();
        MatchRental();
        if (lblAllocateStatus.Text == "") { lblAllocateStatus.Text = "Reconciliation Complete"; }
    }

    private void Match() {
        try {
            int period = int.Parse(cmbTrustPeriod.SelectedValue);
            DataSet buildingsDS = utils.getData("SELECT * FROM tblBuildings", null);
            DataSet trustDS = utils.getData(" SELECT * FROM tblLedgerTransactions WHERE Allocate = '0'", null);
            String Numbers = "0123456789";
            String Reference = "";
            String Cash1 = "";
            String code = "";
            foreach (DataRow trustRow in trustDS.Tables[0].Rows) {
                int startIndex = 0;
                int CodeLength = 0;
                String trnCode = "";
                String Descript = trustRow["Description"].ToString().Trim();
                String Description = Descript.Replace(" ", "");
                String AccNumber = "";
                int DescrpLength = Description.Length;
                bool isPayment = false;
                double amount = double.Parse(trustRow["Amount"].ToString());
                ////This if statement is to check for "BR CASH" Or "Cash Transactions"
                ////These two's reference will be the code of the building in the line just above them
                if (DescrpLength > 5) { Cash1 = Description.Substring(0, 6); } else { Cash1 = ""; }
                bool changeRef = false;
                String astroRef = "";
                if (Cash1 != "BRCASH" && !Cash1.StartsWith("BRC") && Cash1 != "CASHTR" && Cash1 != "ATMCAS" && !Description.Contains("INTEREST")) {
                    astroRef = GetMatch(Descript);
                    if (String.IsNullOrEmpty(astroRef)) {
                        code = SecondPass(Descript);
                        if (code != "") {
                            Reference = Descript;
                            changeRef = true;
                        }
                    } else {
                        code = SecondPass(astroRef);
                        Reference = astroRef;
                        changeRef = true;
                    }
                    if (code != "") {
                        AccNumber = GetBuildingAccNumber(code);
                        trnCode = "";
                    }
                } else {
                    trnCode = "trn";
                }
                startIndex = -1;
                if (code == "" && trnCode == "") {
                    startIndex = -1;
                } else if (trnCode != "trn") {
                    CodeLength = code.Length;
                    if (amount < 0) {
                        startIndex = Description.IndexOf(AccNumber.Substring(0, 4));
                        isPayment = true;
                        int accStart = Description.IndexOf(AccNumber.Substring(0, 4)) + 5;
                        Reference = Description.Substring(accStart, Description.Length - accStart).Replace("/", "");
                    } else {
                        startIndex = Description.IndexOf(code);
                        if (startIndex == -1 && code != "") {
                            startIndex = Description.IndexOf(GetBuildingName(code));
                        }
                    }
                }
                if (code != "") {
                    String test = Reference;
                    if ((startIndex != -1 || Cash1 == "BRCASH" || Cash1 == "CASHTR" || Cash1.StartsWith("BRC") || Cash1 == "ATMCAS") && test != "") {
                        if (Cash1 == "BRCASH" || Cash1 == "CASHTR" || Cash1 == "ATMCAS" || Cash1.StartsWith("BRC")) {
                            Reference = Reference;
                        } else {
                            if (changeRef && Reference != astroRef) {
                                if (!isPayment) {
                                    if (!Description.Contains(code)) {
                                        Reference = code;
                                    } else {
                                        String outRef = code;
                                        int codeIndex = Description.IndexOf(code);
                                        for (int ei = codeIndex + code.Length; ei < Description.Length; ei++) {
                                            String nextChar = Description.Substring(ei, 1);
                                            if (Numbers.Contains(nextChar)) {
                                                outRef += nextChar;
                                            }
                                        }
                                        Reference = outRef;
                                    }
                                }
                            }
                        }
                        if (Reference != "") {
                            double amt, balance;
                            if (!double.TryParse(trustRow["Amount"].ToString().Trim(), out amt)) { amt = 0; }
                            if (!double.TryParse(trustRow["Balance"].ToString().Trim(), out balance)) { balance = 0; }
                            String str = " BEGIN TRAN ";
                            str += " IF NOT EXISTS (SELECT * FROM tblDevision WHERE Date = '" + trustRow["Date"].ToString().Trim() + "' AND ";
                            str += " Description = '" + Descript + "' AND ";
                            str += " Amount = '" + amt.ToString() + "' AND ";
                            str += " Balance = '" + balance.ToString() + "' AND ";
                            str += " FromAccNumber = '" + trustRow["AccNumber"].ToString().Trim() + "' AND ";
                            str += " AccDescription = '" + trustRow["AccDescription"].ToString().Trim() + "' AND ";
                            str += " StatementNr = '" + trustRow["StatementNr"].ToString().Trim() + "') ";
                            str += "INSERT INTO tblDevision (Date, Description, Amount, Balance, FromAccNumber, AccDescription, StatementNr, ";
                            str += " Allocate, Reference, Building, AccNumber, Period, lid) VALUES ";
                            str += " ('" + trustRow["Date"].ToString().Trim() + "', ";
                            str += "'" + Descript + "', ";
                            str += "'" + amt.ToString() + "', ";
                            str += "'" + balance.ToString() + "', ";
                            str += "'" + trustRow["AccNumber"].ToString().Trim() + "', ";
                            str += "'" + trustRow["AccDescription"].ToString().Trim() + "', ";
                            str += "'" + trustRow["StatementNr"].ToString().Trim() + "', ";
                            str += "'1', '" + Reference + "', ";
                            //RENT
                            if (Description.Contains("INTEREST")) {
                                str += "'126', '9320000', ";
                            } else if (Description.Contains("D/") && Description.EndsWith("R")) {
                                code = "RENT";
                                str += "'" + GetBuildingId(code) + "', ";
                                str += "'" + GetBuildingAccNumber(code) + "', ";
                            } else {
                                str += "'" + GetBuildingId(code) + "', ";
                                str += "'" + GetBuildingAccNumber(code) + "', ";
                            }
                            str += period + ", ";
                            str += "'" + trustRow["id"].ToString().Trim() + "')";
                            str += " ELSE ";
                            str += " UPDATE tblDevision SET posted = 'False' WHERE Date = '" + trustRow["Date"].ToString().Trim() + "' AND ";
                            str += " Description = '" + trustRow["Description"].ToString().Trim() + "' AND ";
                            str += " Amount = '" + amt.ToString() + "' AND ";
                            str += " Balance = '" + balance.ToString() + "' AND ";
                            str += " FromAccNumber = '" + trustRow["AccNumber"].ToString().Trim() + "' AND ";
                            str += " AccDescription = '" + trustRow["AccDescription"].ToString().Trim() + "' AND ";
                            str += " StatementNr = '" + trustRow["StatementNr"].ToString().Trim() + "'";

                            str += " UPDATE tblLedgerTransactions SET Allocate = '1' WHERE Date = '" + trustRow["Date"].ToString().Trim() + "' AND ";
                            str += " Description = '" + trustRow["Description"].ToString().Trim() + "' AND ";
                            str += " Amount = '" + amt.ToString() + "' AND ";
                            str += " Balance = '" + balance.ToString() + "' AND ";
                            str += " AccNumber = '" + trustRow["AccNumber"].ToString().Trim() + "' AND ";
                            str += " AccDescription = '" + trustRow["AccDescription"].ToString().Trim() + "' AND ";
                            str += " StatementNr = '" + trustRow["StatementNr"].ToString().Trim() + "'";
                            str += " COMMIT TRAN";
                            utils.executeQuery(str, null);
                        }
                    } else {
                        code = "";
                    }
                }
            }
        } catch (Exception ex) {
            lblAllocateStatus.Text = ex.Message;
        }

        String loadExport1 = "UPDATE d SET d.building = b.id FROM tblDevision d JOIN tblBuildings b ON d.building = b.code or d.Building = b.building;";
        utils.executeQuery(loadExport1, null);
        String loadExport = "INSERT INTO tblExport(lid, trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period, una)";
        loadExport += " SELECT tblDevision.lid, tblDevision.Date, tblDevision.Amount, tblBuildings.Building, tblBuildings.Code, ";
        loadExport += " tblDevision.Description, tblDevision.Reference, tblBuildings.AccNumber, tblBuildings.Contra, tblBuildings.DataPath, tblDevision.period, 0";
        loadExport += " FROM tblDevision INNER JOIN tblBuildings ON tblDevision.Building = tblBuildings.id WHERE (tblDevision.posted = 'False') ";
        loadExport += " AND (tblDevision.lid NOT IN (SELECT lid FROM tblExport AS tblExport_1));";

        //loadExport += " INSERT INTO tblUnallocated(lid, trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period) ";
        //loadExport += " SELECT (lid, trnDate, amount, building, code, description, reference, accnumber, contra, datapath, period) FROM tblExport WHERE code = 227 AND lid not in (SELECT lid from tblUnallocated);";

        //loadExport += " UPDATE tblDevision SET posted = 'True' WHERE lid in (SELECT lid FROM tblExport);";
        lblAllocateStatus.Text = utils.executeQuery(loadExport, null);
    }

    private void MatchRental() {
        String str = " INSERT INTO tblRentalRecon (rentalId, trnDate, value, account, contra)";
        str += " SELECT tblRentals.id, tblRentals.trnDate, CASE WHEN drValue = 0 THEN crvalue ELSE drvalue * - 1 END AS value, ";
        str += " tblRentalAccounts.crAccount, tblRentalAccounts.crContra FROM tblRentals INNER JOIN tblRentalAccounts ON ";
        str += " tblRentals.description = tblRentalAccounts.description WHERE (tblRentalAccounts.crAccount IS NOT NULL) AND tblRentals.id not in";
        str += " (SELECT distinct rentalId FROM tblRentalRecon) AND tblRentalAccounts.crAccount <> 'NULL'";
        lblAllocateStatus.Text = utils.executeQuery(str, null);
    }

    private String SecondPass(String description) {
        String fullDesc = description.Replace(" ", "");
        int x = 0;
        String testChar = fullDesc.Substring(x, 1);
        String codeString = "";
        int unitNumber = 0;
        while (!int.TryParse(testChar, out unitNumber)) {
            codeString += testChar;
            if (fullDesc.Length - 1 > x + 1) {
                x++;
                testChar = fullDesc.Substring(x, 1);
            } else {
                break;
            }
        }
        String twoChar = "";
        String threeChar = "";
        String bCode = "";
        if (x >= 2) { twoChar = codeString.Substring(x - 2, 2); }
        if (x >= 3) { threeChar = codeString.Substring(x - 3, 3); }
        if (threeChar != "") { bCode = GetExactCode(threeChar); }
        if (bCode == "" && twoChar != "") {
            bCode = GetExactCode(twoChar);
        }
        if ((bCode == "") && (fullDesc.Contains("(") && description.Contains(")") && description.Contains("/"))) {
            String testAccount = fullDesc.Substring(fullDesc.IndexOf("(") + 1, 4);
            bCode = GetAccNumber(testAccount);
        }
        if (bCode == "") {
            String[] stringSplit = new String[] { " " };

            String[] descBits = description.Split(stringSplit, StringSplitOptions.RemoveEmptyEntries);

            foreach (String descBit in descBits) {
                bCode = GetBuilding(descBit);
                if (bCode != "") { break; }
            }
        }
        return bCode;
    }

    private String GetMatch(String statementRef) {
        String astroRef = String.Empty;
        String query = "SELECT astroRef FROM tblMatch WHERE statementRef = '" + statementRef + "' AND statementRef <> astroRef";
        DataSet mDS = utils.getData(query, null);
        if (mDS != null && mDS.Tables.Count > 0 && mDS.Tables[0].Rows.Count > 0) {
            astroRef = mDS.Tables[0].Rows[0]["astroRef"].ToString();
        }
        return astroRef;
    }

    private String GetBuildingId(String Code) {
        String str = "SELECT id FROM tblBuildings WHERE Code = '" + Code + "'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                return idDS.Tables[0].Rows[0]["id"].ToString().Trim();
            } catch {
                return "";
            }
        } else {
            return "";
        }
    }

    private String GetBuildingAccNumber(String Code) {
        String str = "SELECT AccNumber FROM tblBuildings WHERE Code = '" + Code + "'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                return idDS.Tables[0].Rows[0]["AccNumber"].ToString().Trim();
            } catch {
                return "";
            }
        } else {
            return "";
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

    private String GetAccNumber(String Code) {
        String str = "SELECT Code FROM tblBuildings WHERE AccNumber LIKE '%" + Code + "%'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                return idDS.Tables[0].Rows[0]["Code"].ToString().Trim();
            } catch {
                return "";
            }
        } else {
            return "";
        }
    }

    private String GetBuilding(String Code) {
        String str = "SELECT Code FROM tblBuildings WHERE Building LIKE '" + Code + "%'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                return idDS.Tables[0].Rows[0]["Code"].ToString().Trim();
            } catch {
                return "";
            }
        } else {
            return "";
        }
    }

    private String GetBuildingName(String Code) {
        String str = "SELECT Building FROM tblBuildings WHERE Code = '" + Code + "'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                return idDS.Tables[0].Rows[0]["Code"].ToString().Trim();
            } catch {
                return "";
            }
        } else {
            return "";
        }
    }

    private String GetTheCode(String Code, String Building) {
        String str = "SELECT Code FROM tblBuildings WHERE Building LIKE '%" + Building.Replace("'", " ") + "%' OR Code = '" + Code + "'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                return idDS.Tables[0].Rows[0]["Code"].ToString().Trim();
            } catch {
                return "";
            }
        } else {
            return "";
        }
    }

    private String GetExactCode(String Code) {
        String str = "SELECT Code FROM tblBuildings WHERE Code = '" + Code + "'";
        DataSet idDS = utils.getData(str, null);
        if (idDS != null) {
            try {
                return idDS.Tables[0].Rows[0]["Code"].ToString().Trim();
            } catch {
                return "";
            }
        } else {
            return "";
        }
    }

    protected void btnMatch_Click(object sender, EventArgs e) {
    }
}