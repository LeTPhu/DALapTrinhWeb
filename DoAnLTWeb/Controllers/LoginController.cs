using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
			return View();
        }
		[HttpPost]
		public ActionResult Index(string name,string pass)
		{
			if (name != null )
				foreach (TaiKhoan i in DataIn.GetListTaiKhoans().Where(m=>m.trangThai==true).ToList<TaiKhoan>())
				{
					if (i.taiKhoan1.Equals(name.ToLower().Trim()) && i.matKhau.Equals(pass))
					{
						Session["DangNhap"] = i;
						return RedirectToAction("Dashboard","Dashboard",new {Area = "PrivatePages"});
					}
					else
					{
						ViewBag.mess = "Tên tài khoản hoặc mật khẩu nhập không đúng";
					}
				}
			return View();
		}
		//Logout
		public ActionResult Logout()
		{
			Session.Abandon();//remove session
			return RedirectToAction("Index");
		}

	}
}