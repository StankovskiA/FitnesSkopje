using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.ViewModels
{
    public class SearchApplicationModels
    {
        public int GymId { get; set; } = -1;

        public string SearchText { get; set; }

        public string ControllerName { get; set; }
        public string ControllerAction { get; set; }
    }
}