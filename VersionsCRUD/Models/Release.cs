using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Release
    {
        public Guid Id { get; set; }
        public Guid? Versionid { get; set; }
        public DateTime? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Version? Version { get; set; }
    }
}
