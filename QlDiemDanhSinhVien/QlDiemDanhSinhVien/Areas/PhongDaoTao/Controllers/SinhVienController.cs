using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.DiemDanhSV;

namespace QlDiemDanhSinhVien.Areas.PhongDaoTao.Controllers
{
    public class SinhVienController : Controller
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: PhongDaoTao/SinhVien
        public ActionResult Index()
        {
            return View(db.SINH_VIEN.ToList());
        }

        // GET: PhongDaoTao/SinhVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINH_VIEN sINH_VIEN = db.SINH_VIEN.Find(id);
            if (sINH_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINH_VIEN);
        }

        // GET: PhongDaoTao/SinhVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongDaoTao/SinhVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,HoTen,NgaySinh,QueQuan,DiaChiHienTai,GioiTinh,SoDienThoai,NgayTao,NguoiTao,NgaySua,NguoiSua")] SINH_VIEN sINH_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.SINH_VIEN.Add(sINH_VIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sINH_VIEN);
        }

        // GET: PhongDaoTao/SinhVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINH_VIEN sINH_VIEN = db.SINH_VIEN.Find(id);
            if (sINH_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINH_VIEN);
        }

        // POST: PhongDaoTao/SinhVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,HoTen,NgaySinh,QueQuan,DiaChiHienTai,GioiTinh,SoDienThoai,NgayTao,NguoiTao,NgaySua,NguoiSua")] SINH_VIEN sINH_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINH_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sINH_VIEN);
        }

        // GET: PhongDaoTao/SinhVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINH_VIEN sINH_VIEN = db.SINH_VIEN.Find(id);
            if (sINH_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINH_VIEN);
        }

        // POST: PhongDaoTao/SinhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SINH_VIEN sINH_VIEN = db.SINH_VIEN.Find(id);
            db.SINH_VIEN.Remove(sINH_VIEN);
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
