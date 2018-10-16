using System;
using System.Web.Services;

/// <summary>
/// Summary description for SMSService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
// [System.Web.Script.Services.ScriptService]
public class SMSService : System.Web.Services.WebService {
    private DataHandler dh;

    public SMSService() {
        dh = new DataHandler();
    }

    [WebMethod]
    public bool SendMessage(SMSMessage msg, out String status) {
        int rs = 0;
        status = String.Empty;
        if (msg.id == 0) {
            //billable, bulkbillable, astStatus, batchID, status, nextPolled, pollCount
            msg.id = dh.SaveQueuedMessage(msg.building, msg.customer, msg.cbal, msg.smsType, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status);
            msg.reference = String.Format("{0}/{1}", msg.customer, msg.id);
            rs = dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
            if (msg.smsType == "Disconnection SMS") { SendImmediateMessage(msg, out status); }
        }
        return (rs != -1 ? true : false);
    }

    public bool SendImmediateMessage(SMSMessage msg, out String status) {
        if (msg.id == 0) {
            //billable, bulkbillable, astStatus, batchID, status, nextPolled, pollCount
            msg.id = dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
            msg.reference = String.Format("{0}/{1}", msg.customer, msg.id);
            dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
        } else {
            dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
        }
        String bid = "";
        bool success = SMS.SendSMS(msg.number, String.Format("{0} - Reply with ref:{1}", msg.message, msg.reference), out status, out bid);
        if (success) {
            msg.batchID = bid;
            dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
        }
        return success;
    }

    [WebMethod]
    public bool SendBulkMessage(SMSMessage msg, out String status) {
        if (msg.id == 0) {
            //billable, bulkbillable, astStatus, batchID, status, nextPolled, pollCount
            msg.id = dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
        } else {
            dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
            msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
        }
        String bid = "";
        String[] numbers = msg.number.Split(new String[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (String number in numbers) {
            bool success = SMS.SendSMS(number, String.Format("{0}", msg.message), out status, out bid);
            if (success) {
                msg.batchID += bid + ";";
            }
        }
        dh.SaveOutboundMessage(msg.id, msg.building, msg.customer, msg.number, msg.reference, msg.message, msg.billable, msg.bulkbillable, msg.sent, msg.sender, msg.astStatus,
        msg.batchID, msg.status, msg.nextPolled, msg.pollCount, msg.cbal, msg.smsType, out status);
        return true;
    }
}