using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OpenQA.Selenium;

namespace Bordrolama10



{
    public static class programreferans
    {
        public static string firmaid = "";
        public static int subid = 0;
        public static int firmid = 0;
        public static string subeunvan ="";
        public static string firmaunvan ="";
        public static string IsyeriSgkNo ="";


    }
    public static class v1guvenliksozcugu
    {
        public static string v1guvenlik = "";
    }
    public static class ebildirgeV1Guvenlik
    {
        public static string v1GuvenlikImageUrl = "";
    }
    public static class v1driver
    {
        public static IWebDriver v1driver1 { get; set; }
    }
}
