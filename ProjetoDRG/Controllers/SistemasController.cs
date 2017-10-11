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
    public class SistemasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SistemasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sistemas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sistema.ToListAsync());
        }

        // GET: Sistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistema
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sistema == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        // GET: Sistemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sistemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeSistema")] Sistema sistema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sistema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sistema);
        }

        // GET: Sistemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistema.SingleOrDefaultAsync(m => m.Id == id);
            if (sistema == null)
            {
                return NotFound();
            }
            return View(sistema);
        }

        // POST: Sistemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeSistema")] Sistema sistema)
        {
            if (id != sistema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sistema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SistemaExists(sistema.Id))
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
            return View(sistema);
        }

        // GET: Sistemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sistema = await _context.Sistema
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sistema == null)
            {
                return NotFound();
            }

            return View(sistema);
        }

        // POST: Sistemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sistema = await _context.Sistema.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sistema.Remove(sistema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SistemaExists(int id)
        {
            return _context.Sistema.Any(e => e.Id == id);
        }
    }
}
