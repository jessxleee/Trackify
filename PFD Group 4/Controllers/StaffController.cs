using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PFD_Group_4.DAL;
using PFD_Group_4.Models;
using System;
using System.Collections.Generic;

namespace PFD_Group_4.Controllers
{
    public class StaffController : Controller
    {
        private CustomerDAL customerContext = new CustomerDAL();
        private ApplicationDAL applicationContext = new ApplicationDAL();
        public IActionResult Index()
        {
            List<Customer> customerList = customerContext.GetAllCustomer();
            List<CustomerViewModel> customerVMList = new List<CustomerViewModel>();
            foreach (Customer customer in customerList)
            {
                CustomerViewModel customerVM = MapToCustomerVM(customer);
                customerVMList.Add(customerVM);
            }
            return View(customerVMList);
        }
        public ActionResult Details(string id)
        {
            Customer customer = customerContext.GetCustomerDetails(id);
            CustomerViewModel customerVM = MapToCustomerVM(customer);
            return View(customerVM);
        }
        public ActionResult Account(string id)
        {
            Customer customer = customerContext.GetCustomerDetails(id);
            return View(customer);
        }
        public CustomerViewModel MapToCustomerVM(Customer customer)
        {
            char progressStatus = '\0';
            if (customer.CustomerID != null)
            {
                List<Application> applicationList = applicationContext.GetAllApplication();
                foreach(Application application in applicationList)
                {
                    if (customer.CustomerID == application.CustomerID)
                    {
                        progressStatus = application.ProgressStatus;
                        break;
                    }
                                           
                }
            }

            CustomerViewModel customerVM = new CustomerViewModel
            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                CustomerNRIC = customer.CustomerNRIC,
                CAddress = customer.CAddress,
                CDOB = customer.CDOB,
                CNationality = customer.CNationality,
                CRace = customer.CRace,
                CGender = customer.CGender,
                CEmailAddress = customer.CEmailAddress,
                CMobileNumber = customer.CMobileNumber,
                CBankruptStatus = customer.CBankruptStatus,
                CDepositAccountNo = customer.CDepositAccountNo,
                CInvestAccountNo = customer.CInvestAccountNo,
                ProgressStatus = progressStatus,
            };

            return customerVM;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(IFormCollection formData)
        {
            List<string> customeridList = customerContext.GetCustomerID();
            string customerid = formData["CustomerID"].ToString();
            if (customeridList.Contains(customerid))
            {
                Customer customer = customerContext.GetCustomerDetails(customerid);
                return RedirectToAction("Detail", "Staff", new { customer.CustomerID });
            }
            else
            {
                TempData["IdNotFound"] = "Records not found.";
                return RedirectToAction("Index", "Staff");
            }

        }
        public IActionResult Compiling()
        {
            List<Customer> customerList = customerContext.GetAllCustomer();
            List<CustomerViewModel> customerVMList = new List<CustomerViewModel>();
            foreach (Customer customer in customerList)
            {
                CustomerViewModel customerVM = MapToCustomerVM(customer);
                if(customerVM.ProgressStatus == '1')
                {
                    customerVMList.Add(customerVM);
                }
                
            }
            return View(customerVMList);
        }
        public IActionResult Reviewing()
        {
            List<Customer> customerList = customerContext.GetAllCustomer();
            List<CustomerViewModel> customerVMList = new List<CustomerViewModel>();
            foreach (Customer customer in customerList)
            {
                CustomerViewModel customerVM = MapToCustomerVM(customer);
                if (customerVM.ProgressStatus == '2')
                {
                    customerVMList.Add(customerVM);
                }

            }
            return View(customerVMList);
        }
        public IActionResult Completed()
        {
            List<Customer> customerList = customerContext.GetAllCustomer();
            List<CustomerViewModel> customerVMList = new List<CustomerViewModel>();
            foreach (Customer customer in customerList)
            {
                CustomerViewModel customerVM = MapToCustomerVM(customer);
                if (customerVM.ProgressStatus == '3')
                {
                    customerVMList.Add(customerVM);
                }

            }
            return View(customerVMList);
        }

        public ActionResult ViewStatus(string id)
        {
            Application application = applicationContext.GetSpecificApplication(id);
            HttpContext.Session.SetString("Status", application.ProgressStatus.ToString());
            HttpContext.Session.SetString("id", application.CustomerID);
            return View(application);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewStatus (IFormCollection formData, Application application)
        {
            string id = HttpContext.Session.GetString("id");
            string status = HttpContext.Session.GetString("Status");
            if (status == "1")
            {
                applicationContext.Update('2', id);
                return RedirectToAction("Index", "Staff");
            }
            else if (status == "2")
            {
                applicationContext.Update('3', id);
                return RedirectToAction("Index", "Staff");
            }
            else if (status == "0")
            {
                applicationContext.Update('1', id);
                return RedirectToAction("Index", "Staff");
            }
            else if (status == "3")
            {

                TempData["ErrorMessage"] = "Application has been created. No updates are needed";
                return View();
            }

            else
            {
                TempData["ErrorMessage"] = "Unable to update";
                return View();
            }
            }
        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult Error(IFormCollection formData, Application application)
        {
            string id = HttpContext.Session.GetString("id");
            applicationContext.Update('0', id);
            return RedirectToAction("Index", "Staff");

        }

        public ActionResult EditDetails(string id)
        {

            Customer customer = customerContext.GetCustomerDetails(id);
            CustomerViewModel customerVM = MapToCustomerVM(customer);
            return View(customerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDetails(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerContext.UpdateCustomerDetails(customer);
                return RedirectToAction("Index");
            }
            else
            {
                return View(customer);
            }
        }

    }
}
