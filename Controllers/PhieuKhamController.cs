using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;

namespace Hospital.Controllers
{
	public class PhieuKhamController : Controller
	{
		private readonly BenhVienContext _context;

		public PhieuKhamController(BenhVienContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var toaThuoc = await _context.ToaThuoc
				.Where(t=> t.TinhTrang == "ChuaKham")
				.Include(t => t.IdBenhNhanNavigation)
				.ToListAsync();
			return View(toaThuoc);
		}
		//public async Task<IActionResult> BenhNhanDetail(int? id)
		//{
		//    if (id == null)
		//    {
		//        return NotFound();
		//    }

		//    var toaThuoc = await _context.ToaThuoc
		//        .Include(t => t.IdBenhNhanNavigation)
		//        .FirstOrDefaultAsync(m => m.Id == id);

		//    if (toaThuoc == null)
		//    {
		//        return NotFound();
		//    }

		//    // Load danh sách thuốc cho dropdownlist
		//    ViewBag.ThuocList = _context.Thuoc.Select(t => new { t.Id, t.Ten }).ToList();

		//    return View(toaThuoc);
		//}

		public async Task<IActionResult> KhamBenh(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var toaThuoc = await _context.ToaThuoc
				.Include(t => t.IdBenhNhanNavigation)
						 .Include(t => t.IdBacSiNavigation)
				.FirstOrDefaultAsync(m => m.Id == id);			

			if (toaThuoc == null)
			{
				return NotFound();
			}
			// Load danh sách thuốc cho dropdownlist
			ViewData["IdThuoc"] = new SelectList(_context.Thuoc, "Id", "Ten");
			ViewData["ToaThuoc"] = toaThuoc;
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KhamBenh(int? idToaThuoc, string chanDoan, List<ToaThuocChiTiet> list)
        {
            if (ModelState.IsValid)
            {
				var toaThuoc = await _context.ToaThuoc.FindAsync(idToaThuoc);
                if (toaThuoc == null)
                {
                    return NotFound();
                }
				//Cập nhật thông tin toa thuốc
                toaThuoc.ChanDoan = chanDoan;
				toaThuoc.TinhTrang = "DaKham";
                _context.Update(toaThuoc);

                //thêm các dòng ToaThuocChiTiet
                foreach (ToaThuocChiTiet ttct in list)
                {
                    if (ttct.SoLuong != null)
                    {
                        ttct.IdToaThuoc = idToaThuoc;
                        _context.ToaThuocChiTiet.Add(ttct);
                        //_context.Add(ttct);
                    }
                }
				//cập nhật db
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        

		

	}


}
