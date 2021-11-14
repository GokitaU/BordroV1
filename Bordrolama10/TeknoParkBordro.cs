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
        private void yuklubordro()
        {

            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("select FirmaPersId, TcNo,PersAdı,PersSoyadı,GirisTarihi,CikisTarihi,Net_Brüt,PrimGunu,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,DamgaVrg,BesKesintisi,SairKesintiler,AylikNetUcret,KanunNo from FirmaBordro WHERE FirmaNo = '" + firmaid + "' and SubeNo='" + subeid + "' and PuantajDonem =  '" + donem + "'", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(Bordro);
            }
            baglan.Close();
        }

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
            decimal gv = bordroHesaplama.GvHesapla(vergidilimleri.Where(x => x.yil == "2015").FirstOrDefault(), 20000, 650000);
        }
        private void hizmetListesiDoldur()
        {

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
            dtgrtHizmet.Columns.Add("SpekToplami", "SpekToplami");
            dtgrtHizmet.Columns.Add("ThkAdet", "ThkAdet");
            dtgrtHizmet.Columns.Add("hBrdTopl", "hBrdTopl");
            dtgrtHizmet.Columns.Add("hBrdSpek", "hBrdSpek");
            dtgrtHizmet.Columns.Add("hBrdIsciPy", "hBrdIsciPy");
            dtgrtHizmet.Columns.Add("hBrdVergi", "hBrdVergi");
            dtgrtHizmet.Columns.Add("hBrdKes", "hBrdKes");
            dtgrtHizmet.Columns.Add("hBrdNet", "hBrdNet");
            dtgrtHizmet.Columns.Add("BrdNet", "BrdNet");
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                donem = listBox1.GetItemText(listBox1.SelectedItem);
            }
            hizmetListesiDoldur();
            tahakkukBilgisiListBox();

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
                        bKesintiler = Convert.ToDecimal(item["BesKesintisi"]) + Convert.ToDecimal(item["SairKesintiler"]);
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

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
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


            APHBSayfaToplamlari();



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
            lblTplCalisan.Text = (dtgrtAPHB.Rows.Count - 1).ToString();
            lblTplGun.Text = gunToplami.ToString();
            lblTplSpek.Text = spekToplami.ToString("N2");
            lblToplIkramiye.Text = ikramiyeToplami.ToString("N2");
            
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


    }
}
