using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Areas.Identity.Data;


public class IdentityDBContext : IdentityDbContext<CustomUser>
{
    public IdentityDBContext(DbContextOptions<IdentityDBContext> options)
        : base(options)
    {
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentReaction> CommentReactions { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<ReactionType> ReactionTypes { get; set; }
    //public DbSet<CustomUser> User { get; set; }
    public DbSet<CustomUser> Users { get; set; } = default;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CustomUser>().Property(u => u.Id);

        modelBuilder.Entity<CustomUser>()
            .HasMany(u => u.Blogs) // Assuming CustomUser has a collection of Blogs
            .WithOne(b => b.User) // Assuming Blog has a User property
            .HasForeignKey(b => b.UserID);
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
        modelBuilder.Entity<Blog>()
            .HasMany(b => b.Comments) 
            .WithOne(c => c.Blog) 
            .HasForeignKey(c => c.BlogID)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.Blog)
            .WithMany()
            .HasForeignKey(r => r.BlogID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.ReactionType)
            .WithMany(rt => rt.Reactions)
            .HasForeignKey(r => r.ReactionTypeID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reaction>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Blog)
            .WithMany(b => b.Comments)
            .HasForeignKey(c => c.BlogID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(cr => cr.Comment)
            .WithMany(c => c.CommentReactions)
            .HasForeignKey(cr => cr.CommentID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CommentReaction>()
            .HasOne(cr => cr.ReactionType)
            .WithMany(rt => rt.CommentReactions)
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

public DbSet<WebApplication6.Models.User> User { get; set; } = default!;

public DbSet<WebApplication6.Models.BlogMetric> BlogMetric { get; set; } = default!;

public DbSet<WebApplication6.Models.UserMetric> UserMetric { get; set; } = default!;

    //public DbSet<User> User { get; set; } = default!;
}
