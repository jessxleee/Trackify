using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PFD_Group_4.Models;
using PFD_Group_4.DAL;

namespace PFD_Group_4.Controllers
{
    public class HomeController : Controller
    {
        private StaffDAL staffContext = new StaffDAL();
        private CustomerDAL customerContext = new CustomerDAL();
        private ApplicationDAL applicationContext = new ApplicationDAL();
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StaffLogin(IFormCollection formData)
        {
            string loginID = formData["LoginID"].ToString();
            string password = formData["userPassword"].ToString();
            bool success = false;
            List<Staff> staffList = staffContext.GetAllStaff();
            foreach (Staff staff in staffList)
            {
                if (loginID == staff.SUsername && password == staff.SPassword)
                {
                    HttpContext.Session.SetString("Username", staff.SUsername);
                    HttpContext.Session.SetString("Role", "Staff");
                    success = true;
                    return RedirectToAction("Index", "Staff");
                }
            }
            if (success == false)
            {
                TempData["Message"] = "Invalid Login Credentials!";
            }
            return RedirectToAction("StaffLogin");
        }
        public ActionResult StaffLogin()
        {
            return View();
        }
        public ActionResult StaffMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustLogin(IFormCollection formData)
        {
            string loginID = formData["LoginID"].ToString();
            string password = formData["userPassword"].ToString();
            bool success = false;
            List<Customer> customerList = customerContext.GetAllCustomer();
            foreach (Customer customer in customerList)
            {
                if (loginID == customer.CUsername && password == customer.CPassword)
                {
                    success = true;
                    Application applicationProgress = applicationContext.GetProgressStatus(customer.CustomerID);
                    if (customer.CInvestAccountNo == null && applicationProgress == null)
                    {
                        HttpContext.Session.SetString("CustomerId", customer.CustomerID);
                        return RedirectToAction("ApplicationType", "Application");
                    }
                    else
                    {
                        HttpContext.Session.SetString("CustomerId", customer.CustomerID);
                        HttpContext.Session.SetString("Role", "Customer");
                        return RedirectToAction("CustMenu");
                    }
                }
            }
            if (success == false)
            {
                TempData["Message"] = "Invalid Login Credentials!";
            }
            return RedirectToAction("CustLogin");
        }
        public ActionResult CustLogin()
        {
            return View();
        }
        public ActionResult CustMenu()
        {
            return View();
        }
        public ActionResult ApplicationMain()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            // Call the Index action of Home controller
            return RedirectToAction("Index");
        }
    }
}
