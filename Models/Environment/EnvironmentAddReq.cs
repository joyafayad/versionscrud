using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Environment
{
    public class EnvironmentAddReq
    {
        [Required]
        public string? name { get; set; }
        public string? description { get; set; }
        public Guid? projectid { get; set; }
    }
}
