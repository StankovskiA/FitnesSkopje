using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Models
{
    public class Favourite
    {
       [Key]
        public int id { get; set; }
        public int userId { get; set; }
        public int gymId { get; set; }
        public string gymName { get; set; }
    }
}