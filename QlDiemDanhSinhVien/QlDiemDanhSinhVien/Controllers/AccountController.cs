using Model.DiemDanhSV;
using QlDiemDanhSinhVien.Custom;
using QlDiemDanhSinhVien.FwCore;
using QlDiemDanhSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QlDiemDanhSinhVien.Controllers
{
    public class AccountController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string type)
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string username, string password, string captcha)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewData["Message"] = "Bạn chưa nhập user name";
                return View();
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewData["Message"] = "Bạn chưa nhập mật khẩu";
                return View();
            }

            try
            {
                if (Authenticate.ValidateLogin(username, password))
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    SessionManager.Remove("LoginFailCount");
                    TaiKhoanInfo user = (TaiKhoanInfo)SessionManager.GetUserInfo();
                    if (user != null)
                    {
                        if (user.IdQuyen == 1 || user.IdQuyen == 2)
                        {
                            return RedirectToAction("Index", "TAI_KHOAN", new { area = "PhongDaoTao" });
                        }
                        else if (user.IdQuyen == 3)
                        {
                            return RedirectToAction("Index", "DiemDanh", new { area = "GiangVien" });
                        }
                        else
                        {
                            return RedirectToAction("Index", "QuaTrinhHoc", new { area = "SinhVien" });
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "Không tồn tại tài khoản trên";
                        return View();
                    }
                }
                else
                {
                    ViewData["Message"] = "Không tồn tại tài khoản trên";
                    return View();
                }
            }
            catch
            {
                ViewData["Message"] = "Không tồn tại tài khoản trên";
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegisterLogin()
        {
            if (Request.IsAuthenticated)
            {

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            SessionManager.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView("Password");
        }
    }
}