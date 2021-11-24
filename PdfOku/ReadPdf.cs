
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;





namespace PdfOku
{
    public class ReadPdf
    {
        private static int _numberOfCharsToKeep = 15;
        public static List<IptalKayitlar> SilinecekIptalKayitlar = new List<IptalKayitlar>();
        private RichTextBox rbMetin = new RichTextBox();
        private List<PdfHizmetListesi> HList = new List<PdfHizmetListesi>();
        private List<DizindekiDosyalar> DizindekiDosyaList = new List<DizindekiDosyalar>();
        private string DosyaYolu = Application.StartupPath + "\\Pdf\\";
        private string BaslangicDonemi = "";
        private int Sira;
        private int GKod;
        private bool TaramaSonu;
        private bool kk;
        private IContainer components;
        private Button button1;
        private LinkLabel linkLabel1;

        private List<DizindekiDosyalar> DizinIceriginiListeyeEkle(string dizin)
        {
            DizinDosyalari.DosyaList.Clear();
            foreach (string file in Directory.GetFiles(dizin))
                DizinDosyalari.DosyaAdiEkle(new FileInfo(file).Name);
            List<DizindekiDosyalar> dizindekiDosyalarList = new List<DizindekiDosyalar>();
            return DizinDosyalari.DosyalariGetir();
        }

