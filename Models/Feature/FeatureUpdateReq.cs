using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Feature
{
    public class FeaturesUpdateReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? release { get; set; }
    }
}