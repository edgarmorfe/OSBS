using DL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class EmployeeBL
    {
        public static List<Employee> GetEmployeeMasterInfoByLoginId(string loginId)
        {
            return EmployeeFacade.GetEmployeeMasterInfoByLoginId(loginId);
        }
        public static List<Employee> GetEmployeeMasterInfoByManNo(string manNo)
        {
            return EmployeeFacade.GetEmployeeMasterInfoByManNo(manNo);
        }

        public static List<Employee> GetEmployeeMasterInfoBySupEmpNo(string supEmpNo)
        {
            return EmployeeFacade.GetEmployeeMasterInfoBySupEmpNo(supEmpNo);
        }

        public static List<Employee> GetEmployeeMaster()
        {
            return EmployeeFacade.GetEmployeeMaster();
        }
    }
}
