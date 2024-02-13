using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PFD_Group_4.DAL;
using PFD_Group_4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace PFD_Group_4.Controllers
{
    public class ApplicationController : Controller
    {
        //DAl
        private ApplicationDAL applicationContext = new ApplicationDAL();
        private CustomerDAL customerContext = new CustomerDAL();

        //Private Methods
        private List<SelectListItem> GetAnnualIncomes()
        {
            List<SelectListItem> annualIncome = new List<SelectListItem>();
            annualIncome.Add(new SelectListItem
            {
                Value = "< S$30,000",
                Text = "< S$30,000"
            });
            annualIncome.Add(new SelectListItem
            {
                Value = "S$30,000 - S$60,000",
                Text = "S$30,000 - S$60,000"
            });
            annualIncome.Add(new SelectListItem
            {
                Value = "S$60,001 - S$100,000",
                Text = "S$60,001 - S$100,000"
            });
            annualIncome.Add(new SelectListItem
            {
                Value = "S$100,001 - S$150,000",
                Text = "S$100,001 - S$150,000"
            });
            annualIncome.Add(new SelectListItem
            {
                Value = "S$150,001 - S$300,000",
                Text = "S$150,001 - S$300,000"
            });
            annualIncome.Add(new SelectListItem
            {
                Value = "> S$300,000",
                Text = "> S$300,000"
            });

            return annualIncome;
        }
        //GET: ApplicationController
        public ActionResult ApplicationType()
        {
            string customerId = HttpContext.Session.GetString("CustomerId");
            Customer customer = customerContext.GetCustomerDetails(customerId);
            return View(customer);
        }

        // GET: ApplicationController
        public ActionResult CustomerDetails()
        {
            string customerId = HttpContext.Session.GetString("CustomerId");
            Customer customer = customerContext.GetCustomerDetails(customerId);
            return View(customer);
        }


        //GET : ApplicationController/Create
        public ActionResult ApplicationForm()
        {
            string customerId = HttpContext.Session.GetString("CustomerId");

            //Setting application attributes/members 
            Application application = new Application();
            application.CustomerID = customerId;
            ViewData["AnnualIncomeList"] = GetAnnualIncomes();
            return View(application);
        }

        //POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationForm(Application application)
        {
            //Adding New Application Attribute values
            DateTime submitedTime = DateTime.Today;
            application.DateTimeApplied = submitedTime;
            char progressStatusDefault = '1';
            application.ProgressStatus = progressStatusDefault;
            ViewData["AnnualIncomeList"] = GetAnnualIncomes();
            //await UploadFile(application);

            if (ModelState.IsValid)
            {
                application.ApplicationID = applicationContext.Add(application);
                return RedirectToAction("UploadFile", "Application", new { id = application.ApplicationID });
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid inputs! Try again.";
                return View(application);
            }
        }
        public ActionResult UploadFile(string id)
        {
            Application application = applicationContext.GetSpecificApplication(id);
            return View(application);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadFile(Application application)
        {
            if (application.fileToUpload != null &&
            application.fileToUpload.Length > 0)
            {
                try
                {
                    // Find the filename extension of the file to be uploaded.
                    string fileExt = Path.GetExtension(
                     application.fileToUpload.FileName);
                    // Rename the uploaded file with the customer’s name.
                    string uploadedFile = application.CustomerID + fileExt;
                    // Get the complete path to the images folder in server
                    string savePath = Path.Combine(
                     Directory.GetCurrentDirectory(),
                     "wwwroot\\Documents", uploadedFile);
                    // Upload the file to server
                    using (var fileSteam = new FileStream(
                     savePath, FileMode.Create))
                    {
                        application.fileToUpload.CopyTo(fileSteam);
                    }
                    application.ApplicationDocument = uploadedFile;
                    ViewData["Message"] = "File uploaded successfully.";
                }
                catch (IOException)
                {
                    //File IO error, could be due to access rights denied
                    ViewData["Message"] = "File uploading fail!";
                }
                catch (Exception ex) //Other type of error
                {
                    ViewData["Message"] = ex.Message;
                }
            }
            return View(application);
        }
        public ActionResult DisplayApplications()
        {
            
            List<Application> applicationList = applicationContext.GetAllApplication();
            return View(applicationList);
        }
        public ActionResult Finished()
        {
            return View();
        }

        public IActionResult Application()
        {
            return View("Index");
        } 

        // GET: ApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public Application GetApplication(string customerID)
        {
            Application application = applicationContext.GetSpecificApplication(customerID);
            return application;
        }

        
    }
}
