using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Feature
{
    public class FeatureGetByIdReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
    }
}
