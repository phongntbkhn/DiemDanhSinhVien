using Model.DiemDanhSV;
using QlDiemDanhSinhVien.FwCore;
using QlDiemDanhSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace QlDiemDanhSinhVien.Custom
{
    public class Authorize
    {
        public static bool ValidateRequest(HttpContextBase httpContext)
        {
            // Write Authorize code here
            TaiKhoanInfo UserInfo = (TaiKhoanInfo)SessionManager.GetUserInfo();
            if (UserInfo == null)
            {
                FormsAuthentication.SignOut();
                SessionManager.Clear();
                httpContext.Response.Redirect("/Account/Login");
                return true;
            }
            string username = httpContext.User.Identity.Name;

            var rd = httpContext.Request.RequestContext.RouteData;

            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            string currentArea = rd.DataTokens["area"] as string;

            string ActionPath = string.Format("/{0}/{1}/{2}", currentArea, currentController, currentAction);
            string ActionPathShort = string.Format("/{0}/{1}", currentArea, currentController);

            TaiKhoanInfo userinfo = (TaiKhoanInfo)SessionManager.GetUserInfo();

            if (ActionPath == "//Home/Index" ||
                ActionPath == "//Home/UnAuthor" ||
                ActionPathShort == "//AttachFile" ||
                ActionPath == "//Account/ChangePassword")
            {
                return true;
            }
            else
            {
                QUYEN_CHUCNANG qUYEN_CHUCNANG = new QUYEN_CHUCNANG();
                List<QUYEN_CHUCNANG> lstChucNang = UserInfo.lsChucNang;
                qUYEN_CHUCNANG = lstChucNang.Where(o => o.Url.ToLower() == ActionPath.ToLower()).FirstOrDefault();
                if (qUYEN_CHUCNANG != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}