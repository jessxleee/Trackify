using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PFD_Group_4.Models
{
    public class Staff
    {
        [Required(ErrorMessage = "You must enter an ID")]
        [Display(Name = "Staff ID")]
        public string StaffID { get; set; }

        [Required(ErrorMessage = "Please enter staff name")]
        [Display(Name = "Staff Name")]
        public string SName { get; set; }

        [Display(Name = "Staff Gender")]
        public char SGender { get; set; }

        [Display(Name = "Staff Email Address")]
        [EmailAddress]
        //[RegularExpression(@"[A-Za-z09._%+-]+@[A-Za-z0-9.-]\.[A-Za-z]{2,4}")]
        public string SEmailAddr { get; set; }

        [Display(Name = "Staff Mobile Number")]
        public string SMobileNumber { get; set; }

        [Display(Name = "Date Joined")]
        [DataType(DataType.Date)]
        public DateTime SDateJoin { get; set; }

        [Required(ErrorMessage = "Please enter staff appointment")]
        public string SAppt { get; set; }

        [Required(ErrorMessage = "Please enter staff username")]
        [Display(Name = "Staff UserName")]
        public string SUsername { get; set; }

        [Required(ErrorMessage = "Please enter passsword")]
        public string SPassword { get; set; }
    }
}
