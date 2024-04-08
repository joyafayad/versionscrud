using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Project
    {
        public Project()
        {
            Bugs = new HashSet<Bug>();
            Features = new HashSet<Feature>();
            Versions = new HashSet<Version>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }

        public virtual ICollection<Bug> Bugs { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual ICollection<Version> Versions { get; set; }
    }
}
