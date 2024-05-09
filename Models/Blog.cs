using System.ComponentModel.DataAnnotations;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Models
{
    public class Blog
    {
        [Key]
        public int? BlogID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Body { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public string? UserID { get; set; }
        public string? ImagePath { get; set; }
        //[ForeignKey("UserID")]
        public virtual CustomUser? User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}
