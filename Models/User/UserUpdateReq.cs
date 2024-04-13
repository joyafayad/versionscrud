using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.User
{
    public class UserUpdateReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
        public string? username { get; set; }
        [Email]
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
