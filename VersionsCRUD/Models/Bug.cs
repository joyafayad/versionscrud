using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Bug
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateOnly? Reported { get; set; }
        public DateOnly? Fixed { get; set; }
        public string? Status { get; set; }
        public string? Contributor { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }
    }
}
