using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bordrolama10
{
    public partial class TeknoPersListesiYukle : Form
    {
        public TeknoPersListesiYukle()
        {
            InitializeComponent();
        }

        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);
        static int firmano = Convert.ToInt32(programreferans.firmaid);
        static int subeno = programreferans.subid;

        public void teknoPersonelGoster(string TeknoPersonel)
        {

            SQLiteDataAdapter da = new SQLiteDataAdapter(TeknoPersonel, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns.Add("SıraNo", "SıraNo");
            dataGridView1.Columns["SıraNo"].DisplayIndex = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Columns["SıraNo"][i] = i + 1;
            }

        }
        private void btnDosyaYolu_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            //file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // her açıldığında masa üstünü göstersin
            file.Filter = "Excel Dosyası|*.Xls| Excel Dosyası |*.xlsx| Tüm Dosyalar |*.*";
            file.FilterIndex = 2;
            file.RestoreDirectory = true;// en son açılan dosya klasörünü tekrar açmaya yarar 
            file.CheckFileExists = false;// dosya ismi kısmına manuel isim yazdığında hatayı engeller

            if (file.ShowDialog() == DialogResult.OK)
            {
                txtdosyayolu.Text = file.FileName;

                // işlem yapılacak sayfa seçimi için combobox1 dolduruluyor
                Microsoft.Office.Interop.Excel.Application oXL = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook oWB = oXL.Workbooks.Open(file.FileName);

                List<string> liste = new List<string>();
                foreach (Microsoft.Office.Interop.Excel.Worksheet oSheet in oWB.Worksheets)
                {
                    comboBox1.Items.Add(oSheet.Name);
                }
                oXL.Quit();
            }
        }

        private void TeknoPersListesiYukle_Load(object sender, EventArgs e)
        {
            lblFirmaId.Text = programreferans.firmaid.ToString();
            lblSubeId.Text = programreferans.subid.ToString();
            lblFirmaUnvan.Text = programreferans.firmaunvan;
            lblSubeUnvan.Text = programreferans.subeunvan;
            teknoPersonelGoster("Select SgkNo,Ad,Soyad,IlkSoyad From Tekno5746PersonelListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "'");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (txtdosyayolu.Text.Length == 0 || comboBox1.Text.Length==0)
            {
                MessageBox.Show("Lütfen Bir Dosya Seçiniz veya Sayfa Seçiniz.. ");
            }
            else
            {


                DataTable personelListesi = new DataTable();
                personelListesi.Clear();
                baglan.Open();
                using (SQLiteCommand sorgu = new SQLiteCommand("Select SgkNo,Ad,Soyad,IlkSoyad,firmaid,subeid From Tekno5746PersonelListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "'", baglan))
                {
                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    da.SelectCommand = sorgu;
                    da.Fill(personelListesi);
                }
                baglan.Close();
                if (personelListesi.Rows.Count > 0)
                {
                    DialogResult dialog = new DialogResult();
                    dialog = MessageBox.Show("Personel Listesi Zaten Mevcut, \n Hayır Derseniz Liste Silinerek Yeniden oluşturulacak, \n Listeye Ekleme Yapmak İstiyormusunuz", "Dikkat", MessageBoxButtons.YesNoCancel);

                    if (dialog == DialogResult.Yes)
                    {

                        using (OleDbConnection con = new OleDbConnection())
                        {
                            DataTable yeniPersoneller = new DataTable();
                            con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = '" + txtdosyayolu.Text.Trim().ToString() + "';Extended Properties = 'Excel 8.0;HDR=YES'";
                            con.Open();
                            OleDbCommand sorgu = new OleDbCommand("Select * From [" + comboBox1.Text.ToString() + "$]", con);
                            OleDbDataAdapter da = new OleDbDataAdapter();
                            da.SelectCommand = sorgu;
                            da.Fill(yeniPersoneller);
                            for (int i = 0; i < yeniPersoneller.Rows.Count; i++)
                            {
                                string SgkNo = yeniPersoneller.Rows[i][0].ToString();
                                string Ad = yeniPersoneller.Rows[i][1].ToString();
                                string Soyad = yeniPersoneller.Rows[i][2].ToString();
                                string IlkSoyad = yeniPersoneller.Rows[i][3].ToString();
                                int firmaid = Convert.ToInt32(programreferans.firmaid);
                                int subeid = programreferans.subid;

                                baglan.Open();
                                SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [Tekno5746PersonelListesi] (SgkNo,Ad,Soyad,IlkSoyad,firmaid,subeid) values (@SgkNo,@Ad,@Soyad,@IlkSoyad,@firmaid,@subeid)", baglan);

                                ekle.Parameters.AddWithValue("@SgkNo", SgkNo);
                                ekle.Parameters.AddWithValue("@Ad", Ad);
                                ekle.Parameters.AddWithValue("@Soyad", Soyad);
                                ekle.Parameters.AddWithValue("@IlkSoyad", IlkSoyad);
                                ekle.Parameters.AddWithValue("@firmaid", firmaid);
                                ekle.Parameters.AddWithValue("@subeid", subeid);

                                ekle.ExecuteNonQuery();
                                baglan.Close();

                            }
                            
                        }
                        MessageBox.Show("Yeni Liste Mevcut Listeye kelendi");
                    }
                    if (dialog == DialogResult.No)
                    {
                        using (OleDbConnection con = new OleDbConnection())
                        {
                            {
                                baglan.Open();
                                SQLiteCommand komut = new SQLiteCommand("Delete from Tekno5746PersonelListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "'", baglan);
                                komut.ExecuteNonQuery();
                                MessageBox.Show(lblFirmaUnvan.Text.Substring(0, 20) + " Firmasına ait tüm veriler silinmiştir");
                                baglan.Close();

                                DataTable yeniPersoneller = new DataTable();
                                con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = '" + txtdosyayolu.Text.Trim().ToString() + "';Extended Properties = 'Excel 8.0;HDR=YES'";
                                con.Open();
                                OleDbCommand sorgu = new OleDbCommand("Select * From [" + comboBox1.Text.ToString() + "$]", con);
                                OleDbDataAdapter da = new OleDbDataAdapter();
                                da.SelectCommand = sorgu;
                                da.Fill(yeniPersoneller);
                                for (int i = 0; i < yeniPersoneller.Rows.Count; i++)
                                {
                                    string SgkNo = yeniPersoneller.Rows[i][0].ToString();
                                    string Ad = yeniPersoneller.Rows[i][1].ToString();
                                    string Soyad = yeniPersoneller.Rows[i][2].ToString();
                                    string IlkSoyad = yeniPersoneller.Rows[i][3].ToString();
                                    int firmaid = Convert.ToInt32(programreferans.firmaid);
                                    int subeid = programreferans.subid;

                                    baglan.Open();
                                    SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [Tekno5746PersonelListesi] (SgkNo,Ad,Soyad,IlkSoyad,firmaid,subeid) values (@SgkNo,@Ad,@Soyad,@IlkSoyad,@firmaid,@subeid)", baglan);

                                    ekle.Parameters.AddWithValue("@SgkNo", SgkNo);
                                    ekle.Parameters.AddWithValue("@Ad", Ad);
                                    ekle.Parameters.AddWithValue("@Soyad", Soyad);
                                    ekle.Parameters.AddWithValue("@IlkSoyad", IlkSoyad);
                                    ekle.Parameters.AddWithValue("@firmaid", firmaid);
                                    ekle.Parameters.AddWithValue("@subeid", subeid);

                                    ekle.ExecuteNonQuery();
                                    baglan.Close();

                                }
                            }
                        }
                    }
                    MessageBox.Show("Yeni Liste Mevcut Listeye kelendi");
                }
                else
                {
                    using (OleDbConnection con = new OleDbConnection())
                    {
                        {
                            DataTable yeniPersoneller = new DataTable();
                            con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = '" + txtdosyayolu.Text.Trim().ToString() + "';Extended Properties = 'Excel 8.0;HDR=YES'";
                            con.Open();
                            OleDbCommand sorgu = new OleDbCommand("Select * From [" + comboBox1.Text.ToString() + "$]", con);
                            OleDbDataAdapter da = new OleDbDataAdapter();
                            da.SelectCommand = sorgu;
                            da.Fill(yeniPersoneller);
                            for (int i = 0; i < yeniPersoneller.Rows.Count; i++)
                            {
                                string SgkNo = yeniPersoneller.Rows[i][0].ToString();
                                string Ad = yeniPersoneller.Rows[i][1].ToString();
                                string Soyad = yeniPersoneller.Rows[i][2].ToString();
                                string IlkSoyad = yeniPersoneller.Rows[i][3].ToString();
                                int firmaid = Convert.ToInt32(programreferans.firmaid);
                                int subeid = programreferans.subid;

                                baglan.Open();
                                SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [Tekno5746PersonelListesi] (SgkNo,Ad,Soyad,IlkSoyad,firmaid,subeid) values (@SgkNo,@Ad,@Soyad,@IlkSoyad,@firmaid,@subeid)", baglan);

                                ekle.Parameters.AddWithValue("@SgkNo", SgkNo);
                                ekle.Parameters.AddWithValue("@Ad", Ad);
                                ekle.Parameters.AddWithValue("@Soyad", Soyad);
                                ekle.Parameters.AddWithValue("@IlkSoyad", IlkSoyad);
                                ekle.Parameters.AddWithValue("@firmaid", firmaid);
                                ekle.Parameters.AddWithValue("@subeid", subeid);

                                ekle.ExecuteNonQuery();
                                baglan.Close();

                            }
                        }
                        MessageBox.Show("Yeni Liste Mevcut Listeye kelendi");
                    }
                }

                

            }
            teknoPersonelGoster("Select SgkNo,Ad,Soyad,IlkSoyad From Tekno5746PersonelListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "'");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SQLiteCommand komut = new SQLiteCommand("Delete from Tekno5746PersonelListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "'", baglan);
            komut.ExecuteNonQuery();
            MessageBox.Show(lblFirmaUnvan.Text.Substring(0, 20) + " Firmasına ait tüm veriler silinmiştir");
            baglan.Close();
            teknoPersonelGoster("Select SgkNo,Ad,Soyad,IlkSoyad From Tekno5746PersonelListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "'");
        }


    }
}