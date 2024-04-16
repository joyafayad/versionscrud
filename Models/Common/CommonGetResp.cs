using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VersionsCRUD.Common;

namespace Models.Common
{
    public class CommonGetResp : CommonResp
    {
        public int totalCount { get; set; }
    }
}
