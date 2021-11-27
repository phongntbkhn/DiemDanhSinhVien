using QlDiemDanhSinhVien.FwCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace QlDiemDanhSinhVien.Custom.HtmlHelpers
{
    public static class UIHelpers
    {

        public static MvcHtmlString GridRowCount(this HtmlHelper html, WebGrid grid)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class='grid-row-count'>");
            result.AppendLine(string.Format("0 bản ghi", grid.Rows.Count));
            result.AppendLine("</div>");
            return MvcHtmlString.Create(result.ToString());
        }

        public static MvcHtmlString VtAntiCsrfToken(this HtmlHelper html)
        {
            string str = "<input type='hidden' class='time-{1}' name='CsrfToken' value='{0}'/>";
            string token = SessionManager.GenerateCsrfToken();
            return MvcHtmlString.Create(string.Format(str, token, DateTime.Now.Ticks));
        }
    }
}