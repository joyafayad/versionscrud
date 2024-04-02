using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Taggeduser { get; set; }
        public Guid? Iduser { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }

        public virtual User? IduserNavigation { get; set; }
    }
}
