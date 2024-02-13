using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace PFD_Group_4.Models
{
    public class Application
    {
        [Display(Name = "Application ID")]
        public int ApplicationID { get; set; }

        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }

        [Display(Name = "Date and Time of Application")]
        [DataType (DataType.DateTime)]
        
        public DateTime DateTimeApplied { get; set; }

        [Display(Name = "Employment Status")]
        public char SelfEmployed { get; set; }

        [Display(Name = "Annual Income")]
        public string AnnualIncome { get; set; }

        [Display(Name = "Progress Status")]
        public char ProgressStatus { get; set; }

        [Display(Name = "Documents")]
        public string ApplicationDocument { get; set; }

        public IFormFile fileToUpload { get; set; }
    }
}
