using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exam_2.Models;
using Exam_2.Models.OthModels;

namespace Exam_2.Controllers
{
    public class RestoransController : Controller
    {
        private readonly DbCont _context;

        public RestoransController(DbCont context)
        {
            _context = context;
        }

        // GET: Restorans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Restorans.ToListAsync());
        }

        // GET: Restorans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();  
            }

            var restoran = await _context.Restorans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restoran == null)
            {
                return NotFound();
            }

            return View(restoran);
        }

        // GET: Restorans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restorans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Restoran restoran)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restoran);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restoran);
        }

        // GET: Restorans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restorans.FindAsync(id);
            if (restoran == null)
            {
                return NotFound();
            }
            return View(restoran);
        }

        // POST: Restorans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Restoran restoran)
        {
            if (id != restoran.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restoran);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestoranExists(restoran.Id))
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
            return View(restoran);
        }

        // GET: Restorans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restoran = await _context.Restorans
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restoran == null)
            {
                return NotFound();
            }

            return View(restoran);
        }

        // POST: Restorans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restoran = await _context.Restorans.FindAsync(id);
            _context.Restorans.Remove(restoran);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestoranExists(int id)
        {
            return _context.Restorans.Any(e => e.Id == id);
        }
    }
}
