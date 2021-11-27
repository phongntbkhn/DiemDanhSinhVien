using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Model.DiemDanhSV;
using QlDiemDanhSinhVien.Custom;
using QlDiemDanhSinhVien.Models;

namespace QlDiemDanhSinhVien.Areas.PhongDaoTao.Controllers
{
    public class SinhVienLopMonController : BaseController
    {
        private DiemDanhSvDbEntities db = new DiemDanhSvDbEntities();

        // GET: PhongDaoTao/SinhVienLopMon
        public ActionResult Index()
        {
            string sql = @"
                        SELECT svlm.*
                        , l.TenLop
                        , mh.TenMonHoc
                        , lm.PhongHoc
                        , sv.HoTen as TenSinhVien
                        , gv.HoTen as TenGiangVien
                        FROM SINH_VIEN_LOP_MON svlm
                        INNER JOIN LOP_MON lm on lm.id = svlm.IdLopMon
                        INNER JOIN SINH_VIEN sv on sv.id = svlm.IdSinhVien
                        INNER JOIN LOP l on l.Id = lm.IdLop
                        INNER JOIN DM_MON_HOC mh on mh.id = lm.IdMonHoc
                        INNER JOIN GIANG_VIEN gv on gv.id = lm.IdGiangVien
                        ";

            int returnCode = 0;
            List<SinhVienLopMonVM> sinhVienLopMonVMs = GetData<SinhVienLopMonVM>(sql, ref returnCode); 
            return View(sinhVienLopMonVMs);
        }

        // GET: PhongDaoTao/SinhVienLopMon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINH_VIEN_LOP_MON sINH_VIEN_LOP_MON = db.SINH_VIEN_LOP_MON.Find(id);
            if (sINH_VIEN_LOP_MON == null)
            {
                return HttpNotFound();
            }
            return View(sINH_VIEN_LOP_MON);
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

        // GET: PhongDaoTao/SinhVienLopMon/Create
        public ActionResult Create()
        {
            ViewData["lst_sinhvien"] = getListSinhVien();
            ViewData["lst_lopmon"] = getListLopMon();
            return View();
        }

        // POST: PhongDaoTao/SinhVienLopMon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdSinhVien,IdLopMon,NgayTao,NguoiTao,NgaySua,NguoiSua")] SINH_VIEN_LOP_MON sINH_VIEN_LOP_MON)
        {
            if (ModelState.IsValid)
            {
                db.SINH_VIEN_LOP_MON.Add(sINH_VIEN_LOP_MON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["lst_sinhvien"] = getListSinhVien();
            ViewData["lst_lopmon"] = getListLopMon();
            return View(sINH_VIEN_LOP_MON);
        }

        // GET: PhongDaoTao/SinhVienLopMon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINH_VIEN_LOP_MON sINH_VIEN_LOP_MON = db.SINH_VIEN_LOP_MON.Find(id);
            if (sINH_VIEN_LOP_MON == null)
            {
                return HttpNotFound();
            }
            return View(sINH_VIEN_LOP_MON);
        }

        // POST: PhongDaoTao/SinhVienLopMon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdSinhVien,IdLopMon,NgayTao,NguoiTao,NgaySua,NguoiSua")] SINH_VIEN_LOP_MON sINH_VIEN_LOP_MON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINH_VIEN_LOP_MON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sINH_VIEN_LOP_MON);
        }

        // GET: PhongDaoTao/SinhVienLopMon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINH_VIEN_LOP_MON sINH_VIEN_LOP_MON = db.SINH_VIEN_LOP_MON.Find(id);
            if (sINH_VIEN_LOP_MON == null)
            {
                return HttpNotFound();
            }
            return View(sINH_VIEN_LOP_MON);
        }

        // POST: PhongDaoTao/SinhVienLopMon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SINH_VIEN_LOP_MON sINH_VIEN_LOP_MON = db.SINH_VIEN_LOP_MON.Find(id);
            db.SINH_VIEN_LOP_MON.Remove(sINH_VIEN_LOP_MON);
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

