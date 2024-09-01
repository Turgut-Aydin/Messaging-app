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
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();

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
            set { resim = value; pictureBox3.Image = value; AddHeightText(); }
        }
        private Image resim1;
        public Image Resim1
        {
            get { return resim1; }
            set { resim1 = value; pictureBox1.Image = value; AddHeightText(); }           
        }
   

        void AddHeightText()
        {
            UserControl3 userControl3 = new UserControl3();
            userControl3.BringToFront();
            label1.Height = UIuserlist.GetTextHeight(label1) + 10;
            userControl3.Height = label1.Top + label1.Height;
            this.Height = userControl3.Bottom + 10;
        }

        private void UserControl3_Load(object sender, EventArgs e)
        {
            AddHeightText();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl3_Load_1(object sender, EventArgs e)
        {
            pictureBox3.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        
        }
    }
}
//using System;
//using System.Drawing;
//using System.Reflection.Emit;
//using System.Windows.Forms;

//namespace DESTEK_UYGULAMASI
//{
//    public partial class UserControl3 : UserControl
//    {
//        public UserControl3()
//        {
//            InitializeComponent();
//        }

//        private string baslik;
//        public string Baslik
//        {
//            get { return baslik; }
//            set { baslik = value; label1.Text = value; }
//        }

//        private Image resim;
//        public Image Resim
//        {
//            get { return resim; }
//            set { resim = value; pictureBox3.Image = value; }
//        }

//        private void label1_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}

