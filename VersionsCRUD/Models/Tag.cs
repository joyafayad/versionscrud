namespace VersionsCRUD.Models
{
    public partial class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Taggeduser { get; set; }
        public Guid? Iduser { get; set; }
        public DateTime? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }

        public virtual User? IduserNavigation { get; set; }
    }
}
