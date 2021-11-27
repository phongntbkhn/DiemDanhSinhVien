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
    public class DmMonHocController : Controller
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: PhongDaoTao/DmMonHoc
        public ActionResult Index()
        {
            return View(db.DM_MON_HOC.ToList());
        }

        // GET: PhongDaoTao/DmMonHoc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_MON_HOC dM_MON_HOC = db.DM_MON_HOC.Find(id);
            if (dM_MON_HOC == null)
            {
                return HttpNotFound();
            }
            return View(dM_MON_HOC);
        }

        // GET: PhongDaoTao/DmMonHoc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongDaoTao/DmMonHoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TenMonHoc,SoTinChi")] DM_MON_HOC dM_MON_HOC)
        {
            if (ModelState.IsValid)
            {
                db.DM_MON_HOC.Add(dM_MON_HOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dM_MON_HOC);
        }

        // GET: PhongDaoTao/DmMonHoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_MON_HOC dM_MON_HOC = db.DM_MON_HOC.Find(id);
            if (dM_MON_HOC == null)
            {
                return HttpNotFound();
            }
            return View(dM_MON_HOC);
        }

        // POST: PhongDaoTao/DmMonHoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TenMonHoc,SoTinChi")] DM_MON_HOC dM_MON_HOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dM_MON_HOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dM_MON_HOC);
        }

        // GET: PhongDaoTao/DmMonHoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DM_MON_HOC dM_MON_HOC = db.DM_MON_HOC.Find(id);
            if (dM_MON_HOC == null)
            {
                return HttpNotFound();
            }
            return View(dM_MON_HOC);
        }

        // POST: PhongDaoTao/DmMonHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DM_MON_HOC dM_MON_HOC = db.DM_MON_HOC.Find(id);
            db.DM_MON_HOC.Remove(dM_MON_HOC);
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
