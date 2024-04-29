using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Models
{
    [Table("CustomUser")]
    public class User
    {
        public string UserID { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }

        public CustomUser CustomUser { get; set; }
    }
}
