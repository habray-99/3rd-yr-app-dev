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
    public class ReactionTypesController : Controller
    {
        private readonly IdentityDBContext _context;

        public ReactionTypesController(IdentityDBContext context)
        {
            _context = context;
        }

        // GET: ReactionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReactionTypes.ToListAsync());
        }

        // GET: ReactionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reactionType = await _context.ReactionTypes
                .FirstOrDefaultAsync(m => m.ReactionTypeID == id);
            if (reactionType == null)
            {
                return NotFound();
            }

            return View(reactionType);
        }

        // GET: ReactionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReactionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReactionTypeID,ReactionName")] ReactionType reactionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reactionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reactionType);
        }

        // GET: ReactionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reactionType = await _context.ReactionTypes.FindAsync(id);
            if (reactionType == null)
            {
                return NotFound();
            }
            return View(reactionType);
        }

        // POST: ReactionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ReactionTypeID,ReactionName")] ReactionType reactionType)
        {
            if (id != reactionType.ReactionTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reactionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReactionTypeExists(reactionType.ReactionTypeID))
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
            return View(reactionType);
        }

        // GET: ReactionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reactionType = await _context.ReactionTypes
                .FirstOrDefaultAsync(m => m.ReactionTypeID == id);
            if (reactionType == null)
            {
                return NotFound();
            }

            return View(reactionType);
        }

        // POST: ReactionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var reactionType = await _context.ReactionTypes.FindAsync(id);
            if (reactionType != null)
            {
                _context.ReactionTypes.Remove(reactionType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReactionTypeExists(int? id)
        {
            return _context.ReactionTypes.Any(e => e.ReactionTypeID == id);
        }
    }
}
