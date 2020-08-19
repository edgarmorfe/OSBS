using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Utility;

namespace WebUI.Helpers
{
    public static class SMSHelper
    {
        public static bool SendSMS(string mobileno, string txtmsgbody)
        {
            try
            {
                string smsuser = ConfigurationManager.AppSettings["smsusername"].ToString();
                string smspassword = ConfigurationManager.AppSettings["smspassword"].ToString();

                SmartSMS smsmsg = new SmartSMS();
                LoginResult lr = smsmsg.Login(smsuser, smspassword);
                return smsmsg.SendSMS(lr, txtmsgbody, mobileno);
            }
            catch (Exception)
            {
                // TBD: email notification/other
                return false;
            }

        }
    }
}