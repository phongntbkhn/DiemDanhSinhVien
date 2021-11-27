using Model.DiemDanhSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlDiemDanhSinhVien.Models
{
    public class SinhVienLopMonVM : SINH_VIEN_LOP_MON
    {
        public string TenLop { get; set; }
        public string TenMonHoc { get; set; }
        public string PhongHoc { get; set; }
        public string TenSinhVien { get; set; }
        public string TenGiangVien { get; set; }
    }
}