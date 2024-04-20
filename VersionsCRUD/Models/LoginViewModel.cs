using System.ComponentModel.DataAnnotations;

namespace VersionsCRUD.Models
{
	public class LoginViewModel
	{
		[Required]
		public string username { get; set; }
        [Required]
        public string password { get; set; }
	}
}
