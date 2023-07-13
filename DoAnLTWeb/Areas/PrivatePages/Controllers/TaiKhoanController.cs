using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DoAnLTWeb.Areas.PrivatePages.Controllers
{
    public class TaiKhoanController : BaoMatController
	{
		ShopOnlineEntities db = new ShopOnlineEntities();
		// Kiểm tra xem có phải người dùng thực hiện lệnh Update không
		private static bool isUpdate = false;
		// GET: PrivatePages/TaiKhoan
		public ActionResult DSTaiKhoan()
        {
            TaiKhoan x = Session["DangNhap"] as TaiKhoan;
            List<TaiKhoan> list;
			if (x.taiKhoan1 == "admin") list = DataIn.GetListTaiKhoans();
			else list = DataIn.GetListTaiKhoans().Where(m=>m.taiKhoan1.Equals(x.taiKhoan1)).ToList<TaiKhoan>();
			ViewData["list"] = list;
			return View();
        }
		public ActionResult ThemTV() { return View(); }
		[HttpPost]
		public ActionResult ThemTV(TaiKhoan x, string xnmk)
		{
			using (DbContextTransaction trans = db.Database.BeginTransaction())
			{
				try
				{
					if (!isUpdate)
					{
						if (db.TaiKhoans.Find(x.taiKhoan1) == null)
						{
							if (x.matKhau.Equals(xnmk))
							{
								if (x.email == null) x.email = "";
								if (x.hoDem == null) x.hoDem = "";
								if (x.diaChi == null) x.diaChi = "";
								if (x.soDT == null) x.soDT = "";
								x.trangThai = true;
								db.TaiKhoans.Add(x);
							}
							else
							{
								ViewBag.usc = "2 mật khẩu nhập khác nhau";
								return View("ThemTV", x);
							}
						}
						else
						{
							ViewBag.usc = "Username đã tồn tại";
							return View("ThemTV", x);
						}
					}
					else // Dành cho Update
					{
						if (db.TaiKhoans.Find(x.taiKhoan1) != null)
						{
							if (x.matKhau.Equals(xnmk))
							{
								TaiKhoan y = db.TaiKhoans.Find(x.taiKhoan1);
								y.tenTV = x.tenTV;
								if (x.email != null) y.email = x.email;
								if (x.hoDem != null) y.hoDem = x.hoDem;
								if (x.diaChi != null) y.diaChi = x.diaChi;
								if (x.soDT != null) y.soDT = x.soDT;
								if (x.ngaysinh != null) y.ngaysinh = x.ngaysinh;
								y.matKhau = x.matKhau;
							}
							else
							{
								ViewBag.usc = "2 mật khẩu nhập khác nhau";
								return View("ThemTV", x);

							}
							isUpdate = false;
						}
						else
						{
							isUpdate = false;
							ViewBag.usc = "Username đã tồn tại";
							return View("ThemTV", x);
						}
					}
					// lưu vào cơ sở dữ liệu
					db.SaveChanges();
					trans.Commit();
					return RedirectToAction("DSTaiKhoan");
				}
				catch (Exception ex)
				{
					trans.Rollback();
					string e = ex.Message;
				}
				return View();
			}
		}
		public ActionResult DSTaiKhoanNgungHD()
		{
			List<TaiKhoan> list = DataIn.GetListTaiKhoans().Where(m => m.trangThai == false).ToList<TaiKhoan>();
			ViewData["list"] = list;
			return View();
		}
		[HttpPost]
		public ActionResult UpdateTV(string maTV)
		{
			//---Tìm đối tượng tài khoản trong dữ liệu
			TaiKhoan x = db.TaiKhoans.Find(maTV);
			isUpdate = true;
			//---
			return View("ThemTV", x);
		}
		[HttpPost]
		public ActionResult ActiveTV(string maTV)
		{
			TaiKhoan x = db.TaiKhoans.Find(maTV);
			if (x.taiKhoan1 != "admin") x.trangThai = !x.trangThai;
			db.SaveChanges();
			return RedirectToAction("DSTaiKhoan");
		}
		[HttpPost]
		public ActionResult DeleteTV(string maTV)
		{
			TaiKhoan x = db.TaiKhoans.Find(maTV);
			db.TaiKhoans.Remove(x);
			db.SaveChanges();
			return View("DSTaiKhoanNgungHD");
		}
	}
}