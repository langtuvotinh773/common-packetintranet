using System;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Collections;
using System.Configuration;


using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Linq;

using System.Security.Cryptography;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.AccessControl;

namespace PacketIntranet.Helpers
{
    public static class Common
    {
        public static bool UploadFile(HttpPostedFileBase File, string UploadPath)
        {
            try
            {
                File.SaveAs(HttpContext.Current.Server.MapPath(UploadPath + File.FileName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UploadFile(HttpPostedFileBase File, string UploadPath, string NewName)
        {
            try
            {
              File.SaveAs(HttpContext.Current.Server.MapPath(UploadPath + NewName));
               //  File.SaveAs("E:\\New SpotVN 7.0\\spotvietnam\\MvcFontend\\Content\\RealImages" + NewName);
               // File.SaveAs("http://localhost:12908//spotvietnam\\MvcFontend\\Content\\RealImages" + NewName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteFile(string FilePath)
        {
            try
            {
                //if (System.IO.File.Exists(FilePath))
                //{
                System.IO.File.Delete(HttpContext.Current.Server.MapPath(FilePath));
                    return true;
                //}
                //else
                //{
                //    return false;
                //}

            }
            catch
            {
                return false;
            }
        }
        public static bool UploadImages( HttpPostedFileBase File, string RenameFile)
        {
           
                try
                {
                   
                    string strImageName = "", strImageType = "";
                    // đếm số lượng file 
                    strImageName = RenameFile; // Tên file Images
                    strImageType = strImageName.Substring(strImageName.LastIndexOf(".")); // Cắt lấy đuôi ảnh
                    string pathStoredImage = ConfigurationManager.ConnectionStrings["pathImg_Product"].ToString(); // đường dẫn hình cần lưu
                    File.SaveAs(HttpContext.Current.Server.MapPath(pathStoredImage + strImageName));

                    #region thumnails Images
                    //if (imageLoad2 != null)
                    //{
                    //    string sddfsdfsdf = "ooo";
                    //}
                    //System.Drawing.Image image = System.Drawing.Image.FromStream(Request.Files[item].InputStream);
                    //string FilePath = Request.PhysicalApplicationPath + "Content\\RealImages\\";
                    //string LocalFile = "";
                    //int imgWidth = 252, imgHeight = 192;
                    //System.Drawing.Image thumb1 = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate() { return false; }, (IntPtr)0);
                    //LocalFile = FilePath + file.FileId + "_thumbnail" + strImageType;
                    //thumb1.Save(LocalFile);
                    //newFile.Thumbnails = file.FileId + "_thumbnail" + strImageType;
                    //UpdateModel(newFile);
                    //db.SaveChanges();
                    #endregion


                    return true;


                }
                catch
                {
                    return false;
                }
            
            return false;
        }
        //public static bool UploadImage_thumnail(HttpPostedFileBase File, string allFile, string RenameFile)
        //{
        //    for (int item = 0; item < allFile.Split(',').Length; item++)
        //    {
        //        try
        //        {

        //            string strImageName = "", strImageType = "";
        //            // đếm số lượng file 
        //            strImageName = RenameFile; // Tên file Images
        //            strImageType = strImageName.Substring(strImageName.LastIndexOf(".")); // Cắt lấy đuôi ảnh
        //            string pathStoredImage = ConfigurationManager.ConnectionStrings["RealImage"].ToString(); // đường dẫn hình cần lưu
        //            File.SaveAs(HttpContext.Current.Server.MapPath(pathStoredImage + strImageName));

        //            #region thumnails Images
        //            System.Drawing.Image image = System.Drawing.Image.FromStream(Request.Files[item].InputStream);
        //            string FilePath = Request.PhysicalApplicationPath + "Content\\RealImages\\";
        //            string LocalFile = "";
        //            int imgWidth = 252, imgHeight = 192;
        //            System.Drawing.Image thumb1 = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate() { return false; }, (IntPtr)0);
        //            LocalFile = FilePath + file.FileId + "_thumbnail" + strImageType;
        //            thumb1.Save(LocalFile);
        //            newFile.Thumbnails = file.FileId + "_thumbnail" + strImageType;
        //            UpdateModel(newFile);
        //            db.SaveChanges();
        //            #endregion


        //            return true;


        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}
        //public static bool UploadFile(HttpPostedFileBase File, string UploadPath)
        //{
        //    try
        //    {
        //        File.SaveAs(HttpContext.Current.Server.MapPath(UploadPath + File.FileName));
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public static bool UploadFile(HttpPostedFileBase File, string UploadPath, string NewName)
        //{
        //    try
        //    {
        //        File.SaveAs(HttpContext.Current.Server.MapPath(UploadPath + NewName));
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public static bool DeleteFile(string FilePath)
        //{
        //    try
        //    {
        //        if (System.IO.File.Exists(FilePath))
        //        {
        //            System.IO.File.Delete(FilePath);
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        

        #region Encypt Algorithms

        /// <summary>
        /// Auto genarate to string with max 20 random character.
        /// </summary>
        /// <returns>Maximun is 20 random character.</returns>
        public static string GenaratePasswordSalt()
        {
            byte[] randomBytes = new byte[10];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            string paswordSalt = Convert.ToBase64String(randomBytes);
            return paswordSalt;
        }


        /// <summary>
        /// Get password string whith MD5 encrypt standar.
        /// </summary>
        /// <param name="PASSWORD_FROM_SCREEN">Password get from screen</param>
        /// <param name="PASSWORD_SALT">Password salt</param>
        /// <returns></returns>
        public static string GetMD5EncyptString(string PASSWORD_FROM_SCREEN, string PASSWORD_SALT)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedBytes;
            string str = PASSWORD_FROM_SCREEN + PASSWORD_SALT;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(str));
            string str_md5 = string.Empty;
            foreach (byte b in hashedBytes)
            {
                str_md5 += b.ToString("X2");
            }
            return str_md5;
        }

        #endregion

        #region Common Maths

        /// <summary>
        /// Subtract two datetime value and return the number of days between them.
        /// </summary>
        /// <param name="minusDate">The minus value</param>
        /// <param name="subtrahendDate">The ubtrahend value</param>
        /// <remarks>
        /// Author: Nguyen Tien Lam
        /// Date create: 21/07/2011
        /// </remarks>/// <returns>The number of days</returns>
        public static int SubtractDateTime(DateTime minusDate, DateTime subtrahendDate)
        {
            return minusDate.Subtract(subtrahendDate).Days;
        }

        /// <summary>
        /// Subtract two datetime value and return the number of years between them.
        /// </summary>
        /// <param name="minusDate"></param>
        /// <param name="subtrahendDate"></param>
        /// <remarks>
        /// Author: Nguyen Tien Lam
        /// Date create: 21/07/2011
        /// </remarks> /// <returns></returns>
        public static int SubtractDateTimeToGetYear(object minusDate, object subtrahendDate)
        {
            try
            {
                DateTime minusTemp = (DateTime)minusDate;
                DateTime subtractTemp = (DateTime)subtrahendDate;
                return (minusTemp.Year - subtractTemp.Year);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public static decimal Divide(object dividend, object divisor)
        {
            try
            {
                decimal dividendTemp = Common.ParseDecimal(dividend);
                decimal divisorTemp = Common.ParseDecimal(divisor);
                if (dividendTemp != 0)
                {
                    return dividendTemp / divisorTemp;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

     

        #region Files

        /// <summary>
        /// Get the file name withow extention.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileNameWithowExtention(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        /// <summary>
        /// Get file name from file path string.
        /// </summary>
        /// <param name="fileURL">The path of file.</param>
        /// <returns>The name of file.</returns>
        public static string GetFileName(string fileURL)
        {
            return Path.GetFileName(fileURL);
        }

        /// <summary>
        /// Combine the name and folder directory path to return the new full file path.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Combine(string directoryPath, string fileName)
        {
            return Path.Combine(directoryPath, fileName);
        }


        /// <summary>
        /// Get file Extension from file name or file path.
        /// </summary>
        /// <param name="fileName">File name or file path</param>
        /// <returns>file extension string</returns>
        public static string GetFileExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        #endregion

        #region User login section

        #endregion

        #region Currents

        /// <summary>
        /// Get current year. (yyyy)
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }

        /// <summary>
        /// Get current date. (Examples: dd/MM/yyyy)
        /// </summary>
        /// <author>
        /// Phan Trung An
        /// </author>
        /// <returns></returns>
        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Get current date with custom format. (Examples: formatString = "yyyyMMdd_hhmmss")
        /// </summary>
        /// <author>
        /// Phan Trung An
        /// </author>
        /// <returns></returns>
        public static string GetCurrentDate(string formatString)
        {
            try
            {
                return DateTime.Now.ToString(formatString);
            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
        }
        /// <summary>
        /// Get temple file name to dowmload (formatString = "ddMMyyyyhhmmsstt")
        /// </summary>
        /// <author>
        /// Phan Trung An
        /// </author>
        /// <returns></returns>
        public static string GetDownloadFileNameTemp()
        {
            try
            {
                return Common.GetCurrentDate("ddMMyyyyhhmmsstt");
            }
            catch (Exception ex)
            {
                return string.Empty;
                throw ex;
            }
        }

        /// <summary>
        /// Get temple file name to dowmload (formatString = "ddMMyyyyhhmmsstt")
        /// </summary>
        /// <author>
        /// Nguyen Huu Khuong
        /// </author>
        /// <returns></returns>
        public static int CalculateMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        #endregion End Currents

        #region Validation

        /// <summary>
        /// Check the object is null or empty.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object obj)
        {
            try
            {
                if (obj == null)
                    return true;
                if (obj.ToString().Trim().Equals(""))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


        public static bool IsNumeric(object Expression)
        {
            //Variable to collect the Return value of the TryParse method.
            bool isNum;

            //Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            //The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            //The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        /// <summary>
        /// Valid max length of a object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static bool IsValidMaxLength(object obj, long maxLength)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return true;
                if (obj.ToString().Trim().Length <= maxLength)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// Valid the email format.
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public static bool IsValidEmailAddress(string sEmail)
        {
            if (sEmail == null)
            {
                return false;
            }

            int nFirstAT = sEmail.IndexOf('@');
            int nLastAT = sEmail.LastIndexOf('@');

            if ((nFirstAT > 0) && (nLastAT == nFirstAT) &&
            (nFirstAT < (sEmail.Length - 1)))
            {
                //address is ok regarding the single @ sign
                return (Regex.IsMatch(sEmail, @"(\w+)@(\w+)\.(\w+)"));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Vaild the string is date format or not. (only accepted format: yyyy/MM/dd or yyyy-MM-dd)
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool IsDate(string strDate)
        {
            string valuePassed = string.Empty;
            valuePassed = strDate;
            DateTime dt = DateTime.MinValue;

            try
            {
                string strRegex = @"^\d{4}([/-])\d{1,2}([/-])\d{1,2}$";
                Regex re = new Regex(strRegex);
                if (!re.IsMatch(strDate))
                    return false;

                IFormatProvider format = new CultureInfo("en-US");

                dt = DateTime.Parse(valuePassed, format, DateTimeStyles.None);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion Validation End

        #region Convert Methods

        /// <summary>
        /// Convert the object to Long format.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ParseLong(object obj)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return 0;
                return long.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// Convert the object to Int format. (Default is 0)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ParseInt(object obj)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return 0;
                return int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        /// <summary>
        /// Convert the object to Double format. (0 is default)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ParseDouble(object obj)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return 0;
                return double.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        /// <summary>
        /// Convert the object to Decimal format.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ParseDecimal(object obj)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return 0;
                return decimal.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        /// Author: KhuongNH
        /// <summary>
        /// Convert the object to Decimal format.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ParseDecimalDefaultNegativeOne(object obj)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return -1;
                return decimal.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return -1;
                throw ex;
            }
        }

        /// <summary>
        /// Convert the object to String format.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ParseString(object obj)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return "";
                return obj.ToString();
            }
            catch (Exception ex)
            {
                return "";
                throw ex;
            }
        }

        /// <summary>
        /// Get boolean value from implicitly boolean object. (Default is false)
        /// </summary>
        /// <remarks>
        /// Author:     Nguyen Tien Lam
        /// Date create:01/07/2011
        /// </remarks>
        /// <returns></returns>
        public static bool ParseBoolean(bool? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
                return false;
        }

        /// <summary>
        /// Get boolean value from object. (Default is false)
        /// </summary>
        /// <remarks>
        /// Author:     Nguyen Tien Lam
        /// Date create:01/07/2011
        /// </remarks>
        public static bool ParseBoolean(object obj)
        {
            try
            {
                if (IsNullOrEmpty(obj))
                    return false;
                return bool.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }


        /// <summary>
        /// Convert the number to word in English.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertNumberToWord(string str)
        {
            string strNum1 = "";
            string strNum2 = "";
            string total = "";
            string strTemp = "";
            string strTemp2 = "";
            string result = "";
            str = str.Replace(",", "");

            if (str.Contains("."))
            {
                strNum1 = ConvertToWord(Convert.ToInt64(str.Substring(str.IndexOf(".") + 1, str.Length - str.IndexOf(".") - 1)));
                if (str.Contains(","))
                {
                    strTemp = str.Substring(0, str.IndexOf(","));
                    strTemp2 = str.Substring(strTemp.ToString().Length + 1, str.Length - (strTemp.ToString().Length + 4));
                    total = strTemp.ToString() + strTemp2.ToString();
                }
                else
                {
                    strTemp2 = str.Substring(0, str.IndexOf("."));
                    total = strTemp2.ToString();
                }
                strNum2 = ConvertToWord(Convert.ToInt64(total));

                if (strNum1 != "")
                {
                    return result = strNum2 + " USD AND " + strNum1 + " CENTS";
                }
                else
                {
                    return result = strNum2 + " USD";
                }
            }
            else
            {
                return result = ConvertToWord(Convert.ToInt64(str)) + " USD ";
            }
        }

        private static string ConvertToWord(long nNumber)
        {
            long CurrentNumber = nNumber;
            string sReturn = "";

            if (CurrentNumber >= 1000000000)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 1000000000, "BILLION");
                CurrentNumber = CurrentNumber % 1000000000;
            }
            if (CurrentNumber >= 1000000)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 1000000, "MILLION");
                CurrentNumber = CurrentNumber % 1000000;
            }
            if (CurrentNumber >= 1000)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 1000, "THOUSAND");
                CurrentNumber = CurrentNumber % 1000;
            }
            if (CurrentNumber >= 100)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 100, "HUNDRED");
                CurrentNumber = CurrentNumber % 100;
            }
            if (CurrentNumber >= 20)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber, "");
                CurrentNumber = CurrentNumber % 10;
            }
            else if (CurrentNumber > 0)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber, "");
                CurrentNumber = 0;
            }
            return sReturn.Replace("  ", " ").Trim();
        }

