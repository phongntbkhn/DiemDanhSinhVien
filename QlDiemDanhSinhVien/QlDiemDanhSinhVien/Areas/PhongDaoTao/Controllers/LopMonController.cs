using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.DiemDanhSV;
using QlDiemDanhSinhVien.Custom;
using QlDiemDanhSinhVien.Models;

namespace QlDiemDanhSinhVien.Areas.PhongDaoTao.Controllers
{
    public class LopMonController : BaseController
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: PhongDaoTao/LopMon
        public ActionResult Index()
        {
            string sql = @"SELECT lm.*
                        , l.TenLop
                        , mh.TenMonHoc
                        , gv.HoTen AS TenGiangVien
                        FROM LOP_MON lm
                        INNER JOIN LOP l on l.Id = lm.IdLop
                        INNER JOIN DM_MON_HOC mh on mh.id = lm.IdMonHoc
                        INNER JOIN GIANG_VIEN gv on gv.id = lm.IdGiangVien";
            int returnCode = 0;
            List<LopMonVM> lopMonVMs = GetData<LopMonVM>(sql, ref returnCode);

            return View(lopMonVMs);
        }

        // GET: PhongDaoTao/LopMon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_MON lOP_MON = db.LOP_MON.Find(id);
            if (lOP_MON == null)
            {
                return HttpNotFound();
            }
            return View(lOP_MON);
        }

        // GET: PhongDaoTao/LopMon/Create
        public ActionResult Create()
        {
            ViewData["lst_lop"] = getListLop();
            ViewData["lst_dm_mon_hoc"] = getListDmMonHoc();
            ViewData["lst_giang_vien"] = getListGiangVien();
            return View();
        }

        // POST: PhongDaoTao/LopMon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,IdLop,IdMonHoc,IdGiangVien,PhongHoc,NgayTao,NguoiTao,NgaySua,NguoiSua")] LOP_MON lOP_MON)
        {
            if (ModelState.IsValid)
            {
                db.LOP_MON.Add(lOP_MON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["lst_lop"] = getListLop();
            ViewData["lst_dm_mon_hoc"] = getListDmMonHoc();
            ViewData["lst_giang_vien"] = getListGiangVien();

            return View(lOP_MON);
        }

        public List<SelectListItem> getListLop()
        {
            List<SelectListItem> selectListItemLops = new List<SelectListItem>();
            List<LOP> lOPs = new List<LOP>();
            lOPs = db.LOPs.ToList();

            var tempSelectItemList = from s in lOPs
                                     select new SelectListItem()
                                     {
                                         Value = s.Id.ToString(),
                                         Text = s.TenLop
                                     };
            selectListItemLops = tempSelectItemList.ToList();
            return selectListItemLops;
        }

        public List<SelectListItem> getListDmMonHoc()
        {
            List<SelectListItem> selectListItemDmMonHocs = new List<SelectListItem>();
            List<DM_MON_HOC> dmMonHocs = new List<DM_MON_HOC>();
            dmMonHocs = db.DM_MON_HOC.ToList();

            var tempSelectItemList = from s in dmMonHocs
                                     select new SelectListItem()
                                     {
                                         Value = s.id.ToString(),
                                         Text = s.TenMonHoc
                                     };
            selectListItemDmMonHocs = tempSelectItemList.ToList();
            return selectListItemDmMonHocs;
        }

        public List<SelectListItem> getListGiangVien()
        {
            List<SelectListItem> selectListItemGiangViens = new List<SelectListItem>();
            List<GIANG_VIEN> gIANG_VIENs = new List<GIANG_VIEN>();
            gIANG_VIENs = db.GIANG_VIEN.ToList();

            var tempSelectItemList = from s in gIANG_VIENs
                                     select new SelectListItem()
                                     {
                                         Value = s.id.ToString(),
                                         Text = s.HoTen
                                     };
            selectListItemGiangViens = tempSelectItemList.ToList();
            return selectListItemGiangViens;
        }

        // GET: PhongDaoTao/LopMon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_MON lOP_MON = db.LOP_MON.Find(id);
            if (lOP_MON == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> selectListItemLops = getListDmMonHoc();
            selectListItemLops.Where(s => s.Value == lOP_MON.IdLop.ToString()).FirstOrDefault().Selected = true;
            ViewData["lst_lop"] = selectListItemLops;

            List<SelectListItem> selectListItemDmMonHocs = getListDmMonHoc();
            selectListItemDmMonHocs.Where(s => s.Value == lOP_MON.IdMonHoc.ToString()).FirstOrDefault().Selected = true;
            ViewData["lst_dm_mon_hoc"] = selectListItemDmMonHocs;

            List<SelectListItem> selectListItemGiangViens = getListGiangVien();
            selectListItemDmMonHocs.Where(s => s.Value == lOP_MON.IdGiangVien.ToString()).FirstOrDefault().Selected = true;
            ViewData["lst_giang_vien"] = selectListItemGiangViens;

            return View(lOP_MON);
        }

        // POST: PhongDaoTao/LopMon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,IdLop,IdMonHoc,IdGiangVien,PhongHoc,NgayTao,NguoiTao,NgaySua,NguoiSua")] LOP_MON lOP_MON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOP_MON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOP_MON);
        }

        // GET: PhongDaoTao/LopMon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP_MON lOP_MON = db.LOP_MON.Find(id);
            if (lOP_MON == null)
            {
                return HttpNotFound();
            }
            return View(lOP_MON);
        }

        // POST: PhongDaoTao/LopMon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOP_MON lOP_MON = db.LOP_MON.Find(id);
            db.LOP_MON.Remove(lOP_MON);
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
