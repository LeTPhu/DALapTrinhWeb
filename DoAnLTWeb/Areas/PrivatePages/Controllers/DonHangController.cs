using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DoAnLTWeb.Areas.PrivatePages.Controllers
{
    public class DonHangController : BaoMatController
    {
		private ShopOnlineEntities db = new ShopOnlineEntities();
		// GET: PrivatePages/DonHang
		public ActionResult DSDonHang()
		{
            return View();
        }
		public ActionResult DHDangXuLy()
		{
			return View();
		}
		[HttpPost]
		public ActionResult chiTiet(string maDH)
		{
			DonHang x = db.DonHangs.Find(maDH);
			List<CtDonHang> list = DataIn.GetCtDonHangs(maDH); 
			ViewData["DH"] = x;
			ViewData["CTDH"] = list;
			ViewData["KH"] = DataIn.GetKhachHang(x.maKH);
			return View();
		}
		[HttpPost]
		public ActionResult Active(string maDH)
		{
			DonHang x =  db.DonHangs.Find(maDH);
			x.daKichHoat = true;
			db.SaveChanges();
			return View("DHDangXuLy");
		}
		[HttpPost]
		public ActionResult Delete(string maDH)
		{
			foreach(CtDonHang i in (new ShopOnlineEntities().CtDonHangs.Where(m=>m.soDH == maDH).ToList())) 
			{
				CtDonHang a = db.CtDonHangs.Find(maDH,i.maSP);
				db.CtDonHangs.Remove(a);
			}	
			DonHang x = db.DonHangs.Find(maDH);
			db.DonHangs.Remove(x);
			db.SaveChanges();
			return View("DHDangXuLy");
		}
		[HttpPost]
		public ActionResult TC(string maDH)
		{
			DonHang x = db.DonHangs.Find(maDH);
			x.trangThai = "TC";
			db.SaveChanges();
			return View("DSDonHang");
		}
		[HttpPost]
		public ActionResult HUY(string maDH)
		{
			DonHang x = db.DonHangs.Find(maDH);
			x.trangThai = "HUY";
			db.SaveChanges();
			return View("DSDonHang");
		}

	}
}