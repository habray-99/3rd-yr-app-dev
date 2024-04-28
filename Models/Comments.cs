using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Models
{
    public class Comment
    {
        [Key]
        public int? CommentID { get; set; }
        [Required]
        public int? BlogID { get; set; }
        [Required]
        public string? UserID { get; set; }
        [Required]
        public string? CommentText { get; set; }
        public DateTime? CreatedDate { get; set; }
        [ForeignKey("BlogID")]
        public virtual Blog? Blog { get; set; }
        [ForeignKey("UserID")]
        public virtual CustomUser? User { get; set; }

        public ICollection<CommentReaction>? CommentReactions { get; set; }
    }
}
