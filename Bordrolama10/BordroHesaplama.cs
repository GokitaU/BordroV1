using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bordrolama10
{
    public class Oranlar
    {
        public static int SgkIsciOrani { get; set; } = 15;

    }
    public class VergiDilimleri
    {
        public string yil { get; set; }
        public decimal Dilim1Aralik { get; set; }
        public int Dilim1Oran { get; set; }
        public decimal Dilim2Aralik { get; set; }
        public int Dilim2Oran { get; set; }
        public decimal Dilim3Aralik { get; set; }
        public int Dilim3Oran { get; set; }
        public decimal Dilim4Aralik { get; set; }
        public int Dilim4Oran { get; set; }
        public decimal Dilim5Aralik { get; set; }
        public int Dilim5Oran { get; set; }


    }

    public class BordroHesaplama
    {
        public decimal SgkIsciHesapla(decimal sgkMatrah)
        {
            return sgkMatrah * Oranlar.SgkIsciOrani / 100;

        }
        public decimal GvHesapla(VergiDilimleri vergiDilimleri, decimal gvMatrahi, decimal kumulatifMatrah)//5000, 25.000
        {
            //1. vergi dilimi 
            if (gvMatrahi <= 0) return 0;
            if (gvMatrahi + kumulatifMatrah <= vergiDilimleri.Dilim1Aralik)
            {
                return gvMatrahi * vergiDilimleri.Dilim1Oran / 100;
            }
            //2. vergi dilimi
            if (gvMatrahi + kumulatifMatrah >= vergiDilimleri.Dilim1Aralik && gvMatrahi + kumulatifMatrah <= vergiDilimleri.Dilim2Aralik)
                if (kumulatifMatrah < vergiDilimleri.Dilim1Aralik)
                {
                    var matrah1 = (vergiDilimleri.Dilim1Aralik - kumulatifMatrah) * vergiDilimleri.Dilim1Oran / 100;
                    return ((kumulatifMatrah + gvMatrahi - vergiDilimleri.Dilim1Aralik) * vergiDilimleri.Dilim2Oran / 100) + matrah1;
                }
                else
                {
                    return gvMatrahi * vergiDilimleri.Dilim2Oran / 100;
                }
            //3. vergi dilimi
            if (gvMatrahi + kumulatifMatrah >= vergiDilimleri.Dilim2Aralik && gvMatrahi + kumulatifMatrah <= vergiDilimleri.Dilim3Aralik)
                if (kumulatifMatrah < vergiDilimleri.Dilim2Aralik)
                {
                    var matrah1 = (vergiDilimleri.Dilim2Aralik - kumulatifMatrah) * vergiDilimleri.Dilim2Oran / 100;
                    return ((kumulatifMatrah + gvMatrahi - vergiDilimleri.Dilim2Aralik) * vergiDilimleri.Dilim3Oran / 100) + matrah1;
                }
                else
                {
                    return gvMatrahi * vergiDilimleri.Dilim3Oran / 100;
                }
            //4. vergi dilimi
            if (gvMatrahi + kumulatifMatrah >= vergiDilimleri.Dilim3Aralik && gvMatrahi + kumulatifMatrah <= vergiDilimleri.Dilim5Aralik)
                if (kumulatifMatrah < vergiDilimleri.Dilim3Aralik)
                {
                    var matrah1 = (vergiDilimleri.Dilim3Aralik - kumulatifMatrah) * vergiDilimleri.Dilim3Oran / 100;
                    return ((kumulatifMatrah + gvMatrahi - vergiDilimleri.Dilim3Aralik) * vergiDilimleri.Dilim4Oran / 100) + matrah1;
                }
                else
                {
                    return gvMatrahi * vergiDilimleri.Dilim4Oran / 100;
                }
            //5. vergi dilimi
            if (gvMatrahi + kumulatifMatrah >= vergiDilimleri.Dilim4Aralik )
                if (kumulatifMatrah < vergiDilimleri.Dilim4Aralik)
                {
                    var matrah1 = (vergiDilimleri.Dilim4Aralik - kumulatifMatrah) * vergiDilimleri.Dilim4Oran / 100;
                    return ((kumulatifMatrah + gvMatrahi - vergiDilimleri.Dilim4Aralik) * vergiDilimleri.Dilim5Oran / 100) + matrah1;
                }
                else
                {
                    return gvMatrahi * vergiDilimleri.Dilim5Oran / 100;
                }
            return 0;
           
        }
    }
}
