using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bug
{
    public class BugAddReq
    {
        public string Description { get; set; }
        public int Status { get; set; }
        public string Release { get; set; }

    }
}
