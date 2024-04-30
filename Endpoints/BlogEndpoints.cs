using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Models;
namespace WebApplication6.Endpoints;
[Route("apis/ve1/{controller}")]
[ApiController]
public static class BlogEndpoints
{
    public static void MapBlogEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Blog").WithTags(nameof(Blog));

        group.MapGet("/", async (IdentityDBContext db) =>
        {
            return await db.Blogs.ToListAsync();
        })
        .WithName("GetAllBlogs")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Blog>, NotFound>> (int? blogid, IdentityDBContext db) =>
        {
            return await db.Blogs.AsNoTracking()
                .FirstOrDefaultAsync(model => model.BlogID == blogid)
                is Blog model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBlogById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int? blogid, Blog blog, IdentityDBContext db) =>
        {
            var affected = await db.Blogs
                .Where(model => model.BlogID == blogid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.BlogID, blog.BlogID)
                    .SetProperty(m => m.Title, blog.Title)
                    .SetProperty(m => m.Body, blog.Body)
                    .SetProperty(m => m.CreatedDate, blog.CreatedDate)
                    .SetProperty(m => m.UserID, blog.UserID)
                    .SetProperty(m => m.ImagePath, blog.ImagePath)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        group.MapPost("/", async (Blog blog, IdentityDBContext db) =>
        {
            db.Blogs.Add(blog);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Blog/{blog.BlogID}", blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int? blogid, IdentityDBContext db) =>
        {
            var affected = await db.Blogs
                .Where(model => model.BlogID == blogid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}
