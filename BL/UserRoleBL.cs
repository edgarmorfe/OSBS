using DL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class UserRoleBL
    {
        public static int Create(UserRole userRole)
        {
            return UserRoleFacade.Create(userRole);
        }

        public static List<UserRole> GetUserRoles()
        {
            return UserRoleFacade.GetUserRoles();
        }
        public static UserRole GetUserRoleById(int id)
        {
            return UserRoleFacade.GetUserRoleById(id);
        }

        public static int Update(UserRole userRole)
        {
            return UserRoleFacade.Update(userRole);
        }
        public static int Delete(int id)
        {
            return UserRoleFacade.Delete(id);
        }
    }
}
