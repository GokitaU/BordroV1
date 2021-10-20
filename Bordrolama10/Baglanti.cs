using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Bordrolama10



{
    public class Baglanti
    {
        public static string Baglan { get; set; } = ConfigurationManager.AppSettings["Baglanti"];
    }
}

