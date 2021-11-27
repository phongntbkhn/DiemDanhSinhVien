using Dapper;
using Model.DiemDanhSV;
using QlDiemDanhSinhVien.FwCore;
using QlDiemDanhSinhVien.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace QlDiemDanhSinhVien.Custom
{
    public class BaseController : Controller
    {
        //Context per request
        protected DiemDanhSvDbEntities Context;
        protected Dictionary<string, object> ListBusiness = new Dictionary<string, object>();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = filterContext.ActionDescriptor.ActionName;
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Create new context if null
        /// </summary>
        public DiemDanhSvDbEntities GetContext()
        {
            if (Context == null)
            {
                Context = new DiemDanhSvDbEntities();
            }
            return Context;
        }

        public List<T> GetData<T>(string query, ref int returnCode, DynamicParameters @params = null)
        {
            List<T> dtRt = null;
            string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DiemDanhSvDb"].ConnectionString;
            int _sql_timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlTimeout"]);
            try
            {
                using (SqlConnection _conn = new SqlConnection(constring))
                {
                    if (_conn.State == ConnectionState.Closed)
                        _conn.Open();
                    using (var _trans = _conn.BeginTransaction())
                    {
                        dtRt = _conn.Query<T>(query, @params, transaction: _trans, commandTimeout: _sql_timeout).ToList();

                        if (dtRt == null | dtRt.Count == 0)
                            returnCode = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                returnCode = 1;
                throw ex;
            }

            return dtRt;
        }

        public DataTable GetData(string query, ref int returnCode, Dictionary<string, object> parameters)
        {
            DataTable dtRt = new DataTable();
            string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DiemDanhSvDb"].ConnectionString;
            int _sql_timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlTimeout"]);
            try
            {
                using (SqlConnection conn = new SqlConnection(constring))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = commandType;
                        //cmd.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, object> parameter in parameters)
                            {
                                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dtRt);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                returnCode = 1;
            }

            return dtRt;
        }

        public int Update(string query, DynamicParameters @params, ref int returnCode)
        {
            int affectedRows;
            string constring = System.Configuration.ConfigurationManager.ConnectionStrings["DiemDanhSvDb"].ConnectionString;
            int _sql_timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SqlTimeout"]);
            try
            {
                using (SqlConnection _conn = new SqlConnection(constring))
                {
                    if (_conn.State == ConnectionState.Closed)
                        _conn.Open();
                    using (var _trans = _conn.BeginTransaction())
                    {
                        affectedRows = _conn.Execute(query, @params, _trans);

                        if (affectedRows == 0)
                            returnCode = 0;
                        else
                            _trans.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                returnCode = 1;
                throw ex;
            }
            return affectedRows;
        }

        public TaiKhoanInfo GetUserInfo()
        {
            return (TaiKhoanInfo)SessionManager.GetUserInfo();
        }

        public static string RenderToString(PartialViewResult partialView)
        {
            var httpContext = System.Web.HttpContext.Current;

            if (httpContext == null)
            {
                throw new NotSupportedException("An HTTP context is required to render the partial view to a string");
            }

            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName).View;

            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                using (var tw = new HtmlTextWriter(sw))
                {
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, tw), tw);
                }
            }

            return sb.ToString();
        }
    }
}