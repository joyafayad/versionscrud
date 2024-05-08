namespace VersionsCRUD.Models
{
    public partial class Version
    {
        public Version()
        {
            Documents = new HashSet<Document>();
            Releases = new HashSet<Release>();
        }

        public Guid Id { get; set; }
        public Guid? Projectid { get; set; }
        public string? Versionnumber { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Isactive { get; set; }
        public string? Platform { get; set; }
        public string? Link { get; set; }
        public Guid? FeatureId { get; set; }
        public Guid? BugId { get; set; }
        public bool? IsMajor { get; set; }
        public bool? IsMinor { get; set; }
        public bool? IsPatch { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        //public Guid? EnvironmentId { get; set; }

        //public virtual Environment? Environment { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Release> Releases { get; set; }
    }
}
