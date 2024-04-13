using VersionsCRUD.Common;

namespace VersionsCRUD.Version
{
    public class VersionGetResp : CommonResp
    {
        public List<VersionGet>? versions { get; set; } = new List<VersionGet>();
    }

    public class VersionGet
    {
        public Guid? id { get; set; }
        public Guid? projectId { get; set; }
        public string? versionNumber { get; set; }
    }
}
