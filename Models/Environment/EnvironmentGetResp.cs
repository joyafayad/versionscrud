using Models.Common;
using VersionsCRUD.Common;

namespace VersionsCRUD.Environment
{
    public class EnvironmentGetResp : CommonGetResp
    {
        public List<EnvironmentGet>? environments { get; set; } = new List<EnvironmentGet>();
        public int totalCount { get; set; }
    }

    public class EnvironmentGet
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public Guid? projectid { get; set; }
        public string? projectName { get; set; }
    }
}
