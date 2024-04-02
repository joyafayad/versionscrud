using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Project
    {
        public Project()
        {
            Versions = new HashSet<Version>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Isactive { get; set; }

        public virtual ICollection<Version> Versions { get; set; }
    }
}
