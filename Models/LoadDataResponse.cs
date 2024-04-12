namespace Models
{
    public class LoadDataResponse
    {
        public List<ProjectLoadDataResponse> Projects { get; set; }
        public List<FeatureLoadDataResponse> Features { get; set; }
        public List<BugLoadDataResponse> Bugs { get; set; }
    }
    public class ProjectLoadDataResponse
    {

        public Guid Id { get; set; }
        public string? name { get; set; }
    }

    public class FeatureLoadDataResponse
    {

        public Guid Id { get; set; }
        public string? name { get; set; }
    }
    public class BugLoadDataResponse
    {

        public Guid Id { get; set; }
        public string? description { get; set; }
    }

}
