﻿namespace test.Models
{
    public class VersionUpdateReq
    {
       public Guid Id { get; set; }
        public int? projectId { get; set; }
        public string? versionNumber { get; set; }
    }
}
