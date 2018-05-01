using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoShop.Common
{
    public class ConvertUtility
    {
        public static int ToInt32(object obj)
        {
            int retVal = default(int);

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static long ToInt64(object obj)
        {
            long retVal = default(long);

            try
            {
                retVal = Convert.ToInt64(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static int ToInt32(object obj, int defaultValue)
        {
            int retVal;

            try
            {
                if (obj != null)
                    retVal = Convert.ToInt32(obj);
                else
                    retVal = defaultValue;
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }
        public static int? ToInt32OrNull(object obj, int? defaultValue)
        {
            int? retVal;

            try
            {
                if (obj != null)
                    retVal = Convert.ToInt32(obj);
                else
                    retVal = defaultValue;
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static string ToString(object obj)
        {
            string retVal;

            try
            {
                retVal = Convert.ToString(obj);
            }
            catch
            {
                retVal = String.Empty;
            }

            return retVal;
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.MinValue;
            }
            if (retVal == new DateTime(1, 1, 1)) return DateTime.MinValue;

            return retVal;
        }

        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.MinValue;
            }
            if (retVal == new DateTime(1, 1, 1)) return defaultValue;

            return retVal;
        }

        public static DateTime? ToDateTimeOrNull(object obj)
        {
            DateTime? retVal;
            try
            {
                if (obj != null)
                    retVal = Convert.ToDateTime(obj);
                else
                    retVal = null;
            }
            catch
            {
                retVal = null;
            }

            return retVal;
        }

        public static bool ToBoolean(object obj)
        {
            bool retVal;

            try
            {
                retVal = Convert.ToBoolean(obj);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public static double ToDouble(object obj)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static double ToDouble(object obj, double defaultValue)
        {
            double retVal;

            try
            {
                if (obj != null)
                    retVal = Convert.ToDouble(obj);
                else
                    retVal = defaultValue;
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }
        public static double? ToDoubleOrNull(object obj, double? defaultValue)
        {
            double? retVal;

            try
            {
                if (obj != null)
                    retVal = Convert.ToDouble(obj.ToString());
                else
                    retVal = defaultValue;
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public static string ToVNDString(long number)
        {
            string price = number >= 0 ? number.ToString(CultureInfo.InvariantCulture) : "0";
            int count = price.Count();
            string priceStr;
            switch (count)
            {
                case 1:
                case 2:
                case 3:
                    priceStr = number + " đồng";
                    break;
                case 4:
                case 5:
                case 6:
                    priceStr = number / 1000 + " nghìn ";
                    if (number % 1000 > 0)
                        priceStr += number % 1000 + " đồng";
                    else
                        priceStr += " đồng";
                    break;
                case 7:
                case 8:
                case 9:
                    priceStr = number / 1000000 + " triệu ";
                    if (number % 1000000 > 0)
                    {
                        long thousand = number % 1000000;
                        priceStr += thousand / 1000 + " nghìn ";
                        if (thousand % 1000 > 0)
                            priceStr += thousand % 1000 + " đồng";
                        else
                            priceStr += " đồng";
                    }
                    else
                        priceStr += " đồng";
                    break;
                case 10:
                case 11:
                case 12:
                    priceStr = number / 1000000000 + " tỷ ";
                    if (number % 1000000000 > 0)
                    {
                        long hundredThousand = number % 1000000000;
                        priceStr += hundredThousand / 1000000 + " triệu ";

                        if (hundredThousand % 1000000 > 0)
                        {
                            long thousand = number % 1000000;
                            priceStr += thousand / 1000 + " nghìn ";
                            if (thousand % 1000 > 0)
                                priceStr += thousand % 1000 + " đồng";
                            else
                                priceStr += " đồng";
                        }
                        else
                            priceStr += " đồng";
                    }
                    else
                        priceStr += " đồng";

                    break;

                default:
                    priceStr = number + " đồng";
                    break;
            }
            return priceStr;
        }
    }
}
