﻿using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Version
    {
        public Guid Id { get; set; }
        public Guid? Projectid { get; set; }
        public string? Versionnumber { get; set; }
    }
}
