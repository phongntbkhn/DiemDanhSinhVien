using System.Web;
using System.Web.Mvc;
using Web.FwCore.Factory;

namespace QlDiemDanhSinhVien
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new VtAuthAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new VtExceptionAttribute());
        }
    }
}
