namespace test.Models
{
    public class VersionUpdateReq
    {
       public int Id { get; set; }
        public int projectId { get; set; }
        public string? versionNumber { get; set; }
    }
}
