using Model.DiemDanhSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QlDiemDanhSinhVien.Areas.GiangVien.Controllers
{
    public class GiangDayController : Controller
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: GiangVien/GiangDay
        public ActionResult Index()
        {
            return View();
        }
    }
}