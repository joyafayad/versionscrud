using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.PagingRequest;

namespace Models.Versions
{
    public class ProjectGetByIdReq : Paging
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
       
    }
}
