using VersionsCRUD.Project;
using VersionsCRUD.Common;

namespace VersionsCRUD.Feature
{
    public class FeatureLoadDataResp : CommonResp
    {
        public List<ProjectResp>? projects { get; set; } = new List<ProjectResp>();
    }
}