        public void DosyaOkumayaBasla()//public olacak
        {
            this.DizindekiDosyaList = this.DizinIceriginiListeyeEkle(this.DosyaYolu);
            foreach (DizindekiDosyalar dizindekiDosya in this.DizindekiDosyaList)
                this.PdfOku(this.DosyaYolu + dizindekiDosya.DosyaAdi);
            HizmetListesiniKaydet(this.HList);
            //if (SilinecekIptalKayitlar.Count > 0)
            //    KayitIptalSil();
            int num = (int)MessageBox.Show("Okuma İşlemi Tamamlandı. \n\nVeritabanına Kayıt İşlemi Tamamlandı.", "Bilgi Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }

        private static void KayitIptalSil()
        {
            using (SQLiteConnection sqlConnection = ConnectionVer())
            {
                SQLiteCommand sqlCommand1 = new SQLiteCommand();
                sqlCommand1.CommandText = "Delete From HizmetListesi Where KurumID=" + (object)1 + " And Mahiyet='IPTAL'";
                sqlCommand1.Connection = sqlConnection;
                SQLiteCommand sqlCommand2 = sqlCommand1;
                sqlCommand2.ExecuteNonQuery();
                foreach (IptalKayitlar iptalKayitlar in SilinecekIptalKayitlar)
                {
                    sqlCommand2.CommandText = "Select KurumID From HizmetListesi Where SgkNo='" + iptalKayitlar.SgkNo1 + "' And Year=" + (object)iptalKayitlar.DonemYil1 + " And Month=" + (object)iptalKayitlar.DonemAy1 + " And Kanun_No='" + iptalKayitlar.KanunNo1 + "' And Mahiyet='ASIL' And CGun='" + iptalKayitlar.CGun1 + "' And GGun='" + iptalKayitlar.GGun + "' And Ucret='" + iptalKayitlar.Maas.ToString().Replace(',', '.') + "'";
                    sqlCommand2.Connection = sqlConnection;
                    SQLiteDataReader sqlDataReader = sqlCommand2.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        sqlDataReader.Close();
                        sqlCommand2.CommandText = "Delete From HizmetListesi Where SgkNo='" + iptalKayitlar.SgkNo1 + "' And Year=" + (object)iptalKayitlar.DonemYil1 + " And Month=" + (object)iptalKayitlar.DonemAy1 + " And Kanun_No='" + iptalKayitlar.KanunNo1 + "' And Mahiyet='ASIL' And CGun='" + iptalKayitlar.CGun1 + "' And GGun='" + iptalKayitlar.GGun + "' And Ucret='" + iptalKayitlar.Maas.ToString().Replace(',', '.') + "'";
                        sqlCommand2.Connection = sqlConnection;
                        sqlCommand2.ExecuteNonQuery();
                    }
                    else
                    {
                        sqlDataReader.Close();
                        sqlCommand2.CommandText = "Delete From HizmetListesi Where SgkNo='" + iptalKayitlar.SgkNo1 + "' And Year=" + (object)iptalKayitlar.DonemYil1 + " And Month=" + (object)iptalKayitlar.DonemAy1 + " And Kanun_No='" + iptalKayitlar.KanunNo1 + "' And Mahiyet='EK' And CGun='" + iptalKayitlar.CGun1 + "' And GGun='" + iptalKayitlar.GGun + "' And Ucret='" + iptalKayitlar.Maas.ToString().Replace(',', '.') + "'";
                        sqlCommand2.Connection = sqlConnection;
                        sqlCommand2.ExecuteNonQuery();
                    }
                }
            }
        }

        private static void HizmetListesiniKaydet(List<PdfHizmetListesi> HL)
        {
            int num1 = 0;
            int num2 = 1;
            if (HL.Count <= 0)
                return;
            int count = HL.Count;
            using (SQLiteConnection sqlConnection = ConnectionVer())
            {
                SQLiteCommand sqlCommand = new SQLiteCommand();
                foreach (PdfHizmetListesi pdfHizmetListesi in HL)
                {
                    ++num2;
                    sqlCommand.CommandText = "Insert Into HizmetListesi (KurumID,Year,Month,SgkNo,Ad,Soyad,IlkSoyad,Ucret,Ikramiye," +
                                             "Gun,Eksik_Gun,GGun,CGun,Egs,Icn,Meslek_Kodu,Kanun_No,Belge_Cesidi,Belge_Turu,OnayBekleyen,Mahiyet,UCG,subeid,firmaid,personelid,firmPersid,Donem,Asg_Ucr_GV,Agi_Minumum,Asg_Ucr_Trk_icin_Matrah,gvTerkin,dvTerkin)VALUES (@KURUMID,@YEAR,@MONTH,@SGKNO,@AD,@SOYAD,@ILKSOYAD,@UCRET,@IKRAMIYE,@GUN,@EKSIK_GUN,@GGUN,@CGUN,@EGS,@ICN,@MESLEK_KODU,@KANUN_NO,@BELGE_CESIDI,@BELGETURU,@ONAYBEKLEYEN,@MAHIYET,@UCG,@subid,@firmaid,@Persid,@firmPersid,@DONEM,@augv,@agimin,@trkMatrh,@gvterkin,@dvterkin)";
                    sqlCommand.Parameters.Clear();
                    sqlCommand.Parameters.AddWithValue("@KURUMID", (object)1);
                    //sqlCommand.Parameters.AddWithValue("@YEAR", (object)Convert.ToInt32(pdfHizmetListesi.Donem.Split('/')[0].ToString().Trim()));
                    sqlCommand.Parameters.AddWithValue("@YEAR", (object)(pdfHizmetListesi.Donem.Split('/')[0].ToString().Trim()));
                    sqlCommand.Parameters.AddWithValue("@MONTH", (object)(pdfHizmetListesi.Donem.Split('/')[1].ToString().Trim()));
                    sqlCommand.Parameters.AddWithValue("@SGKNO", (object)pdfHizmetListesi.SgkNo.Trim());
                    sqlCommand.Parameters.AddWithValue("@AD", (object)pdfHizmetListesi.Ad);
                    sqlCommand.Parameters.AddWithValue("@SOYAD", (object)pdfHizmetListesi.Soyad);
                    sqlCommand.Parameters.AddWithValue("@ILKSOYAD", (object)pdfHizmetListesi.IlkSoyad);
                    sqlCommand.Parameters.AddWithValue("@UCRET", (object)Convert.ToDouble(pdfHizmetListesi.Ucret.Replace(".", "").Trim()));
                    sqlCommand.Parameters.AddWithValue("@IKRAMIYE", (object)Convert.ToDouble(pdfHizmetListesi.Ikramiye.Replace(".", "").Trim()));
                    sqlCommand.Parameters.AddWithValue("@GUN", (object)Convert.ToInt32(pdfHizmetListesi.Gun.Trim()));
                    sqlCommand.Parameters.AddWithValue("@UCG", (object)Convert.ToInt32(pdfHizmetListesi.UCG.Trim()));       // değiştirilen alan
                    sqlCommand.Parameters.AddWithValue("@EKSIK_GUN", (object)Convert.ToInt32(pdfHizmetListesi.EksikGun.Trim()));
                    if (pdfHizmetListesi.GGun == "")
                        sqlCommand.Parameters.AddWithValue("@GGUN", (object)"");
                    else
                        sqlCommand.Parameters.AddWithValue("@GGUN", (object)(pdfHizmetListesi.GGun.Substring(0, pdfHizmetListesi.GGun.Length - 2) + "/" + pdfHizmetListesi.GGun.Substring(pdfHizmetListesi.GGun.Length - 2)));
                    if (pdfHizmetListesi.CGun == "")
                        sqlCommand.Parameters.AddWithValue("@CGUN", (object)"");
                    else
                        sqlCommand.Parameters.AddWithValue("@CGUN", (object)(pdfHizmetListesi.CGun.Substring(0, pdfHizmetListesi.CGun.Length - 2) + "/" + pdfHizmetListesi.CGun.Substring(pdfHizmetListesi.CGun.Length - 2)));
                    sqlCommand.Parameters.AddWithValue("@EGS", (object)pdfHizmetListesi.Egn);
                    sqlCommand.Parameters.AddWithValue("@ICN", (object)pdfHizmetListesi.Icn);
                    sqlCommand.Parameters.AddWithValue("@MESLEK_KODU", (object)pdfHizmetListesi.MeslekKodu);
                    sqlCommand.Parameters.AddWithValue("@KANUN_NO", (object)pdfHizmetListesi.KanunNo.Replace('(', '-').ToString().Trim().ToString().Replace(')', ' ').ToString().Trim());
                    sqlCommand.Parameters.AddWithValue("@BELGE_CESIDI", (object)pdfHizmetListesi.BelgeTuru);
                    sqlCommand.Parameters.AddWithValue("@BELGETURU", (object)pdfHizmetListesi.BelgeTuru);
                    sqlCommand.Parameters.AddWithValue("@ONAYBEKLEYEN", (object)0);
                    sqlCommand.Parameters.AddWithValue("@MAHIYET", (object)pdfHizmetListesi.Mahiyet);
                    sqlCommand.Parameters.AddWithValue("@firmaid", firmid);
                    sqlCommand.Parameters.AddWithValue("@subid", subid);

                    int gun = Convert.ToInt32(pdfHizmetListesi.Gun.Trim());
                    string yill = (pdfHizmetListesi.Donem.Split('/')[0].ToString().Trim());
                    string ayy = (pdfHizmetListesi.Donem.Split('/')[1].ToString().Trim());
                    string tcn = pdfHizmetListesi.SgkNo.Trim();
                    sqlCommand.Parameters.AddWithValue("@Persid", yill + "" + ayy + "" + tcn);
                    sqlCommand.Parameters.AddWithValue("@firmPersid", firmid + "" + subid + "" + yill + "" + ayy + "" + tcn);
                    sqlCommand.Parameters.AddWithValue("@DONEM", (object)(pdfHizmetListesi.Donem.ToString().Trim()));

                    string kanun = pdfHizmetListesi.KanunNo.Replace('(', '-').ToString().Trim().ToString().Replace(')', ' ').ToString().Trim();
                    if (kanun == "00687" || kanun == "01687" || kanun == "17103" || kanun == "27103")
                    {


                        SQLiteCommand frm = new SQLiteCommand("select * from agi_tablosu where agi_yil='" + yill + "' ", sqlConnection);
                        SQLiteDataReader da = frm.ExecuteReader();
                        while (da.Read())
                        {

                            var minAgitutari = (Convert.ToDouble(da[2]));
                            var asUcrGvTut = (Convert.ToDouble(da[4]));
                            var gvterkinmax = (Convert.ToDouble(da[5]));
                            var dvterkin = (Convert.ToDouble(da[6]));
                            var auterkmatrahi = Convert.ToDouble((asUcrGvTut / 30) * Convert.ToInt32(gun));
                            sqlCommand.Parameters.AddWithValue("@agimin", minAgitutari);
                            sqlCommand.Parameters.AddWithValue("@augv", asUcrGvTut);
                            sqlCommand.Parameters.AddWithValue("@trkMatrh", auterkmatrahi);
                            if (((asUcrGvTut / 30) * Convert.ToInt32(gun)) >= asUcrGvTut)
                            {
                                sqlCommand.Parameters.AddWithValue("@gvterkin", Math.Round(Convert.ToDouble(gvterkinmax),2));
                            }
                            else if (((asUcrGvTut / 30) * Convert.ToInt32(gun) >= minAgitutari && (asUcrGvTut / 30) * Convert.ToInt32(gun) < asUcrGvTut))
                            {
                                sqlCommand.Parameters.AddWithValue("@gvterkin", Math.Round(Convert.ToDouble(auterkmatrahi - minAgitutari),2));
                            }
                            else
                            {
                                sqlCommand.Parameters.AddWithValue("@gvterkin", 0);
                            }
                            sqlCommand.Parameters.AddWithValue("@dvterkin", Math.Round(Convert.ToDouble((dvterkin/30)*gun),2));
                        }

                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@agimin", 0);
                        sqlCommand.Parameters.AddWithValue("@augv", 0);
                        sqlCommand.Parameters.AddWithValue("@trkMatrh", 0);
                        sqlCommand.Parameters.AddWithValue("@gvterkin", 0);
                        sqlCommand.Parameters.AddWithValue("@dvterkin", 0);

                    }



                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.ExecuteNonQuery();
                    if (pdfHizmetListesi.Mahiyet == "IPTAL")      //Convert.ToInt32(                                              //Convert.ToInt32
                        KayitIptalListeEkle(pdfHizmetListesi.Donem.Split('/')[0].Trim().ToString(), pdfHizmetListesi.Donem.Split('/')[1].Trim().ToString(), pdfHizmetListesi.SgkNo, pdfHizmetListesi.KanunNo, pdfHizmetListesi.CGun, pdfHizmetListesi.GGun, pdfHizmetListesi.Ucret);
                    //---------------------------------------------------------------------------------------------------

                    ++num1;
                }
            }
        }

        public static int firmid { get; set; }
        public static int subid { get; set; }

        private void PdfOku(string Dosya)
        {
            this.rbMetin.Text = "";
            //  PdfHizmetListesi pdfHizmetListesi1 = new PdfHizmetListesi();
            HizmetGenelBilgi hizmetGenelBilgi = new HizmetGenelBilgi();
            this.ExtractText(Dosya, Dosya);
            this.rbMetin.Text = this.rbMetin.Text.Replace("Ý", "İ");
            this.rbMetin.Text = this.rbMetin.Text.Replace("þ", "ş");
            this.rbMetin.Text = this.rbMetin.Text.Replace("ý", "ı");
            this.rbMetin.Text = this.rbMetin.Text.Replace("Þ", "Ş");
            this.rbMetin.Text = this.rbMetin.Text.Replace("Ð", "Ğ");
            this.rbMetin.Text = this.rbMetin.Text.Replace("ð", "ğ");
            bool flag = false;

            for (int index1 = 6; index1 < this.rbMetin.Lines.Length; ++index1)
            {
                if (index1 == 6)
                {
                    hizmetGenelBilgi = new HizmetGenelBilgi();
                    string str = this.rbMetin.Lines[index1].ToString();
                    int index2 = 0;
                    while (true)
                    {
                        if (index2 < str.Split(' ').Length)
                        {
                            if (!string.IsNullOrEmpty(hizmetGenelBilgi.BelgeTuru) && !string.IsNullOrEmpty(hizmetGenelBilgi.Donem) && !string.IsNullOrEmpty(hizmetGenelBilgi.KanunNo) && !string.IsNullOrEmpty(hizmetGenelBilgi.Mahiyet))
                                index2 = str.Split(' ').Length;
                            //else if (str.Split(' ')[index2] == "kapıno")
                            //    hizmetGenelBilgi.SgkIsyeri = str.Split(' ')[index2 + 1].ToString().Trim();
                            else if (str.Split(' ')[index2] == "SGM(kod-ad)")
                                hizmetGenelBilgi.Donem = str.Split(' ')[index2 - 1].ToString().Trim();
                            else if (str.Split(' ')[index2].IndexOf("İşyeri") != -1)
                                hizmetGenelBilgi.BelgeTuru = str.Split(' ')[index2 + 2].ToString().Trim();
                            else if (str.Split(' ')[index2] == "Mahiyet")
                            {
                                int index3 = index2 + 1;
                                hizmetGenelBilgi.Mahiyet = str.Split(' ')[index3].ToString().Trim();
                                index2 = index3 + 1;
                                while (!flag)
                                {
                                    if (str.Split(' ')[index2].ToString().Trim() == "Kanun")
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        hizmetGenelBilgi.KanunNo += str.Split(' ')[index2].ToString().Trim();
                                        ++index2;
                                    }
                                }
                                flag = false;
                            }
                            ++index2;
                        }
                        else
                            break;
                    }
                }
                if (this.rbMetin.Lines[index1].Trim() != "" && this.rbMetin.Lines[index1].ToString().IndexOf("T.C. ÇALI") == -1)
                {
                    string str1 = this.rbMetin.Lines[index1].ToString();
                    string str2 = str1.Substring(str1.IndexOf("Meslek Kod") + 11, str1.IndexOf("Sayfa i") - str1.IndexOf("Meslek Kod") - 11);
                    int index2 = 0;
                    while (true)
                    {
                        if (index2 < str2.Split(' ').Length)
                        {
                            if (!string.IsNullOrEmpty(str2.Split(' ')[index2].ToString()))
                            {
                                try
                                {
                                    PdfHizmetListesi pdfHizmetListesi2 = new PdfHizmetListesi();
                                    pdfHizmetListesi2.Donem = hizmetGenelBilgi.Donem.Trim();
                                    pdfHizmetListesi2.BelgeTuru = hizmetGenelBilgi.BelgeTuru.Trim();
                                    pdfHizmetListesi2.Mahiyet = hizmetGenelBilgi.Mahiyet.Trim();
                                    pdfHizmetListesi2.KanunNo = hizmetGenelBilgi.KanunNo.Trim();
                                    //pdfHizmetListesi2.SgkIsyeri = hizmetGenelBilgi.SgkIsyeri.Trim();//-------- eklenen alan
                                    pdfHizmetListesi2.SiraNo = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.SgkNo = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.Ad = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.Soyad = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    while (true)
                                    {
                                        if (!this.SayisalMi(str2.Split(' ')[index2].ToString()))
                                        {
                                            pdfHizmetListesi2.IlkSoyad = str2.Split(' ')[index2].ToString();
                                            ++index2;
                                        }
                                        else
                                            break;
                                    }
                                    if (pdfHizmetListesi2.IlkSoyad == null)
                                        pdfHizmetListesi2.IlkSoyad = "";
                                    pdfHizmetListesi2.Ucret = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.Ikramiye = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.Gun = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.UCG = str2.Split(' ')[index2].ToString();                    // değiştirilen alan 
                                    ++index2;//değiştirilen alan 
                                    pdfHizmetListesi2.EksikGun = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.GGun = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.CGun = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.Icn = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.Egn = str2.Split(' ')[index2].ToString();
                                    ++index2;
                                    pdfHizmetListesi2.MeslekKodu = str2.Split(' ')[index2].ToString();
                                    if (Convert.ToInt32(pdfHizmetListesi2.GGun) == 0)
                                        pdfHizmetListesi2.GGun = "";
                                    if (Convert.ToInt32(pdfHizmetListesi2.CGun) == 0)
                                        pdfHizmetListesi2.CGun = "";
                                    this.HList.Add(pdfHizmetListesi2);
                                }
                                catch { }
                            }
                            ++index2;
                        }
                        else
                            // goto label_38;
                            break;
                    }
                }
            }
        }

