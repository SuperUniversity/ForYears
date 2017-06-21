using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FourYears.Areas.Area.Models
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public int ClassID { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string Host { get; set; }
    }
}