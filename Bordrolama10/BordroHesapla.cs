using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraPrinting;

namespace Bordrolama10
{
    public partial class BordroHesapla : Form
    {
        public BordroHesapla()
        {
            InitializeComponent();
        }

        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        static int firmaid;
        static string donem;
        static int subeid;
        static string persid;


        public void subelistele()
        {
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT sb.subeid as Id, sb.subeunvan as Sube_Unvan, count(fb.KanunNo) as tesvikli,round(sum(fb.TerkinGv),2) as TrkGv,round(sum(fb.TerkinDv),2) as TrkDv  From FirmaBordro fb INNER JOIN sube_bilgileri as sb on sb.subeid = fb.subeno where sb.firmaid = " + firmaid + " and PuantajDonem between '" + cmbilk.Text + "' and '" + cmbson.Text + "'   GROUP by sb.subeid", baglan);
            DataTable dt = new DataTable();

            da.Fill(dt);
            baglan.Close();
            dtgrtSubeSecim.DataSource = dt;
        }

        public void gvTesvikDonemBazli()
        {
            string subefiltre = "";

            if (subeid != 0)
            {
                subefiltre = "and SubeNo = '" + subeid + "'".ToString();
            }
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT PuantajDonem as Donem, count(BdrId) as Çalışan, count(KanunNo) as tesvikli,round(sum(TerkinGv), 2) as TrkGv,round(sum(TerkinDv), 2) as TrkDv  from firmaBordro WHERE FirmaNo = '" + firmaid + "' " + subefiltre + "and PuantajDonem between '" + cmbilk.Text + "' and '" + cmbson.Text + "' GROUP by PuantajDonem", baglan);
            DataTable table = new DataTable();
            da.Fill(table);
            baglan.Close();
            dtgrtBrdDonem.DataSource = table;
        }
        public void gvTesvikBordro()
        {
            string subefiltre = "";
            if (subeid != 0)
            {
                subefiltre = "and SubeNo = '" + subeid + "'".ToString();
            }


            string personelid = "";
            if (persid != null)
            {
                personelid = " and firmaPersid = '" + persid + "'".ToString();
            }
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT FirmaPersId as Id, PuantajDonem as Donem,PersAdı,PersSoyadı,GirisTarihi,CikisTarihi,PrimGunu,AylikBrutUcret as Brut, ToplamKazanc as KazançTop,SgkMatrahi,SGkIsciPrim as isciPrm,IszlikIsciPrim as iszIsci,KumVergMatr as Kum_Matr,GvMatrahi, GelirVergisi, Agi, VergiInd,DamgaVrg,KanunNo, round(TerkinGv,2) as TERKINgv, Round(TerkinDv,2) as TERKİNdv, SgkIsverenPrim as SgkIsv,IssizlikIsvPrim as iszIsv,AylikNetUcret  From FirmaBordro where FirmaNo = '" + firmaid + "'" + subefiltre + " and PuantajDonem='" + donem + "'" + personelid + "", baglan);
            DataTable table = new DataTable();
            da.Fill(table);
            baglan.Close();
            dtgrtFrmBordro.DataSource = table;
        }
        public void gvTesvikiPers()
        {
            string subefiltre = "";

            if (subeid != 0)
            {
                subefiltre = "and SubeNo = '" + subeid + "'".ToString();
            }

            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT FirmaPersId as Id, PuantajDonem as Donem, KanunNo, PersAdı, PersSoyadı, PrimGunu, GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, round(TerkinGv, 2) as TERKINgv, Round(TerkinDv, 2) as TERKİNdv From FirmaBordro where FirmaNo = '" + firmaid + "'" + subefiltre + "  and PuantajDonem = '" + donem + "' and (KanunNo = '00687' or KanunNo = '01687' or KanunNo = '17103' or KanunNo = '27103')", baglan);
            DataTable table = new DataTable();
            da.Fill(table);
            baglan.Close();
            dtgrtBrdTesvikliPers.DataSource = table;
        }
        public void hizmetListesi()
        {
            string subefiltre = "";

            if (subeid != 0)
            {
                subefiltre = " and subeid = " + subeid + "".ToString();
            }

            string personelid = "";
            if (persid != null)
            {
                personelid = " and firmPersid = '" + persid + "'".ToString();
            }

            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT firmPersid as Id, Donem, SgkNo, Ad,Soyad,Ucret,Ikramiye,Gun,Eksik_Gun,GGun,CGun,Kanun_No,Mahiyet From HizmetListesi WHERE firmaid=" + firmaid + "" + subefiltre + " " + personelid + " and Donem='" + donem + "'", baglan);
            DataTable table = new DataTable();
            da.Fill(table);
            baglan.Close();
            dtgrtHzmtListe.DataSource = table;
        }


        private void BordroHesapla_Load(object sender, EventArgs e)
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

