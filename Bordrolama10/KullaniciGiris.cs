using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Bordrolama10
{
    public partial class KullaniciGiris : Form
    {
        public KullaniciGiris()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        int sayac = 0;
        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtkullanici.Text == "Bordrolama" && txtsifre.Text == "Bordrolama571")
            {

                var bugun = DateTime.Now.ToString("dd.MM.yyyy");
                var sonTarih = "01.05.2022";
                if (Convert.ToDateTime(bugun) < Convert.ToDateTime(sonTarih))
                {
                    AnaEkran ana = new AnaEkran();
                    ana.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Programda Güncellenmesi Gereken eklentiler Mevcut Lütfen Yazılım Ekibiyle irtibata geçiniz...");
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Yanlış Şifre veya Kullanıcı Adı");
                sayac = sayac + 1;
                if (sayac == 3)
                {
                    MessageBox.Show("Maximum giriş denemesini geçtiniz");
                    this.Close();
                }
            }
            if (txtkullanici.Text == "Bordrolama" && txtsifre.Text == "Bordrolama784512")
            {
                AnaEkran ana = new AnaEkran();
                ana.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            btnKaydet.Visible = true;
            label3.Visible = true;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Dosyayı Nereye Kaydetmek İstersiniz?";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = @"C:\Program Files";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.SelectedPath;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            //var appSettingsSection = (AppSettingsSection)config.GetSection("appSettings");
            config.AppSettings.Settings["Baglanti"].Value = "Data Source =" + textBox1.Text + "\\TesvikData.db";

            connectionStringsSection.ConnectionStrings["TesvikData"].ConnectionString = "XpoProvider=SQLite;Data Source="+textBox1.Text+ "\\TesvikData.db";
            connectionStringsSection.ConnectionStrings["TesvikData 1"].ConnectionString = "XpoProvider=SQLite;Data Source=" + textBox1.Text + "\\TesvikData.db";

            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            ConfigurationManager.RefreshSection("appSettings");

            textBox1.Visible = false;
            btnKaydet.Visible = false;
            label3.Visible = false;

            //if (baglan.State==ConnectionState.Closed)
            //{
            //    baglan.Open();
            //    MessageBox.Show("Veri Tabanı Baglantısı Doğru");
            //}
            //else
            //{
            //    MessageBox.Show("Veri Tabanı Bağlantısı Yanlış");
            //}
            

        }
    }

}
