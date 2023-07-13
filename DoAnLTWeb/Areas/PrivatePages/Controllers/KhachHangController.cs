using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTWeb.Areas.PrivatePages.Controllers
{
    public class KhachHangController : BaoMatController
	{
        // GET: PrivatePages/KhachHang
		ShopOnlineEntities db = new ShopOnlineEntities();
        public ActionResult DSKhachHang()
        {
            return View();
        }
		[HttpPost]
		public ActionResult Delete(string makh)
		{
			KhachHang x = db.KhachHangs.Find(makh);
			db.KhachHangs.Remove(x);
			db.SaveChanges();
			return View("DSKhachHang");
		}
	}
}