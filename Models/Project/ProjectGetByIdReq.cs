using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Project
{
    public class ProjectGetByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
       
    }
}
