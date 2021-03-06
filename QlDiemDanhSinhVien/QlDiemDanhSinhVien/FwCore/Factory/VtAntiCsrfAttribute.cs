using QlDiemDanhSinhVien.FwCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.FwCore.Factory
{
    public class VtAntiCsrfAttribute : AuthorizeAttribute
    {
        
        public const string TOKEN_KEY = "AuthenticationToken";

        public VtAntiCsrfAttribute()
            : base()
        {
            
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string token = filterContext.HttpContext.Request["CsrfToken"];
            
            string[] arr = token.Split(',');
            token = arr.Last().Trim();

            bool valid = SessionManager.ValidateCsrfToken(token);
        }
    }
}