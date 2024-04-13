using VersionsCRUD.Common;

namespace VersionsCRUD.User
{
    public class UserGetByIdResp : CommonResp
    {
        public UserGet? user { get; set; }
    }
}