            cmbilk.Text = "2017/02";
            cmbson.Text = "2020/12";
            txtdosyayolu.Text = Application.StartupPath + "\\GvTesvikBordro";
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
            gvTesvikDonemBazli();
            donem = dtgrtBrdDonem.Rows[0].Cells[0].Value.ToString();
            persid = null;
            gvTesvikiPers();
            hizmetListesi();
            gvTesvikBordro();

            grid1duduzenle_DonemBazli();
            grid5duzenle_tesvikliPersonel();
            grid4duduzenle_SubeBazli();
            grid3duduzenle_HizmetListesi();
            grid2duduzenle_firmaBordro();

        }

        private void dataGridView4_Click(object sender, EventArgs e)
        {
            int secim = dtgrtSubeSecim.SelectedCells[0].RowIndex;
            subeid = dtgrtSubeSecim.Rows[secim].Cells[0].Value != DBNull.Value ? Convert.ToInt32(dtgrtSubeSecim.Rows[secim].Cells[0].Value) : 0;
            programreferans.subeunvan = dtgrtSubeSecim.Rows[secim].Cells[1].Value != DBNull.Value ? dtgrtSubeSecim.Rows[secim].Cells[1].Value.ToString() : "";
            programreferans.subid = subeid;

            gvTesvikDonemBazli();
            donem = dtgrtBrdDonem.Rows[0].Cells[0].Value.ToString();
            persid = null;
            gvTesvikiPers();
            hizmetListesi();
            gvTesvikBordro();

            grid3duduzenle_HizmetListesi();
            grid1duduzenle_DonemBazli();
            grid2duduzenle_firmaBordro();
            grid5duzenle_tesvikliPersonel();

        }

