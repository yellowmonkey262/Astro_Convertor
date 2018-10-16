var xmlHttp
var xmlHttpE
var txtblockSubmitQualifyme
var backFunction = new Array();

function stateChanged() {
    if (xmlHttp.readyState == 4) {
        document.getElementById(txtblock).innerHTML = xmlHttp.responseText;
    }
}

function GetXmlHttpObject() {
    var xmlHttp = null;
    try {
        xmlHttp = new XMLHttpRequest();
    }
    catch (e) {
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
    }
    return xmlHttp;
}

function Trust(thisval) {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmReconMng.aspx?Buildings="+thisval;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function Allocated() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmAllocated.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function NotAllocated() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmNotAllocated.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function ImportFiles() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Import/frmImportFiles.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function Import() {
    var dra = document.getElementById("ListBox1").value;
    //var period = document.getElementById("txtPeriod").value;
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Import/frmImport.aspx?FileName="+dra;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function HideImport() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Import/frmImportFiles.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function Reconcile() {
    var period = "";
    while (period == "") {
        period = prompt("Please enter TRUST period", "");
        if ((period != "")&&(period != null)) {
            txtblock = "divContent";
            document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
            xmlHttp = GetXmlHttpObject();
            if (xmlHttp == null) {
                alert("Your browser does not support AJAX!");
                return;
            }
            var url = "Reconcile/frmReconcile.aspx?period=" + period;
            xmlHttp.onreadystatechange = stateChanged;
            xmlHttp.open("GET", url, true);
            xmlHttp.send(null);
        }
    }
}

