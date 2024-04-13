using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Bug
{
    public class BugGetByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
    }
}