using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Core1.Models;


namespace MVC_Core1.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
          
            return View();
        }

        public IActionResult details1()
        {
           Employee e1=new Employee();
            e1.empid = 101;
            e1.firstname = "John";
            e1.lastname = "Doe";
            e1.city = "New York";
            return View(e1);
        }

        public IActionResult viewdemo()
        {
            Employee e1 = new Employee();
            e1.empid = 101;
            e1.firstname = "John";
            e1.lastname = "Doe";
            e1.city = "New York";
            return View(e1);
        }

        public IActionResult create_employee()
        {
            return View();
        }
    }
}
