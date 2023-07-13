using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DoAnLTWeb.Models;
using DoAnLTWeb;


namespace DoAnLTWeb
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
		protected void Session_Start(object sender, EventArgs e)
		{
			Session["DangNhap"] = new TaiKhoan();
			// Tạo giỏ hàng cho người truy cập vào trang web
			Session["GioHang"] = new Shopcart();
		}
	}
}
