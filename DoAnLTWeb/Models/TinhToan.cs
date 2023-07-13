using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnLTWeb.Models
{
	public class TinhToan
	{
		public static long tien1sp(string maDH,string masp)
		{
			long sum = 0;
			foreach (CtDonHang i in DataIn.GetCtDonHangs(maDH).Where(m=>m.maSP.Equals(masp)))
				sum += (long)(i.giaBan * i.soLuong -(i.giaBan*i.soLuong*i.giamGia/100));
			return sum;
		}
		public static long tienDaGiamGia(string maDH)
		{
			long tienGiam = 0;
			foreach (CtDonHang i in DataIn.GetCtDonHangs(maDH))
				tienGiam += (long)(i.giaBan * i.soLuong * i.giamGia / 100);
			return tienGiam;
		}
		public static long tongGia(string maDH)
		{
			long kq = 0;
			foreach (CtDonHang i in DataIn.GetCtDonHangs(maDH))
				kq += (long)(i.giaBan * i.soLuong);
			return kq;
		}
		public static long tongTien(string maDH)
		{
			long kq = 0;
			foreach (CtDonHang i in DataIn.GetCtDonHangs(maDH))
				kq += TinhToan.tien1sp(maDH,i.maSP);
			return kq;
		}
		public static int tongSP(string maDH)
		{
			int soLuong = 0;
			foreach (CtDonHang i in DataIn.GetCtDonHangs(maDH))
				soLuong += (int)i.soLuong;
			return soLuong;
		}
		public static int tongSoDH(string makh)
		{
			int soLuong = 0;
			foreach (DonHang i in DataIn.NhanDHDaKichHoat().Where(m => m.maKH.Equals(makh)).ToList())
				soLuong++;
			return soLuong;
		}

	}
}