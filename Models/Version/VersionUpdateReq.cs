using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Version
{
    public class VersionUpdateReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
        [Required(ErrorMessage = "Project Id is required")]
        public Guid projectId { get; set; }
        public string? versionNumber { get; set; }
    }
}
