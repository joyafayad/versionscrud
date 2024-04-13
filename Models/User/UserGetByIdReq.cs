using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.User
{
    public class UserGetByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
    }
}
