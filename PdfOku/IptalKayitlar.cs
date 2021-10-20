using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfOku
{

    public class IptalKayitlar
    {
        private string _GGun = "";
        private string _CGun = "";
        private int DonemYil;
        private int DonemAy;
        private string SgkNo;
        private string KanunNo;
        private double _Maas;

        public int DonemYil1
        {
            get
            {
                return this.DonemYil;
            }
            set
            {
                this.DonemYil = value;
            }
        }

        public int DonemAy1
        {
            get
            {
                return this.DonemAy;
            }
            set
            {
                this.DonemAy = value;
            }
        }

        public string SgkNo1
        {
            get
            {
                return this.SgkNo;
            }
            set
            {
                this.SgkNo = value;
            }
        }

        public string KanunNo1
        {
            get
            {
                return this.KanunNo;
            }
            set
            {
                this.KanunNo = value;
            }
        }

        public string GGun
        {
            get
            {
                return this._GGun;
            }
            set
            {
                this._GGun = value;
            }
        }

        public string CGun1
        {
            get
            {
                return this._CGun;
            }
            set
            {
                this._CGun = value;
            }
        }

        public double Maas
        {
            get
            {
                return this._Maas;
            }
            set
            {
                this._Maas = value;
            }
        }
    }

}
