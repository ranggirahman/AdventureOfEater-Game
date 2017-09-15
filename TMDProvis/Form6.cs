using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;   // menggunakan reporting CR

namespace TMDProvis
{
    public partial class Form6 : Form
    {
        private Form3 form3;
        private ReportDocument myReport2;

        public Form6()
        {
            InitializeComponent();

            myReport2 = new ReportDocument();
            myReport2.Load("../../CrystalReport2.rpt");
            crystalReportViewer1.ReportSource = myReport2;
            crystalReportViewer1.Refresh();
        }

        public void setBack(Form3 sodara)
        {
            //form3 masih dalam mdi yg sama, sodaraan
            this.form3 = sodara;
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            this.form3.Show();
        }
    }
}
