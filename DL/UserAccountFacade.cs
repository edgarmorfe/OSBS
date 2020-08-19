using Dapper;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public static class UserAccountFacade
    {
        public static List<UserAccount> GetUserRoleByUserId(string userId)
        {
            var dParam = new DynamicParameters();
            dParam.Add("userId", userId);
            return DataAccessHelper.LoadData<UserAccount>("spGetUserRoleByUserId", dParam);
        }

        public static int InsertUserRole(string userName, int roleId)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@userId", userName);
            dParams.Add("@roleId", roleId);
            return DataAccessHelper.SaveData("spInsertUserRole", dParams);
        }
    }
}
