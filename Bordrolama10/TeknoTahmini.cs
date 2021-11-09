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
    public partial class TeknoTahmini : Form
    {
        public TeknoTahmini()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        private void spektoplamlari (string spekler)
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            spektoplamlari("SELECT Kanun_No, Mahiyet, count(SgkNo) as Calisan, sum(Gun) as gun, sum(Ucret) as spek, sum(Ikramiye) as Ikramiye  from HizmetListesi where firmaid = '"+programreferans.firmid+"' and  subeid='"+programreferans.subid+"' and Kanun_No like '%5746%'  GROUP by Mahiyet");

            dataGridView1.Columns["Calisan"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["gun"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["spek"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";
            


        }
    }
}
