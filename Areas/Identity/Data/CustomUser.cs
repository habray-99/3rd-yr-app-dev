using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebApplication6.Models;

namespace WebApplication6.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CustomUser class
public class CustomUser : IdentityUser
{
    [Required] [MaxLength(10)] 
    public string? Role { get; set; } = "Blogger";
    [ProtectedPersonalData]
    [MaxLength(30)]
    public string? Address { get; set; }
    public string? ProfilePicture { get; set; }
    public ICollection<Blog>? Blogs { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Reaction>? Reactions { get; set; }
    public ICollection<Notification>? Notifications { get; set; }
    public ICollection<CommentReaction>? CommentReactions { get; set; }
}