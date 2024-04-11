using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Permission
    {
        public Guid Id { get; set; }
        public Guid? Documentid { get; set; }
        public Guid? Userid { get; set; }
        public bool? Edit { get; set; }
        public bool? View { get; set; }
        public DateTime? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Document? Document { get; set; }
        public virtual User? User { get; set; }
    }
}
