using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Models;

namespace WebApplication6;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("BisleriumWebBlogContextConnection") ??
                               throw new InvalidOperationException(
                                   "Connection string 'BisleriumWebBlogContextConnection' not found.");

        builder.Services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDBContext>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        builder.Services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = config["GoogleKeys:ClientId"] ?? throw new InvalidOperationException();
                googleOptions.ClientSecret = config["GoogleKeys:ClientSecret"] ?? throw new InvalidOperationException();
            })
            .AddFacebook(opt =>
            {
                opt.ClientId = config["FacebookKeys:ClientId"] ?? throw new InvalidOperationException();
                opt.ClientSecret = config["FacebookKeys:ClientSecret"] ?? throw new InvalidOperationException();
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        ;

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        //app.Run();
        app.UseSwagger();
        app.UseSwaggerUI();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "Blogger" };
            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    try
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception
                        Console.WriteLine($"Failed to create role {role}: {ex.Message}");
                    }
        }
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();

            string email = "admin@admin.com";
            string password = "Test@1234";
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new CustomUser();
                user.UserName = email;
                user.Email = email;
                user.EmailConfirmed = true;
                user.Role = "Admin";
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        // Log or handle each error
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
        //using (var scope = app.Services.CreateScope())
        //{
        //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();

        //    const string email = "iamadmin@admin.com";
        //    const string password = "Test1234";
        //    if (await userManager.FindByEmailAsync(email) == null)
        //    {
        //        var user = new CustomUser
        //        {
        //            UserName = email,
        //            Email = email,
        //            EmailConfirmed = true
        //        };
        //        try
        //        {
        //            var result = await userManager.CreateAsync(user, password);
        //            if (result.Succeeded)
        //                await userManager.AddToRoleAsync(user, "Admin");
        //            else
        //                foreach (var error in result.Errors)
        //                    // Log or handle each error
        //                    Console.WriteLine(error.Description);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Log or handle the exception
        //            Console.WriteLine($"Failed to create user {email}: {ex.Message}");
        //        }
        //    }
        //}
        //using (var scope = app.Services.CreateScope())
        //{
        //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();

        //     string email = "admin@admin.com";
        //     string password = "Test@1234";
        //     if (await userManager.FindByEmailAsync(email) == null)
        //     {
        //         var user = new CustomUser();
        //         user.UserName = email;
        //         user.Email = email;
        //         user.EmailConfirmed = true;
        //         var result = await userManager.CreateAsync(user, password);
        //         if (result.Succeeded)
        //         {
        //             await userManager.AddToRoleAsync(user, "Admin");
        //         }
        //         else
        //         {
        //             foreach (var error in result.Errors)
        //             {
        //                 // Log or handle each error
        //                 Console.WriteLine(error.Description);
        //             }
        //         }
        //     }
        // }

        // Start the application
        await app.RunAsync();
    }
}