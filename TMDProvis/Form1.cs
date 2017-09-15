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
    public partial class FormMDI : Form
    { 
        //deklarasi form atau objek yang ada di dalam form MDI
        Form2 frmTPermainan;
        Form3 frmDetail;
        Form4 frmGame;
        Form5 form5;    // form cetak tpermainan
        Form6 form6;    // form cetak tjejak

        public FormMDI()
        {
            InitializeComponent();
            //inisialisasi form TPermainan
            frmTPermainan = new Form2();
            frmTPermainan.MdiParent = this;

            
            form6 = new Form6();
            form6.MdiParent = this;
            form6.Hide();
            

            // form 5 cetak tpermaininan
            form5 = new Form5();
            form5.MdiParent = this;
            frmTPermainan.setCetak(form5);
            form5.Hide();
            form5.setBack(frmTPermainan);

            //inisalisasi form untuk tampilan game
            frmGame = new Form4();
            //frmGame.MdiParent = this;
            frmGame.Hide();
            //frmGame.setKembali(frmTPermainan);

            frmDetail = new Form3();
            frmDetail.MdiParent = this;
            frmDetail.Hide();
            frmDetail.setKembali(frmTPermainan);
            frmDetail.setCetak(form6);
            form6.setBack(frmDetail);

            //persiapan form TPermainan
            frmTPermainan.setFrmGame(frmGame);
            frmTPermainan.setFrmDetail(frmDetail);
            frmTPermainan.Show();    //agar frmTPermainan tampil ketika awal dieksekusi
        }

        private void FormMDI_Load(object sender, EventArgs e)
        {

        }
    }
}