        private static string GetWord(long nNumber, string sPrefix)
        {
            long nCurrentNumber = nNumber;
            string sReturn = "";
            while (nCurrentNumber > 0)
            {
                if (nCurrentNumber > 100)
                {
                    sReturn = sReturn + " " + GetWord(nCurrentNumber / 100, "HUNDRED");
                    nCurrentNumber = nCurrentNumber % 100;
                }
                else if (nCurrentNumber > 20)
                {
                    sReturn = sReturn + " " + GetTwentyWord(nCurrentNumber / 10);
                    nCurrentNumber = nCurrentNumber % 10;
                }
                else
                {
                    sReturn = sReturn + " " + GetLessThanTwentyWord(nCurrentNumber);
                    nCurrentNumber = 0;
                }
            }
            sReturn = sReturn + " " + sPrefix;
            return sReturn;
        }

        private static string GetTwentyWord(long nNumber)
        {
            string sReturn = "";
            switch (nNumber)
            {
                case 2:
                    sReturn = "TWENTY";
                    break;
                case 3:
                    sReturn = "THIRTY";
                    break;
                case 4:
                    sReturn = "FORTY";
                    break;
                case 5:
                    sReturn = "FIFTY";
                    break;
                case 6:
                    sReturn = "SIXTY";
                    break;
                case 7:
                    sReturn = "SEVENTY";
                    break;
                case 8:
                    sReturn = "EIGHTY";
                    break;
                case 9:
                    sReturn = "NINETY";
                    break;
            }
            return sReturn;
        }

