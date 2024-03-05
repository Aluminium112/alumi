using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;

namespace Hospital.Controllers
{
    public class ToaThuocChiTietsController : Controller
    {
        private readonly BenhVienContext _context;

        public ToaThuocChiTietsController(BenhVienContext context)
        {
            _context = context;
        }

        // GET: ToaThuocChiTiets
        public async Task<IActionResult> Index()
        {
            var benhVienContext = _context.ToaThuocChiTiet.Include(t => t.IdThuocNavigation).Include(t => t.IdToaThuocNavigation);
            return View(await benhVienContext.ToListAsync());
        }

        // GET: ToaThuocChiTiets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToaThuocChiTiet == null)
            {
                return NotFound();
            }

            var toaThuocChiTiet = await _context.ToaThuocChiTiet
                .Include(t => t.IdThuocNavigation)
                .Include(t => t.IdToaThuocNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toaThuocChiTiet == null)
            {
                return NotFound();
            }

            return View(toaThuocChiTiet);
        }

        // GET: ToaThuocChiTiets/KhamBenh
        public IActionResult KhamBenh()
        {
            ViewData["IdThuoc"] = new SelectList(_context.Thuoc, "Id", "Ten", null);
            ViewData["IdToaThuoc"] = new SelectList(_context.ToaThuoc, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KhamBenh(int? idToaThuoc, List<ToaThuocChiTiet> list)
        {
            if (ModelState.IsValid)
            {
                foreach (ToaThuocChiTiet ttct in list)
                {
                    if (ttct.SoLuong != null)
                    {
                        ttct.IdToaThuoc = idToaThuoc;
                        _context.ToaThuocChiTiet.Add(ttct);
                        //_context.Add(ttct);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdThuoc"] = new SelectList(_context.Thuoc, "Id", "Id");
            ViewData["IdToaThuoc"] = new SelectList(_context.ToaThuoc, "Id", "Id");
            return View();
        }

        // GET: ToaThuocChiTiets/Create
        public IActionResult Create()
        {
            ViewData["IdThuoc"] = new SelectList(_context.Thuoc, "Id", "Ten");
            ViewData["IdToaThuoc"] = new SelectList(_context.ToaThuoc, "Id", "Id");
            return View();
        }

        // POST: ToaThuocChiTiets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdThuoc,IdToaThuoc,SoLuong,Sang,Trua,Chieu,CachDung")] ToaThuocChiTiet toaThuocChiTiet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toaThuocChiTiet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdThuoc"] = new SelectList(_context.Thuoc, "Id", "Ten", toaThuocChiTiet.IdThuoc);
            ViewData["IdToaThuoc"] = new SelectList(_context.ToaThuoc, "Id", "Id", toaThuocChiTiet.IdToaThuoc);
            return View(toaThuocChiTiet);
        }

        // GET: ToaThuocChiTiets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToaThuocChiTiet == null)
            {
                return NotFound();
            }

            var toaThuocChiTiet = await _context.ToaThuocChiTiet.FindAsync(id);
            if (toaThuocChiTiet == null)
            {
                return NotFound();
            }
            ViewData["IdThuoc"] = new SelectList(_context.Thuoc, "Id", "Id", toaThuocChiTiet.IdThuoc);
            ViewData["IdToaThuoc"] = new SelectList(_context.ToaThuoc, "Id", "Id", toaThuocChiTiet.IdToaThuoc);
            return View(toaThuocChiTiet);
        }

        // POST: ToaThuocChiTiets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdThuoc,IdToaThuoc,SoLuong,Sang,Trua,Chieu,CachDung")] ToaThuocChiTiet toaThuocChiTiet)
        {
            if (id != toaThuocChiTiet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toaThuocChiTiet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToaThuocChiTietExists(toaThuocChiTiet.Id))
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
            ViewData["IdThuoc"] = new SelectList(_context.Thuoc, "Id", "Id", toaThuocChiTiet.IdThuoc);
            ViewData["IdToaThuoc"] = new SelectList(_context.ToaThuoc, "Id", "Id", toaThuocChiTiet.IdToaThuoc);
            return View(toaThuocChiTiet);
        }

        // GET: ToaThuocChiTiets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToaThuocChiTiet == null)
            {
                return NotFound();
            }

            var toaThuocChiTiet = await _context.ToaThuocChiTiet
                .Include(t => t.IdThuocNavigation)
                .Include(t => t.IdToaThuocNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toaThuocChiTiet == null)
            {
                return NotFound();
            }

            return View(toaThuocChiTiet);
        }

        // POST: ToaThuocChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToaThuocChiTiet == null)
            {
                return Problem("Entity set 'BenhVienContext.ToaThuocChiTiet'  is null.");
            }
            var toaThuocChiTiet = await _context.ToaThuocChiTiet.FindAsync(id);
            if (toaThuocChiTiet != null)
            {
                _context.ToaThuocChiTiet.Remove(toaThuocChiTiet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToaThuocChiTietExists(int id)
        {
            return (_context.ToaThuocChiTiet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
