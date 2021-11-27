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
    public class QLPhongDTController : Controller
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: PhongDaoTao/QLPhongDT
        public ActionResult Index()
        {
            return View(db.QL_PHONG_DT.ToList());
        }

        // GET: PhongDaoTao/QLPhongDT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QL_PHONG_DT qL_PHONG_DT = db.QL_PHONG_DT.Find(id);
            if (qL_PHONG_DT == null)
            {
                return HttpNotFound();
            }
            return View(qL_PHONG_DT);
        }

        // GET: PhongDaoTao/QLPhongDT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongDaoTao/QLPhongDT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,HoTen,NgaySinh,QueQuan,DiaChiHienTai,GioiTinh,SoDienThoai,NgayTao,NguoiTao,NgaySua,NguoiSua")] QL_PHONG_DT qL_PHONG_DT)
        {
            if (ModelState.IsValid)
            {
                db.QL_PHONG_DT.Add(qL_PHONG_DT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qL_PHONG_DT);
        }

        // GET: PhongDaoTao/QLPhongDT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QL_PHONG_DT qL_PHONG_DT = db.QL_PHONG_DT.Find(id);
            if (qL_PHONG_DT == null)
            {
                return HttpNotFound();
            }
            return View(qL_PHONG_DT);
        }

        // POST: PhongDaoTao/QLPhongDT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,HoTen,NgaySinh,QueQuan,DiaChiHienTai,GioiTinh,SoDienThoai,NgayTao,NguoiTao,NgaySua,NguoiSua")] QL_PHONG_DT qL_PHONG_DT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qL_PHONG_DT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qL_PHONG_DT);
        }

        // GET: PhongDaoTao/QLPhongDT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QL_PHONG_DT qL_PHONG_DT = db.QL_PHONG_DT.Find(id);
            if (qL_PHONG_DT == null)
            {
                return HttpNotFound();
            }
            return View(qL_PHONG_DT);
        }

        // POST: PhongDaoTao/QLPhongDT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QL_PHONG_DT qL_PHONG_DT = db.QL_PHONG_DT.Find(id);
            db.QL_PHONG_DT.Remove(qL_PHONG_DT);
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
