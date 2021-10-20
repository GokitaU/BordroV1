using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfOku
{
    public static class DizinDosyalari
    {
        public static List<DizindekiDosyalar> DosyaList = new List<DizindekiDosyalar>();
        public static DizindekiDosyalar DD;

        public static void DosyaAdiEkle(string DosyaAdi)
        {
            DizinDosyalari.DD = new DizindekiDosyalar();
            DizinDosyalari.DD.DosyaAdi = DosyaAdi;
            DizinDosyalari.DosyaList.Add(DizinDosyalari.DD);
        }

        public static List<DizindekiDosyalar> DosyalariGetir()
        {
            return DizinDosyalari.DosyaList;
        }
    }

}
