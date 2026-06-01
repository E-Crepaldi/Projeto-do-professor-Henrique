using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Cine99.Data;
using Cine99.Models;

namespace Cine99.Controllers
{
    [Authorize]
    public class AvaliacoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AvaliacoesController(ApplicationDbContext context)
        { _context = context; }

        // GET: Avaliacoes/Create?filmeId=1
        public async Task<IActionResult> Create(int filmeId)
        {
            var filme = await _context.Filmes.FindAsync(filmeId);
            if (filme == null) return NotFound();
            ViewBag.Filme = filme;
            return View(new Avaliacao { FilmeId = filmeId });
        }

        // POST: Avaliacoes/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Avaliacao avaliacao)
        {
            avaliacao.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            avaliacao.DataAvaliacao = DateTime.Now;
            ModelState.Remove("Filme");
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Filmes",
                    new { id = avaliacao.FilmeId });
            }
            ViewBag.Filme = await _context.Filmes.FindAsync(avaliacao.FilmeId);
            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var av = await _context.Avaliacoes
                .Include(a => a.Filme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (av == null) return NotFound();
            return View(av);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var av = await _context.Avaliacoes.FindAsync(id);
            int filmeId = av?.FilmeId ?? 0;
            if (av != null) _context.Avaliacoes.Remove(av);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Filmes",
                new { id = filmeId });
        }
    }
}