using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTWeb.Controllers
{
    public class GioHangController : Controller
    {
		// GET: GioHang
		ShopOnlineEntities db = new ShopOnlineEntities();
		public ActionResult AddToCart(string maSP)
		{
			Shopcart gh = Session["GioHang"] as Shopcart;
			gh.addItem(maSP);
			Session["GioHang"] = gh;
			return RedirectToAction("ShopGrid","Shop");
		}
		[HttpGet]
		public ActionResult ShopCart()
		{
			return View();
		}
		// GET: Shopcart
		[HttpPost]
		public ActionResult Increase(string maSP)
		{
			Shopcart gh = Session["GioHang"] as Shopcart;
			gh.addItem(maSP);
			Session["GioHang"] = gh;
			return RedirectToAction("ShopCart");
		}
		[HttpPost]
		public ActionResult Decrease(string maSP)
		{
			Shopcart gh = Session["GioHang"] as Shopcart;
			gh.GiamSP(maSP);
			Session["GioHang"] = gh;
			return RedirectToAction("ShopCart");
		}
		[HttpPost]
		public ActionResult Delete(string maSP)
		{
			Shopcart gh = Session["GioHang"] as Shopcart;
			gh.deleteItem(maSP);
			Session["GioHang"] = gh;
			return RedirectToAction("ShopCart");
		}
		//===============================================================================================
		public ActionResult Checkout()
		{
			return View();
		}
		public ActionResult SaveToData(KhachHang x)
		{
			using (var db = new ShopOnlineEntities())
			{
				using (DbContextTransaction trans = db.Database.BeginTransaction())
				{
					try
					{
						//Kiểm tra xem có phải khách hàng mới hay không
						if (db.KhachHangs.Find(x.soDT)==null)
						{
							// new customer object and add to khachhang domain
							x.maKH = x.soDT;
							x.gioiTinh = true;
							if (x.email == null) x.email = "";
							if (x.ghiChu == null) x.ghiChu = "";
							//Add customer info  to data model
							db.KhachHangs.Add(x);
							// save to database
							db.SaveChanges();
							// new an order object and add to Donhang domain 
							DonHang d = new DonHang();
							//update order  info to donhang object you have just created before
							d.soDH = string.Format("{0:yyMMddhhmm}", DateTime.Now);
							d.maKH = x.maKH;
							d.ngayDat = DateTime.Now;
							d.ngayGH = DateTime.Now.AddDays(2);
							d.taiKhoan = "admin";
							d.diaChiGH = x.diaChi;
							d.ghiChu = x.ghiChu;
							d.daKichHoat = false;
							d.trangThai = "";
							//Add mordel info to datamodel
							db.DonHangs.Add(d);
							//save to database
							db.SaveChanges();
							//get list of item from cartshop
							Shopcart gh = Session["GioHang"] as Shopcart;
							// update order info to Donhang object you have just created before
							foreach (CtDonHang i in gh.spChon.Values)
							{
								i.soDH = d.soDH;
								db.CtDonHangs.Add(i);
							}
							//save to database
							db.SaveChanges();
							//finish and commit all
							trans.Commit();
							gh.maKH = x.maKH;
							gh.maDH = d.soDH;
							Session["GioHang"] = gh;
							return RedirectToAction("CheckoutSuccess", "GioHang");
						}
						else
						{
							KhachHang kh = db.KhachHangs.Find(x.soDT);
							if (x.email == null) x.email = "";
							else if (x.email != null) kh.email = x.email;
							if (x.ghiChu == null) x.ghiChu = "";
							else if (x.ghiChu != null) kh.ghiChu = x.ghiChu;
							db.SaveChanges();
							DonHang d = new DonHang();
							//update order  info to donhang object you have just created before
							d.soDH = string.Format("{0:yyMMddhhmm}", DateTime.Now);
							d.maKH = kh.maKH;
							d.ngayDat = DateTime.Now;
							d.ngayGH = DateTime.Now.AddDays(2);
							d.taiKhoan = "admin";
							d.diaChiGH = x.diaChi;
							d.ghiChu = x.ghiChu;
							d.daKichHoat = false;
							d.trangThai = "";
							//Add mordel info to datamodel
							db.DonHangs.Add(d);
							//save to database
							db.SaveChanges();
							//get list of item from cartshop
							Shopcart gh = Session["GioHang"] as Shopcart;
							// update order info to Donhang object you have just created before
							foreach (CtDonHang i in gh.spChon.Values)
							{
								i.soDH = d.soDH;
								db.CtDonHangs.Add(i);
							}
							//save to database
							db.SaveChanges();
							//finish and commit all
							trans.Commit();
							gh.maKH = kh.maKH;
							gh.maDH = d.soDH;
							Session["GioHang"] = gh;
							return RedirectToAction("CheckoutSuccess", "GioHang");
						}
					}
					catch (Exception e)
					{
						trans.Rollback();
						string s = e.Message;
					}
				}
			}
			return RedirectToAction("Checkout","GioHang");
		}
		public ActionResult CheckoutSuccess()
		{
			Shopcart gh = Session["GioHang"] as Shopcart;
			DonHang x = db.DonHangs.Find(gh.maDH);
			List<CtDonHang> list = DataIn.GetCtDonHangs(gh.maDH);
			ViewData["DH"] = x;
			ViewData["CTDH"] = list;
			ViewData["KH"] = DataIn.GetKhachHang(gh.maKH);
			Session["GioHang"] = new Shopcart();
			return View();
		}
	}
}