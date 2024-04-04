using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bug
{
    public class BugAddResp
    {
        public Guid Id { get; set; }
        public int code { get; set; }
        public string Message { get; set; }
    }
}
