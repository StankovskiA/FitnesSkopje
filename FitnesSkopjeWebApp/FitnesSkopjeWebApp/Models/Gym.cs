using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Models
{
    public class Gym
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Areas { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string workingTimeSunday { get; set; }
        public string workingTimeWeek { get; set; }
    }
}