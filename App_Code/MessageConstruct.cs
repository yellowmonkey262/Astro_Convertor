using System;
using System.Collections.Generic;

public class MessageConstruct {
    public List<int> clients { get; set; }

    public String msgID { get; set; }

    public EmailConstruct message { get; set; }
}

public class SMSMessage {
    public int id { get; set; }

    public String building { get; set; }

    public String customer { get; set; }

    public String number { get; set; }

    public String reference { get; set; }

    public String message { get; set; }

    public bool direction { get; set; }

    public DateTime sent { get; set; }

    public String sender { get; set; }

    public bool billable { get; set; }

    public bool bulkbillable { get; set; }

    public String astStatus { get; set; }

    public String batchID { get; set; }

    public String status { get; set; }

    public DateTime nextPolled { get; set; }

    public int pollCount { get; set; }

    public double cbal { get; set; }

    public String smsType { get; set; }
}

public class EmailConstruct {
    public String ID { get; set; }

    public DateTime ReceivedDate { get; set; }

    public String SentFrom { get; set; }

    public String SentTo { get; set; }

    public String Subject { get; set; }

    public String Body { get; set; }

    public String ForwardedTo { get; set; }

    public DateTime ForwardDate { get; set; }

    public DateTime HandledDate { get; set; }

    public String Reference { get; set; }

    public bool Handled { get; set; }
}

public class EmailArgs : EventArgs {
    public MessageConstruct message { get; set; }

    public EmailArgs(MessageConstruct msg) {
        message = msg;
    }
}

public class SMSArgs : EventArgs {
    public SMSMessage message { get; set; }

    public SMSArgs(SMSMessage msg) {
        message = msg;
    }
}

public class MessageArgs : EventArgs {
    public String message { get; set; }

    public MessageArgs(String msg) {
        message = msg;
    }
}

public delegate void EmailMessageEventHandler(object sender, EmailArgs e);

public delegate void SMSMessageEventHandler(object sender, SMSArgs e);

public delegate void ClientMessageEventHandler(object sender, MessageArgs e);