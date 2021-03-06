﻿using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

public class SMS {
    private const String username = "astrodon_sms";
    private const String password = "[sms@66r94e!@#]";
    //private const String username = "metasteve";
    //private const String password = "St3ph3n";

    public static bool SendSMS(String phoneNumber, String message, out String status, out String batchID) {
        double credits = GetCredits(out status);
        batchID = "";
        if (credits > 0) {
            if (!phoneNumber.StartsWith("27")) {
                if (phoneNumber.StartsWith("0")) { phoneNumber = phoneNumber.Substring(1); }
                phoneNumber = "27" + phoneNumber;
            }

            string url = "http://bulksms.2way.co.za:5567/eapi/submission/send_sms/2/2.0";
            Hashtable result;

            string data = createMessage(phoneNumber, message);
            result = send_sms(data, url);
            if ((int)result["success"] == 1) {
                batchID = result["api_batch_id"].ToString().Replace("\n", "");
                status = formatted_server_response(result);
                return true;
            } else {
                status = formatted_server_response(result);
                return false;
            }
        } else {
            if (credits == 0) { status = "Insufficient credits"; }
            return false;
        }
    }

    public static bool SendBatch(String batchFile, out String status) {
        TextReader tr = new StreamReader(batchFile);
        // E.g. TextReader tr = new StreamReader(@"C:\Users\user\Desktop\my_batch_file.csv")
        // Please see http://bulksms.2way.co.za/docs/eapi/submission/send_batch/ for information
        // on what the format of your input file should be.

        /*
         msisdn,message
        "111111111","Hi there"
        "333333333","Hello again"

         */

        string line;
        string batch = "";
        double requiredCredits = 0;
        while ((line = tr.ReadLine()) != null) {
            requiredCredits++;
            batch += line + "\n";
        }
        //Console.WriteLine(batch);

        tr.Close();
        double credits = GetCredits(out status);
        if (credits >= requiredCredits) {
            string url = "http://bulksms.2way.co.za:5567/eapi/submission/send_batch/1/1.0";

            /*****************************************************************************************************
            **Construct data
            *****************************************************************************************************/
            /*
            * Note the suggested encoding for the some parameters, notably
            * the username, password and especially the message.  ISO-8859-1
            * is essentially the character set that we use for message bodies,
            * with a few exceptions for e.g. Greek characters. For a full list,
            * see: http://bulksms.2way.co.za/docs/eapi/submission/character_encoding/
            */

            string data = "";
            data += "username=" + HttpUtility.UrlEncode(username, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            data += "&password=" + HttpUtility.UrlEncode(password, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            data += "&batch_data=" + HttpUtility.UrlEncode(batch, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            data += "&want_report=1";

            string sms_result = Post(url, data);

            string[] parts = sms_result.Split('|');

            string statusCode = parts[0];
            string statusString = parts[1];

            if (!statusCode.Equals("0")) {
                status = "Error: " + statusCode + ": " + statusString;
                return false;
            } else {
                status = "Success: batch ID " + parts[2];
                return true;
            }
        } else {
            if (credits < requiredCredits && credits > -1) { status = "Insufficient credits"; }
            return false;
        }
    }

    private static double GetCredits(out String status) {
        string url = "http://bulksms.2way.co.za:5567/eapi/user/get_credits/1/1.1";
        string data = "";
        data += "username=" + HttpUtility.UrlEncode(username, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        data += "&password=" + HttpUtility.UrlEncode(password, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        String result = Post(url, data);
        string[] parts = result.Split('|');
        if (parts.Length > 1) {
            string statusCode = parts[0];
            string statusString = parts[1];
            if (statusCode == "0") {
                status = "";

                return double.Parse(statusString);
            } else {
                status = statusString;
                return -1;
            }
        } else {
            status = parts[0];
            return -1;
        }
    }

    public static string formatted_server_response(Hashtable result) {
        string ret_string = "";
        if ((int)result["success"] == 1) {
            ret_string += "Success: batch ID " + (string)result["api_batch_id"] + "API message: " + (string)result["api_message"] + "\nFull details " + (string)result["details"];
        } else {
            ret_string += "Fatal error: HTTP status " + (string)result["http_status_code"] + " API status " + (string)result["api_status_code"] + " API message " + (string)result["api_message"] + "\nFull details " + (string)result["details"];
        }

        return ret_string;
    }

    public static Hashtable send_sms(string data, string url) {
        string sms_result = Post(url, data);

        Hashtable result_hash = new Hashtable();

        string tmp = "";
        tmp += "Response from server: " + sms_result + "\n";
        string[] parts = sms_result.Split('|');

        string statusCode = parts[0];
        string statusString = parts[1];

        result_hash.Add("api_status_code", statusCode);
        result_hash.Add("api_message", statusString);

        if (parts.Length != 3) {
            tmp += "Error: could not parse valid return data from server.\n";
        } else {
            if (statusCode.Equals("0")) {
                result_hash.Add("success", 1);
                result_hash.Add("api_batch_id", parts[2]);
                tmp += "Message sent - batch ID " + parts[2] + "\n";
            } else if (statusCode.Equals("1")) {
                // Success: scheduled for later sending.
                result_hash.Add("success", 1);
                result_hash.Add("api_batch_id", parts[2]);
            } else {
                result_hash.Add("success", 0);
                tmp += "Error sending: status code " + parts[0] + " description: " + parts[1] + "\n";
            }
        }
        result_hash.Add("details", tmp);
        return result_hash;
    }

    public static string Post(string url, string data) {
        string result = null;
        try {
            byte[] buffer = Encoding.Default.GetBytes(data);

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);
            WebReq.Method = "POST";
            WebReq.ContentType = "application/x-www-form-urlencoded";
            WebReq.ContentLength = buffer.Length;
            Stream PostData = WebReq.GetRequestStream();

            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            Console.WriteLine(WebResp.StatusCode);

            Stream Response = WebResp.GetResponseStream();
            StreamReader _Response = new StreamReader(Response);
            result = _Response.ReadToEnd();
        } catch (Exception ex) {
            result += "\n" + ex.Message;
        }
        return result.Trim() + "\n";
    }

    public static string character_resolve(string body) {
        Hashtable chrs = new Hashtable();
        chrs.Add('Ω', "Û");
        chrs.Add('Θ', "Ô");
        chrs.Add('Δ', "Ð");
        chrs.Add('Φ', "Þ");
        chrs.Add('Γ', "¬");
        chrs.Add('Λ', "Â");
        chrs.Add('Π', "º");
        chrs.Add('Ψ', "Ý");
        chrs.Add('Σ', "Ê");
        chrs.Add('Ξ', "±");

        string ret_str = "";
        foreach (char c in body) {
            if (chrs.ContainsKey(c)) {
                ret_str += chrs[c];
            } else {
                ret_str += c;
            }
        }
        return ret_str;
    }

    public static string createMessage(string msisdn, string message) {
        /********************************************************************
        * Construct data                                                    *
        *********************************************************************/
        /*
        * Note the suggested encoding for the some parameters, notably
        * the username, password and especially the message.  ISO-8859-1
        * is essentially the character set that we use for message bodies,
        * with a few exceptions for e.g. Greek characters. For a full list,
        * see: http://bulksms.vsms.net/docs/eapi/submission/character_encoding/
        */

        string data = "";
        data += "username=" + HttpUtility.UrlEncode(username, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        data += "&password=" + HttpUtility.UrlEncode(password, System.Text.Encoding.GetEncoding("ISO-8859-1"));
        data += "&message=" + HttpUtility.UrlEncode(character_resolve(message), System.Text.Encoding.GetEncoding("ISO-8859-1"));
        data += "&msisdn=" + msisdn;
        data += "&want_report=1";

        return data;
    }
}