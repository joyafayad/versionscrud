using VersionsCRUD.Common;

namespace VersionsCRUD.Feature
{
    public class FeatureGetByIdResp : CommonResp
    {
        public FeatureGet? feature { get; set; }
    }
}
