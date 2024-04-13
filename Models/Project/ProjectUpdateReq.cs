using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Project
{
    public class ProjectUpdateReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
        public string? name { get; set; }
    }
}
