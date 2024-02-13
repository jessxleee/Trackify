using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace PFD_Group_4.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }

        [Display(Name = "Customer Name")]
        [StringLength(50, ErrorMessage = "Customer Name cannot exceed 50 characters.")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer NRIC")]
        [MinLength(9, ErrorMessage = "Customer NRIC cannot be less than 9 characters.")]
        [MaxLength(9, ErrorMessage = "Customer NRIC cannot exceed 9 characters.")]
        public string CustomerNRIC { get; set; }

        [Display(Name = "Customer Address")]
        public string CAddress { get; set; }

        [Display(Name = "Customer Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CDOB { get; set; }

        [Display(Name = "Customer Nationality")]
        public char CNationality { get; set; }

        [Display(Name = "Customer Race")]
        public string CRace { get; set; }

        [Display(Name = "Customer Gender")]
        public char CGender { get; set; }

        [Display(Name = "Customer Email Address")]
        [EmailAddress]
        public string CEmailAddress { get; set; }

        [Display(Name = "Customer Mobile Number")]
        public string CMobileNumber { get; set; }
        [MinLength(8, ErrorMessage = "Customer Mobile Number cannot be less than 8 characters.")]
        [MaxLength(8, ErrorMessage = "Customer Mobile Number cannot exceed 8 characters.")]

        [Display(Name = "Customer Bankrupt Status")]
        public char CBankruptStatus { get; set; }

        [Display(Name = "Customer Deposit Account Number")]
        [StringLength(14, ErrorMessage = "Deposit Account Number cannot exceed 14 characters.")]
        public string CDepositAccountNo { get; set; }

#nullable enable
        [Display(Name = "Customer Investment Account Number")]
        public string? CInvestAccountNo { get; set; }
#nullable disable
        [Display(Name = "Progress Status")]
        public char ProgressStatus { get; set; }
    }
}
