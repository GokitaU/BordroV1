using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfOku
{
    public class DizindekiDosyalar
    {
        //public string DosyaAdi { get; }

        private string _DosyaAdi;

        public string DosyaAdi
        {
            get
            {
                return this._DosyaAdi;
            }
            set
            {
                this._DosyaAdi = value;
            }
        }
    }

}
