using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlDiemDanhSinhVien.Models
{
    public class CustomDiemDanh
    {
        public int id { get; set; }
        public Nullable<int> IdSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public Nullable<int> IdLopMon { get; set; }
        public Nullable<int> SoNgayHoc { get; set; }
        public Nullable<System.DateTime> NgayHoc { get; set; }
        public Nullable<int> TrangThaiHoc { get; set; }
        public Nullable<int> IdLop { get; set; }
        public Nullable<int> IdMonHoc { get; set; }
        public Nullable<int> SoTinChi { get; set; }
    }
}