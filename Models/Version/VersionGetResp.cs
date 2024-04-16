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
        public string? versionNumber { get; set; }
    }
}
