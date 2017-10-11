using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoDRG.Data;
using ProjetoDRG.Models;

namespace ProjetoDRG.Controllers
{
    public class BancoSistemasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BancoSistemasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BancoSistemas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BancoSistema.Include(b => b.banco).Include(b => b.sistema);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BancoSistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancoSistema = await _context.BancoSistema
                .Include(b => b.banco)
                .Include(b => b.sistema)
                .SingleOrDefaultAsync(m => m.IdSistema == id);
            if (bancoSistema == null)
            {
                return NotFound();
            }

            return View(bancoSistema);
        }

        // GET: BancoSistemas/Create
        public IActionResult Create()
        {
            ViewData["IdBanco"] = new SelectList(_context.Banco, "Id", "Nome");
            ViewData["IdSistema"] = new SelectList(_context.Sistema, "Id", "NomeSistema");
            return View();
        }

        // POST: BancoSistemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSistema,IdBanco")] BancoSistema bancoSistema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bancoSistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBanco"] = new SelectList(_context.Banco, "Id", "Nome", bancoSistema.IdBanco);
            ViewData["IdSistema"] = new SelectList(_context.Sistema, "Id", "NomeSistema", bancoSistema.IdSistema);
            return View(bancoSistema);
        }

        // GET: BancoSistemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancoSistema = await _context.BancoSistema.SingleOrDefaultAsync(m => m.IdSistema == id);
            if (bancoSistema == null)
            {
                return NotFound();
            }
            ViewData["IdBanco"] = new SelectList(_context.Banco, "Id", "Nome", bancoSistema.IdBanco);
            ViewData["IdSistema"] = new SelectList(_context.Sistema, "Id", "NomeSistema", bancoSistema.IdSistema);
            return View(bancoSistema);
        }

        // POST: BancoSistemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSistema,IdBanco")] BancoSistema bancoSistema)
        {
            if (id != bancoSistema.IdSistema)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bancoSistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BancoSistemaExists(bancoSistema.IdSistema))
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
            ViewData["IdBanco"] = new SelectList(_context.Banco, "Id", "Nome", bancoSistema.IdBanco);
            ViewData["IdSistema"] = new SelectList(_context.Sistema, "Id", "NomeSistema", bancoSistema.IdSistema);
            return View(bancoSistema);
        }

        // GET: BancoSistemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bancoSistema = await _context.BancoSistema
                .Include(b => b.banco)
                .Include(b => b.sistema)
                .SingleOrDefaultAsync(m => m.IdSistema == id);
            if (bancoSistema == null)
            {
                return NotFound();
            }

            return View(bancoSistema);
        }

        // POST: BancoSistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bancoSistema = await _context.BancoSistema.SingleOrDefaultAsync(m => m.IdSistema == id);
            _context.BancoSistema.Remove(bancoSistema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BancoSistemaExists(int id)
        {
            return _context.BancoSistema.Any(e => e.IdSistema == id);
        }
    }
}
