//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace qlCaPhe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class thanhVien
    {
        public thanhVien()
        {
            this.danhGiaNhanViens = new HashSet<danhGiaNhanVien>();
            this.taiKhoans = new HashSet<taiKhoan>();
        }
    
        public int maTV { get; set; }
        public string hoTV { get; set; }
        public string tenTV { get; set; }
        public Nullable<bool> gioiTinh { get; set; }
        public Nullable<System.DateTime> ngaySinh { get; set; }
        public string noiSinh { get; set; }
        public string diaChi { get; set; }
        public string soDT { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string soCMND { get; set; }
        public Nullable<System.DateTime> ngayCap { get; set; }
        public string noiCap { get; set; }
        public byte[] hinhDD { get; set; }
        public Nullable<System.DateTime> ngayThamGia { get; set; }
        public string ghiChu { get; set; }
    
        public virtual ICollection<danhGiaNhanVien> danhGiaNhanViens { get; set; }
        public virtual ICollection<taiKhoan> taiKhoans { get; set; }
    }
}
