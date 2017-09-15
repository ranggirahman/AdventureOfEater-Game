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
    public partial class Form4 : Form
    {
        private DBConnection db;
        private DataTable dt;
        private BindingSource bs;
        private bool statusOpen;

        private Form2 frmTPermainan;
        private string username;
        private string waktu;
        private int x;
        private int y;
        private Bitmap btm;
        private Timer timer;
        private Graphics g;
        private Graphics scg;
        private Bitmap[] imgb;                           // array image
        private int[] xb;
        private int[] yb;
        private System.Media.SoundPlayer mp;        // untuk memainkan sound
        private int score;


        public Form4()
        {
            InitializeComponent();
            
            db = new DBConnection();
            statusOpen = db.openConnection();

            score = 0;            
            frmTPermainan = new Form2();
            player.Left = this.Width/2;
            player.Top = this.Height/ 2;
            g = this.CreateGraphics();
            btm = new Bitmap(this.Width, this.Height);
            scg = Graphics.FromImage(btm);
            timer = new Timer();
            x = 0;
            y = 0;

            imgb = new Bitmap[9];
            xb = new int[9];
            yb = new int[9];

            imgb[0] = new Bitmap("../../Properties/Resource/1.png");
            xb[0] = 100;
            yb[0] = 0;

            // deklarasi tantangan
            imgb[1] = new Bitmap("../../Properties/Resource/2.png");
            imgb[2] = new Bitmap("../../Properties/Resource/3.png");
            imgb[3] = new Bitmap("../../Properties/Resource/4.png");
            imgb[4] = new Bitmap("../../Properties/Resource/5.png");
            imgb[5] = new Bitmap("../../Properties/Resource/6.png");
            imgb[6] = new Bitmap("../../Properties/Resource/7.png");
            imgb[7] = new Bitmap("../../Properties/Resource/8.png");
            imgb[8] = new Bitmap("../../Properties/Resource/9.png");

            // memainkan sound
            mp = new System.Media.SoundPlayer();
            mp.SoundLocation = "../../Properties/Resource/s.wav";
            mp.Load();
            // mp.Play();           // kalau sound mau dimainkan sekali
            

            timer.Interval = 2;
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        public void setFrmGame(Form2 frmTPermainan)
        {
            this.frmTPermainan = frmTPermainan;
        }

        public void setUsername(String tbUsername, String waktu)
        {
            //form3 masih dalam mdi yg sama, sodaraan
            this.username = tbUsername;
            this.waktu = waktu;
        }

        public void timer_tick(object sender, EventArgs e)
        {
            yb[0]++;

            scg.Clear(Color.White);
            label1.Text = ""+score;

            if ((y >= this.Width))
            {
                y = 0;
            }
            else
            {
                y = y + 2;
            }

            Rectangle imgp = new Rectangle(player.Left, player.Top, 50, 50);

            scg.DrawImage(imgb[0], new Point(x, y));
            Rectangle imgb1 = new Rectangle(x, y, 50, 50);
            scg.DrawImage(imgb[1], new Point(x + 300, y - 200));
            Rectangle imgb2 = new Rectangle(x + 300, y - 200, 50, 50);
            scg.DrawImage(imgb[2], new Point(x + 500, y - 150));
            Rectangle imgb3 = new Rectangle(x + 500, y - 150, 50, 50);
            scg.DrawImage(imgb[3], new Point(x + 800, y - 300));
            Rectangle imgb4 = new Rectangle(x + 800, y - 300, 50, 50);
            scg.DrawImage(imgb[4], new Point(x + 100, y - 400));
            Rectangle imgb5 = new Rectangle(x + 100, y - 400, 50, 50);
            scg.DrawImage(imgb[5], new Point(x + 200, y - 250));
            Rectangle imgb6 = new Rectangle(x + 200, y - 250, 50, 50);
            scg.DrawImage(imgb[6], new Point(x + 900, y - 200));
            Rectangle imgb7 = new Rectangle(x + 900, y - 200, 50, 50);
            scg.DrawImage(imgb[7], new Point(x + 200, y - 320));
            Rectangle imgb8 = new Rectangle(x + 200, y - 320, 50, 50);
            scg.DrawImage(imgb[8], new Point(x + 450, y - 130));
            Rectangle imgb9 = new Rectangle(x + 450, y - 130, 50, 50);

            g.DrawImage(btm, Point.Empty);

            if (imgp.IntersectsWith(imgb1) || imgp.IntersectsWith(imgb2) || imgp.IntersectsWith(imgb3) || imgp.IntersectsWith(imgb4) || imgp.IntersectsWith(imgb5) || imgp.IntersectsWith(imgb6) || imgp.IntersectsWith(imgb7) || imgp.IntersectsWith(imgb8) || imgp.IntersectsWith(imgb9))
            {
                //MessageBox.Show("XXXX");
                //y = 0;
                score++;

            }

        }

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.A)
            {
                if (player.Left >= 0)
                {
                    player.Left = player.Left - 5;
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                if (player.Left <= (this.Width - (player.Width + 18)))
                {
                    player.Left = player.Left + 5;
                }
                e.Handled = true;
            }

            else if (e.KeyCode == Keys.W)
            {
                if (player.Top >= 0)
                {
                    player.Top = player.Top - 5;
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.S)
            {
                if (player.Top <= (this.Height - (player.Height + 43)))
                {
                    player.Top = player.Top + 5;
                }
                e.Handled = true;
            }else if (e.KeyCode == Keys.Space)
            {
                timer.Stop();
                mp.Stop();
                this.frmTPermainan.Show();
                this.Hide();

                string query = "INSERT INTO tjejak(Username, Waktu, Skor) values('" + username + "','" + waktu + "', '" +score+ "')";
                db.executeQueryReader(query);
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            mp.PlayLooping();       // sound dimainkan berulang2
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
            mp.Stop();
            this.frmTPermainan.Show();
            this.Hide();
        }
    }
}
