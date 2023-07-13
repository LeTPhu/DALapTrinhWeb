using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DoAnLTWeb.Areas.PrivatePages.Controllers
{
    public class SanPhamController : BaoMatController
	{
		ShopOnlineEntities db =new ShopOnlineEntities();
		// Kiểm tra xem có phải người dùng thực hiện lệnh Update cho 1 sản phẩm hay loại sản phẩm nào đó
		private static bool isUpdate = false; 
		// GET: PrivatePages/SanPham	
		public ActionResult SPMoi()
		{
			SanPham sp = new SanPham();
			sp.ngayDang = DateTime.Now;
			sp.taiKhoan = (Session["DangNhap"] as TaiKhoan).taiKhoan1;
			isUpdate = false;
			return View(sp);
		}
		[HttpPost]
		public ActionResult SPMoi(SanPham x, HttpPostedFileBase HinhDaiDien)
		{
			using (DbContextTransaction trans = db.Database.BeginTransaction())
			{
				try
				{
					bool isDaDuyet = true;
					if (!isUpdate)
					{
						// B1: Xử lý thông tin nhận về từ View
						x.maSP = string.Format("{0:ddMMyyhhmm}", DateTime.Now);
						x.ngayDang = DateTime.Now;
						x.taiKhoan = (Session["DangNhap"] as TaiKhoan).taiKhoan1;
						x.daDuyet = isDaDuyet = false;
						if (HinhDaiDien != null)
						{
							// Lưu hình vào bài viết
							string viTri = "/Assets/img/";
							string viTriSv = Server.MapPath("~/" + viTri);
							string PMoRong = Path.GetExtension(HinhDaiDien.FileName);
							string tenF = "HDD" + x.maSP + PMoRong;
							HinhDaiDien.SaveAs(viTriSv + tenF);
							x.hinhDD = viTri + tenF;
						}
						else x.hinhDD = "";
						db.SanPhams.Add(x);
					}
					else
					{
						SanPham a = db.SanPhams.Find(x.maSP);
						a.maSP = x.maSP;
						a.ngayDang = DateTime.Now;
						a.taiKhoan = (Session["DangNhap"] as TaiKhoan).taiKhoan1;
						a.tenSP = x.tenSP;
						a.giaBan = x.giaBan;
						a.giamGia = x.giamGia;
						a.ndTomTat = x.ndTomTat;
						a.maLoai = x.maLoai;
						a.noiDung = x.noiDung;
						if (HinhDaiDien != null)
						{
							// Lưu hình vào bài viết
							string viTri = "/Assets/img/";
							string viTriSv = Server.MapPath("~/" + viTri);
							string PMoRong = Path.GetExtension(HinhDaiDien.FileName);
							string tenF = "HDD" + a.maSP + PMoRong;
							HinhDaiDien.SaveAs(viTriSv + tenF);
							a.hinhDD = viTri + tenF;
							isUpdate = false;
						}
						else isUpdate = false;
					}
					db.SaveChanges();
					trans.Commit();
					if (isDaDuyet == false)
						return RedirectToAction("SPNgungKD");
					else if (isDaDuyet == true)
						return RedirectToAction("DSSanPham");
				}
				catch (Exception ex)
				{
					trans.Rollback();
					string s = ex.Message;
				}
			}
		return View(x);
		}
		public ActionResult SPDangKinhDoanh()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SPNgungKD(int ma)
		{
			List<SanPham> list = DataIn.GetListSPEqualsLoaiSPChuaDuyet(ma);
			ViewData["list"] = list;
			return View();
		}
		public ActionResult SPNgungKD()
		{
			return View();
		}
		public ActionResult DSSanPham()
		{
			return View();
		}
		[HttpPost]
		public ActionResult DSSanPham(int ma)
		{
			List<SanPham> list = DataIn.GetListSPEqualsLoaiSPDaDuyet(ma);
			ViewData["list"] = list;
			ViewBag.loaiSP = db.LoaiSPs.Find(ma).tenLoai.ToLower();
			return View();
		}
		[HttpPost]
		public ActionResult DeleteSP(string maSP)
		{
			//---Dùng lệnh để xóa bài viết dựa vào mã bài viết
			SanPham x = db.SanPhams.Find(maSP);
			db.SanPhams.Remove(x);
			//---Cập nhật Database
			db.SaveChanges();
			//---Hiển thị lại danh sách với các danh sách sau cập nhật
			if (x.daDuyet == true) { return View("DSSanPham"); }
			else return View("SPNgungKD");
		}
		[HttpPost]
		public ActionResult ActiveSP(string maSanPham)
		{
			//---Dùng lệnh để cấm bài viết dựa vào mã bài viết
			SanPham x = db.SanPhams.Find(maSanPham);
			x.daDuyet = !x.daDuyet;
			//---Cập nhật Database
			db.SaveChanges();
			//---Hiển thị lại danh sách với các danh sách sau cập nhật
			if (x.daDuyet == true) { return View("SPNgungKD"); }
			else return View("DSSanPham");
		}
		public ActionResult UpdateSP(string masp)
		{
			//---Tìm đối tượng bài viết trong cs dữ liệu
			SanPham x = db.SanPhams.Find(masp);
			isUpdate = true;
			//---
			return View("SPMoi",x);
		}

	}
}