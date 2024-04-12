namespace VersionsCRUD.Models
{
    public partial class Metadatum
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Guid? Documentid { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Document? Document { get; set; }
    }
}
