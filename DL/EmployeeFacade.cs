using Dapper;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public static class EmployeeFacade
    {
        public static List<Employee> GetEmployeeMasterInfoByLoginId(string loginId)
        {
            string query = $"SELECT [empNo],[empFName],[empMName],[empLName],[empCorpEmail],[empMobile],[coName],[teamName],[empLoginID],[supEmpNo],[supEmpFName],[supEmpMName],[supEmpLName],[supMobileNo],[supEmail] FROM [dbo].[vw_tblEmpConMaster] WHERE empLoginID = @loginId";
            DynamicParameters dParam = new DynamicParameters();
            dParam.Add("@loginId", loginId);
            return DataAccessHelper.LoadDataText<Employee>(query, dParam, connectionName: "SYSDEVMaster");
        }

        public static List<Employee> GetEmployeeMasterInfoByManNo(string manNo)
        {
            var query = $"SELECT [empNo],[empFName],[empMName],[empLName],[empCorpEmail],[empMobile],[coName],[teamName],[empLoginID] FROM [dbo].[tblEmpMaster] WHERE empNo = @manNo";
            var dParam = new DynamicParameters();
            dParam.Add("@manNo", manNo);
            return DataAccessHelper.LoadDataText<Employee>(query, dParam, connectionName: "SYSDEVMaster");
        }

        public static List<Employee> GetEmployeeMasterInfoBySupEmpNo(string supEmpNo)
        {
            var query = $"SELECT [empNo],[empFName],[empMName],[empLName],[empCorpEmail],[empMobile],[coName],[teamName],[empLoginID],[supEmpNo],[supEmpFName],[supEmpMName],[supEmpLName],[supMobileNo],[supEmail] FROM [dbo].[vw_tblEmpConMaster] WHERE SupEmpNo = @sumEmpNo";
            var dParam = new DynamicParameters();
            dParam.Add("@sumEmpNo", supEmpNo);
            return DataAccessHelper.LoadDataText<Employee>(query, dParam, connectionName: "SYSDEVMaster");
        }

        public static List<Employee> GetEmployeeMaster()
        {
            var query = $"SELECT [empNo],[empFName],[empMName],[empLName],[empCorpEmail],[empMobile],[coName],[teamName],[empLoginID] FROM [dbo].[tblEmpMaster]";
            return DataAccessHelper.LoadDataText<Employee>(query, connectionName: "SYSDEVMaster");
        }
    }
}
