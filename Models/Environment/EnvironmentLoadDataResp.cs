using VersionsCRUD.Common;

namespace VersionsCRUD.Environment
{
    public class EnvironmentLoadDataResp : CommonResp
    {
        public List<ProjectResp>? projects { get; set; } = new List<ProjectResp>();
    }

    public class ProjectResp
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
    }
}
