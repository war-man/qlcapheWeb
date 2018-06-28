﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;

    public partial class datBanOnline
    {
        public datBanOnline()
        {
            this.ctDatBans = new HashSet<ctDatBan>();
        }

        public int maDatBan { get; set; }
        [Required(ErrorMessage = "*Vui lòng nhập họ và tên")]
        public string hoTenKH { get; set; }

        [Required(ErrorMessage = "*Vui lòng nhập số điện thoại")]
        public string SDT { get; set; }

        [Range(1, 9999, ErrorMessage = "Vui lòng nhập số lượng chính xác")]
        public int soLuongKhach { get; set; }
        public Nullable<System.DateTime> ngayDat { get; set; }

        [Required(ErrorMessage = "*Vui lòng nhập ngày đến")]
        public Nullable<System.DateTime> ngayDenDuKien { get; set; }
        public Nullable<int> trangThai { get; set; }
        public string yeuCauThem { get; set; }

        public virtual ICollection<ctDatBan> ctDatBans { get; set; }
    }
}