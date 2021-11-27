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
    public class GiangVienController : Controller
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: PhongDaoTao/GiangVien
        public ActionResult Index()
        {
            return View(db.GIANG_VIEN.ToList());
        }

        // GET: PhongDaoTao/GiangVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANG_VIEN gIANG_VIEN = db.GIANG_VIEN.Find(id);
            if (gIANG_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIANG_VIEN);
        }

        // GET: PhongDaoTao/GiangVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongDaoTao/GiangVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,HoTen,NgaySinh,QueQuan,DiaChiHienTai,GioiTinh,SoDienThoai,NgayTao,NguoiTao,NgaySua,NguoiSua")] GIANG_VIEN gIANG_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.GIANG_VIEN.Add(gIANG_VIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gIANG_VIEN);
        }

        // GET: PhongDaoTao/GiangVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANG_VIEN gIANG_VIEN = db.GIANG_VIEN.Find(id);
            if (gIANG_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIANG_VIEN);
        }

        // POST: PhongDaoTao/GiangVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,HoTen,NgaySinh,QueQuan,DiaChiHienTai,GioiTinh,SoDienThoai,NgayTao,NguoiTao,NgaySua,NguoiSua")] GIANG_VIEN gIANG_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIANG_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gIANG_VIEN);
        }

        // GET: PhongDaoTao/GiangVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANG_VIEN gIANG_VIEN = db.GIANG_VIEN.Find(id);
            if (gIANG_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIANG_VIEN);
        }

        // POST: PhongDaoTao/GiangVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GIANG_VIEN gIANG_VIEN = db.GIANG_VIEN.Find(id);
            db.GIANG_VIEN.Remove(gIANG_VIEN);
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
