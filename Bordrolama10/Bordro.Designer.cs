
namespace Bordrolama10
{
    partial class Bordro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDosyaYolu = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtdosyayolu = new System.Windows.Forms.TextBox();
            this.btnOku = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnfiltrekaldir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblfirma = new System.Windows.Forms.Label();
            this.lblsube = new System.Windows.Forms.Label();
            this.lblfirmano = new System.Windows.Forms.Label();
            this.lblsubeno = new System.Windows.Forms.Label();
            this.btnKapat = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblsgkisyerino = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnArgeHesapla = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblHesaplanan = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(205, 97);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1219, 532);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnDosyaYolu
            // 
            this.btnDosyaYolu.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDosyaYolu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDosyaYolu.Location = new System.Drawing.Point(1165, 3);
            this.btnDosyaYolu.Name = "btnDosyaYolu";
            this.btnDosyaYolu.Size = new System.Drawing.Size(101, 23);
            this.btnDosyaYolu.TabIndex = 1;
            this.btnDosyaYolu.Text = "Dosya Yolu";
            this.btnDosyaYolu.UseVisualStyleBackColor = false;
            this.btnDosyaYolu.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtdosyayolu
            // 
            this.txtdosyayolu.Location = new System.Drawing.Point(937, 6);
            this.txtdosyayolu.Name = "txtdosyayolu";
            this.txtdosyayolu.Size = new System.Drawing.Size(210, 20);
            this.txtdosyayolu.TabIndex = 2;
            // 
            // btnOku
            // 
            this.btnOku.BackColor = System.Drawing.Color.Gainsboro;
            this.btnOku.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOku.Location = new System.Drawing.Point(1165, 30);
            this.btnOku.Name = "btnOku";
            this.btnOku.Size = new System.Drawing.Size(101, 23);
            this.btnOku.TabIndex = 3;
            this.btnOku.Text = "Kaydet";
            this.btnOku.UseVisualStyleBackColor = false;
            this.btnOku.Click += new System.EventHandler(this.btnOku_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1026, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(851, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "İşlem Yapılacak Sayfa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(851, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Dosya Yolu";
            // 
            // btnfiltrekaldir
            // 
            this.btnfiltrekaldir.BackColor = System.Drawing.Color.Gainsboro;
            this.btnfiltrekaldir.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnfiltrekaldir.Location = new System.Drawing.Point(8, 87);
            this.btnfiltrekaldir.Name = "btnfiltrekaldir";
            this.btnfiltrekaldir.Size = new System.Drawing.Size(179, 23);
            this.btnfiltrekaldir.TabIndex = 11;
            this.btnfiltrekaldir.Text = "Dönem Filtresini Kaldır";
            this.btnfiltrekaldir.UseVisualStyleBackColor = false;
            this.btnfiltrekaldir.Click += new System.EventHandler(this.btnfiltrekaldir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(32, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "No\'lu İşlem Yapılacak Firma";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(32, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "No\'lu İşlem Yapılacak Şube";
            // 
            // lblfirma
            // 
            this.lblfirma.AutoSize = true;
            this.lblfirma.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblfirma.Location = new System.Drawing.Point(218, 9);
            this.lblfirma.Name = "lblfirma";
            this.lblfirma.Size = new System.Drawing.Size(14, 16);
            this.lblfirma.TabIndex = 14;
            this.lblfirma.Text = "-";
            // 
            // lblsube
            // 
            this.lblsube.AutoSize = true;
            this.lblsube.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblsube.Location = new System.Drawing.Point(218, 30);
            this.lblsube.Name = "lblsube";
            this.lblsube.Size = new System.Drawing.Size(14, 16);
            this.lblsube.TabIndex = 15;
            this.lblsube.Text = "-";
            // 
            // lblfirmano
            // 
            this.lblfirmano.AutoSize = true;
            this.lblfirmano.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblfirmano.Location = new System.Drawing.Point(12, 9);
            this.lblfirmano.Name = "lblfirmano";
            this.lblfirmano.Size = new System.Drawing.Size(14, 16);
            this.lblfirmano.TabIndex = 16;
            this.lblfirmano.Text = "-";
            // 
            // lblsubeno
            // 
            this.lblsubeno.AutoSize = true;
            this.lblsubeno.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblsubeno.Location = new System.Drawing.Point(12, 30);
            this.lblsubeno.Name = "lblsubeno";
            this.lblsubeno.Size = new System.Drawing.Size(14, 16);
            this.lblsubeno.TabIndex = 17;
            this.lblsubeno.Text = "-";
            // 
            // btnKapat
            // 
            this.btnKapat.BackColor = System.Drawing.Color.Gainsboro;
            this.btnKapat.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKapat.Location = new System.Drawing.Point(1349, 58);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(75, 23);
            this.btnKapat.TabIndex = 18;
            this.btnKapat.Text = "Çıkış";
            this.btnKapat.UseVisualStyleBackColor = false;
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSil.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.Location = new System.Drawing.Point(1317, 29);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(107, 23);
            this.btnSil.TabIndex = 19;
            this.btnSil.Text = "Bordro Sil";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 133);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 25;
            this.dataGridView2.Size = new System.Drawing.Size(187, 496);
            this.dataGridView2.TabIndex = 20;
            this.dataGridView2.Click += new System.EventHandler(this.dataGridView2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(202, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Yükleme Yapılan Bordro";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(9, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(178, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Yükleme Yapılan Dönemler";
            // 
            // lblsgkisyerino
            // 
            this.lblsgkisyerino.AutoSize = true;
            this.lblsgkisyerino.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblsgkisyerino.Location = new System.Drawing.Point(220, 52);
            this.lblsgkisyerino.Name = "lblsgkisyerino";
            this.lblsgkisyerino.Size = new System.Drawing.Size(14, 16);
            this.lblsgkisyerino.TabIndex = 24;
            this.lblsgkisyerino.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(32, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 23;
            this.label8.Text = "Şube Sgk No";
            // 
            // btnArgeHesapla
            // 
            this.btnArgeHesapla.BackColor = System.Drawing.Color.Gainsboro;
            this.btnArgeHesapla.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnArgeHesapla.Location = new System.Drawing.Point(1165, 59);
            this.btnArgeHesapla.Name = "btnArgeHesapla";
            this.btnArgeHesapla.Size = new System.Drawing.Size(130, 23);
            this.btnArgeHesapla.TabIndex = 25;
            this.btnArgeHesapla.Text = "Arge Hesapla";
            this.btnArgeHesapla.UseVisualStyleBackColor = false;
            this.btnArgeHesapla.Click += new System.EventHandler(this.btnArgeHesapla_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(589, 29);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(239, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 26;
            this.progressBar1.UseWaitCursor = true;
            // 
            // lblHesaplanan
            // 
            this.lblHesaplanan.AutoSize = true;
            this.lblHesaplanan.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHesaplanan.Location = new System.Drawing.Point(744, 55);
            this.lblHesaplanan.Name = "lblHesaplanan";
            this.lblHesaplanan.Size = new System.Drawing.Size(14, 16);
            this.lblHesaplanan.TabIndex = 27;
            this.lblHesaplanan.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(586, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 16);
            this.label9.TabIndex = 28;
            this.label9.Text = "Hesaplanan Kayıt";
            // 
            // Bordro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(1436, 641);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblHesaplanan);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnArgeHesapla);
            this.Controls.Add(this.lblsgkisyerino);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.lblsubeno);
            this.Controls.Add(this.lblfirmano);
            this.Controls.Add(this.lblsube);
            this.Controls.Add(this.lblfirma);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnfiltrekaldir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnOku);
            this.Controls.Add(this.txtdosyayolu);
            this.Controls.Add(this.btnDosyaYolu);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Bordro";
            this.Text = "Bordro";
            this.Load += new System.EventHandler(this.Bordro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDosyaYolu;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtdosyayolu;
        private System.Windows.Forms.Button btnOku;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnfiltrekaldir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblfirma;
        private System.Windows.Forms.Label lblsube;
        private System.Windows.Forms.Label lblfirmano;
        private System.Windows.Forms.Label lblsubeno;
        private System.Windows.Forms.Button btnKapat;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblsgkisyerino;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnArgeHesapla;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblHesaplanan;
        private System.Windows.Forms.Label label9;
    }
}