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

        [Display(Name="Теретана")]
        public string Name { get; set; }

        [Display(Name = "Адреса")]
        public string Address { get; set; }

        [Display(Name = "Телефонски број")]
        public string Number { get; set; }

        [Display(Name = "Дејности")]
        public string Areas { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [Display(Name = "Работно време недела")]
        public string workingTimeSunday { get; set; }

        [Display(Name = "Работно време пон-сабота")]
        public string workingTimeWeek { get; set; }
    }
}