using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Globalization;
using System.Data;
using Consul;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Bordrolama10
{
    public partial class Bordro : Form
    {
        public Bordro()
        {
            InitializeComponent();
        }


        public void bordrolarigoster(string bordro)
        {
            SQLiteDataAdapter da = new SQLiteDataAdapter(bordro, baglan);
            DataSet dss = new DataSet();
            da.Fill(dss);
            dataGridView1.DataSource = dss.Tables[0];
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


            dataGridView1.Columns["AylikBrutUcret"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["AylikBrutUcret"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GunlukBrut"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["GunlukBrut"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["FmUcreti"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["FmUcreti"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["AylikEkOd"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["AylikEkOd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["ToplamKazanc"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["ToplamKazanc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["SgkMatrahi"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["SgkMatrahi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["SGkIsciPrim"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["SGkIsciPrim"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["IszlikIsciPrim"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["IszlikIsciPrim"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["KumVergMatr"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["KumVergMatr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GvMatrahi"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["GvMatrahi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["GelirVergisi"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["GelirVergisi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Agi"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["Agi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["VergiInd"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["VergiInd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["DamgaMatrahi"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["DamgaMatrahi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["DamgaVrg"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["DamgaVrg"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["SgkIsverenPrim"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["SgkIsverenPrim"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["IssizlikIsvPrim"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["IssizlikIsvPrim"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["BesKesintisi"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["BesKesintisi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["SairKesintiler"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["SairKesintiler"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["AylikNetUcret"].DefaultCellStyle.Format = "#,#.##";
            dataGridView1.Columns["AylikNetUcret"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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

        private void btnOku_Click(object sender, EventArgs e)
        {
            if (txtdosyayolu.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lütfen Dosya Yolu veya Yükleme Yapılacak Sayfa Seçimini Yapınız");
            }
            else
            {

                TemelTablo.Clear();
                TemelTablo.Columns.Clear();

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
                        DataSet ds = new DataSet();
                        da.Fill(TemelTablo);
                        //Table = ds.Tables[0];
                        Basliklaruygunmu();
                        ZorunluAlantammi();

                        //for (int k = 0; k < Table.Columns.Count; k++)
                        //{
                        //    var caption = Table.Columns[k].Caption;
                        //    for (int i = 0; i < length; i++)
                        //    {

                        //    }
                        //    var isChecked = chktlistZorunluAlan.GetItemCheckState(m) == CheckState.Unchecked;
                        //}



                        if (EksikAlanlar == 0 && hataliAlanlar == 0)
                        {

                            //for (int i = 0; i < TemelTablo.Columns.Count; i++)
                            //{
                            //    var temelBaslik = TemelTablo.Columns[i].Caption;
                            //}

                            //for (int i = 0; i < Table.Columns.Count; i++)
                            //{
                            //    var caption = Table.Columns[i].Caption;
                            //    bool isEqual = false;
                            //    for (int j = 0; j < chktlistZorunluAlan.Items.Count; j++)
                            //    {

                            //        //var isChecked = chktlistZorunluAlan.GetItemCheckState(j) == CheckState.Unchecked;
                            //        //if (chktlistZorunluAlan.Items[j].ToString()==caption) //continue;
                            //        //{
                            //        //    MessageBox.Show("Zorunlu Alan eksik" + caption + "");
                            //        //}
                            //        if (chktlistZorunluAlan.Items[j].ToString() == caption)
                            //        {
                            //            isEqual = true;
                            //            break;
                            //        }

                            //    }

                            //if (!isEqual)
                            //{
                            //    Table.Columns.RemoveAt(i);
                            //    i--;
                            //}
                            // }
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
        private void ZorunluAlantammi()
        {
            string sutunadi;
            string chksutunadi;
            var eksikExcelAlanlari = new List<string>();
            bool eksik = false;
            for (int i = 0; i < chktlistZorunluAlan.CheckedItems.Count; i++)
            {
                chksutunadi = chktlistZorunluAlan.CheckedItems[i].ToString();
                for (int j = 0; j < Table.Columns.Count; j++)
                {
                    sutunadi = Table.Columns[j].Caption;
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
        private void Basliklaruygunmu()
        {



            string sutunadi;
            string chksutunadi;
            var hataliExcelAlanlari = new List<string>();
            bool hatali = false;

            for (int i = 0; i < Table.Columns.Count; i++)
            {
                sutunadi = Table.Columns[i].Caption;
                for (int j = 0; j < chktlistZorunluAlan.Items.Count; j++)
                {
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
            //for (int i = 0; i < chktlistZorunluAlan.Items.Count; i++)
            //{
            //    sutunadi = chktlistZorunluAlan.Items[i].ToString();
            //    for (int j = 0; j < Table.Columns.Count; j++)
            //    {
            //        if (Table.Columns[j].Caption == sutunadi) continue;
            //        {
            //            if (j==Table.Columns.Count)
            //            {
            //                MessageBox.Show("Tabloda Uyumsuz alanlar Var");
            //            }
            //        }
            //    }


            //bool hata = false;
            //string sutunAdi;
            //int index = -1;
            //for (int i = 0; i < chktlistZorunluAlan.Items.Count; i++)
            //{
            //    //sutunAdi = dataGridView1.Columns[i].HeaderText;
            //    //sutunAdi = Table.Columns[i].Caption;
            //    //if (chktlistZorunluAlan.GetItemChecked(i) == true)
            //    //{


            //        sutunAdi = chktlistZorunluAlan.Items[i].ToString();

            //        for (int j = 0; j < Table.Columns.Count; j++)
            //        {
            //            //if (chktlistZorunluAlan.CheckedItems[j].ToString() == sutunAdi)
            //            if (Table.Columns[j].Caption == sutunAdi)
            //            {
            //                hata = false;
            //                break;

            //            }
            //            else
            //            {
            //                hata = true;
            //                index = j;
            //                // MessageBox.Show($"Yeniden Bordro Hesaplama İşlemlerinde olması gereken zorunlu alan eksik\n Lütfen Zorunlu Alanları Tamamlayınız. Eksik Olan Alan = {sutunAdi}");
            //            }
            //        }

            // }
            //if (hata)
            //{
            //    //break;
            //    throw new Exception($"Sutun Bulunamadı-{sutunAdi}-{index}");
            //}


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
            chktlistZorunluAlan.Items.Add("DamgaMatrahi");
            chktlistZorunluAlan.Items.Add("DamgaVrg", true);
            chktlistZorunluAlan.Items.Add("SgkIsverenPrim");
            chktlistZorunluAlan.Items.Add("IssizlikIsvPrim");
            chktlistZorunluAlan.Items.Add("BesKesintisi");
            chktlistZorunluAlan.Items.Add("SairKesintiler");
            chktlistZorunluAlan.Items.Add("AylikNetUcret", true);



            //// dataGridView1.Columns.Add(new DataGridViewColumn { Name = "BdrId", CellTemplate = new DataGridViewTextBoxCell() });// datagrite başlık ekler
            int firmano = Convert.ToInt32(programreferans.firmaid);
            int subeno = programreferans.subid;


            load = true;
            bordrolarigoster("Select * From FirmaBordro where FirmaNo = '" + firmano + "' and SubeNo ='" + subeno + "'");
            donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");

            //  datagiritalanlariniduzenle();
        }

        //private void btnKaydet_Click(object sender, EventArgs e)
        private void BordroVeritabaninaYaz()
        {
            baglan.Open();
            SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [FirmaBordro](BordroSira,FirmaNo,SubeNo,PersId,PuantajYil,PuantajAy,PuantajDonem, TcNo, SgkNo, PersAdı, PersSoyadı, PersAdıSoyadı, GirisTarihi,CikisTarihi, Normal_Emekli, Net_Brüt, MesaiGun, HaftaSonu, GenelTatil,UcretsizIzin,SihhiIzin, PrimGunu, FazlaMesaiGun, AylikBrutUcret, GunlukBrut, FmUcreti, AylikEkOd,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,VergiInd,DamgaMatrahi,DamgaVrg,SgkIsverenPrim,IssizlikIsvPrim,BesKesintisi,SairKesintiler,AylikNetUcret) values (@bdrsira,@fno,@sno, @pid,@pyil,@pay,@pdnm,@tc,@sgkno,@padi,@psydi,@padisoyadi,@gtarih,@ctarih,@nrm,@ntbrt,@mesai,@hsonu, @gtatil,@uizin,@sihhi,@pun,@fmgun,@brtuc,@gnluk,@fmucr,@ekod,@tkznc, @sgkmat, @sgkisci, @iszisci, @kumgvmt, @gvmt, @gv,@agi,@vind,@dvmt,@dv,@sgkisv,@iszisv,@beskes,@sairkes,@netucr)", baglan);

            var parameterList = new List<string> { "@bdrsira", "@fno", "@sno", "@pid", "@pyil", "@pay", "@pdnm", "@tc", "@sgkno", "@padi", "@psydi", "@padisoyadi", "@gtarih", "@ctarih", "@nrm", "@ntbrt", "@mesai", "@hsonu", "@gtatil", "@uizin", "@sihhi", "@pun", "@fmgun", "@brtuc", "@gnluk", "@fmucr", "@ekod", "@tkznc", "@sgkmat", "@sgkisci", "@iszisci", "@kumgvmt", "@gvmt", "@gv", "@agi", "@vind", "@dvmt", "@dv", "@sgkisv", "@iszisv", "@beskes", "@sairkes", "@netucr" };

            //SQLiteCommand sorgu = new SQLiteCommand("Select COLUMN_NAME * from FirmaBordro", baglan);
            //SQLiteDataReader dr = sorgu.ExecuteReader();


            //for (int k = 0; k < chktlistZorunluAlan.Items.Count; k++)
            //{


            //    for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //    {
            //        var baslik = dataGridView1.Columns[i].HeaderText;
            //        if (chktlistZorunluAlan.Items[k].ToString() == baslik)
            //        {
            //            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            //            {
            //                var parametredegeri = dataGridView1.Rows[j].Cells[baslik].Value.ToString();
            //                if (parametredegeri != "")
            //                {
            //                    ekle.Parameters.AddWithValue(parameterList[k - 1], dataGridView1.Rows[j].Cells[baslik].Value);
            //                    break;
            //                }
            //                else
            //                {
            //                    ekle.Parameters.AddWithValue(parameterList[k - 1], null);
            //                    break;
            //                }
            //            }

            //            break;
            //        }


            //    }
            //}
            //ekle.ExecuteNonQuery();





            //baglan.Open();
            //SQLiteCommand ekle = new SQLiteCommand("INSERT INTO [FirmaBordro](BordroSira,FirmaNo,SubeNo,PersId,PuantajYil,PuantajAy,PuantajDonem, TcNo, SgkNo, PersAdı, PersSoyadı, PersAdıSoyadı, GirisTarihi,CikisTarihi, Normal_Emekli, Net_Brüt, MesaiGun, HaftaSonu, GenelTatil,UcretsizIzin,SihhiIzin, PrimGunu, FazlaMesaiGun, AylikBrutUcret, GunlukBrut, FmUcreti, AylikEkOd,ToplamKazanc,SgkMatrahi,SGkIsciPrim,IszlikIsciPrim,KumVergMatr,GvMatrahi,GelirVergisi,Agi,VergiInd,DamgaMatrahi,DamgaVrg,SgkIsverenPrim,IssizlikIsvPrim,BesKesintisi,SairKesintiler,AylikNetUcret) values (@bdrsira,@fno,@sno, @pid,@pyil,@pay,@pdnm,@tc,@sgkno,@padi,@psydi,@padisoyadi,@gtarih,@ctarih,@nrm,@ntbrt,@mesai,@hsonu, @gtatil,@uizin,@sihhi,@pun,@fmgun,@brtuc,@gnluk,@fmucr,@ekod,@tkznc, @sgkmat, @sgkisci, @iszisci, @kumgvmt, @gvmt, @gv,@agi,@vind,@dvmt,@dv,@sgkisv,@iszisv,@beskes,@sairkes,@netucr",baglan);




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
                string donem = yil + ay;
                string pid = yil + ay + tcno;

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
                var damgamatrahi = dataGridView1.Rows[i].Cells["DamgaMatrahi"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["DamgaMatrahi"].Value) : 0;
                var dv = dataGridView1.Rows[i].Cells["DamgaVrg"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["DamgaVrg"].Value) : 0;
                var sgkisveren = dataGridView1.Rows[i].Cells["SgkIsverenPrim"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["SgkIsverenPrim"].Value) : 0;
                var issizlikIsveren = dataGridView1.Rows[i].Cells["IssizlikIsvPrim"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["IssizlikIsvPrim"].Value) : 0;
                var beskes = dataGridView1.Rows[i].Cells["BesKesintisi"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["BesKesintisi"].Value) : 0;
                var sairkes = dataGridView1.Rows[i].Cells["SairKesintiler"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["SairKesintiler"].Value) : 0;
                var ayliknet = dataGridView1.Rows[i].Cells["AylikNetUcret"].Value != DBNull.Value ? Convert.ToDecimal(dataGridView1.Rows[i].Cells["AylikNetUcret"].Value) : 0;



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
                ekle.Parameters.AddWithValue("@dvmt", damgamatrahi);
                ekle.Parameters.AddWithValue("@dv", dv);
                ekle.Parameters.AddWithValue("@sgkisv", sgkisciprim);
                ekle.Parameters.AddWithValue("@iszisv", issizlikIsveren);
                ekle.Parameters.AddWithValue("@beskes", beskes);
                ekle.Parameters.AddWithValue("@sairkes", sairkes);
                ekle.Parameters.AddWithValue("@netucr", ayliknet);


                ekle.ExecuteNonQuery();


            }

            baglan.Close();
            MessageBox.Show("Veriler Veritabanına başarı ile kaydedildi");

            donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");
            //datagiritalanlariniduzenle();
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
                bordrolarigoster("Select * From FirmaBordro where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "'");
                donemlerigoster("SELECT PuantajDonem as Donem, count(PersId) as Per_Sayi From FirmaBordro Where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' GROUP by Donem");
                baglan.Close();


            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            int secim = dataGridView2.SelectedCells[0].RowIndex;
            var donem = dataGridView2.Rows[secim].Cells[0].Value.ToString();
            bordrolarigoster("Select * From FirmaBordro where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "' and PuantajDonem = '" + donem + "'");
            datagiritalanlariniduzenle();
        }

        private void btnfiltrekaldir_Click(object sender, EventArgs e)
        {
            bordrolarigoster("Select * From FirmaBordro where FirmaNo = '" + programreferans.firmaid + "' and SubeNo ='" + programreferans.subid + "'");
            // datagiritalanlariniduzenle();
        }

        private void btnArgeHesapla_Click(object sender, EventArgs e)
        {
            baglan.Open();
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









            int gritSatirSayisi = dataGridView1.Rows.Count;
            progressBar1.Maximum = gritSatirSayisi-1;
            for (int i = 0; i < gritSatirSayisi-1; i++)
            {
                

                string agiyil = dataGridView1.Rows[i].Cells["PuantajYil"].Value.ToString();
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


                string dtgritPersid = programreferans.firmaid.ToString() + programreferans.subid.ToString() + dataGridView1.Rows[i].Cells["PersId"].Value;

                using (SQLiteCommand HzmtListesi = new SQLiteCommand("Select * From HizmetListesi where firmPersid = '" + dtgritPersid + "'", baglan))
                {
                    using (SQLiteDataReader dr = HzmtListesi.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            hzmtListPersid = dr["firmPersid"].ToString();//.GetString(26);//dr[26].ToString().Trim();
                            //hzmKanunNo = dr[18].ToString();
                            hzmKanunNo = dr["Kanun_No"].ToString();//.GetString(26);//dr[26].ToString().Trim();
                        }
                    } 
                }


                if (dtgritPersid == hzmtListPersid)
                {
                    if (hzmKanunNo == "00687" || hzmKanunNo == "01687" || hzmKanunNo == "27103" || hzmKanunNo == "17103")
                    {
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
                        else if (bdrGv <= agi)
                        {
                            terkinGv = 0;
                        }
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
                        else if (bdrDv <= (asgUcrDvGunluk*gun))
                        {
                            terkinDv = bdrGv;
                        }
                        else if (bdrDv > (asgUcrDvGunluk * gun))
                        {
                            terkinDv = asgUcrDvGunluk*gun;
                        }


                        dataGridView1.Rows[i].Cells["TerkinGv"].Value = terkinGv;
                        dataGridView1.Rows[i].Cells["TerkinDv"].Value = terkinDv;
                    }
                }
                progressBar1.Value = i;
                lblHesaplanan.Text = (gritSatirSayisi - i).ToString();
            }
            //  dataGridView1.Refresh();
            baglan.Close();
            baglan.Dispose();
        }

        private void terkinhesapla()
        {


        }

    }
}
