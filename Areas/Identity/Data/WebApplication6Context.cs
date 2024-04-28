using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Areas.Identity.Data;


public class WebApplication6Context : IdentityDbContext<CustomUser>
{
    public WebApplication6Context(DbContextOptions<WebApplication6Context> options)
        : base(options)
    {
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentReaction> CommentReactions { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<ReactionType> ReactionTypes { get; set; }
    public DbSet<CustomUser> User { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CustomUser>().Property(u => u.Id);
        // Configure relationships and any additional configurations
        modelBuilder.Entity<User>()
            .HasOne(u => u.CustomUser)
            .WithMany() // Adjust this if CustomUser has a collection navigation property for User
            .HasForeignKey(u => u.UserID)
            .HasPrincipalKey(c => c.Id);
        modelBuilder.Entity<Blog>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Blog)
            .WithMany()
            .HasForeignKey(r => r.BlogID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.ReactionType)
            .WithMany()
            .HasForeignKey(r => r.ReactionTypeID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Blog)
            .WithMany()
            .HasForeignKey(c => c.BlogID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(cr => cr.Comment)
            .WithMany()
            .HasForeignKey(cr => cr.CommentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(cr => cr.ReactionType)
            .WithMany()
            .HasForeignKey(cr => cr.ReactionTypeID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(cr => cr.User)
            .WithMany()
            .HasForeignKey(cr => cr.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BlogMetric>()
            .HasOne(bm => bm.Blog)
            .WithMany()
            .HasForeignKey(bm => bm.BlogID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserMetric>()
            .HasOne(um => um.User)
            .WithMany()
            .HasForeignKey(um => um.UserID)
            .OnDelete(DeleteBehavior.Restrict);
    }

    //public DbSet<User> User { get; set; } = default!;
}
