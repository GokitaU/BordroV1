using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OpenQA.Selenium;

namespace Bordrolama10



{
    public class programreferans
    {
        public static string firmaid = "";
        public static int subid = -1;
        public static int firmid = -1;
        public static string subeunvan = "";
        public static string firmaunvan = "";
        public static string IsyeriSgkNo = "";


    }
    public class v1guvenliksozcugu
    {
        public static string v1guvenlik = "";
    }
    public class ebildirgeV1Guvenlik
    {
        public static string v1GuvenlikImageUrl = "";
    }
    public class v1driver
    {
        public static IWebDriver v1driver1 { get; set; }
    }

    public static class Deneme
    {
        public static int ElemanSayisi(this DataTable table)
        {
            return table.Rows.Count;
        }
    }


}
