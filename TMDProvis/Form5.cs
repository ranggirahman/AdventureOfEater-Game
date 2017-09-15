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
    public partial class Form5 : Form
    {
        private Form2 form2;
        private ReportDocument myReport;

        public Form5()
        {
            InitializeComponent();

            myReport = new ReportDocument();
            myReport.Load("../../CrystalReport1.rpt");
            crystalReportViewer1.ReportSource = myReport;
            crystalReportViewer1.Refresh();
        }

        public void setBack(Form2 sodara)
        {
            //form3 masih dalam mdi yg sama, sodaraan
            this.form2 = sodara;
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            this.form2.Show();
        }

    }
}
