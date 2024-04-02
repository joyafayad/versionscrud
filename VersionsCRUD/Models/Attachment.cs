﻿using System;
using System.Collections.Generic;

namespace VersionsCRUD.Models
{
    public partial class Attachment
    {
        public Guid Id { get; set; }
        public Guid? Documentid { get; set; }
        public string Filename { get; set; } = null!;
        public long? Filesize { get; set; }
        public string? Filetype { get; set; }
        public DateOnly? Date { get; set; }
        public bool? Isactive { get; set; }

        public virtual Document? Document { get; set; }
    }
}
