using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace Utility
{
    public class LoginResult
    {
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string tokenType { get; set; }
        public string refreshToken { get; set; }
        public string grantType { get; set; }
    }

    public class SmartSMS
    {
        public LoginResult Login(string smartUserName, string smartPassword)
        {
            var baseAddress = "https://messagingsuite.smart.com.ph/rest/auth/login";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";

            string parsedContent = "{\"username\":\"" + smartUserName + "\",\"password\":\"" + smartPassword + "\"}";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(parsedContent);

            Stream ReqStrm = http.GetRequestStream();
            ReqStrm.Write(bytes, 0, bytes.Length);
            ReqStrm.Close();
            ReqStrm.Dispose();
            ReqStrm = null;

            var response = http.GetResponse();
            var ResStrm = response.GetResponseStream();
            var sr = new StreamReader(ResStrm);
            var content = sr.ReadToEnd();

            ResStrm.Close();
            ResStrm.Dispose();
            ResStrm = null;

            sr.Close();
            sr.Dispose();
            sr = null;

            JavaScriptSerializer JSS = new JavaScriptSerializer();
            return JSS.Deserialize<LoginResult>(content);
        }

        public bool SendSMS(LoginResult loginResult, string smsMessage, string recipient)
        {
            var baseAddress = "https://messagingsuite.smart.com.ph/rest/messages/sms";

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.Headers.Add("Authorization", loginResult.tokenType + " " + loginResult.accessToken);

            string parsedContent = "{\"message\":{\"text\":\"" + smsMessage + "\" },\"endpoints\": [\"" + recipient + "\"],\"messageType\":\"sms\"}";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] bytes = encoding.GetBytes(parsedContent);

            Stream ReqStrm = http.GetRequestStream();
            ReqStrm.Write(bytes, 0, bytes.Length);
            ReqStrm.Close();
            ReqStrm.Dispose();
            ReqStrm = null;

            var response = http.GetResponse();
            var ResStrm = response.GetResponseStream();
            var sr = new StreamReader(ResStrm);
            var content = sr.ReadToEnd();

            ResStrm.Close();
            ResStrm.Dispose();
            ResStrm = null;

            sr.Close();
            sr.Dispose();
            sr = null;

            return content == "";
        }

    }
}
