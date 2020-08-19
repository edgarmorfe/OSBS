using BL;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Attributes;
using WebUI.Models;

namespace WebUI.Controllers
{
    [SessionExpire]
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return RedirectToAction("ViewAll");
        }

        public ActionResult ViewAll()
        {
            //var employees = EmployeeBL.Read();
            //List<EmployeeModel> employeeList = new List<EmployeeModel>();
            //employeeList = GenerateList(employees, employeeList);
            return View();
        }

        protected List<EmployeeModel> GenerateList(List<Employee> employees, List<EmployeeModel> employeelist)
        {
            foreach (Employee emp in employees)
            {
                employeelist.Add(GenerateDriverModel(emp));
            }
            return employeelist;
        }
        protected EmployeeModel GenerateDriverModel(Employee empModel)
        {
            EmployeeModel model = new EmployeeModel()
            {
               
            };
            return model;
        }
    }
}