using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models.DTO
{
    public class PhieuSuaChuaDTO
    {
        public int IDCTPSC { get; set; }
        public int IDPSC { get; set; }
        public int IDVTPT { get; set; }
        public int IDTienCong { get; set; }
        public int SoLuong { get; set; }
        public int IDXe { get; set; }
        public int IDTN { get; set; }
        public int? IDTK { get; set; }
        public DateTime NgaySuaChua { get; set; }
        public decimal? Luong { get; set; }
    }
}