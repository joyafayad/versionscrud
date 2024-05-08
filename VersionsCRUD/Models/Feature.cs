using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Feature
    {
        public Feature()
        {
            Versions = new HashSet<Version>();
        }

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
        public virtual ICollection<Version> Versions { get; set; }
    }
}
