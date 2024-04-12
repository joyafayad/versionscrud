using VersionsCRUD.Common;
using VersionsCRUD.Project;

namespace Models.Versions
{
    public class ProjectGetByIdResp : CommonResp
    {
        public ProjectGet? project { get; set; }
    }
}