        private static string GetLessThanTwentyWord(long nNumber)
        {
            string sReturn = "";
            switch (nNumber)
            {
                case 1:
                    sReturn = "ONE";
                    break;
                case 2:
                    sReturn = "TWO";
                    break;
                case 3:
                    sReturn = "THREE";
                    break;
                case 4:
                    sReturn = "FOUR";
                    break;
                case 5:
                    sReturn = "FIVE";
                    break;
                case 6:
                    sReturn = "SIX";
                    break;
                case 7:
                    sReturn = "SEVEN";
                    break;
                case 8:
                    sReturn = "EIGHT";
                    break;
                case 9:
                    sReturn = "NINE";
                    break;
                case 10:
                    sReturn = "TEN";
                    break;
                case 11:
                    sReturn = "ELEVEN";
                    break;
                case 12:
                    sReturn = "TWELVE";
                    break;
                case 13:
                    sReturn = "THIRTEEN";
                    break;
                case 14:
                    sReturn = "FORTEEN";
                    break;
                case 15:
                    sReturn = "FIFTEEN";
                    break;
                case 16:
                    sReturn = "SIXTEEN";
                    break;
                case 17:
                    sReturn = "SEVENTEEN";
                    break;
                case 18:
                    sReturn = "EIGHTEEN";
                    break;
                case 19:
                    sReturn = "NINETEEN";
                    break;
            }
            return sReturn;
        }


