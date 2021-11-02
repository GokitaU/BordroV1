using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Data.SQLite;
using PdfOku;
using System.IO;

namespace Bordrolama10


{
    public partial class ebildirge : Form
    {
        public ebildirge()
        {
            InitializeComponent();
        }

        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);
        public IWebDriver driver { get; set; }

        public void verilerilistele(string veriler)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(veriler, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void tahakkuklarigoster(string tahakkuklar)
        {
            SQLiteDataAdapter daa = new SQLiteDataAdapter(tahakkuklar, baglan);
            DataSet dss = new DataSet();
            daa.Fill(dss);
            dataGridView2.DataSource = dss.Tables[0];
        }
        public void hizmetlistesinigoster(string hizmetlistesi)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(hizmetlistesi, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            dataGridView3.Columns["Ucret"].DefaultCellStyle.Format = "N2";
            dataGridView3.Columns["Ikramiye"].DefaultCellStyle.Format = "N2";
            dataGridView3.Columns["Ucret"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView3.Columns["Ikramiye"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView3.AutoResizeColumns();
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        public string sgkDonemSiraDuzenle()
        {

            DateTime dt = DateTime.Now;
            return dt.ToString("yyyy") + "/" + dt.ToString("MM");
        }

        private void ebildirge_Load(object sender, EventArgs e)
        {



            // SQL DEN COMBOBOXUN DOLDURULMASI -- SGK NIN SİTESİNDEN YAPILACAĞ İÇİN ALT KISIMDA YER ALMIŞTIR. 
            //baglan.Open();
            //SQLiteCommand Donem = new SQLiteCommand("select * from DonemBilgisi WHERE Donem <='" + sgkDonemSiraDuzenle() + "' ORDER BY Donem DESC", baglan);
            //SQLiteDataReader dnmdr = Donem.ExecuteReader();

            //int index = 1;
            //List<SgkDonemler> IlkDonemList = new List<SgkDonemler>();
            //List<SgkDonemler> SonDonemList = new List<SgkDonemler>();
            //while (dnmdr.Read())
            //{
            //    IlkDonemList.Add(new SgkDonemler { DisplayMember = dnmdr[3].ToString(), ValueMember = index });
            //    SonDonemList.Add(new SgkDonemler { DisplayMember = dnmdr[3].ToString(), ValueMember = index });
            //    index++;
            //}
            //baglan.Close();

            //cmbilk.DataSource = IlkDonemList;
            //cmbilk.DisplayMember = "DisplayMember";
            //cmbilk.ValueMember = "ValueMember";

            //cmbson.DataSource = SonDonemList;
            //cmbson.DisplayMember = "DisplayMember";
            //cmbson.ValueMember = "ValueMember";



            baglan.Open();
            SQLiteCommand combobx = new SQLiteCommand("select * From Hizli_Firma_Kayit", baglan);//  where aktifpasif like'Aktif'
            SQLiteDataReader dr = combobx.ExecuteReader();
            while (dr.Read())
            {
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
                lblfirmano.Text = (da[0].ToString().Trim());
                lblyetkiliadsoyad.Text = (da[5].ToString().Trim());
                lbltelefon.Text = (da[6].ToString().Trim());

            }
            da.Close();
            baglan.Close();
            int firmaid = Convert.ToInt32(lblfirmano.Text);
            verilerilistele("select subeid as ID,firmunvantam as FİRMA_UNVAN,subeunvan AS SUBE,sgkkullanici AS KULLANICI,sgkek AS EK,sgksistemsif AS SİSTEM_SİF,sgkisyerisif AS İSYERİ_SİF From sube_bilgileri where aktifpasif='Aktif' and firmaid='" + firmaid + "'");


        }


        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int secim = dataGridView1.SelectedCells[0].RowIndex;
            string subeid = dataGridView1.Rows[secim].Cells[0].Value.ToString().Trim();
            string sgkkullanici = dataGridView1.Rows[secim].Cells[3].Value.ToString().Trim();
            string sgkek = dataGridView1.Rows[secim].Cells[4].Value.ToString().Trim();
            string sgksistem = dataGridView1.Rows[secim].Cells[5].Value.ToString().Trim();
            string sgkisyeri = dataGridView1.Rows[secim].Cells[6].Value.ToString().Trim();

            lblsubeid.Text = subeid.Trim();
            lblsgkkullanici.Text = sgkkullanici.Trim();
            lblek.Text = sgkek.Trim();
            lblsistemsif.Text = sgksistem.Trim();
            lblisyerisif.Text = sgkisyeri.Trim();

            PdfOku.ReadPdf.firmid = Convert.ToInt32(lblfirmano.Text);

            PdfOku.ReadPdf.subid = Convert.ToInt32(lblsubeid.Text);
            if (lblsubeid.Text == "subeid")
            {

            }
            else
            {

                hizmetlistesinigoster("select Year as YIL,Month as AY, SgkNo as TCNO,Ad,Soyad,IlkSoyad,Ucret,Ikramiye,Gun,UCG,Eksik_Gun as Egun,GGun,CGun,Egs,Icn,Meslek_Kodu as MSLK_KOD,Kanun_No as Kanun,Belge_Cesidi as BÇşd, Belge_Turu as BTuru,Mahiyet from HizmetListesi Where subeid=" + subeid + "");
            }

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var request = WebRequest.Create("https://ebildirge.sgk.gov.tr/EBildirgeV2/PG");

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                pictureBox1.Image = Bitmap.FromStream(stream);
            }

        }

        public void ebildv2Baglan(Object sender, EventArgs e)
        {

        }

        private void btnthkkal_Click(object sender, EventArgs e)
        {

            FileInfo fileinfo;
            var pdfPath = Application.StartupPath + "Pdf\\";
            foreach (string item in Directory.GetFiles(pdfPath))
            {
                fileinfo = new FileInfo(item);
                if (fileinfo.Extension == ".pdf")
                {
                    fileinfo.Delete();
                }

            }

            driver.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/EBildirgeV2/tahakkuk/tahakkukonaylanmisTahakkukDonemSecildi.action?hizmet_yil_ay_index=" + cmbilk.SelectedValue.ToString() + "&hizmet_yil_ay_index_bitis=" + cmbson.SelectedValue.ToString() + "");



            var tahakkukadet = driver.FindElements(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr")).Count;

            //ilk önce veri tabanındaki ilgili şubeye kayıtlı olan daha önce indirilmiş tahakkuk bilgileri siliniyor
            baglan.Open();
            int subeid = Convert.ToInt32(lblsubeid.Text);
            SQLiteCommand thklarisil = new SQLiteCommand("delete from ilktahakkukbilgi where subeid='" + subeid + "' ", baglan);
            thklarisil.ExecuteNonQuery();
            baglan.Close();
            //tahakkuk bilgi datagirtview dolduruluyor 
            tahakkuklarigoster("select * from ilktahakkukbilgi where subeid='" + subeid + "'");

            //indirilen tahakkuklar için data set oluşturuloyr
            SQLiteCommand hizmetlistesinisil = new SQLiteCommand("delete from HizmetListesi where subeid='" + subeid + "' ", baglan);


            baglan.Open();
            for (int i = 3; i < tahakkukadet + 1; i++)
            {


                SQLiteCommand thklarial = new SQLiteCommand("INSERT INTO ilktahakkukbilgi (firmaid,subeid,thkkukdonem,hzmtdonem,blgtur,bmahiyet,bkanun,bcalisan,bgun,spek,pdfindurm) values (@firmaid,@subeid,@donmay,@hizmetay,@bturu,@bmahiyet,@kanunno,@calisan,@gun,@spk,@pdf)", baglan);

                IWebElement donemay = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[1]"));
                IWebElement hizmetay = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[2]"));
                IWebElement belgeturu = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[3]"));
                IWebElement belgemahiyeti = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[4]"));
                IWebElement kanunno = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[5]"));
                IWebElement calisan = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[6]"));
                IWebElement gun = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[7]"));
                IWebElement spek = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[8]"));

                IWebElement pdf = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[10]/div/a[2]"));


                thklarial.Parameters.AddWithValue("@firmaid", Convert.ToInt32(lblfirmano.Text.Trim()));
                thklarial.Parameters.AddWithValue("@subeid", Convert.ToInt32(lblsubeid.Text.Trim()));
                thklarial.Parameters.AddWithValue("@donmay", donemay.Text.ToString().Trim());
                thklarial.Parameters.AddWithValue("@hizmetay", (object)hizmetay.Text.ToString().Trim());
                thklarial.Parameters.AddWithValue("@bturu", (object)belgeturu.Text.ToString().Trim());
                thklarial.Parameters.AddWithValue("@bmahiyet", (object)belgemahiyeti.Text.ToString().Trim());
                thklarial.Parameters.AddWithValue("@kanunno", kanunno.Text.ToString().Trim());
                thklarial.Parameters.AddWithValue("@calisan", (object)calisan.Text.ToString().Trim());
                thklarial.Parameters.AddWithValue("@gun", (object)gun.Text.ToString().Trim());
                //string spekk = spek.ToString().Substring(0, spek.First().Text.ToString());
                var split = spek.Text.ToString().Trim().Split(' ');
                var tutar = Convert.ToDouble(split[0]);
                //tutar.First().Text.ToString().Split(' ')[0]);
                thklarial.Parameters.AddWithValue("@spk", tutar);//Convert.ToDecimal(spek.First().Text.ToString().Split(' ')[0]));
                thklarial.Parameters.AddWithValue("@pdf", (object)pdf.Text.ToString().Trim());

                if (pdf.Text == "")
                {
                    driver.Url = "https://ebildirge.sgk.gov.tr/EBildirgeV2/tahakkuk/tahakkukonaylanmisTahakkukDonemSecildi.action?hizmet_yil_ay_index=" + cmbilk.SelectedValue.ToString() + "&hizmet_yil_ay_index_bitis=" + cmbson.SelectedValue.ToString() + "";
                    pdf = driver.FindElement(By.XPath("//*[@id=\"contentContainer\"]/div/table/tbody/tr/td/table/tbody/tr/td/div/table/tbody/tr[" + i + "]/td[10]/div/a[2]"));
                    thklarial.Parameters.AddWithValue("@pdf", (object)pdf.Text.ToString().Trim());


                }

                pdf.Click();
                //if ((pdf.Text.ToString().Trim()) == "H")
                //{
                //    pdf.Click();
                //}
                //else
                //{
                //    thklarial.Parameters.AddWithValue("@pdf", donemay.Text.ToString().Trim() + " / " + kanunno.Text.ToString().Trim() + " indirme başarısız");
                //}

                thklarial.ExecuteNonQuery();
                //progresbar ı dolduruyoruz. 
                progressBar1.Maximum = tahakkukadet;
                progressBar1.Value = i;
            }

            tahakkuklarigoster("select id as ID, firmaid, subeid, thkkukdonem as DONEM, hzmtdonem as HZDONEM, blgtur as TÜR, bmahiyet as MAHİYET,bkanun as KANUN, bcalisan as ÇLŞN, bgun as GÜN, spek as SPEK, pdfindurm as İŞLEM FROM ilktahakkukbilgi");


            // HİZMET LİSTELERİ AÇILIYOR
            //baglan.Open();
            //int subeid = Convert.ToInt32(lblsubeid.Text);



            baglan.Close();
            ReadPdf pdfOku = new ReadPdf();
            pdfOku.DosyaOkumayaBasla();


            hizmetlistesinigoster("select Year as YIL,Month as AY, SgkNo as TCNO,Ad,Soyad,IlkSoyad,Ucret,Ikramiye,Gun,UCG,Eksik_Gun as Egun,GGun,CGun,Egs,Icn,Meslek_Kodu as MSLK_KOD,Kanun_No as Kanun,Belge_Cesidi as BÇşd, Belge_Turu as BTuru,Mahiyet from HizmetListesi Where subeid=" + subeid + "");
            baglan.Close();

        }




        private void tb6sgkisyeribilgi_Click(object sender, EventArgs e)
        {
            if (lblsubeid.Text == "subeid")
            {

            }
            else
            {
                int subeid = Convert.ToInt32(lblsubeid.Text.ToString());
                hizmetlistesinigoster("select Year as YIL,Month as AY, SgkNo as TCNO,Ad,Soyad,IlkSoyad,Ucret,Ikramiye,Gun,UCG,Eksik_Gun as Egun,GGun,CGun,Egs,Icn,Meslek_Kodu as MSLK_KOD,Kanun_No as Kanun,Belge_Cesidi as BÇşd, Belge_Turu as BTuru,Mahiyet from HizmetListesi Where subeid=" + subeid + "");
            }

        }

        private void btnaphlistele_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var pdfPath = Application.StartupPath + "Pdf\\";
            var driverPath = Application.StartupPath;
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddUserProfilePreference("download.default_directory", pdfPath);
            //11111111 -- CHROME BROWSER İN GİZLENMESİ İÇİN 
            //chromeOptions.AddArgument("headless");
            //chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
            //11111111
            chromeOptions.AddUserProfilePreference("intl.accept_languages", "tr");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            driver = new ChromeDriver(driverPath, chromeOptions);

            string v = txtebldv2guvenlik.Text.ToString().Trim();
            string klnc = lblsgkkullanici.Text.ToString().Trim();
            string ek = lblek.Text.ToString().Trim();
            string sistem = lblsistemsif.Text.ToString().Trim();
            string isyeri = lblisyerisif.Text.ToString().Trim();

            driver.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/EBildirgeV2/login/kullaniciIlkKontrollerGiris.action?username=" + klnc + "&isyeri_kod=" + ek + "&password=" + sistem + "&isyeri_sifre=" + isyeri + "&isyeri_guvenlik=" + v + "");
            //dönem bilgileri comboboxa alınıyor
            cmbilk.DataSource = null;
            cmbson.DataSource = null;

            driver.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/EBildirgeV2/tahakkuk/tahakkukonaylanmisTahakkukDonemBilgileriniYukle");
            int donemadet = driver.FindElements(By.XPath("//*[@id=\"tahakkukonaylanmisTahakkukDonemSecildi_hizmet_yil_ay_index\"]/option")).Count;

            List<SgkDonemler> IlkDonemList = new List<SgkDonemler>();
            List<SgkDonemler> SonDonemList = new List<SgkDonemler>();
            for (int i = 2; i < donemadet; i++)
            {
                IWebElement tahakkukDonem = driver.FindElement(By.XPath("//*[@id=\"tahakkukonaylanmisTahakkukDonemSecildi_hizmet_yil_ay_index\"]/option[" + i + "]"));
                IlkDonemList.Add(new SgkDonemler { DisplayMember = tahakkukDonem.Text.ToString().Trim(), ValueMember = i - 1 });
                SonDonemList.Add(new SgkDonemler { DisplayMember = tahakkukDonem.Text.ToString().Trim(), ValueMember = i - 1 });
            }
            //2019/01

            cmbilk.DataSource = IlkDonemList;
            cmbilk.DisplayMember = "DisplayMember";
            cmbilk.ValueMember = "ValueMember";

            cmbson.DataSource = SonDonemList;
            cmbson.DisplayMember = "DisplayMember";
            cmbson.ValueMember = "ValueMember";

            driver.Navigate().GoToUrl("https://ebildirge.sgk.gov.tr/EBildirgeV2/anasayfa");


        }


        private void tabPage1_Click(object sender, EventArgs e)
        {
            tahakkuklarigoster("select id as ID, firmaid, subeid, thkkukdonem as DONEM, hzmtdonem as HZDONEM, blgtur as TÜR, bmahiyet as MAHİYET,bkanun as KANUN, bcalisan as ÇLŞN, bgun as GÜN, spek as SPEK, pdfindurm as İŞLEM FROM ilktahakkukbilgi");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Close();
            ReadPdf pdfOku = new ReadPdf();
            pdfOku.DosyaOkumayaBasla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Tahmin tahmin = new Tahmin();
            tahmin.Show();
        }
    }

}
