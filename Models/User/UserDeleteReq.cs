using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.User
{
    public class UserDeleteReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
    }
}
