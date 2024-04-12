namespace Models.Versions
{
    public class VersionGetByIdResp
    {
        public Guid Id { get; set; }
        public Guid? ProjectID { get; set; }

        public string VersionNumber { get; set; }
    }
}
