using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTutorial.Models;

namespace MVCTutorial.Controllers
{
    public class TestController : Controller
    {

        public ActionResult Index()
        {


            MVCTutorialEntities db = new MVCTutorialEntities();
            List<Department> list = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentID", "DepartmentName");
            List<EmployeeViewModel> listEmp = db.Employees.Where(x => x.Isbool == false).Select(x => new EmployeeViewModel { Name = x.Name, Address = x.Address, EmployeeID = x.EmployeeID, DepartmentName = x.Department.DepartmentName }).ToList();
            ViewBag.EmpVMList = listEmp;
            return View();

        }


        [HttpPost]
        public ActionResult Index(EmployeeViewModel model)
        {

            try
            {
                MVCTutorialEntities db = new MVCTutorialEntities();




                // db.SaveChanges();

                if (model.EmployeeID > 0)
                {
                    //update
                    Employee emp = db.Employees.SingleOrDefault(x => x.EmployeeID == model.EmployeeID && x.Isbool == false);
                    emp.DepartmentID = model.DepartmentID;
                    emp.Name = model.Name;
                    emp.Address = model.Address;
                    db.SaveChanges();

                }
                else
                {
                    //insert
                    Employee emp = new Employee();

                    emp.Name = model.Name;
                    emp.Address = model.Address;
                    emp.DepartmentID = model.DepartmentID;
                    emp.Isbool = false;
                    db.Employees.Add(emp);
                    db.SaveChanges();

                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //return RedirectToAction("Index");
        }


        public JsonResult DeleteEmp(int EmployeeID)
        {
            bool result = false;
            MVCTutorialEntities db = new MVCTutorialEntities();
            Employee emp = db.Employees.SingleOrDefault(x => x.Isbool == false && x.EmployeeID == EmployeeID);
            if (emp != null)
            {
                emp.Isbool = true;
                db.SaveChanges();
                result = true;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult SaveRecord(EmployeeViewModel model)
        //{

        //    try
        //    {
        //        MVCTutorialEntities db = new MVCTutorialEntities();
        //        Employee emp = new Employee();
        //        emp.Name = model.Name;
        //        emp.Address = model.Address;
        //        emp.DepartmentID = model.DepartmentID;


        //        db.Employees.Add(emp);
        //        db.SaveChanges();





        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    return RedirectToAction("Index");
        //}
        public ActionResult ShowPartial()
        {



            return PartialView("Partial1");

        }
        public ActionResult ShowEmployee(int EmployeeID)
        {

            MVCTutorialEntities db = new MVCTutorialEntities();
            List<EmployeeViewModel> listemp = db.Employees.Where(x => x.Isbool == false && x.EmployeeID == EmployeeID).Select(x => new EmployeeViewModel { Name = x.Name, Address = x.Address, EmployeeID = x.EmployeeID, DepartmentName = x.Department.DepartmentName }).ToList();
            ViewBag.EmployeeList = listemp;
            return PartialView("Partial1");

        }
        public ActionResult AddEditEmployee(int EmployeeID)
        {

            MVCTutorialEntities db = new MVCTutorialEntities();
            EmployeeViewModel model = new EmployeeViewModel();
            List<Department> list = db.Departments.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentID", "DepartmentName");

            if (EmployeeID > 0)
            {
                Employee emp = db.Employees.SingleOrDefault(x => x.EmployeeID == EmployeeID && x.Isbool == false);
                model.EmployeeID = emp.EmployeeID;
                model.DepartmentID = emp.DepartmentID;
                model.Address = emp.Address;
                model.Name = emp.Name;
            }
            return PartialView("Partial2", model);


        }
        public ActionResult EmployeeDetail()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(RegistrationViewModel model)
        {
            MVCTutorialEntities db = new MVCTutorialEntities();
            SiteUser siteUser = new SiteUser();
            siteUser.UserName = model.UserName;
            siteUser.EmailId = model.EmailId;
            siteUser.Password = model.Password;
            siteUser.Address = model.Address;
            siteUser.RoleId = 3;
            db.SiteUsers.Add(siteUser);
            db.SaveChanges();

            return View();
        }
    }
}


//// GET: Test
//public ActionResult Index()
//{
//    MVCTutorialEntities db = new MVCTutorialEntities();

//    List<Employee> employeelist = db.Employees.ToList();


//    List<EmployeeViewModel> employeeVMList = employeelist.Select(x => new EmployeeViewModel
//    {
//        Name = x.Name,
//        EmployeeID = x.EmployeeID,

//    }).ToList();


//    // Address =x.Address,
//    // DepartmentID =x.DepartmentID,
//    // DepartmentName =x.Department.DepartmentName 


//    return View(employeeVMList);
//}

//public ActionResult EmployeeDetail(int EmployeeID)
//{
//    MVCTutorialEntities db = new MVCTutorialEntities();
//    Employee employee = db.Employees.SingleOrDefault(x => x.EmployeeID == EmployeeID);

//    EmployeeViewModel employeeVM = new EmployeeViewModel();
//    employeeVM.Name = employee.Name;
//    employeeVM.Address = employee.Address;
//    employeeVM.DepartmentName = employee.Department.DepartmentName;
//    return View(employeeVM);


//}