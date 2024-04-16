using VersionsCRUD.Common;
using VersionsCRUD.Project;

namespace VersionsCRUD.Environment
{
    public class EnvironmentLoadDataResp : CommonResp
    {
        public List<ProjectResp>? projects { get; set; } = new List<ProjectResp>();
    }
}
