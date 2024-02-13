using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PFD_Group_4.Models
{
    public class Notification
    {
        [Display(Name = "Notification ID")]
        public int NotificationID { get; set; }

        public int ApplicationID { get; set; }

        public string NotificationHeader { get; set; }

        public string NotificationBody { get; set; }

        public bool IsRead { get; set; }
       
        public DateTime CreatedDate { get; set; }
    }
}
