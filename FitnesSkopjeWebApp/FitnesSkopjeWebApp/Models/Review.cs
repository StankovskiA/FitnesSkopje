using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Models
{
    public class Review
    {
        //fk
        public int userId { get; set; }
        //fk
        public int gymId { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
    }
}