using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cine99.Data;
using Cine99.Models;

namespace Cine99.Controllers
{
    public class FilmesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilmesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Filmes
        public async Task<IActionResult> Index()
        {
            var filmes = await _context.Filmes
                .Include(f => f.Avaliacoes)
                .ToListAsync();
            return View(filmes);
        }

        // GET: Filmes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var filme = await _context.Filmes
                .Include(f => f.Avaliacoes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filme == null) return NotFound();
            return View(filme);
        }

        // GET: Filmes/Create
        [Authorize]
        public IActionResult Create() => View();

        // POST: Filmes/Create
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Create(Filme filme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null) return NotFound();
            return View(filme);
        }

        // POST: Filmes/Edit/5
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Edit(int id, Filme filme)
        {
            if (id != filme.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(filme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // GET: Filmes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null) return NotFound();
            return View(filme);
        }

        // POST: Filmes/Delete/5

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme != null) _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

