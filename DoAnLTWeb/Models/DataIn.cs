using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DoAnLTWeb.Models
{
	public class DataIn
	{
		static ShopOnlineEntities db = new ShopOnlineEntities();
		/// <summary>
		/// Phương thức nhận danh sách loại sản phẩm
		/// </summary>
		/// <returns></returns>
		public static List<LoaiSP> GetListLoaiSP()
		{
	
			return new ShopOnlineEntities().LoaiSPs.ToList<LoaiSP>();
		}
		/// <summary>
		/// Phương thức nhận danh sách sản phẩm = loại sản phẩm
		/// </summary>
		/// <param name="Ma"></param>
		/// <returns></returns>
		public static List<SanPham> GetListSPEqualsLoaiSP(int Ma)
		{
			return new ShopOnlineEntities().SanPhams.Where(x => x.maLoai==Ma).ToList<SanPham>();
		}
		/// <summary>
		/// Phương thức nhận danh sách sản phẩm theo mã loại đã duyệt
		/// </summary>
		/// <param name="Ma"></param>
		/// <returns></returns>
		public static List<SanPham> GetListSPEqualsLoaiSPDaDuyet(int Ma)
		{
			return DataIn.GetListSPEqualsLoaiSP(Ma).Where(m => m.daDuyet == true).ToList<SanPham>();
		}
		/// <summary>
		/// Phương thức nhận danh sách sản phẩm theo mã loại nhưng chưa duyệt
		/// </summary>
		/// <param name="Ma"></param>
		/// <returns></returns>
		public static List<SanPham> GetListSPEqualsLoaiSPChuaDuyet(int Ma)
		{
			return DataIn.GetListSPEqualsLoaiSP(Ma).Where(m => m.daDuyet == false).ToList<SanPham>();
		}
		/// <summary>
		/// phương thức nhận danh sách sản phẩm
		/// </summary>
		/// <returns></returns>
		public static List<SanPham> GetListSP()
		{
			return new ShopOnlineEntities().SanPhams.OrderByDescending(x =>x.ngayDang).ToList<SanPham>();
		}
		public static List<SanPham> GetListSPChuaDuyet()
		{
			return DataIn.GetListSP().Where(m => m.daDuyet == false).ToList<SanPham>();
		}
		public static List<SanPham> GetListSPDaDuyet()
		{
			return DataIn.GetListSP().Where(m => m.daDuyet == true).ToList<SanPham>();
		}

		/// <summary>
		/// phương thức nhận danh sách tài khoản
		/// </summary>
		/// <returns></returns>
		public static List<TaiKhoan> GetListTaiKhoans()
		{
			return new ShopOnlineEntities().TaiKhoans.ToList<TaiKhoan>();
		}
		/// <summary>
		/// phương thức nhận mã sản phẩm
		/// </summary>
		/// <param name="masp"></param>
		/// <returns></returns>
		public static SanPham NhanSP(string masp)
		{
			return new ShopOnlineEntities().SanPhams.Where(m => m.maSP.Equals(masp) ).FirstOrDefault<SanPham>();
		}
		/// <summary>
		/// phương thức nhận bài viết 
		/// </summary>
		/// <param name="mabv"></param>
		/// <returns></returns>
		public static BaiViet NhanBV(string mabv)
		{
			return new ShopOnlineEntities().BaiViets.Where(m => m.maBV.Equals(mabv)).FirstOrDefault<BaiViet>();
		}
		/// <summary>
		/// Phương thức nhận tất cả đơn hàng
		/// </summary>
		/// <returns></returns>
        public static List<DonHang> NhanFullDonHang()
        {
            return new ShopOnlineEntities().DonHangs.ToList<DonHang>();
        }
		public static List<DonHang> NhanDHDaKichHoat()
		{
			return DataIn.NhanFullDonHang().Where(m=>m.daKichHoat == true).ToList();
		}
		public static List<DonHang> NhanDHChuaKichHoat()
		{
			return DataIn.NhanFullDonHang().Where(m => m.daKichHoat == false).ToList();
		}

		/// <summary>
		/// Phương thức lấy sản phẩm thông qua mã sản phẩm
		/// </summary>
		/// <param name="maSP"></param>
		/// <returns></returns>
		public static SanPham Getproductbyid(string maSP)
		{
			return db.Set<SanPham>().Find(maSP);
		}
		// lay ten dua tren masp
		public static String  getNameOfProductByID(string maSP)
		{
			return db.Set<SanPham>().Find(maSP).tenSP;
		}
		// lay hinh dua tren masp
        public static String getImageOfProductByID(string maSP)
        {
            return db.Set<SanPham>().Find(maSP).hinhDD;
        }
		public static KhachHang GetKhachHang(string maKH)
		{
			return db.KhachHangs.Find(maKH);
		}
		public static DonHang GetDonHang(string maDH)
		{
			return db.DonHangs.Find(maDH);
		}
		public static List<CtDonHang> GetCtDonHangs(string maDH)
		{
			return new ShopOnlineEntities().CtDonHangs.Where(m=>m.soDH.Equals(maDH)).ToList();
		}
		public static SanPham getSPDT(int maloai) 
		{
			return new ShopOnlineEntities().SanPhams.Where(m=>m.maLoai==maloai).First();
		}

	}
}