        /// <summary>
        /// Convert the numer to string with percent format.
        /// </summary>
        /// <returns></returns>
        public static string ToStringWithPercentFormat(object numberic)
        {
            try
            {
                double value = Common.ParseDouble(numberic);
                return value.ToString("P1", CultureInfo.CreateSpecificCulture("en-US"));
            }
            catch (Exception ex)
            {
                return Common.ParseString(numberic); ;
                throw ex;
            }
        }

        /// <summary>
        /// Convert the numer to string percent format with no decimal place.
        /// </summary>
        /// <returns></returns>
        public static string ToStringWithPercentFormatNoDecimalPlace(object numberic)
        {
            try
            {
                double value = Common.ParseDouble(numberic);
                return value.ToString("P0", CultureInfo.CreateSpecificCulture("en-US"));
            }
            catch (Exception ex)
            {
                return Common.ParseString(numberic);
                throw ex;
            }
        }

        /// <summary>
        /// Get the string with "hhmmss" format from system date.
        /// </summary>
        /// <remarks>
        /// Author: Nguyen Tien Lam
        /// Create date: 04/07/2011
        /// </remarks>
        /// <returns></returns>
        public static string GetHourMinuteSecond(DateTime date)
        {
            string tt = date.ToString("tt").ToUpper();
            string hhmmss = string.Empty;
            if (tt.Equals("AM"))
            {
                hhmmss = date.ToString("hhmmss");
            }
            else
            {
                int hh = date.Hour;
                if (hh <= 12)
                {
                    hhmmss = (date.Hour + 12).ToString() + date.ToString("mmss");
                }
                else
                {
                    hhmmss = hh.ToString() + date.ToString("mmss");
                }
            }
            return hhmmss;
        }

