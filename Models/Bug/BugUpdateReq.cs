using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bug
{
    public class BugUpdateReq
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public  string Fixed {  get; set; }
    }
}
