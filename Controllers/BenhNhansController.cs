using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace Hospital.Controllers
{
    public class BenhNhansController : Controller
    {
        private readonly BenhVienContext _context;

        public BenhNhansController(BenhVienContext context)
        {
            _context = context;
        }

        // GET: BenhNhans
        public async Task<IActionResult> Index()
        {
              return _context.BenhNhan != null ? 
                          View(await _context.BenhNhan.ToListAsync()) :
                          Problem("Entity set 'BenhVienContext.BenhNhan'  is null.");
        }

        // GET: BenhNhans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BenhNhan == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // GET: BenhNhans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BenhNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ho,Ten,Cmnd,NgaySinh,GioiTinh,DiaChi,Email")] BenhNhan benhNhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(benhNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(benhNhan);
        }

        // GET: BenhNhans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BenhNhan == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhan.FindAsync(id);
            if (benhNhan == null)
            {
                return NotFound();
            }
            return View(benhNhan);
        }

        // POST: BenhNhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ho,Ten,Cmnd,NgaySinh,GioiTinh,DiaChi,Email")] BenhNhan benhNhan)
        {
            if (id != benhNhan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(benhNhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenhNhanExists(benhNhan.Id))
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
            return View(benhNhan);
        }

        // GET: BenhNhans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BenhNhan == null)
            {
                return NotFound();
            }

            var benhNhan = await _context.BenhNhan
                .FirstOrDefaultAsync(m => m.Id == id);
            if (benhNhan == null)
            {
                return NotFound();
            }

            return View(benhNhan);
        }

        // POST: BenhNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BenhNhan == null)
            {
                return Problem("Entity set 'BenhVienContext.BenhNhan'  is null.");
            }
            var benhNhan = await _context.BenhNhan.FindAsync(id);
            if (benhNhan != null)
            {
                _context.BenhNhan.Remove(benhNhan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenhNhanExists(int id)
        {
          return (_context.BenhNhan?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> SearchByCMND(string cmnd)
        {
            //if (string.IsNullOrEmpty(cmnd) || _context.BenhNhan == null)
            //{
            //    return View("Index", await _context.BenhNhan.ToListAsync());
            //}

            var benhNhan = await _context.BenhNhan.Where(b => b.Cmnd == cmnd).ToListAsync();

            if (benhNhan == null || !benhNhan.Any())
            {
                return View(await _context.BenhNhan.ToListAsync());
            }

            return View(benhNhan);
        }

        public async Task<IActionResult> TransferToDoctor(int? Id)
        {
            var benhNhan = await _context.BenhNhan.FindAsync(Id);
            ViewData["benhNhan"] = benhNhan;
            ViewData["IdBacSi"] = new SelectList(_context.NhanVien.Where(nv => nv.ChucVu == "BS").ToList()
                .Select(nv => new { Id = nv.Id, FullName = String.Format("{0} {1}", nv.Ho, nv.Ten) }), "Id", "FullName");
            return View();
        }

        // POST: BenhNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> TransferToDoctor([Bind("Ngay,ChanDoan,IdBacSi,IdBenhNhan,TinhTrang")] ToaThuoc toaThuoc)
        {
            if (ModelState.IsValid)
            {
                toaThuoc.Ngay = DateTime.Today;
                toaThuoc.TinhTrang = "ChuaKham";
                _context.Add(toaThuoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toaThuoc);
        }


    }
}
