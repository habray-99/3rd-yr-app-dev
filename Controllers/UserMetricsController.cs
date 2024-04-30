using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Areas.Identity.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class UserMetricsController : Controller
    {
        private readonly IdentityDBContext _context;

        public UserMetricsController(IdentityDBContext context)
        {
            _context = context;
        }

        // GET: UserMetrics
        public async Task<IActionResult> Index()
        {
            var identityDBContext = _context.UserMetric.Include(u => u.User);
            return View(await identityDBContext.ToListAsync());
        }

        // GET: UserMetrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMetric = await _context.UserMetric
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserMetricID == id);
            if (userMetric == null)
            {
                return NotFound();
            }

            return View(userMetric);
        }

        // GET: UserMetrics/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID");
            return View();
        }

        // POST: UserMetrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserMetricID,UserID,TotalBlogPosts,TotalUpvotes,TotalDownvotes,TotalComments")] UserMetric userMetric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMetric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", userMetric.UserID);
            return View(userMetric);
        }

        // GET: UserMetrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMetric = await _context.UserMetric.FindAsync(id);
            if (userMetric == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", userMetric.UserID);
            return View(userMetric);
        }

        // POST: UserMetrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserMetricID,UserID,TotalBlogPosts,TotalUpvotes,TotalDownvotes,TotalComments")] UserMetric userMetric)
        {
            if (id != userMetric.UserMetricID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMetric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMetricExists(userMetric.UserMetricID))
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
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", userMetric.UserID);
            return View(userMetric);
        }

        // GET: UserMetrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMetric = await _context.UserMetric
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserMetricID == id);
            if (userMetric == null)
            {
                return NotFound();
            }

            return View(userMetric);
        }

        // POST: UserMetrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMetric = await _context.UserMetric.FindAsync(id);
            if (userMetric != null)
            {
                _context.UserMetric.Remove(userMetric);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMetricExists(int id)
        {
            return _context.UserMetric.Any(e => e.UserMetricID == id);
        }
    }
}
