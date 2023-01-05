using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyGaraOto.Models.DTO
{
    public class CTPhieuNhapDTO
    {
        public int IDPN { get; set; }
        public int IDVTPT { get; set; }
        public string TenVTPT { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
    }
}