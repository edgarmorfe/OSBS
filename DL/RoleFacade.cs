using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public static class RoleFacade
    {
        public static List<Role> GetAllRole()
        {
            return DataAccessHelper.LoadData<Role>("spGetAllRole");
        }

    }
}
