using Model.DiemDanhSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlDiemDanhSinhVien.Models
{
    public class LopMonVM : LOP_MON
    {
        public string TenLop { get; set; }

        public string TenMonHoc { get; set; }

        public string TenGiangVien { get; set; }
    }
}