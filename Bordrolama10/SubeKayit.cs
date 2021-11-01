using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
using OpenQA.Selenium.Chrome;
using System.Net;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using System.IO;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace Bordrolama10

{
    public partial class SubeKayit : Form
    {
        public SubeKayit()
        {
            InitializeComponent();
        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);

        public IWebDriver v1driver1 { get; private set; }

        // public IWebDriver driver { get; set; }
        public void verilerigoster(string veriler)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["subeid"].Visible = false;
            dataGridView1.Columns["firmaid"].Visible = false;
            dataGridView1.Columns["isyeriSubeKodu"].Visible = false;

        }
        public void tabloyutemizle()
        {
            lblsubeid.Text = "-";
            txttamunvan.Text = "";
            txtsubeunvan.Text = "";
            txtvd.Text = "";
            txtvn.Text = "";
            txtticsic.Text = "";
            txtisyerisicil.Text = "";
            rcbadres.Text = "";
            txtil.Text = "";
            txtilce.Text = "";
            txtsgkkullanici.Text = "";
            txtek.Text = "";
            txtsistem.Text = "";
            txtsgkisyeri.Text = "";
            txtsubeno.Text = "";

        }


        private void SubeKayit_Load(object sender, EventArgs e)
        {
            baglan.Open();
            SQLiteCommand cmddoldur = new SQLiteCommand("select *  From Hizli_Firma_Kayit", baglan);
            SQLiteDataReader rdr = cmddoldur.ExecuteReader();
            while (rdr.Read())
            {
                comboBox1.Items.Add(rdr[2]);
            }
            baglan.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblfirmano.Text = "-";
            lblvd.Text = "";
            lblvn.Text = "";
            lblyetkiliadsoyad.Text = "";
            lbltelefon.Text = "";
            lbleposta.Text = "";
            FirmaAra fr = new FirmaAra();
            fr.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lblfirmano.Text == "-")
            {
                MessageBox.Show("Firma Seçimi Yapmadan Şube Kayıt İşlemini Gerçekleştiremezsiniz...!");
            }
            else
            {
                int firmaid = Convert.ToInt32(lblfirmano.Text);
                if (lblsubeid.Text == "-")
                {

                    baglan.Open();
                    SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [sube_bilgileri] (subeno,firmunvantam,subeunvan,vd,vn,ticsiciln,sgkisyerino,adres,il,ilce,sgkkullanici,sgkek,sgksistemsif,sgkisyerisif,aktifpasif,firmaid,isyeriSubeKodu,subeNotu) values (@subeno,@firmaunvan,@subeunvan,@vd,@vn,@ticsicil,@sgkisyerino,@adres,@il,@ilce,@sgkkullanici,@sgkek,@sgksistem,@sgkissif,@aktifpasif,@firmaid,@isySubKod,@subeNotu)", baglan);



                    //int subeid = Convert.ToInt32(lblsubeno.Text);
                    //int firmaid = Convert.ToInt32(lblfirmano.Text);
                    ekle.Parameters.AddWithValue("@subeno", txtsubeno.Text);
                    ekle.Parameters.AddWithValue("@firmaunvan", txttamunvan.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@subeunvan", txtsubeunvan.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@vd", txtvd.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@vn", txtvn.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@ticsicil", txtticsic.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@sgkisyerino", txtisyerisicil.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@adres", rcbadres.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@il", txtil.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@ilce", txtilce.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@sgkkullanici", txtsgkkullanici.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@sgkek", txtek.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@sgksistem", txtsgkisyeri.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@sgkissif", txtsistem.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@isySubKod", txtisySubeKod.Text.ToString().Trim());
                    ekle.Parameters.AddWithValue("@subeNotu", richFirmaNotu.Text.ToString().Trim());
                    string durum = (chkbxpasif.Checked == true) ? "Pasif" : "Aktif";
                    ekle.Parameters.AddWithValue("@aktifpasif", durum);
                    //ekle.Parameters.AddWithValue("@subeid", subeid);
                    ekle.Parameters.AddWithValue("@firmaid", firmaid);
                    ekle.ExecuteNonQuery();

                    MessageBox.Show("Kayıt  Eklendi");
                    baglan.Close();

                }
                else
                {



                    baglan.Open();
                    SQLiteCommand guncelle = new SQLiteCommand("update [sube_bilgileri] set subeno=@subeno,firmunvantam=@firmaunvan,subeunvan=@subeunvan,vd=@vd,vn=@vn,ticsiciln=@ticsicil,sgkisyerino=@sgkisyerino,adres=@adres,il=@il,ilce=@ilce,sgkkullanici=@sgkkullanici,sgkek=@sgkek,sgksistemsif=@sgksistem,sgkisyerisif=@sgkissif,aktifpasif=@aktifpasif,firmaid=@firmaid,isyeriSubeKodu=@isySubKod,subeNotu=@subeNotu WHERE subeid = @subeid", baglan);

                    int subeid = Convert.ToInt32(lblsubeid.Text);
                    //int firmaid = Convert.ToInt32(lblfirmano.Text);
                    guncelle.Parameters.AddWithValue("@subeno", txtsubeno.Text);
                    guncelle.Parameters.AddWithValue("@firmaunvan", txttamunvan.Text);
                    guncelle.Parameters.AddWithValue("@subeunvan", txtsubeunvan.Text);
                    guncelle.Parameters.AddWithValue("@vd", txtvd.Text);
                    guncelle.Parameters.AddWithValue("@vn", txtvn.Text);
                    guncelle.Parameters.AddWithValue("@ticsicil", txtticsic.Text);
                    guncelle.Parameters.AddWithValue("@sgkisyerino", txtisyerisicil.Text);
                    guncelle.Parameters.AddWithValue("@adres", rcbadres.Text);
                    guncelle.Parameters.AddWithValue("@il", txtil.Text);
                    guncelle.Parameters.AddWithValue("@ilce", txtilce.Text);
                    guncelle.Parameters.AddWithValue("@sgkkullanici", txtsgkkullanici.Text);
                    guncelle.Parameters.AddWithValue("@sgkek", txtek.Text);
                    guncelle.Parameters.AddWithValue("@sgksistem", txtsgkisyeri.Text);
                    guncelle.Parameters.AddWithValue("@sgkissif", txtsistem.Text);
                    guncelle.Parameters.AddWithValue("@isySubKod", txtisySubeKod.Text);
                    guncelle.Parameters.AddWithValue("@subeNotu", richFirmaNotu.Text.ToString().Trim());
                    String durum = (chkbxpasif.Checked == true) ? "Pasif" : "Aktif";
                    guncelle.Parameters.AddWithValue("@aktifpasif", durum);
                    guncelle.Parameters.AddWithValue("@subeid", subeid);
                    guncelle.Parameters.AddWithValue("@firmaid", firmaid);
                    guncelle.ExecuteNonQuery();

                    MessageBox.Show("Kayıt  Güncellendi");
                    baglan.Close();


                }

                verilerigoster("Select * from sube_bilgileri where firmaid = '" + firmaid + "'");
            }
            tabloyutemizle();

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            baglan.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * From Hizli_Firma_Kayit where Firmakisaadi = '" + comboBox1.Text.ToString() + "'", baglan);
            SQLiteDataReader firmabilgileri = cmd.ExecuteReader();
            while (firmabilgileri.Read())
            {
                lblfirmano.Text = (firmabilgileri[0].ToString());
                lblvd.Text = (firmabilgileri[3].ToString());
                lblvn.Text = (firmabilgileri[4].ToString());
                lblyetkiliadsoyad.Text = (firmabilgileri[5].ToString());
                lbltelefon.Text = (firmabilgileri[6].ToString());
                lbleposta.Text = (firmabilgileri[7].ToString());
                lblvdkullanici.Text = (firmabilgileri[11].ToString());
                lblvdparola.Text = (firmabilgileri[12].ToString());
                lblvdsifre.Text = (firmabilgileri[13].ToString());

            }


            firmabilgileri.Close();
            int firmaid = Convert.ToInt32(lblfirmano.Text);
            verilerigoster("Select * from sube_bilgileri where firmaid = '" + firmaid + "'"); // COMBOBOX CLİK İLEMİNE EKLENECEK
            baglan.Close();



        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            int subid = dataGridView1.Rows[secim].Cells[0].Value != DBNull.Value ? Convert.ToInt32(dataGridView1.Rows[secim].Cells[0].Value) :0;
            
            string subeno = dataGridView1.Rows[secim].Cells[1].Value.ToString().Trim();
            string firmaunvan = dataGridView1.Rows[secim].Cells[2].Value.ToString().Trim();
            string subeunvan = dataGridView1.Rows[secim].Cells[3].Value.ToString().Trim();
            string vd = dataGridView1.Rows[secim].Cells[4].Value.ToString().Trim();
            string vn = dataGridView1.Rows[secim].Cells[5].Value.ToString().Trim();
            string ticsicil = dataGridView1.Rows[secim].Cells[6].Value.ToString().Trim();
            string sgkisyerino = dataGridView1.Rows[secim].Cells[7].Value.ToString().Trim();
            string adres = dataGridView1.Rows[secim].Cells[8].Value.ToString().Trim();
            string il = dataGridView1.Rows[secim].Cells[9].Value.ToString().Trim();
            string ilce = dataGridView1.Rows[secim].Cells[10].Value.ToString().Trim();
            string sgkkullanici = dataGridView1.Rows[secim].Cells[11].Value.ToString().Trim();
            string sgkek = dataGridView1.Rows[secim].Cells[12].Value.ToString().Trim();
            string sgksistem = dataGridView1.Rows[secim].Cells[13].Value.ToString().Trim();
            string sgkisysifr = dataGridView1.Rows[secim].Cells[14].Value.ToString().Trim();
            string firmaid = dataGridView1.Rows[secim].Cells[16].Value.ToString().Trim();
            string aktifpasif = dataGridView1.Rows[secim].Cells[15].Value.ToString().Trim();
            string isySubeKod = dataGridView1.Rows[secim].Cells[16].Value.ToString().Trim();
            string subeNotu = dataGridView1.Rows[secim].Cells[18].Value.ToString().Trim();

            lblsubeid.Text = subid.ToString();
            txtsubeno.Text = subeno;
            txttamunvan.Text = firmaunvan;
            txtsubeunvan.Text = subeunvan;
            txtvd.Text = vd;
            txtvn.Text = vn;
            txtticsic.Text = ticsicil;
            txtisyerisicil.Text = sgkisyerino;
            rcbadres.Text = adres;
            txtil.Text = il;
            txtilce.Text = ilce;
            txtsgkkullanici.Text = sgkkullanici;
            txtek.Text = sgkek;
            txtsistem.Text = sgksistem;
            txtsgkisyeri.Text = sgkisysifr;
            txtisySubeKod.Text = isySubeKod;
            richFirmaNotu.Text = subeNotu;
            if (aktifpasif == "Pasif")
            {
                chkbxpasif.Checked = true;
            }
            else
            {
                chkbxpasif.Checked = false;
            }

            programreferans.firmaid =firmaid;
            programreferans.subid = subid;
            programreferans.firmaunvan = firmaunvan;
            programreferans.subeunvan = subeunvan;
            
            if (sgkisyerino.Length > 0)
            {
                programreferans.IsyeriSgkNo = sgkisyerino.Substring(13, 7);
            }

            baglan.Open();
            SQLiteCommand hizmetListesiSayi = new SQLiteCommand("Select Count(*) From HizmetListesi where firmaid='" + lblfirmano.Text + "' and subeid='" + lblsubeid.Text + "'", baglan);
            int yukluHizmetList = int.Parse(hizmetListesiSayi.ExecuteScalar().ToString());
            
            SQLiteCommand BordroSayisi = new SQLiteCommand("Select Count(*) From FirmaBordro where FirmaNo='" + lblfirmano.Text + "' and SubeNo='" + lblsubeid.Text + "'", baglan);
            int yukluBordroBilgisi = int.Parse(BordroSayisi.ExecuteScalar().ToString());
            baglan.Close();

            if (yukluHizmetList > 0)
            {
                lblYukluBordro.Text = "Bordro Listesi Yüklü \n '"+yukluBordroBilgisi+"' adet kayıt";
            }

            if (yukluHizmetList > 0)
            {
                lblHizmetListesi.Text = "Hizmet Listesi Yüklü \n '" + yukluHizmetList + "' adet kayıt";
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            int subeid = Convert.ToInt32(lblsubeid.Text);
            SQLiteCommand sil = new SQLiteCommand("Delete from sube_bilgileri where subeid= '" + subeid + "'", baglan);
            sil.ExecuteNonQuery();
            int firmaid = Convert.ToInt32(lblfirmano.Text);
            verilerigoster("Select * from sube_bilgileri where firmaid = '" + firmaid + "'");
            tabloyutemizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var driverPath = Application.StartupPath;
            //var chromeOptions = new ChromeOptions();
            //chromeOptions.AddUserProfilePreference("intl.accept_languages", "tr");
            //chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            //var v1driver1 = new ChromeDriver(driverPath, chromeOptions);


            //v1driver1.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/WPEB/amp/loginldap");



            // ebildirgeV1Guvenlik.v1GuvenlikImageUrl = v1driver1.Url;
            EbildV1Guvenlik frmGuvenlik = new EbildV1Guvenlik();
            frmGuvenlik.ShowDialog();
            string v = v1guvenliksozcugu.v1guvenlik.ToString();

            string klnc = txtsgkkullanici.Text.ToString().Trim();
            string ek = txtek.Text.ToString().Trim();
            string sistem = txtsistem.Text.ToString().Trim();
            string isyeri = txtsgkisyeri.Text.ToString().Trim();
            // string guvenlik = txtGuvenlik.Text.ToString().Trim();

            v1driver1 = v1driver.v1driver1;
            v1driver1.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/WPEB/amp/loginldap?j_username=" + klnc + "&isyeri_kod=" + ek + "&j_password=" + sistem + "&isyeri_sifre=" + isyeri + "&isyeri_guvenlik=" + v + "&btnSubmit=Giri%FE");


        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabloyutemizle();
        }

        public static void ebildv1()
        {
            //var driverPath = Application.StartupPath;
            //var chromeOptions = new ChromeOptions();
            //chromeOptions.AddUserProfilePreference("intl.accept_languages", "tr");
            //chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            //var driver = new ChromeDriver(driverPath, chromeOptions);
            //driver.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/WPEB/amp/loginldap");

            //var request = (driver.FindElement(By.XPath("//*[@id=\"formA\"]/table/tbody/tr[5]/td/table/tbody/tr[2]/td[2]/img")));

            //{
            ////    pictureBox1.Image = Bitmap.FromStream(stream);
            //}

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {



            ////var request = WebRequest.Create("https://ebildirge.sgk.gov.tr/WPEB/PG");
            //var request = (v1driver.FindElement(By.XPath("//*[@id=\"formA\"]/table/tbody/tr[5]/td/table/tbody/tr[2]/td[2]/img")));
            //using (var response = request.GetResponse())
            //using (var stream = response.GetResponseStream())
            //{
            //    pictureBox1.Image = Bitmap.FromStream(stream);
            //}
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInteraktif_Click(object sender, EventArgs e)
        {
            var driverPath = Application.StartupPath;
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "tr");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            var intvd = new ChromeDriver(driverPath, chromeOptions);


            intvd.Navigate().GoToUrl("https://ivd.gib.gov.tr");
            


            // intvd.FindElement(By.XPath("//*[@id='gen__1057']")).SendKeys(lblvdkullanici.Text.ToString());
            intvd.FindElement(By.Id("gen__1057")).SendKeys(lblvdkullanici.Text.Trim());
            //intvd.FindElement(By.XPath("//*[@id='gen__1058']")).SendKeys(lblvdsifre.Text.ToString());
            intvd.FindElement(By.Id("gen__1058")).SendKeys(lblvdkullanici.Text.Trim());
            //intvd.FindElement(By.Id("gen__1063")).SendKeys(txtVdguvenlik.Text.ToString());
            //intvd.FindElement(By.Id("gen__1067")).Click();


        }

        private void btnBilgiAl_Click(object sender, EventArgs e)
        {
            string isyerisicil = v1driver1.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td/table/tbody/tr[2]/td[2]/table[1]/tbody/tr/td[1]/table/tbody/tr[2]/td[3]/font")).Text;
            if (isyerisicil != "")
            {
                txtisyerisicil.Text = isyerisicil.Trim().Replace(" ", String.Empty);
                txttamunvan.Text = v1driver1.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td/table/tbody/tr[2]/td[2]/table[1]/tbody/tr/td[1]/table/tbody/tr[3]/td[3]")).Text;
                string adres = v1driver1.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td/table/tbody/tr[2]/td[2]/table[1]/tbody/tr/td[1]/table/tbody/tr[4]/td[3]")).Text;
                rcbadres.Text = adres;
                txtvn.Text = v1driver1.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td/table/tbody/tr[2]/td[2]/table[1]/tbody/tr/td[1]/table/tbody/tr[6]/td[3]/font/table/tbody/tr[1]/td[6]")).Text;
                txtvd.Text = v1driver1.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td/table/tbody/tr[2]/td[2]/table[1]/tbody/tr/td[1]/table/tbody/tr[6]/td[3]/font/table/tbody/tr[2]/td[3]")).Text;

                txtsubeunvan.Text = v1driver1.FindElement(By.XPath("/html/body/table[3]/tbody/tr/td/table/tbody/tr[2]/td[2]/table[1]/tbody/tr/td[1]/table/tbody/tr[3]/td[3]")).Text;
                v1driver1.FindElement(By.XPath("/html/body/table[2]/tbody/tr/td[2]/a[5]")).Click();

                MessageBox.Show("Firma Bilgileri Ebildirge V1'den alındı.\n Sistemden Çıkış Yapıldı");
            }
            else
            {
                MessageBox.Show("Sisteme Giriş Yapmadınız");
            }

        }

        private void btnebeyanname_Click(object sender, EventArgs e)
        {
            var driverPath = Application.StartupPath;
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "tr");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            var ebeyn = new ChromeDriver(driverPath, chromeOptions);


            ebeyn.Navigate().GoToUrl("https://ebeyanname.gib.gov.tr/giris.html");

            //*[@id="buton"]
            ebeyn.FindElement(By.XPath("//*[@id='buton']")).Click();
            ebeyn.SwitchTo().Window(ebeyn.WindowHandles.Last());
            ebeyn.FindElement(By.Name("username")).SendKeys(lblvdkullanici.Text.ToString());
            ebeyn.FindElement(By.Name("password2")).SendKeys(lblvdparola.Text.ToString());
            ebeyn.FindElement(By.Name("password1")).SendKeys(lblvdsifre.Text.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SQLiteCommand totalCountCommand = new SQLiteCommand("Select Count(*) From HizmetListesi where firmaid='" + lblfirmano.Text + "' and subeid='" + lblsubeid.Text + "'", baglan);
            int totalCount = int.Parse(totalCountCommand.ExecuteScalar().ToString());
            baglan.Close();


            if (totalCount>0)
            {
                if (lblfirmano.Text != "-" && lblsubeid.Text != "-")
                {
                    BordroYukle bordro = new BordroYukle();
                    bordro.Show();
                }
                else
                {
                    MessageBox.Show("Lütfen FİRMA ve ŞUBE seçimini yapınız");
                }
            }
            else
            {
                MessageBox.Show("Önce e-Bildirge Menüsünden APHB lerini indiriniz");
            }

        }
    }

}
