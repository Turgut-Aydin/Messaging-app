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
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
        {
            InitializeComponent();
        }
        private string baslik;
        public string Baslik
        {
            get { return baslik; }
            set { baslik = value; label1.Text = value; AddHeightText(); }
        }
        void AddHeightText()
        {
            UserControl2 c = new UserControl2();    
            c.BringToFront();
            label1.Height = UIuserlist.GetTextHeight(label1) + 10;
            c.Height=label1.Top+label1.Height;
            this.Height=c.Bottom+10;

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserControl2_Load(object sender, EventArgs e)
        {
            AddHeightText();
        }
    }
}
