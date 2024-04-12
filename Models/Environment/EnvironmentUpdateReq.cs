namespace VersionsCRUD.Environment
{
    public class EnvironmentUpdateReq
    {
        public Guid? id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public Guid? projectid { get; set; }
    }
}
