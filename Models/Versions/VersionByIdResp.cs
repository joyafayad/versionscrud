using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Versions
{
    public class VersionByIdResp
    {
        public Guid Id { get; set; }
        public Guid? ProjectID { get; set; }

        public string VersionNumber { get; set; }
    }
}
