using VersionsCRUD.Common;

namespace VersionsCRUD.Bug
{
    public class BugGetByIdResp : CommonResp
    {
        public BugGet? bug { get; set; }
    }
}