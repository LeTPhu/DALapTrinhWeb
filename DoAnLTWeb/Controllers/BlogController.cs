using DoAnLTWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTWeb.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Blog()
        {
            return View();
        }
		public ActionResult BlogDetails(string mabv)
		{
			BaiViet bv = DataIn.NhanBV(mabv);
			ViewData["bv"] = bv;
			return View();
		}
	}
}