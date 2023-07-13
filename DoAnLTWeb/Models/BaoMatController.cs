using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTWeb.Models
{
	public class BaoMatController: Controller
	{
		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			TaiKhoan t = Session["DangNhap"] as TaiKhoan;
			
			if (t.taiKhoan1 ==null)
			{
				Response.Redirect(Url.Action("Index", "Login", new { area = "" }),true);
			}
		}
	}
}