namespace VersionsCRUD.Models
{
    public partial class Comment
    {
        public Guid Id { get; set; }
        public Guid? Documentid { get; set; }
        public Guid? Userid { get; set; }
        public string? Comment1 { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Document? Document { get; set; }
        public virtual User? User { get; set; }
    }
}