        /// <summary>
        /// Get the string with "hh:mm" format from system date.
        /// </summary>
        /// <remarks>
        /// Author: Phan Trung An
        /// Create date: 07/07/2011
        /// </remarks>
        /// <returns></returns>
        public static string GetHourMinute(DateTime date)
        {
            string tt = date.ToString("tt").ToUpper();
            string hhmm = string.Empty;
            if (tt.Equals("AM"))
            {
                hhmm = date.ToString("hh:mm tt");
            }
            else
            {
                int hh = date.Hour;
                if (hh <= 12)
                {
                    hhmm = (date.Hour + 12).ToString() + date.ToString(":mm tt");
                }
                else
                {
                    hhmm = hh.ToString() + date.ToString(":mm tt");
                }
            }
            return hhmm;
        }

        /// <summary>
        /// Convert date to format(DD/MM/YYYY)
        /// </summary>
        /// <param name="_date">The date value</param>
        /// <returns>The date with format (DD/MM/YYYY)</returns>
        public static DateTime ConvertDateTime(string _date)
        {
            try
            {
                DateTime newDate = DateTime.ParseExact(_date, "dd/MM/yyyy", null);
                return newDate;
            }
            catch
            {
                return DateTime.Now;
            }
        }



        /// <summary>
        /// Convert to number format.
        /// </summary>
        /// <remarks>
        /// Author: Phan Trung An
        /// Create date: 06/07/2011
        /// </remarks>
        /// <returns></returns>
        public static string ConVertToNumber(object obj)
        {
            try
            {
                return string.Format("{0:N}", obj);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string ConVertToNumberPercent(object obj)
        {
            try
            {
                return string.Format("{0:N}%", obj);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert data time value to string. (Default is string.empty)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dateTimeFormat"></param>
        /// <remarks>
        /// Author: Nguyen Tien Lam
        /// Date create: 21/07/2011
        /// </remarks>
        /// <returns></returns>
        public static string ParseTime(object value, string dateTimeFormat)
        {
            try
            {
                if (value is Nullable)
                    return string.Empty;
                return string.Format("{0:" + dateTimeFormat + "}", value);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Convert the number format object to string. Hyphen (-) is default.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ParseStringWithHyphenDefault(object value)
        {
            string defaultString = "-";
            try
            {
                double valueTemp = Common.ParseDouble(value);
                if (valueTemp == 0)
                {
                    return defaultString;
                }
                else
                {
                    return Common.ParseString(value);
                }
            }
            catch
            {
                return defaultString;
            }
        }
        /// <summary>
        /// Replace spaces before,between,after string
        /// </summary>
        /// Author: Nhan Anh Nguyen
        /// Date create: 29/07/2011
        /// <param name="value"></param>
        /// <returns></returns>
        public static string replaceSpaces(string text)
        {
            return Regex.Replace(text, @"\s+", " ").Trim();
        }

        #endregion Convert End

        #region Get OS Infomations

        /// <summary>
        /// Check IsWow64.
        /// </summary>
        /// <returns></returns>
        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check Is Wow64 Process
        /// </summary>
        /// <param name="hProcess"></param>
        /// <param name="wow64Process"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );


        /// <summary>
        /// Get the type of Operation system. (32: x86, 64: x64, 0: N/A)
        /// </summary>
        /// <returns></returns>
        public static Int16 GetOSType()
        {
            bool is64BitProcess = (IntPtr.Size == 8);
            bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();
            Int16 type = 0;
            if (is64BitOperatingSystem)
            {
                type = 64;
            }
            else
            {
                type = 32;
            }
            return type;
        }
        /// <summary>
        /// Get Seven Zip Base DLL for each OS. (7z64.dll for 64bit; 7z.dll for 32bit);
        /// </summary>
        /// <returns></returns>
        public static string GetSevenZipBaseDLLForThisOS()
        {
            Int16 osType = Common.GetOSType();
            if (osType == 64)
            {
                return "7z64.dll";
            }
            else
            {
                return "7z.dll";
            }
        }
        #endregion Get OS Infomations

      

     
    }

   
}