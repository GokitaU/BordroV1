using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bordrolama10
{
    public partial class TeknoParkBordro : Form
    {
        public TeknoParkBordro()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        static int firmaid = -1;
        static string donem = "";
        static int subeid = -1;
        static string persid = "";



        DataTable Bordro = new DataTable();
        DataTable HzmtListesi = new DataTable();
        DataTable Tahakkuk = new DataTable();
        private void yuklubordro()
        {
            Bordro.Clear();
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("select FirmaPersId,PersId, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,CikisTarihi,Net_Brüt,Net_BrtUcret as N_B_Ucret,PrimGunu,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,DamgaVrg,(BesKesintisi+SairKesintiler) as Kesintiler,AylikNetUcret,KanunNo from FirmaBordro WHERE FirmaNo = '" + firmaid + "' and SubeNo='" + subeid + "' and PuantajDonem =  '" + donem + "'", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(Bordro);
            }
            baglan.Close();
        }
        DataTable TabanTavan = new DataTable();
        private void SgkTabanTavan()
        {
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
        DataTable yHesaplananHizmetListesi = new DataTable();
        private void YhizmetHespalama()
        {
            yHesaplananHizmetListesi.Clear();
            baglan.Open();
            SQLiteDataAdapter Yhizmtda = new SQLiteDataAdapter("SELECT HesaplansınMı,Donem,SgkNo,Ad,Soyad,Gun,KanunNo,Mahiyet,ThkAdet,A_Ucret,B_Ikramiye,AB_Toplam,C_TumSpek,Brt_Net,D_BDRBrut,E_BDRSgkMatrah,C_D_BrtFarkı,AB_E_MatrahFarkı,BRDIsciPayi_1,VergiMatrahi,BRDVergi_2,Agi_3,Kesintiler_4,BrdNet_1234,F_AylıkNet,G_BordroNet,F_G_NetFarkı,Baz_Net_5746,Brut_5746,Gunluk_5746,Asg_TbnGunluk,Asg_TvnGunluk,Tbn_Tvn_UygunMu,EskiYeniSpekFarkı,YeniAPHBMatrah,Acıklama from  Bordro5746 where firmaid='" + firmaid + "' and subeid = '" + subeid + "' and Donem = '" + donem + "' order by SgkNo", baglan);

            Yhizmtda.Fill(yHesaplananHizmetListesi);
            baglan.Close();

            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.Name = "Hesapla";
            combo.DataPropertyName = "HesaplansınMı";
            combo.Items.Add("Evet");
            combo.Items.Add("Hayır");


            if (dataGridView1.Columns["Hesapla"] == null)
            {
                dataGridView1.Columns.Add(combo);
                dataGridView1.Columns["Hesapla"].HeaderText = "Hesapla";
            }

            dataGridView1.DataSource = yHesaplananHizmetListesi;
            dataGridView1.Columns["Brt_Net"].Frozen = true;// sütün dondurma
                                                           // dataGridView1.Columns["HesaplansınMı"].Visible = false;

            // dataGridView1.Columns["HesaplansınMı"].Visible = false;
            //combo.DataSource = yHesaplananHizmetListesi.Columns["HesaplansınMı"];

        }



        private void SgkTahakkukbilgileri()
        {
            Tahakkuk.Clear();

            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("select thkkukdonem,hzmtdonem,blgtur,bmahiyet,bkanun,bcalisan,bgun,spek,pdfindurm as Pdf,dnmhzlistcalisan as HzmCalisan,dnmhzlistgun as HzmGun,dnmhzlistspek as HzmSpek,YenThkCalisan as YeniCalsn, YeniThkGun as YeniGun, YeniThkSpek as YeniSpek From ilktahakkukbilgi WHERE firmaid = '" + firmaid + "' and subeid='" + subeid + "' and thkkukdonem like '" + cbmYil.Text + "%'", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(Tahakkuk);
            }
            baglan.Close();
        }
        decimal vMatrahi = 0;
        decimal kVMatrahi = 0;
        string VYili = "";
        decimal gv = 0;
        private void BordroHesapla()
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter("select * From VergiDilimleri", baglan);

            DataTable dilimler = new DataTable();
            da.Fill(dilimler);

            List<VergiDilimleri> vergidilimleri = new List<VergiDilimleri>();
            for (int i = 0; i < dilimler.Rows.Count; i++)
            {
                VergiDilimleri dilim = new VergiDilimleri();

                vergidilimleri.Add(new VergiDilimleri
                {
                    yil = dilimler.Rows[i]["Yil"].ToString(),
                    Dilim1Aralik = Convert.ToDecimal(dilimler.Rows[i]["Dilim_1"]),
                    Dilim1Oran = Convert.ToInt32(dilimler.Rows[i]["Oran_1"]),
                    Dilim2Aralik = Convert.ToDecimal(dilimler.Rows[i]["Dilim_2"]),
                    Dilim2Oran = Convert.ToInt32(dilimler.Rows[i]["Oran_2"]),
                    Dilim3Aralik = Convert.ToDecimal(dilimler.Rows[i]["Dilim_3"]),
                    Dilim3Oran = Convert.ToInt32(dilimler.Rows[i]["Oran_3"]),
                    Dilim4Aralik = Convert.ToDecimal(dilimler.Rows[i]["Dilim_4"]),
                    Dilim4Oran = Convert.ToInt32(dilimler.Rows[i]["Oran_4"]),
                    Dilim5Aralik = Convert.ToDecimal(dilimler.Rows[i]["Dilim_5"]),
                    Dilim5Oran = Convert.ToInt32(dilimler.Rows[i]["Oran_5"])

                });

            }
            BordroHesaplama bordroHesaplama = new BordroHesaplama();
            gv = bordroHesaplama.GvHesapla(vergidilimleri.Where(x => x.yil == VYili).FirstOrDefault(), vMatrahi, kVMatrahi);
        }
        private void hizmetListesiDoldur()
        {
            yuklubordro();
            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Evet")
            {
                islemKanunu = cmbIslemKanunu.Text;
            }
            string asil = "Mahiyet = '%'";
            string ek = "Mahiyet = '%'";
            string iptal = "Mahiyet = '%'";

            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                asil = "Mahiyet = 'ASIL'";
            }
            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                ek = "Mahiyet = 'EK'";
            }
            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                iptal = "Mahiyet = 'IPTAL'";
            }
            baglan.Open();
            // SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT Donem,SgkNo,ad,soyad,IlkSoyad,Ucret,Ikramiye,Gun,GGun,CGun,Egs,Icn,Kanun_No,Mahiyet From HizmetListesi where firmaid='" + firmaid + "' and subeid = '" + subeid + "' and Donem = '" + donem + "' AND Kanun_No like '%" + islemKanunu + "%' and ( " + asil + " or " + ek + " or " + iptal + ")", baglan);
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT firmPersid as ID, Donem,SgkNo,ad,soyad,Gun,Kanun_No,Mahiyet,Ucret,Ikramiye,(ucret + Ikramiye) as Brt_Spek From HizmetListesi where firmaid='" + firmaid + "' and subeid = '" + subeid + "' and Donem = '" + donem + "' AND Kanun_No like '%" + islemKanunu + "%' and ( " + asil + " or " + ek + " or " + iptal + ") order by sgkno", baglan);

            DataTable HzmtListesi = new DataTable();
            da.Fill(HzmtListesi);
            //hzmtlistesi.Columns.Add(new DataColumn("Degisti", typeof(bool)));

            string PrId = "";

            for (int k = 0; k < HzmtListesi.Rows.Count; k++)
            {
                string Kanun = "";
                PrId = HzmtListesi.Rows[k]["ID"].ToString();
                var Tekno5746daVarmiYokmu = HzmtListesi.Select("ID='" + PrId + "'");
                foreach (var item in Tekno5746daVarmiYokmu)
                {
                    Kanun = item["Kanun_No"].ToString();
                    if (Kanun.Contains("5746")) continue;

                }
                if (!Kanun.Contains("5746"))
                {
                    HzmtListesi.Rows[k].Delete();
                }
            }




            dtgrtHizmet.DataSource = HzmtListesi;

            baglan.Close();
            dtgrtHizmet.Columns["ID"].Visible = false;
            dtgrtHizmet.Columns["ucret"].DefaultCellStyle.Format = "N2";
            dtgrtHizmet.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";
            dtgrtHizmet.Columns["Brt_Spek"].DefaultCellStyle.Format = "N2";

            string tcno = "";
            for (int i = 0; i < dtgrtHizmet.Rows.Count; i++)
            {

                tcno = dtgrtHizmet.Rows[i].Cells["SgkNo"].Value.ToString();

                for (int j = 0; j < dtgrtHizmet.Rows.Count; j++)
                {
                    if (dtgrtHizmet.Rows[i].Cells["SgkNo"].RowIndex == j) continue;
                    {
                        if (dtgrtHizmet.Rows[j].Cells["SgkNo"].Value.ToString() == tcno)
                        {
                            dtgrtHizmet.Rows[j].DefaultCellStyle.BackColor = Color.IndianRed;
                        }
                    }

                }
                if (dtgrtHizmet.Rows[i].Cells["Kanun_No"].Value.ToString().Contains("5746"))
                {
                    dtgrtHizmet.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }

            }

        }


        public void tahakkukBilgisiListBox()
        {
            if (comboBox1.SelectedItem == null) return;
            string subefiltre = "";

            if (subeid != 0)
            {
                subefiltre = "and subeid = '" + subeid + "'".ToString();
            }

            string islemKanunu = cmbIslemKanunu.Text;

            string asil = "";
            //string ek = "Mahiyet = ''";
            //string iptal = "Mahiyet = ''";

            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                asil = " and Mahiyet = 'ASIL'";
            }
            //if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            //{
            //    ek = "Mahiyet = 'EK'";
            //}
            //if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            //{
            //    iptal = "Mahiyet = 'IPTAL'";
            //}

            baglan.Open();
            SQLiteDataAdapter asilAphb = new SQLiteDataAdapter("SELECT count(personelid) as calisan, sum(gun) as gun, sum(ucret) as spek fROM hizmetlistesi where firmaid ='" + firmaid + "'  " + subefiltre + " and  Donem = '" + donem + "' AND Kanun_No like '%" + islemKanunu + "%' " + asil + "", baglan);
            DataTable table = new DataTable();
            asilAphb.Fill(table);

            string basil = "";
            //string bek = "bmahiyet = ''";
            //string biptal = "bmahiyet = ''";

            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                basil = " and bmahiyet = 'ASIL'";
            }
            //if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            //{
            //    bek = "bmahiyet = 'EK'";
            //}
            //if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            //{
            //    biptal = "bmahiyet = 'IPTAL'";
            //}

            SQLiteDataAdapter asilThkkuk = new SQLiteDataAdapter("SELECT bcalisan as calisan,bgun as gun,spek FROM ilktahakkukbilgi where firmaid  ='" + firmaid + "'  " + subefiltre + " and thkkukdonem = '" + donem + "' and  bkanun like '%" + islemKanunu + "%' " + basil + "", baglan);

            DataTable table1 = new DataTable();
            asilThkkuk.Fill(table1);
            baglan.Close();


            if (table1.Rows.Count > 0 && table.Rows.Count > 0)
            {
                List<TeknoOzet> ozetList = new List<TeknoOzet>();
                ozetList.Add(new TeknoOzet
                {
                    calisan = Convert.ToInt32(table.Rows[0]["calisan"].ToString()),
                    gun = Convert.ToInt32(table.Rows[0]["gun"].ToString()),
                    spek = Convert.ToDecimal(table.Rows[0]["spek"].ToString()),
                });

                ozetList.Add(new TeknoOzet
                {
                    calisan = Convert.ToInt32(table1.Rows[0]["calisan"].ToString()),
                    gun = Convert.ToInt32(table1.Rows[0]["gun"].ToString()),
                    spek = Convert.ToDecimal(table1.Rows[0]["spek"].ToString()),
                });
                txtKanunNo.DataSource = ozetList;
            }
            else
            {
                MessageBox.Show("" + cmbIslemKanunu.Text + " Kanun Türünden \n Tahakkuk Bilgisi Bulunamadı...");
            }








        }


        public void TeknoDonemOzet()
        {
            if (comboBox1.SelectedItem == null) return;
            string subefiltre = "";

            if (subeid != 0)
            {
                subefiltre = "and subeid = '" + subeid + "'".ToString();
            }
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("select donem From hizmetListesi where firmaid='" + firmaid + "'  " + subefiltre + " and Donem between '" + cmbilk.Text + "' and '" + cmbson.Text + "' GROUP by donem", baglan);
            DataTable table = new DataTable();
            da.Fill(table);
            baglan.Close();


            listBox1.DataSource = table;
            listBox1.DisplayMember = "donem";

        }
        public void subelistele()
        {
            if (comboBox1.SelectedItem == null) return;
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT subeid as Id, subeunvan as Sube_Unvan  From  sube_bilgileri  where firmaid = " + firmaid + "", baglan);
            DataTable dt = new DataTable();

            da.Fill(dt);
            baglan.Close();
            dtgrtSubeSecim.DataSource = dt;
        }


        private void TeknoParkBordro_Load(object sender, EventArgs e)
        {

            baglan.Open();
            SQLiteCommand combobx = new SQLiteCommand("select * From Hizli_Firma_Kayit", baglan);//  where aktifpasif like'Aktif'
            SQLiteDataReader dr = combobx.ExecuteReader();
            while (dr.Read())
            {
                firmaid = Convert.ToInt32(dr["firmaid"]);
                comboBox1.Items.Add(dr[2]);
            }
            baglan.Close();
            baglan.Open();
            SQLiteCommand cmbdonem = new SQLiteCommand("select * from DonemBilgisi", baglan);
            SQLiteDataReader dr1 = cmbdonem.ExecuteReader();
            while (dr1.Read())
            {
                cmbilk.Items.Add(dr1[3]);
                cmbson.Items.Add(dr1[3]);
            }
            baglan.Close();

            cbmYil.Text = "2020";
            cmbilk.Text = "2014/04";
            cmbson.Text = "2021/01";
            txtdosyayolu.Text = Application.StartupPath + "\\GvTesvikBordro";
            cmbIslemKanunu.Text = "5746";
            cmbAyrıBordro.Text = "Hayır";
            checkedListBox2.SetItemChecked(0, true);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SQLiteCommand frm = new SQLiteCommand("select * from Hizli_Firma_Kayit where Firmakisaadi like '" + comboBox1.Text.ToString() + "'", baglan);
            SQLiteDataReader da = frm.ExecuteReader();
            while (da.Read())
            {
                firmaid = Convert.ToInt32(da[0]);
            }
            da.Close();
            baglan.Close();

            subelistele();
            //TeknoDonemOzet();

        }

        private void dtgrtSubeSecim_Click(object sender, EventArgs e)
        {

            if (dtgrtSubeSecim.Rows.Count > 1)
            {
                int secim = dtgrtSubeSecim.SelectedCells[0].RowIndex;
                subeid = dtgrtSubeSecim.Rows[secim].Cells[0].Value != DBNull.Value ? Convert.ToInt32(dtgrtSubeSecim.Rows[secim].Cells[0].Value) : -1;
            }
            TeknoDonemOzet();
            if (listBox1.Items.Count > 1)
            {

                donem = listBox1.GetItemText(listBox1.Items[0]);
            }
            else
            {
                donem = "'%'";
            }
            hizmetListesiDoldur();
            tahakkukBilgisiListBox();
            //dtgrtHizmet.Columns.Add("SpekToplami", "SpekToplami");
            //dtgrtHizmet.Columns.Add("ThkAdet", "ThkAdet");
            //dtgrtHizmet.Columns.Add("hBrdTopl", "hBrdTopl");
            //dtgrtHizmet.Columns.Add("hBrdSpek", "hBrdSpek");
            //dtgrtHizmet.Columns.Add("hBrdIsciPy", "hBrdIsciPy");
            //dtgrtHizmet.Columns.Add("hBrdVergi", "hBrdVergi");
            //dtgrtHizmet.Columns.Add("hBrdKes", "hBrdKes");
            //dtgrtHizmet.Columns.Add("hBrdNet", "hBrdNet");
            //dtgrtHizmet.Columns.Add("BrdNet", "BrdNet");
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                donem = listBox1.GetItemText(listBox1.SelectedItem);
            }
            // hizmetListesiDoldur();
            tahakkukBilgisiListBox();
            if (tabControl1.SelectedTab == tabPage1)
            {
                hizmetListesiDoldur();
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                APHBSayfasiVerileri();
                APHBSayfaToplamlari();
            }
            if (tabControl1.SelectedTab == tabPage3)
            {
                yuklubordro();

            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                SgkTahakkukbilgileri();
                dtgrtTAHAKUKLAR.DataSource = Tahakkuk;
                dtgrTahakkukAlanlariDuzenle();
            }
            if (tabControl1.SelectedTab == tabPage5)
            {
                YhizmetHespalama();
                hesaplaDtgrAlanlariDuzenle();
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            BordroHesapla();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            yuklubordro();



            for (int i = 0; i < dtgrtHizmet.Rows.Count; i++)
            {
                persid = dtgrtHizmet.Rows[i].Cells["ID"].Value.ToString();

                if (dtgrtHizmet.Rows[i].Cells["Kanun_No"].Value.ToString().Contains("5746"))
                {
                    decimal spektoplami = 0;
                    int calisantoplami = 0;
                    for (int z = 0; z < dtgrtHizmet.Rows.Count; z++)
                    {
                        if (dtgrtHizmet.Rows[z].Cells["ID"].Value.ToString() == persid)
                        {
                            spektoplami += Convert.ToDecimal(dtgrtHizmet.Rows[z].Cells["Brt_Spek"].Value.ToString());
                            calisantoplami += 1;
                        }

                    }
                    dtgrtHizmet.Rows[i].Cells["SpekToplami"].Value = spektoplami;
                    dtgrtHizmet.Rows[i].Cells["ThkAdet"].Value = calisantoplami;
                    decimal bBrtKazanc = 0;
                    decimal bSpek = 0; // bordrodan alınan veriler hesaplama tekrar brüt ücretten çıkartılarak nete ulaşılmaya çalışılıyor ve 
                    decimal bsgkIsci = 0;
                    decimal bgv_Dv = 0;
                    decimal bKesintiler = 0;
                    decimal bNet = 0; // net ücret hesaplanıyor 
                    decimal BordroNet = 0; // yine bordrodaki net ücret tekrar alınaraka farklı bir kesinti varmı kontrolü yapılıyor 

                    var teknoKentPersoneli = Bordro.Select("FirmaPersId='" + persid + "'");

                    foreach (var item in teknoKentPersoneli)
                    {
                        bBrtKazanc = Convert.ToDecimal(item["ToplamKazanc"]);
                        bSpek = Convert.ToDecimal(item["SgkMatrahi"]);
                        bsgkIsci = Convert.ToDecimal(item["SGkIsciPrim"]) + Convert.ToDecimal(item["IszlikIsciPrim"]);
                        bgv_Dv = Convert.ToDecimal(item["GelirVergisi"]) + Convert.ToDecimal(item["DamgaVrg"]);
                        bKesintiler = Convert.ToDecimal(item["Kesintiler"]);
                        bNet = (bBrtKazanc - (bsgkIsci + bgv_Dv));

                        BordroNet = Convert.ToDecimal(item["AylikNetUcret"]);

                    }
                    dtgrtHizmet.Rows[i].Cells["hBrdTopl"].Value = bBrtKazanc;
                    dtgrtHizmet.Rows[i].Cells["hBrdSpek"].Value = bSpek;
                    dtgrtHizmet.Rows[i].Cells["hBrdIsciPy"].Value = bsgkIsci;
                    dtgrtHizmet.Rows[i].Cells["hBrdVergi"].Value = bgv_Dv;
                    dtgrtHizmet.Rows[i].Cells["hBrdKes"].Value = bKesintiler;
                    dtgrtHizmet.Rows[i].Cells["hBrdNet"].Value = bNet;
                    dtgrtHizmet.Rows[i].Cells["BrdNet"].Value = BordroNet;



                }

            }


        }
        DataTable hizmetListesiToplamlari = new DataTable();
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {


            if (tabControl1.SelectedTab == tabPage1)
            {
                hizmetListesiDoldur();
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                APHBSayfasiVerileri();
                APHBSayfaToplamlari();
                txtAdFiltresi.Text = filtrePersAdi;
                txtSoyadFiltresi.Text = filtrePersSoyadi;
                txtTCnoFiltresi.Text = filtrePersTc;

            }
            if (tabControl1.SelectedTab == tabPage3)
            {

                yuklubordro();
                dtgrtBORDRO.DataSource = Bordro;

                txtBRDadfiltre.Text = filtrePersAdi;
                txtBRDSoyadFiltre.Text = filtrePersSoyadi;
                txtBRDTcNoFiltre.Text = filtrePersTc;

                BordroAlanlariDuzenle();
            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                SgkTahakkukbilgileri();
                dtgrtTAHAKUKLAR.DataSource = Tahakkuk;
                dtgrTahakkukAlanlariDuzenle();
            }

            if (tabControl1.SelectedTab == tabPage5)
            {
                YhizmetHespalama();
                hesaplaDtgrAlanlariDuzenle();
            }


        }

        private void BordroAlanlariDuzenle()
        {
            dtgrtBORDRO.Columns["FirmaPersId"].Visible = false;
            dtgrtBORDRO.Columns["KanunNo"].Visible = false;
            dtgrtBORDRO.Columns["ToplamKazanc"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["SgkMatrahi"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["SGkIsciPrim"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["IszlikIsciPrim"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["KumVergMatr"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["GvMatrahi"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["GelirVergisi"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["Agi"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["DamgaVrg"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["Kesintiler"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["AylikNetUcret"].DefaultCellStyle.Format = "N2";
            dtgrtBORDRO.Columns["N_B_Ucret"].DefaultCellStyle.Format = "N2";
        }

        private void dtgrTahakkukAlanlariDuzenle()
        {
            dtgrtTAHAKUKLAR.Columns["spek"].DefaultCellStyle.Format = "N2";
            dtgrtTAHAKUKLAR.Columns["HzmSpek"].DefaultCellStyle.Format = "N2";
        }
        private void APHBSayfasiVerileri()
        {

            APHBTam.Clear();

            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Evet")
            {
                islemKanunu = cmbIslemKanunu.Text;
            }
            string asil = "Mahiyet = '%'";
            string ek = "Mahiyet = '%'";
            string iptal = "Mahiyet = '%'";

            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                asil = "Mahiyet = 'ASIL'";
            }
            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                ek = "Mahiyet = 'EK'";
            }
            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                iptal = "Mahiyet = 'IPTAL'";
            }

            SQLiteDataAdapter hizmetListesiTam = new SQLiteDataAdapter("SELECT firmPersid as ID, Donem,SgkNo,ad,soyad,Ucret,Ikramiye,Gun,UCG,Eksik_Gun,GGun,CGun,Egs,Icn,Kanun_No,Mahiyet From HizmetListesi where firmaid='" + firmaid + "' and subeid = '" + subeid + "' and Donem = '" + donem + "' AND Kanun_No like '%" + islemKanunu + "%' and ( " + asil + " or " + ek + " or " + iptal + ") order by sgkno", baglan);

            // DataTable APHBTam = new DataTable();


            hizmetListesiTam.Fill(APHBTam);
            //hzmtlistesi.Columns.Add(new DataColumn("Degisti", typeof(bool)));

            dtgrtAPHB.DataSource = APHBTam;
            dtgrtAPHB.Columns["ID"].Visible = false;
            dtgrtAPHB.Columns["Ucret"].DefaultCellStyle.Format = "N2";
            dtgrtAPHB.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";
        }
        private void APHBSayfaToplamlari()
        {
            decimal spekToplami = 0;
            decimal ikramiyeToplami = 0;
            int gunToplami = 0;
            for (int i = 0; i < dtgrtAPHB.Rows.Count; i++)
            {
                spekToplami += Convert.ToDecimal(dtgrtAPHB.Rows[i].Cells["Ucret"].Value);
                ikramiyeToplami += Convert.ToDecimal(dtgrtAPHB.Rows[i].Cells["Ikramiye"].Value);
                gunToplami += Convert.ToInt32(dtgrtAPHB.Rows[i].Cells["Gun"].Value);

            }
            txtTopCalisan.Text = (dtgrtAPHB.Rows.Count).ToString();
            txtTopGun.Text = gunToplami.ToString();
            txtTopSpek.Text = spekToplami.ToString("N2");
            txtToplIkramiye.Text = ikramiyeToplami.ToString("N2");
            txtTopPrim.Text = (spekToplami + ikramiyeToplami).ToString("N2");

        }

        DataTable APHBTam = new DataTable();
        DataView adfiltrele()
        {
            DataView adi = new DataView();
            adi = APHBTam.DefaultView;
            adi.RowFilter = "ad like '" + txtAdFiltresi.Text + "%'";
            return adi;
        }
        DataView soyadfiltrele()
        {
            DataView soyadi = new DataView();
            soyadi = APHBTam.DefaultView;
            soyadi.RowFilter = "soyad like '" + txtSoyadFiltresi.Text + "%'";
            return soyadi;
        }
        DataView tcNofiltrele()
        {
            DataView TcNo = new DataView();
            TcNo = APHBTam.DefaultView;
            TcNo.RowFilter = "SgkNo like '" + txtTCnoFiltresi.Text + "%'";
            return TcNo;
        }
        DataView kanunfiltrele()
        {
            DataView Kanun = new DataView();
            Kanun = APHBTam.DefaultView;
            Kanun.RowFilter = "Kanun_No like '" + textBox1.Text + "%'";
            return Kanun;
        }
        DataView mahiyetfiltrele()
        {
            DataView Mahiyet = new DataView();
            Mahiyet = APHBTam.DefaultView;
            Mahiyet.RowFilter = "Mahiyet like '" + txtMahiyet.Text + "%'";
            return Mahiyet;
        }

        DataView BRDadfiltre()
        {
            DataView BrdAdi = new DataView();
            BrdAdi = Bordro.DefaultView;
            BrdAdi.RowFilter = "PersAdı like '" + txtBRDadfiltre.Text + "%'";
            return BrdAdi;
        }
        DataView BRDSoyadfiltre()
        {
            DataView BrdSoyad = new DataView();
            BrdSoyad = Bordro.DefaultView;
            BrdSoyad.RowFilter = "PersSoyadı like '" + txtBRDSoyadFiltre.Text + "%'";
            return BrdSoyad;
        }
        DataView BRDTcfiltre()
        {
            DataView BrdTc = new DataView();
            BrdTc = Bordro.DefaultView;
            BrdTc.RowFilter = "TcNo like '" + txtBRDTcNoFiltre.Text + "%'";
            return BrdTc;
        }
        DataView THKDonemFiltre()
        {
            DataView ThkDonem = new DataView();
            ThkDonem = Tahakkuk.DefaultView;
            ThkDonem.RowFilter = "thkkukdonem like '" + txtTHKDonem.Text + "%'";
            return ThkDonem;
        }
        DataView THKTurFiltre()
        {
            DataView ThkTur = new DataView();
            ThkTur = Tahakkuk.DefaultView;
            ThkTur.RowFilter = "blgtur like '" + txtTHKTur.Text + "%'";
            return ThkTur;
        }
        DataView THKMaliyetfiltre()
        {
            DataView ThkMahiyet = new DataView();
            ThkMahiyet = Tahakkuk.DefaultView;
            ThkMahiyet.RowFilter = "bmahiyet like '" + txtTHKMahiyet.Text + "%'";
            return ThkMahiyet;
        }
        DataView THKKanunFiltre()
        {
            DataView ThkKanun = new DataView();
            ThkKanun = Tahakkuk.DefaultView;
            ThkKanun.RowFilter = "bkanun like '" + txtTHKKanun.Text + "%'";
            return ThkKanun;
        }
        private void txtAdFiltresi_TextChanged(object sender, EventArgs e)
        {
            adfiltrele();
            APHBSayfaToplamlari();
        }

        private void txtSoyadFiltresi_TextChanged(object sender, EventArgs e)
        {
            soyadfiltrele();
            APHBSayfaToplamlari();
        }

        private void txtTCnoFiltresi_TextChanged(object sender, EventArgs e)
        {
            tcNofiltrele();
            APHBSayfaToplamlari();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            kanunfiltrele();
            APHBSayfaToplamlari();
        }

        private void txtMahiyet_TextChanged(object sender, EventArgs e)
        {
            mahiyetfiltrele();
            APHBSayfaToplamlari();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtAdFiltresi.Text = "";
            txtSoyadFiltresi.Text = "";
            txtTCnoFiltresi.Text = "";
            textBox1.Text = "";

        }


        static string filtrePersAdi = "";
        static string filtrePersSoyadi = "";
        static string filtrePersTc = "";


        private void dtgrtHizmet_Click(object sender, EventArgs e)
        {
            int secim = dtgrtHizmet.SelectedCells[0].RowIndex;
            filtrePersAdi = dtgrtHizmet.Rows[secim].Cells["Ad"].Value.ToString();
            filtrePersSoyadi = dtgrtHizmet.Rows[secim].Cells["Soyad"].Value.ToString();
            filtrePersTc = dtgrtHizmet.Rows[secim].Cells["SgkNo"].Value.ToString();

            //if (tabControl1.SelectedTab == tabPage2)
            //{
            //    txtAdFiltresi.Text = filtrePersAdi;
            //    txtSoyadFiltresi.Text = filtrePersSoyadi;
            //    txtTCnoFiltresi.Text = filtrePersTc;
            //}

        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtBRDadfiltre.Text = "";
            txtBRDSoyadFiltre.Text = "";
            txtBRDTcNoFiltre.Text = "";

        }

        private void txtBRDTcNoFiltre_TextChanged(object sender, EventArgs e)
        {
            BRDTcfiltre();
        }

        private void txtBRDadfiltre_TextChanged(object sender, EventArgs e)
        {
            BRDadfiltre();
        }

        private void txtBRDSoyadFiltre_TextChanged(object sender, EventArgs e)
        {
            BRDSoyadfiltre();
        }

        private void btnHzmLstBilgileriniAl_Click(object sender, EventArgs e)
        {
            string donem = "";
            string kanun = "";
            string mahiyet = "";
            string calisan = "";
            string gun = "";
            decimal spek = 0;
            for (int k = 0; k < Tahakkuk.Rows.Count; k++)
            {
                hizmetListesiToplamlari.Clear();

                donem = Tahakkuk.Rows[k]["thkkukdonem"].ToString();
                kanun = Tahakkuk.Rows[k]["bkanun"].ToString();
                mahiyet = Tahakkuk.Rows[k]["bmahiyet"].ToString();
                if (kanun.Contains("5746"))
                {
                    baglan.Open();
                    using (SQLiteCommand sorgu = new SQLiteCommand("SELECT count(personelid) as HCalisan,sum(Gun) as Hgun, (sum(Ucret) + sum(Ikramiye)) as spek  from HizmetListesi where firmaid = '" + firmaid + "' and subeid = '" + subeid + "' and Kanun_No like '%" + kanun + "%' and Mahiyet = '" + mahiyet + "' and Donem = '" + donem + "'", baglan))
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter();
                        da.SelectCommand = sorgu;
                        da.Fill(hizmetListesiToplamlari);
                    }
                    baglan.Close();

                    baglan.Open();
                    SQLiteCommand guncelle = new SQLiteCommand("update [ilktahakkukbilgi] set dnmhzlistcalisan=@calisan , dnmhzlistgun=@gun , dnmhzlistspek=@spek  where firmaid = '" + firmaid + "' and subeid = '" + subeid + "' and bkanun like '%" + kanun + "%' and bmahiyet = '" + mahiyet + "' and thkkukdonem = '" + donem + "'", baglan);

                    calisan = hizmetListesiToplamlari.Rows[0]["HCalisan"].ToString();
                    gun = hizmetListesiToplamlari.Rows[0]["Hgun"].ToString();
                    spek = hizmetListesiToplamlari.Rows[0]["spek"] != DBNull.Value ? Convert.ToDecimal(hizmetListesiToplamlari.Rows[0]["spek"]) : 0;

                    guncelle.Parameters.AddWithValue("@calisan", calisan);
                    guncelle.Parameters.AddWithValue("@gun", gun);
                    guncelle.Parameters.AddWithValue("@spek", spek);
                    guncelle.ExecuteNonQuery();
                    baglan.Close();
                }
            }
            MessageBox.Show("Hizmet Belgesi İle Tahakkuk Bilgileri Eşleştirildi \n Kayıt İşlemi Tamamlandı");
            dtgrtTAHAKUKLAR.DataSource = Tahakkuk;
            dtgrTahakkukAlanlariDuzenle();
        }

        private void cbmYil_SelectedIndexChanged(object sender, EventArgs e)
        {
            SgkTahakkukbilgileri();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtTHKDonem.Text = "";
            txtTHKKanun.Text = "";
            txtTHKMahiyet.Text = "";
            txtTHKTur.Text = "";
        }

        private void txtTHKDonem_TextChanged(object sender, EventArgs e)
        {
            THKDonemFiltre();
        }

        private void txtTHKTur_TextChanged(object sender, EventArgs e)
        {
            THKTurFiltre();
        }

        private void txtTHKKanun_TextChanged(object sender, EventArgs e)
        {
            THKKanunFiltre();
        }

        private void txtTHKMahiyet_TextChanged(object sender, EventArgs e)
        {
            THKMaliyetfiltre();
        }

        private void ThkAlanlariEslestir()
        {
            decimal fark = 0;
            decimal fark1 = 0;
            string kanun = "";
            for (int i = 0; i < dtgrtTAHAKUKLAR.Rows.Count; i++)
            {
                kanun = dtgrtTAHAKUKLAR.Rows[i].Cells["bkanun"].Value.ToString();
                if (kanun.Contains("5746"))
                {
                    decimal Tspek = dtgrtTAHAKUKLAR.Rows[i].Cells["spek"].Value != DBNull.Value ? Convert.ToDecimal(dtgrtTAHAKUKLAR.Rows[i].Cells["spek"].Value) : 0;
                    decimal Hspek = dtgrtTAHAKUKLAR.Rows[i].Cells["HzmSpek"].Value != DBNull.Value ? Convert.ToDecimal(dtgrtTAHAKUKLAR.Rows[i].Cells["HzmSpek"].Value) : 0;
                    fark = Tspek - Hspek;
                    if (fark < 5 || fark > -5)
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["spek"].Style.BackColor = Color.Green;
                        dtgrtTAHAKUKLAR.Rows[i].Cells["HzmSpek"].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["spek"].Style.BackColor = Color.Red;
                        dtgrtTAHAKUKLAR.Rows[i].Cells["HzmSpek"].Style.BackColor = Color.Red;
                    }

                    int bcalisan = dtgrtTAHAKUKLAR.Rows[i].Cells["bcalisan"].Value != DBNull.Value ? Convert.ToInt32(dtgrtTAHAKUKLAR.Rows[i].Cells["bcalisan"].Value) : 0;
                    int Hcalisan = dtgrtTAHAKUKLAR.Rows[i].Cells["HzmCalisan"].Value != DBNull.Value ? Convert.ToInt32(dtgrtTAHAKUKLAR.Rows[i].Cells["HzmCalisan"].Value) : 0;
                    if (bcalisan == Hcalisan)
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["bcalisan"].Style.BackColor = Color.Green;
                        dtgrtTAHAKUKLAR.Rows[i].Cells["HzmCalisan"].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["bcalisan"].Style.BackColor = Color.Red;
                        dtgrtTAHAKUKLAR.Rows[i].Cells["HzmCalisan"].Style.BackColor = Color.Red;
                    }
                    int bgun = dtgrtTAHAKUKLAR.Rows[i].Cells["bgun"].Value != DBNull.Value ? Convert.ToInt32(dtgrtTAHAKUKLAR.Rows[i].Cells["bgun"].Value) : 0;
                    int Hgun = dtgrtTAHAKUKLAR.Rows[i].Cells["HzmGun"].Value != DBNull.Value ? Convert.ToInt32(dtgrtTAHAKUKLAR.Rows[i].Cells["HzmGun"].Value) : 0;
                    if (bgun == Hgun)
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["bgun"].Style.BackColor = Color.Green;
                        dtgrtTAHAKUKLAR.Rows[i].Cells["HzmGun"].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["bgun"].Style.BackColor = Color.Red;
                        dtgrtTAHAKUKLAR.Rows[i].Cells["HzmGun"].Style.BackColor = Color.Red;
                    }


                    int Ycalisan = dtgrtTAHAKUKLAR.Rows[i].Cells["YeniCalsn"].Value != DBNull.Value ? Convert.ToInt32(dtgrtTAHAKUKLAR.Rows[i].Cells["YeniCalsn"].Value) : 0;
                    if (bcalisan == Ycalisan)
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["YeniCalsn"].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["YeniCalsn"].Style.BackColor = Color.Red;
                    }


                    int Ygun = dtgrtTAHAKUKLAR.Rows[i].Cells["YeniGun"].Value != DBNull.Value ? Convert.ToInt32(dtgrtTAHAKUKLAR.Rows[i].Cells["YeniGun"].Value) : 0;

                    if (bgun == Ygun)
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["YeniGun"].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dtgrtTAHAKUKLAR.Rows[i].Cells["YeniGun"].Style.BackColor = Color.Red;
                    }
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ThkAlanlariEslestir();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SgkTabanTavan();

            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Evet")
            {
                islemKanunu = cmbIslemKanunu.Text;
            }
            string asil = "Mahiyet = '%'";
            string ek = "Mahiyet = '%'";
            string iptal = "Mahiyet = '%'";

            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                asil = "Mahiyet = 'ASIL'";
            }
            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                ek = "Mahiyet = 'EK'";
            }
            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                iptal = "Mahiyet = 'IPTAL'";
            }
            HzmtListesi.Clear();
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT firmPersid as ID, Donem,SgkNo,ad,soyad,Gun,Kanun_No,Mahiyet,Ucret,Ikramiye,(ucret + Ikramiye) as Brt_Spek From HizmetListesi where firmaid='" + firmaid + "' and subeid = '" + subeid + "' AND Kanun_No like '%" + islemKanunu + "%' and ( " + asil + " or " + ek + " or " + iptal + ") order by sgkno", baglan);

            DataTable HzmtListesiHL = new DataTable();
            DataTable Bordrobd = new DataTable();
            da.Fill(HzmtListesiHL);
            baglan.Close();


            Bordro.Clear();
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("select FirmaPersId,PersId, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,CikisTarihi,Net_Brüt,Net_BrtUcret as N_B_Ucret,PrimGunu,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,DamgaVrg,(BesKesintisi+SairKesintiler) as Kesintiler,AylikNetUcret,KanunNo from FirmaBordro WHERE FirmaNo = '" + firmaid + "' and SubeNo='" + subeid + "' ", baglan))
            {
                SQLiteDataAdapter dabdr = new SQLiteDataAdapter();
                dabdr.SelectCommand = sorgu;
                dabdr.Fill(Bordrobd);
            }
            baglan.Close();

            for (int k = 0; k < HzmtListesiHL.Rows.Count; k++)
            {
                string PrId = "";
                string donem = "";
                string TcNo = "";
                string Ad = "";
                string Soyad = "";
                string Gun = "";
                string Kanun = "";
                string Mahiyet = "";
                string ThkAdet = "";
                decimal Ucret = 0;
                decimal ikramiye = 0;
                decimal ToplamSpek = 0;

                decimal BdrBrt = 0;
                decimal BdrMatrah = 0;
                decimal BrtFarki = 0;
                decimal MatrhFarki = 0;
                decimal VergiMatrahi = 0;
                decimal BrdIsciPayi = 0;
                decimal BrdVergi = 0;
                decimal Agi = 0;
                decimal Kesintiler = 0;
                decimal BrdNet = 0;

                decimal AylikNet = 0;
                decimal BrdNett = 0;
                decimal NetFarki = 0;
                decimal Baznet5746 = 0;
                decimal Brut5746 = 0;
                decimal Gunluk5746 = 0;
                decimal AsgTbnGun = 0;
                decimal AsgTvnGun = 0;
                string TbnTvnUygunMu = "";
                decimal EskiYeniSpekFarki = 0;
                decimal YeniMatrah = 0;
                string Aciklama = "";
                string HesDahilMi = "";
                string yil = "";

                PrId = HzmtListesiHL.Rows[k]["ID"].ToString();
                var Tekno5746daVarmiYokmu = HzmtListesiHL.Select("ID='" + PrId + "'");
                foreach (var item in Tekno5746daVarmiYokmu)
                {
                    donem = item["Donem"].ToString();
                    TcNo = item["SgkNo"].ToString();
                    Ad = item["ad"].ToString();
                    Soyad = item["soyad"].ToString();
                    Gun = item["Gun"].ToString();
                    Kanun = item["Kanun_No"].ToString();
                    Mahiyet = item["Mahiyet"].ToString();
                    //ThkAdet = item[""].ToString();
                    Ucret = Convert.ToDecimal(item["Ucret"]);
                    ikramiye = Convert.ToDecimal(item["Ikramiye"]);
                    ToplamSpek = Ucret + ikramiye;

                    if (Kanun.Contains("5746")) continue;

                }


                if (Kanun.Contains("5746"))
                {

                    var bordrodavarmi = Bordrobd.Select("FirmaPersId='" + PrId + "'");
                    foreach (var bdrPers in bordrodavarmi)
                    {
                        BdrBrt = Convert.ToDecimal(bdrPers["ToplamKazanc"]);
                        BdrMatrah = Convert.ToDecimal(bdrPers["SgkMatrahi"]);
                        BrtFarki = ToplamSpek - BdrBrt;
                        MatrhFarki = Ucret - BdrMatrah;
                        VergiMatrahi = Convert.ToDecimal(bdrPers["GvMatrahi"]);
                        BrdIsciPayi = Convert.ToDecimal(bdrPers["SGkIsciPrim"]) + Convert.ToDecimal(bdrPers["IszlikIsciPrim"]);
                        BrdVergi = Convert.ToDecimal(bdrPers["GelirVergisi"]) + Convert.ToDecimal(bdrPers["DamgaVrg"]);
                        Agi = Convert.ToDecimal(bdrPers["Agi"]);
                        Kesintiler = Convert.ToDecimal(bdrPers["Kesintiler"]);
                        BrdNet = (BdrBrt - (BrdIsciPayi + BrdVergi + Agi + Kesintiler));
                        // bence vergi matrahini da ekleyelim
                        AylikNet = Convert.ToDecimal(bdrPers["N_B_Ucret"]);
                        BrdNett = Convert.ToDecimal(bdrPers["AylikNetUcret"]);
                        NetFarki = BrdNett - AylikNet;
                        if (AylikNet == BrdNett || AylikNet > BrdNett)
                        {
                            Baznet5746 = AylikNet;
                        }
                        else
                        {
                            Baznet5746 = AylikNet;
                        }
                        Brut5746 = (Baznet5746 / 85) * 100;
                        if (Brut5746 > 0 && Convert.ToInt32(Gun) > 0)
                        {
                            Gunluk5746 = Brut5746 / Convert.ToInt32(Gun);
                        }
                        else
                        {
                            Gunluk5746 = 0;
                        }


                        var asgTabanTavan = TabanTavan.Select("asg_donem='" + donem + "'");
                        foreach (var tbntvn in asgTabanTavan)
                        {
                            AsgTbnGun = Convert.ToDecimal(tbntvn["asg_taban_ucr"]) / 30;
                            AsgTvnGun = Convert.ToDecimal(tbntvn["asg_tavan_ucr"]) / 30;
                        }
                        if (Gunluk5746 >= AsgTbnGun && Gunluk5746 <= AsgTvnGun)
                        {
                            TbnTvnUygunMu = "Uygun";
                        }
                        else
                        {
                            TbnTvnUygunMu = "Uygun Değil";
                        }

                        EskiYeniSpekFarki = Ucret - Brut5746;
                        if (Brut5746 == Ucret || Brut5746 > Ucret)
                        {
                            YeniMatrah = Ucret;
                        }
                        else
                        {
                            YeniMatrah = Brut5746;
                        }

                        Aciklama = "";
                        HesDahilMi = "";
                    }


                }
                baglan.Open();
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [Bordro5746] (FrmPrId,PersId,Donem,SgkNo,Ad,Soyad,Gun,KanunNo,Mahiyet,ThkAdet,A_Ucret,B_Ikramiye,AB_Toplam,C_TumSpek,D_BDRBrut,E_BDRSgkMatrah,C_D_BrtFarkı,AB_E_MatrahFarkı,BRDIsciPayi_1,VergiMatrahi,BRDVergi_2,Agi_3,Kesintiler_4,BrdNet_1234,F_AylıkNet,G_BordroNet,F_G_NetFarkı,Baz_Net_5746,Brut_5746,Gunluk_5746,Asg_TbnGunluk,Asg_TvnGunluk,Tbn_Tvn_UygunMu,EskiYeniSpekFarkı,YeniAPHBMatrah,Acıklama,HesaplansınMı) values (@FrmPrId,@PersId,@Donem, @SgkNo, @Ad, @Soyad, @Gun, @KanunNo, @Mahiyet, @ThkAdet, @A_Ucret, @B_Ikramiye, @AB_Toplam, @C_TumSpek, @D_BDRBrut, @E_BDRSgkMatrah, @C_D_BrtFarkı, @AB_E_MatrahFarkı, @BRDIsciPayi_1, @VergiMatrahi, @BRDVergi_2, @Agi_3, @Kesintiler_4, @BrdNet_1234, @F_AylıkNet, @G_BordroNet, @F_G_NetFarkı, @Baz_Net_5746, @Brut_5746, @Gunluk_5746, @Asg_TbnGunluk, @Asg_TvnGunluk, @Tbn_Tvn_UygunMu, @EskiYeniSpekFarkı, @YeniAPHBMatrah, @Acıklama, @HesaplansınMı)", baglan);


                ekle.Parameters.AddWithValue("@FrmPrId", PrId);
                ekle.Parameters.AddWithValue("@PersId", 0);
                ekle.Parameters.AddWithValue("@Donem", donem);
                ekle.Parameters.AddWithValue("@SgkNo", TcNo);
                ekle.Parameters.AddWithValue("@Ad", Ad);
                ekle.Parameters.AddWithValue("@Soyad", Soyad);
                ekle.Parameters.AddWithValue("@Gun", Gun);
                ekle.Parameters.AddWithValue("@KanunNo", Kanun);
                ekle.Parameters.AddWithValue("@Mahiyet", Mahiyet);
                ekle.Parameters.AddWithValue("@ThkAdet", ThkAdet);
                ekle.Parameters.AddWithValue("@A_Ucret", Ucret);
                ekle.Parameters.AddWithValue("@B_Ikramiye", ikramiye);
                ekle.Parameters.AddWithValue("@AB_Toplam", ToplamSpek);
                ekle.Parameters.AddWithValue("@C_TumSpek", 0);
                ekle.Parameters.AddWithValue("@D_BDRBrut", BdrBrt);
                ekle.Parameters.AddWithValue("@E_BDRSgkMatrah", BdrMatrah);
                ekle.Parameters.AddWithValue("@C_D_BrtFarkı", BrtFarki);
                ekle.Parameters.AddWithValue("@AB_E_MatrahFarkı", MatrhFarki);
                ekle.Parameters.AddWithValue("@BRDIsciPayi_1", BrdIsciPayi);
                ekle.Parameters.AddWithValue("@VergiMatrahi", VergiMatrahi);
                ekle.Parameters.AddWithValue("@BRDVergi_2", BrdVergi);
                ekle.Parameters.AddWithValue("@Agi_3", Agi);
                ekle.Parameters.AddWithValue("@Kesintiler_4", Kesintiler);
                ekle.Parameters.AddWithValue("@BrdNet_1234", BrdNet);
                ekle.Parameters.AddWithValue("@F_AylıkNet", AylikNet);
                ekle.Parameters.AddWithValue("@G_BordroNet", BrdNett);
                ekle.Parameters.AddWithValue("@F_G_NetFarkı", NetFarki);
                ekle.Parameters.AddWithValue("@Baz_Net_5746", Baznet5746);
                ekle.Parameters.AddWithValue("@Brut_5746", Brut5746);
                ekle.Parameters.AddWithValue("@Gunluk_5746", Gunluk5746);
                ekle.Parameters.AddWithValue("@Asg_TbnGunluk", AsgTbnGun);
                ekle.Parameters.AddWithValue("@Asg_TvnGunluk", AsgTvnGun);
                ekle.Parameters.AddWithValue("@Tbn_Tvn_UygunMu", TbnTvnUygunMu);
                ekle.Parameters.AddWithValue("@EskiYeniSpekFarkı", EskiYeniSpekFarki);
                ekle.Parameters.AddWithValue("@YeniAPHBMatrah", YeniMatrah);
                ekle.Parameters.AddWithValue("@Acıklama", Aciklama);
                ekle.Parameters.AddWithValue("@HesaplansınMı", HesDahilMi);


                ekle.ExecuteNonQuery();
                baglan.Close();

            }
            MessageBox.Show("Verier Başarı İle Veri Tabanına Eklendi");



            // dtgrtHizmet.DataSource = HzmtListesi;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Evet")
            {
                islemKanunu = cmbIslemKanunu.Text;
            }

            HzmtListesi.Clear();
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT firmPersid as ID, Donem,SgkNo,ad,soyad,Gun,Kanun_No,Mahiyet,Ucret,Ikramiye,(ucret + Ikramiye) as Brt_Spek,firmaid,subeid From HizmetListesi where firmaid='" + firmaid + "' and subeid = '" + subeid + "'", baglan);

            DataTable HzmtListesiHL = new DataTable();

            da.Fill(HzmtListesiHL);
            lblIslemDurumu.Text = cmbIslemKanunu.Text + " Nolu Kanun için Hizmet Listesi Diğer Kanunlardan Arındırılıyor...";
            System.Threading.Thread.Sleep(1000);
            progressBar1.Maximum = HzmtListesiHL.Rows.Count;
            baglan.Close();
            baglan.Open();
            SQLiteDataAdapter Hsplda = new SQLiteDataAdapter("SELECT count(FrmPrId) as sayi from  Bordro5746 where firmaid='" + firmaid + "' and subeid = '" + subeid + "'", baglan);

            DataTable YeniHzmtListesiHL = new DataTable();

            Hsplda.Fill(YeniHzmtListesiHL);
            baglan.Close();
            if (Convert.ToInt32(YeniHzmtListesiHL.Rows[0][0]) > 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Hizmet Listesi Daha Önceden aktarılmış, Listeyi Silerek Yeniden oluşturmak istiyormusunuz.", "Dikkat", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    baglan.Open();
                    SQLiteCommand komut = new SQLiteCommand("Delete from Bordro5746 where firmaid='" + firmaid + "' and subeid = '" + subeid + "'", baglan);
                    komut.ExecuteNonQuery();
                    MessageBox.Show(comboBox1.Text + " Firmasına ait tüm veriler silinmiştir");
                    baglan.Close();
                }
            }
            for (int k = 0; k < HzmtListesiHL.Rows.Count - 1; k++)
            {
                progressBar1.Value = k;
                string FrmPrId = "";
                string Kanun = "";
                FrmPrId = HzmtListesiHL.Rows[k]["ID"].ToString();
                var Tekno5746daVarmiYokmu = HzmtListesiHL.Select("ID='" + FrmPrId + "'");

                foreach (var item in Tekno5746daVarmiYokmu)
                {
                    Kanun = item["Kanun_No"].ToString();
                    if (Kanun.Contains("5746")) continue;

                }
                if (!Kanun.Contains("5746"))
                {
                    HzmtListesiHL.Rows[k].Delete();

                }

            }
            HzmtListesiHL.AcceptChanges();
            progressBar1.Maximum = HzmtListesiHL.Rows.Count;


            lblIslemDurumu.Text = "Arındırılmış Hizmet Listesi Veri Tamanına Kayıt ediliyor... Lütfen Bekleyiniz.";
            System.Threading.Thread.Sleep(1000);
            for (int i = 0; i < HzmtListesiHL.Rows.Count; i++)
            {
                progressBar1.Value = i;
                // string PrId = HzmtListesiHL.Rows[i]["PersId"].ToString();
                string FrmPrId = HzmtListesiHL.Rows[i]["ID"].ToString();
                string donem = HzmtListesiHL.Rows[i]["Donem"].ToString();
                string TcNo = HzmtListesiHL.Rows[i]["SgkNo"].ToString();
                string Ad = HzmtListesiHL.Rows[i]["ad"].ToString();
                string Soyad = HzmtListesiHL.Rows[i]["soyad"].ToString();
                string Gun = HzmtListesiHL.Rows[i]["Gun"].ToString();
                string Kanun = HzmtListesiHL.Rows[i]["Kanun_No"].ToString();
                string Mahiyet = HzmtListesiHL.Rows[i]["Mahiyet"].ToString();

                decimal Ucret = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Ucret"]);
                decimal ikramiye = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Ikramiye"]);
                decimal ToplamSpek = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Brt_Spek"]);
                string firmaid = HzmtListesiHL.Rows[i]["firmaid"].ToString();
                string subeid = HzmtListesiHL.Rows[i]["subeid"].ToString();

                decimal TumSpek = 0;
                int ThkAdet = 0;


                var personelSayiveToplami = HzmtListesiHL.Select("ID='" + FrmPrId + "'"); // seçili kanun maddesi personeli başka bir kanundan faydalandı ise seç
                if (Kanun.Contains("5746"))
                {
                    foreach (var adet in personelSayiveToplami)
                    {
                        TumSpek = Convert.ToDecimal(adet["Brt_Spek"]);
                        ThkAdet += 1;
                    }
                }


                baglan.Open();
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [Bordro5746] (FrmPrId,Donem,SgkNo,Ad,Soyad,Gun,KanunNo,Mahiyet,ThkAdet,A_Ucret,B_Ikramiye,AB_Toplam,C_TumSpek,firmaid,subeid) values (@FrmPrId,@Donem, @SgkNo, @Ad, @Soyad, @Gun, @KanunNo, @Mahiyet, @ThkAdet, @A_Ucret, @B_Ikramiye, @AB_Toplam, @C_TumSpek,@firmaid,@subeid)", baglan);


                ekle.Parameters.AddWithValue("@FrmPrId", FrmPrId);
                //ekle.Parameters.AddWithValue("@PersId", 0);
                ekle.Parameters.AddWithValue("@Donem", donem);
                ekle.Parameters.AddWithValue("@SgkNo", TcNo);
                ekle.Parameters.AddWithValue("@Ad", Ad);
                ekle.Parameters.AddWithValue("@Soyad", Soyad);
                ekle.Parameters.AddWithValue("@Gun", Gun);
                ekle.Parameters.AddWithValue("@KanunNo", Kanun);
                ekle.Parameters.AddWithValue("@Mahiyet", Mahiyet);
                ekle.Parameters.AddWithValue("@ThkAdet", ThkAdet);
                ekle.Parameters.AddWithValue("@A_Ucret", Ucret);
                ekle.Parameters.AddWithValue("@B_Ikramiye", ikramiye);
                ekle.Parameters.AddWithValue("@AB_Toplam", ToplamSpek);
                ekle.Parameters.AddWithValue("@C_TumSpek", TumSpek);
                ekle.Parameters.AddWithValue("@firmaid", firmaid);
                ekle.Parameters.AddWithValue("@subeid", subeid);


                ekle.ExecuteNonQuery();
                baglan.Close();

            }
            MessageBox.Show("Verier Başarı İle Veri Tabanına Eklendi");

            baglan.Open();
            SQLiteDataAdapter Yhizmtda = new SQLiteDataAdapter("SELECT Donem,SgkNo,Ad,Soyad,Gun,KanunNo,Mahiyet,ThkAdet,A_Ucret,B_Ikramiye,AB_Toplam,C_TumSpek,D_BDRBrut,E_BDRSgkMatrah,C_D_BrtFarkı,AB_E_MatrahFarkı,BRDIsciPayi_1,VergiMatrahi,BRDVergi_2,Agi_3,Kesintiler_4,BrdNet_1234,F_AylıkNet,G_BordroNet,F_G_NetFarkı,Baz_Net_5746,Brut_5746,Gunluk_5746,Asg_TbnGunluk,Asg_TvnGunluk,Tbn_Tvn_UygunMu,EskiYeniSpekFarkı,YeniAPHBMatrah,Acıklama,HesaplansınMı from  Bordro5746 where firmaid='" + firmaid + "' and subeid = '" + subeid + "' and Donem = '" + donem + "'", baglan);

            DataTable YHizmetListesi = new DataTable();

            Yhizmtda.Fill(YHizmetListesi);
            baglan.Close();

            dataGridView1.DataSource = YHizmetListesi;


        }

        private void button9_Click(object sender, EventArgs e)
        {

            SgkTabanTavan();

            DataTable YeniHsHizmetListesi = new DataTable();
            DataTable Bordrobd = new DataTable();

            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Evet")
            {
                islemKanunu = cmbIslemKanunu.Text;
            }

            YeniHsHizmetListesi.Clear();
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT TeknoPrId,FrmPrId,Donem, SgkNo, Ad, Soyad, Gun, KanunNo, Mahiyet, ThkAdet, A_Ucret, B_Ikramiye, AB_Toplam, C_TumSpek, D_BDRBrut, E_BDRSgkMatrah, C_D_BrtFarkı, AB_E_MatrahFarkı, BRDIsciPayi_1, VergiMatrahi, BRDVergi_2, Agi_3, Kesintiler_4, BrdNet_1234, F_AylıkNet, G_BordroNet, F_G_NetFarkı, Baz_Net_5746, Brut_5746, Gunluk_5746, Asg_TbnGunluk, Asg_TvnGunluk, Tbn_Tvn_UygunMu, EskiYeniSpekFarkı, YeniAPHBMatrah, Acıklama, HesaplansınMı from Bordro5746 where firmaid = '" + firmaid + "' and subeid = '" + subeid + "'", baglan);
            da.Fill(YeniHsHizmetListesi);
            baglan.Close();


            Bordrobd.Clear();
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("select FirmaPersId,PersId, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,CikisTarihi,Net_Brüt,Net_BrtUcret as N_B_Ucret,PrimGunu,AylikBrutUcret,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,DamgaVrg,(BesKesintisi+SairKesintiler) as Kesintiler,AylikNetUcret,KanunNo from FirmaBordro WHERE FirmaNo = '" + firmaid + "' and SubeNo='" + subeid + "' ", baglan))
            {
                SQLiteDataAdapter dabdr = new SQLiteDataAdapter();
                dabdr.SelectCommand = sorgu;
                dabdr.Fill(Bordrobd);
            }
            baglan.Close();





            progressBar1.Maximum = YeniHsHizmetListesi.Rows.Count;
            for (int k = 0; k < YeniHsHizmetListesi.Rows.Count; k++)
            {
                progressBar1.Value = k;
                int TeknoPrId = Convert.ToInt32(YeniHsHizmetListesi.Rows[k]["TeknoPrId"]);
                string PrId = YeniHsHizmetListesi.Rows[k]["FrmPrId"].ToString();
                string donem = YeniHsHizmetListesi.Rows[k]["Donem"].ToString();
                string Gun = YeniHsHizmetListesi.Rows[k]["Gun"].ToString();
                string Kanun = YeniHsHizmetListesi.Rows[k]["KanunNo"].ToString();
                string Mahiyet = YeniHsHizmetListesi.Rows[k]["Mahiyet"].ToString();
                string ThkAdet = YeniHsHizmetListesi.Rows[k]["ThkAdet"].ToString();
                decimal Ucret = Convert.ToDecimal(YeniHsHizmetListesi.Rows[k]["A_Ucret"]);
                decimal ikramiye = Convert.ToDecimal(YeniHsHizmetListesi.Rows[k]["B_Ikramiye"]);
                decimal ToplamSpek = Convert.ToDecimal(YeniHsHizmetListesi.Rows[k]["AB_Toplam"]);
                decimal GenelSgkMatrah = Convert.ToDecimal(YeniHsHizmetListesi.Rows[k]["C_TumSpek"]);

                string BrutNet = "";
                decimal BdrBrt = 0;
                decimal BdrMatrah = 0;
                decimal BrtFarki = 0;
                decimal MatrhFarki = 0;
                decimal VergiMatrahi = 0;
                decimal BrdIsciPayi = 0;
                decimal BrdVergi = 0;
                decimal Agi = 0;
                decimal Kesintiler = 0;
                decimal BrdNet = 0;

                decimal AylikNet = 0;
                decimal BrdNett = 0;
                decimal NetFarki = 0;
                decimal Baznet5746 = 0;
                decimal Brut5746 = 0;
                decimal Gunluk5746 = 0;
                decimal AsgTbnGun = 0;
                decimal AsgTvnGun = 0;
                string TbnTvnUygunMu = "";
                decimal EskiYeniSpekFarki = 0;
                decimal YeniMatrah = 0;
                string Aciklama = "";
                string HesDahilMi = "";
                string yil = "";
                string VergiAciklama = "";
                string netAciklama = "";
                string brutAciklama = "";
                string brtNetAciklama = "";
                string matrahAciklama = "";
                string kanunAciklama = "";
                decimal AylikKazanc = 0;
                decimal KmVergiMatrahi = 0;
                decimal EkOdemeler = 0;
                decimal farkmatrah = 0;


                decimal digerucreti = 0;
                var bordrodavarmi = Bordrobd.Select("FirmaPersId='" + PrId + "'");
                foreach (var bdrPers in bordrodavarmi)
                {

                    AylikKazanc = Convert.ToDecimal(bdrPers["N_B_Ucret"]);

                    BrutNet = bdrPers["Net_Brüt"].ToString();
                    BdrBrt = Convert.ToDecimal(bdrPers["ToplamKazanc"]);
                    EkOdemeler = BdrBrt - Convert.ToDecimal(bdrPers["AylikBrutUcret"]);
                    BdrMatrah = Convert.ToDecimal(bdrPers["SgkMatrahi"]);
                    BrtFarki = ToplamSpek - BdrBrt;
                    MatrhFarki = Ucret - BdrMatrah;
                    KmVergiMatrahi = Convert.ToDecimal(bdrPers["KumVergMatr"]);
                    VergiMatrahi = Convert.ToDecimal(bdrPers["GvMatrahi"]);
                    BrdIsciPayi = Convert.ToDecimal(bdrPers["SGkIsciPrim"]) + Convert.ToDecimal(bdrPers["IszlikIsciPrim"]);
                    BrdVergi = Convert.ToDecimal(bdrPers["GelirVergisi"]) + Convert.ToDecimal(bdrPers["DamgaVrg"]);
                    Agi = Convert.ToDecimal(bdrPers["Agi"]);
                    Kesintiler = Convert.ToDecimal(bdrPers["Kesintiler"]);
                    BrdNet = (BdrBrt - (BrdIsciPayi + BrdVergi + Agi + Kesintiler));// manuel hesaplanan aylik net kazanc
                    BrdNett = Convert.ToDecimal(bdrPers["AylikNetUcret"]); // bize gelen bordor üzerinde hesaplanmış olan aylik net 
                                                                           // 5746 kanun kapsamı dışındaki kazanç sağlayan personelin net ücretine ulaşmaya çalışıyoruz. 

                    if (!Kanun.Contains("5746"))
                    {

                        BrdIsciPayi = GenelSgkMatrah * 15 / 100;
                        vMatrahi = GenelSgkMatrah - BrdIsciPayi;// Vergi Matrahını sadece 5746 dışındaki kanun Matrahını dikkate alarak hesaplıyoruz. 
                        kVMatrahi = KmVergiMatrahi;
                        VYili = donem.Substring(0, 4);
                        BordroHesapla();
                        BrdVergi = gv + Convert.ToDecimal(Convert.ToDouble(vMatrahi) * 0.00759);
                        VergiMatrahi = vMatrahi;
                        BdrBrt = GenelSgkMatrah;
                        BdrMatrah = GenelSgkMatrah;
                        BrtFarki = GenelSgkMatrah - GenelSgkMatrah;
                        MatrhFarki = GenelSgkMatrah - GenelSgkMatrah;

                        BrdNet = EkOdemeler - BrdIsciPayi - BrdVergi;
                        BrdNett = EkOdemeler - BrdIsciPayi - BrdVergi;
                        AylikKazanc = EkOdemeler - BrdIsciPayi - BrdVergi;


                    }
                    else
                    {
                        

                        if (MatrhFarki < 0)
                        {
                            farkmatrah = Math.Abs(MatrhFarki);

                            decimal farkiIsciPayi = farkmatrah * 15 / 100;
                            vMatrahi = farkmatrah - farkiIsciPayi;// Vergi Matrahını sadece 5746 dışındaki kanun Matrahını dikkate alarak hesaplıyoruz. 
                            kVMatrahi = KmVergiMatrahi;
                            VYili = donem.Substring(0, 4);
                            BordroHesapla();

                            decimal farkVergi = gv + Convert.ToDecimal(Convert.ToDouble(vMatrahi) * 0.00759);
                            VergiMatrahi = vMatrahi;
                            decimal farknet = EkOdemeler - (farkiIsciPayi + farkVergi);

                            //BrdIsciPayi = BrdIsciPayi-farkiIsciPayi;
                            //BrdVergi = BrdVergi-farkVergi;

                            BrdNet = BrdNet - farknet;
                        }
                        //else
                        //{
                            if (BrutNet.Contains("Net") || BrutNet.Contains("NET"))
                            {
                                if (Convert.ToInt32(Gun) == 30)
                                {
                                    AylikNet = AylikKazanc;
                                }
                                else
                                {
                                    AylikNet = (AylikKazanc / 30) * (Convert.ToInt32(Gun));
                                }

                            }
                            else
                            {
                                AylikNet = BrdNet;//küsüratlardan kurtulmak için bordroda gelen net ücret baz alındı 
                            }
                        




                        NetFarki = BrdNett - AylikNet;
                        if (AylikNet >= BrdNet)
                        {
                            Baznet5746 = AylikNet;
                        }
                        else
                        {
                            Baznet5746 = BrdNet;
                        }
                        Brut5746 = (Baznet5746 / 85) * 100;

                        if (Brut5746 > 0 && Convert.ToInt32(Gun) > 0)
                        {
                            Gunluk5746 = Brut5746 / Convert.ToInt32(Gun);
                        }
                        else
                        {
                            Gunluk5746 = 0;
                        }


                        var asgTabanTavan = TabanTavan.Select("asg_donem='" + donem + "'");
                        foreach (var tbntvn in asgTabanTavan)
                        {
                            AsgTbnGun = Convert.ToDecimal(tbntvn["asg_taban_ucr"]) / 30;
                            AsgTvnGun = Convert.ToDecimal(tbntvn["asg_tavan_ucr"]) / 30;
                        }
                        if (Gunluk5746 > AsgTbnGun || Gunluk5746 < AsgTvnGun)
                        {
                            TbnTvnUygunMu = "Uygun";
                        }
                        else
                        {
                            TbnTvnUygunMu = "Uygun Değil";
                        }


                        if (Brut5746 >= Ucret)
                        {
                            YeniMatrah = Ucret;
                        }
                        if ((Gunluk5746 * Convert.ToInt32(Gun)) > (AsgTvnGun * Convert.ToInt32(Gun)))
                        {
                            YeniMatrah = Ucret;
                            TbnTvnUygunMu = "Sgk Matrahi Asgari Ücret Tavanından Fazla";
                        }
                        if ((Gunluk5746 * Convert.ToInt32(Gun)) < (AsgTbnGun * Convert.ToInt32(Gun)))
                        {
                            YeniMatrah = Ucret;
                            TbnTvnUygunMu = "Sgk Matrahi Asgari Ücret Tabanından Düşük";
                        }
                        if (YeniMatrah != Ucret)
                        {
                            YeniMatrah = Brut5746;
                        }
                        EskiYeniSpekFarki = Ucret - YeniMatrah;
                        //HESAPLAMAYA DAHİL EDİLSİN Mİ EDİLMESİN Mİ ? 
                        //1. krider 5746 matrahlı personel olmuş olması
                        if (!Kanun.Contains("5746"))
                        {
                            HesDahilMi = "Hayır";
                            kanunAciklama = " 5746 Kanun Kapsamında Değil.., ";
                        }
                        else
                        {
                            //2. kriter anlaşma net ücret üzerinden olmuş olmalı
                            if (!BrutNet.Contains("NET"))
                            {
                                HesDahilMi = "Hayır";
                                brtNetAciklama = "Brüt Ücret Hesaplanamaz., ";
                            }
                            else
                            {
                                //3. Kriter Ücretinden vergi kesintisi yapılmış mı 
                                if (BrdVergi < 1 || VergiMatrahi < 1)
                                {
                                    HesDahilMi = "Hayır";
                                    VergiAciklama = "Ücretten Vergi Kesilmemiş, ";
                                }
                                else
                                {
                                    //4. kriter yeni hesaplanan spek eski spekmi baz almış yeni 5746 speki mi 
                                    if (YeniMatrah == Brut5746)
                                    {
                                        HesDahilMi = "Evet";
                                    }
                                    else
                                    {
                                        HesDahilMi = "Hayır";
                                    }
                                }

                            }


                            if (NetFarki > 1 || NetFarki < -1)
                            {
                                netAciklama = "Net Ücret Tutmuyor, ";
                            }

                            if (MatrhFarki > 1 || MatrhFarki < -1)
                            {
                                matrahAciklama = "Sgk Matrahında Fark Var, ";
                            }

                        }

                        Aciklama = kanunAciklama + " " + matrahAciklama + " " + VergiAciklama;


                    }

                }

                baglan.Open();
                SQLiteCommand guncelle = new SQLiteCommand("update Bordro5746 set  Brt_Net=@Brt_Net,D_BDRBrut=@D_BDRBrut,E_BDRSgkMatrah=@E_BDRSgkMatrah,C_D_BrtFarkı=@C_D_BrtFarkı,AB_E_MatrahFarkı=@AB_E_MatrahFarkı,BRDIsciPayi_1=@BRDIsciPayi_1,VergiMatrahi=@VergiMatrahi,BRDVergi_2=@BRDVergi_2,Agi_3= @Agi_3,Kesintiler_4= @Kesintiler_4,BrdNet_1234=@BrdNet_1234,F_AylıkNet=@F_AylıkNet,G_BordroNet= @G_BordroNet,F_G_NetFarkı= @F_G_NetFarkı,Baz_Net_5746=@Baz_Net_5746,Brut_5746= @Brut_5746,Gunluk_5746= @Gunluk_5746,Asg_TbnGunluk=@Asg_TbnGunluk,Asg_TvnGunluk= @Asg_TvnGunluk,Tbn_Tvn_UygunMu= @Tbn_Tvn_UygunMu,EskiYeniSpekFarkı= @EskiYeniSpekFarkı,YeniAPHBMatrah= @YeniAPHBMatrah,Acıklama=@Acıklama,HesaplansınMı= @HesaplansınMı where TeknoPrId ='" + TeknoPrId + "'", baglan);

                guncelle.Parameters.AddWithValue("@Brt_Net", BrutNet);
                guncelle.Parameters.AddWithValue("@D_BDRBrut", BdrBrt);
                guncelle.Parameters.AddWithValue("@E_BDRSgkMatrah", BdrMatrah);
                guncelle.Parameters.AddWithValue("@C_D_BrtFarkı", BrtFarki);
                guncelle.Parameters.AddWithValue("@AB_E_MatrahFarkı", MatrhFarki);
                guncelle.Parameters.AddWithValue("@BRDIsciPayi_1", BrdIsciPayi);
                guncelle.Parameters.AddWithValue("@VergiMatrahi", VergiMatrahi);
                guncelle.Parameters.AddWithValue("@BRDVergi_2", BrdVergi);
                guncelle.Parameters.AddWithValue("@Agi_3", Agi);
                guncelle.Parameters.AddWithValue("@Kesintiler_4", Kesintiler);
                guncelle.Parameters.AddWithValue("@BrdNet_1234", BrdNet);
                guncelle.Parameters.AddWithValue("@F_AylıkNet", AylikNet);
                guncelle.Parameters.AddWithValue("@G_BordroNet", BrdNett);
                guncelle.Parameters.AddWithValue("@F_G_NetFarkı", NetFarki);
                guncelle.Parameters.AddWithValue("@Baz_Net_5746", Baznet5746);
                guncelle.Parameters.AddWithValue("@Brut_5746", Brut5746);
                guncelle.Parameters.AddWithValue("@Gunluk_5746", Gunluk5746);
                guncelle.Parameters.AddWithValue("@Asg_TbnGunluk", AsgTbnGun);
                guncelle.Parameters.AddWithValue("@Asg_TvnGunluk", AsgTvnGun);
                guncelle.Parameters.AddWithValue("@Tbn_Tvn_UygunMu", TbnTvnUygunMu);
                guncelle.Parameters.AddWithValue("@EskiYeniSpekFarkı", EskiYeniSpekFarki);
                guncelle.Parameters.AddWithValue("@YeniAPHBMatrah", YeniMatrah);
                guncelle.Parameters.AddWithValue("@Acıklama", Aciklama);
                guncelle.Parameters.AddWithValue("@HesaplansınMı", HesDahilMi);


                guncelle.ExecuteNonQuery();
                baglan.Close();

            }
            MessageBox.Show("Verier Başarı İle Veri Tabanına Eklendi");
            YhizmetHespalama();
            hesaplaDtgrAlanlariDuzenle();


        }

        private void hesaplaDtgrAlanlariDuzenle()
        {
            dataGridView1.Columns["A_Ucret"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["B_Ikramiye"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["AB_Toplam"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["C_TumSpek"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["D_BDRBrut"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["E_BDRSgkMatrah"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["C_D_BrtFarkı"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["AB_E_MatrahFarkı"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["BRDIsciPayi_1"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["VergiMatrahi"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["BRDVergi_2"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Agi_3"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Kesintiler_4"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["BrdNet_1234"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["F_AylıkNet"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["G_BordroNet"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["F_G_NetFarkı"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Baz_Net_5746"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Brut_5746"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Gunluk_5746"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Asg_TbnGunluk"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Asg_TvnGunluk"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["EskiYeniSpekFarkı"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["YeniAPHBMatrah"].DefaultCellStyle.Format = "N2";

            dataGridView1.Columns["Gunluk_5746"].Visible = false;
            dataGridView1.Columns["Asg_TbnGunluk"].Visible = false;
            dataGridView1.Columns["Asg_TvnGunluk"].Visible = false;


            //dataGridView1.Columns["Brt_Net"].Frozen = true;// sütün dondurma

            //combo.ValueMember= dataGridView1.Columns["HesaplansınMı"].ToString();
            //combo.Name = dataGridView1.Columns["HesaplansınMı"];
            //combo.Items.AddRange("Evet", "Hayır");

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Evet")
            {
                islemKanunu = cmbIslemKanunu.Text;
            }

            HzmtListesi.Clear();
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT firmPersid as ID, Donem,SgkNo,ad,soyad,Gun,Kanun_No,Mahiyet,Ucret,Ikramiye,(ucret + Ikramiye) as Brt_Spek,firmaid,subeid From HizmetListesi where firmaid='" + firmaid + "' and subeid = '" + subeid + "'", baglan);

            DataTable HzmtListesiHL = new DataTable();

            da.Fill(HzmtListesiHL);
            lblIslemDurumu.Text = cmbIslemKanunu.Text + " Nolu Kanun için Hizmet Listesi Diğer Kanunlardan Arındırılıyor...";
            System.Threading.Thread.Sleep(1000);
            progressBar1.Maximum = HzmtListesiHL.Rows.Count;
            baglan.Close();
            baglan.Open();
            SQLiteDataAdapter Hsplda = new SQLiteDataAdapter("SELECT count(FrmPrId) as sayi from  Bordro5746 where firmaid='" + firmaid + "' and subeid = '" + subeid + "'", baglan);

            DataTable yeniHizmetListPersonelVarmi = new DataTable();

            Hsplda.Fill(yeniHizmetListPersonelVarmi);
            baglan.Close();

            if (Convert.ToInt32(yeniHizmetListPersonelVarmi.Rows[0][0]) > 0)
            {
                DialogResult msg = new DialogResult();
                msg = MessageBox.Show("Hizmet Listesi Daha Önceden aktarılmış, Listeyi Silerek Yeniden oluşturmak istiyormusunuz.", "Dikkat", MessageBoxButtons.YesNo);
                if (msg == DialogResult.Yes)
                {
                    baglan.Open();
                    SQLiteCommand komut = new SQLiteCommand("Delete from Bordro5746 where firmaid='" + firmaid + "' and subeid = '" + subeid + "'", baglan);
                    komut.ExecuteNonQuery();
                    MessageBox.Show(comboBox1.Text + " Firmasına ait tüm veriler silinmiştir");
                    baglan.Close();
                }
            }
            for (int k = 0; k < HzmtListesiHL.Rows.Count; k++)
            {
                progressBar1.Value = k;
                string FrmPrId = "";
                string Kanun = "";
                FrmPrId = HzmtListesiHL.Rows[k]["ID"].ToString();


                Kanun = HzmtListesiHL.Rows[k]["Kanun_No"].ToString();
                decimal Ucret = Convert.ToDecimal(HzmtListesiHL.Rows[k]["Ucret"]);
                decimal Ikramiye = Convert.ToDecimal(HzmtListesiHL.Rows[k]["Ikramiye"]);
                string Mahiyet = HzmtListesiHL.Rows[k]["Mahiyet"].ToString();
                string gun = HzmtListesiHL.Rows[k]["Gun"].ToString();

                if (HzmtListesiHL.Rows[k]["Mahiyet"].ToString().Contains("IPTAL"))
                {
                    string iptalTahkkuk = "";
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
                            HzmtListesiHL.Rows[k].Delete();
                            break;
                        }

                    }

                    progressBar1.Value = k;
                }


            }
            HzmtListesiHL.AcceptChanges();
            progressBar1.Maximum = HzmtListesiHL.Rows.Count;

            System.Threading.Thread.Sleep(500);
            lblIslemDurumu.Text = cmbIslemKanunu.Text + " Kanun Maddeli Personellerin Mükerrer Kanun Maddeleri Arındırılıyor...";
            System.Threading.Thread.Sleep(500);


            for (int j = 0; j < HzmtListesiHL.Rows.Count; j++)
            {
                progressBar1.Value = j;
                string FrmPrId = "";
                string Kanun = "";
                FrmPrId = HzmtListesiHL.Rows[j]["ID"].ToString();
                var Tekno5746daVarmiYokmu = HzmtListesiHL.Select("ID='" + FrmPrId + "'");

                foreach (var item in Tekno5746daVarmiYokmu)
                {
                    Kanun = item["Kanun_No"].ToString();
                    if (Kanun.Contains("5746")) continue;

                }
                if (!Kanun.Contains("5746"))
                {

                    HzmtListesiHL.Rows[j].Delete();

                }
            }
            HzmtListesiHL.AcceptChanges();
            System.Threading.Thread.Sleep(500);
            lblIslemDurumu.Text = "Arındırılmış Hizmet Listesi Veri Tabanına Kayıt ediliyor... Lütfen Bekleyiniz.";
            System.Threading.Thread.Sleep(500);

            progressBar1.Maximum = HzmtListesiHL.Rows.Count;
            for (int i = 0; i < HzmtListesiHL.Rows.Count; i++)
            {
                progressBar1.Value = i;
                // string PrId = HzmtListesiHL.Rows[i]["PersId"].ToString();
                string FrmPrId = HzmtListesiHL.Rows[i]["ID"].ToString();
                string donem = HzmtListesiHL.Rows[i]["Donem"].ToString();
                string TcNo = HzmtListesiHL.Rows[i]["SgkNo"].ToString();
                string Ad = HzmtListesiHL.Rows[i]["ad"].ToString();
                string Soyad = HzmtListesiHL.Rows[i]["soyad"].ToString();
                string Gun = HzmtListesiHL.Rows[i]["Gun"].ToString();
                string Kanun = HzmtListesiHL.Rows[i]["Kanun_No"].ToString();
                string Mahiyet = HzmtListesiHL.Rows[i]["Mahiyet"].ToString();

                decimal Ucret = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Ucret"]);
                decimal ikramiye = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Ikramiye"]);
                decimal ToplamSpek = Convert.ToDecimal(HzmtListesiHL.Rows[i]["Brt_Spek"]);
                string firmaid = HzmtListesiHL.Rows[i]["firmaid"].ToString();
                string subeid = HzmtListesiHL.Rows[i]["subeid"].ToString();

                decimal TumSpek = 0;
                int ThkAdet = 0;




                if (!Kanun.Contains("5746"))
                {
                    var personelSayiveToplami = HzmtListesiHL.Select("ID='" + FrmPrId + "'"); // seçili kanun maddesi personeli başka bir kanundan faydalandı ise seç
                    foreach (var adet in personelSayiveToplami)
                    {
                        string knn = adet["Kanun_No"].ToString();
                        if (!knn.Contains("5746"))
                        {
                            TumSpek += Convert.ToDecimal(adet["Ucret"]) + Convert.ToDecimal(adet["Ikramiye"]);
                            ThkAdet += 1;
                        }
                        else
                        {
                            TumSpek += Convert.ToDecimal(adet["Ikramiye"]);
                            ThkAdet += 1;
                        }
                    }
                }




                baglan.Open();
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [Bordro5746] (FrmPrId,Donem,SgkNo,Ad,Soyad,Gun,KanunNo,Mahiyet,ThkAdet,A_Ucret,B_Ikramiye,AB_Toplam,C_TumSpek,firmaid,subeid) values (@FrmPrId,@Donem, @SgkNo, @Ad, @Soyad, @Gun, @KanunNo, @Mahiyet, @ThkAdet, @A_Ucret, @B_Ikramiye, @AB_Toplam, @C_TumSpek,@firmaid,@subeid)", baglan);


                ekle.Parameters.AddWithValue("@FrmPrId", FrmPrId);
                //ekle.Parameters.AddWithValue("@PersId", 0);
                ekle.Parameters.AddWithValue("@Donem", donem);
                ekle.Parameters.AddWithValue("@SgkNo", TcNo);
                ekle.Parameters.AddWithValue("@Ad", Ad);
                ekle.Parameters.AddWithValue("@Soyad", Soyad);
                ekle.Parameters.AddWithValue("@Gun", Gun);
                ekle.Parameters.AddWithValue("@KanunNo", Kanun);
                ekle.Parameters.AddWithValue("@Mahiyet", Mahiyet);
                ekle.Parameters.AddWithValue("@ThkAdet", ThkAdet);
                ekle.Parameters.AddWithValue("@A_Ucret", Ucret);
                ekle.Parameters.AddWithValue("@B_Ikramiye", ikramiye);
                ekle.Parameters.AddWithValue("@AB_Toplam", ToplamSpek);
                ekle.Parameters.AddWithValue("@C_TumSpek", TumSpek);
                ekle.Parameters.AddWithValue("@firmaid", firmaid);
                ekle.Parameters.AddWithValue("@subeid", subeid);


                ekle.ExecuteNonQuery();
                baglan.Close();
            }
        }


    }
}





//dataGridView1.ColumnCount = 3;
//dataGridView1.ColumnHeadersVisible = true;

//dataGridView1.Columns[0].HeaderText = "Öğrenci Adı";
//dataGridView1.Columns[1].HeaderText = "Soyad";
//dataGridView1.Columns[2].HeaderText = "Adres";

//DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
//combo.Items.AddRange("10A", "And11A", "11C");
//combo.HeaderText = "Sınıf";
//dataGridView1.Columns.Add(combo);
//int sayi = dataGridView1.Columns.Count - 1;
//dataGridView1.Columns[sayi].DisplayIndex = 2;