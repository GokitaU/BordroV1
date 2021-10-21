using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bordrolama10
{
    public partial class IslemDurumuPopup : Form
    {
        public IslemDurumuPopup()
        {
            InitializeComponent();
        }

        private void IslemDurumuPopup_Load(object sender, EventArgs e)
        {

            timer1.Enabled = true;
        }

        int sayac = 0;
        
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            sayac++;
            timer1.Interval = 500;
            if (IslemDurumu.islemdurumu == "")
            {
                this.lblbaslik.ForeColor = Color.Black;
            }
            if (IslemDurumu.islemdurumu == "")
            {
                this.lblbaslik.ForeColor = Color.Red;
            }
            if (IslemDurumu.islemdurumu == "")
            {
                this.lblbaslik.ForeColor = Color.Blue;
            }
            if (IslemDurumu.islemdurumu != "")
            {
                this.Close();
            }


        }
    }
}
