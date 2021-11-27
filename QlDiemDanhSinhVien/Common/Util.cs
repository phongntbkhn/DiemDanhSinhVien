using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class Util
    {
        /// <summary>
        /// convert datetime to string yyyy/MM/dd
        /// </summary>
        /// <param name="dtp"></param>
        /// <returns></returns>
        public static string CnvDateToString(DateTime? dtp)
        {
            string strDate = "";
            try
            {
                if (dtp != null)
                {
                    DateTime tmp = (DateTime)dtp;
                    strDate = tmp.ToString("yyyy/MM/dd");
                }
            }
            catch (Exception)
            {
                strDate = "";
            }
            return strDate;
        }

        /// <summary>
        /// Convert Object to string
        /// </summary>
        /// <param name="in_val"></param>
        /// <returns></returns>
        public static string CnvString(object in_val)
        {
            string out_val = "";
            try
            {

                if (in_val == null) { return out_val; }

                if (in_val is DBNull) { return out_val; }

                out_val = in_val.ToString();

            }
            catch (Exception)
            {
                out_val = "";
            }

            return out_val;
        }

        /// <summary>
        /// Convert Date yyyy/mm/dd to string
        /// </summary>
        /// <param name="in_val"></param>
        /// <returns></returns>
        public static string CnvDateTimeToString(string date)
        {
            string outDate = "";
            try
            {
                if (string.IsNullOrEmpty(date))
                {
                    outDate = "00000000";
                    return outDate;
                }
                else if (date.Length == 8)
                {
                    outDate = date;
                }
                else
                {
                    string[] temp = date.Split('/');
                    outDate = temp[0] + temp[1] + temp[2];
                }
            }
            catch (Exception)
            {
                outDate = "00000000";
            }

            return outDate;
        }

        /// <summary>
        /// Convert Time hh:mm to string
        /// </summary>
        /// <param name="in_val"></param>
        /// <returns></returns>
        public static string CnvTimeToString(string time)
        {
            string outTime = "";
            try
            {
                if (string.IsNullOrEmpty(time))
                {
                    outTime = "0000";
                    return outTime;
                }
                else
                {
                    string[] temp = time.Split(':');
                    outTime = temp[0] + temp[1];
                }
            }
            catch (Exception)
            {
                outTime = "0000";
            }

            return outTime;
        }

        /// <summary>
        /// Convert Object to string
        /// </summary>
        /// <param name="in_val"></param>
        /// <returns></returns>
        public static int CnvInt(object in_val, int defaul_val = 0)
        {
            int out_val = defaul_val;
            try
            {

                if (in_val == null) { return out_val; }

                if (in_val is DBNull) { return out_val; }

                out_val = Convert.ToInt32(in_val);

            }
            catch (Exception)
            {
                out_val = defaul_val;
            }

            return out_val;
        }

        /// <summary>
        /// Convert Object to string
        /// </summary>
        /// <param name="in_val"></param>
        /// <returns></returns>
        public static decimal CnvDecimal(object in_val, int defaul_val = 0)
        {
            decimal out_val = defaul_val;
            try
            {

                if (in_val == null) { return out_val; }

                if (in_val is DBNull) { return out_val; }

                out_val = Convert.ToDecimal(in_val);

            }
            catch (Exception)
            {
                out_val = defaul_val;
            }

            return out_val;
        }

        /// <summary>
        /// Convert Object to double
        /// </summary>
        /// <param name="in_val"></param>
        /// <returns></returns>
        public static double CnvDouble(object in_val, int defaul_val = 0)
        {
            double out_val = defaul_val;
            try
            {

                if (in_val == null) { return out_val; }

                if (in_val is DBNull) { return out_val; }

                out_val = Convert.ToDouble(in_val);

            }
            catch (Exception)
            {
                out_val = defaul_val;
            }

            return out_val;
        }
        /// <summary>
        /// Convert String to DateTime
        /// </summary>
        /// <param name="objInput"></param>
        /// <returns></returns>
        public static System.DateTime CnvDatetime(object objInput, string strDataFormat = "")
        {
            System.DateTime dtmResult = default(System.DateTime);

            try
            {
                if (Convert.IsDBNull(objInput) || objInput == null)
                {
                    return dtmResult;
                }

                if (string.IsNullOrEmpty(CnvString(objInput)))
                {
                    return dtmResult;
                }

                if (IsDate(objInput, strDataFormat))
                {
                    if (string.IsNullOrEmpty(strDataFormat))
                    {
                        return Convert.ToDateTime(objInput);
                    }
                    else
                    {
                        return DateTime.ParseExact(Convert.ToString(objInput), strDataFormat, CultureInfo.CurrentCulture);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtmResult;
        }

        /// <summary>
        /// Check object is datetime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsDate(Object obj, string format = "")
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = new DateTime();
                if (string.IsNullOrEmpty(format))
                {
                    dt = DateTime.Parse(strDate);
                }
                else
                {
                    dt = DateTime.ParseExact(strDate, format, null);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region MD5
        public static string MD5Hash(string input)
        {
            string md5_hash = "";
            using (var md5 = MD5.Create())
            {
                try
                {
                    var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                    md5_hash = Encoding.ASCII.GetString(result);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return md5_hash;
        }
        #endregion

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
