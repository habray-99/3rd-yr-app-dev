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
    public class CommentReactionsController : Controller
    {
        private readonly IdentityDBContext _context;

        public CommentReactionsController(IdentityDBContext context)
        {
            _context = context;
        }

        // GET: CommentReactions
        public async Task<IActionResult> Index()
        {
            var identityDBContext = _context.CommentReactions.Include(c => c.Comment).Include(c => c.ReactionType).Include(c => c.User);
            return View(await identityDBContext.ToListAsync());
        }

        // GET: CommentReactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentReaction = await _context.CommentReactions
                .Include(c => c.Comment)
                .Include(c => c.ReactionType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommentReactionID == id);
            if (commentReaction == null)
            {
                return NotFound();
            }

            return View(commentReaction);
        }

        // GET: CommentReactions/Create
        public IActionResult Create()
        {
            ViewData["CommentID"] = new SelectList(_context.Comments, "CommentID", "CommentText");
            ViewData["ReactionTypeID"] = new SelectList(_context.ReactionTypes, "ReactionTypeID", "ReactionName");
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CommentReactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentReactionID,CommentID,UserID,ReactionTypeID")] CommentReaction commentReaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentReaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommentID"] = new SelectList(_context.Comments, "CommentID", "CommentText", commentReaction.CommentID);
            ViewData["ReactionTypeID"] = new SelectList(_context.ReactionTypes, "ReactionTypeID", "ReactionName", commentReaction.ReactionTypeID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", commentReaction.UserID);
            return View(commentReaction);
        }

        // GET: CommentReactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentReaction = await _context.CommentReactions.FindAsync(id);
            if (commentReaction == null)
            {
                return NotFound();
            }
            ViewData["CommentID"] = new SelectList(_context.Comments, "CommentID", "CommentText", commentReaction.CommentID);
            ViewData["ReactionTypeID"] = new SelectList(_context.ReactionTypes, "ReactionTypeID", "ReactionName", commentReaction.ReactionTypeID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", commentReaction.UserID);
            return View(commentReaction);
        }

        // POST: CommentReactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("CommentReactionID,CommentID,UserID,ReactionTypeID")] CommentReaction commentReaction)
        {
            if (id != commentReaction.CommentReactionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentReaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentReactionExists(commentReaction.CommentReactionID))
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
            ViewData["CommentID"] = new SelectList(_context.Comments, "CommentID", "CommentText", commentReaction.CommentID);
            ViewData["ReactionTypeID"] = new SelectList(_context.ReactionTypes, "ReactionTypeID", "ReactionName", commentReaction.ReactionTypeID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", commentReaction.UserID);
            return View(commentReaction);
        }

        // GET: CommentReactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentReaction = await _context.CommentReactions
                .Include(c => c.Comment)
                .Include(c => c.ReactionType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommentReactionID == id);
            if (commentReaction == null)
            {
                return NotFound();
            }

            return View(commentReaction);
        }

        // POST: CommentReactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var commentReaction = await _context.CommentReactions.FindAsync(id);
            if (commentReaction != null)
            {
                _context.CommentReactions.Remove(commentReaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentReactionExists(int? id)
        {
            return _context.CommentReactions.Any(e => e.CommentReactionID == id);
        }
    }
}
