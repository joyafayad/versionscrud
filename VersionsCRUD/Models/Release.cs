using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Release
    {
        public Guid Id { get; set; }
        public Guid? Versionid { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }

        public virtual Version? Version { get; set; }
    }
}
