using BL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UserRoleController : Controller
    {
        // GET: UserRole
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var _roles = RoleBL.GetAllRole();
            var _userRoleMod = new UserRoleView();
            _userRoleMod.Roles = new List<Models.Role>();
            foreach (DomainObject.Role _role in _roles)
            {
                var _roleMod = new Models.Role()
                {
                    Id = _role.Id,
                    RoleName = _role.RoleName
                };
                _userRoleMod.Roles.Add(_roleMod);
            }
            return View(_userRoleMod);
        }

        public ActionResult all()
        {
            var _userRoles = UserRoleBL.GetUserRoles();
            var _userRolesMod = new List<UserRoleView>();
            foreach (UserRole _userRole in _userRoles)
            {
                UserRoleView _userRoleView = new UserRoleView()
                {
                    Id = _userRole.Id,
                    UserName = _userRole.UserId,
                    RoleId = _userRole.RoleId
                };
                _userRolesMod.Add(_userRoleView);
            }
            StringBuilder reserveStr = GenerateUserRoleTable(_userRolesMod);
            TempData["reseveList"] = reserveStr;
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserRoleView userRolemod)
        {
            var _userRole = new UserRole()
            {
                UserId = userRolemod.Name,
                RoleId = userRolemod.RoleId
            };

            UserRoleBL.Create(_userRole);
            TempData["message"] = "Saved Successful";
            return RedirectToAction("Create");
        }

        public ActionResult update(int id)
        {
            var _roles = RoleBL.GetAllRole();
            var _userRole = UserRoleBL.GetUserRoleById(id);
            var _userRoleMod = new UserRoleView()
            {
                Id = _userRole.Id,
                UserName = _userRole.UserId,
                RoleId = _userRole.RoleId
            };

            _userRoleMod.Roles = new List<Models.Role>();
            foreach (DomainObject.Role _role in _roles)
            {
                var _roleMod = new Models.Role()
                {
                    Id = _role.Id,
                    RoleName = _role.RoleName
                };
                _userRoleMod.Roles.Add(_roleMod);
            }
            return View(_userRoleMod);
        }

        [HttpPost]
        public ActionResult Update(UserRoleView userRoleMod)
        {
            var _userRole = new UserRole()
            {
                Id = userRoleMod.Id,
                UserId = userRoleMod.UserName,
                RoleId = userRoleMod.RoleId
            };
            UserRoleBL.Update(_userRole);
            TempData["message"] = "Update Successful";
            return RedirectToAction("Update", new { id = userRoleMod.Id});
        }

        public ActionResult delete(int id)
        {
            UserRoleBL.Delete(id);
            TempData["message"] = "Delete Successful";
            return RedirectToAction("all");
        }

        [HttpGet]
        public JsonResult EmployeeName()
        {
            var employees = EmployeeBL.GetEmployeeMaster().ToList();
            List<EmployeeModel> empList = new List<EmployeeModel>();

            foreach (Employee emp in employees)
            {
                EmployeeModel model = new EmployeeModel()
                {
                    UserName = emp.empLoginID,
                    FullName = $"{emp.empFName} {emp.empLName}"
                };
                empList.Add(model);
            }

            return Json(empList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EmployeeNameByFullName(string empNo)
        {
            var employees = EmployeeBL.GetEmployeeMaster().Where(e => $"{e.empFName} {e.empLName}" == empNo).ToList();
            List<EmployeeModel> empList = new List<EmployeeModel>();

            foreach (Employee emp in employees)
            {
                EmployeeModel model = new EmployeeModel()
                {
                    UserName = emp.empNo,
                    FullName = $"{emp.empFName} {emp.empLName}"
                };
                empList.Add(model);
            }
            return Json(empList, JsonRequestBehavior.AllowGet);
        }

        private StringBuilder GenerateUserRoleTable(List<UserRoleView> userRolesModel)
        {
            var reservesStr = new StringBuilder();
            foreach (UserRoleView _userRole in userRolesModel)
            {
                var _roleName = RoleBL.GetAllRole().Where(r => r.Id == _userRole.RoleId).FirstOrDefault();
                reservesStr.Append($"[\"{_userRole.Id}\",\"{_userRole.UserName}\",\"{_roleName.RoleName}\", " +
                    $"'<a href=\"/userrole/update/{_userRole.Id}\"><i class=\"material-icons\">edit</i></a><a href=\"/userrole/delete/{_userRole.Id}\"><i class=\"material-icons\">delete_forever</i></a>'],");
            }
            reservesStr.Remove(reservesStr.Length - 1, 1);
            return reservesStr;
        }
    }
}