            ViewData["lst_lopmon"] = getListLopMon();

            return View(insertMultipleHocSinhLopMons);
        }

        // POST: GiangVien/DiemDanh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateAll(List<InsertMultipleHocSinhLopMon> lst, int id_lop_mon)
        {
            try
            {
                foreach (var item in lst)
                {

                    if (item.ischeck == true & id_lop_mon > 0)
                    {
                        SINH_VIEN_LOP_MON sINH_VIEN_LOP_MON = new SINH_VIEN_LOP_MON();
                        sINH_VIEN_LOP_MON.IdSinhVien = item.id;
                        sINH_VIEN_LOP_MON.IdLopMon = id_lop_mon;

                        var ddanh = db.DIEM_DANH.Where(s => s.IdSinhVien == item.id && s.IdLopMon == id_lop_mon).FirstOrDefault();
                        if (ddanh == null)
                        {
                            var temp = from lm in db.LOP_MON.ToList()
                                       join mh in db.DM_MON_HOC.ToList() on lm.IdMonHoc equals mh.id
                                       where lm.id == id_lop_mon
                                       select mh;

                            InsertEmptyDiemDanh(item.id, id_lop_mon, temp.FirstOrDefault().SoTinChi.Value);
                        }


                        db.SINH_VIEN_LOP_MON.Add(sINH_VIEN_LOP_MON);
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

            }
            return View();
        }

        public void InsertEmptyDiemDanh(int id_sinh_vien, int id_lop_mon, int so_tin_chi)
        {
            // Do 1 tín chỉ = 15 tiết, 1 buổi học = 5 tiết
            int soNgayHoc = (so_tin_chi * 15 / 3);
            for (int i = 1; i <= soNgayHoc; i++)
            {
                DIEM_DANH dIEM_DANH = new DIEM_DANH();
                dIEM_DANH.IdSinhVien = id_sinh_vien;
                dIEM_DANH.IdLopMon = id_lop_mon;
                dIEM_DANH.SoNgayHoc = i;
                dIEM_DANH.TrangThaiHoc = 0;

                db.DIEM_DANH.Add(dIEM_DANH);
            }
        }

        public ActionResult CapNhatSinhVienLopMon()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TimKiemHocSinhLopMonByLopMon(int id_lop_mon)
        {
            List<SinhVienLopMonVM> sinhVienLopMonVMs = new List<SinhVienLopMonVM>();
            string sql = @"
                        SELECT sv.id
                        , sv.HoTen
                        , svlm.IdLopMon
                        FROM SINH_VIEN sv
                        LEFT JOIN SINH_VIEN_LOP_MON svlm on svlm.IdSinhVien = sv.id and svlm.IdLopMon = @id_lop_mon
                        ";
            int returnCode = 0;
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            var param = new DynamicParameters();
            param.Add("@id_lop_mon", id_lop_mon);

            sinhVienLopMonVMs = GetData<SinhVienLopMonVM>(sql, ref returnCode, param);

            ViewData["sinhVienLopMonVMs"] = sinhVienLopMonVMs;
            return Json(new
            {
                data = sinhVienLopMonVMs.Count == 0 ? new SinhVienLopMonVM() : sinhVienLopMonVMs.ToList()[0],
                html = RenderToString(PartialView("_DataHocBaDienTu")),
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DanhSachLopMon()
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

            ViewBag.Total = lopMonVMs.Count;
            ViewData["list_lop_mon"] = lopMonVMs;
            return View();
        }

        public List<LopMonVM> getListLopMonVM()
        {
            List<LopMonVM> lst = new List<LopMonVM>();

            return lst;
        }

        [HttpGet]
        public ActionResult TimKiemLopMon()
        {
            List<LopMonVM> lst = new List<LopMonVM>();
            lst = getListLopMonVM();

            ViewBag.Total = lst.Count;
            ViewData["TraCuuHocBaDienTu"] = lst;

            return Json(new
            {
                data = lst.Count == 0 ? new LopMonVM() : lst.ToList()[0],
                html = RenderToString(PartialView("_DataHocBaDienTu")),
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
