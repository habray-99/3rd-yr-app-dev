using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Models
{
    public class CommentReaction
    {
        [Key]
        public int? CommentReactionID { get; set; }
        [Required]
        public int? CommentID { get; set; }
        [Required]
        public string? UserID { get; set; }
        [Required]
        public int? ReactionTypeID { get; set; }
        [ForeignKey("CommentID")]
        public virtual Comment? Comment { get; set; }
        [ForeignKey("UserID")]
        public virtual CustomUser? User { get; set; }
        [ForeignKey("ReactionTypeID")]
        public virtual ReactionType? ReactionType { get; set; }
    }
}