        private void grid5duzenle_tesvikliPersonel()
        {
            dtgrtBrdTesvikliPers.Columns["GvMatrahi"].DefaultCellStyle.Format = "N2";
            dtgrtBrdTesvikliPers.Columns["GelirVergisi"].DefaultCellStyle.Format = "N2";
            dtgrtBrdTesvikliPers.Columns["Agi"].DefaultCellStyle.Format = "N2";
            dtgrtBrdTesvikliPers.Columns["VergiInd"].DefaultCellStyle.Format = "N2";
            dtgrtBrdTesvikliPers.Columns["DamgaVrg"].DefaultCellStyle.Format = "N2";
            dtgrtBrdTesvikliPers.Columns["TERKINgv"].DefaultCellStyle.Format = "N2";
            dtgrtBrdTesvikliPers.Columns["TERKİNdv"].DefaultCellStyle.Format = "N2";
            dtgrtBrdTesvikliPers.Columns["Id"].Visible = false;
        }
        private void grid2duduzenle_firmaBordro()
        {
            dtgrtFrmBordro.Columns["Id"].Visible = false;
            dtgrtFrmBordro.Columns["Brut"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["KazançTop"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["SgkMatrahi"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["isciPrm"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["iszIsci"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["GvMatrahi"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["GelirVergisi"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["Agi"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["VergiInd"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["DamgaVrg"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["TERKINgv"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["TERKİNdv"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["SgkIsv"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["iszIsv"].DefaultCellStyle.Format = "N2";
            dtgrtFrmBordro.Columns["AylikNetUcret"].DefaultCellStyle.Format = "N2";

        }
        private void grid3duduzenle_HizmetListesi()
        {

            dtgrtHzmtListe.Columns["Ucret"].DefaultCellStyle.Format = "N2";
            dtgrtHzmtListe.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";
            dtgrtHzmtListe.Columns["Id"].Visible = false;
        }

        private void grid1duduzenle_DonemBazli()
        {

            dtgrtBrdDonem.Columns["TrkGv"].DefaultCellStyle.Format = "N2";
            dtgrtBrdDonem.Columns["TrkDv"].DefaultCellStyle.Format = "N2";
        }
        private void grid4duduzenle_SubeBazli()
        {

            dtgrtSubeSecim.Columns["TrkGv"].DefaultCellStyle.Format = "N2";
            dtgrtSubeSecim.Columns["TrkDv"].DefaultCellStyle.Format = "N2";
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            persid = null;
            int secim = dtgrtBrdDonem.SelectedCells[0].RowIndex;
            donem = dtgrtBrdDonem.Rows[secim].Cells[0].Value.ToString();
            gvTesvikiPers();
            hizmetListesi();
            gvTesvikBordro();

            grid3duduzenle_HizmetListesi();
            grid2duduzenle_firmaBordro();
            grid5duzenle_tesvikliPersonel();
        }

        private void dtgrtBrdTesvikliPers_Click(object sender, EventArgs e)
        {
            int secim = dtgrtBrdTesvikliPers.SelectedCells[0].RowIndex;
            persid = dtgrtBrdTesvikliPers.Rows[secim].Cells[0].Value.ToString();
            hizmetListesi();
            gvTesvikBordro();

            grid3duduzenle_HizmetListesi();
            grid2duduzenle_firmaBordro();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  DataSet ds = new DataSet("DataSource");

            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            if (subeid == 0 || donem == null)
            {
                MessageBox.Show("Lütfen İlgili Şube ve Dönem Seçiniz");
            }
            else
            {
                baglan.Open();
                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid='" + subeid + "'", baglan);
                SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "' and SubeNo='" + subeid + "' and PuantajDonem='" + donem + "' ", baglan);

                baslikda.Fill(baslikTable);
                bordroda.Fill(bordroTable);

                //baslikTable.Columns.Add("PuantajDonem", typeof(string));
                //if (bordroTable.Rows.Count > 0)
                //{
                //    baslikTable.Rows[0]["PuantajDonem"] = bordroTable.Rows[0]["PuantajDonem"];
                //}
                bordroTable.Columns.Add("firmunvantam", typeof(string));
                bordroTable.Columns.Add("subeunvan", typeof(string));
                bordroTable.Columns.Add("vd", typeof(string));
                bordroTable.Columns.Add("vn", typeof(string));
                bordroTable.Columns.Add("ticsiciln", typeof(string));
                bordroTable.Columns.Add("adres", typeof(string));
                bordroTable.Columns.Add("sgkisyerino", typeof(string));


                //bordroTable.Rows[0]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                for (int i = 0; i < bordroTable.Rows.Count; i++)
                {
                    bordroTable.Rows[i]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                    bordroTable.Rows[i]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                    bordroTable.Rows[i]["vd"] = baslikTable.Rows[0]["vd"];
                    bordroTable.Rows[i]["vn"] = baslikTable.Rows[0]["vn"];
                    bordroTable.Rows[i]["ticsiciln"] = baslikTable.Rows[0]["ticsiciln"];
                    bordroTable.Rows[i]["adres"] = baslikTable.Rows[0]["adres"];
                    bordroTable.Rows[i]["sgkisyerino"] = baslikTable.Rows[0]["sgkisyerino"];
                }

                //ds.Tables.Add(baslikTable);
                //ds.Tables.Add(bordroTable);

                baglan.Close();


                GelirVergisiBordro report = new GelirVergisiBordro { DataSource = bordroTable, DataMember = "bordroTable" };

                // report.DataSource = bordroTable;
                //  report.DataMember = "baslikTable";

                //report.DetailReport.DataSource = ds;
                //report.DetailReport.DataMember = "bordroTable";
                //string dnm = donem.Replace('/', '-');
                //report.Name = programreferans.subeunvan + "-" + dnm;
                //string filepathpdf = @txtdosyayolu.Text + "\\" + report.Name + ".pdf";
                ////string filepathpdf = Application.StartupPath + "\\"+ report.Name+".pdf";
                //report.ExportToPdf(filepathpdf);

                RaporGoruntule rpr = new RaporGoruntule();
                rpr.documentViewer1.DocumentSource = report;

                rpr.ShowDialog();
            }
        }

        private void cmbilk_SelectedValueChanged(object sender, EventArgs e)
        {
            subelistele();
            gvTesvikDonemBazli();
            donem = dtgrtBrdDonem.Rows[0].Cells[0].Value != null ? dtgrtBrdDonem.Rows[0].Cells[0].Value.ToString() : "";
            // donem = dtgrtBrdDonem.Rows[0].Cells[0].Value.ToString();
            persid = null;
            gvTesvikiPers();
            hizmetListesi();
            gvTesvikBordro();

            grid1duduzenle_DonemBazli();
            grid5duzenle_tesvikliPersonel();
            grid4duduzenle_SubeBazli();
            grid3duduzenle_HizmetListesi();
            grid2duduzenle_firmaBordro();
        }

        private void cmbson_SelectedValueChanged(object sender, EventArgs e)
        {
            subelistele();
            gvTesvikDonemBazli();
            donem = dtgrtBrdDonem.Rows[0].Cells[0].Value != null ? dtgrtBrdDonem.Rows[0].Cells[0].Value.ToString() : "";
            persid = null;
            gvTesvikiPers();
            hizmetListesi();
            gvTesvikBordro();

            grid1duduzenle_DonemBazli();
            grid5duzenle_tesvikliPersonel();
            grid4duduzenle_SubeBazli();
            grid3duduzenle_HizmetListesi();
            grid2duduzenle_firmaBordro();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // DataSet ds = new DataSet("DataSource");

            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            baglan.Open();
            for (int i = 0; i < dtgrtSubeSecim.Rows.Count - 1; i++)
            {

                subeid = Convert.ToInt32(dtgrtSubeSecim.Rows[i].Cells["Id"].Value);

                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid = '" + subeid + "'", baglan);
                baslikda.Fill(baslikTable);

                for (int j = 0; j < dtgrtBrdDonem.Rows.Count - 1; j++)
                {
                    donem = dtgrtBrdDonem.Rows[j].Cells["Donem"].Value.ToString();
                    if (Convert.ToDecimal(dtgrtBrdDonem.Rows[j].Cells["TrkGv"].Value) > 0 || Convert.ToDecimal(dtgrtBrdDonem.Rows[j].Cells["TrkDv"].Value) > 0)
                    {
                        SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "'and SubeNo='" + subeid + "' and PuantajDonem='" + donem + "'", baglan);
                        bordroda.Fill(bordroTable);

                        bordroTable.Columns.Add("firmunvantam", typeof(string));
                        bordroTable.Columns.Add("subeunvan", typeof(string));
                        bordroTable.Columns.Add("vd", typeof(string));
                        bordroTable.Columns.Add("vn", typeof(string));
                        bordroTable.Columns.Add("ticsiciln", typeof(string));
                        bordroTable.Columns.Add("adres", typeof(string));
                        bordroTable.Columns.Add("sgkisyerino", typeof(string));


                        //bordroTable.Rows[0]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                        for (int y = 0; y < bordroTable.Rows.Count; y++)
                        {
                            bordroTable.Rows[y]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                            bordroTable.Rows[y]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                            bordroTable.Rows[y]["vd"] = baslikTable.Rows[0]["vd"];
                            bordroTable.Rows[y]["vn"] = baslikTable.Rows[0]["vn"];
                            bordroTable.Rows[y]["ticsiciln"] = baslikTable.Rows[0]["ticsiciln"];
                            bordroTable.Rows[y]["adres"] = baslikTable.Rows[0]["adres"];
                            bordroTable.Rows[y]["sgkisyerino"] = baslikTable.Rows[0]["sgkisyerino"];
                        }


                        GelirVergisiBordro report = new GelirVergisiBordro { DataSource = bordroTable, DataMember = "bordroTable" };
                        string dnm = donem.Replace('/', '-');
                        report.Name = programreferans.subeunvan + "-" + dnm;
                        string filepathpdf = @txtdosyayolu.Text + "\\" + report.Name + ".pdf";
                        report.ExportToPdf(filepathpdf);

                        bordroTable.Rows.Clear();
                        bordroTable.Columns.Clear();


                    }
                }

            }
            baglan.Close();
        }


        private void btnDosyaYolu_Click(object sender, EventArgs e)
        {


            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Dosyayı Nereye Kaydetmek İstersiniz?";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = @"C:\Program Files";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtdosyayolu.Text = dialog.SelectedPath;
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //  DataSet ds = new DataSet("DataSource");

            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            if (subeid == 0 || donem == null)
            {
                MessageBox.Show("Lütfen İlgili Şube ve Dönem Seçiniz");
            }
            else
            {
                baglan.Open();
                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid='" + subeid + "'", baglan);
                SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "' and SubeNo='" + subeid + "' and PuantajDonem='" + donem + "' ", baglan);

                baslikda.Fill(baslikTable);
                bordroda.Fill(bordroTable);

                bordroTable.Columns.Add("firmunvantam", typeof(string));
                bordroTable.Columns.Add("subeunvan", typeof(string));
                bordroTable.Columns.Add("vd", typeof(string));
                bordroTable.Columns.Add("vn", typeof(string));
                bordroTable.Columns.Add("ticsiciln", typeof(string));
                bordroTable.Columns.Add("adres", typeof(string));
                bordroTable.Columns.Add("sgkisyerino", typeof(string));


                //bordroTable.Rows[0]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                for (int i = 0; i < bordroTable.Rows.Count; i++)
                {
                    bordroTable.Rows[i]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                    bordroTable.Rows[i]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                    bordroTable.Rows[i]["vd"] = baslikTable.Rows[0]["vd"];
                    bordroTable.Rows[i]["vn"] = baslikTable.Rows[0]["vn"];
                    bordroTable.Rows[i]["ticsiciln"] = baslikTable.Rows[0]["ticsiciln"];
                    bordroTable.Rows[i]["adres"] = baslikTable.Rows[0]["adres"];
                    bordroTable.Rows[i]["sgkisyerino"] = baslikTable.Rows[0]["sgkisyerino"];
                }

                //ds.Tables.Add(baslikTable);
                //ds.Tables.Add(bordroTable);

                baglan.Close();


                GelirVergisiBordro report = new GelirVergisiBordro { DataSource = bordroTable, DataMember = "bordroTable" };

                // report.DataSource = bordroTable;
                //  report.DataMember = "baslikTable";

                string dnm = donem.Replace('/', '-');
                report.Name = programreferans.subeunvan + "-" + dnm + " Dönem Bordrosu";
                string filepathpdf = @txtdosyayolu.Text + "\\" + report.Name + ".pdf";
                report.ExportToPdf(filepathpdf);


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            if (subeid == 0 || donem == null)
            {
                MessageBox.Show("Lütfen İlgili Şube ve Dönem Seçiniz");
            }
            else
            {
                baglan.Open();
                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid='" + subeid + "'", baglan);
                SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "' and SubeNo='" + subeid + "' and PuantajDonem='" + donem + "' and (TerkinGv >0 or TerkinDv>0)  ", baglan);

                baslikda.Fill(baslikTable);
                bordroda.Fill(bordroTable);

                bordroTable.Columns.Add("firmunvantam", typeof(string));
                bordroTable.Columns.Add("subeunvan", typeof(string));
                bordroTable.Columns.Add("vd", typeof(string));
                bordroTable.Columns.Add("vn", typeof(string));
                bordroTable.Columns.Add("il", typeof(string));
                bordroTable.Columns.Add("sgkisyerino", typeof(string));
                bordroTable.Columns.Add("Yeni_Kod", typeof(string));
                bordroTable.Columns.Add("Eski_Kod", typeof(string));
                bordroTable.Columns.Add("isyeri_Kod", typeof(string));
                bordroTable.Columns.Add("Araci_Kod", typeof(string));
                bordroTable.Columns.Add("isyeriSubeKodu", typeof(string));

                string isyerisgkno;
                for (int i = 0; i < bordroTable.Rows.Count; i++)
                {
                    bordroTable.Rows[i]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                    bordroTable.Rows[i]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                    bordroTable.Rows[i]["vd"] = baslikTable.Rows[0]["vd"];
                    bordroTable.Rows[i]["vn"] = baslikTable.Rows[0]["vn"];
                    bordroTable.Rows[i]["il"] = baslikTable.Rows[0]["il"];
                    isyerisgkno = baslikTable.Rows[0]["sgkisyerino"].ToString();
                    bordroTable.Rows[i]["sgkisyerino"] = isyerisgkno;
                    bordroTable.Rows[i]["Yeni_Kod"] = isyerisgkno.Substring(2, 2);
                    bordroTable.Rows[i]["Eski_Kod"] = isyerisgkno.Substring(4, 2);
                    bordroTable.Rows[i]["isyeri_Kod"] = isyerisgkno.Substring(13, 7);
                    bordroTable.Rows[i]["Araci_Kod"] = isyerisgkno.Substring(31, 3);
                    bordroTable.Rows[i]["isyeriSubeKodu"] = baslikTable.Rows[0]["isyeriSubeKodu"];
                }


                baglan.Close();



                IstihdaminTesvikiListe report = new IstihdaminTesvikiListe { DataSource = bordroTable, DataMember = "bordroTable" };
                RaporGoruntule rpr = new RaporGoruntule();
                rpr.documentViewer1.DocumentSource = report;

                rpr.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            if (subeid == 0 || donem == null)
            {
                MessageBox.Show("Lütfen İlgili Şube ve Dönem Seçiniz");
            }
            else
            {
                baglan.Open();
                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid='" + subeid + "'", baglan);
                SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "' and SubeNo='" + subeid + "' and PuantajDonem='" + donem + "' and (TerkinGv >0 or TerkinDv>0)  ", baglan);

                baslikda.Fill(baslikTable);
                bordroda.Fill(bordroTable);

                bordroTable.Columns.Add("firmunvantam", typeof(string));
                bordroTable.Columns.Add("subeunvan", typeof(string));
                bordroTable.Columns.Add("vd", typeof(string));
                bordroTable.Columns.Add("vn", typeof(string));
                bordroTable.Columns.Add("il", typeof(string));
                bordroTable.Columns.Add("sgkisyerino", typeof(string));
                bordroTable.Columns.Add("Yeni_Kod", typeof(string));
                bordroTable.Columns.Add("Eski_Kod", typeof(string));
                bordroTable.Columns.Add("isyeri_Kod", typeof(string));
                bordroTable.Columns.Add("Araci_Kod", typeof(string));
                bordroTable.Columns.Add("isyeriSubeKodu", typeof(string));

                string isyerisgkno;
                for (int i = 0; i < bordroTable.Rows.Count; i++)
                {
                    bordroTable.Rows[i]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                    bordroTable.Rows[i]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                    bordroTable.Rows[i]["vd"] = baslikTable.Rows[0]["vd"];
                    bordroTable.Rows[i]["vn"] = baslikTable.Rows[0]["vn"];
                    bordroTable.Rows[i]["il"] = baslikTable.Rows[0]["il"];
                    isyerisgkno = baslikTable.Rows[0]["sgkisyerino"].ToString();
                    bordroTable.Rows[i]["sgkisyerino"] = isyerisgkno;
                    bordroTable.Rows[i]["Yeni_Kod"] = isyerisgkno.Substring(2, 2);
                    bordroTable.Rows[i]["Eski_Kod"] = isyerisgkno.Substring(4, 2);
                    bordroTable.Rows[i]["isyeri_Kod"] = isyerisgkno.Substring(13, 7);
                    bordroTable.Rows[i]["Araci_Kod"] = isyerisgkno.Substring(31, 3);
                    bordroTable.Rows[i]["isyeriSubeKodu"] = baslikTable.Rows[0]["isyeriSubeKodu"];
                }


                baglan.Close();



                IstihdaminTesvikiListe report = new IstihdaminTesvikiListe { DataSource = bordroTable, DataMember = "bordroTable" };

                string dnm = donem.Replace('/', '-');
                report.Name = programreferans.subeunvan + "-" + dnm + "İstihdamın Teşviki Listesi";
                string filepathpdf = @txtdosyayolu.Text + "\\" + report.Name + ".pdf";
                report.ExportToPdf(filepathpdf);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            if (subeid == 0 || donem == null)
            {
                MessageBox.Show("Lütfen İlgili Şube ve Dönem Seçiniz");
            }
            else
            {
                baglan.Open();
                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid='" + subeid + "'", baglan);
                SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "' and SubeNo='" + subeid + "' and (TerkinGv >0 or TerkinDv>0)  ", baglan);

                baslikda.Fill(baslikTable);
                bordroda.Fill(bordroTable);

                bordroTable.Columns.Add("firmunvantam", typeof(string));
                bordroTable.Columns.Add("subeunvan", typeof(string));
                bordroTable.Columns.Add("vd", typeof(string));
                bordroTable.Columns.Add("vn", typeof(string));
                bordroTable.Columns.Add("il", typeof(string));
                bordroTable.Columns.Add("sgkisyerino", typeof(string));
                bordroTable.Columns.Add("Yeni_Kod", typeof(string));
                bordroTable.Columns.Add("Eski_Kod", typeof(string));
                bordroTable.Columns.Add("isyeri_Kod", typeof(string));
                bordroTable.Columns.Add("Araci_Kod", typeof(string));
                bordroTable.Columns.Add("isyeriSubeKodu", typeof(string));

                string isyerisgkno;
                for (int i = 0; i < bordroTable.Rows.Count; i++)
                {
                    bordroTable.Rows[i]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                    bordroTable.Rows[i]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                    bordroTable.Rows[i]["vd"] = baslikTable.Rows[0]["vd"];
                    bordroTable.Rows[i]["vn"] = baslikTable.Rows[0]["vn"];
                    bordroTable.Rows[i]["il"] = baslikTable.Rows[0]["il"];
                    isyerisgkno = baslikTable.Rows[0]["sgkisyerino"].ToString();
                    bordroTable.Rows[i]["sgkisyerino"] = isyerisgkno;
                    bordroTable.Rows[i]["Yeni_Kod"] = isyerisgkno.Substring(2, 2);
                    bordroTable.Rows[i]["Eski_Kod"] = isyerisgkno.Substring(4, 2);
                    bordroTable.Rows[i]["isyeri_Kod"] = isyerisgkno.Substring(13, 7);
                    bordroTable.Rows[i]["Araci_Kod"] = isyerisgkno.Substring(31, 3);
                    bordroTable.Rows[i]["isyeriSubeKodu"] = baslikTable.Rows[0]["isyeriSubeKodu"];
                }


                baglan.Close();



                IstihdaminTesvikiListe report = new IstihdaminTesvikiListe { DataSource = bordroTable, DataMember = "bordroTable" };


                report.Name = programreferans.subeunvan + "-" + "İstihdamın Teşviki Listesi";
                string filepathpdf = @txtdosyayolu.Text + "\\" + report.Name + ".pdf";
                report.ExportToPdf(filepathpdf);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            if (subeid == 0 || donem == null)
            {
                MessageBox.Show("Lütfen İlgili Şube ve Dönem Seçiniz");
            }
            else
            {
                baglan.Open();
                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid='" + subeid + "'", baglan);
                SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "' and SubeNo='" + subeid + "' and PuantajDonem='" + donem + "' and (TerkinGv >0 or TerkinDv>0)  ", baglan);

                baslikda.Fill(baslikTable);
                bordroda.Fill(bordroTable);

                bordroTable.Columns.Add("firmunvantam", typeof(string));
                bordroTable.Columns.Add("subeunvan", typeof(string));
                bordroTable.Columns.Add("vd", typeof(string));
                bordroTable.Columns.Add("vn", typeof(string));
                bordroTable.Columns.Add("il", typeof(string));
                bordroTable.Columns.Add("sgkisyerino", typeof(string));
                bordroTable.Columns.Add("Yeni_Kod", typeof(string));
                bordroTable.Columns.Add("Eski_Kod", typeof(string));
                bordroTable.Columns.Add("isyeri_Kod", typeof(string));
                bordroTable.Columns.Add("Araci_Kod", typeof(string));
                bordroTable.Columns.Add("isyeriSubeKodu", typeof(string));

                string isyerisgkno;
                for (int i = 0; i < bordroTable.Rows.Count; i++)
                {
                    bordroTable.Rows[i]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                    bordroTable.Rows[i]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                    bordroTable.Rows[i]["vd"] = baslikTable.Rows[0]["vd"];
                    bordroTable.Rows[i]["vn"] = baslikTable.Rows[0]["vn"];
                    bordroTable.Rows[i]["il"] = baslikTable.Rows[0]["il"];
                    isyerisgkno = baslikTable.Rows[0]["sgkisyerino"].ToString();
                    bordroTable.Rows[i]["sgkisyerino"] = isyerisgkno;
                    bordroTable.Rows[i]["Yeni_Kod"] = isyerisgkno.Substring(2, 2);
                    bordroTable.Rows[i]["Eski_Kod"] = isyerisgkno.Substring(4, 2);
                    bordroTable.Rows[i]["isyeri_Kod"] = isyerisgkno.Substring(13, 7);
                    bordroTable.Rows[i]["Araci_Kod"] = isyerisgkno.Substring(31, 3);
                    bordroTable.Rows[i]["isyeriSubeKodu"] = baslikTable.Rows[0]["isyeriSubeKodu"];
                }


                baglan.Close();



                IstTesvTxt report = new IstTesvTxt { DataSource = bordroTable, DataMember = "bordroTable" };
                string dnm = donem.Replace('/', '-');
                report.Name = programreferans.subeunvan + "-" + dnm + "İstihdamın Teşviki Listesi";
                string fileTxt = @txtdosyayolu.Text + "\\" + report.Name + ".Txt";
                report.ExportToText(fileTxt);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            if (subeid == 0 || donem == null)
            {
                MessageBox.Show("Lütfen İlgili Şube ve Dönem Seçiniz");
            }
            else
            {
                baglan.Open();
                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid='" + subeid + "'", baglan);
                SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "' and SubeNo='" + subeid + "'  and (TerkinGv >0 or TerkinDv>0)  ", baglan);

                baslikda.Fill(baslikTable);
                bordroda.Fill(bordroTable);

                bordroTable.Columns.Add("firmunvantam", typeof(string));
                bordroTable.Columns.Add("subeunvan", typeof(string));
                bordroTable.Columns.Add("vd", typeof(string));
                bordroTable.Columns.Add("vn", typeof(string));
                bordroTable.Columns.Add("il", typeof(string));
                bordroTable.Columns.Add("sgkisyerino", typeof(string));
                bordroTable.Columns.Add("Yeni_Kod", typeof(string));
                bordroTable.Columns.Add("Eski_Kod", typeof(string));
                bordroTable.Columns.Add("isyeri_Kod", typeof(string));
                bordroTable.Columns.Add("Araci_Kod", typeof(string));
                bordroTable.Columns.Add("isyeriSubeKodu", typeof(string));

                string isyerisgkno;
                for (int i = 0; i < bordroTable.Rows.Count; i++)
                {
                    bordroTable.Rows[i]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                    bordroTable.Rows[i]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                    bordroTable.Rows[i]["vd"] = baslikTable.Rows[0]["vd"];
                    bordroTable.Rows[i]["vn"] = baslikTable.Rows[0]["vn"];
                    bordroTable.Rows[i]["il"] = baslikTable.Rows[0]["il"];
                    isyerisgkno = baslikTable.Rows[0]["sgkisyerino"].ToString();
                    bordroTable.Rows[i]["sgkisyerino"] = isyerisgkno;
                    bordroTable.Rows[i]["Yeni_Kod"] = isyerisgkno.Substring(2, 2);
                    bordroTable.Rows[i]["Eski_Kod"] = isyerisgkno.Substring(4, 2);
                    bordroTable.Rows[i]["isyeri_Kod"] = isyerisgkno.Substring(13, 7);
                    bordroTable.Rows[i]["Araci_Kod"] = isyerisgkno.Substring(31, 3);
                    bordroTable.Rows[i]["isyeriSubeKodu"] = baslikTable.Rows[0]["isyeriSubeKodu"];
                }


                baglan.Close();



                IstTesvTxt report = new IstTesvTxt { DataSource = bordroTable, DataMember = "bordroTable" };
                string dnm = donem.Replace('/', '-');
                report.Name = programreferans.subeunvan + "-" + "İstihdamın Teşviki Listesi";
                string fileTxt = @txtdosyayolu.Text + "\\" + report.Name + ".Txt";
                report.ExportToText(fileTxt);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

            button2_Click(sender, e);
            button6_Click(sender, e);
            button8_Click(sender, e);
        }

        private void btnOtoBdrIstTsvDonemlik_Click(object sender, EventArgs e)
        {
            // DataSet ds = new DataSet("DataSource");

            DataTable baslikTable = new DataTable("baslikTable");
            DataTable bordroTable = new DataTable("bordroTable");

            baglan.Open();
            for (int i = 0; i < dtgrtSubeSecim.Rows.Count - 1; i++)
            {

                subeid = Convert.ToInt32(dtgrtSubeSecim.Rows[i].Cells["Id"].Value);

                SQLiteDataAdapter baslikda = new SQLiteDataAdapter("SELECT * From sube_bilgileri where firmaid ='" + firmaid + "' and subeid = '" + subeid + "'", baglan);
                baslikda.Fill(baslikTable);

                for (int j = 0; j < dtgrtBrdDonem.Rows.Count - 1; j++)
                {
                    donem = dtgrtBrdDonem.Rows[j].Cells["Donem"].Value.ToString();
                    if (Convert.ToDecimal(dtgrtBrdDonem.Rows[j].Cells["TrkGv"].Value) > 0 || Convert.ToDecimal(dtgrtBrdDonem.Rows[j].Cells["TrkDv"].Value) > 0)
                    {
                        SQLiteDataAdapter bordroda = new SQLiteDataAdapter("SELECT * From FirmaBordro where FirmaNo='" + firmaid + "'and SubeNo='" + subeid + "' and PuantajDonem='" + donem + "'", baglan);
                        bordroda.Fill(bordroTable);

                        bordroTable.Columns.Add("firmunvantam", typeof(string));
                        bordroTable.Columns.Add("subeunvan", typeof(string));
                        bordroTable.Columns.Add("vd", typeof(string));
                        bordroTable.Columns.Add("vn", typeof(string));
                        bordroTable.Columns.Add("ticsiciln", typeof(string));
                        bordroTable.Columns.Add("adres", typeof(string));
                        bordroTable.Columns.Add("sgkisyerino", typeof(string));
                        bordroTable.Columns.Add("il", typeof(string));
                        bordroTable.Columns.Add("Yeni_Kod", typeof(string));
                        bordroTable.Columns.Add("Eski_Kod", typeof(string));
                        bordroTable.Columns.Add("isyeri_Kod", typeof(string));
                        bordroTable.Columns.Add("Araci_Kod", typeof(string));
                        bordroTable.Columns.Add("isyeriSubeKodu", typeof(string));


                        string isyerisgkno;
                        //bordroTable.Rows[0]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                        for (int y = 0; y < bordroTable.Rows.Count; y++)
                        {
                            bordroTable.Rows[y]["firmunvantam"] = baslikTable.Rows[0]["firmunvantam"];
                            bordroTable.Rows[y]["subeunvan"] = baslikTable.Rows[0]["subeunvan"];
                            bordroTable.Rows[y]["vd"] = baslikTable.Rows[0]["vd"];
                            bordroTable.Rows[y]["vn"] = baslikTable.Rows[0]["vn"];
                            bordroTable.Rows[y]["ticsiciln"] = baslikTable.Rows[0]["ticsiciln"];
                            bordroTable.Rows[y]["adres"] = baslikTable.Rows[0]["adres"];
                            bordroTable.Rows[y]["il"] = baslikTable.Rows[0]["il"];
                            isyerisgkno = baslikTable.Rows[0]["sgkisyerino"].ToString();
                            bordroTable.Rows[y]["sgkisyerino"] = isyerisgkno;
                            bordroTable.Rows[y]["Yeni_Kod"] = isyerisgkno.Substring(2, 2);
                            bordroTable.Rows[y]["Eski_Kod"] = isyerisgkno.Substring(4, 2);
                            bordroTable.Rows[y]["isyeri_Kod"] = isyerisgkno.Substring(13, 7);
                            bordroTable.Rows[y]["Araci_Kod"] = isyerisgkno.Substring(31, 3);
                            bordroTable.Rows[y]["isyeriSubeKodu"] = baslikTable.Rows[0]["isyeriSubeKodu"];


                        }

                        string dnm = donem.Replace('/', '-');

                        GelirVergisiBordro Brdr = new GelirVergisiBordro { DataSource = bordroTable, DataMember = "bordroTable" };

                        Brdr.Name = programreferans.subeunvan + "-" + dnm + " Dönem Bordrosu";
                        string bdrpdf = @txtdosyayolu.Text + "\\" + Brdr.Name + ".pdf";
                        Brdr.ExportToPdf(bdrpdf);


                        IstihdaminTesvikiListe isthListe = new IstihdaminTesvikiListe { DataSource = bordroTable, DataMember = "bordroTable" };


                        isthListe.Name = programreferans.subeunvan + "-" + dnm + " İstihdamın Teşviki Listesi";
                        string isthListePdf = @txtdosyayolu.Text + "\\" + isthListe.Name + ".pdf";
                        isthListe.ExportToPdf(isthListePdf);


                        IstTesvTxt ishtTxt = new IstTesvTxt { DataSource = bordroTable, DataMember = "bordroTable" };

                        ishtTxt.Name = programreferans.subeunvan + "-" + dnm + " İstihdamın Teşviki Listesi";
                        string isthListeTXT = @txtdosyayolu.Text + "\\" + ishtTxt.Name + ".Txt";
                        ishtTxt.ExportToText(isthListeTXT);






                        bordroTable.Rows.Clear();
                        bordroTable.Columns.Clear();


                    }
                }

            }
            baglan.Close();
        }
   
    }
}
