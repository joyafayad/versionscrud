using VersionsCRUD.Common;

namespace VersionsCRUD.Version
{
    public class VersionGetByIdResp : CommonResp
    {
        public VersionGet? version { get; set; }
    }
}