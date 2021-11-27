using Model.DiemDanhSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlDiemDanhSinhVien.Models
{
    public class TaiKhoanInfo
    {
        public int id { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string TenHienThi { get; set; }
        public Nullable<int> IdQuyen { get; set; }
        public string TenQuyen { get; set; }
        public Nullable<bool> IsQlPhongDT { get; set; }
        public Nullable<bool> IsGiangVien { get; set; }
        public Nullable<bool> IsSinhVien { get; set; }
        public Nullable<int> IdQlPhongDT { get; set; }
        public Nullable<int> IdGiangVien { get; set; }
        public Nullable<int> IdSinhVien { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<int> NguoiTao { get; set; }
        public Nullable<System.DateTime> NgaySua { get; set; }
        public Nullable<int> NguoiSua { get; set; }

        public List<QUYEN_CHUCNANG>  lsChucNang { get; set; }
    }
}