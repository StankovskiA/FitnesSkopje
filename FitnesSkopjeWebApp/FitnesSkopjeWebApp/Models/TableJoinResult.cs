using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Models
{
    public class TableJoinResult
    {
        public Gym gym { get; set; }
        public Review review { get; set; }
    }
}