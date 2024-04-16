using VersionsCRUD.Common;
using VersionsCRUD.Environment;

namespace VersionsCRUD.Version
{
    public class VersionLoadDataResp : CommonResp
    {
        public List<ProjectResp>? projects { get; set; } = new List<ProjectResp>();
        public List<EnvironmentResp>? environments { get; set; } = new List<EnvironmentResp>();
        public List<FeatureResp>? features { get; set; } = new List<FeatureResp>();
        public List<BugResp>? bugs { get; set; } = new List<BugResp>();
    }

    public class FeatureResp
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
    }
    public class EnvironmentResp
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public Guid? projectId { get; set; } //make sure to return this w ma77e l comment hyda cava
    }
    public class BugResp
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
    }
}