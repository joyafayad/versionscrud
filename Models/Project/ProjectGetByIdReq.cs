using System.ComponentModel.DataAnnotations;

namespace Models.Versions
{
    public class ProjectGetByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
       
    }
}
