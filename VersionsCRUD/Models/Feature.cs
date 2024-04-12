namespace VersionsCRUD.Models
{
    public partial class Feature
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly? Release { get; set; }
        public DateTime? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }
        public Guid? ProjectId { get; set; }

        public virtual Project? Project { get; set; }
    }
}
