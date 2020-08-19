using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObject
{
    public class Employee : IEmployee
    {
        public string empNo { get ; set; }
        public string empFName { get; set; }
        public string empMName { get; set; }
        public string empLName { get; set; }
        public string empCorpEmail { get; set; }
        public string empMobile { get; set; }
        public string coName { get; set; }
        public string teamName { get; set; }
        public string empLoginID { get; set; }
        public string supEmpNo { get; set; }
        public string supEmpFName { get; set; }
        public string supEmpMName { get; set; }
        public string supEmpLName { get; set; }
        public string supMobileNo { get; set; }
        public string supEmail { get; set; }
    }
}
