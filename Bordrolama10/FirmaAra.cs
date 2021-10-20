using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Bordrolama10



{
    public partial class FirmaAra : Form
    {
        public FirmaAra()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        public void verilerigoster(string veriler)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }

        private void FirmaAra_Load(object sender, EventArgs e)
        {

            verilerigoster("select firmaid as ID,Firma_No AS NO,Firmakisaadi AS FİRMA_KÜNYE,Vn AS VNO,Refadsoyad AS REFERANS from Hizli_Firma_Kayit");
        }

        private void btnrfrnsara_Click(object sender, EventArgs e)
        {
            verilerigoster("select firmaid as ID,Firma_No AS NO,Firmakisaadi AS FİRMA_KÜNYE,Vn AS VNO,Refadsoyad AS REFERANS from Hizli_Firma_Kayit where Firmakisaadi like '%" + txtunvan.Text + "%' and Refadsoyad like '%" + txtreferans.Text + "%'");
        }


        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            int secilialan = dataGridView1.SelectedCells[0].RowIndex;
            string firmaid = dataGridView1.Rows[secilialan].Cells[0].Value.ToString();
            lblid.Text = firmaid.ToString().Trim();
            programreferans.firmaid = lblid.Text.ToString().Trim();

            this.Hide();
            FirmaKayit fk = new FirmaKayit();
            fk.Show();
        }



    }
}
