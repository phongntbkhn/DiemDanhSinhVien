using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Model.DiemDanhSV;
using Model.ViewModel;
using QlDiemDanhSinhVien.Custom;

namespace QlDiemDanhSinhVien.Areas.PhongDaoTao.Controllers
{
    public class TAI_KHOANController : BaseController
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: PhongDaoTao/TAI_KHOAN
        public ActionResult Index()
        {
            string query = @"
                SELECT tk.*
                , q.TenQuyen 
                FROM TAI_KHOAN tk
                LEFT JOIN QUYEN q on tk.IdQuyen = q.Id
                ";

            int resultCode = 0;
            List<TaiKhoanVM> taiKhoanVMs = GetData<TaiKhoanVM>(query, ref resultCode);

            ViewData["lst_tai_khoan"] = taiKhoanVMs;

            return View();
        }

        [HttpGet]
        public ActionResult TimKiem(string ten_dang_nhap, string ten_hien_thi)
        {
            string query = @"
                SELECT tk.*
                , q.TenQuyen 
                FROM TAI_KHOAN tk
                LEFT JOIN QUYEN q on tk.IdQuyen = q.Id";

            int resultCode = 0;
            List<TaiKhoanVM> taiKhoanVMs = GetData<TaiKhoanVM>(query, ref resultCode);
            taiKhoanVMs = taiKhoanVMs.Where(s => s.TenDangNhap.Contains(ten_dang_nhap) && s.TenHienThi.Contains(ten_hien_thi)).ToList();
            ViewBag.Total = taiKhoanVMs.Count;
            ViewData["lst_tai_khoan"] = taiKhoanVMs;

            return PartialView("_DataTaiKhoan");
        }

        // GET: PhongDaoTao/TAI_KHOAN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAI_KHOAN tAI_KHOAN = db.TAI_KHOAN.Find(id);
            if (tAI_KHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAI_KHOAN);
        }

        // GET: PhongDaoTao/TAI_KHOAN/Create
        public ActionResult Create()
        {
            ViewData["lst_quyen"] = getListItemQuyen();
            return View();
        }

        public List<SelectListItem> getListItemQuyen()
        {
            List<QUYEN> qUYENs = db.QUYENs.ToList();
            List<SelectListItem> lstItemQuyen = new List<SelectListItem>();
            var tempLstItem = from s in qUYENs
                              select new SelectListItem()
                              {
                                  Value = s.Id.ToString(),
                                  Text = s.TenQuyen
                              };
            lstItemQuyen = tempLstItem.ToList();
            return lstItemQuyen;
        }

        // POST: PhongDaoTao/TAI_KHOAN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TenDangNhap,MatKhau,TenHienThi,IdQuyen,IsQlPhongDT,IsGiangVien,IsSinhVien,IdQlPhongDT,IdGiangVien,IdSinhVien,NgayTao,NguoiTao,NgaySua,NguoiSua")] TAI_KHOAN tAI_KHOAN)
        {
            if (ModelState.IsValid)
            {
                db.TAI_KHOAN.Add(tAI_KHOAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["lst_quyen"] = getListItemQuyen();
            return View(tAI_KHOAN);
        }

        // GET: PhongDaoTao/TAI_KHOAN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAI_KHOAN tAI_KHOAN = db.TAI_KHOAN.Find(id);
            if (tAI_KHOAN == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> selectListItems = getListItemQuyen();
            selectListItems.Where(s => s.Value == tAI_KHOAN.IdQuyen.ToString()).FirstOrDefault().Selected = true;

            ViewData["lst_quyen"] = getListItemQuyen();
            return View(tAI_KHOAN);
        }

        // POST: PhongDaoTao/TAI_KHOAN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TenDangNhap,MatKhau,TenHienThi,IdQuyen,IsQlPhongDT,IsGiangVien,IsSinhVien,IdQlPhongDT,IdGiangVien,IdSinhVien,NgayTao,NguoiTao,NgaySua,NguoiSua")] TAI_KHOAN tAI_KHOAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAI_KHOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["lst_quyen"] = getListItemQuyen();
            return View(tAI_KHOAN);
        }

        // GET: PhongDaoTao/TAI_KHOAN/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAI_KHOAN tAI_KHOAN = db.TAI_KHOAN.Find(id);
            if (tAI_KHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAI_KHOAN);
        }

        // POST: PhongDaoTao/TAI_KHOAN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TAI_KHOAN tAI_KHOAN = db.TAI_KHOAN.Find(id);
            db.TAI_KHOAN.Remove(tAI_KHOAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
