using Models.Common;
using VersionsCRUD.Common;

namespace VersionsCRUD.Project
{
    public class ProjectGetResp : CommonGetResp
    {
        public List<ProjectGet>? projects { get; set; } = new List<ProjectGet>();
        public int totalCount { get; set; }
    }

    public class ProjectGet
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
       
    }
}
