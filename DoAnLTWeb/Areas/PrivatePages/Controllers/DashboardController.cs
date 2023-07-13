using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnLTWeb.Models;

namespace DoAnLTWeb.Areas.PrivatePages.Controllers
{
    public class DashboardController : BaoMatController
    {
        // GET: PrivatePages/Dashboard
        private ShopOnlineEntities db = new ShopOnlineEntities();
        public ActionResult Dashboard()
        {
            TaiKhoan x = Session["DangNhap"] as TaiKhoan;
            x.matKhau = "";
			return View(x);
        }
        [HttpPost]
		public ActionResult Dashboard(TaiKhoan x,string xnmk)
        {
            using (DbContextTransaction trans = db.Database.BeginTransaction())
            {
                try
                {
					if (x.matKhau.Equals(xnmk))
					{
						TaiKhoan y = db.TaiKhoans.Find(x.taiKhoan1);
						y.hoDem = x.hoDem;
						y.tenTV = x.tenTV;
						y.email = x.email;
						y.matKhau = x.matKhau;
						db.SaveChanges();
						trans.Commit();
						ViewBag.sc = "Cập nhật thành công";
						return RedirectToAction("Dashboard");
					}
					else
					{
						ViewBag.usc = "2 mật khẩu nhập khác nhau";
						return RedirectToAction("Dashboard");
					}
				}
				catch(Exception ex)
				{
					trans.Rollback();
					string e = ex.Message;
				}
			}
			return RedirectToAction("Dashboard");

		}


	}
}