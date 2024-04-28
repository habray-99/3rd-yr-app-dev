using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Models
{
    public class Reaction
    {
        [Key]
        public int? ReactionID { get; set; }
        [Required]
        public int? BlogID { get; set; }
        [Required]
        public string? UserID { get; set; }
        [Required]
        public int? ReactionTypeID { get; set; }
        [ForeignKey("BlogID")]
        public virtual Blog? Blog { get; set; }
        public virtual CustomUser? User { get; set; }
        [ForeignKey("ReactionTypeID")]
        public virtual ReactionType? ReactionType { get; set; }
    }
}
