using VersionsCRUD.Project;
using VersionsCRUD.Bug;
using VersionsCRUD.Common;
using VersionsCRUD.Environment;
using VersionsCRUD.Feature;

namespace VersionsCRUD.Version
{
    public class VersionLoadDataResp : CommonResp
    {
        public List<ProjectResp>? projects { get; set; } = new List<ProjectResp>();
        public List<EnvironmentResp>? environments { get; set; } = new List<EnvironmentResp>();
        public List<FeatureResp>? features { get; set; } = new List<FeatureResp>();
        public List<BugResp>? bugs { get; set; } = new List<BugResp>();
    }

}