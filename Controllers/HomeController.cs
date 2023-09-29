using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using GGProjectss.Models;

namespace GGProjectss.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDBEntities employeeDBEntities = new EmployeeDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            try
            {
                if (id > 0)
                {
                    var list = employeeDBEntities.Employees.Where(x => x.Deleted == false && x.Id==id).FirstOrDefault();
                   return View(list);

                }
                else
                {
                    var list = employeeDBEntities.Employees.Where(x => x.Deleted == false).ToList();
                    ViewBag.Employees = list;

                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee obj)
        {
            try
            {
                var dataList=employeeDBEntities.Employees.Where(x=>x.Id==obj.Id && x.Deleted==false).FirstOrDefault();
                if (dataList !=null)
                {
                    obj.Deleted = false;
                    employeeDBEntities.Entry(dataList).CurrentValues.SetValues(obj);
                    employeeDBEntities.Entry(dataList).State = System.Data.Entity.EntityState.Modified;
                    employeeDBEntities.SaveChanges();

                }
                else
                {
                    obj.Deleted = false;
                    employeeDBEntities.Employees.Add(obj);
                    employeeDBEntities.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Create", "Home");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var data = employeeDBEntities.Employees.Where(x => x.Id == id).FirstOrDefault();
                if (data != null)
                {
                    data.Deleted = true;
                    employeeDBEntities.Entry(data).CurrentValues.SetValues(data);
                    employeeDBEntities.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    employeeDBEntities.SaveChanges();



                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return RedirectToAction("Create", "Home");

        }

        public ActionResult getEmployeeById(int id)
        {
            try
            {
                var data = employeeDBEntities.Employees.Where(x => x.Id == id).FirstOrDefault();
                return Json (data,JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
           

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}