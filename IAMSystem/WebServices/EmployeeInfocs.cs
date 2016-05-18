using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices
{
    public class EmployeeInfocs
    {
        public string code { get; set; }
        public string posts { get; set; }
        public string deptname { get; set; }
        public string name { get; set; }
        public string moblephone { get; set; }
        public DateTime? topostDate { get; set; }
        public DateTime? leavePostsDate { get; set; }
        public string userScope { get; set; }
        public bool? isSync { get; set; }
        public DateTime? syncdate { get; set; }
    }
}