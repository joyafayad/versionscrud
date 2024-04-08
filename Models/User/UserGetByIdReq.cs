using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.PagingRequest;

namespace Models.User
{
    public class UserGetByIdReq : Paging
    {
        public Guid Id { get; set; }
        
    }
}
