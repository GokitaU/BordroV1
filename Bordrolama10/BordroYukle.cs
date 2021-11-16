using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Bordrolama10
{
    public partial class BordroYukle : Form
    {
        public BordroYukle()
        {
            InitializeComponent();
        }

        string command = "Select * From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo ='" + subeno + "'";
        int totalCount = 0;
        int currentRecord = 0;
        static int firmano = Convert.ToInt32(programreferans.firmaid);
        static int subeno = programreferans.subid;
        static string donem = "";
        string bordroCommand = "Select BordroSira AS Sıra, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,	CikisTarihi, Normal_Emekli as N_E,Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, MesaiGun as Mesai,HaftaSonu as HS,GenelTatil as GT,UcretsizIzin AS Ucrsz,SihhiIzin as Sıhhi, PrimGunu as PrmGun,FazlaMesaiGun as FmGun, AylikBrutUcret as BrutUcret,FmUcreti as FmUcrt, AylikEkOd as EkOdeme,ToplamKazanc, SgkMatrahi, SGkIsciPrim	as IsciPrim, IszlikIsciPrim as IszIsci, KumVergMatr,GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, SgkIsverenPrim as IsvPrim, IssizlikIsvPrim as IszIsv, BesKesintisi as BesKes, SairKesintiler as SairKes, AylikNetUcret as AylikNet, KanunNo From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo ='" + subeno + "' and PuantajDonem='" + donem + "'";
        public static int MyProperty { get; set; }
        public int MyProperty1 { get; set; }


        private void GetValues()
        {
            // SQLiteConnection baglan1 = new SqlConnection(Baglanti.Baglan1);
            baglan.Open();

            SQLiteDataAdapter da = new SQLiteDataAdapter(command, baglan);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("IsChanged", typeof(bool)));

            int showPageRowCount = int.Parse(txtShowRowCount.Text);
            int currentPage = int.Parse(txtCurrentPage.Text);
            // var pageCount = totalCount / showPageRowCount;

            da.Fill(currentRecord, showPageRowCount, table);
            currentRecord += showPageRowCount;



            dataGridView1.DataSource = table;

            baglan.Close();
        }

        //public void bordrolarigoster()
        //{
        //    //SQLiteDataAdapter da = new SQLiteDataAdapter(command, baglan);
        //    //DataTable table = new DataTable();

        //    //baglan.Open();
        //    //SQLiteCommand totalCountCommand = new SQLiteCommand("Select Count(*) From FirmaBordro where FirmaNo='" + firmano + "' and SubeNo='" + subeno + "'", baglan);
        //    //totalCount = int.Parse(totalCountCommand.ExecuteScalar().ToString());
        //    //baglan.Close();
        //    // command = bordro;
        //    // GetValues();

        //    //da.Fill(table);

        //    SQLiteDataAdapter da = new SQLiteDataAdapter(donem, baglan);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    dataGridView1.DataSource = ds.Tables[0];


        //}

        public void bordrolarigoster(string bordro)
        {

            SQLiteDataAdapter da = new SQLiteDataAdapter(bordro, baglan);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        public void donemlerigoster(string donem)
        {
            SQLiteDataAdapter daa = new SQLiteDataAdapter(donem, baglan);
            DataSet ds = new DataSet();
            daa.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void datagiritalanlariniduzenle()
        {

            dataGridView1.Columns["AylikBrutUcret"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["GunlukBrut"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["FmUcreti"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["AylikEkOd"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["ToplamKazanc"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["SgkMatrahi"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["SGkIsciPrim"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["IszlikIsciPrim"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["KumVergMatr"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["GvMatrahi"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["GelirVergisi"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["Agi"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["VergiInd"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["nbrtucret"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["DamgaVrg"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["SgkIsverenPrim"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["IssizlikIsvPrim"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["BesKesintisi"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["SairKesintiler"].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns["AylikNetUcret"].DefaultCellStyle.Format = "N2";


            dataGridView1.Columns["bdrId"].Visible = false;
            dataGridView1.Columns["BordroSira"].Visible = false;
            dataGridView1.Columns["PuantajYil"].Visible = false;
            dataGridView1.Columns["PuantajAy"].Visible = false;
            dataGridView1.Columns["FirmaNo"].Visible = false;
            dataGridView1.Columns["SubeNo"].Visible = false;
            dataGridView1.Columns["PersId"].Visible = false;
            dataGridView1.Columns["bdrId"].Visible = false;
            dataGridView1.Columns["FirmaPersId"].Visible = false;
            dataGridView1.Columns["Gv_Agi"].Visible = false;
            dataGridView1.Columns["AsgUcrGv"].Visible = false;
            dataGridView1.Columns["GunlukGv"].Visible = false;
            dataGridView1.Columns["AsgUcrDv"].Visible = false;
            dataGridView1.Columns["TerkinGv"].Visible = false;
            dataGridView1.Columns["TerkinDv"].Visible = false;

        }
        SQLiteConnection baglan = new SQLiteConnection(Baglanti.Baglan);
        CheckedListBox chktlistZorunluAlan = new CheckedListBox();
        bool load = false;
        int EksikAlanlar;
        int hataliAlanlar;
        private void button1_Click(object sender, System.EventArgs e)
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
                Excel.Application oXL = new Excel.Application();
                Excel.Workbook oWB = oXL.Workbooks.Open(file.FileName);

                List<string> liste = new List<string>();
                foreach (Excel.Worksheet oSheet in oWB.Worksheets)
                {
                    comboBox1.Items.Add(oSheet.Name);
                }
                oXL.Quit();
            }

        }



        DataTable Table = new DataTable();
        DataTable TemelTablo = new DataTable();
        DataTable yukluBordroTablo = new DataTable();
        DataTable tesvikliHizmetListesi = new DataTable();
        DataTable yillikGvListesi = new DataTable();
        DataTable TesvikHesBordrosu = new DataTable();
        DataTable TeknotesvikliHizmetListesi = new DataTable();

        private void TesvikHesBordro()
        {
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("Select BordroSira,FirmaNo,SubeNo,PuantajYil,PuantajAy, PuantajDonem, TcNo,PersAdı,PersSoyadı,GirisTarihi,	CikisTarihi, Normal_Emekli, Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, PrimGunu, AylikBrutUcret,FmUcreti, AylikEkOd,ToplamKazanc, SgkMatrahi, SGkIsciPrim, IszlikIsciPrim, KumVergMatr,GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, SgkIsverenPrim, IssizlikIsvPrim, Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, nbrtucret, BesKesintisi, SairKesintiler, AylikNetUcret, KanunNo,FirmaPersId From FirmaBordro", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(TesvikHesBordrosu);
            }
            baglan.Close();
        }

        private void tesvikliListe()
        {
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("Select * From HizmetListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "' and (Kanun_No='00687' or Kanun_No='01687' or Kanun_No='17103' or Kanun_No='27103' or Kanun_No like '%5746')", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(tesvikliHizmetListesi);
            }
            baglan.Close();
        }
        private void TeknotesvikliListe()
        {
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("Select * From HizmetListesi where firmaid = '" + programreferans.firmaid + "' and subeid='" + programreferans.subid + "'", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(TeknotesvikliHizmetListesi);
            }
            baglan.Close();
        }

        private void yillikGvListe()
        {
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("Select * From agi_tablosu", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(yillikGvListesi);
            }
            baglan.Close();
        }
        private void yuklubordro()
        {
            if (dataGridView2.Rows.Count > 0)
            {
                donem = dataGridView2.Rows[0].Cells[0].Value.ToString();
            }
            else
            {
                donem = "'%'";
            }
            baglan.Open();
            using (SQLiteCommand sorgu = new SQLiteCommand("Select * From FirmaBordro where FirmaNo = '" + programreferans.firmaid + "' and SubeNo='" + programreferans.subid + "' and PuantajDonem = '" + donem + "'", baglan))
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                da.SelectCommand = sorgu;
                da.Fill(yukluBordroTablo);
            }
            baglan.Close();
        }



        private void btnOku_Click(object sender, EventArgs e)
        {
            TemelTablo.Clear();
            TemelTablo.Columns.Clear();



            if (txtdosyayolu.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen Dosya Yolu veya Yükleme Yapılacak Sayfa Seçimini Yapınız");
            }

            yuklubordro();

            if (yukluBordroTablo.Rows.Count > 0)
            {
                MessageBox.Show("DİKKAT;" + programreferans.firmaunvan + " /" + programreferans.subeunvan + " Şubesine ait \n daha önceden yüklenmiş Bordro bulunmaktadır. \n Yeni Bir Yükleme Yapacaksanız önceki bordro bilgilerini siliniz");
            }
            else
            {
                List<DataColumn> dataColumns = new List<DataColumn>();
                if (dataColumns.Count > 0)
                {
                    dataColumns.Clear();
                }
                foreach (var item in chktlistZorunluAlan.Items)
                {
                    dataColumns.Add(new DataColumn { ColumnName = item.ToString() });
                }

                TemelTablo.Columns.AddRange(dataColumns.ToArray());

                if (txtdosyayolu.Text.Trim().Length > 0)
                {
                    using (OleDbConnection con = new OleDbConnection())
                    {

                        con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = '" + txtdosyayolu.Text.Trim().ToString() + "';Extended Properties = 'Excel 8.0;HDR=YES'";
                        con.Open();
                        OleDbCommand sorgu = new OleDbCommand("Select * From [" + comboBox1.Text.ToString() + "$]", con);
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = sorgu;

                        da.Fill(TemelTablo);
                        //Table = ds.Tables[0];
                        Basliklaruygunmu();
                        ZorunluAlantammi();

                        if (EksikAlanlar == 0 && hataliAlanlar == 0)
                        {
                            dataGridView1.DataSource = TemelTablo;
                        }
                        else
                        {

                            MessageBox.Show("Eksik veya Hatalı alanları düzelttikten sonra tekrar deneyiniz.");
                        }
                    }
                }
            }

            BordroVeritabaninaYaz();

        }
        private void zorunluAlanUyari()
        {
            lblbaslik.Text = "2. işlem Excel Zorunlu Alanlar Kontrol ediliyor...";
        }

        private void ZorunluAlantammi()
        {
            string sutunadi;
            string chksutunadi;
            var eksikExcelAlanlari = new List<string>();
            bool eksik = false;
            zorunluAlanUyari();
            progressBar1.Maximum = chktlistZorunluAlan.CheckedItems.Count;
            progressBar2.Maximum = TemelTablo.Columns.Count;
            for (int i = 0; i < chktlistZorunluAlan.CheckedItems.Count; i++)
            {
                progressBar1.Value = i;
                chksutunadi = chktlistZorunluAlan.CheckedItems[i].ToString();
                for (int j = 0; j < TemelTablo.Columns.Count; j++)
                {
                    progressBar2.Value = j;
                    sutunadi = TemelTablo.Columns[j].Caption;
                    if (chksutunadi == sutunadi)
                    {
                        eksik = false;
                        break;
                    }
                    else
                    {
                        eksik = true;
                    }
                }
                if (eksik)
                {
                    eksikExcelAlanlari.Add(chksutunadi);
                    eksik = false;
                    chksutunadi = "";
                }
            }
            EksikAlanlar = eksikExcelAlanlari.Count;
            if (EksikAlanlar > 0)
            {
                var builder = new StringBuilder();
                for (int i = 0; i < EksikAlanlar; i++)
                {
                    builder.Append($"{eksikExcelAlanlari[i]}");
                    if (i + 1 < EksikAlanlar)
                    {
                        builder.Append($", ");
                    }
                }
                MessageBox.Show("İlgili Bordroda Yüklemek Zorunda Olduğunuz Alanlar =  " + builder.ToString() + "\n ilgili alanlar olmadan bordro hesaplama işlemi yapamazsınız \n   LÜTFEN EKSİK ALANLARI TAMAMLAYARAK DEVAM EDİNİZ");
            }

        }
        private void basliklarUygunmuUyari()
        {
            lblbaslik.Text = "1. işlem Excel İçin Başlık Kontrolü Yapılıyor .... ";
        }
        private void Basliklaruygunmu()
        {

            string sutunadi;
            string chksutunadi;
            var hataliExcelAlanlari = new List<string>();
            bool hatali = false;

            basliklarUygunmuUyari();
            progressBar1.Maximum = TemelTablo.Rows.Count;
            progressBar2.Maximum = chktlistZorunluAlan.Items.Count;

            for (int i = 0; i < TemelTablo.Columns.Count; i++)
            {
                progressBar1.Value = i;

                sutunadi = TemelTablo.Columns[i].Caption;
                for (int j = 0; j < chktlistZorunluAlan.Items.Count; j++)
                {
                    progressBar2.Value = j;
                    chksutunadi = chktlistZorunluAlan.Items[j].ToString();
                    if (sutunadi == chksutunadi)
                    {
                        hatali = false;
                        break;
                    }
                    else
                        hatali = true;
                }

                if (hatali)
                {
                    hataliExcelAlanlari.Add(sutunadi);
                    hatali = false;
                    sutunadi = "";

                }

            }
            hataliAlanlar = hataliExcelAlanlari.Count;
            if (hataliAlanlar > 0)
            {
                var builder = new StringBuilder();
                for (int i = 0; i < hataliAlanlar; i++)
                {
                    builder.Append($"{hataliExcelAlanlari[i]}");

                    if (i + 1 < hataliAlanlar)
                        builder.Append($", ");
                }


                MessageBox.Show("Yüklemeye çalıştığınız Excell Sutunlarında \n Uyumsuz Alanlar Bulunmaktadır. \n Uyumsuz Alanlar= " + builder.ToString() + "LÜTFEN BAŞLIKLARI DÜZELTİNİZ.");

            }

        }



        private void Bordro_Load(object sender, EventArgs e)
        {



            // dataGridView1.AutoGenerateColumns = false;// datagrit mükerrer başlık gelmesine engeller
            lblfirmano.Text = programreferans.firmaid;
            lblfirma.Text = programreferans.firmaunvan;
            lblsubeno.Text = programreferans.subid.ToString();
            lblsube.Text = programreferans.subeunvan;
            lblsgkisyerino.Text = programreferans.IsyeriSgkNo.ToString();

            chktlistZorunluAlan.Items.Add("BdrId");
            chktlistZorunluAlan.Items.Add("BordroSira");
            chktlistZorunluAlan.Items.Add("FirmaNo");
            chktlistZorunluAlan.Items.Add("SubeNo");
            chktlistZorunluAlan.Items.Add("PersId");
            chktlistZorunluAlan.Items.Add("PuantajYil", true);
            chktlistZorunluAlan.Items.Add("PuantajAy", true);
            chktlistZorunluAlan.Items.Add("PuantajDonem");
            chktlistZorunluAlan.Items.Add("TcNo", true);
            chktlistZorunluAlan.Items.Add("SgkNo");
            chktlistZorunluAlan.Items.Add("PersAdı", true);
            chktlistZorunluAlan.Items.Add("PersSoyadı", true);
            chktlistZorunluAlan.Items.Add("PersAdıSoyadı");
            chktlistZorunluAlan.Items.Add("GirisTarihi", true);
            chktlistZorunluAlan.Items.Add("CikisTarihi", true);
            chktlistZorunluAlan.Items.Add("Normal_Emekli");
            chktlistZorunluAlan.Items.Add("Net_Brüt", true);
            chktlistZorunluAlan.Items.Add("Net_BrtUcret");
            chktlistZorunluAlan.Items.Add("MesaiGun");
            chktlistZorunluAlan.Items.Add("HaftaSonu");
            chktlistZorunluAlan.Items.Add("GenelTatil");
            chktlistZorunluAlan.Items.Add("UcretsizIzin");
            chktlistZorunluAlan.Items.Add("SihhiIzin");
            chktlistZorunluAlan.Items.Add("PrimGunu", true);
            chktlistZorunluAlan.Items.Add("FazlaMesaiGun");
            chktlistZorunluAlan.Items.Add("AylikBrutUcret", true);
            chktlistZorunluAlan.Items.Add("GunlukBrut");
            chktlistZorunluAlan.Items.Add("FmUcreti");
            chktlistZorunluAlan.Items.Add("AylikEkOd");
            chktlistZorunluAlan.Items.Add("ToplamKazanc");
            chktlistZorunluAlan.Items.Add("SgkMatrahi", true);
            chktlistZorunluAlan.Items.Add("SGkIsciPrim", true);
            chktlistZorunluAlan.Items.Add("IszlikIsciPrim", true);
            chktlistZorunluAlan.Items.Add("KumVergMatr", true);
            chktlistZorunluAlan.Items.Add("GvMatrahi", true);
            chktlistZorunluAlan.Items.Add("GelirVergisi", true);
            chktlistZorunluAlan.Items.Add("Agi", true);
            chktlistZorunluAlan.Items.Add("VergiInd");

            chktlistZorunluAlan.Items.Add("DamgaVrg", true);
            chktlistZorunluAlan.Items.Add("SgkIsverenPrim");
            chktlistZorunluAlan.Items.Add("IssizlikIsvPrim");
            chktlistZorunluAlan.Items.Add("BesKesintisi");
            chktlistZorunluAlan.Items.Add("SairKesintiler");
            chktlistZorunluAlan.Items.Add("AylikNetUcret", true);



            //// dataGridView1.Columns.Add(new DataGridViewColumn { Name = "BdrId", CellTemplate = new DataGridViewTextBoxCell() });// datagrite başlık ekler
            //int firmano = Convert.ToInt32(programreferans.firmaid);
            //int subeno = programreferans.subid;



            //load = true;

            //donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");
            //if (dataGridView2.Rows.Count>0)
            //{
            //    var secim = dataGridView2.SelectedCells[0].RowIndex;
            //    donem = dataGridView2.Rows[secim].Cells[0].Value.ToString();
            //}
            //else
            //{
            //    donem = "";
            //}


            //bordrolarigoster("Select * From FirmaBordro where FirmaNo='" + programreferans.firmaid + "' and SubeNo='" + programreferans.subid + "' and PuantajDonem='" + donem + "'");
            //datagiritalanlariniduzenle();
            //    txtTotalPage.Text = Math.Round(double.Parse(totalCount.ToString()) / double.Parse(txtShowRowCount.Text), MidpointRounding.AwayFromZero).ToString();

            //    if (totalCount > 0)
            //    {
            //        txtCurrentRow.Text = "1";
            //        txtTotalRow.Text = totalCount.ToString();
            //    }
            //    //  datagiritalanlariniduzenle();
        }

        //private void btnKaydet_Click(object sender, EventArgs e)
        private void BordroVeritabaninaYaz()
        {

            yillikGvListe();
            tesvikliListe();

            progressBar1.Maximum = dataGridView1.Rows.Count;
            lblbaslik.Text = "Veri Tabanına Kayıt İşlemi Başladı";


            baglan.Open();
            SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [FirmaBordro](BordroSira,FirmaNo,SubeNo,PersId,PuantajYil,PuantajAy,PuantajDonem, TcNo, SgkNo, PersAdı, PersSoyadı, PersAdıSoyadı, GirisTarihi,CikisTarihi, Normal_Emekli, Net_Brüt,Net_BrtUcret, MesaiGun, HaftaSonu, GenelTatil,UcretsizIzin,SihhiIzin, PrimGunu, FazlaMesaiGun, AylikBrutUcret, GunlukBrut, FmUcreti, AylikEkOd,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,VergiInd,DamgaVrg,SgkIsverenPrim,IssizlikIsvPrim,BesKesintisi,SairKesintiler,AylikNetUcret,KanunNo,Gv_Agi,AsgUcrGv,GunlukGv,AsgUcrDv,TerkinGv,TerkinDv,FirmaPersId) values (@bdrsira,@fno,@sno, @pid,@pyil,@pay,@pdnm,@tc,@sgkno,@padi,@psydi,@padisoyadi,@gtarih,@ctarih,@nrm,@ntbrt,@nbrtucret,@mesai,@hsonu, @gtatil,@uizin,@sihhi,@pun,@fmgun,@brtuc,@gnluk,@fmucr,@ekod,@tkznc, @sgkmat, @sgkisci, @iszisci, @kumgvmt, @gvmt, @gv,@agi,@vind,@dv,@sgkisv,@iszisv,@beskes,@sairkes,@netucr,@kanun,@gvAgi,@auGv,@auGvgun,@auDv,@gvTerkin,@dvTerkin,@frmPerId)", baglan);





            int firmano = Convert.ToInt32(lblfirmano.Text);
            int subeno = Convert.ToInt32(lblsubeno.Text);


            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string ay;
                if ((dataGridView1.Rows[i].Cells["PuantajAy"].Value.ToString()).Length == 1)
                {
                    ay = "0" + dataGridView1.Rows[i].Cells["PuantajAy"].Value.ToString();
                }
                else
                {
                    ay = dataGridView1.Rows[i].Cells["PuantajAy"].Value.ToString();
                }

                string tcno = dataGridView1.Rows[i].Cells["TcNo"].Value.ToString();
                string yil = dataGridView1.Rows[i].Cells["PuantajYil"].Value.ToString();
                string donem = yil + "/" + ay;
                string pid = yil + ay + tcno;
                string firmPersid = Convert.ToString(firmano) + Convert.ToString(subeno) + pid;
                var bordroSira = dataGridView1.Rows[i].Cells["BordroSira"].Value.ToString();
                var sgkNo = dataGridView1.Rows[i].Cells["SgkNo"].Value.ToString();
                var persadi = dataGridView1.Rows[i].Cells["PersAdı"].Value.ToString();
                var persoyadi = dataGridView1.Rows[i].Cells["PersSoyadı"].Value.ToString();
                var persadisoyadi = dataGridView1.Rows[i].Cells["PersAdıSoyadı"].Value.ToString();
                var giristarih = dataGridView1.Rows[i].Cells["GirisTarihi"].Value != DBNull.Value ? String.Format("{0:dd/MM/yyyy}", dataGridView1.Rows[i].Cells["GirisTarihi"].Value.ToString()) : "";
                var cikistarih = dataGridView1.Rows[i].Cells["CikisTarihi"].Value != DBNull.Value ? String.Format("{0:dd/MM/yyyy}", dataGridView1.Rows[i].Cells["CikisTarihi"].Value.ToString()) : "";
                var normalemekli = dataGridView1.Rows[i].Cells["Normal_Emekli"].Value.ToString();
                var netbrüt = dataGridView1.Rows[i].Cells["Net_Brüt"].Value.ToString();

                var mesaigun = dataGridView1.Rows[i].Cells["MesaiGun"].Value.ToString();
                var haftasonu = dataGridView1.Rows[i].Cells["HaftaSonu"].Value.ToString();
                var geneltatil = dataGridView1.Rows[i].Cells["GenelTatil"].Value.ToString();
                var ucretsizizin = dataGridView1.Rows[i].Cells["UcretsizIzin"].Value.ToString();
                var sihhiizin = dataGridView1.Rows[i].Cells["SihhiIzin"].Value.ToString();
                var primgun = dataGridView1.Rows[i].Cells["PrimGunu"].Value.ToString();
                var fmgun = dataGridView1.Rows[i].Cells["FazlaMesaiGun"].Value.ToString();
                var nbrtucret = dataGridView1.Rows[i].Cells["N_B_Ucret"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["N_B_Ucret"].Value) : 0;

                var aylbrutucr = dataGridView1.Rows[i].Cells["AylikBrutUcret"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["AylikBrutUcret"].Value) : 0;
                var gunlukucr = dataGridView1.Rows[i].Cells["GunlukBrut"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["GunlukBrut"].Value) : 0;
                var fmucreti = dataGridView1.Rows[i].Cells["FmUcreti"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["FmUcreti"].Value) : 0;
                var aylikekod = dataGridView1.Rows[i].Cells["AylikEkOd"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["AylikEkOd"].Value) : 0;
                var tomlamkazanc = dataGridView1.Rows[i].Cells["ToplamKazanc"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["ToplamKazanc"].Value) : 0;
                var sgkmatrah = dataGridView1.Rows[i].Cells["SgkMatrahi"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["SgkMatrahi"].Value) : 0;
                var sgkisciprim = dataGridView1.Rows[i].Cells["SGkIsciPrim"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["SGkIsciPrim"].Value) : 0;
                var issizlikisci = dataGridView1.Rows[i].Cells["IszlikIsciPrim"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["IszlikIsciPrim"].Value) : 0;
                var kumvargimat = dataGridView1.Rows[i].Cells["KumVergMatr"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["KumVergMatr"].Value) : 0;
                var gvmatrahi = dataGridView1.Rows[i].Cells["GvMatrahi"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["GvMatrahi"].Value) : 0;
                var gv = dataGridView1.Rows[i].Cells["GelirVergisi"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["GelirVergisi"].Value) : 0;
                var agi = dataGridView1.Rows[i].Cells["Agi"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["Agi"].Value) : 0;
                var vergiind = dataGridView1.Rows[i].Cells["VergiInd"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["VergiInd"].Value) : 0;

                var dv = dataGridView1.Rows[i].Cells["DamgaVrg"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["DamgaVrg"].Value) : 0;
                var sgkisveren = dataGridView1.Rows[i].Cells["SgkIsverenPrim"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["SgkIsverenPrim"].Value) : 0;
                var issizlikIsveren = dataGridView1.Rows[i].Cells["IssizlikIsvPrim"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["IssizlikIsvPrim"].Value) : 0;
                var beskes = dataGridView1.Rows[i].Cells["BesKesintisi"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["BesKesintisi"].Value) : 0;
                var sairkes = dataGridView1.Rows[i].Cells["SairKesintiler"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["SairKesintiler"].Value) : 0;
                var ayliknet = dataGridView1.Rows[i].Cells["AylikNetUcret"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["AylikNetUcret"].Value) : 0;

                var tesviklipersonel = tesvikliHizmetListesi.Select("firmPersid='" + firmPersid + "' and (Kanun_No='00687' or Kanun_No='01687' or Kanun_No='17103' or Kanun_No='027103')", "Kanun_No");
                string kanunno = "";
                decimal asUcrGv = 0;
                decimal asUcrDv = 0;
                decimal gvTerkin = 0;
                decimal dvTerkin = 0;
                decimal asUcrGvGun = 0;
                decimal asUcrDvGun = 0;

                foreach (var item in tesviklipersonel)
                {
                    kanunno = item["Kanun_No"].ToString();
                }


                if (kanunno != "")
                {
                    var ilgiliYilAsUcrGv = yillikGvListesi.Select("agi_yil='" + yil + "'");
                    foreach (var item in ilgiliYilAsUcrGv)
                    {
                        asUcrGv = Convert.ToDecimal(item["asgariucr_gv"]);
                        asUcrDv = Convert.ToDecimal(item["asgariucr_dv"]);
                        asUcrGvGun = asUcrGv / 30;
                        asUcrDvGun = asUcrDv / 30;
                    }
                    if (gv == 0)
                    {
                        gvTerkin = 0;
                    }
                    if (gv > (asUcrGvGun * Convert.ToInt32(primgun)) && gv > agi)
                    {
                        gvTerkin = decimal.Round((asUcrGvGun * Convert.ToInt32(primgun)) - agi, 2);
                    }
                    if (gv < (asUcrGvGun * Convert.ToInt32(primgun)) && gv > agi)
                    {
                        gvTerkin = decimal.Round((gv - agi), 2);
                    }

                    if (dv == 0)
                    {
                        dvTerkin = 0;
                    }
                    if (dv > (asUcrDvGun * Convert.ToInt32(primgun)))
                    {
                        dvTerkin = decimal.Round((asUcrDvGun * Convert.ToInt32(primgun)), 2);
                    }
                    if (dv < (asUcrDvGun * Convert.ToInt32(primgun)))
                    {
                        dvTerkin = decimal.Round((dv), 2);
                    }
                }
                else
                {
                    kanunno = null;
                }


                ekle.Parameters.AddWithValue("@bdrsira", bordroSira);
                ekle.Parameters.AddWithValue("@fno", firmano);
                ekle.Parameters.AddWithValue("@sno", subeno);
                ekle.Parameters.AddWithValue("@pid", pid);
                ekle.Parameters.AddWithValue("@pyil", yil);
                ekle.Parameters.AddWithValue("@pay", ay);
                ekle.Parameters.AddWithValue("@pdnm", donem);
                ekle.Parameters.AddWithValue("@tc", tcno);
                ekle.Parameters.AddWithValue("@sgkno", sgkNo);
                ekle.Parameters.AddWithValue("@padi", persadi);
                ekle.Parameters.AddWithValue("@psydi", persoyadi);
                ekle.Parameters.AddWithValue("@padisoyadi", persadisoyadi);
                ekle.Parameters.AddWithValue("@gtarih", giristarih);
                ekle.Parameters.AddWithValue("@ctarih", cikistarih);
                ekle.Parameters.AddWithValue("@nrm", normalemekli);
                ekle.Parameters.AddWithValue("@ntbrt", netbrüt);
                ekle.Parameters.AddWithValue("@dvmt", nbrtucret);
                ekle.Parameters.AddWithValue("@mesai", mesaigun);
                ekle.Parameters.AddWithValue("@hsonu", haftasonu);
                ekle.Parameters.AddWithValue("@gtatil", geneltatil);
                ekle.Parameters.AddWithValue("@uizin", ucretsizizin);
                ekle.Parameters.AddWithValue("@sihhi", sihhiizin);
                ekle.Parameters.AddWithValue("@pun", primgun);
                ekle.Parameters.AddWithValue("@fmgun", fmgun);
                ekle.Parameters.AddWithValue("@brtuc", aylbrutucr);
                ekle.Parameters.AddWithValue("@gnluk", gunlukucr);
                ekle.Parameters.AddWithValue("@fmucr", fmucreti);
                ekle.Parameters.AddWithValue("@ekod", aylikekod);
                ekle.Parameters.AddWithValue("@tkznc", tomlamkazanc);
                ekle.Parameters.AddWithValue("@sgkmat", sgkmatrah);
                ekle.Parameters.AddWithValue("@sgkisci", sgkisciprim);
                ekle.Parameters.AddWithValue("@iszisci", issizlikisci);
                ekle.Parameters.AddWithValue("@kumgvmt", kumvargimat);
                ekle.Parameters.AddWithValue("@gvmt", gvmatrahi);
                ekle.Parameters.AddWithValue("@gv", gv);
                ekle.Parameters.AddWithValue("@agi", agi);
                ekle.Parameters.AddWithValue("@vind", vergiind);

                ekle.Parameters.AddWithValue("@dv", dv);
                ekle.Parameters.AddWithValue("@sgkisv", sgkisveren);
                ekle.Parameters.AddWithValue("@iszisv", issizlikIsveren);
                ekle.Parameters.AddWithValue("@beskes", beskes);
                ekle.Parameters.AddWithValue("@sairkes", sairkes);
                ekle.Parameters.AddWithValue("@netucr", ayliknet);

                ekle.Parameters.AddWithValue("@kanun", kanunno);
                ekle.Parameters.AddWithValue("@gvAgi", gv - agi);
                ekle.Parameters.AddWithValue("@auGv", asUcrGv);
                ekle.Parameters.AddWithValue("@auGvgun", asUcrGvGun);
                ekle.Parameters.AddWithValue("@auDv", asUcrDv);
                ekle.Parameters.AddWithValue("@gvTerkin", gvTerkin);
                ekle.Parameters.AddWithValue("@dvTerkin", dvTerkin);

                ekle.Parameters.AddWithValue("@frmPerId", firmPersid);

                ekle.ExecuteNonQuery();

                progressBar1.Value = i;

            }

            baglan.Close();
            MessageBox.Show("Veriler Veritabanına başarı ile kaydedildi");

            donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");
            //datagiritalanlariniduzenle();

            IslemDurumu.islemdurumu = "Tamamlandı";
        }


        CheckState Current;
        private void chktlistlistelenecek_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!load) return;

            Current = e.CurrentValue;
            var index = e.Index;
            if (chktlistZorunluAlan.CheckedItems[index].ToString() != "")
                e.NewValue = Current;

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            baglan.Dispose();
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("İlgili Şubeye ait Tüm Bordro Bilgisi Silinecektir", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                baglan.Open();

                SQLiteCommand komut = new SQLiteCommand("Delete from FirmaBordro where FirmaNo ='" + programreferans.firmaid + "' and SubeNo='" + programreferans.subid + "'", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();

                MessageBox.Show("Tüm Kayıtlar Silindi");

                donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");
                baglan.Close();
                bordrolarigoster("Select BordroSira AS Sıra, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,	CikisTarihi, Normal_Emekli as N_E,Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, MesaiGun as Mesai,HaftaSonu as HS,GenelTatil as GT,UcretsizIzin AS Ucrsz,SihhiIzin as Sıhhi, PrimGunu as PrmGun,FazlaMesaiGun as FmGun, AylikBrutUcret as BrutUcret,FmUcreti as FmUcrt, AylikEkOd as EkOdeme,ToplamKazanc, SgkMatrahi, SGkIsciPrim	as IsciPrim, IszlikIsciPrim as IszIsci, KumVergMatr,GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, SgkIsverenPrim as IsvPrim, IssizlikIsvPrim as IszIsv, BesKesintisi as BesKes, SairKesintiler as SairKes, AylikNetUcret as AylikNet, KanunNo From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo ='" + subeno + "' and PuantajDonem='" + donem + "'");

            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {

            int secim = dataGridView2.SelectedCells[0].RowIndex;
            donem = dataGridView2.Rows[secim].Cells[0].Value.ToString();
            bordrolarigoster("Select BordroSira AS Sıra, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,	CikisTarihi, Normal_Emekli as N_E,Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, MesaiGun as Mesai,HaftaSonu as HS,GenelTatil as GT,UcretsizIzin AS Ucrsz,SihhiIzin as Sıhhi, PrimGunu as PrmGun,FazlaMesaiGun as FmGun, AylikBrutUcret as BrutUcret,FmUcreti as FmUcrt, AylikEkOd as EkOdeme,ToplamKazanc, SgkMatrahi, SGkIsciPrim	as IsciPrim, IszlikIsciPrim as IszIsci, KumVergMatr,GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, SgkIsverenPrim as IsvPrim, IssizlikIsvPrim as IszIsv, BesKesintisi as BesKes, SairKesintiler as SairKes, AylikNetUcret as AylikNet, KanunNo From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo ='" + subeno + "' and PuantajDonem='" + donem + "'");

            //baglan.Open();
            //SQLiteCommand totalCountCommand = new SQLiteCommand("Select Count(*) From FirmaBordro where FirmaNo='" + programreferans.firmaid + "' and SubeNo='" + programreferans.subid + "'and PuantajDonem = '" + donem + "'", baglan);
            //totalCount = int.Parse(totalCountCommand.ExecuteScalar().ToString());
            //baglan.Close();



            //datagiritalanlariniduzenle();
            // GetValues();
        }

        private void btnfiltrekaldir_Click(object sender, EventArgs e)
        {
            bordrolarigoster("Select BordroSira AS Sıra, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,	CikisTarihi, Normal_Emekli as N_E,Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, MesaiGun as Mesai,HaftaSonu as HS,GenelTatil as GT,UcretsizIzin AS Ucrsz,SihhiIzin as Sıhhi, PrimGunu as PrmGun,FazlaMesaiGun as FmGun, AylikBrutUcret as BrutUcret,FmUcreti as FmUcrt, AylikEkOd as EkOdeme,ToplamKazanc, SgkMatrahi, SGkIsciPrim	as IsciPrim, IszlikIsciPrim as IszIsci, KumVergMatr,GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, SgkIsverenPrim as IsvPrim, IssizlikIsvPrim as IszIsv, BesKesintisi as BesKes, SairKesintiler as SairKes, AylikNetUcret as AylikNet, KanunNo From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo ='" + subeno + "' and PuantajDonem='" + donem + "'");
            // datagiritalanlariniduzenle();
        }

        private void btnArgeHesapla_Click(object sender, EventArgs e)
        {

            string hzmtListPersid = "";
            string hzmKanunNo = "";

            string yil = "";
            decimal asgucrGv = 0;
            decimal asgucrDv = 0;
            decimal asgUcrGvGunluk = 0;
            decimal asgUcrDvGunluk = 0;
            decimal bdrGv = 0;
            decimal bdrDv = 0;
            decimal agi = 0;
            decimal terkinGv = 0;
            decimal terkinDv = 0;
            int gun = 0;


            tesvikliListe();
            yillikGvListe();

            progressBar1.Maximum = tesvikliHizmetListesi.Rows.Count;

            for (int j = 0; j < tesvikliHizmetListesi.Rows.Count; j++)
            {

                var hizmListTesvPersid = tesvikliHizmetListesi.Rows[j]["firmPersid"].ToString();
                hzmKanunNo = tesvikliHizmetListesi.Rows[j]["Kanun_No"].ToString();


                baglan.Open();
                int gritSatirSayisi = dataGridView1.Rows.Count;

                for (int i = 0; i < gritSatirSayisi - 1; i++)
                {
                    string dtgritPersid = programreferans.firmaid.ToString() + programreferans.subid.ToString() + dataGridView1.Rows[i].Cells["PersId"].Value;
                    string agiyil = dataGridView1.Rows[i].Cells["PuantajYil"].Value.ToString();


                    if (dtgritPersid == hizmListTesvPersid)
                    {
                        if (hzmKanunNo == "00687" || hzmKanunNo == "01687" || hzmKanunNo == "27103" || hzmKanunNo == "17103")
                        {


                            using (SQLiteCommand gvlistesi = new SQLiteCommand("select * From agi_tablosu where agi_yil = '" + agiyil + "' ", baglan))
                            {
                                using (SQLiteDataReader gvreader = gvlistesi.ExecuteReader())
                                {
                                    while (gvreader.Read())
                                    {
                                        yil = gvreader["agi_yil"].ToString();
                                        asgucrGv = Convert.ToDecimal(gvreader["asgariucr_gv"]);
                                        asgucrDv = Convert.ToDecimal(gvreader["asgariucr_dv"]);
                                        asgUcrGvGunluk = asgucrGv / 30;
                                        asgUcrDvGunluk = asgucrDv / 30;
                                    }
                                }

                            }
                            dataGridView1.Rows[i].Cells["KanunNo"].Value = hzmKanunNo;
                            bdrGv = Convert.ToDecimal(dataGridView1.Rows[i].Cells["GelirVergisi"].Value);
                            bdrDv = Convert.ToDecimal(dataGridView1.Rows[i].Cells["DamgaVrg"].Value);
                            agi = Convert.ToDecimal(dataGridView1.Rows[i].Cells["Agi"].Value);
                            dataGridView1.Rows[i].Cells["(Gv-Agi)Gv"].Value = bdrGv - agi;

                            dataGridView1.Rows[i].Cells["PrimGunu"].Value = gun;
                            dataGridView1.Rows[i].Cells["AsgUcrGv"].Value = asgucrGv;
                            dataGridView1.Rows[i].Cells["GunlukGv"].Value = asgUcrGvGunluk;
                            dataGridView1.Rows[i].Cells["AsgUcrDv"].Value = asgucrDv;


                            // Gelir vergisi terkin hesaplama
                            if (bdrGv == 0)
                            {
                                terkinGv = 0;
                            }
                            //else if (bdrGv <= agi)
                            //{
                            //    terkinGv = 0;
                            //}
                            else if (bdrGv > (asgUcrGvGunluk * gun) && bdrGv > agi)
                            {
                                terkinGv = (asgUcrGvGunluk * gun) - agi;
                            }
                            else if (bdrGv < (asgUcrGvGunluk * gun) && bdrGv > agi)
                            {
                                terkinGv = bdrGv - agi;
                            }
                            // Damga vergisi terkin hesaplama
                            if (bdrDv == 0)
                            {
                                terkinDv = 0;
                            }
                            else if (bdrDv <= (asgUcrDvGunluk * gun))
                            {
                                terkinDv = bdrGv;
                            }
                            else if (bdrDv > (asgUcrDvGunluk * gun))
                            {
                                terkinDv = asgUcrDvGunluk * gun;
                            }


                            dataGridView1.Rows[i].Cells["TerkinGv"].Value = terkinGv;
                            dataGridView1.Rows[i].Cells["TerkinDv"].Value = terkinDv;

                        }


                    }
                    lblHesaplanan.Text = (gritSatirSayisi - i).ToString();


                }
                progressBar1.Value = j;
                //  dataGridView1.Refresh();
                baglan.Close();
            }
        }

        //private void btnNextPage_Click(object sender, EventArgs e)
        //{
        //    if (txtCurrentPage.Text==txtTotalPage.Text)
        //    {
        //        MessageBox.Show("Zaten Son Sayfadasınız... ");
        //    }
        //    else
        //    {
        //        var pageNo = int.Parse(txtCurrentPage.Text);
        //        txtCurrentPage.Text = (pageNo += 1).ToString();

        //        GetValues();
        //    }

        //}

        //private void btnLastPage_Click(object sender, EventArgs e)
        //{

        //    txtCurrentPage.Text = txtTotalPage.Text;

        //    int showPageRowCount = int.Parse(txtShowRowCount.Text);
        //    currentRecord = (int.Parse(txtCurrentPage.Text) - 1) * showPageRowCount;
        //    GetValues();
        //}

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var currentPage = int.Parse(txtCurrentPage.Text);
            txtCurrentRow.Text = (currentPage == 1 ? currentPage + e.RowIndex : ((currentPage - 1) * int.Parse(txtShowRowCount.Text)) + e.RowIndex + 1).ToString();

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["IsChanged"].Value = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");
            if (dataGridView2.Rows.Count > 0)
            {
                var secim = dataGridView2.SelectedCells[0].RowIndex;
                donem = dataGridView2.Rows[secim].Cells[0].Value.ToString();
            }
            else
            {
                donem = "";
            }


            bordrolarigoster("Select BordroSira AS Sıra, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,	CikisTarihi, Normal_Emekli as N_E,Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, MesaiGun as Mesai,HaftaSonu as HS,GenelTatil as GT,UcretsizIzin AS Ucrsz,SihhiIzin as Sıhhi, PrimGunu as PrmGun,FazlaMesaiGun as FmGun, AylikBrutUcret as BrutUcret,FmUcreti as FmUcrt, AylikEkOd as EkOdeme,ToplamKazanc, SgkMatrahi, SGkIsciPrim	as IsciPrim, IszlikIsciPrim as IszIsci, KumVergMatr,GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, SgkIsverenPrim as IsvPrim, IssizlikIsvPrim as IszIsv, BesKesintisi as BesKes, SairKesintiler as SairKes, AylikNetUcret as AylikNet, KanunNo From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo = '" + subeno + "' and PuantajDonem = '" + donem + "'");
            //datagiritalanlariniduzenle();
        }

        private void veriTabaninaKayitUyari()
        {
            lblbaslik.Text = "Veri Tabanına Kayıt İşlemi Başladı";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            TemelTablo.Clear();
            TemelTablo.Columns.Clear();



            if (txtdosyayolu.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen Dosya Yolu veya Yükleme Yapılacak Sayfa Seçimini Yapınız");
            }

            yuklubordro();

            if (yukluBordroTablo.Rows.Count > 0)
            {
                MessageBox.Show("DİKKAT;" + programreferans.firmaunvan + " /" + programreferans.subeunvan + " Şubesine ait \n daha önceden yüklenmiş Bordro bulunmaktadır. \n Yeni Bir Yükleme Yapacaksanız önceki bordro bilgilerini siliniz");
            }
            else
            {
                List<DataColumn> dataColumns = new List<DataColumn>();
                if (dataColumns.Count > 0)
                {
                    dataColumns.Clear();
                }
                foreach (var item in chktlistZorunluAlan.Items)
                {
                    dataColumns.Add(new DataColumn { ColumnName = item.ToString() });
                }

                TemelTablo.Columns.AddRange(dataColumns.ToArray());

                if (txtdosyayolu.Text.Trim().Length > 0)
                {
                    using (OleDbConnection con = new OleDbConnection())
                    {

                        con.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = '" + txtdosyayolu.Text.Trim().ToString() + "';Extended Properties = 'Excel 8.0;HDR=YES'";
                        con.Open();
                        OleDbCommand sorgu = new OleDbCommand("Select * From [" + comboBox1.Text.ToString() + "$]", con);
                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = sorgu;

                        da.Fill(TemelTablo);
                        //Table = ds.Tables[0];
                        Basliklaruygunmu();
                        ZorunluAlantammi();

                        if (EksikAlanlar == 0 && hataliAlanlar == 0)
                        {
                            //dataGridView1.DataSource = TemelTablo;
                        }
                        else
                        {

                            MessageBox.Show("Eksik veya Hatalı alanları düzelttikten sonra tekrar deneyiniz.");
                        }
                    }
                }
            }

            //BordroVeritabaninaYaz();

            yillikGvListe();

            tesvikliListe();

            progressBar1.Maximum = TemelTablo.Rows.Count;
            veriTabaninaKayitUyari();


            baglan.Open();
            SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [FirmaBordro](BordroSira,FirmaNo,SubeNo,PersId,PuantajYil,PuantajAy,PuantajDonem, TcNo, SgkNo, PersAdı, PersSoyadı, PersAdıSoyadı, GirisTarihi,CikisTarihi, Normal_Emekli, Net_Brüt, Net_BrtUcret, MesaiGun, HaftaSonu, GenelTatil,UcretsizIzin,SihhiIzin, PrimGunu, FazlaMesaiGun, AylikBrutUcret, GunlukBrut, FmUcreti, AylikEkOd,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,VergiInd,DamgaVrg,SgkIsverenPrim,IssizlikIsvPrim,BesKesintisi,SairKesintiler,AylikNetUcret,FirmaPersId) values (@bdrsira,@fno,@sno, @pid,@pyil,@pay,@pdnm,@tc,@sgkno,@padi,@psydi,@padisoyadi,@gtarih,@ctarih,@nrm,@ntbrt,@nbrtucret,@mesai,@hsonu, @gtatil,@uizin,@sihhi,@pun,@fmgun,@brtuc,@gnluk,@fmucr,@ekod,@tkznc, @sgkmat, @sgkisci, @iszisci, @kumgvmt, @gvmt, @gv,@agi,@vind,@dv,@sgkisv,@iszisv,@beskes,@sairkes,@netucr,@frmPerId)", baglan);
            //KanunNo,Gv_Agi,AsgUcrGv,GunlukGv,AsgUcrDv,TerkinGv,TerkinDv,
            //@kanun,@gvAgi,@auGv,@auGvgun,@auDv,@gvTerkin,@dvTerkin,




            int firmano = Convert.ToInt32(lblfirmano.Text);
            int subeno = Convert.ToInt32(lblsubeno.Text);


            for (int i = 0; i < TemelTablo.Rows.Count; i++)
            {
                string ay;
                //string ayy = TemelTablo.Rows[i]["PuantajAy"].ToString();
                if ((TemelTablo.Rows[i]["PuantajAy"].ToString()).Length == 1)
                {
                    ay = "0" + TemelTablo.Rows[i]["PuantajAy"].ToString();
                }
                else
                {
                    ay = TemelTablo.Rows[i]["PuantajAy"].ToString();
                }

                string tcno = TemelTablo.Rows[i]["TcNo"].ToString();
                string yil = TemelTablo.Rows[i]["PuantajYil"].ToString();
                string donem = yil + "/" + ay;
                string pid = yil + ay + tcno;
                string firmPersid = Convert.ToString(firmano) + Convert.ToString(subeno) + pid;
                var bordroSira = TemelTablo.Rows[i]["BordroSira"].ToString();
                var sgkNo = TemelTablo.Rows[i]["SgkNo"].ToString();
                var persadi = TemelTablo.Rows[i]["PersAdı"].ToString();
                var persoyadi = TemelTablo.Rows[i]["PersSoyadı"].ToString();
                var persadisoyadi = TemelTablo.Rows[i]["PersAdıSoyadı"].ToString();
                var giristarih = TemelTablo.Rows[i]["GirisTarihi"] != DBNull.Value ? Convert.ToDateTime(TemelTablo.Rows[i]["GirisTarihi"]).ToShortDateString().Replace('.', '/') : "";
                var cikistarih = TemelTablo.Rows[i]["CikisTarihi"] != DBNull.Value ? Convert.ToDateTime(TemelTablo.Rows[i]["CikisTarihi"]).ToShortDateString().Replace('.', '/') : "";
                if (cikistarih=="01.01.2100"||cikistarih=="01.01.2101" || cikistarih == "01/01/2100" || cikistarih == "01/01/2101")
                {
                    cikistarih = "";
                }
                var normalemekli = TemelTablo.Rows[i]["Normal_Emekli"].ToString();
                var netbrüt = TemelTablo.Rows[i]["Net_Brüt"].ToString();
                var nbrtucret = TemelTablo.Rows[i]["Net_BrtUcret"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["Net_BrtUcret"]) : 0;
                var mesaigun = TemelTablo.Rows[i]["MesaiGun"].ToString();
                var haftasonu = TemelTablo.Rows[i]["HaftaSonu"].ToString();
                var geneltatil = TemelTablo.Rows[i]["GenelTatil"].ToString();
                var ucretsizizin = TemelTablo.Rows[i]["UcretsizIzin"].ToString();
                var sihhiizin = TemelTablo.Rows[i]["SihhiIzin"].ToString();
                var primgun = TemelTablo.Rows[i]["PrimGunu"].ToString();
                var fmgun = TemelTablo.Rows[i]["FazlaMesaiGun"].ToString();

                var aylbrutucr = TemelTablo.Rows[i]["AylikBrutUcret"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["AylikBrutUcret"]) : 0;
                var gunlukucr = TemelTablo.Rows[i]["GunlukBrut"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["GunlukBrut"]) : 0;
                var fmucreti = TemelTablo.Rows[i]["FmUcreti"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["FmUcreti"]) : 0;
                var aylikekod = TemelTablo.Rows[i]["AylikEkOd"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["AylikEkOd"]) : 0;
                var tomlamkazanc = TemelTablo.Rows[i]["ToplamKazanc"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["ToplamKazanc"]) : 0;
                var sgkmatrah = TemelTablo.Rows[i]["SgkMatrahi"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["SgkMatrahi"]) : 0;
                var sgkisciprim = TemelTablo.Rows[i]["SGkIsciPrim"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["SGkIsciPrim"]) : 0;
                var issizlikisci = TemelTablo.Rows[i]["IszlikIsciPrim"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["IszlikIsciPrim"]) : 0;
                var kumvargimat = TemelTablo.Rows[i]["KumVergMatr"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["KumVergMatr"]) : 0;
                var gvmatrahi = TemelTablo.Rows[i]["GvMatrahi"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["GvMatrahi"]) : 0;
                var gv = TemelTablo.Rows[i]["GelirVergisi"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["GelirVergisi"]) : 0;
                var agi = TemelTablo.Rows[i]["Agi"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["Agi"]) : 0;
                var vergiind = TemelTablo.Rows[i]["VergiInd"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["VergiInd"]) : 0;

                var dv = TemelTablo.Rows[i]["DamgaVrg"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["DamgaVrg"]) : 0;
                var sgkisveren = TemelTablo.Rows[i]["SgkIsverenPrim"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["SgkIsverenPrim"]) : 0;
                var issizlikIsveren = TemelTablo.Rows[i]["IssizlikIsvPrim"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["IssizlikIsvPrim"]) : 0;
                var beskes = TemelTablo.Rows[i]["BesKesintisi"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["BesKesintisi"]) : 0;
                var sairkes = TemelTablo.Rows[i]["SairKesintiler"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["SairKesintiler"]) : 0;
                var ayliknet = TemelTablo.Rows[i]["AylikNetUcret"] != DBNull.Value ? Convert.ToDecimal(TemelTablo.Rows[i]["AylikNetUcret"]) : 0;

                //var tesviklipersonel = tesvikliHizmetListesi.Select("firmPersid='" + firmPersid + "' and (Kanun_No='00687' or Kanun_No='01687' or Kanun_No='17103' or Kanun_No='27103')", "Kanun_No");
                //string kanunno = "";
                //decimal asUcrGv = 0;
                //decimal asUcrDv = 0;
                //decimal gvTerkin = 0;
                //decimal dvTerkin = 0;
                //decimal asUcrGvGun = 0;
                //decimal asUcrDvGun = 0;

                //foreach (var item in tesviklipersonel)
                //{
                //    kanunno = item["Kanun_No"].ToString();
                //}


                //if (kanunno != "")
                //{
                //var ilgiliYilAsUcrGv = yillikGvListesi.Select("agi_yil='" + yil + "'");
                //foreach (var item in ilgiliYilAsUcrGv)
                //{
                //    asUcrGv = Convert.ToDecimal(item["asgariucr_gv"]);
                //    asUcrDv = Convert.ToDecimal(item["asgariucr_dv"]);
                //    asUcrGvGun = asUcrGv / 30;
                //    asUcrDvGun = asUcrDv / 30;
                //}
                //if (gv == 0)
                //{
                //    gvTerkin = 0;
                //}
                //if (gv > (asUcrGvGun * Convert.ToInt32(primgun)) && gv > agi)
                //{
                //    gvTerkin = (asUcrGvGun * Convert.ToInt32(primgun)) - agi;
                //}
                //if (gv < (asUcrGvGun * Convert.ToInt32(primgun)) && gv > agi)
                //{
                //    gvTerkin = gv - agi;
                //}

                //if (dv == 0)
                //{
                //    dvTerkin = 0;
                //}
                //if (dv > (asUcrDvGun * Convert.ToInt32(primgun)))
                //{
                //    dvTerkin = (asUcrDvGun * Convert.ToInt32(primgun));
                //}
                //if (dv < (asUcrDvGun * Convert.ToInt32(primgun)))
                //{
                //    dvTerkin = dv;
                //}
                //}
                //else
                //{
                //    kanunno = null;
                //}


                ekle.Parameters.AddWithValue("@bdrsira", bordroSira);
                ekle.Parameters.AddWithValue("@fno", firmano);
                ekle.Parameters.AddWithValue("@sno", subeno);
                ekle.Parameters.AddWithValue("@pid", pid);
                ekle.Parameters.AddWithValue("@pyil", yil);
                ekle.Parameters.AddWithValue("@pay", ay);
                ekle.Parameters.AddWithValue("@pdnm", donem);
                ekle.Parameters.AddWithValue("@tc", tcno);
                ekle.Parameters.AddWithValue("@sgkno", sgkNo);
                ekle.Parameters.AddWithValue("@padi", persadi);
                ekle.Parameters.AddWithValue("@psydi", persoyadi);
                ekle.Parameters.AddWithValue("@padisoyadi", persadisoyadi);
                ekle.Parameters.AddWithValue("@gtarih", giristarih);
                ekle.Parameters.AddWithValue("@ctarih", cikistarih);
                ekle.Parameters.AddWithValue("@nrm", normalemekli);
                ekle.Parameters.AddWithValue("@ntbrt", netbrüt);
                ekle.Parameters.AddWithValue("@nbrtucret", nbrtucret);
                ekle.Parameters.AddWithValue("@mesai", mesaigun);
                ekle.Parameters.AddWithValue("@hsonu", haftasonu);
                ekle.Parameters.AddWithValue("@gtatil", geneltatil);
                ekle.Parameters.AddWithValue("@uizin", ucretsizizin);
                ekle.Parameters.AddWithValue("@sihhi", sihhiizin);
                ekle.Parameters.AddWithValue("@pun", primgun);
                ekle.Parameters.AddWithValue("@fmgun", fmgun);
                ekle.Parameters.AddWithValue("@brtuc", aylbrutucr);
                ekle.Parameters.AddWithValue("@gnluk", gunlukucr);
                ekle.Parameters.AddWithValue("@fmucr", fmucreti);
                ekle.Parameters.AddWithValue("@ekod", aylikekod);
                ekle.Parameters.AddWithValue("@tkznc", tomlamkazanc);
                ekle.Parameters.AddWithValue("@sgkmat", sgkmatrah);
                ekle.Parameters.AddWithValue("@sgkisci", sgkisciprim);
                ekle.Parameters.AddWithValue("@iszisci", issizlikisci);
                ekle.Parameters.AddWithValue("@kumgvmt", kumvargimat);
                ekle.Parameters.AddWithValue("@gvmt", gvmatrahi);
                ekle.Parameters.AddWithValue("@gv", gv);
                ekle.Parameters.AddWithValue("@agi", agi);
                ekle.Parameters.AddWithValue("@vind", vergiind);

                ekle.Parameters.AddWithValue("@dv", dv);
                ekle.Parameters.AddWithValue("@sgkisv", sgkisveren);
                ekle.Parameters.AddWithValue("@iszisv", issizlikIsveren);
                ekle.Parameters.AddWithValue("@beskes", beskes);
                ekle.Parameters.AddWithValue("@sairkes", sairkes);
                ekle.Parameters.AddWithValue("@netucr", ayliknet);

                //ekle.Parameters.AddWithValue("@kanun", kanunno);
                //ekle.Parameters.AddWithValue("@gvAgi", gv - agi);
                //ekle.Parameters.AddWithValue("@auGv", asUcrGv);
                //ekle.Parameters.AddWithValue("@auGvgun", asUcrGvGun);
                //ekle.Parameters.AddWithValue("@auDv", asUcrDv);
                //ekle.Parameters.AddWithValue("@gvTerkin", gvTerkin);
                //ekle.Parameters.AddWithValue("@dvTerkin", dvTerkin);

                ekle.Parameters.AddWithValue("@frmPerId", firmPersid);

                ekle.ExecuteNonQuery();

                progressBar1.Value = i;

            }

            baglan.Close();
            MessageBox.Show("Veriler Veritabanına başarı ile kaydedildi");

            donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");
            donem = dataGridView2.Rows[0].Cells[0].Value.ToString();
            bordrolarigoster("Select BordroSira AS Sıra, PuantajDonem as Donem, TcNo,PersAdı,PersSoyadı,GirisTarihi,	CikisTarihi, Normal_Emekli as N_E,Net_Brüt as N_B,Net_BrtUcret as N_B_Ucret, MesaiGun as Mesai,HaftaSonu as HS,GenelTatil as GT,UcretsizIzin AS Ucrsz,SihhiIzin as Sıhhi, PrimGunu as PrmGun,FazlaMesaiGun as FmGun, AylikBrutUcret as BrutUcret,FmUcreti as FmUcrt, AylikEkOd as EkOdeme,ToplamKazanc, SgkMatrahi, SGkIsciPrim	as IsciPrim, IszlikIsciPrim as IszIsci, KumVergMatr,GvMatrahi, GelirVergisi, Agi, VergiInd, DamgaVrg, SgkIsverenPrim as IsvPrim, IssizlikIsvPrim as IszIsv, BesKesintisi as BesKes, SairKesintiler as SairKes, AylikNetUcret as AylikNet, KanunNo From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo ='" + subeno + "' and PuantajDonem='" + donem + "'");
            //datagiritalanlariniduzenle();

            IslemDurumu.islemdurumu = "Tamamlandı";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int firmano = Convert.ToInt32(lblfirmano.Text);
            int subeno = Convert.ToInt32(lblsubeno.Text);
            string HizmListTesviklipersid = "";
            string BordrTesvikliPersid = "";
            string yil = "";
            int primgun = 0;

            string kanunno = "";
            decimal asUcrGv = 0;
            decimal asUcrDv = 0;
            decimal gvTerkin = 0;
            decimal dvTerkin = 0;
            decimal asUcrGvGun = 0;
            decimal asUcrDvGun = 0;
            decimal gv = 0;
            decimal dv = 0;
            decimal agi = 0;

            yillikGvListe();
            TesvikHesBordro();
            tesvikliListe();
            var firmabordrosu = TesvikHesBordrosu.Select("FirmaNo = '" + firmano + "' and SubeNo = '" + subeno + "'");

            progressBar1.Maximum = tesvikliHizmetListesi.Rows.Count;

            for (int i = 0; i < tesvikliHizmetListesi.Rows.Count; i++)
            {

                HizmListTesviklipersid = tesvikliHizmetListesi.Rows[i]["firmPersid"].ToString();
                kanunno = tesvikliHizmetListesi.Rows[i]["Kanun_No"].ToString();

                var tesviklipersonel = TesvikHesBordrosu.Select("FirmaPersId='" + HizmListTesviklipersid + "'", "PrimGunu");

                foreach (var item in tesviklipersonel)
                {

                    primgun = Convert.ToInt32(item["PrimGunu"]);
                    yil = item["PuantajYil"].ToString();
                    gv = Convert.ToDecimal(item["GelirVergisi"]);
                    agi = Convert.ToDecimal(item["Agi"]);
                    dv = Convert.ToDecimal(item["DamgaVrg"]);
                }

                //if (kanunno != "")


                //var primgun = TemelTablo.Rows[i]["PrimGunu"].ToString();


                //string yil = TesvikHesBordrosu.Rows[i]["PuantajYil"].ToString();
                //gv = TesvikHesBordrosu.Rows[i]["GelirVergisi"] != DBNull.Value ? Convert.ToDecimal(TesvikHesBordrosu.Rows[i]["GelirVergisi"]) : 0;
                //agi = TesvikHesBordrosu.Rows[i]["Agi"] != DBNull.Value ? Convert.ToDecimal(TesvikHesBordrosu.Rows[i]["Agi"]) : 0;
                //dv = TesvikHesBordrosu.Rows[i]["DamgaVrg"] != DBNull.Value ? Convert.ToDecimal(TesvikHesBordrosu.Rows[i]["DamgaVrg"]) : 0;


                var ilgiliYilAsUcrGv = yillikGvListesi.Select("agi_yil='" + yil + "'");
                foreach (var item in ilgiliYilAsUcrGv)
                {
                    asUcrGv = Convert.ToDecimal(item["asgariucr_gv"]);
                    asUcrDv = Convert.ToDecimal(item["asgariucr_dv"]);
                    asUcrGvGun = asUcrGv / 30;
                    asUcrDvGun = asUcrDv / 30;
                }
                if (gv == 0)
                {
                    gvTerkin = 0;
                }
                if (gv > asUcrGv && gv > agi || gv == asUcrGv && gv > agi)
                {
                    gvTerkin = asUcrGv - agi;
                }
                if (gv < asUcrGv && gv > agi)
                {
                    gvTerkin = gv - agi;
                }
                if (gv == agi || gv < agi)
                {
                    gvTerkin = 0;
                }
                //if (gv== asUcrGv && gv> agi)
                //{
                //    gvTerkin = gv - agi;
                //}


                if (dv == 0)
                {
                    dvTerkin = 0;
                }
                if (dv > asUcrDv)
                {
                    dvTerkin = asUcrDv;
                }
                if (dv < asUcrDv)
                {
                    dvTerkin = dv;
                }
                if (dv == asUcrDv)
                {
                    dvTerkin = dv;
                }

                //else
                //{
                //    kanunno = null;
                //}
                baglan.Open();
                SQLiteCommand guncelle = new SQLiteCommand("update [FirmaBordro] set KanunNo= @kanun ,Gv_Agi=@gvAgi,AsgUcrGv=@auGv,GunlukGv=@auGvgun,AsgUcrDv=@auDv,TerkinGv=@gvTerkin,TerkinDv=@dvTerkin where FirmaPersId= '" + HizmListTesviklipersid + "'", baglan);

                guncelle.Parameters.AddWithValue("@kanun", kanunno);
                guncelle.Parameters.AddWithValue("@gvAgi", gv - agi);
                guncelle.Parameters.AddWithValue("@auGv", asUcrGv);
                guncelle.Parameters.AddWithValue("@auGvgun", asUcrGvGun);
                guncelle.Parameters.AddWithValue("@auDv", asUcrDv);
                guncelle.Parameters.AddWithValue("@gvTerkin", gvTerkin);
                guncelle.Parameters.AddWithValue("@dvTerkin", dvTerkin);
                guncelle.ExecuteNonQuery();
                baglan.Close();
                progressBar1.Value = i;
            }
            MessageBox.Show("Gelir Vergisi Teşvik İşlemi Tamamlandı...");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int firmano = Convert.ToInt32(lblfirmano.Text);
            int subeno = Convert.ToInt32(lblsubeno.Text);
            string HizmListTesviklipersid = "";
            string BordrTesvikliPersid = "";


            string kanunno = "";

            TesvikHesBordro();
            TeknotesvikliListe();
            var firmabordrosu = TesvikHesBordrosu.Select("FirmaNo = '" + firmano + "' and SubeNo = '" + subeno + "'");

            progressBar1.Maximum = TeknotesvikliHizmetListesi.Rows.Count;

            for (int i = 0; i < TeknotesvikliHizmetListesi.Rows.Count; i++)
            {

                HizmListTesviklipersid = TeknotesvikliHizmetListesi.Rows[i]["firmPersid"].ToString();
                kanunno = TeknotesvikliHizmetListesi.Rows[i]["Kanun_No"].ToString();

                var tesviklipersonel = TesvikHesBordrosu.Select("FirmaPersId='" + HizmListTesviklipersid + "'", "PrimGunu");


                baglan.Open();
                SQLiteCommand guncelle = new SQLiteCommand("update [FirmaBordro] set KanunNo= @kanun  where FirmaPersId= '" + HizmListTesviklipersid + "'", baglan);

                guncelle.Parameters.AddWithValue("@kanun", kanunno);

                guncelle.ExecuteNonQuery();
                baglan.Close();
                progressBar1.Value = i;
            }
            MessageBox.Show("Firma Bordrosuna Kanun Maddeleri Eklenmiştir. ");
        }
    }
}
