using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models.DTO
{
    public class LuongDTO
    {
        public string TenNhanVien { get; set; }
        public int Thang { get; set; }
        public decimal? LuongCung { get; set; }
        public decimal? Thuong { get; set; }
        public decimal? TongLuong { get; set; }
    }
}