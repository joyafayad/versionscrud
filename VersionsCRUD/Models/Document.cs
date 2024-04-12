namespace VersionsCRUD.Models
{
    public partial class Document
    {
        public Document()
        {
            AccessLogs = new HashSet<AccessLog>();
            Attachments = new HashSet<Attachment>();
            Comments = new HashSet<Comment>();
            Metadata = new HashSet<Metadatum>();
            Permissions = new HashSet<Permission>();
            Subscriptions = new HashSet<Subscription>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Versionid { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Version? Version { get; set; }
        public virtual ICollection<AccessLog> AccessLogs { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Metadatum> Metadata { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
