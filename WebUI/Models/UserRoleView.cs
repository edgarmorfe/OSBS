using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class UserRoleView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public List<Role> Roles { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}