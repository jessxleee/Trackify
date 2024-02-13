using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD_Group_4.DAL;
using PFD_Group_4.Models;
using System.Collections.Generic;

namespace PFD_Group_4.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDAL applicationContext = new ApplicationDAL();
        private CustomerDAL customerContext = new CustomerDAL();
        private NotificationDAL notificationContext = new NotificationDAL();

        // GET: CustomerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult Details()
        {
            string customerId = HttpContext.Session.GetString("CustomerId");
            Application application = applicationContext.GetProgressStatus(customerId);
            return View(application);
        }

        public ActionResult FAQ()
        {
            return View();
        }

        private int GetApplicationID()
        {
            string customerId = HttpContext.Session.GetString("CustomerId");
            Application application = applicationContext.GetSpecificApplication(customerId);
            int applicationID = application.ApplicationID;
            return applicationID;
        }

        public ActionResult UpdateRead()
        {
            int applicationID = GetApplicationID();
            int updateSuccess = notificationContext.UpdateIsRead(applicationID);
            if (updateSuccess > 0)
            {
                return RedirectToAction(nameof(Notification));
            }
            return View();
        }
        public ActionResult Notification()
        {
            int applicationId = GetApplicationID();
            List<Notification> notificationList = notificationContext.GetAllNotification(applicationId);
            List<Notification> recentSorted = notificationList;
            recentSorted.Sort(
                delegate (Notification n1, Notification n2)
            {
                return n2.CreatedDate.CompareTo(n1.CreatedDate);
            });

                return View(recentSorted);
        }
        public ActionResult Profile()
        {
            string customerId = HttpContext.Session.GetString("CustomerId");
            Customer customer = customerContext.GetCustomerDetails(customerId);            
            return View(customer);
        }
        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CustomerDetails/Edit
        public ActionResult Edit(string username)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
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

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
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
    }
}
