﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.DiemDanhSV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class QL_PHONG_DT
    {
        [Display(Name = "ID")]
        public int id { get; set; }

        [Required(ErrorMessage = "Họ tên là trường bắt buộc")]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }


        [Required(ErrorMessage = "Ngày sinh là trường bắt buộc")]
        [Display(Name = "Ngày sinh")]
        public Nullable<System.DateTime> NgaySinh { get; set; }

        [Required(ErrorMessage = "Quê quán là trường bắt buộc")]
        [Display(Name = "Quê quán")]
        public string QueQuan { get; set; }

        [Display(Name = "Địa chỉ hiện tại")]
        public string DiaChiHienTai { get; set; }

        [Required(ErrorMessage = "Giới tính là trường bắt buộc")]
        [Display(Name = "Giới tính")]
        public Nullable<int> GioiTinh { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> NgayTao { get; set; }

        [Display(Name = "Người tạo")]
        public Nullable<int> NguoiTao { get; set; }

        [Display(Name = "Ngày sửa")]
        public Nullable<System.DateTime> NgaySua { get; set; }

        [Display(Name = "Người sửa")]
        public Nullable<int> NguoiSua { get; set; }
    }
}