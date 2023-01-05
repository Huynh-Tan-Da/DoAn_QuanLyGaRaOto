using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models.DTO
{
    public class NhanVienDTO
    {
        public int IDTK { get; set; }
        public string TenDN { get; set; }
        public string MatKhau { get; set; }
        public int IDNV { get; set; }
        public int IDPhanQuyen { get; set; }

        public int ID_NhanVien { get; set; }
        public string Ten { get; set; }
        public string CMND { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public Nullable<decimal> Luong { get; set; }
        public string ChucVu { get; set; }
    }
}