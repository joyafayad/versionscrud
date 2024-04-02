﻿using System;
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
        public DateOnly? Created { get; set; }
        public bool? Isactive { get; set; }

        public virtual Document? Document { get; set; }
        public virtual User? User { get; set; }
    }
}