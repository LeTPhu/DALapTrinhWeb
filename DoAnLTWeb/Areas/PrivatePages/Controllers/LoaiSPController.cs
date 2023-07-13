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
    public class LoaiSPController : BaoMatController
	{
		ShopOnlineEntities db = new ShopOnlineEntities();
		// Kiểm tra xem có phải người dùng thực hiện lệnh Update cho 1 sản phẩm hay loại sản phẩm nào đó
		private static bool isUpdate = false;
		// GET: PrivatePages/LoaiSP
		public ActionResult LoaiSP()
		{
			return View();
		}
		[HttpPost]
		public ActionResult LoaiSP(LoaiSP x)
		{
			using (DbContextTransaction trans = db.Database.BeginTransaction())
			{
				try
				{
					if (!isUpdate)
						db.LoaiSPs.Add(x);
					else
					{
						LoaiSP y = db.LoaiSPs.Find(x.maLoai);
						y.tenLoai = x.tenLoai;
						y.ghiChu = x.ghiChu;
						isUpdate = false;
					}

					// lưu vào cơ sở dữ liệu
					db.SaveChanges();
					trans.Commit();
					if (ModelState.IsValid)
					{
						ModelState.Clear();
						ViewBag.sc = "Cập nhật thành công";
						return View();
					}
				}
				catch (Exception ex)
				{
					trans.Rollback();
					string e = ex.Message;
				}
			}
			ViewBag.sc = "Cập nhật không thành công";
			return View(x);
		}
		[HttpPost]
		public ActionResult DeleteLoaiSP(string maLoai)
		{
			ShopOnlineEntities db = new ShopOnlineEntities();
			int ma = int.Parse(maLoai);
			//---Tìm đối tượng loại sản phẩm trong dữ liệu
			LoaiSP x = db.LoaiSPs.Find(ma);
			//--- Xóa Loại sản phẩm trong danh sách 
			db.LoaiSPs.Remove(x);
			//---Cập nhật list trên View DBase
			db.SaveChanges();
			//-- Đọc danh sách dữ liệu từ DBase
			return View("LoaiSP");
		}
		public ActionResult UpdateLoaiSP(string mlupdate)
		{
			ShopOnlineEntities db = new ShopOnlineEntities();
			int ma = int.Parse(mlupdate);
			//---Tìm đối tượng loại sản phẩm trong dữ liệu
			LoaiSP x = db.LoaiSPs.Find(ma);
			isUpdate = true;
			//---
			return View("LoaiSP", x);
		}
	}
}