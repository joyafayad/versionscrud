using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Version
{
    public class VersionAddReq
    {
        [Required(ErrorMessage = "Project Id is required")]
        public Guid projectId { get; set; }
        public string? versionNumber { get; set; }
    }
}
