using QlDiemDanhSinhVien.Custom;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.FwCore.Factory
{
    public class VtAuthAttribute : AuthorizeAttribute
    {
        private bool baseValidate;
        private bool customValidate;
        public const string TOKEN_KEY = "AuthenticationToken";

        public VtAuthAttribute():base()
        {
            
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            baseValidate = base.AuthorizeCore(httpContext);

            customValidate = Authorize.ValidateRequest(httpContext);
            return baseValidate && customValidate;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (baseValidate && !customValidate)
            {
                customValidate = true;
                throw new Exception("Bạn không có quyền truy cập chức năng này!");
            }
        }
    }
}