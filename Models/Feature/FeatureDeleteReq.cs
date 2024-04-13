using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Feature
{
    public class FeatureDeleteReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id {  get; set; }
    }
}
