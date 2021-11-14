
namespace Bordrolama10
{
    partial class FirmaAra
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
            this.btnrfrnsara = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtunvan = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtreferans = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblid = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnrfrnsara
            // 
            this.btnrfrnsara.BackColor = System.Drawing.Color.Gainsboro;
            this.btnrfrnsara.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnrfrnsara.ForeColor = System.Drawing.Color.Black;
            this.btnrfrnsara.Location = new System.Drawing.Point(358, 61);
            this.btnrfrnsara.Name = "btnrfrnsara";
            this.btnrfrnsara.Size = new System.Drawing.Size(117, 32);
            this.btnrfrnsara.TabIndex = 89;
            this.btnrfrnsara.Text = "ARA";
            this.btnrfrnsara.UseVisualStyleBackColor = false;
            this.btnrfrnsara.Click += new System.EventHandler(this.btnrfrnsara_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 104);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(465, 276);
            this.dataGridView1.TabIndex = 88;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // txtunvan
            // 
            this.txtunvan.BackColor = System.Drawing.Color.White;
            this.txtunvan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtunvan.Location = new System.Drawing.Point(163, 43);
            this.txtunvan.Name = "txtunvan";
            this.txtunvan.Size = new System.Drawing.Size(177, 23);
            this.txtunvan.TabIndex = 83;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.AntiqueWhite;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(26, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(131, 16);
            this.label11.TabIndex = 87;
            this.label11.Text = "ÜNVANA GÖRE ARA";
            // 
            // txtreferans
            // 
            this.txtreferans.BackColor = System.Drawing.Color.White;
            this.txtreferans.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtreferans.Location = new System.Drawing.Point(163, 68);
            this.txtreferans.Name = "txtreferans";
            this.txtreferans.Size = new System.Drawing.Size(177, 23);
            this.txtreferans.TabIndex = 84;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 16);
            this.label2.TabIndex = 86;
            this.label2.Text = "REFERANSA GÖRE ARA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(200, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 22);
            this.label1.TabIndex = 85;
            this.label1.Text = "FİRMA ARA";
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.BackColor = System.Drawing.Color.AntiqueWhite;
            this.lblid.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblid.ForeColor = System.Drawing.Color.Black;
            this.lblid.Location = new System.Drawing.Point(10, 11);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(19, 16);
            this.lblid.TabIndex = 90;
            this.lblid.Text = "id";
            // 
            // FirmaAra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(489, 390);
            this.Controls.Add(this.lblid);
            this.Controls.Add(this.btnrfrnsara);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtunvan);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtreferans);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FirmaAra";
            this.Text = "ReferansAra";
            this.Load += new System.EventHandler(this.FirmaAra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnrfrnsara;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtunvan;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtreferans;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblid;
    }
}