using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Environment
{
    public class EnvironmentUpdateReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Project Id is required")]
        public Guid projectid { get; set; }
    }
}
