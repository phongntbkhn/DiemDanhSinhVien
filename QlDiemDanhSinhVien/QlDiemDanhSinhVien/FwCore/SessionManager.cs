using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QlDiemDanhSinhVien.FwCore
{
    public class SessionManager
    {
        public const string USER_INFO = "UserInfo";
        public const string QUYEN_ERROR = "QuyenError";
        public const string LIST_PERMISSTION = "LIST_PERMISSTION";
        public const string LIST_CSRF_TOKEN = "LIST_CSRF_TOKEN";

        public static void SetValue(string Key, object Value)
        {
            HttpContext context = HttpContext.Current;
            context.Session[Key] = Value;
        }

        public static object GetValue(string Key)
        {
            HttpContext context = HttpContext.Current;
            return context.Session[Key];
        }

        public static void Remove(string Key)
        {
            HttpContext context = HttpContext.Current;
            context.Session.Remove(Key);
        }

        public static void Clear()
        {
            HttpContext context = HttpContext.Current;
            context.Session.RemoveAll();
        }

        public static bool HasValue(string Key)
        {
            HttpContext context = HttpContext.Current;
            return context.Session[Key] != null;
        }

        public static object GetUserInfo()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[USER_INFO];
        }

        public static object GetListPermistion()
        {
            HttpContext context = HttpContext.Current;
            return context.Session[LIST_PERMISSTION];
        }

        public static bool ValidateCsrfToken(string token, bool InvalidAfterValidate = true)
        {
            List<string> listToken = (List<string>)GetValue(LIST_CSRF_TOKEN);
            if (listToken == null || !listToken.Contains(token))
            {
                return false;
            }
            else
            {
                if (InvalidAfterValidate)
                {
                    listToken.Remove(token);
                    SetValue(LIST_CSRF_TOKEN, listToken);
                }

                return true;
            }
        }

        public static string GenerateCsrfToken()
        {
            List<string> listToken = (List<string>)GetValue(LIST_CSRF_TOKEN);
            if (listToken == null)
            {
                listToken = new List<string>();
            }
            string token = Guid.NewGuid().ToString();
            listToken.Add(token);

            SetValue(LIST_CSRF_TOKEN, listToken);

            return token;
        }
    }
}