using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudWithStoreprocedure.Models;
namespace CrudWithStoreprocedure.Controllers
{
    public class HomeController : Controller
    {
        DataAccessLayer db = new DataAccessLayer();
        [HttpGet]
        public ActionResult Index()
        {
            List<Customer> customerlist = db.ShowList().ToList();
            return View(customerlist);

        }
        [HttpPost]
        public ActionResult Index(Customer objcust)
        {
            db.InsertData(objcust);
            ModelState.Clear();
            TempData["msg"] = "Data Successfully";
            Response.Write("<script>alert('Data SuccessFully')</script>");
            return View();
        }
        public ActionResult Delete(int id)
        {
            db.DeleteData(id);
            Response.Write("<script>alert('Are You Sure You Want TO Delete')</script>");

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer cobj = new Customer();
            DataAccessLayer objDB = new DataAccessLayer();
            return View(objDB.SelectDatabyID(id));
        }
        [HttpPost]
        public ActionResult Edit(Customer objcust)
        {
            db.UpdateData(objcust);
            ModelState.Clear();
            Response.Write("<script>alert('Update SuccessFully')</script>");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(string Email, string Password)
        {
            string queryr = "select Email,Password From tbl_CrudDemo Where='" + Email + "'And Password='" + Password + "'";
            db.UserLogin(Email, Password);
            {
                Response.Write("<script>alert('Email and Password Not Match')</script>");
            }
            return RedirectToAction("Index","User");
        }

    }
}