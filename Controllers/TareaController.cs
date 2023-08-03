using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto.Data;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class TareaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TareaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tarea
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tarea.Include(t => t.Usuario);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tarea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarea == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TareaID == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tarea/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: Tarea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TareaID,Descripcion,Fecha,Realizada,UsuarioID")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioID"] = new SelectList(_context.Users, "Id", "Id", tarea.UsuarioID);
            return View(tarea);
        }

        // GET: Tarea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarea == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = new SelectList(_context.Users, "Id", "Id", tarea.UsuarioID);
            return View(tarea);
        }

        // POST: Tarea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TareaID,Descripcion,Fecha,Realizada,UsuarioID")] Tarea tarea)
        {
            if (id != tarea.TareaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.TareaID))
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
            ViewData["UsuarioID"] = new SelectList(_context.Users, "Id", "Id", tarea.UsuarioID);
            return View(tarea);
        }

        // GET: Tarea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarea == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.TareaID == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tarea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarea == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tarea'  is null.");
            }
            var tarea = await _context.Tarea.FindAsync(id);
            if (tarea != null)
            {
                _context.Tarea.Remove(tarea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
          return (_context.Tarea?.Any(e => e.TareaID == id)).GetValueOrDefault();
        }
    }
}
