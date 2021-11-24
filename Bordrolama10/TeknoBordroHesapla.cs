using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bordrolama10
{
    public partial class TeknoBordroHesapla : Form
    {
        public TeknoBordroHesapla()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        static int frmId = -1;
        static string dnm = "";
        static int sbId = -1;
        static string prsId = "";

        /// <summary>
        /// FORM LOAD İŞLEMİ YAPILIYOR 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeknoBordroHesapla_Load(object sender, EventArgs e)
        {

            frmId = 10;
            sbId = 7;
            txtFirmaUnvan.Text = programreferans.firmaunvan;
            txtSubeUnvan.Text = programreferans.subeunvan;
            // Şube işlemleri içerisine bu formun açılması için buton ekle, seçili şube ve firma no bilgisi ile load olsun 
            // klasör varmı yokmu kontrol ediyor
            string klasorAdiFirmaUnvani = "MONAD BİLİŞİM".Substring(0, 10);//txtFirmaUnvan.Text
            bool klasor = Directory.Exists(Application.StartupPath + "\\TeknoParkIslemleri");
            if (!klasor)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\TeknoParkIslemleri");
            }
            // firma klasörü varmı yokmu onu kontrol ediyor 
            bool firmaKlasor = Directory.Exists(Application.StartupPath + "\\TeknoParkIslemleri\\" + klasorAdiFirmaUnvani);
            if (!firmaKlasor)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\TeknoParkIslemleri\\" + klasorAdiFirmaUnvani);
            }
            txtPdfKayitYeri.Text = Application.StartupPath + "\\TeknoParkIslemleri\\" + klasorAdiFirmaUnvani;


            cmbKanunMd.Text = "5746";
            cmbTumKanunlar.Text = "Evet";
            chkAsılEkIptal.SetItemChecked(0, true);
            chkAsılEkIptal.SetItemChecked(1, true);
            chkAsılEkIptal.SetItemChecked(2, true);

            baglan.Open();
            SQLiteCommand cmbdonem = new SQLiteCommand("select * from DonemBilgisi", baglan);
            SQLiteDataReader dr1 = cmbdonem.ExecuteReader();
            while (dr1.Read())
            {
                cmbilk.Items.Add(dr1[3]);
                cmbson.Items.Add(dr1[3]);
            }
            baglan.Close();
        }

        private void asilIptalEkBildirgeleriAyikla()// Asıl İptal Ek Bildirgeleri Ayrıştır
        {
            // Asıl İptal ve Ek Bildirgeler Ayıklanıyor 


            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT hzmlstid,KurumID,Year,Month,SgkNo,Ad,Soyad,IlkSoyad, Ucret,Ikramiye,Gun,UCG,Eksik_Gun,GGun,CGun,Egs,Icn,Meslek_Kodu,Kanun_No,Belge_Cesidi,Belge_Turu,OnayBekleyen,Mahiyet,Donem, subeid,firmaid,personelid,firmPersid as ID  From HizmetListesi where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable HzmtListesiHL = new DataTable();
            da.Fill(HzmtListesiHL);

            SQLiteDataAdapter Hsplda = new SQLiteDataAdapter("SELECT count(SgkNo) as sayi from  HizmetListesiIptalsiz where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable yeniHizmetListPersonelVarmi = new DataTable();
            Hsplda.Fill(yeniHizmetListPersonelVarmi);
            baglan.Close();
            // HİZMETLİSTESİ DAHA ÖNCEDEN AYIKLANDI İSE KONTROL EDER,, EVET DERSEN SİLER YENİDEN OLUŞTURUR
            if (Convert.ToInt32(yeniHizmetListPersonelVarmi.Rows[0][0]) > 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Asıl Ek İptal Hizmet Listeleri Daha Önce Ayrıştırılmış, Listeyi Silerek Yeniden oluşturmak istiyormusunuz.", "Dikkat", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    baglan.Open();
                    SQLiteCommand komut = new SQLiteCommand("Delete from HizmetListesiIptalsiz where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show(txtFirmaUnvan.Text + " \n Firmasına ait tüm veriler silinmiştir");
                }
            }
            prgrsBrAsilEkIptal.Maximum = HzmtListesiHL.Rows.Count;
            for (int i = 0; i < HzmtListesiHL.Rows.Count; i++)
            {
                prgrsBrAsilEkIptal.Value = i;
                string FrmPrId = "";
                string Kanun = "";
                FrmPrId = HzmtListesiHL.Rows[i]["ID"].ToString();


                Kanun = HzmtListesiHL.Rows[i]["Kanun_No"].ToString();
                decimal Ucret = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Ucret"]);
                decimal Ikramiye = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Ikramiye"]);
                string Mahiyet = HzmtListesiHL.Rows[i]["Mahiyet"].ToString();
                string gun = HzmtListesiHL.Rows[i]["Gun"].ToString();

                if (HzmtListesiHL.Rows[i]["Mahiyet"].ToString().Contains("IPTAL"))
                {

                    string tespEdIptalPersohelId = "";
                    string tespEdIptalPersohelkanun = "";
                    string tespedIptalPersohelGun = "";
                    decimal tespEdIptalPersohelucret = 0;
                    decimal tespEdIptalPersohelIkramiye = 0;


                    var asılIptalEkAyristir = HzmtListesiHL.Select("ID='" + FrmPrId + "'");
                    foreach (var iptaliOlanPersonel in asılIptalEkAyristir)
                    {

                        tespEdIptalPersohelId = iptaliOlanPersonel["ID"].ToString();
                        tespEdIptalPersohelkanun = iptaliOlanPersonel["Kanun_No"].ToString();
                        tespedIptalPersohelGun = iptaliOlanPersonel["Gun"].ToString();
                        tespEdIptalPersohelucret = Convert.ToDecimal(iptaliOlanPersonel["Ucret"]);
                        tespEdIptalPersohelIkramiye = Convert.ToDecimal(iptaliOlanPersonel["Ikramiye"]);
                        if (FrmPrId == tespEdIptalPersohelId && tespEdIptalPersohelkanun == Kanun && tespEdIptalPersohelucret == Ucret && tespEdIptalPersohelIkramiye == Ikramiye && tespedIptalPersohelGun == gun)
                        {
                            iptaliOlanPersonel.Delete();
                            HzmtListesiHL.Rows[i].Delete();
                            break;
                        }

                    }

                }
            }
            HzmtListesiHL.AcceptChanges();
            System.Threading.Thread.Sleep(500);
            lblAsilIptalEkAyristir.Text = "Veritabanına Kayıt İşlemi Başladı.. Lütfen Bekleyiniz.";
            System.Threading.Thread.Sleep(500);

            prgrsBrAsilEkIptal.Maximum = HzmtListesiHL.Rows.Count;
            for (int j = 0; j < HzmtListesiHL.Rows.Count; j++)
            {
                prgrsBrAsilEkIptal.Value = j;
                int HzListId = Convert.ToInt32(HzmtListesiHL.Rows[j]["hzmlstid"]);
                int KurumID = Convert.ToInt32(HzmtListesiHL.Rows[j]["KurumID"]);
                string Yil = HzmtListesiHL.Rows[j]["Year"].ToString();
                string Ay = HzmtListesiHL.Rows[j]["Month"].ToString();
                string SgkNo = HzmtListesiHL.Rows[j]["SgkNo"].ToString();
                string Ad = HzmtListesiHL.Rows[j]["Ad"].ToString();
                string Soyad = HzmtListesiHL.Rows[j]["Soyad"].ToString();
                string IlkSoyad = HzmtListesiHL.Rows[j]["IlkSoyad"].ToString();
                decimal Ucret = Convert.ToDecimal(HzmtListesiHL.Rows[j]["Ucret"]);
                decimal Ikramiye = Convert.ToDecimal(HzmtListesiHL.Rows[j]["Ikramiye"]);
                string Gun = HzmtListesiHL.Rows[j]["Gun"].ToString();
                string UCG = HzmtListesiHL.Rows[j]["UCG"].ToString();
                string Eg = HzmtListesiHL.Rows[j]["Eksik_Gun"].ToString();
                string GGun = HzmtListesiHL.Rows[j]["GGun"].ToString();
                string CGun = HzmtListesiHL.Rows[j]["CGun"].ToString();
                string Egs = HzmtListesiHL.Rows[j]["Egs"].ToString();
                string Icn = HzmtListesiHL.Rows[j]["Icn"].ToString();
                string M_Kodu = HzmtListesiHL.Rows[j]["Meslek_Kodu"].ToString();
                string KnnNo = HzmtListesiHL.Rows[j]["Kanun_No"].ToString();
                string Bc = HzmtListesiHL.Rows[j]["Belge_Cesidi"].ToString();
                string Bt = HzmtListesiHL.Rows[j]["Belge_Turu"].ToString();
                string OnayBklyn = HzmtListesiHL.Rows[j]["OnayBekleyen"].ToString();
                string Mahiyet = HzmtListesiHL.Rows[j]["Mahiyet"].ToString();
                string Donem = HzmtListesiHL.Rows[j]["Donem"].ToString();
                int firmaid = Convert.ToInt32(HzmtListesiHL.Rows[j]["firmaid"]);
                int subeid = Convert.ToInt32(HzmtListesiHL.Rows[j]["subeid"]);
                string FrmPrId = HzmtListesiHL.Rows[j]["ID"].ToString();

                baglan.Open();
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [HizmetListesiIptalsiz] (HzListId,KurumID,Yil,Ay,SgkNo,Ad,Soyad,IlkSoyad,Ucret,Ikramiye,Gun,UCG,Eg,GGun,CGun,Egs,Icn,M_Kodu,KnnNo,Bc,Bt,OnayBklyn,Mahiyet,Donem,firmaid,subeid,FrmPrId) values (@HzListId, @KurumID,@Yil,@Ay, @SgkNo,@Ad,@Soyad,@IlkSoyad,@Ucret,@Ikramiye,@Gun,@UCG,@Eg,@GGun,@CGun,@Egs,@Icn,@M_Kodu,@KnnNo,@Bc,@Bt,@OnayBklyn,@Mahiyet,@Donem,@firmaid,@subeid,@FrmPrId)", baglan);
                //HzListId,KurumID,Yil,Ay,SgkNo,Ad,Soyad,IlkSoyad,Ucret,Ikramiye,Gun,UCG,Eg,GGun,CGun,Egs,Icn,M_Kodu,KnnNo,Bc,Bt,OnayBklyn,Mahiyet,Donem,firmaid,subeid,FrmPrId
                //@HzListId, @KurumID,@Yil,@Ay, @SgkNo,@Ad,@Soyad,@IlkSoyad,@Ucret,@Ikramiye,@Gun,@UCG,@Eg,@GGun,@CGun,@Egs,@Icn,@M_Kodu,@KnnNo,@Bc,@Bt,@OnayBklyn,@Mahiyet,@Donem,@firmaid,@subeid,@FrmPrId



                ekle.Parameters.AddWithValue("@HzListId", HzListId);
                ekle.Parameters.AddWithValue("@KurumID", KurumID);
                ekle.Parameters.AddWithValue("@Yil", Yil);
                ekle.Parameters.AddWithValue("@Ay", Ay);
                ekle.Parameters.AddWithValue("@SgkNo", SgkNo);
                ekle.Parameters.AddWithValue("@Ad", Ad);
                ekle.Parameters.AddWithValue("@Soyad", Soyad);
                ekle.Parameters.AddWithValue("@IlkSoyad", IlkSoyad);
                ekle.Parameters.AddWithValue("@Ucret", Ucret);
                ekle.Parameters.AddWithValue("@Ikramiye", Ikramiye);
                ekle.Parameters.AddWithValue("@Gun", Gun);
                ekle.Parameters.AddWithValue("@UCG", UCG);
                ekle.Parameters.AddWithValue("@Eg", Eg);
                ekle.Parameters.AddWithValue("@GGun", GGun);
                ekle.Parameters.AddWithValue("@CGun", CGun);
                ekle.Parameters.AddWithValue("@Egs", Egs);
                ekle.Parameters.AddWithValue("@Icn", Icn);
                ekle.Parameters.AddWithValue("@M_Kodu", M_Kodu);
                ekle.Parameters.AddWithValue("@KnnNo", KnnNo);
                ekle.Parameters.AddWithValue("@Bc", Bc);
                ekle.Parameters.AddWithValue("@Bt", Bt);
                ekle.Parameters.AddWithValue("@OnayBklyn", OnayBklyn);
                ekle.Parameters.AddWithValue("@Mahiyet", Mahiyet);
                ekle.Parameters.AddWithValue("@Donem", Donem);
                ekle.Parameters.AddWithValue("@firmaid", firmaid);
                ekle.Parameters.AddWithValue("@subeid", subeid);
                ekle.Parameters.AddWithValue("@FrmPrId", FrmPrId);



                ekle.ExecuteNonQuery();
                baglan.Close();
            }
        }
        private void HizmetListesindenTeknoPersListesiOluszur()// Tekno Park Personel Listesi oluştur
        {
            // AYLIK PRİM VE HİZMET BELGESİNDEN TEKNOPARK PERSONELİ LİSETİS OLUŞTURULUYOR
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT DISTINCT sgkno, ad, soyad,IlkSoyad,firmaid,subeid From HizmetListesi where firmaid ='" + frmId + "' and subeid='" + sbId + "' and Kanun_No like '%5746%'", baglan);
            DataTable TeknoPersListesi = new DataTable();
            da.Fill(TeknoPersListesi);

            SQLiteDataAdapter Hsplda = new SQLiteDataAdapter("SELECT count(TknPrsId) as sayi from  Tekno5746PersonelListesi where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable TeknoPersonelListesi = new DataTable();
            Hsplda.Fill(TeknoPersonelListesi);
            baglan.Close();

            if (Convert.ToInt32(TeknoPersonelListesi.Rows[0][0]) > 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Asıl Ek İptal Hizmet Listeleri Daha Önce Ayrıştırılmış, Listeyi Silerek Yeniden oluşturmak istiyormusunuz.", "Dikkat", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    baglan.Open();
                    SQLiteCommand komut = new SQLiteCommand("Delete from Tekno5746PersonelListesi where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show(txtFirmaUnvan.Text.Substring(0, 20) + "  \n Firmasına ait tüm veriler silinmiştir");
                }
            }
            prgrsBrTeknoPrsOlustur.Maximum = TeknoPersListesi.Rows.Count-1;
            for (int i = 0; i < TeknoPersListesi.Rows.Count; i++)
            {
                prgrsBrTeknoPrsOlustur.Value = i;


                string teknoPersSgkNo = TeknoPersListesi.Rows[i]["SgkNo"].ToString();
                string tknoPersAd = TeknoPersListesi.Rows[i]["Ad"].ToString();
                string tknoPersSoyad = TeknoPersListesi.Rows[i]["Soyad"].ToString();
                string tknoPersIlkSoyad = TeknoPersListesi.Rows[i]["IlkSoyad"].ToString();
                string tknoPersFirmaid = TeknoPersListesi.Rows[i]["firmaid"].ToString();
                string tknoPersSubeid = TeknoPersListesi.Rows[i]["subeid"].ToString();

                baglan.Open();
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [Tekno5746PersonelListesi] (SgkNo,Ad,Soyad,IlkSoyad,firmaid,subeid) values (@SgkNo, @Ad, @Soyad,@IlkSoyad, @firmaid,@subeid)", baglan);

                ekle.Parameters.AddWithValue("@SgkNo", teknoPersSgkNo);
                ekle.Parameters.AddWithValue("@Ad", tknoPersAd);
                ekle.Parameters.AddWithValue("@Soyad", tknoPersSoyad);
                ekle.Parameters.AddWithValue("@IlkSoyad", tknoPersIlkSoyad);
                ekle.Parameters.AddWithValue("@firmaid", tknoPersFirmaid);
                ekle.Parameters.AddWithValue("@subeid", tknoPersSubeid);


                ekle.ExecuteNonQuery();
                baglan.Close();


            }


        }




        private void btnAsilEkİptalAyikla_Click_1(object sender, EventArgs e)
        {
            lblAsilIptalEkAyristir.Text = "İşlem Başladı Lütfen Bekleyiniz";
            asilIptalEkBildirgeleriAyikla();
            lblAsilIptalEkAyristir.Text = "İşlem Başarı İle Tamamlandı .... ";
        }

        private void btnTeknPrkPersOlustur_Click(object sender, EventArgs e)
        {
            lblTeknPrkPerOlustur.Text = "İşlem Başladı Lütfen Bekleyiniz";
            HizmetListesindenTeknoPersListesiOluszur();
            lblTeknPrkPerOlustur.Text = "İşlem Başarı İle Tamamlandı .... ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TeknoPersListesiYukle tknPrsListesi = new TeknoPersListesiYukle();
            tknPrsListesi.Show();
        }
    }
}

