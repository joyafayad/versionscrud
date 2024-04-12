namespace Models.Environment
{
    public class EnvironmentAddReq
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid  projectid { get; set; }
    }
}
