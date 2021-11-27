using Model.DiemDanhSV;
using QlDiemDanhSinhVien.Custom;
using QlDiemDanhSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QlDiemDanhSinhVien.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            TaiKhoanInfo taiKhoan = (TaiKhoanInfo)GetUserInfo();
            if (taiKhoan != null)
            {
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}