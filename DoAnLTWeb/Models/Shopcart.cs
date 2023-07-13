using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace DoAnLTWeb.Models
{
    public class Shopcart
    {
        public string maKH { get; set; }
		public string maDH { get; set; }
		public string taiKhoan { get; set; }
        public SortedList<string, CtDonHang> spChon { get; set; }
        public Shopcart()
        {
            this.maKH = "";
            this.taiKhoan = "";
            this.spChon = new SortedList<string, CtDonHang>();
        }
       //---neu ko co san pham chon mua thi tra ve true
        public Boolean IsEmpty()
        {
            return (spChon.Keys.Count==0);
        }
        //--add san pham
        public void addItem(string maSP)
        {
            if (spChon.Keys.Contains(maSP))
            {
                //Lấy sản phẩm trong giỏ hàng
                CtDonHang x = spChon.Values[spChon.IndexOfKey(maSP)];
                x.soLuong++;
            }
            else
            {
                //---Tạo chi tiết đơn hàng mới
                CtDonHang i = new CtDonHang();
                //--cap nhat thong tin cho doi tuong 
                i.maSP = maSP;
                i.soLuong = 1;
                // lay gia ban ,giam gia tu database
                SanPham z = DataIn.Getproductbyid(maSP);
                i.giaBan = z.giaBan;
                i.giamGia = z.giamGia;
                // nem vao gio hang
                spChon.Add(maSP, i);
            }
        }
   
        // giam so luong hoac xoa san pham
        public void deleteItem(string maSP)
        {
            if (spChon.Keys.Contains(maSP))
                spChon.Remove(maSP);
        }
        public void GiamSP(string maSP)
        {
            if (spChon.Keys.Contains(maSP))
            {
                CtDonHang x = spChon.Values[spChon.IndexOfKey(maSP)];
                if (x.soLuong > 1)
                {
                    x.soLuong--;
					//spChon.Values[spChon.IndexOfKey(maSP)].soLuong = x.soLuong;
				}
				else
                    deleteItem(maSP);
            }
        }
		// Tính tiền 1 sản phẩm
		public long tien1Sp(CtDonHang x)
        {
            return (long)(x.giaBan * x.soLuong - (x.giaBan * x.soLuong * x.giamGia/100));
        }
		// Tính số tiền đã giảm giá
		public long tienDaGiamGia()
        {
            long tienGiam = 0;
            foreach (CtDonHang i in spChon.Values)
                tienGiam += (long) (i.giaBan * i.soLuong * i.giamGia / 100);
            return tienGiam;
		}
		public long tongGia()
		{
			long kq = 0;
			foreach (CtDonHang i in spChon.Values)
				kq +=(long) (i.giaBan*i.soLuong);
			return kq;
		}
		public long tongTien()
        {
            long kq = 0;
            foreach (CtDonHang i in spChon.Values)
                kq += tien1Sp(i);
            return kq;
        }
        public int tongSP()
        {
            int soLuong = 0;
            foreach (CtDonHang i in spChon.Values)
                soLuong +=(int) i.soLuong;
            return soLuong;
		}

    }
}