function EditAllocate(theId, Date, Description, Amount, Balance, AccNumber, AccDescription, StatementNr, Allocate) {
    txtblock = "divAllocation";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmEditAllocation.aspx";
    url = url + "?id=" + theId;
    url = url + "&Date=" + Date;
    url = url + "&Description=" + Description;
    url = url + "&Amount=" + Amount;
    url = url + "&Balance=" + Balance;
    url = url + "&AccNumber=" + AccNumber;
    url = url + "&AccDescription=" + AccDescription;
    url = url + "&StatementNr=" + StatementNr;
    url = url + "&Allocate=" + Allocate;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function EditAllocate2(theId, Date, Description, Amount, Balance, FromAccNumber, AccDescription, StatementNr, Allocate, Reference, Building, AccNumber) {
    txtblock = "divNotAllocation";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmEditAllocation2.aspx";
    url = url + "?id=" + theId;
    url = url + "&Date=" + Date;
    url = url + "&Description=" + Description;
    url = url + "&Amount=" + Amount;
    url = url + "&Balance=" + Balance;
    url = url + "&FromAccNumber=" + FromAccNumber;
    url = url + "&AccDescription=" + AccDescription;
    url = url + "&StatementNr=" + StatementNr;
    url = url + "&Allocate=" + Allocate;
    url = url + "&Reference=" + Reference;
    url = url + "&Building=" + Building;
    url = url + "&AccNumber=" + AccNumber;
    alert(url);
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function DeleteMe(theId) {
    txtblock = "divNotAllocation";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmDeleteLine.aspx";
    url = url + "?id=" + theId;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function Split(theId, Date, Description, Amount, Balance, FromAccNumber, AccDescription, StatementNr, Allocate, Reference, Building, AccNumber) {
    txtblock = "divNotAllocation";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmSplitEntry.aspx";
    url = url + "?id=" + theId;
    url = url + "&Date=" + Date;
    url = url + "&Description=" + Description;
    url = url + "&Amount=" + Amount;
    url = url + "&Balance=" + Balance;
    url = url + "&FromAccNumber=" + FromAccNumber;
    url = url + "&AccDescription=" + AccDescription;
    url = url + "&StatementNr=" + StatementNr;
    url = url + "&Allocate=" + Allocate;
    url = url + "&Reference=" + Reference;
    url = url + "&Building=" + Building;
    url = url + "&AccNumber=" + AccNumber;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function CloseAllocate() {
    document.getElementById("divAllocation").innerHTML = "";
}

function CloseNotAllocate() {
    document.getElementById("divNotAllocation").innerHTML = "";
}

function MoveItem() {
    var BuildingId = document.getElementById("DropDownList1").value;
    var LedgerId = document.getElementById("TextBox1").value;
    var Date = document.getElementById("TextBox2").value;
    var Description = document.getElementById("TextBox3").value;
    var Amount = document.getElementById("TextBox4").value;
    var Balance = document.getElementById("TextBox5").value;
    var AccNumber = document.getElementById("TextBox6").value;
    var AccDescription = document.getElementById("TextBox7").value;
    var StatementNr = document.getElementById("TextBox8").value;
    var Allocate = document.getElementById("TextBox9").value;
    var period = document.getElementById("txtPeriod").value;
    //var Reference = document.getElementById("txtReference").value;
    txtblock = "divAllocation";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmSaveAllocation.aspx";
    url = url + "?BuildingId=" + BuildingId;
    url = url + "&LedgerId=" + LedgerId;
    url = url + "&Date=" + Date;
    url = url + "&Description=" + Description;
    url = url + "&Amount=" + Amount;
    url = url + "&Balance=" + Balance;
    url = url + "&AccNumber=" + AccNumber;
    url = url + "&AccDescription=" + AccDescription;
    url = url + "&StatementNr=" + StatementNr;
    url = url + "&Allocate=" + Allocate;
    url = url + "&period=" + period;
    //url = url + "&Reference=" + Reference;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function MoveItem2() {
    var Building = document.getElementById("DropDownList1").value;
    var LedgerId = document.getElementById("TextBox12").value;
    var Date = document.getElementById("TextBox13").value;
    var Description = document.getElementById("TextBox14").value;
    var Amount = document.getElementById("TextBox15").value;
    var Balance = document.getElementById("TextBox16").value;
    var FromAccNumber = document.getElementById("TextBox17").value;
    var AccDescription = document.getElementById("TextBox18").value;
    var StatementNr = document.getElementById("TextBox19").value;
    var Allocate = document.getElementById("TextBox20").value;
    var Reference = document.getElementById("TextBox21").value;
    var AccNumber = document.getElementById("TextBox22").value;
    var period = document.getElementById("txtPeriod").value;
    txtblock = "divNotAllocation";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmSaveAllocation2.aspx";
    url = url + "?Building=" + Building;
    url = url + "&LedgerId=" + LedgerId;
    url = url + "&Date=" + Date;
    url = url + "&Description=" + Description;
    url = url + "&Amount=" + Amount;
    url = url + "&Balance=" + Balance;
    url = url + "&FromAccNumber=" + FromAccNumber;
    url = url + "&AccDescription=" + AccDescription;
    url = url + "&StatementNr=" + StatementNr;
    url = url + "&Allocate=" + Allocate;
    url = url + "&Reference=" + Reference;
    url = url + "&AccNumber=" + AccNumber;
    url = url + "&period=" + period;
    alert(url);
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function SaveSplit() {
    var Descrip1 = document.getElementById("txtDescrip1").value;
    var Amount1 = document.getElementById("txtAmount1").value;
    var Reference1 = document.getElementById("txtReference1").value;
    var Building1 = document.getElementById("ddlBuilding1").value;
    var Descrip2 = document.getElementById("txtDescrip2").value;
    var Amount2 = document.getElementById("txtAmount2").value;
    var Reference2 = document.getElementById("txtReference2").value;
    var Building2 = document.getElementById("ddlBuilding2").value;
    var aId = document.getElementById("splitTXT1").value;
    var aDate = document.getElementById("splitTXT2").value;
    var Balance = document.getElementById("splitTXT3").value;
    var FromAccNumber = document.getElementById("splitTXT4").value;
    var AccDescription = document.getElementById("splitTXT5").value;
    var StatementNr = document.getElementById("splitTXT6").value;
    var Allocate = document.getElementById("splitTXT7").value;
    var AccNumber = document.getElementById("splitTXT8").value;
    txtblock = "divNotAllocation";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconcile/frmSaveSplit.aspx";
    url = url + "?Descrip1=" + Descrip1;
    url = url + "&Amount1=" + Amount1;
    url = url + "&Reference1=" + Reference1;
    url = url + "&Building1=" + Building1;
    url = url + "&Descrip2=" + Descrip2;
    url = url + "&Amount2=" + Amount2;
    url = url + "&Reference2=" + Reference2;
    url = url + "&Building2=" + Building2;
    url = url + "&aId=" + aId;
    url = url + "&aDate=" + aDate;
    url = url + "&Balance=" + Balance;
    url = url + "&FromAccNumber=" + FromAccNumber;
    url = url + "&AccDescription=" + AccDescription;
    url = url + "&StatementNr=" + StatementNr;
    url = url + "&Allocate=" + Allocate;
    url = url + "&AccNumber=" + AccNumber;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function TrustCodes() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "TrustCodes/frmViewCodes.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function SaveTrustCodes() {
    var Building = document.getElementById("txtBuildingName").value;
    var Code = document.getElementById("txtCode").value;
    var AccNumber = document.getElementById("txtAccNumber").value;
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "TrustCodes/frmSaveCodes.aspx";
    url = url + "?Building=" + Building;
    url = url + "&Code=" + Code;
    url = url + "&AccNumber=" + AccNumber;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function SaveBankingCharges() {
    var CashDeposit = document.getElementById("txtCashDep").value;
    var DebitOrder = document.getElementById("txtDebitOrder").value;
    var one = document.getElementById("TextBox1").value;
    var two = document.getElementById("TextBox2").value;
    var three = document.getElementById("TextBox3").value;
    var four = document.getElementById("TextBox4").value;
    var five = document.getElementById("TextBox5").value;
    var six = document.getElementById("TextBox6").value;
    var seven = document.getElementById("TextBox7").value;
    var eight = document.getElementById("TextBox8").value;
    var nine = document.getElementById("TextBox9").value;
    var ten = document.getElementById("TextBox10").value;
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "TrustCodes/frmSaveBankCharges.aspx";
    url = url + "?CashDeposit=" + CashDeposit;
    url = url + "&DebitOrder=" + DebitOrder;
    url = url + "&one=" + one;
    url = url + "&two=" + two;
    url = url + "&three=" + three;
    url = url + "&four=" + four;
    url = url + "&five=" + five;
    url = url + "&six=" + six;
    url = url + "&seven=" + seven;
    url = url + "&eight=" + eight;
    url = url + "&nine=" + nine;
    url = url + "&ten=" + ten;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function CreateInvoice() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Invoice/frmCreateInvoice.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function Reconciliation() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconciliation/frmReconciliation.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function Recon() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "Reconciliation/frmRecon.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
//    alert(url);

//    window.open("Reconciliation/frmRecon.aspx");
}

function ShowBuildingDate(BuildingName) {
    window.open("Reconciliation/frmBuildings.aspx?BuildingName=" + BuildingName, "mywindow","menubar=1,resizable=1,width=850,height=405,scrollbars=1");
}

function ShowHomePage() {
    txtblock = "divContent";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "frmHomePage.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function EditBuilding(BuildingId, BuildingName, Cod, AccNr, Period, DataPath, contra) {
    txtblock = "divEditBuilding";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "TrustCodes/frmEditBuilding.aspx";
    url = url + "?BuildingId=" + BuildingId;
    url = url + "&BuildingName=" + BuildingName;
    url = url + "&Code=" + Cod;
    url = url + "&AccNumber=" + AccNr;
    url = url + "&Period=" + Period;
    url = url + "&DataPath=" + DataPath;
    url = url + "&contra=" + contra;
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function SaveBuilding() {
    var BuildingId = document.getElementById("hdBuildingId").value;
    var BuildingName = document.getElementById("DropDownList1").value;
    var Code = document.getElementById("txtCod").value;
    var AccNumber = document.getElementById("txtAccNr").value;
    var Period = document.getElementById("txtPeriod").value;
    var DataPath = document.getElementById("txtDataPath").value;
    var contra = document.getElementById("txtBank").value;
    txtblock = "divEditBuilding";
    document.getElementById(txtblock).innerHTML = "<img src=\"Images/loading.gif\">";
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }
    var url = "TrustCodes/frmSaveBuilding.aspx";
    url = url + "?BuildingId=" + BuildingId;
    url = url + "&BuildingName=" + BuildingName;
    url = url + "&Code=" + Code;
    url = url + "&AccNumber=" + AccNumber;
    url = url + "&Period=" + Period;
    url = url + "&DataPath=" + DataPath;
    url = url + "&contra=" + contra;
    alert(url);
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function CloseBuilding() {
    document.getElementById("divEditBuilding").innerHTML = "";
}

function CloseEditBuilding() {
    document.getElementById("divEditBuilding").innerHTML = "";
}