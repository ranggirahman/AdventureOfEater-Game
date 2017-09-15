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
    public partial class Form2 : Form
    {
        private Form4 frmGame;
        private Form3 frmDetail;
        private Form5 form5;
        private DBConnection db;
        private DataTable dt;
        private BindingSource bs;
        private bool statusOpen;
      
        public Form2()
        {
            InitializeComponent();
            db = new DBConnection();
            statusOpen = db.openConnection();

            if (statusOpen)
            {
                string query = "SELECT * FROM tpermainan";

                //proses query dan tampilkan hasil di DataGridView
                dt = new DataTable();
                db.executeQueryReader(query);
                dt.Load(db.getReader());

                bs = new BindingSource();
                bs.DataSource = dt;

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = bs;

                //menambahkan kolom baru
                DataGridViewColumn newCol = new DataGridViewColumn();
                DataGridViewImageColumn image = new DataGridViewImageColumn();  //untuk iamge
                Image gambar = Image.FromFile("../../Properties/Resource/detail.png");   //load image di folder resource
                image.Image = gambar;   //menambahkan image ke colom baru
                image.HeaderText = "Detail";    //header kolom
                dataGridView1.Columns.Add(image);   //menambahkan kolom baru ke datagridview

                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //menambahkan button untuk detail permainan pada datagridview
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(0))
            {
                this.frmDetail.setJudul(dataGridView1[1, (dataGridView1.CurrentRow.Index)].Value.ToString());
                this.Hide();
                this.frmDetail.Show();
            }
        }

        public void setFrmGame(Form4 frmGame)
        {
            //untuk diset di form MDI
            this.frmGame = frmGame;
        }

        public void setFrmDetail(Form3 frmDetail)
        {
            //untuk diset di form MDI
            this.frmDetail = frmDetail;
        }

        public void setCetak(Form5 sodara)
        {
            this.form5 = sodara;
        }

        public bool getStatusOpen()
        {
            //mengambil status open 
            return statusOpen;
        }

        public void insertData(string tbUsername)
        {
            if (statusOpen)
            {
                string query = "INSERT INTO tpermainan(Username, Skor_Total, Jumlah) VALUES ('" + tbUsername + "', 0, 1)";
                db.executeQueryReader(query);
            }
        }

        private void insertData2(string tbUsername, string waktu)
        {
            if (statusOpen)
            {
                string query = "INSERT INTO tjejak(Username, Waktu, Skor) values('" + tbUsername + "','" + waktu + "', '0')";
                db.executeQueryReader(query);
            }
        }

        private void updateData(string tbUsername)
        {
            if (statusOpen)
            {
                string q = "UPDATE tpermainan SET Jumlah= Jumlah+'1' where Username = '" + tbUsername + "'";
                //proses query dan tampilkan hasil di grid view
                db.executeQueryReader(q);
            }
            
        }

        //ketika tombol main diklik
        private void btnMain_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")    //jika textBox ada isinya
            {
                string tbUsername = textBox1.Text;
                textBox1.Clear();

                if (statusOpen)
                {
                    string query = "SELECT * FROM tpermainan WHERE Username = '" + tbUsername + "'";
                    db.executeQueryReader(query);

                    List<string>[] data = db.getResult(1);
                    if (data.Any())
                    {
                        this.insertData(tbUsername);

                        this.updateData(tbUsername);
                    }
                    else
                    {
                        
                    }
                    string waktu = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    this.insertData2(tbUsername, waktu);
                    frmGame.Show();
                    this.Hide();
                }
            }
            else
            {
                // error handling kalau username kosong
                MessageBox.Show("Username tidak valid", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //menutup MDI
            this.MdiParent.Close();
        }

        //ketika form ditutup
        private void FormTPermainan_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (statusOpen == true)
            {
                db.closeConnection();
            }
        }

        private void FormTPermainan_Load(object sender, EventArgs e)
        {

        }

        private void btnCetak_Click(object sender, EventArgs e)
        {
            this.Hide();       //form2 tidak ditampilkan
            this.form5.Show(); //form5 ditampilkan
        }
    }
}
