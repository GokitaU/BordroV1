using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace Bordrolama10
{
    public partial class GelirVergisiBordro : DevExpress.XtraReports.UI.XtraReport
    {
        public object MyDataSource { get; set; }
        public string MyDataMember { get; set; }

        public GelirVergisiBordro()
        {
            InitializeComponent();
          //  DataSource = MyDataSource;
            //  DataMember = MyDataMember;
        }

        
    }
}
