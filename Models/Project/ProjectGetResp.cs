using VersionsCRUD.Common;

namespace VersionsCRUD.Project
{
    public class ProjectGetResp : CommonResp
    {
        public List<ProjectGet>? projects { get; set; } = new List<ProjectGet>();
    }

    public class ProjectGet
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
    }
}
