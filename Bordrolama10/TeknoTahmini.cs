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
    public partial class teknoTahmini : Form
    {
        public teknoTahmini()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        private void spektoplamlari(string spekler)
        {

            SQLiteDataAdapter spektoplami = new SQLiteDataAdapter(spekler, baglan);
            DataSet ds = new DataSet();
            spektoplami.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void TeknoTahmini_Load(object sender, EventArgs e)
        {
            lblfirmaunvani.Text = programreferans.firmaunvan;
            lblsubeunvani.Text = programreferans.subeunvan;

            txtKVSoran.Text = "2";
            txtMyOlumOran.Text = "20";
            txtGssOran.Text = "12,5";
            txtIssizlikOran.Text = "3";
            txt5746Oran.Text = "15,5";
            txt5510Oran.Text = "5";

            txtKVSoran.Text = string.Format("{0:#,##0.00}", double.Parse(txtKVSoran.Text));
            txtMyOlumOran.Text = string.Format("{0:#,##0.00}", double.Parse(txtMyOlumOran.Text));
            txtGssOran.Text = string.Format("{0:#,##0.00}", double.Parse(txtGssOran.Text));
            txtIssizlikOran.Text = string.Format("{0:#,##0.00}", double.Parse(txtIssizlikOran.Text));
            txt5746Oran.Text = string.Format("{0:#,##0.00}", double.Parse(txt5746Oran.Text));
            txt5510Oran.Text = string.Format("{0:#,##0.00}", double.Parse(txt5510Oran.Text));

        }

        private void button4_Click(object sender, EventArgs e)
        {
            spektoplamlari("SELECT Kanun_No, Mahiyet, count(SgkNo) as Calisan, sum(Gun) as gun, sum(Ucret) as spek, sum(Ikramiye) as Ikramiye  from HizmetListesi where firmaid = '" + programreferans.firmid + "' and  subeid='" + programreferans.subid + "' and Kanun_No like '%5746%'  GROUP by Mahiyet");

            dataGridView1.Columns["Calisan"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["gun"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["spek"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";

        }

        double spek = 0;
        double YeniSpek = 0;
        double FarkSpek = 0;

        double KVSOran = 0;
        double MyOlumOran = 0;
        double GssOran = 0;
        double Issizlikoran = 0;
        double tekn5746Ind = 0;
        double SGk5510Ind = 0;


        private void button1_Click(object sender, EventArgs e)
        {
            spek = Convert.ToDouble(txtSpek.Text);
            YeniSpek = spek * 0.837541176470588;
            FarkSpek = spek - YeniSpek;

            KVSOran = Convert.ToDouble(txtKVSoran.Text);
            MyOlumOran = Convert.ToDouble(txtMyOlumOran.Text);
            GssOran = Convert.ToDouble(txtGssOran.Text);
            Issizlikoran = Convert.ToDouble(txtIssizlikOran.Text);
            tekn5746Ind = Convert.ToDouble(txt5746Oran.Text);
            SGk5510Ind = Convert.ToDouble(txt5510Oran.Text);



            txtBildirilenSpek.Text = txtSpek.Text;
            txtOlmasıGerSpek.Text = Math.Round((YeniSpek), 2).ToString();
            txtFarkSpek.Text = Math.Round((FarkSpek), 2).ToString();

            txtKVSTahakkuk.Text = Math.Round(((Convert.ToDouble(spek) * KVSOran)/100), 2).ToString();
            txtMyOlumTahakkuk.Text = Math.Round(((Convert.ToDouble(spek) * MyOlumOran) / 100), 2).ToString();
            txtGssTahakkuk.Text = Math.Round(((Convert.ToDouble(spek) * GssOran) / 100), 2).ToString();
            txtIssizlikTahakkuk.Text = Math.Round(((Convert.ToDouble(spek) * Issizlikoran) / 100), 2).ToString();
            txtTahakkukToplam.Text = (Convert.ToDouble(txtKVSTahakkuk.Text) + Convert.ToDouble(txtMyOlumTahakkuk.Text) + Convert.ToDouble(txtGssTahakkuk.Text) + Convert.ToDouble(txtIssizlikTahakkuk.Text)).ToString();
            txt5746TahakkukInd.Text= Math.Round((((Convert.ToDouble(spek) * tekn5746Ind) / 100)/2), 2).ToString();
            txt5510TTahakkukInd.Text = Math.Round(((Convert.ToDouble(spek) * SGk5510Ind) / 100), 2).ToString();
            txtTahakkukNet.Text= (Convert.ToDouble(txtTahakkukToplam.Text) - (Convert.ToDouble(txt5746TahakkukInd.Text) + Convert.ToDouble(txt5510TTahakkukInd.Text))).ToString();

            txtKVSYeniTahakkuk.Text = Math.Round(((Convert.ToDouble(YeniSpek) * KVSOran) / 100), 2).ToString();
            txtMyOlumYeniTahakkuk.Text = Math.Round(((Convert.ToDouble(YeniSpek) * MyOlumOran) / 100), 2).ToString();
            txtGssYeniTahakkuk.Text = Math.Round(((Convert.ToDouble(YeniSpek) * GssOran) / 100), 2).ToString();
            txtIssizlikYeniTahakkuk.Text = Math.Round(((Convert.ToDouble(YeniSpek) * Issizlikoran) / 100), 2).ToString();
            TxtYeniTahakkukToplam.Text = (Convert.ToDouble(txtKVSYeniTahakkuk.Text) + Convert.ToDouble(txtMyOlumYeniTahakkuk.Text) + Convert.ToDouble(txtGssYeniTahakkuk.Text) + Convert.ToDouble(txtIssizlikYeniTahakkuk.Text)).ToString();
            txt5746YeniInd.Text = Math.Round((((Convert.ToDouble(YeniSpek) * tekn5746Ind) / 100) / 2), 2).ToString();
            txt5510YeniInd.Text = Math.Round(((Convert.ToDouble(YeniSpek) * SGk5510Ind) / 100), 2).ToString();
            txtYeniNet.Text = (Convert.ToDouble(TxtYeniTahakkukToplam.Text) - (Convert.ToDouble(txt5746YeniInd.Text) + Convert.ToDouble(txt5510YeniInd.Text))).ToString();

            txtKVSFarkTahakkuk.Text = (Convert.ToDouble(txtKVSTahakkuk.Text) - Convert.ToDouble(txtKVSYeniTahakkuk.Text)).ToString();
            txtMyOlumFarkTahakkuk.Text = (Convert.ToDouble(txtMyOlumTahakkuk.Text) - Convert.ToDouble(txtMyOlumYeniTahakkuk.Text)).ToString();
            txtGssFarkTahakkuk.Text = (Convert.ToDouble(txtGssTahakkuk.Text) - Convert.ToDouble(txtGssYeniTahakkuk.Text)).ToString();
            txtIssizlikFarkTahakkuk.Text = (Convert.ToDouble(txtIssizlikTahakkuk.Text) - Convert.ToDouble(txtIssizlikYeniTahakkuk.Text)).ToString();
            txtFarkTahakkukTopl.Text = (Convert.ToDouble(txtTahakkukToplam.Text) - Convert.ToDouble(TxtYeniTahakkukToplam.Text)).ToString();
            txt5746FarkInd.Text = (Convert.ToDouble(txt5746TahakkukInd.Text) - Convert.ToDouble(txt5746YeniInd.Text)).ToString();
            txt5510FarkInd.Text = (Convert.ToDouble(txt5510TTahakkukInd.Text) - Convert.ToDouble(txt5510YeniInd.Text)).ToString();
            txtFarkNet.Text = (Convert.ToDouble(txtTahakkukNet.Text) - Convert.ToDouble(txtYeniNet.Text)).ToString();

            txtBildirilenSpek.Text = string.Format("{0:#,##0.00}", double.Parse(txtBildirilenSpek.Text));
            txtSpek.Text = string.Format("{0:#,##0.00}", double.Parse(txtSpek.Text));
            txtOlmasıGerSpek.Text = string.Format("{0:#,##0.00}", double.Parse(txtOlmasıGerSpek.Text));
            txtFarkSpek.Text = string.Format("{0:#,##0.00}", double.Parse(txtFarkSpek.Text));

            txtKVSTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtKVSTahakkuk.Text));
            txtMyOlumTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtMyOlumTahakkuk.Text));
            txtGssTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtGssTahakkuk.Text));
            txtIssizlikTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtIssizlikTahakkuk.Text));
            txtTahakkukToplam.Text = string.Format("{0:#,##0.00}", double.Parse(txtTahakkukToplam.Text));
            txt5746TahakkukInd.Text = string.Format("{0:#,##0.00}", double.Parse(txt5746TahakkukInd.Text));
            txt5510TTahakkukInd.Text = string.Format("{0:#,##0.00}", double.Parse(txt5510TTahakkukInd.Text));
            txtTahakkukNet.Text = string.Format("{0:#,##0.00}", double.Parse(txtTahakkukNet.Text));

            txtKVSYeniTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtKVSYeniTahakkuk.Text));
            txtMyOlumYeniTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtMyOlumYeniTahakkuk.Text));
            txtGssYeniTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtGssYeniTahakkuk.Text));
            txtIssizlikYeniTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtIssizlikYeniTahakkuk.Text));
            TxtYeniTahakkukToplam.Text = string.Format("{0:#,##0.00}", double.Parse(TxtYeniTahakkukToplam.Text));
            txt5746YeniInd.Text = string.Format("{0:#,##0.00}", double.Parse(txt5746YeniInd.Text));
            txt5510YeniInd.Text = string.Format("{0:#,##0.00}", double.Parse(txt5510YeniInd.Text));
            txtYeniNet.Text = string.Format("{0:#,##0.00}", double.Parse(txtYeniNet.Text));

            txtKVSFarkTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtKVSFarkTahakkuk.Text));
            txtMyOlumFarkTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtMyOlumFarkTahakkuk.Text));
            txtGssFarkTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtGssFarkTahakkuk.Text));
            txtIssizlikFarkTahakkuk.Text = string.Format("{0:#,##0.00}", double.Parse(txtIssizlikFarkTahakkuk.Text));
            txtFarkTahakkukTopl.Text = string.Format("{0:#,##0.00}", double.Parse(txtFarkTahakkukTopl.Text));
            txt5746FarkInd.Text = string.Format("{0:#,##0.00}", double.Parse(txt5746FarkInd.Text));
            txt5510FarkInd.Text = string.Format("{0:#,##0.00}", double.Parse(txt5510FarkInd.Text));
            txtFarkNet.Text = string.Format("{0:#,##0.00}", double.Parse(txtFarkNet.Text));

            lblMinKazanc.Text = txtFarkNet.Text;
            
        }
    }
}
