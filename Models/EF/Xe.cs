//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyGaraOto.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Xe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Xe()
        {
            this.PhieuSuaChuas = new HashSet<PhieuSuaChua>();
            this.PhieuThuTiens = new HashSet<PhieuThuTien>();
            this.TiepNhanXes = new HashSet<TiepNhanXe>();
        }
    
        public int IDXe { get; set; }
        public string BienSo { get; set; }
        public int IDHieuXe { get; set; }
        public string TenChuXe { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public decimal TienNo { get; set; }
    
        public virtual HieuXe HieuXe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuSuaChua> PhieuSuaChuas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuThuTien> PhieuThuTiens { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TiepNhanXe> TiepNhanXes { get; set; }
    }
}
