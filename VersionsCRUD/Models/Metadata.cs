using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Metadata
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Guid? Documentid { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }

        public virtual Document? Document { get; set; }
    }
}
