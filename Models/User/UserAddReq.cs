using DataAnnotationsExtensions;

namespace VersionsCRUD.User
{
    public class UserAddReq
    {
        public string? username { get; set; }
        [Email]
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
