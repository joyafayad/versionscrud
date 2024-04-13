using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Bug
{
    public class BugUpdateReq
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid id { get; set; }
        public string? description { get; set; }
        public int status { get; set; }
        //public string? release { get; set; }
        //public string Fixed {  get; set; }
    }
}