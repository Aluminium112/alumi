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
    public class ToaThuocsController : Controller
    {
        private readonly BenhVienContext _context;

        public ToaThuocsController(BenhVienContext context)
        {
            _context = context;
        }

        // GET: ToaThuocs
        public async Task<IActionResult> Index()
        {
            var benhVienContext = _context.ToaThuoc.Include(t => t.IdBacSiNavigation).Include(t => t.IdBenhNhanNavigation);
            return View(await benhVienContext.ToListAsync());
        }

        // GET: ToaThuocs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ToaThuoc == null)
            {
                return NotFound();
            }

            var toaThuoc = await _context.ToaThuoc
                .Include(t => t.IdBacSiNavigation)
                .Include(t => t.IdBenhNhanNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toaThuoc == null)
            {
                return NotFound();
            }

            return View(toaThuoc);
        }

        // GET: ToaThuocs/Create
        public IActionResult Create()
        {
            ViewData["IdBacSi"] = new SelectList(_context.NhanVien, "Id", "Id");
            ViewData["IdBenhNhan"] = new SelectList(_context.BenhNhan, "Id", "Id");
            return View();
        }

        // POST: ToaThuocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ngay,ChanDoan,IdBacSi,IdBenhNhan,TinhTrang")] ToaThuoc toaThuoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toaThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBacSi"] = new SelectList(_context.NhanVien, "Id", "Id", toaThuoc.IdBacSi);
            ViewData["IdBenhNhan"] = new SelectList(_context.BenhNhan, "Id", "Id", toaThuoc.IdBenhNhan);
            return View(toaThuoc);
        }

        // GET: ToaThuocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ToaThuoc == null)
            {
                return NotFound();
            }

            var toaThuoc = await _context.ToaThuoc.FindAsync(id);
            if (toaThuoc == null)
            {
                return NotFound();
            }
            ViewData["IdBacSi"] = new SelectList(_context.NhanVien, "Id", "Id", toaThuoc.IdBacSi);
            ViewData["IdBenhNhan"] = new SelectList(_context.BenhNhan, "Id", "Id", toaThuoc.IdBenhNhan);
            return View(toaThuoc);
        }

        // POST: ToaThuocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ngay,ChanDoan,IdBacSi,IdBenhNhan,TinhTrang")] ToaThuoc toaThuoc)
        {
            if (id != toaThuoc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toaThuoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToaThuocExists(toaThuoc.Id))
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
            ViewData["IdBacSi"] = new SelectList(_context.NhanVien, "Id", "Id", toaThuoc.IdBacSi);
            ViewData["IdBenhNhan"] = new SelectList(_context.BenhNhan, "Id", "Id", toaThuoc.IdBenhNhan);
            return View(toaThuoc);
        }

        // GET: ToaThuocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ToaThuoc == null)
            {
                return NotFound();
            }

            var toaThuoc = await _context.ToaThuoc
                .Include(t => t.IdBacSiNavigation)
                .Include(t => t.IdBenhNhanNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (toaThuoc == null)
            {
                return NotFound();
            }

            return View(toaThuoc);
        }

        // POST: ToaThuocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ToaThuoc == null)
            {
                return Problem("Entity set 'BenhVienContext.ToaThuoc'  is null.");
            }
            var toaThuoc = await _context.ToaThuoc.FindAsync(id);
            if (toaThuoc != null)
            {
                _context.ToaThuoc.Remove(toaThuoc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToaThuocExists(int id)
        {
          return (_context.ToaThuoc?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
