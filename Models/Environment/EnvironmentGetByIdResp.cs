using VersionsCRUD.Common;

namespace VersionsCRUD.Environment
{
    public class EnvironmentGetByIdResp : CommonResp
    {
        public EnvironmentGet? environment { get; set; }
    }
}
