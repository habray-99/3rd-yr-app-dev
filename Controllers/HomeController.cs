using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IdentityDBContext _context;

    public HomeController(ILogger<HomeController> logger, IdentityDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(int? month = null, int? year = null)
    {
        var blogCount = _context.Blogs.Count();
        var upvoteCount = _context.Reactions.Count(r => r.ReactionTypeID == 1);
        var downvoteCount = _context.Reactions.Count(r => r.ReactionTypeID == 2);
        var commentCount = _context.Comments.Count();

        var topBlogPosts = (month.HasValue || year.HasValue)
            ? _context.Blogs.OrderByDescending(b => 2 * b.Reactions.Count(r => r.ReactionTypeID == 1) - b.Reactions.Count(r => r.ReactionTypeID == 2) + b.Comments.Count)
                .Where(b => (!month.HasValue || b.CreatedDate.Month == month) &&
                            (!year.HasValue || b.CreatedDate.Year == year))
                .Take(10)
                .ToList()
            : _context.Blogs.OrderByDescending(b => 2 * b.Reactions.Count(r => r.ReactionTypeID == 1) - b.Reactions.Count(r => r.ReactionTypeID == 2) + b.Comments.Count)
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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}