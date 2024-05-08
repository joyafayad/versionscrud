using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Environment
    {
        public Environment()
        {
            Versions = new HashSet<Version>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Isactive { get; set; }
        public DateTime Created { get; set; }
        public Guid Createdby { get; set; }
        public DateTime Updated { get; set; }
        public Guid Updatedby { get; set; }
        public Guid? Projectid { get; set; }

        public virtual Project? Project { get; set; }
        public virtual ICollection<Version> Versions { get; set; }
    }
}
