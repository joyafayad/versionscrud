using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.PagingRequest;

namespace Models.Feature
{
    public class FeatureGetByIdReq : Paging
    {
        public Guid Id { get; set; }
       
    }
}
