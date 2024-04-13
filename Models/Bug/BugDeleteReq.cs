using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Bug
{
    public class BugDeleteReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
    }
}
