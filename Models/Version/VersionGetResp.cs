using Models.Common;
using VersionsCRUD.Common;

namespace VersionsCRUD.Version
{
    public class VersionGetResp : CommonGetResp
    {
        public List<VersionGet>? versions { get; set; } = new List<VersionGet>();
        public int totalCount { get; set; }
    }

    public class VersionGet
    {
        public Guid? id { get; set; }
        public Guid? projectId { get; set; }
        public string? projectName { get; set; }
        public string? versionNumber { get; set; }
        public Guid? featureId { get; set; }
        public string? featureName { get; set; }
        public Guid? bugId { get; set; }
        public string? bugName { get; set; }
        public Guid? environmentId { get; set; }
        public string? environmentName { get; set; }
        public Boolean? isMajor { get; set; }
        public Boolean? isMinor { get; set; }
        public Boolean? isPatch { get; set; }
        public string link { get; set; }
    }
}
