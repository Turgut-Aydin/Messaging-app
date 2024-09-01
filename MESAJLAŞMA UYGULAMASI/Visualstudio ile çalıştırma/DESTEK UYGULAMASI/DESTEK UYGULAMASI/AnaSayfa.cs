using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATURUTA;
using DESTEK_UYGULAMASI.Models;
using DESTEK_UYGULAMASI.Properties;

namespace DESTEK_UYGULAMASI
{
    public partial class AnaSayfa : Form
    {

        public AnaSayfa()
        {
            InitializeComponent();
        }
        VTI.Veritabani vt = new VTI.Veritabani();
        string consrtring = "Data Source=ATURUTA;Initial Catalog=db_chat;Integrated Security=True";

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            panel3.Visible = false;
            halk_profil.Text = Giris.eMailHalk;
            byte[] getresim = new byte[0];
            SqlConnection con = new SqlConnection(consrtring);
            con.Open();
            string q = "select * from tbl_halk where eposta ='" + halk_profil.Text + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            sqlDataReader.Read();
            if (sqlDataReader.HasRows)
            {
                halk_profil.Text = sqlDataReader["eposta"].ToString();
                hlk_ad.Text = sqlDataReader["ad"].ToString();
                hlk_syd.Text = sqlDataReader["soyad"].ToString();
                halk_eposta.Text = sqlDataReader["eposta"].ToString();
                halk_sfr.Text = sqlDataReader["sifre"].ToString();
                hlk_tkrsfr.Text = sqlDataReader["onaykodu"].ToString();
                byte[] resim = (byte[])sqlDataReader["resim"];
                if (resim == null)
                {
                    halk_foto.Image = null;
                    pictureBox6.Image = null;

                }
                else
                {
                    MemoryStream me = new MemoryStream(resim);
                    halk_foto.Image = Image.FromStream(me);
                    pictureBox6.Image = Image.FromStream(me);
                }
            }
            con.Close();
            //profiliGöster();
            List<Mudurluk> mudurlukler = vt.GetMudurlukler();
            int x = 0;
            int y = 0;
            int buttonWidth = 100;
            int buttonHeight = 65;
            int horizontalSpacing = 20; // Butonlar arasındaki yatay boşluk
            int verticalSpacing = 20;   // Butonlar arasındaki dikey boşluk
            int buttonsPerRow = 5;

            for (int i = 0; i < mudurlukler.Count; i++)
            {
                var item = mudurlukler[i];

                // Buton oluşturma
                Button buttonClickMe = new Button();
                buttonClickMe.Text = item.Name;
                buttonClickMe.Name = "buttonClick" + item.Id;
                buttonClickMe.Tag = item.Id;
                buttonClickMe.Size = new System.Drawing.Size(buttonWidth, buttonHeight);
                buttonClickMe.Location = new System.Drawing.Point(100 + x, 70 + y);

                // Buton olayına olay işleyici ekleme
                buttonClickMe.Click += new EventHandler(ButtonClickMe_Click);

                // Butonu formun kontrollerine ekleme
                this.Controls.Add(buttonClickMe);

                x += buttonWidth + horizontalSpacing;

                // Her 4 butonda bir satırı değiştir
                if ((i + 1) % buttonsPerRow == 0)
                {
                    x = 0;
                    y += buttonHeight + verticalSpacing;
                }
            }

        }

        // Butonun olay işleyicisi
        private void ButtonClickMe_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            int buttonID = (int)clickedButton.Tag;
            Chat chat=new Chat();
            chat.profiliGöster(buttonID);
            chat.Show();
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void halk_profil_Click(object sender, EventArgs e)
        {

        }

