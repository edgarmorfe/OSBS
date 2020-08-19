using Dapper;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public static class UserRoleFacade
    {
        public static int Create(UserRole userRole)
        {
            DynamicParameters dParams = new DynamicParameters();
            dParams.Add("@userId", userRole.UserId);
            dParams.Add("@roleId", userRole.RoleId);
            return DataAccessHelper.SaveData<UserRole>("spInsertUserRole", dParams);
        }

        public static List<UserRole> GetUserRoles()
        {
            return DataAccessHelper.LoadData<UserRole>("spGetUserRoles");
        }

        public static UserRole GetUserRoleById(int id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@id", id);
            return DataAccessHelper.LoadData<UserRole>("spGetUserRoleById", dParam).FirstOrDefault();
        }

        public static int Update(UserRole userRole)
        {
            var dParams = new DynamicParameters();
            dParams.Add("@id", userRole.Id);
            dParams.Add("@roleId", userRole.RoleId);
            return DataAccessHelper.SaveData("spUpdateUserRoleById", dParams);
        }

        public static int Delete(int id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@id", id);
            return DataAccessHelper.SaveData("spDeleteUserRoleById", dParam);
        }
    }
}
