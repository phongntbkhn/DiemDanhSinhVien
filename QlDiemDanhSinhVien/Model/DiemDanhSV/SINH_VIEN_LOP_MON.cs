//------------------------------------------------------------------------------
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

    public partial class SINH_VIEN_LOP_MON
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ID sinh viên là trường bắt buộc")]
        [Display(Name = "ID sinh viên")]
        public Nullable<int> IdSinhVien { get; set; }

        [Required(ErrorMessage = "ID lớp môn là trường bắt buộc")]
        [Display(Name = "ID lớp môn")]
        public Nullable<int> IdLopMon { get; set; }

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
