using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class ReactionType
    {
        [Key]
        public int? ReactionTypeID { get; set; }
        [Required]
        public string? ReactionName { get; set; }

        public ICollection<Reaction>? Reactions { get; set; }
        public ICollection<CommentReaction>? CommentReactions { get; set; }
    }
}
