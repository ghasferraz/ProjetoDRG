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
    public class ConfiguracoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfiguracoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Configuracoes
        public async Task<IActionResult> Index()
        {
			
            return View(await _context.Configuracao.ToListAsync());
        }

        // GET: Configuracoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracao
                .SingleOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // GET: Configuracoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuracoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Usuario,Senha")] Configuracao configuracao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracao);
        }
		public ActionResult ControlePaginas(IList<Banco> banco, IList<Sistema> sistemas)
		{
			if (_context.Configuracao.FirstOrDefault()==null)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return RedirectToAction("Home");
			}
			
			return View(banco);
		}
        // GET: Configuracoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracao.SingleOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }
            return View(configuracao);
        }

        // POST: Configuracoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,Senha")] Configuracao configuracao)
        {
            if (id != configuracao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracaoExists(configuracao.Id))
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
            return View(configuracao);
        }

        // GET: Configuracoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracao = await _context.Configuracao
                .SingleOrDefaultAsync(m => m.Id == id);
            if (configuracao == null)
            {
                return NotFound();
            }

            return View(configuracao);
        }

        // POST: Configuracoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuracao = await _context.Configuracao.SingleOrDefaultAsync(m => m.Id == id);
            _context.Configuracao.Remove(configuracao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracaoExists(int id)
        {
            return _context.Configuracao.Any(e => e.Id == id);
        }
    }
}
