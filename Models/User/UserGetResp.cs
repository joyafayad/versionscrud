using Models.Common;
using VersionsCRUD.Common;

namespace VersionsCRUD.User
{
    public class UserGetResp : CommonGetResp
    {
        public List<UserGet>? users { get; set; } = new List<UserGet>();
        public int totalCount { get; set; }
    }

        public class UserGet
    {
        public Guid? id { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
