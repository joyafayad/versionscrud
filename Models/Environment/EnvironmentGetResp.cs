using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Environment
{
    public class EnvironmentGetResp
    {
        public Guid Id;

        public string? projectname { get; set; }
        public string? description { get; set; }
    }
}
