using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bug
{
    public class BugGetResp
    {
        public string Description { get; set; }
        public int Status { get; set; }
        public Guid Id { get; set; }
        public string Fixed { get; set; }
       public  string Reported { get; set; }

       
    }
}
