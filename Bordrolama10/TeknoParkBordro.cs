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


        DataTable HizmetListesi = new DataTable();
        DataTable Bordro = new DataTable();

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
            decimal gv = bordroHesaplama.GvHesapla(vergidilimleri.Where(x => x.yil == "2015").FirstOrDefault(),20000,650000);
        }
        private void hizmetListesiDoldur()
        {

            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Hayır")
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
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT Donem,SgkNo,ad,soyad,IlkSoyad,Gun,Kanun_No,Mahiyet,Ucret,Ikramiye,(ucret + Ikramiye) as Brt_Spek From HizmetListesi where firmaid='" + firmaid + "' and subeid = '" + subeid + "' and Donem = '" + donem + "' AND Kanun_No like '%" + islemKanunu + "%' and ( " + asil + " or " + ek + " or " + iptal + ") order by sgkno", baglan);

            DataTable hzmtlistesi = new DataTable();
            
            
            da.Fill(hzmtlistesi);
            //hzmtlistesi.Columns.Add(new DataColumn("Degisti", typeof(bool)));
            dtgrtHizmet.DataSource = hzmtlistesi;
            
            baglan.Close();
            dtgrtHizmet.Columns["ucret"].DefaultCellStyle.Format = "N2";
            dtgrtHizmet.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";

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

            string islemKanunu = "%";

            if (cmbAyrıBordro.Text == "Hayır")
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
            SQLiteDataAdapter asilAphb = new SQLiteDataAdapter("SELECT count(personelid) as calisan, sum(gun) as gun, sum(ucret) as spek fROM hizmetlistesi where firmaid ='" + firmaid + "'  " + subefiltre + " and  Donem = '" + donem + "' AND Kanun_No like '%" + islemKanunu + "%' and ( " + asil + " or " + ek + " or " + iptal + ")", baglan);
            DataTable table = new DataTable();
            asilAphb.Fill(table);

            string basil = "bmahiyet = '%'";
            string bek = "bmahiyet = '%'";
            string biptal = "bmahiyet = '%'";

            if (checkedListBox2.GetItemCheckState(0) == CheckState.Checked)
            {
                basil = "bmahiyet = 'ASIL'";
            }
            if (checkedListBox2.GetItemCheckState(1) == CheckState.Checked)
            {
                bek = "bmahiyet = 'EK'";
            }
            if (checkedListBox2.GetItemCheckState(2) == CheckState.Checked)
            {
                biptal = "bmahiyet = 'IPTAL'";
            }

            SQLiteDataAdapter asilThkkuk = new SQLiteDataAdapter("SELECT bcalisan as calisan,bgun as gun,spek FROM ilktahakkukbilgi where firmaid  ='" + firmaid + "'  " + subefiltre + " and thkkukdonem = '" + donem + "' and  bkanun like '%" + islemKanunu + "%' and ( " + basil + " or " + bek + " or " + biptal + ")", baglan);

            DataTable table1 = new DataTable();
            asilThkkuk.Fill(table1);


            baglan.Close();

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

            dtgrtOzet.DataSource = ozetList;




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
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                donem = listBox1.GetItemText(listBox1.SelectedItem);
            }
            hizmetListesiDoldur();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BordroHesapla();
        }
    }
}
