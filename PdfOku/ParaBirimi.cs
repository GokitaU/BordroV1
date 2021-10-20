using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PdfOku
{
    public static class ParaBirimi
    {
        public static double PBCevir(string Tutar)
        {
            Tutar = Tutar.Trim();
            if (Tutar == "")
                Tutar = "0";
            string decimalSeparator1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string decimalSeparator2 = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            if (decimalSeparator1 == ",")
            {
                if (Tutar.ToString().IndexOf(".") != -1)
                    return Convert.ToDouble(Tutar.Replace(".", ","));
                return Convert.ToDouble(Tutar);
            }
            if (decimalSeparator2 == ".")
            {
                if (Tutar.ToString().IndexOf(",") != -1)
                    return Convert.ToDouble(Tutar.Replace(",", "."));
                return Convert.ToDouble(Tutar);
            }
            if (Tutar.ToString().IndexOf(".") != -1)
                return Convert.ToDouble(Tutar.Replace(".", ","));
            return Convert.ToDouble(Tutar);
        }
    }

}
