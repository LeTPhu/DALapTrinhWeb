using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnLTWeb.Models;

namespace DoAnLTWeb.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
		public ActionResult ShopDetails(string masp)
		{
			Shopcart gh = Session["GioHang"] as Shopcart;
			SanPham sp = DataIn.NhanSP(masp);
			gh.addItem(masp);
			Session["GioHang"] = gh;
			ViewData["sp"] = sp;
			return View();
		}
		public ActionResult ShopGrid()
		{
			return View();
		}
		[HttpPost]
		public ActionResult ShopGrid(int ma)
		{
			List<SanPham> list = DataIn.GetListSPEqualsLoaiSPDaDuyet(ma); 
			ViewData["list"] = list;
			return View();
		}
		//===============================================================================================
		


	}
}