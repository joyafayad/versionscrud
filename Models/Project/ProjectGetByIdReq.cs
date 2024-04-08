using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Versions
{
    public class ProjectGetByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
    }
}
