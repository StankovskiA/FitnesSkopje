using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Models
{
    public class User
    {

        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        
        [EmailAddress]
        public string email { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string roleId { get; set; } 

        [Display(Name = "Корисничко име")]
        public string username { get; set; }
    }
}