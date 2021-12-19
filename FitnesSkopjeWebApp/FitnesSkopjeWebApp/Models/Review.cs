using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Models
{
    public class Review
    {

        [Key]
        public int id { get; set; }
        //fk
        public int userId { get; set; }
        //fk
        public int gymId { get; set; }

        [Display(Name = "Оценка")]
        public int rating { get; set; }

        [Display(Name = "Коментар")]
        public string comment { get; set; }

        public virtual Gym gym { get; set; }
        public virtual User user { get; set; }
    }
}