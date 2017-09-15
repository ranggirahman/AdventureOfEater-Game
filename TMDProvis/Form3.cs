using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TMDProvis
{
    public partial class Form3 : Form
    {

        private Form2 frmTPermainan;
        private Form6 form6;
        private String username;
        private DBConnection db;
        private DataTable dt;
        private BindingSource bs;
        private bool statusOpen;

        public Form3()
        {
            InitializeComponent();
            frmTPermainan = new Form2();
            form6 = new Form6();
            db = new DBConnection();
            statusOpen = db.openConnection();
        }

        public void setKembali(Form2 frmTPermainan)
        {
            this.frmTPermainan = frmTPermainan;
        }

        public void setCetak(Form6 sodara)
        {
            this.form6 = sodara;
        }

        public void setJudul(String username)
        {
            if (statusOpen)
            {
                string query = "select * from tjejak where Username = '" + username + "'";

                //proses query dan tampilkan hasil ke data grid view
                dt = new DataTable();
                db.executeQueryReader(query);
                dt.Load(db.getReader());

                bs = new BindingSource();
                bs.DataSource = dt;

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = bs;

                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            }
            this.username = ("The Detail Adventures of " + username);
            label1.Text = this.username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ketika tombol kemabali di klik
            this.frmTPermainan.Show();
            this.Hide();
            
        }

        private void FormDetailPermainan_Load(object sender, EventArgs e)
        {

        }

        private void FormDetailPermainan_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (statusOpen == true)
            {
                db.closeConnection();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();       //form3 tidak ditampilkan
            this.form6.Show(); //form6 ditampilkan
        }
    }
}
