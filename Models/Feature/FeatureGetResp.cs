using Models.Common;
using VersionsCRUD.Common;

namespace VersionsCRUD.Feature
{
    public class FeatureGetResp : CommonGetResp
    {
        public List<FeatureGet>? features { get; set; } = new List<FeatureGet>();
        public int totalCount { get; set; }
    }

    public class FeatureGet
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? release { get; set; }
    }
}
