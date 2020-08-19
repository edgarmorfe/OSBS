using DL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class RoleBL
    {
        public static List<Role> GetAllRole()
        {
            return RoleFacade.GetAllRole();
        }
    }
}
