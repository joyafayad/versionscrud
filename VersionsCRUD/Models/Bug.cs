﻿using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Bug
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateOnly? Reported { get; set; }
        public DateOnly? Fixed { get; set; }
        public int Status { get; set; }
        public string? Contributor { get; set; }
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }
        public Guid? Createdby { get; set; }
        public Guid? Updatedby { get; set; }
        public DateTime? Updated { get; set; }
        public string release {  get; set; }
    }
}
