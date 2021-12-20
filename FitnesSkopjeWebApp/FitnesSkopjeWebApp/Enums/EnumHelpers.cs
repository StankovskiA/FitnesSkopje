using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnesSkopjeWebApp.Enums
{
    public static class EnumHelpers
    {
        public enum EnumUserRoles
        {           
            User = 1,
            Admin = 2,
            Unspecified=-1
        }
    }
}