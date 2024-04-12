namespace Models.Environment
{
    public class EnvironmentUpdateReq
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid projectid { get; set; }
    }
}
