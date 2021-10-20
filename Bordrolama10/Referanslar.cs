using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Bordrolama10



{
    public partial class Referanslar : Form
    {
        public Referanslar()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);
        public void verilerigetir(string veriler)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void Referanslar_Load(object sender, EventArgs e)
        {
            verilerigetir("select referansid as ID,referansadsoyad as ADI_SOYADI,reffirmaunvan as FİRMA_UNVAN,refeposta AS EPOSTA,reftelefon as TELEFON,refadres as ADRES, refil as İL, refilce as İLCE,refnotlar1 as NOT1, refnotlar2 as NOTT from ReferansBilgileri");

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string referansid = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            string referansadsoyad = dataGridView1.Rows[secilialan].Cells[1].Value.ToString();
            string reffirmaunvan = dataGridView1.Rows[secilialan].Cells[2].Value.ToString();
            string refeposta = dataGridView1.Rows[secilialan].Cells[3].Value.ToString();
            string reftelefon = dataGridView1.Rows[secilialan].Cells[4].Value.ToString();
            string refadres = dataGridView1.Rows[secilialan].Cells[5].Value.ToString();
            string refil = dataGridView1.Rows[secilialan].Cells[6].Value.ToString();
            string refilce = dataGridView1.Rows[secilialan].Cells[7].Value.ToString();
            string refnotlar1 = dataGridView1.Rows[secilialan].Cells[8].Value.ToString();
            string refnotlar2 = dataGridView1.Rows[secilialan].Cells[9].Value.ToString();

            lblrefno.Text = referansid.ToString().Trim();
            txtrefadsoyad.Text = referansadsoyad.ToString().Trim();
            txtreffirma.Text = reffirmaunvan.ToString().Trim();
            txteposta.Text = refeposta.ToString().Trim();
            txtreftelefon.Text = reftelefon.ToString().Trim();
            rcbadres.Text = refadres.ToString().Trim();
            txtrefil.Text = refil.ToString().Trim();
            txtrefilce.Text = refilce.ToString().Trim();
            rcbnot1.Text = refnotlar1.ToString().Trim();
            rcbnot2.Text = refnotlar2.ToString().Trim();
        }

        private void btnyeni_Click(object sender, EventArgs e)
        {
            tabloyutemizle();
        }
        public void tabloyutemizle()
        {
            lblrefno.Text = "";
            txtrefadsoyad.Text = "";
            txtreffirma.Text = "";
            txteposta.Text = "";
            txtreftelefon.Text = "";
            rcbadres.Text = "";
            txtrefil.Text = "";
            txtrefilce.Text = "";
            rcbnot1.Text = "";
            rcbnot2.Text = "";
            chkbxpasif.Checked = false;
        }


        private void btnkaydet_Click(object sender, EventArgs e)
        {
            string durum = chkbxpasif.Checked ? "Pasif" : "Aktif";
            baglan.Open();
            if (lblrefno.Text == "-")
            {
                SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [ReferansBilgileri] (referansadsoyad ,reffirmaunvan,refeposta ,reftelefon,refadres, refil, refilce,refnotlar1, refnotlar2,aktifpasif) values (@adi,@firma,@eposta,@telefon,@adres,@il,@ilce,@not1,@not2,@durum)", baglan);

                ekle.Parameters.AddWithValue("@adi", txtrefadsoyad.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@firma", txtreffirma.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@eposta", txteposta.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@telefon", txtreftelefon.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@adres", rcbadres.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@il", txtrefil.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@ilce", txtrefilce.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@not1", rcbnot1.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@not2", rcbnot2.Text.ToString().Trim());
                ekle.Parameters.AddWithValue("@durum", durum.ToString().Trim());

                ekle.ExecuteNonQuery();
                MessageBox.Show("Kayıt Başarılı");
            }

            else
            {
                SQLiteCommand guncelle = new SQLiteCommand("update ReferansBilgileri set referansadsoyad='" + txtrefadsoyad.Text + "' ,reffirmaunvan='" + txtreffirma.Text + "',refeposta='" + txteposta.Text + "' ,reftelefon='" + txtreftelefon.Text + "',refadres='" + rcbadres.Text + "', refil='" + txtrefil.Text + "', refilce='" + txtrefilce.Text + "',refnotlar1='" + rcbnot1.Text + "', refnotlar2='" + rcbnot2.Text + "',aktifpasif=@durum where referansid ='" + lblrefno.Text + "'", baglan);

                guncelle.Parameters.AddWithValue("@durum", durum.ToString().Trim());

                guncelle.ExecuteNonQuery();
                MessageBox.Show("Güncelleme Başarılı");
            }
            baglan.Close();
            tabloyutemizle();
            verilerigetir("select referansid as ID,referansadsoyad as ADI_SOYADI,reffirmaunvan as FİRMA_UNVAN,refeposta AS EPOSTA,reftelefon as TELEFON,refadres as ADRES, refil as İL, refilce as İLCE,refnotlar1 as NOT1, refnotlar2 as NOTT from ReferansBilgileri");
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kayıt Silinecektir", "Dikkat", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes)
            {
                baglan.Open();
                int id = Convert.ToInt32(lblrefno.Text);
                SQLiteCommand sil = new SQLiteCommand("delete from ReferansBilgileri where referansid='" + id + "'", baglan);
                sil.ExecuteNonQuery();
                verilerigetir("select referansid as ID,referansadsoyad as ADI_SOYADI,reffirmaunvan as FİRMA_UNVAN,refeposta AS EPOSTA,reftelefon as TELEFON,refadres as ADRES, refil as İL, refilce as İLCE,refnotlar1 as NOT1, refnotlar2 as NOTT from ReferansBilgileri");
                tabloyutemizle();
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Yaptığınız Değişiklikleri Kaydetmeyi Unutmayınız. \n Form Kapatılacaktır Onaylıyormusuzun", "Dikkat", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }

        }
    }

}
