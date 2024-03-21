using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Project
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
