using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models.DTO
{
    public class XeDTO
    {
        public int IDTN { get; set; }
        public int IDXe { get; set; }
        public int IDTK { get; set; }
        public int? NV_SC { get; set; }//Nhân viên sửa chữa
        public System.DateTime NgayTiepNhan { get; set; }
        public Nullable<int> TrangThai { get; set; }

        public string BienSo { get; set; }
        public int IDHieuXe { get; set; }
        public string TenChuXe { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public decimal TienNo { get; set; }

        public string TenNhanvien { get; set; }
    }
}