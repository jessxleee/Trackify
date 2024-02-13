using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PFD_Group_4.Models
{
    public class Message
    {
        [Display(Name = "Message ID")]
        public int MessageID { get; set; }

        [Display(Name = "Message Subject")]
        [StringLength(100, ErrorMessage = "Message Subject cannot exceed 100 characters.")]
        public string MessageSubject { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
