using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Sockets;
using QlDiemDanhSinhVien.FwCore;
using QlDiemDanhSinhVien.Common;

namespace Web.FwCore.Factory
{
    public class VtExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.Message.Equals("Bạn không có quyền truy cập chức năng này!"))
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Redirect("/401.html", true);
            }
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ipLoacal = "";
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipLoacal = ip.ToString();
                }
            }
            return ipLoacal;
        }
    }
}