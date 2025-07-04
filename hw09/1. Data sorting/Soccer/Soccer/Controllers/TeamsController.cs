﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Models;

namespace Soccer.Controllers
{
    public class TeamsController : Controller
    {
        private readonly SoccerContext _context;

        public TeamsController(SoccerContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index(SortState sortOrder = SortState.TeamNameAsc)
        {
            IQueryable<Teams> teams = _context.Teams;

            ViewData["NameSort"] = sortOrder == SortState.TeamNameAsc ? SortState.TeamNameDesc : SortState.TeamNameAsc;
            ViewData["CoachSort"] = sortOrder == SortState.CoachAsc ? SortState.CoachDesc : SortState.CoachAsc;

            teams = sortOrder switch
            {
                SortState.TeamNameDesc => teams.OrderByDescending(t => t.Name),
                SortState.CoachAsc => teams.OrderBy(t => t.Coach),
                SortState.CoachDesc => teams.OrderByDescending(t => t.Coach),
                _ => teams.OrderBy(t => t.Name),
            };

            return View(await teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Coach")] Teams teams)
        {
            _context.Add(teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }
            return View(teams);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Coach")] Teams teams)
        {
            if (id != teams.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(teams);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamsExists(teams.Id))
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

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'SoccerContext.Teams'  is null.");
            }
            var teams = await _context.Teams.FindAsync(id);
            if (teams != null)
            {
                _context.Teams.Remove(teams);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(int id)
        {
          return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
