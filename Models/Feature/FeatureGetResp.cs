using VersionsCRUD.Common;

namespace VersionsCRUD.Feature
{
    public class FeatureGetResp : CommonResp
    {
        public List<FeatureGet>? features { get; set; } = new List<FeatureGet>();
    }

    public class FeatureGet
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? release { get; set; }
    }
}
