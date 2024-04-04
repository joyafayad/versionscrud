using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Versions
{
    public class ProjectByIdResp
    {
        public Guid ID { get; set; }

        public Guid? ProjectID { get; set; }

        public string VersionNumber { get; set; }
        public int code { get; set; }
    }
}
