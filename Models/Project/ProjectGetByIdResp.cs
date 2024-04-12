using VersionsCRUD.Common;
using VersionsCRUD.Project;

namespace VersionsCRUD.Project
{
    public class ProjectGetByIdResp : CommonResp
    {
        public ProjectGet? project { get; set; }
    }
}
