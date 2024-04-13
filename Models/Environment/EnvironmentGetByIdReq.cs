using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Environment
{
    public class EnvironmentGetByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
    }
}
