using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paintapp
{
    public partial class paintodev : Form
    {
        public paintodev()
        {
            InitializeComponent();
            bmp = new Bitmap(panel1.ClientSize.Width, panel1.ClientSize.Height);
        }
        Graphics g;
        bool ciz = false;
        int baslaX, baslaY;
        Bitmap bmp;
        Stack<Bitmap> gerial = new Stack<Bitmap>();
        private void kaydet_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 20; i++)
            {
                cmbkalempx.Items.Add(i + "px");
            }
        }
        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (ciz)
            {
                using (g = Graphics.FromImage(bmp))
                {
                    Pen p = new Pen(colorDialog1.Color, float.Parse(cmbkalempx.SelectedIndex.ToString()));
                    g.DrawLine(p, new Point(baslaX, baslaY), new Point(e.X, e.Y));
                    baslaX = e.X;
                    baslaY = e.Y;
                }
                panel1.Refresh();
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ciz = true;
            baslaX = e.X;
            baslaY = e.Y;
            gerial.Push((Bitmap)bmp.Clone());
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            ciz = false;
        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bmp, Point.Empty);
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Title = "Resim dosyaları";
            sfg.DefaultExt = ".png";
            sfg.Filter = "Resim Dosyaları (*.png)|*.png|Tüm Dosyalar(*.*)|*.*";
            if (sfg.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(sfg.FileName, ImageFormat.Png);
            }
        }
        private void Yeniac_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çalışmanız silinecek, devam etmek istediğinize emin misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    panel1.Refresh();
                }

            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (gerial.Count > 0)
            {
                bmp = gerial.Pop();
                g = Graphics.FromImage(bmp);
                panel1.Refresh();
            }
            else
            {
                MessageBox.Show("Geri alınacak çizim bulunamadı !");
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
    }
}
