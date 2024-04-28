using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Controllers;

namespace WebApplication6
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("WebApplication6ContextConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication6ContextConnection' not found.");

            builder.Services.AddDbContext<WebApplication6Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WebApplication6Context>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

                        builder.Services.AddEndpointsApiExplorer();

                        builder.Services.AddSwaggerGen();

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
};

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            //app.Run();
            app.UseSwagger();
            app.UseSwaggerUI();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Blogger", "Surfer" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
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
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();

                const string email = "iamadmin@admin.com";
                const string password = "Test1234";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new CustomUser()
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };
                    try
                    {
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
                    catch (Exception ex)
                    {
                        // Log or handle the exception
                        Console.WriteLine($"Failed to create user {email}: {ex.Message}");
                    }
                }
            }

            // Start the application
            await app.RunAsync();
        }
    }
}
