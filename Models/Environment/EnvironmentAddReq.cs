using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Environment
{
    public class EnvironmentAddReq
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid  projectid { get; set; }
    }
}
