using System.ComponentModel.DataAnnotations;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Models
{
    public class UserMetric
    {
        [Key]
        public int UserMetricID { get; set; }
        [Required]
        public string? UserID { get; set; }
        public int TotalBlogPosts { get; set; }
        public int TotalUpvotes { get; set; }
        public int TotalDownvotes { get; set; }
        public int TotalComments { get; set; }
        //[ForeignKey("UserID")]
        public virtual CustomUser? User { get; set; }
    }
}
