using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Versions
{
    public class ProjectByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
    }
}
