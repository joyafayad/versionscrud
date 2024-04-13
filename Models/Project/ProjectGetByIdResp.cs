using VersionsCRUD.Common;

namespace VersionsCRUD.Project
{
    public class ProjectGetByIdResp : CommonResp
    {
        public ProjectGet? project { get; set; }
    }
}
