using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class UsersController : Controller
    {
        private readonly IdentityDBContext _context;

        public UsersController(IdentityDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customUser = await _context.Users
               .FirstOrDefaultAsync(m => m.Id == id);
            if (customUser == null)
            {
                return NotFound();
            }

            return View(customUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Role,Address,ProfilePicture,Blogs,Comments,Reactions,Notifications,CommentReactions,BlogMetrics,UserMetrics")] CustomUser customUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customUser);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customUser = await _context.Users.FindAsync(id);
            if (customUser == null)
            {
                return NotFound();
            }
            return View(customUser);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount,Role,Address,ProfilePicture,Blogs,Comments,Reactions,Notifications,CommentReactions,BlogMetrics,UserMetrics")] CustomUser customUser)
        {
            if (id != customUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomUserExists(customUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customUser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customUser = await _context.Users
               .FirstOrDefaultAsync(m => m.Id == id);
            if (customUser == null)
            {
                return NotFound();
            }

            return View(customUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customUser = await _context.Users.FindAsync(id);
            if (customUser != null)
            {
                _context.Users.Remove(customUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
