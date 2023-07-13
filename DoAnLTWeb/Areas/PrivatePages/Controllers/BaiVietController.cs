using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DoAnLTWeb.Areas.PrivatePages.Controllers
{
    public class BaiVietController : BaoMatController
	{
		// GET: PrivatePages/BaiViet
		private static ShopOnlineEntities db = new ShopOnlineEntities();
		private static bool isUpdate = false;
		[HttpPost]
		public ActionResult Delete(string maBV)
		{
			//---Dùng lệnh để xóa bài viết dựa vào mã bài viết
			BaiViet x = db.BaiViets.Find(maBV);
			db.BaiViets.Remove(x);
			//---Cập nhật Database
			db.SaveChanges();
			//---Hiển thị lại danh sách với các danh sách sau cập nhật
			return View("DSBaiViet");

		}
		[HttpPost]
		public ActionResult Active(string maBaiViet)
		{
			//---Dùng lệnh để cấm bài viết dựa vào mã bài viết
			BaiViet x = db.BaiViets.Find(maBaiViet);
			x.daDuyet = !x.daDuyet;
			//---Cập nhật Database
			db.SaveChanges();
			if (x.daDuyet == true) return View("DSBaiVietDaAn");
			//---Hiển thị lại danh sách với các danh sách sau cập nhật
			return View("DSBaiViet");
		}
		public ActionResult DSBaiViet()
		{
			return View();
		}
		public ActionResult DSBaiVietDaAn()
		{
			return View();
		}
		[HttpGet]
		public ActionResult DangBaiViet()
		{
			BaiViet bv = new BaiViet();
			bv.ngayDang = DateTime.Now;
			bv.taiKhoan = (Session["DangNhap"] as TaiKhoan).taiKhoan1;
			isUpdate = false;
			return View(bv);
		}
		[HttpPost]
		public ActionResult DangBaiViet(BaiViet x, HttpPostedFileBase HinhDaiDien)
		{
			using (DbContextTransaction trans = db.Database.BeginTransaction())
			{
				try
				{
					bool isDaDuyet = true;
					if (!isUpdate)
					{
						// B1: Xử lý thông tin nhận về từ View
						x.maBV = string.Format("{0:ddMMyyhhmm}", DateTime.Now);
						x.ngayDang = DateTime.Now;
						x.taiKhoan = (Session["DangNhap"] as TaiKhoan).taiKhoan1;
						x.daDuyet = false;
						if (HinhDaiDien != null)
						{
							// Lưu hình vào bài viết
							string viTri = "/Assets/img/";
							string viTriSv = Server.MapPath("~/" + viTri);
							string PMoRong = Path.GetExtension(HinhDaiDien.FileName);
							string tenF = "HDD" + x.maBV + PMoRong;
							HinhDaiDien.SaveAs(viTriSv + tenF);
							x.hinhDD = viTri + tenF;
						}
						else x.hinhDD = "";
						db.BaiViets.Add(x);
					}
					else
					{
						BaiViet a = db.BaiViets.Find(x.maBV);
						a.maBV = x.maBV;
						a.ngayDang = DateTime.Now;
						a.taiKhoan = (Session["DangNhap"] as TaiKhoan).taiKhoan1;
						a.daDuyet = isDaDuyet = false;
						a.tenBV = x.tenBV;
						a.ndTomTat = x.ndTomTat;
						a.loaiTin = x.loaiTin;
						a.noiDung = x.noiDung;
						if (HinhDaiDien != null)
						{
							// Lưu hình vào bài viết
							string viTri = "/Assets/img/";
							string viTriSv = Server.MapPath("~/" + viTri);
							string PMoRong = Path.GetExtension(HinhDaiDien.FileName);
							string tenF = "HDD" + a.maBV + PMoRong;
							HinhDaiDien.SaveAs(viTriSv + tenF);
							a.hinhDD = viTri + tenF;
							isUpdate = false;
						}
						else isUpdate = false;
					}
					db.SaveChanges();
					trans.Commit();
					if (isDaDuyet == false)
						return RedirectToAction("DSBaiVietDaAn");
					else if (isDaDuyet == true)
						return RedirectToAction("DSBaiViet");
				}
				catch (Exception e)
				{
					trans.Rollback();
					string s = e.Message;
				}
			}
			return View(x);
	}
		public ActionResult Update(string mabv)
		{
			//---Tìm đối tượng bài viết trong cs dữ liệu
			BaiViet x = db.BaiViets.Find(mabv);
			isUpdate = true;
			//---
			return View("DangBaiViet", x);
		}
	}
}