using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Bordrolama10


{
    public partial class KanuniParametre : Form
    {
        public KanuniParametre()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);
        DataTable sgkara = new DataTable();

        public int dnmid { get; set; }
        DataView yilfiltrele()
        {
            DataView dv = new DataView();
            dv = sgkara.DefaultView;
            dv.RowFilter = "YIL like '" + txtyilfiltre.Text + "%'";
            return dv;
        }
        DataView donemfiltre()
        {
            DataView dv1 = new DataView();
            dv1 = sgkara.DefaultView;
            dv1.RowFilter = "DONEM like '" + txtdonemfiltre.Text + "%'";
            return dv1;
        }



        public void sgkdonembilgiler(string sgkdonembilg)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter("Select asgucid as Id, asg_yil as YIL,asg_ay as AY, asg_donem as DONEM, asg_taban_ucr as ASGARİ_UCR, asg_tavan_ucr as TAVAN_UCR From yillik_taban_tavan_ucr", baglan);
            //DataSet ds = new DataSet();
            da.Fill(sgkara);
            dataGridView1.DataSource = sgkara;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns["ASGARİ_UCR"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["TAVAN_UCR"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["ASGARİ_UCR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["TAVAN_UCR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["DONEM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void sgkdonembilgilergoster(string sgkdonembilg)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter("Select asgucid as Id, asg_yil as YIL,asg_ay as AY, asg_donem as DONEM, asg_taban_ucr as ASGARİ_UCR, asg_tavan_ucr as TAVAN_UCR From yillik_taban_tavan_ucr", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns["ASGARİ_UCR"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["TAVAN_UCR"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["ASGARİ_UCR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["TAVAN_UCR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["DONEM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void gvdonembilgilerinigoster(string gvdonembilgi)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter("select agiid as ID, agi_yil as YIL, agi_minumum as AGİ_TBN, agi_maxsimum as AGİ_TVN, asgariucr_gv as ASG_GV, agi_asgucr_fark_gv as FARK, asgariucr_dv as DV from agi_tablosu", baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.Columns["AGİ_TBN"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["AGİ_TVN"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["ASG_GV"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["FARK"].DefaultCellStyle.Format = "N2";
            dataGridView2.Columns["DV"].DefaultCellStyle.Format = "N2";

            dataGridView2.Columns["AGİ_TBN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["AGİ_TVN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["ASG_GV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["FARK"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["DV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


        private void KanuniParametre_Load(object sender, EventArgs e)
        {
            sgkdonembilgiler("Select asgucid as Id, asg_yil as YIL,asg_ay as AY, asg_dönem as DONEM, asg_taban_ucr as ASGARİ_UCR, asg_tavan_ucr as TAVAN_UCR From yillik_taban_tavan_ucr");
            gvdonembilgilerinigoster("select agiid as ID, agi_yil as YIL, agi_minumum as AGİ_TBN, agi_maxsimum as AGİ_TVN, asgariucr_gv as ASG_GV, agi_asgucr_fark_gv as FARK, asgariucr_dv as DV from agi_tablosu");
        }

        private void txtyilfiltre_TextChanged(object sender, EventArgs e)
        {
            yilfiltrele();
        }

        private void txtdonemfiltre_TextChanged(object sender, EventArgs e)
        {
            donemfiltre();
        }
        public void temizle()
        {
            txtYil.Text = "";
            TxtAy.Text = "";
            txtDonem.Text = "";
            txtTaban.Text = "";
            TxtTavan.Text = "";
        }
        private void btnYeni_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[secim].Cells[0].Value.ToString().Trim();
            string yil = dataGridView1.Rows[secim].Cells[1].Value.ToString().Trim();
            string ay = dataGridView1.Rows[secim].Cells[2].Value.ToString().Trim();
            string donem = dataGridView1.Rows[secim].Cells[3].Value.ToString().Trim();
            string taban = dataGridView1.Rows[secim].Cells[4].Value.ToString().Trim();
            string tavan = dataGridView1.Rows[secim].Cells[5].Value.ToString().Trim();

            txtYil.Text = yil;
            TxtAy.Text = ay;
            txtDonem.Text = donem;
            txtTaban.Text = taban;
            TxtTavan.Text = tavan;

            txtTaban.Text = string.Format("{0:#,##0.00}", double.Parse(txtTaban.Text));
            TxtTavan.Text = string.Format("{0:#,##0.00}", double.Parse(TxtTavan.Text));

        }

        private void btnKaytet_Click(object sender, EventArgs e)
        {
            baglan.Open();
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[secim].Cells[0].Value.ToString().Trim();
            int dnmid = Convert.ToInt32(id);
            if (dnmid > 1)
            {
                SQLiteCommand guncelle = new SQLiteCommand("Update [yillik_taban_tavan_ucr] set asg_yil=@yil, asg_ay=@ay, asg_donem=@donem, asg_taban_ucr=@taban, asg_tavan_ucr=@tavan WHERE asgucid='" + dnmid + "'", baglan);

                guncelle.Parameters.AddWithValue("@yil", txtYil.Text.ToString().Trim());
                guncelle.Parameters.AddWithValue("@ay", TxtAy.Text.ToString().Trim());
                guncelle.Parameters.AddWithValue("@donem", txtDonem.Text.ToString().Trim());
                guncelle.Parameters.AddWithValue("@taban", Convert.ToDecimal(txtTaban.Text.ToString().Trim()));
                guncelle.Parameters.AddWithValue("@tavan", Convert.ToDecimal(TxtTavan.Text.ToString().Trim()));
                guncelle.ExecuteNonQuery();
                MessageBox.Show("Veriler Başarı İle Guncellendi");
            }
            else
            {
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [yillik_taban_tavan_ucr] (asg_yil,asg_ay,asg_donem,asg_taban_ucr,asg_tavan_ucr) values (@yil,@ay,@donem,@taban,@tavan)", baglan);
                ekle.Parameters.AddWithValue("@yil", txtYil.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@ay", TxtAy.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@donem", txtDonem.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@taban", Convert.ToDecimal(txtTaban.Text.ToString().Trim()));
                ekle.Parameters.AddWithValue("@tavan", Convert.ToDecimal(TxtTavan.Text.ToString().Trim()));
                ekle.ExecuteNonQuery();
                MessageBox.Show("Verier Başarı İle Veri Tabanına Eklendi");
            }

            baglan.Close();

            sgkdonembilgilergoster("Select asgucid as Id, asg_yil as YIL,asg_ay as AY, asg_dönem as DONEM, asg_taban_ucr as ASGARİ_UCR, asg_tavan_ucr as TAVAN_UCR From yillik_taban_tavan_ucr");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string id = dataGridView1.Rows[secim].Cells[0].Value.ToString().Trim();
            int dnmid = Convert.ToInt32(id);
            baglan.Open();
            SQLiteCommand sil = new SQLiteCommand("delete from yillik_taban_tavan_ucr where asgucid='" + dnmid + "' ", baglan);
            sil.ExecuteNonQuery();
            baglan.Close();

            sgkdonembilgilergoster("Select asgucid as Id, asg_yil as YIL,asg_ay as AY, asg_dönem as DONEM, asg_taban_ucr as ASGARİ_UCR, asg_tavan_ucr as TAVAN_UCR From yillik_taban_tavan_ucr");
        }

        private void txtDonem_Click(object sender, EventArgs e)
        {
            txtDonem.Text = txtYil.Text + "/" + TxtAy.Text;
        }

        private void txtTaban_Leave(object sender, EventArgs e)
        {
            txtTaban.Text = string.Format("{0:#,##0.00}", double.Parse(txtTaban.Text));
        }

        private void TxtTavan_Leave_1(object sender, EventArgs e)
        {
            TxtTavan.Text = string.Format("{0:#,##0.00}", double.Parse(TxtTavan.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtgvyil.Text = "";
            txtagitaban.Text = "";
            txtagitavan.Text = "";
            txtasgucrgv.Text = "";
            txtagigvfarki.Text = "";
            txtasgdv.Text = "";
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            int secim = dataGridView2.SelectedCells[0].RowIndex;
            string id = dataGridView2.Rows[secim].Cells[0].Value.ToString().Trim();
            string yil = dataGridView2.Rows[secim].Cells[1].Value.ToString().Trim();
            string agiTaban = dataGridView2.Rows[secim].Cells[2].Value.ToString().Trim();
            string agiTavan = dataGridView2.Rows[secim].Cells[3].Value.ToString().Trim();
            string auGv = dataGridView2.Rows[secim].Cells[4].Value.ToString().Trim();
            string agiAufark = dataGridView2.Rows[secim].Cells[5].Value.ToString().Trim();
            string auDv = dataGridView2.Rows[secim].Cells[6].Value.ToString().Trim();

            txtgvyil.Text = yil;
            txtagitaban.Text = agiTaban.ToString();
            txtagitavan.Text = agiTavan.ToString();
            txtasgucrgv.Text = auGv.ToString();
            txtagigvfarki.Text = agiAufark.ToString();
            txtasgdv.Text = auDv.ToString();

            txtagigvfarki.Text = string.Format("{0:#,##0.00}", double.Parse(txtagigvfarki.Text));
            txtasgucrgv.Text = string.Format("{0:#,##0.00}", double.Parse(txtasgucrgv.Text));
            txtagitavan.Text = string.Format("{0:#,##0.00}", double.Parse(txtagitavan.Text));
            txtagitaban.Text = string.Format("{0:#,##0.00}", double.Parse(txtagitaban.Text));
            txtasgdv.Text = string.Format("{0:#,##0.00}", double.Parse(txtasgdv.Text));



        }

        private void button4_Click(object sender, EventArgs e)
        {
            int secim = dataGridView2.SelectedCells[0].RowIndex;
            string id = dataGridView2.Rows[secim].Cells[0].Value.ToString().Trim();
            int dnmid = Convert.ToInt32(id);
            baglan.Open();
            if (dnmid > 1)
            {
                SQLiteCommand guncele = new SQLiteCommand("Update[agi_tablosu] get agi_yil=@yil ,agi_minumum=@agitaban ,agi_maxsimum=@agitavan ,asgariucr_gv=@augv ,agi_asgucr_fark_gv=@agiauFark ,asgariucr_dv=@audv ,agi_asgucr_fark_dv=@audvFark WHERE agiid='" + dnmid + "'", baglan);

                guncele.Parameters.AddWithValue("@yil", txtYil.Text.ToString().Trim());
                guncele.Parameters.AddWithValue("@agitaban", Convert.ToDecimal(txtagitaban.Text).ToString().Trim());
                guncele.Parameters.AddWithValue("@agitavan", Convert.ToDecimal(txtagitavan.Text).ToString().Trim());
                guncele.Parameters.AddWithValue("@augv", Convert.ToDecimal(txtasgucrgv.Text).ToString().Trim());
                guncele.Parameters.AddWithValue("@agiauFark", Convert.ToDecimal(txtagigvfarki.Text).ToString().Trim());
                guncele.Parameters.AddWithValue("@audv", Convert.ToDecimal(txtasgdv.Text).ToString().Trim());
                guncele.Parameters.AddWithValue("@audvFark", Convert.ToDecimal(txtasgdv.Text).ToString().Trim());
                guncele.ExecuteNonQuery();
                MessageBox.Show("Veriler Başarılı Bir Şekilde Veri Tabanında Güncellendi");

                gvdonembilgilerinigoster("select agiid as ID, agi_yil as YIL, agi_minumum as AGİ_TBN, agi_maxsimum as AGİ_TVN, asgariucr_gv as ASG_GV, agi_asgucr_fark_gv as FARK, asgariucr_dv as DV from agi_tablosu");
            }
            else
            {
                SQLiteCommand ekle = new SQLiteCommand("Insert Into [agi_tablosu] ( agi_yil,agi_minumum ,agi_maxsimum,asgariucr_gv,agi_asgucr_fark_gv,asgariucr_dv,agi_asgucr_fark_dv ) values (@yil,@agitaban,@agitavan,@augv,@agiauFark,@audv,@audvFark)", baglan);

                ekle.Parameters.AddWithValue("@yil", txtYil.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@agitaban", Convert.ToDecimal(txtagitaban.Text).ToString().Trim());
                ekle.Parameters.AddWithValue("@agitavan", Convert.ToDecimal(txtagitavan.Text).ToString().Trim());
                ekle.Parameters.AddWithValue("@augv", Convert.ToDecimal(txtasgucrgv.Text).ToString().Trim());
                ekle.Parameters.AddWithValue("@agiauFark", Convert.ToDecimal(txtagigvfarki.Text).ToString().Trim());
                ekle.Parameters.AddWithValue("@audv", Convert.ToDecimal(txtasgdv.Text).ToString().Trim());
                ekle.Parameters.AddWithValue("@audvFark", Convert.ToDecimal(txtasgdv.Text).ToString().Trim());
                ekle.ExecuteNonQuery();
                MessageBox.Show("Veriler Başarılı Bir Şekilde Veri Tabanına Eklendi");

                gvdonembilgilerinigoster("select agiid as ID, agi_yil as YIL, agi_minumum as AGİ_TBN, agi_maxsimum as AGİ_TVN, asgariucr_gv as ASG_GV, agi_asgucr_fark_gv as FARK, asgariucr_dv as DV from agi_tablosu");
            }
            baglan.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglan.Open();
            int secim = dataGridView2.SelectedCells[0].RowIndex;
            string id = dataGridView2.Rows[secim].Cells[0].Value.ToString().Trim();
            int dnmid = Convert.ToInt32(id);
            SQLiteCommand sil = new SQLiteCommand("Delete From agi_tablosu WHERE agiid='" + dnmid + "'", baglan);
            sil.ExecuteNonQuery();
            gvdonembilgilerinigoster("select agiid as ID, agi_yil as YIL, agi_minumum as AGİ_TBN, agi_maxsimum as AGİ_TVN, asgariucr_gv as ASG_GV, agi_asgucr_fark_gv as FARK, asgariucr_dv as DV from agi_tablosu");
            baglan.Close();
        }

        private void txtasgdv_Leave(object sender, EventArgs e)
        {
            txtasgdv.Text = string.Format("{0:#,##0.00}", double.Parse(txtasgdv.Text));
        }

        private void txtagitaban_Leave(object sender, EventArgs e)
        {
            txtagitaban.Text = string.Format("{0:#,##0.00}", double.Parse(txtagitaban.Text));
        }

        private void txtagitavan_Leave(object sender, EventArgs e)
        {
            txtagitavan.Text = string.Format("{0:#,##0.00}", double.Parse(txtagitavan.Text));
        }

        private void txtasgucrgv_Leave(object sender, EventArgs e)
        {
            txtasgucrgv.Text = string.Format("{0:#,##0.00}", double.Parse(txtasgucrgv.Text));
        }

        private void txtagigvfarki_Leave(object sender, EventArgs e)
        {
            txtagigvfarki.Text = string.Format("{0:#,##0.00}", double.Parse(txtagigvfarki.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}
