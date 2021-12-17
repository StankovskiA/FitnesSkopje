using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Models
{
    public class GymTypes
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }
}