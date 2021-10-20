
namespace Bordrolama10
{
    partial class EbildV1Guvenlik
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
            this.btnTmm = new System.Windows.Forms.Button();
            this.txtV1Guvenlik = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTmm
            // 
            this.btnTmm.BackColor = System.Drawing.Color.Gainsboro;
            this.btnTmm.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTmm.Location = new System.Drawing.Point(215, 68);
            this.btnTmm.Name = "btnTmm";
            this.btnTmm.Size = new System.Drawing.Size(91, 33);
            this.btnTmm.TabIndex = 0;
            this.btnTmm.Text = "Tamam";
            this.btnTmm.UseVisualStyleBackColor = false;
            this.btnTmm.Click += new System.EventHandler(this.btnTmm_Click);
            // 
            // txtV1Guvenlik
            // 
            this.txtV1Guvenlik.Location = new System.Drawing.Point(136, 74);
            this.txtV1Guvenlik.Name = "txtV1Guvenlik";
            this.txtV1Guvenlik.Size = new System.Drawing.Size(73, 23);
            this.txtV1Guvenlik.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 34);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "E-Bildirge V1 İçin Güvenlik Anahtarını Girip Tamam Tuşuna Basınız";
            // 
            // EbildV1Guvenlik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(372, 110);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtV1Guvenlik);
            this.Controls.Add(this.btnTmm);
            this.Name = "EbildV1Guvenlik";
            this.Text = "EbildV1Guvenlik";
            this.Load += new System.EventHandler(this.EbildV1Guvenlik_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTmm;
        private System.Windows.Forms.TextBox txtV1Guvenlik;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}