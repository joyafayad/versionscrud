using VersionsCRUD.Common;

namespace VersionsCRUD.Environment
{
    public class EnvironmentGetResp : CommonResp
    {
        public List<EnvironmentGet>? environments { get; set; } = new List<EnvironmentGet>();
        
    }

    public class EnvironmentGet
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public Guid? projectid { get; set; }
    }
}
