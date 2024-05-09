using Microsoft.AspNetCore.Mvc;
using WebApplication6.Areas.Identity.Data;

namespace WebApplication6.Controllers;

// DashboardController.cs
public class DashboardController : Controller
{
    private readonly IdentityDBContext _context;

    public DashboardController(IdentityDBContext context)
    {
        _context = context;
    }

    //public async Task<IActionResult> Index(int? month = null)
    //{
    //    var blogCount = _context.Blogs.Count();
    //    var commentCount = _context.Comments.Count();
    //    var upvoteCount = _context.Reactions.Count(r => r.ReactionTypeID == 1);
    //    var downvoteCount = _context.Reactions.Count(r => r.ReactionTypeID == 2);

    //    var topBlogPosts = month.HasValue
    //        ? _context.Blogs.OrderByDescending(b => b.Comments.Count + b.Reactions.Count(r => r.ReactionTypeID == 1))
    //            .Where(b => b.CreatedDate.Month == month)
    //            .Take(10)
    //            .ToList()
    //        : _context.Blogs.OrderByDescending(b => b.Comments.Count + b.Reactions.Count(r => r.ReactionTypeID == 1))
    //            .Take(10)
    //            .ToList();

    //    var topBloggers = month.HasValue
    //        ? _context.Users.OrderByDescending(u => u.Blogs.Count + u.Comments.Count + u.Reactions.Count(r => r.ReactionTypeID == 1))
    //            .Where(u => u.Blogs.Any(b => b.CreatedDate.Month == month))
    //            .Take(10)
    //            .ToList()
    //        : _context.Users.OrderByDescending(u => u.Blogs.Count + u.Comments.Count + u.Reactions.Count(r => r.ReactionTypeID == 1))
    //            .Take(10)
    //            .ToList();

    //    var viewModel = new DashboardViewModel
    //    {
    //        BlogCount = blogCount,
    //        UpvoteCount = upvoteCount,
    //        DownvoteCount = downvoteCount,
    //        CommentCount = commentCount,
    //        TopBlogPosts = topBlogPosts,
    //        TopBloggers = topBloggers
    //    };

    //    return View(viewModel);
    //}
    public async Task<IActionResult> Index(int? month = null, int? year = null)
    {
        var blogCount = _context.Blogs.Count();
        var upvoteCount = _context.Reactions.Count(r => r.ReactionTypeID == 1);
        var downvoteCount = _context.Reactions.Count(r => r.ReactionTypeID == 2);
        var commentCount = _context.Comments.Count();

        var topBlogPosts = (month.HasValue || year.HasValue)
            ? _context.Blogs.OrderByDescending(b => b.Comments.Count + b.Reactions.Count(r => r.ReactionTypeID == 1))
                .Where(b => (month.HasValue ? b.CreatedDate.Month == month : true) && (year.HasValue ? b.CreatedDate.Year == year : true))
                .Take(10)
                .ToList()
            : _context.Blogs.OrderByDescending(b => b.Comments.Count + b.Reactions.Count(r => r.ReactionTypeID == 1))
                .Take(10)
                .ToList();

        var topBloggers = (month.HasValue || year.HasValue)
            ? _context.Users.OrderByDescending(u => u.Blogs.Count + u.Comments.Count + u.Reactions.Count(r => r.ReactionTypeID == 1))
                .Where(u => u.Blogs.Any(b => (month.HasValue ? b.CreatedDate.Month == month : true) && (year.HasValue ? b.CreatedDate.Year == year : true)))
                .Take(10)
                .ToList()
            : _context.Users.OrderByDescending(u => u.Blogs.Count + u.Comments.Count + u.Reactions.Count(r => r.ReactionTypeID == 1))
                .Take(10)
                .ToList();

        var viewModel = new DashboardViewModel
        {
            BlogCount = blogCount,
            UpvoteCount = upvoteCount,
            DownvoteCount = downvoteCount,
            CommentCount = commentCount,
            TopBlogPosts = topBlogPosts,
            TopBloggers = topBloggers,
            SelectedMonth = month,
            SelectedYear = year
        };

        return View(viewModel);
    }
}