using System;
using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Version
{
    public class VersionAddReq
    {
        [Required(ErrorMessage = "Project Id is required")]
        public Guid projectId { get; set; }
        public string? versionNumber { get; set; }
        public Guid featureId { get; set; }
        public Guid bugId { get; set; }
        public Boolean isMajor {  get; set; }
        public Boolean isMinor { get; set; }
        public Boolean isPatch { get; set; }
        public string link {  get; set; }
    }
}
