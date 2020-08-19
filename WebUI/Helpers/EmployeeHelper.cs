using DomainObject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WebUI.Helpers
{
    public static class EmployeeHelper
    {
        public static string GetEmployeeProfilePicPath(Employee empName)
        {
            // profile pic path
            string profilePicPath = $"http://hris.rockwell.com.ph/images/emp_pics/{empName.empNo}.jpg";
            
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(profilePicPath);
            request.Method = "HEAD";

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException)
            {
               return profilePicPath = $"http://hris.rockwell.com.ph/images/emp_pics/0000.jpg";
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }
            return profilePicPath;
        }
    }
}