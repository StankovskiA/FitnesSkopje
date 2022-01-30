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

        [Display(Name = "Име")]
        public string firstName { get; set; }

        [Display(Name = "Презиме")]
        public string lastName { get; set; }

        [Display(Name = "Е-маил")]
        [EmailAddress]
        public string email { get; set; }

        [Display(Name = "Адреса")]
        public string address { get; set; }

        [Display(Name = "Телефонски број")]
        public string phoneNumber { get; set; }
        public string roleId { get; set; } 

        [Display(Name = "Корисничко име")]
        public string username { get; set; }
    }
}