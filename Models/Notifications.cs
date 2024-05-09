using System.ComponentModel.DataAnnotations;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Models
{
    public class Notification
    {
        [Key]
        public int? NotificationID { get; set; }
        [Required]
        public string? UserID { get; set; }
        [Required]
        public string? NotificationType { get; set; }
        [Required]
        public int? EntityID { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public bool IsSeen { get; set; } = false;
        public virtual CustomUser? User { get; set; }
    }
}
