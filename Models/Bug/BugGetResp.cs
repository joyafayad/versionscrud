using Models.Common;
using VersionsCRUD.Common;

namespace VersionsCRUD.Bug
{
    public class BugGetResp : CommonGetResp
    {
        public List<BugGet>? bugs { get; set; } = new List<BugGet>();
        public int totalCount { get; set; }
    }

    public class BugGet
    {
        public Guid? id { get; set; }
        public string? description { get; set; }
        public int status { get; set; }
        //public string? release { get; set; }
        //public string? fixed { get; set; }
        public string? reported { get; set; }
    }
}