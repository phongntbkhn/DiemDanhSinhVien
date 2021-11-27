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

namespace QlDiemDanhSinhVien.Areas.GiangVien.Controllers
{
    public class DiemDanhController : BaseController
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: GiangVien/DiemDanh
        public ActionResult Index()
        {
            return View(db.DIEM_DANH.ToList());
        }

        // GET: GiangVien/DiemDanh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEM_DANH dIEM_DANH = db.DIEM_DANH.Find(id);
            if (dIEM_DANH == null)
            {
                return HttpNotFound();
            }
            return View(dIEM_DANH);
        }

        // GET: GiangVien/DiemDanh/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GiangVien/DiemDanh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,IdSinhVien,IdLopMon,SoNgayHoc,NgayHoc,TrangThaiHoc,NgayTao,NguoiTao,NgaySua,NguoiSua")] DIEM_DANH dIEM_DANH)
        {
            if (ModelState.IsValid)
            {
                db.DIEM_DANH.Add(dIEM_DANH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dIEM_DANH);
        }

        // GET: GiangVien/DiemDanh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEM_DANH dIEM_DANH = db.DIEM_DANH.Find(id);
            if (dIEM_DANH == null)
            {
                return HttpNotFound();
            }
            return View(dIEM_DANH);
        }

        // POST: GiangVien/DiemDanh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,IdSinhVien,IdLopMon,SoNgayHoc,NgayHoc,TrangThaiHoc,NgayTao,NguoiTao,NgaySua,NguoiSua")] DIEM_DANH dIEM_DANH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIEM_DANH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dIEM_DANH);
        }

        // GET: GiangVien/DiemDanh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEM_DANH dIEM_DANH = db.DIEM_DANH.Find(id);
            if (dIEM_DANH == null)
            {
                return HttpNotFound();
            }
            return View(dIEM_DANH);
        }

        // POST: GiangVien/DiemDanh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DIEM_DANH dIEM_DANH = db.DIEM_DANH.Find(id);
            db.DIEM_DANH.Remove(dIEM_DANH);
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

        // GET: GiangVien/DiemDanh/Create
        public ActionResult CreateAll()
        {
            List<SINH_VIEN> sINH_VIENs = db.SINH_VIEN.ToList();

            List<InsertMultipleHocSinhLopMon> insertMultipleHocSinhLopMons = new List<InsertMultipleHocSinhLopMon>();
            var temp = from s in sINH_VIENs
                       select new InsertMultipleHocSinhLopMon()
                       {
                           id = s.id,
                           HoTen = s.HoTen,
                           ischeck = false
                       };
            insertMultipleHocSinhLopMons = temp.ToList();
            return View(insertMultipleHocSinhLopMons);
        }

        // POST: GiangVien/DiemDanh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateAll(List<InsertMultipleHocSinhLopMon> lst, string lopmon)
        {

            return View();
        }

        public List<SelectListItem> getListSinhVien()
        {
            List<SelectListItem> selectListItemSinhViens = new List<SelectListItem>();

            List<SINH_VIEN> sinhViens = new List<SINH_VIEN>();

            sinhViens = db.SINH_VIEN.ToList();

            var tempSelectListItemSinhViens = from s in sinhViens
                                              select new SelectListItem()
                                              {
                                                  Value = s.id.ToString(),
                                                  Text = s.HoTen
                                              };
            selectListItemSinhViens = tempSelectListItemSinhViens.ToList();
            return selectListItemSinhViens;
        }

        public List<SelectListItem> getListLopMon()
        {
            List<SelectListItem> selectListItemSinhViens = new List<SelectListItem>();

            string sql = @"SELECT lm.id
                        , CONCAT(l.TenLop,' - ', mh.TenMonHoc) As TenLopTenMon
                        FROM LOP_MON lm
                        INNER JOIN LOP l on lm.IdLop = l.Id
                        INNER JOIN DM_MON_HOC mh on mh.id = lm.IdMonHoc";

            int returnCode = 0;
            List<CustomLopMon> customLopMons = GetData<CustomLopMon>(sql, ref returnCode);

            var tempSelectListItemSinhViens = from s in customLopMons
                                              select new SelectListItem()
                                              {
                                                  Value = s.id.ToString(),
                                                  Text = s.TenLopTenMon
                                              };
            selectListItemSinhViens = tempSelectListItemSinhViens.ToList();
            return selectListItemSinhViens;
        }

        // GET: GiangVien/DiemDanh/Create
        public ActionResult CreateDiemDanhBySinhVienByLopMonByNgay()
        {
            ViewData["lst_sinhvien"] = getListSinhVien();
            ViewData["lst_lopmon"] = getListLopMon();

            return View();
        }

        // POST: GiangVien/DiemDanh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateDiemDanhBySinhVienByLopMonByNgay(int? IdSinhVien, int? IdLopMon, int? SoNgayHoc, int? TrangThaiHoc)
        {
            if (ModelState.IsValid)
            {
                DIEM_DANH dIEM_DANH = new DIEM_DANH();
                dIEM_DANH = db.DIEM_DANH.Where(s => s.IdSinhVien == IdSinhVien && s.IdLopMon == IdLopMon && s.SoNgayHoc == SoNgayHoc).FirstOrDefault();
                if (dIEM_DANH == null)
                {
                    DIEM_DANH dd = new DIEM_DANH();
                    dd.IdSinhVien = IdSinhVien;
                    dd.IdLopMon = IdLopMon;
                    dd.SoNgayHoc = SoNgayHoc;
                    dd.NgayHoc = DateTime.Now;
                    dd.TrangThaiHoc = TrangThaiHoc;
                    db.DIEM_DANH.Add(dIEM_DANH);
                }
                else
                {
                    dIEM_DANH.NgayHoc = DateTime.Now;
                    dIEM_DANH.TrangThaiHoc = TrangThaiHoc;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["lst_sinhvien"] = getListSinhVien();
            ViewData["lst_lopmon"] = getListLopMon();
            return View();
        }
    }
}
