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

namespace Bordrolama10
{
    public partial class BordroHesapla : Form
    {
        public BordroHesapla()
        {
            InitializeComponent();
        }



        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);
        string commadSubeListele = "SELECT sb.subeid as Id, sb.subeunvan as Sube_Unvan, count(fb.KanunNo) as tesvikli,round(sum(fb.TerkinGv),2) as TrkGv,round(sum(fb.TerkinDv),2) as TrkDv  From FirmaBordro fb INNER JOIN sube_bilgileri as sb on sb.subeid = fb.subeno where sb.firmaid = '" + firmaid + "' GROUP by sb.subeid";
        string commadGvTesvikDonemBazli = "SELECT PuantajDonem as Donem, count(BdrId) as Çalışan, count(KanunNo) as tesvikli,round(sum(TerkinGv), 2) as TrkGv,round(sum(TerkinDv), 2) as TrkDv  from firmaBordro WHERE FirmaNo = '" + firmaid + "'  GROUP by PuantajDonem";
        string commadGvTesvikliPers = "SELECT FirmaPersId as Id, PuantajDonem as Donem, KanunNo, PersAdı, PersSoyadı, PrimGunu, GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, round(TerkinGv, 2) as TERKINgv, Round(TerkinDv, 2) as TERKİNdv From FirmaBordro where FirmaNo = '" + firmaid + "' and PuantajDonem = '" + donem + "' and (KanunNo = '00687' or KanunNo = '01687' or KanunNo = '17103' or KanunNo = '27103')";
        string commadHizmetListesi = "SELECT firmPersid as Id, Donem, SgkNo, Ad,Soyad,Ucret,Ikramiye,Gun,Eksik_Gun,GGun,CGun,Kanun_No,Mahiyet From HizmetListesi WHERE firmaid='" + firmaid + "' and Donem='" + donem + "'";
        string commadGvTesvikBordro = "SELECT FirmaPersId as Id, PuantajDonem as Donem,PersAdı,PersSoyadı,GirisTarihi,CikisTarihi,PrimGunu,AylikBrutUcret as Brut, ToplamKazanc as KazançTop,SgkMatrahi,SGkIsciPrim as isciPrm,IszlikIsciPrim as iszIsci,KumVergMatr as Kum_Matr,GvMatrahi, GelirVergisi, Agi, VergiInd,DamgaVrg,KanunNo, round(TerkinGv,2) as TERKINgv, Round(TerkinDv,2) as TERKİNdv, SgkIsverenPrim as SgkIsv,IssizlikIsvPrim as iszIsv,AylikNetUcret  From FirmaBordro where FirmaNo = '" + firmaid + "' and PuantajDonem='" + donem + "'";


        static int firmaid;
        static int subeid;
        static string donem;
        

        public void subelistele()
        {
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter(commadSubeListele, baglan);
            DataTable dt = new DataTable();
            
            da.Fill(dt);
            baglan.Close();
            dataGridView4.DataSource = dt;
        }

        public void gvTesvikDonemBazli()
        {
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter(commadGvTesvikDonemBazli, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglan.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void gvTesvikBordro()
        {
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter(commadGvTesvikBordro, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglan.Close();
            dataGridView2.DataSource = ds.Tables[0];
        }
        public void gvTesvikiPers()
        {
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter(commadGvTesvikliPers, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglan.Close();
            dataGridView5.DataSource = ds.Tables[0];
        }
        public void hizmetListesi()
        {
            baglan.Open();
            SQLiteDataAdapter da = new SQLiteDataAdapter(commadHizmetListesi, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            baglan.Close();
            dataGridView3.DataSource = ds.Tables[0];
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
            donem = dataGridView1.Rows[0].Cells[0].Value.ToString();
            gvTesvikiPers();
            hizmetListesi();
            gvTesvikBordro();

            grid3duduzenle_HizmetListesi();
            grid4duduzenle_SubeBazli();
            grid1duduzenle_DonemBazli();
            grid2duduzenle_firmaBordro();
        }

        private void dataGridView4_Click(object sender, EventArgs e)
        {
            int secim = dataGridView4.SelectedCells[0].RowIndex;
            subeid = Convert.ToInt32(dataGridView4.Rows[secim].Cells[0].Value);
            gvTesvikDonemBazli();
            grid4duduzenle_SubeBazli();
        }


        private void grid2duduzenle_firmaBordro()
        {

            dataGridView2.Columns["Brut"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["KazançTop"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["SgkMatrahi"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["isciPrm"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["iszIsci"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["GvMatrahi"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["GelirVergisi"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["Agi"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["VergiInd"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["DamgaVrg"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["TERKINgv"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["TERKİNdv"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["SgkIsv"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["iszIsv"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["AylikNetUcret"].DefaultCellStyle.Format = "N2";

        }
        private void grid3duduzenle_HizmetListesi()
        {

            dataGridView3.Columns["Ucret"].DefaultCellStyle.Format = "N2";
            dataGridView3.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";
        }

        private void grid1duduzenle_DonemBazli()
        {

            dataGridView1.Columns["TrkGv"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["TrkDv"].DefaultCellStyle.Format = "N2";
        }
        private void grid4duduzenle_SubeBazli()
        {
            dataGridView4.Columns["TrkGv"].DefaultCellStyle.Format = "N2";
            dataGridView4.Columns["TrkDv"].DefaultCellStyle.Format = "N2";
        }

    }
}
