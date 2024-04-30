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
    public class BlogMetricsController : Controller
    {
        private readonly IdentityDBContext _context;

        public BlogMetricsController(IdentityDBContext context)
        {
            _context = context;
        }

        // GET: BlogMetrics
        public async Task<IActionResult> Index()
        {
            var identityDBContext = _context.BlogMetric.Include(b => b.Blog);
            return View(await identityDBContext.ToListAsync());
        }

        // GET: BlogMetrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogMetric = await _context.BlogMetric
                .Include(b => b.Blog)
                .FirstOrDefaultAsync(m => m.BlogMetricID == id);
            if (blogMetric == null)
            {
                return NotFound();
            }

            return View(blogMetric);
        }

        // GET: BlogMetrics/Create
        public IActionResult Create()
        {
            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "Body");
            return View();
        }

        // POST: BlogMetrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogMetricID,BlogID,TotalUpvotes,TotalDownvotes,TotalComments")] BlogMetric blogMetric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogMetric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "Body", blogMetric.BlogID);
            return View(blogMetric);
        }

        // GET: BlogMetrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogMetric = await _context.BlogMetric.FindAsync(id);
            if (blogMetric == null)
            {
                return NotFound();
            }
            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "Body", blogMetric.BlogID);
            return View(blogMetric);
        }

        // POST: BlogMetrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("BlogMetricID,BlogID,TotalUpvotes,TotalDownvotes,TotalComments")] BlogMetric blogMetric)
        {
            if (id != blogMetric.BlogMetricID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogMetric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogMetricExists(blogMetric.BlogMetricID))
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
            ViewData["BlogID"] = new SelectList(_context.Blogs, "BlogID", "Body", blogMetric.BlogID);
            return View(blogMetric);
        }

        // GET: BlogMetrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogMetric = await _context.BlogMetric
                .Include(b => b.Blog)
                .FirstOrDefaultAsync(m => m.BlogMetricID == id);
            if (blogMetric == null)
            {
                return NotFound();
            }

            return View(blogMetric);
        }

        // POST: BlogMetrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var blogMetric = await _context.BlogMetric.FindAsync(id);
            if (blogMetric != null)
            {
                _context.BlogMetric.Remove(blogMetric);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogMetricExists(int? id)
        {
            return _context.BlogMetric.Any(e => e.BlogMetricID == id);
        }
    }
}
