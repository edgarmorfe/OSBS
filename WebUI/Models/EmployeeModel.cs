using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class EmployeeModel
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}