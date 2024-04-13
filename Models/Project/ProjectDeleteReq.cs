using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Project
{
    public class ProjectDeleteReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id {  get; set; }
    }
}
