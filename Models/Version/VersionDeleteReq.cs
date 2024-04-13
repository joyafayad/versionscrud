using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Version
{
    public class VersionDeleteReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
    }
}
