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
	public class PhatThuocController : Controller
	{
		private readonly BenhVienContext _context;

		public PhatThuocController(BenhVienContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			var toaThuoc = await _context.ToaThuoc
				.Where(t => t.TinhTrang == "DaKham")
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

		public async Task<IActionResult> PhatThuocDetail(int? id)
		{

			if (id == null)
			{
				return NotFound();
			}

			var toaThuoc = await _context.ToaThuoc
				.Include(t => t.IdBenhNhanNavigation)
				.Include(t => t.IdBacSiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

			List<ToaThuocChiTiet> ttctList = await _context.ToaThuocChiTiet
				.Where(ct => ct.IdToaThuoc == id)
				.Include(ct => ct.IdThuocNavigation) //để lấy tên thuốc
				.ToListAsync();

			ViewData["toaThuoc"] = toaThuoc;//truyền toaThuoc cho view

            if (toaThuoc == null)
			{
				return NotFound();
			}

			// Load danh sách thuốc cho dropdownlist
			//ViewBag.ThuocList = _context.Thuoc.Select(t => new { t.Id, t.Ten }).ToList();

			return View(ttctList);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CapNhatTinhTrangToaThuoc(int? idToaThuoc)
		{
			// Lấy thông tin toa thuốc
			var toaThuoc = _context.ToaThuoc.Find(idToaThuoc);
            if (toaThuoc == null)
            {
                return NotFound();
            }

            // Thêm chi tiết toa thuốc
            toaThuoc.TinhTrang = "HoanTat";

			// Cập nhật cơ sở dữ liệu
			_context.Update(toaThuoc);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}



	}


}