        public bool ExtractText(string inFileName, string outFileName)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            try
            {
                PdfReader pdfReader = new PdfReader(inFileName);
                if (!Directory.Exists("C:\\Temp"))
                    Directory.CreateDirectory("C:\\Temp");
                streamWriter = new StreamWriter("C:\\Temp\\a.txt", false, Encoding.UTF8);
                int num1 = 68;
                float num2 = (float)num1 / (float)pdfReader.NumberOfPages;
                int num3 = 0;
                float num4 = 0.0f;
                for (int pageNum = 1; pageNum <= pdfReader.NumberOfPages; ++pageNum)
                {
                    streamWriter.Write(this.ExtractTextFromPDFBytes(pdfReader.GetPageContent(pageNum)) + " ");
                    this.rbMetin.Text = this.rbMetin.Text + Environment.NewLine + this.ExtractTextFromPDFBytes(pdfReader.GetPageContent(pageNum));
                    if ((double)num2 >= 1.0)
                    {
                        for (int index = 0; index < (int)num2; ++index)
                        {
                            Console.Write("#");
                            ++num3;
                        }
                    }
                    else
                    {
                        num4 += num2;
                        if ((double)num4 >= 1.0)
                        {
                            for (int index = 0; index < (int)num4; ++index)
                            {
                                Console.Write("#");
                                ++num3;
                            }
                            num4 = 0.0f;
                        }
                    }
                }
                if (num3 < num1)
                {
                    for (int index = 0; index < num1 - num3; ++index)
                        Console.Write("#");
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        private string ExtractTextFromPDFBytes(byte[] input)
        {
            if (input == null || input.Length == 0)
                return "";
            try
            {
                string str = "";
                bool flag1 = false;
                bool flag2 = false;
                int num1 = 0;
                char[] recent = new char[_numberOfCharsToKeep];
                for (int index = 0; index < _numberOfCharsToKeep; ++index)
                    recent[index] = ' ';
                for (int index1 = 0; index1 < input.Length; ++index1)
                {
                    char ch = (char)input[index1];
                    if (flag1)
                    {
                        if (num1 == 0)
                        {
                            if (this.CheckToken(new string[2]
                            {
                "TD",
                "Td"
                            }, recent))
                                str += "\n\r";
                            else if (this.CheckToken(new string[3]
                            {
                "'",
                "T*",
                "\""
                            }, recent))
                                str += "\n";
                            else if (this.CheckToken(new string[1] { "Tj" }, recent))
                                str += " ";
                        }
                        int num2;
                        if (num1 == 0)
                            num2 = !this.CheckToken(new string[1] { "ET" }, recent) ? 1 : 0;
                        else
                            num2 = 1;
                        if (num2 == 0)
                        {
                            flag1 = false;
                            str += " ";
                        }
                        else if (ch == '(' && num1 == 0 && !flag2)
                            num1 = 1;
                        else if (ch == ')' && num1 == 1 && !flag2)
                            num1 = 0;
                        else if (num1 == 1)
                        {
                            if (ch == '\\' && !flag2)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                if (ch >= ' ' && ch <= '~' || ch >= '\x0080' && ch < 'ÿ')
                                    str += ch.ToString();
                                flag2 = false;
                            }
                        }
                    }
                    for (int index2 = 0; index2 < _numberOfCharsToKeep - 1; ++index2)
                        recent[index2] = recent[index2 + 1];
                    recent[_numberOfCharsToKeep - 1] = ch;
                    int num3;
                    if (!flag1)
                        num3 = !this.CheckToken(new string[1] { "BT" }, recent) ? 1 : 0;
                    else
                        num3 = 1;
                    if (num3 == 0)
                        flag1 = true;
                }
                return str;
            }
            catch
            {
                return "";
            }
        }

        private bool CheckToken(string[] tokens, char[] recent)
        {
            foreach (string token in tokens)
            {
                if ((int)recent[_numberOfCharsToKeep - 3] == (int)token[0] && (int)recent[_numberOfCharsToKeep - 2] == (int)token[1] && (recent[_numberOfCharsToKeep - 1] == ' ' || recent[_numberOfCharsToKeep - 1] == '\r' || recent[_numberOfCharsToKeep - 1] == '\n') && (recent[_numberOfCharsToKeep - 4] == ' ' || recent[_numberOfCharsToKeep - 4] == '\r' || recent[_numberOfCharsToKeep - 4] == '\n'))
                    return true;
            }
            return false;
        }


        public bool SayisalMi(string value)
        {
            double result = 0.0;
            bool flag = double.TryParse(value, out result);
            if (flag && value.IndexOf(',') == -1)
                flag = false;
            return flag;
        }

        private static SQLiteConnection ConnectionVer()
        {
            var sqlConnection = new SQLiteConnection();
            sqlConnection.ConnectionString = ConfigurationManager.AppSettings["Baglanti"];
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
            return sqlConnection;
        }

        private static void KayitIptalListeEkle(
          string Yil,
          string Ay,
          string SgkNo,
          string KanunNo,
          string CGun,
          string GGun,
          string Maas)
        {
            if (KanunNo == "06111")
                KanunNo = "06111 -05510";
            else if (KanunNo == "44447")
                KanunNo = "44447 -05510";
            IptalKayitlar iptalKayitlar = new IptalKayitlar()
            {
                DonemYil1 = Convert.ToInt32(Yil),
                DonemAy1 = Convert.ToInt32(Ay),
                SgkNo1 = SgkNo,
                KanunNo1 = KanunNo.Replace('(', '-').Trim().Replace(')', ' ').Trim(),
                CGun1 = CGun,
                GGun = GGun,
                Maas = ParaBirimi.PBCevir(Maas.Replace(".", "").Trim())
            };
            SilinecekIptalKayitlar.Add(iptalKayitlar);
        }

    }

}
