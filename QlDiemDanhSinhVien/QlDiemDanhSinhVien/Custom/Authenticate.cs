using Model.DiemDanhSV;
using QlDiemDanhSinhVien.FwCore;
using QlDiemDanhSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QlDiemDanhSinhVien.Custom
{
    public class Authenticate
    {
        public Authenticate() { }

        public static bool ValidateLogin(string UserName, string Password)
        {
            TAI_KHOAN tAI_KHOAN = new TAI_KHOAN();
            //string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DiemDanhSvDbEntities"].ConnectionString;
            DiemDanhSvDbEntities _context = new DiemDanhSvDbEntities();
            try
            {
                string sql = @"SELECT * 
                        FROM TAI_KHOAN 
                        WHERE TenDangNhap = @UserName 
                        AND MatKhau = @Password";
                List<SqlParameter> lstPara = new List<SqlParameter>();
                lstPara.Add(new SqlParameter("UserName", UserName));
                lstPara.Add(new SqlParameter("Password", Password));

                tAI_KHOAN = _context.Database.SqlQuery<TAI_KHOAN>(sql, lstPara.ToArray()).FirstOrDefault();

                if (tAI_KHOAN != null)
                {
                    TaiKhoanInfo taiKhoanInfo = new TaiKhoanInfo();
                    taiKhoanInfo.id = tAI_KHOAN.id;
                    taiKhoanInfo.TenDangNhap = tAI_KHOAN.TenDangNhap;
                    taiKhoanInfo.MatKhau = tAI_KHOAN.MatKhau;
                    taiKhoanInfo.TenHienThi = tAI_KHOAN.TenHienThi;
                    taiKhoanInfo.IdQuyen = tAI_KHOAN.IdQuyen;
                    if (tAI_KHOAN.IdQuyen != null)
                    {
                        var quyen = _context.QUYENs.FirstOrDefault(s => s.Id == tAI_KHOAN.IdQuyen);
                        if (quyen != null)
                        {
                            taiKhoanInfo.TenQuyen = quyen.TenQuyen;
                        }

                        var lstChucNang = _context.QUYEN_CHUCNANG.Where(s => s.IdQuyen == tAI_KHOAN.IdQuyen).ToList();
                        taiKhoanInfo.lsChucNang = lstChucNang;
                    }
                    taiKhoanInfo.IsQlPhongDT = tAI_KHOAN.IsQlPhongDT;
                    taiKhoanInfo.IsGiangVien = tAI_KHOAN.IsGiangVien;
                    taiKhoanInfo.IsSinhVien = tAI_KHOAN.IsSinhVien;
                    taiKhoanInfo.IdQlPhongDT = tAI_KHOAN.IdQlPhongDT;
                    taiKhoanInfo.IdGiangVien = tAI_KHOAN.IdGiangVien;
                    taiKhoanInfo.IdSinhVien = tAI_KHOAN.IdSinhVien;
                    taiKhoanInfo.NgayTao = tAI_KHOAN.NgayTao;
                    taiKhoanInfo.NguoiTao = tAI_KHOAN.NguoiTao;
                    taiKhoanInfo.NgaySua = tAI_KHOAN.NgaySua;
                    taiKhoanInfo.NguoiSua = tAI_KHOAN.NguoiSua;

                    SessionManager.SetValue(SessionManager.USER_INFO, taiKhoanInfo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static void LoadUserInfo(string UserName)
        {
            DiemDanhSvDbEntities context = new DiemDanhSvDbEntities();
            TAI_KHOAN tAI_KHOAN = new TAI_KHOAN();

            string sql = @"SELECT * 
                        FROM TAI_KHOAN 
                        WHERE TenDangNhap = :UserName";
                List<SqlParameter> lstPara = new List<SqlParameter>();
            lstPara.Add(new SqlParameter("UserName", UserName));
            tAI_KHOAN = context.Database.SqlQuery<TAI_KHOAN>(sql, lstPara.ToArray()).FirstOrDefault();

            if (tAI_KHOAN != null)
            {
                SessionManager.SetValue(SessionManager.USER_INFO, tAI_KHOAN);
            }
        }
    }
}