        private bool check;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (check)
            {
                panel1.Width += 5;
                if (panel1.Size == panel1.MaximumSize)
                {

                    timer1.Stop();
                    check = false;
                    pictureBox3.Image = Resources.back;
                    panel3.Visible = false;
                }

            }
            else
            {
                panel1.Width -= 5;
                if (panel1.Size == panel1.MinimumSize)
                {

                    timer1.Stop();
                    check = true;
                    pictureBox3.Image = Resources.list;
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            BreakoutGame breakoutGame = new BreakoutGame();
            breakoutGame.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void hlk_ad_TextChanged(object sender, EventArgs e)
        {
            string input = hlk_ad.Text;
            foreach (char c in input)
            {

                if (Char.IsDigit(c) || Char.IsPunctuation(c))
                {
                    MessageBox.Show("Lütfen ad kısmına sayı veya noktalama işareti girmeyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    hlk_ad.Text = input.Remove(input.IndexOf(c), 1);

                    hlk_ad.SelectionStart = hlk_ad.Text.Length;
                    break;
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "select image (*Jpg; *.png; *Gif|*.Jpg; *.png; *Gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox6.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            panel3.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "select image (*Jpg; *.png; *Gif|*.Jpg; *.png; *Gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                halk_foto.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
        public void profiliGöster()
        {
            halk_profil.Text = Giris.eMailHalk;
            byte[] getresim = new byte[0];
            SqlConnection con = new SqlConnection(consrtring);
            con.Open();
            string q = "select * from tbl_halk where eposta ='" + halk_eposta.Text + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            sqlDataReader.Read();
            if (sqlDataReader.HasRows)
            {
                halk_profil.Text = sqlDataReader["eposta"].ToString();
                hlk_ad.Text = sqlDataReader["ad"].ToString();
                hlk_syd.Text = sqlDataReader["soyad"].ToString();
                halk_eposta.Text = sqlDataReader["eposta"].ToString();
                halk_sfr.Text = sqlDataReader["sifre"].ToString();
                hlk_tkrsfr.Text = sqlDataReader["onaykodu"].ToString();
                byte[] resim = (byte[])sqlDataReader["resim"];
                if (resim == null)
                {
                    halk_foto.Image = null;
                    pictureBox6.Image = null;

                }
                else
                {
                    MemoryStream me = new MemoryStream(resim);
                    halk_foto.Image = Image.FromStream(me);
                    pictureBox6.Image = Image.FromStream(me);
                }
            }
            con.Close();
        }
        public string MD5Sifrele(string sifrelenecekMetin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            dizi = md5.ComputeHash(dizi);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
        private void hlk_adkyt_Click(object sender, EventArgs e)
        {
            if (pictureBox6.Image == null)
            {
                MessageBox.Show("Resim giriniz !");

            }

            else
            {

                if (string.IsNullOrEmpty(hlk_ad.Text.Trim()))
                {
                    errorProvider1.SetError(hlk_ad, "Ad boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(hlk_ad, string.Empty);
                }
                if (string.IsNullOrEmpty(hlk_syd.Text.Trim()))
                {
                    errorProvider1.SetError(hlk_syd, "Soyad boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(hlk_syd, string.Empty);
                }
                if (string.IsNullOrEmpty(halk_eposta.Text.Trim()))
                {
                    errorProvider1.SetError(halk_eposta, "Eposta boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(halk_eposta, string.Empty);
                }
                string kullaniciEposta = halk_eposta.Text.Trim();
                string epostaRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                if (System.Text.RegularExpressions.Regex.IsMatch(kullaniciEposta, epostaRegex))
                {
                    MessageBox.Show("E-posta adresi geçerli.");

                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir e-posta adresi girin.");
                    return;
                }
                if (string.IsNullOrEmpty(halk_sfr.Text.Trim()))
                {
                    errorProvider1.SetError(halk_sfr, "Şifre boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(halk_sfr, string.Empty);
                }
                if (string.IsNullOrEmpty(hlk_tkrsfr.Text.Trim()))
                {
                    errorProvider1.SetError(hlk_tkrsfr, "Tekrar şifre boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(hlk_tkrsfr, string.Empty);
                }
                if (halk_sfr.Text != hlk_tkrsfr.Text)
                {
                    MessageBox.Show("Tekrar girilen şifre eşleşmedi yeniden deneyiniz! ");
                    return;
                }
                else
                {
                    SqlConnection con = new SqlConnection(consrtring);
                    con.Open();
                    string md5liSifre = MD5Sifrele(halk_sfr.Text);
                    string md5liSifres = MD5Sifrele(hlk_tkrsfr.Text);
                    string q = "updatedelete  tbl_halk set sifre = '" + md5liSifre + "',ad=@ad,soyad=@soyad,eposta=@eposta,onaykodu=@onaykodu,resim=@resim";
                    MemoryStream me = new MemoryStream();
                    pictureBox6.Image.Save(me, pictureBox6.Image.RawFormat);
                    SqlCommand cmd = new SqlCommand(q, con);
                    cmd.Parameters.AddWithValue("@ad", hlk_ad.Text);
                    cmd.Parameters.AddWithValue("@soyad", hlk_syd.Text);
                    cmd.Parameters.AddWithValue("@eposta", halk_eposta.Text);
                    cmd.Parameters.AddWithValue("@onaykodu", md5liSifres);
                    cmd.Parameters.AddWithValue("@resim", me.ToArray());
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("profil güncellendi");
                    panel3.Visible = false;
                    profiliGöster();
                }

            }

            //int kayitsay = vt.UpdateDelete(@"update tbl_halk set ad ='" + hlk_ad.Text + "',soyad='" + hlk_syd.Text + "',eposta='" + halk_eposta.Text + "',sifre='" + halk_sfr.Text + "',onaykodu='" + hlk_tkrsfr.Text + "',resim=@resim");
            //MemoryStream me = new MemoryStream();
            //pictureBox6.Image.Save(me, pictureBox6.Image.RawFormat);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panel3.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            BreakoutGame breakoutGame = new BreakoutGame();
            breakoutGame.Show();
            this.Close();
        }

        private void hlk_syd_TextChanged(object sender, EventArgs e)
        {
            string input = hlk_syd.Text;
            foreach (char c in input)
            {

                if (Char.IsDigit(c) || Char.IsPunctuation(c))
                {
                    MessageBox.Show("Lütfen ad kısmına sayı veya noktalama işareti girmeyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    hlk_syd.Text = input.Remove(input.IndexOf(c), 1);

                    hlk_syd.SelectionStart = hlk_syd.Text.Length;
                    break;
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int buttonID = (int)clickedButton.Tag;
            Chat chat = new Chat();
            chat.Show();
            chat.profiliGöster(buttonID);
            this.Close();
        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int buttonID = (int)clickedButton.Tag;
            Chat chat = new Chat();
            chat.Show();
            chat.profiliGöster(buttonID);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void pROFILToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
        }

        private void oYUNAÇToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BreakoutGame breakoutGame = new BreakoutGame();
            breakoutGame.Show();
            this.Close();
        }

        private void çIKIŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
