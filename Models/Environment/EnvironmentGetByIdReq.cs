using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.PagingRequest;

namespace Models.Environment
{
    public class EnvironmentGetByIdReq : Paging
    {
        public Guid Id { get; set; }
    }
}
