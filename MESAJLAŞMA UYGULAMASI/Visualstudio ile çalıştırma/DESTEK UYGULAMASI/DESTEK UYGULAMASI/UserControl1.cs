
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESTEK_UYGULAMASI
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();

            // PictureBox ve Label'a tıklama olaylarını bağla
            pictureBox1.Click += new EventHandler(UserControl_Click);
            label1.Click += new EventHandler(UserControl_Click);
        }

        private string baslik;
        public string Baslik
        {
            get { return baslik; }
            set { baslik = value; label1.Text = value; }
        }

        private Image resim;
        public Image Resim
        {
            get { return resim; }
            set { resim = value; pictureBox1.Image = value; }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void UserControl_Click(object sender, EventArgs e)
        {
            // Bu metod, PictureBox veya Label'a tıklandığında çağrılır
            OnClick(e); // Bu, UserControl'ün Click olayını tetikler
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

