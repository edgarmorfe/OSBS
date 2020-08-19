using DL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class UserAccountBL
    {
        public static List<UserAccount> GetUserRoleByUserId(string userId)
        {
            return UserAccountFacade.GetUserRoleByUserId(userId);
        }

        public static int InsertUserRole(string userName, int roleId)
        {
            return UserAccountFacade.InsertUserRole(userName, roleId);
        }
    }
}
