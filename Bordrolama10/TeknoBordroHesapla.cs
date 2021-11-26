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
        int teknoPersonelSayisi = 0;
        int HizmetListePersonelSayisi = 0;
        int IPTALHizmetListePersonelSayisi = 0;
        int yeniHizmetListesiPersonelSayisi = 0;
        int teknoBordroCalisanSayisi = 0;


        static int frmId = -1;
        static string dnm = "";
        static int sbId = -1;
        static string prsId = "";
        DataTable TabanTavan = new DataTable();
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
            prgrsBrAsilEkIptal.Maximum = HzmtListesiHL.Rows.Count - 1;
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

            baglan.Open();
            SQLiteDataAdapter daa = new SQLiteDataAdapter("SELECT *  From Tekno5746PersonelListesi where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable teknoPersListesi = new DataTable();
            daa.Fill(teknoPersListesi);
            baglan.Close();

            System.Threading.Thread.Sleep(500);
            lblAsilIptalEkAyristir.Text = "Veritabanına Kayıt İşlemi Başladı.. Lütfen Bekleyiniz.";
            System.Threading.Thread.Sleep(500);

            prgrsBrAsilEkIptal.Maximum = HzmtListesiHL.Rows.Count - 1;
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

                var teknoPersoneli = teknoPersListesi.Select("SgkNo='" + SgkNo + "'");
                string TcNo = "";
                string TeknoPersoneli = "";
                foreach (var personel in teknoPersoneli)
                    TcNo = personel["SgkNo"].ToString();
                if (SgkNo == TcNo)
                {
                    TeknoPersoneli = "Evet";
                }
                else
                {
                    TeknoPersoneli = "Hayır";
                }

                baglan.Open();
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [HizmetListesiIptalsiz] (HzListId,KurumID,Yil,Ay,SgkNo,Ad,Soyad,IlkSoyad,Ucret,Ikramiye,Gun,UCG,Eg,GGun,CGun,Egs,Icn,M_Kodu,KnnNo,Bc,Bt,OnayBklyn,Mahiyet,Donem,firmaid,subeid,FrmPrId,TeknoPersoneli) values (@HzListId, @KurumID,@Yil,@Ay, @SgkNo,@Ad,@Soyad,@IlkSoyad,@Ucret,@Ikramiye,@Gun,@UCG,@Eg,@GGun,@CGun,@Egs,@Icn,@M_Kodu,@KnnNo,@Bc,@Bt,@OnayBklyn,@Mahiyet,@Donem,@firmaid,@subeid,@FrmPrId,@TeknoPersoneli)", baglan);

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
                ekle.Parameters.AddWithValue("@TeknoPersoneli", TeknoPersoneli);


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
            prgrsBrTeknoPrsOlustur.Maximum = TeknoPersListesi.Rows.Count - 1;
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
        private void Tekno5746veDigerKanundanFaydalananlarıAyristir()
        {

            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT HzListId,KurumID,Yil,Ay,SgkNo,Ad,Soyad,IlkSoyad,Ucret,Ikramiye,Gun,UCG,Eg,GGun,CGun,Egs,Icn,M_Kodu,KnnNo,Bc,Bt,OnayBklyn,Mahiyet,Donem,FrmPrId,firmaid,subeid,TeknoPersoneli from HizmetListesiIptalsiz where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable iptalsizHizmetListesi = new DataTable();
            da.Fill(iptalsizHizmetListesi);
            baglan.Close();
            TeknoBordroCalisanSayisi();
            if (teknoBordroCalisanSayisi > 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Hizmet Listesi Daha Önceden aktarılmış, Listeyi Silerek Yeniden oluşturmak istiyormusunuz.", "Dikkat", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    baglan.Open();
                    SQLiteCommand komut = new SQLiteCommand("Delete from Bordro5746 where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
                    komut.ExecuteNonQuery();
                    MessageBox.Show(txtFirmaUnvan.Text.Substring(0, 20) + " Firmasına ait tüm veriler silinmiştir");
                    baglan.Close();
                }
            }


            // 5746 KANUN MADDESİNDEN VE AYNI ZAMANDA DİĞER KANUN MADDELERİNDEN FAYDALANMIŞ PERSONELİ AYRIŞTIRIYOR. 
            iptalsizHizmetListesi.Columns.Add("ToplamaDahilmi");
            prgsBrCiftKanunluTeknoPers.Maximum = iptalsizHizmetListesi.Rows.Count - 1;
            for (int k = 0; k < iptalsizHizmetListesi.Rows.Count; k++)
            {
                prgsBrCiftKanunluTeknoPers.Value = k;

                string FrmPrId = "";
                string Kanun = "";
                string TeknoPersonel = "";
                string toplamaDahilMi = "Hayır";

                FrmPrId = iptalsizHizmetListesi.Rows[k]["FrmPrId"].ToString();
                TeknoPersonel = iptalsizHizmetListesi.Rows[k]["TeknoPersoneli"].ToString();
                var Tekno5746PersCiftMi = iptalsizHizmetListesi.Select("FrmPrId='" + FrmPrId + "'");


                foreach (var item in Tekno5746PersCiftMi)
                {
                    Kanun = item["KnnNo"].ToString();

                    if (Kanun.Contains("5746")) continue;

                }
                if (!Kanun.Contains("5746"))
                {
                    iptalsizHizmetListesi.Rows[k].Delete();
                }



            }
            iptalsizHizmetListesi.AcceptChanges();

            prgsBrCiftKanunluTeknoPers.Maximum = iptalsizHizmetListesi.Rows.Count - 1;

            for (int i = 0; i < iptalsizHizmetListesi.Rows.Count; i++)
            {
                prgsBrCiftKanunluTeknoPers.Value = i;
                int HzListId = Convert.ToInt32(iptalsizHizmetListesi.Rows[i]["HzListId"]);
                string FrmPrId = iptalsizHizmetListesi.Rows[i]["FrmPrId"].ToString();
                string Donem = iptalsizHizmetListesi.Rows[i]["Donem"].ToString();
                string SgkNo = iptalsizHizmetListesi.Rows[i]["SgkNo"].ToString();
                string Ad = iptalsizHizmetListesi.Rows[i]["Ad"].ToString();
                string Soyad = iptalsizHizmetListesi.Rows[i]["Soyad"].ToString();
                string Gun = iptalsizHizmetListesi.Rows[i]["Gun"].ToString();
                string KanunNo = iptalsizHizmetListesi.Rows[i]["KnnNo"].ToString();
                string Mahiyet = iptalsizHizmetListesi.Rows[i]["Mahiyet"].ToString();

                string toplamaDahilMi = iptalsizHizmetListesi.Rows[i]["toplamaDahilMi"].ToString();
                string TeknoPersonel = iptalsizHizmetListesi.Rows[i]["TeknoPersoneli"].ToString();
                decimal A_Ucret = Convert.ToDecimal(iptalsizHizmetListesi.Rows[i]["Ucret"]);
                decimal B_Ikramiye = Convert.ToDecimal(iptalsizHizmetListesi.Rows[i]["Ikramiye"]);
                decimal AB_Toplam = Convert.ToDecimal(iptalsizHizmetListesi.Rows[i]["Ucret"]) + Convert.ToDecimal(iptalsizHizmetListesi.Rows[i]["Ikramiye"]);

                int firmaid = Convert.ToInt32(iptalsizHizmetListesi.Rows[i]["firmaid"]);
                int subeid = Convert.ToInt32(iptalsizHizmetListesi.Rows[i]["subeid"]);

                decimal DigerKanun = 0;
                int ThkAdet = 0;


                var personelSayiveToplami = iptalsizHizmetListesi.Select("FrmPrId='" + FrmPrId + "'"); // seçili kanun maddesi personeli başka bir kanundan faydalandı ise seç


                foreach (var adet in personelSayiveToplami)
                {

                    string arananKanun = adet["KnnNo"].ToString();
                    if (!arananKanun.Contains("5746"))
                    {
                        DigerKanun += A_Ucret + B_Ikramiye;
                        ThkAdet += 1;
                    }
                    else
                    {
                        DigerKanun = B_Ikramiye;
                        ThkAdet += 1;
                    }

                }



                baglan.Open();
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [Bordro5746] (HzListId,TeknoPersonel, toplamaDahilMi,FrmPrId, Donem,SgkNo, Ad, Soyad,  Gun,KanunNo,Mahiyet,ThkAdet,A_Ucret,B_Ikramiye, AB_Toplam,DigerKanun,firmaid,subeid) values (@HzListId,@TeknoPersonel,@toplamaDahilMi, @FrmPrId, @Donem,@SgkNo, @Ad, @Soyad,@Gun,@KanunNo,@Mahiyet,@ThkAdet,@A_Ucret,@B_Ikramiye, @AB_Toplam,@DigerKanun,@firmaid,@subeid)", baglan);

                // Teknopark Personeli Evet ise bu alanda yer alsın ancak hesaba dahil etmesin 
                ekle.Parameters.AddWithValue("@HzListId", HzListId);// 5746 bordroya aktarma işleminde bu Idyi kullanacağız veya bordroyu yüklerken ona göre dizayn et ÖNEMLİ !!!
                ekle.Parameters.AddWithValue("@TeknoPersonel", TeknoPersonel);
                ekle.Parameters.AddWithValue("@toplamaDahilMi", toplamaDahilMi);
                ekle.Parameters.AddWithValue("@FrmPrId", FrmPrId);
                ekle.Parameters.AddWithValue("@Donem", Donem);
                ekle.Parameters.AddWithValue("@SgkNo", SgkNo);
                ekle.Parameters.AddWithValue("@Ad", Ad);
                ekle.Parameters.AddWithValue("@Soyad", Soyad);
                ekle.Parameters.AddWithValue("@Gun", Gun);
                ekle.Parameters.AddWithValue("@KanunNo", KanunNo);
                ekle.Parameters.AddWithValue("@Mahiyet", Mahiyet);
                ekle.Parameters.AddWithValue("@ThkAdet", ThkAdet);
                ekle.Parameters.AddWithValue("@A_Ucret", A_Ucret);
                ekle.Parameters.AddWithValue("@B_Ikramiye", B_Ikramiye);
                ekle.Parameters.AddWithValue("@AB_Toplam", AB_Toplam);
                ekle.Parameters.AddWithValue("@DigerKanun", DigerKanun);
                ekle.Parameters.AddWithValue("@firmaid", firmaid);
                ekle.Parameters.AddWithValue("@subeid", subeid);
                ekle.ExecuteNonQuery();
                baglan.Close();
                // ThkAdet
            }


        }// TEKNO ELEMAN EVET OLANLAR TEKRAR GÖZDEN GEÇECEK
        private void TeknoParkBordroHesapla()
        {

            SgkTabanTavan();
            DataTable Tekno5746Liste = new DataTable();
            DataTable FirmaBordro = new DataTable();
            DataTable ekOdemelerToplami = new DataTable();


            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT TeknoPrId,HzListId,TknoPrsMi,DahilMi,FrmPrId,Donem,SgkNo,Ad,Soyad,Gun,KanunNo,Mahiyet,ThkAdet,A_Ucret,B_Ikramiye,AB_Toplam,DigerKanun,firmaid,subeid from Bordro5746 where firmaid = '" + frmId + "' and subeid = '" + sbId + "'", baglan);
            da.Fill(Tekno5746Liste);

            using (SQLiteCommand sorgu = new SQLiteCommand("select FirmaPersId,PersId, PuantajDonem , TcNo,PersAdı,PersSoyadı,GirisTarihi,CikisTarihi,Net_Brüt,Net_BrtUcret,PrimGunu,AylikBrutUcret,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,DamgaVrg,(BesKesintisi+SairKesintiler) as Kesintiler,AylikNetUcret,KanunNo from FirmaBordro WHERE FirmaNo = '" + frmId + "' and SubeNo='" + sbId + "' ", baglan))
            {
                SQLiteDataAdapter dabdr = new SQLiteDataAdapter();
                dabdr.SelectCommand = sorgu;
                dabdr.Fill(FirmaBordro);
            }
            baglan.Close();


            prgTeknoHesapla.Maximum = Tekno5746Liste.Rows.Count - 1;
            for (int k = 0; k < Tekno5746Liste.Rows.Count; k++)
            {
                prgTeknoHesapla.Value = k;

                int TeknoPrId = Convert.ToInt32(Tekno5746Liste.Rows[k]["TeknoPrId"]);
                int HzListId = Convert.ToInt32(Tekno5746Liste.Rows[k]["HzListId"]);
                string FrmPrId = Tekno5746Liste.Rows[k]["FrmPrId"].ToString();
                string TknoPrsMi = Tekno5746Liste.Rows[k]["TknoPrsMi"].ToString();
                string DahilMi = Tekno5746Liste.Rows[k]["DahilMi"].ToString();
                string Donem = Tekno5746Liste.Rows[k]["Donem"].ToString();
                string SgkNo = Tekno5746Liste.Rows[k]["SgkNo"].ToString();
                string Gun = Tekno5746Liste.Rows[k]["Gun"].ToString();
                string KanunNo = Tekno5746Liste.Rows[k]["KanunNo"].ToString();
                string Mahiyet = Tekno5746Liste.Rows[k]["Mahiyet"].ToString();
                string ThkAdet = Tekno5746Liste.Rows[k]["ThkAdet"].ToString();
                decimal A_Ucret = Convert.ToDecimal(Tekno5746Liste.Rows[k]["A_Ucret"]);
                decimal B_Ikramiye = Convert.ToDecimal(Tekno5746Liste.Rows[k]["B_Ikramiye"]);
                decimal AB_Toplam = Convert.ToDecimal(Tekno5746Liste.Rows[k]["AB_Toplam"]);
                decimal DigerKanun = Convert.ToDecimal(Tekno5746Liste.Rows[k]["DigerKanun"]);

                decimal AylıkUcret = 0;
                string BrutNet = "";
                decimal bBrut = 0;
                decimal bSGkMatrah = 0;
                decimal bIsciPayi = 0;
                decimal bKumMatr = 0;
                decimal bVrgiMatr = 0;
                decimal bGV_DV = 0;
                decimal bAgi = 0;
                decimal bKesintiler = 0;
                decimal BrdNet = 0; // Bordroda yer alan Bordro Net
                decimal bNet = 0; // Manuel Hesaplanan Bordro Neti


                decimal bNet_BrdNet = 0;
                decimal SgkDisiKznc = 0;// BORDRO(BRÜT ÜCRET - SGK MATRAHI = SGK DIŞI KAZANÇ ) - 0 değilse kazanç var ve hep 0 üstü olacak 
                decimal EkOdemeler = 0;// 5746 dışı GV dahil edilecek Tutar
                decimal Sgk_BdrMatrah = 0; // SGK SPEK - BORDRO SGK SPEK = 0 OLMALI 
                decimal BazNetUcret = 0;// 5746 YA BAZ ALINACAK NET ÜCRET 
                // BAZ NET ÜCRETİN HESAPLANMASI İÇİN KOMBİNASYONU KUR 
                decimal Brut_5746 = 0; // 5746 BAZ NET ÜCRET BULUNDUKTAN SONRA 5746 BRÜT ÜCRETE DÖNÜŞTÜR. 
                string TbnTvnKntrl = "";//SGK TABAN TAVAN KONTROLÜ YAPILACAK 
                decimal EskiYeniSpekFarkı = 0; // ESKİ YENİ SPEK FARKLARI BULUNACAK 
                decimal YeniAPHBMatrah = 0;// YENİ APHB BAZ ALINACAK SPEK TESPİTİ - ASG ÜCRET ÜSTÜ - ALTI VS. 
                decimal Hsplansın = 0; //KRİTERLERE UYUYOR İSE EVET YOKSA HAYIR - HAYIR OLANLAR APHB NE AKTARILMAYACAK 
                string Acıklama = "";// HATA MESAJLARINI AKTARILACAK KISIM 

                decimal AsgTbnGun = 0;
                decimal AsgTvnGun = 0;
                decimal Gunluk5746 = 0;

                string HesDahilMi = "";
                string yil = "";
                string VergiAciklama = "";
                string netAciklama = "";
                string brutAciklama = "";
                string brtNetAciklama = "";
                string matrahAciklama = "";
                string kanunAciklama = "";
                
                decimal KmVergiMatrahi = 0;

                decimal farkmatrah = 0;


                decimal digerucreti = 0;
                var bordrodavarmi = FirmaBordro.Select("FirmaPersId='" + FrmPrId + "'");
                foreach (var bdrPers in bordrodavarmi)
                {
                    // 1- Aşama Bordrodan 5746 Bordrosuna aktarılacak veriler
                    AylıkUcret = Convert.ToDecimal(bdrPers["Net_BrtUcret"]);
                    BrutNet = bdrPers["Net_Brüt"].ToString();
                    bBrut = Convert.ToDecimal(bdrPers["ToplamKazanc"]);

                    bSGkMatrah = Convert.ToDecimal(bdrPers["SgkMatrahi"]);
                    bIsciPayi = Convert.ToDecimal(bdrPers["SGkIsciPrim"]) + Convert.ToDecimal(bdrPers["IszlikIsciPrim"]);
                    bKumMatr = Convert.ToDecimal(bdrPers["KumVergMatr"]);
                    bVrgiMatr = Convert.ToDecimal(bdrPers["GvMatrahi"]);
                    bGV_DV = Convert.ToDecimal(bdrPers["GelirVergisi"]) + Convert.ToDecimal(bdrPers["DamgaVrg"]);
                    bAgi = Convert.ToDecimal(bdrPers["Agi"]);
                    bKesintiler = Convert.ToDecimal(bdrPers["Kesintiler"]);
                    BrdNet = Convert.ToDecimal(bdrPers["AylikNetUcret"]);
                    bNet = bBrut - (bIsciPayi + bGV_DV + bAgi + bKesintiler);
                    bNet_BrdNet = BrdNet - bNet;

                    SgkDisiKznc = bBrut - bSGkMatrah;
                    // varsa diğer kanun maddesinden ek ödemeler toplamını alıyoruz 
                    ekOdemelerToplami.Clear();
                    baglan.Open();
                    SQLiteDataAdapter daEkSorgu = new SQLiteDataAdapter("SELECT sum(DigerKanun) as DigerKanunTopl From Bordro5746 where FrmPrId = '" + TeknoPrId + "'", baglan);
                    daEkSorgu.Fill(ekOdemelerToplami);
                    baglan.Close();
                    EkOdemeler = ekOdemelerToplami.Rows[0][0] != DBNull.Value ? Convert.ToDecimal(ekOdemelerToplami.Rows[0][0]) : 0;
                    Sgk_BdrMatrah = bSGkMatrah - A_Ucret;

                    // NET ÜCRETİ TESPİTİ YAPILIYOR
                    if (BrutNet.Contains("Net") || BrutNet.Contains("NET"))
                    {
                        if (Convert.ToInt32(Gun) == 30)
                        {
                            BazNetUcret = AylıkUcret;
                        }
                        else
                        {
                            BazNetUcret = (AylıkUcret / 30) * Convert.ToInt32(Gun);
                        }
                    }
                    else
                    {
                        if (BrdNet > bNet)
                        {
                            BazNetUcret = BrdNet;
                        }
                        else
                        {
                            BazNetUcret = bNet;
                        }

                        hatalar.Add(SgkNo + " Bordrosu Brüt Ücret");
                    }

                    Brut_5746 = BazNetUcret / 85 * 100;// BU TUTARDAN DİĞER KANUN DAN FAYDALANDIĞI NETİ DÜŞECEĞİZ ----------

                    var asgTabanTavan = TabanTavan.Select("asg_donem='" + Donem + "'");
                    foreach (var tbntvn in asgTabanTavan)
                    {
                        AsgTbnGun = Convert.ToDecimal(tbntvn["asg_taban_ucr"]) / 30;
                        AsgTvnGun = Convert.ToDecimal(tbntvn["asg_tavan_ucr"]) / 30;
                    }

                    // Asgari Ücret Kontrolü yapılıyor 
                    if (Gunluk5746 > AsgTbnGun && Brut_5746>AsgTbnGun* Convert.ToInt32(Gun))
                    {
                        YeniAPHBMatrah = Brut_5746;
                        TbnTvnKntrl = "Uygun";
                    }
                    else
                    {
                        YeniAPHBMatrah = A_Ucret;
                        hatalar.Add(SgkNo + " Tc Nolu Spek Tutarı- Sgk Taban ÜCRETTEN DÜŞÜK");
                    }
                    if ((Gunluk5746 < AsgTvnGun && Brut_5746 < AsgTvnGun * Convert.ToInt32(Gun)))
                    {
                        YeniAPHBMatrah = Brut_5746;
                        TbnTvnKntrl = "Uygun";
                    }
                    else
                    {
                        YeniAPHBMatrah = A_Ucret;
                        hatalar.Add(SgkNo + " Tc Nolu Spek Tutarı- Sgk Tavan ÜCRETTEN YÜKSEK");
                    }
                    if ((A_Ucret - YeniAPHBMatrah)<0)
                    {
                        YeniAPHBMatrah = A_Ucret;
                        hatalar.Add(" Spek (-) olduğu için Mevcut Spek Korundu");
                    }
                    EskiYeniSpekFarkı = A_Ucret - YeniAPHBMatrah;



                    baglan.Open();
                    SQLiteCommand guncelle = new SQLiteCommand("update Bordro5746 set  AylıkUcret=@AylıkUcret, BrutNet=@BrutNet, bBrut=@bBrut, bSGkMatrah=@bSGkMatrah, bIsciPayi=@bIsciPayi, bKumMatr=@bKumMatr, bVrgiMatr=@bVrgiMatr,bGV_DV=@bGV_DV, bAgi=@bAgi, bKesintiler=@bKesintiler, BrdNet=@BrdNet, bNet=@bNet, bNet_BrdNet=@bNet_BrdNet, SgkDisiKznc=@SgkDisiKznc, EkOdemeler=@EkOdemeler, Sgk_BdrMatrah=@Sgk_BdrMatrah, BazNetUcret=@BazNetUcret, Brut_5746=@Brut_5746, TbnTvnKntrl=@TbnTvnKntrl, EskiYeniSpekFarkı=@EskiYeniSpekFarkı, YeniAPHBMatrah=@YeniAPHBMatrah, Hsplansın=@Hsplansın, Acıklama=@Acıklama where TeknoPrId ='" + TeknoPrId + "'", baglan);

                    guncelle.Parameters.AddWithValue("@AylıkUcret", AylıkUcret);
                    guncelle.Parameters.AddWithValue("@BrutNet", BrutNet);
                    guncelle.Parameters.AddWithValue("@bBrut", bBrut);
                    guncelle.Parameters.AddWithValue("@bSGkMatrah", bSGkMatrah);
                    guncelle.Parameters.AddWithValue("@bIsciPayi", bIsciPayi);
                    guncelle.Parameters.AddWithValue("@bKumMatr", bKumMatr);
                    guncelle.Parameters.AddWithValue("@bVrgiMatr", bVrgiMatr);
                    guncelle.Parameters.AddWithValue("@bGV_DV", bGV_DV);
                    guncelle.Parameters.AddWithValue("@bAgi", bAgi);
                    guncelle.Parameters.AddWithValue("@bKesintiler", bKesintiler);
                    guncelle.Parameters.AddWithValue("@BrdNet", BrdNet);

                    guncelle.Parameters.AddWithValue("@bNet", bNet);
                    guncelle.Parameters.AddWithValue("@bNet_BrdNet", bNet_BrdNet);
                    guncelle.Parameters.AddWithValue("@SgkDisiKznc", SgkDisiKznc);
                    guncelle.Parameters.AddWithValue("@EkOdemeler", EkOdemeler);
                    guncelle.Parameters.AddWithValue("@Sgk_BdrMatrah", Sgk_BdrMatrah);
                    guncelle.Parameters.AddWithValue("@BazNetUcret", BazNetUcret);
                    guncelle.Parameters.AddWithValue("@Brut_5746", Brut_5746);
                    guncelle.Parameters.AddWithValue("@TbnTvnKntrl", TbnTvnKntrl);
                    guncelle.Parameters.AddWithValue("@EskiYeniSpekFarkı", EskiYeniSpekFarkı);
                    guncelle.Parameters.AddWithValue("@YeniAPHBMatrah", YeniAPHBMatrah);
                    guncelle.Parameters.AddWithValue("@Hsplansın", Hsplansın);
                    guncelle.Parameters.AddWithValue("@Acıklama", Acıklama);
                   // guncelle.Parameters.AddWithValue("@Bos", Bos);


                    guncelle.ExecuteNonQuery();
                    baglan.Close();

                }
            }
        }



        //    Hsplansın
        //    Acıklama



        List<string> hatalar = new List<string>();

        private void teknoParkPersonelSayisi()
        {
            // KAÇ KİŞİNİN TEKNOPARK PERSONELİ OLARAK ÇALIŞTIĞINI BULUYORUZ.
            baglan.Open();
            SQLiteDataAdapter daa = new SQLiteDataAdapter("SELECT count(SgkNo) as islem  From Tekno5746PersonelListesi where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable teknoPersListesi = new DataTable();
            daa.Fill(teknoPersListesi);
            baglan.Close();
            teknoPersonelSayisi = Convert.ToInt32(teknoPersListesi.Rows[0][0]);

        }
        private void hizmetListesiCalisanSayisi()
        {
            //HizmetListesinde Çalışan Personel Sayısını Buluyoruz
            baglan.Open();
            SQLiteDataAdapter daa = new SQLiteDataAdapter("SELECT count(SgkNo) as islem  From HizmetListesi where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable HizmetListesiDolumu = new DataTable();
            daa.Fill(HizmetListesiDolumu);
            baglan.Close();
            HizmetListePersonelSayisi = Convert.ToInt32(HizmetListesiDolumu.Rows[0][0]);
        }
        private void IPTALSIZhizmetListesiCalisanSayisi()
        {
            //HizmetListesinde Çalışan Personel Sayısını Buluyoruz
            baglan.Open();
            SQLiteDataAdapter daa = new SQLiteDataAdapter("SELECT count(SgkNo) as islem  From HizmetListesiIptalsiz where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);
            DataTable IPTALHizmetListesiDolumu = new DataTable();
            daa.Fill(IPTALHizmetListesiDolumu);
            baglan.Close();
            IPTALHizmetListePersonelSayisi = Convert.ToInt32(IPTALHizmetListesiDolumu.Rows[0][0]);
        }
        private void yeniHizmetListesiCalisanSayisi()
        {
            baglan.Open();
            SQLiteDataAdapter Hsplda = new SQLiteDataAdapter("SELECT count(SgkNo) as sayi from  HizmetListesiIptalsiz where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);

            DataTable YeniHzmtListesiHL = new DataTable();

            Hsplda.Fill(YeniHzmtListesiHL);
            baglan.Close();
            yeniHizmetListesiPersonelSayisi = Convert.ToInt32(YeniHzmtListesiHL.Rows[0][0]);
        }
        private void TeknoBordroCalisanSayisi()
        {
            baglan.Open();
            SQLiteDataAdapter Hsplda = new SQLiteDataAdapter("SELECT count(SgkNo) as sayi from  Bordro5746 where firmaid='" + frmId + "' and subeid = '" + sbId + "'", baglan);

            DataTable teknoBordroCalisan = new DataTable();

            Hsplda.Fill(teknoBordroCalisan);
            baglan.Close();
            teknoBordroCalisanSayisi = Convert.ToInt32(teknoBordroCalisan.Rows[0][0]);
        }


        private void SgkTabanTavan()
        {
            // Yıllık Sgk taban ve Tavan Ücretlerini Çeker
            TabanTavan.Clear();

            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("select * From yillik_taban_tavan_ucr", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(TabanTavan);
            }
            baglan.Close();
        }

        private void btnAsilEkİptalAyikla_Click_1(object sender, EventArgs e)
        {
            teknoParkPersonelSayisi();
            hizmetListesiCalisanSayisi();
            if (teknoPersonelSayisi > 0)
            {
                if (HizmetListePersonelSayisi > 0)
                {
                    lblAsilIptalEkAyristir.Text = "İşlem Başladı Lütfen Bekleyiniz";
                    asilIptalEkBildirgeleriAyikla();
                    lblAsilIptalEkAyristir.Text = "İşlem Başarı İle Tamamlandı .... ";
                }
                MessageBox.Show("Öncelikle e bildirge ekranından hizmet listelerini indirmelisiniz \n ve TeknoPark Personel Listesini Oluşturmalısınız...  ");
            }
        }

        private void btnTeknPrkPersOlustur_Click(object sender, EventArgs e)
        {
            hizmetListesiCalisanSayisi();
            if (HizmetListePersonelSayisi > 0)
            {
                lblTeknPrkPerOlustur.Text = "İşlem Başladı Lütfen Bekleyiniz";
                HizmetListesindenTeknoPersListesiOluszur();
                lblTeknPrkPerOlustur.Text = "İşlem Başarı İle Tamamlandı .... ";
            }
            else
            {
                MessageBox.Show("Öncelikle e bildirge ekranından hizmet listelerini indirmelisiniz ... ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TeknoPersListesiYukle tknPrsListesi = new TeknoPersListesiYukle();
            tknPrsListesi.Show();
        }

        private void btnCiftKanunluTeknoPers_Click(object sender, EventArgs e)
        {
            IPTALSIZhizmetListesiCalisanSayisi();

            if (IPTALHizmetListePersonelSayisi > 0)
            {
                lblCiftKanunluOlustur.Text = "İşlem Başladı Lütfen Bekleyiniz";
                Tekno5746veDigerKanundanFaydalananlarıAyristir();
                lblCiftKanunluOlustur.Text = "İşlem Başarı İle Tamamlandı .... ";
            }
            else
            {
                MessageBox.Show("Öncelikle e bildirge ekranından hizmet listelerini indirmelisiniz ... ");
            }
        }

        private void btnTeknoHesapla_Click(object sender, EventArgs e)
        {
            TeknoParkBordroHesapla();
        }
    }
}

