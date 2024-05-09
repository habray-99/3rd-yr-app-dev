using WebApplication6.Areas.Identity.Data;
using WebApplication6.Models;

public class DashboardViewModel
{
    public int? BlogCount { get; set; }
    public int? UpvoteCount {get;set;}
    public int? DownvoteCount {get;set;}
    public int? CommentCount {get;set;}
    public List<Blog> TopBlogPosts {get;set;}
    public List<CustomUser> TopBloggers {get;set;}
    public int? SelectedMonth { get; set; }
    public int? SelectedYear { get; set; }
}