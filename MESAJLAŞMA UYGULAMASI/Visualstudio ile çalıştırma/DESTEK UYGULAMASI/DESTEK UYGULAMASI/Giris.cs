using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace DESTEK_UYGULAMASI
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        VTI.Veritabani vt =new VTI.Veritabani();
        public static string eMailHalk="";
        public static string eMailPersonel = "";
        public int personel_id=0 ;

        string constring = "Data Source=ATURUTA;Initial Catalog=db_chat;Integrated Security=True";
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private bool isPasswordHidden = true;
        // Form'un constructor'ında veya uygun bir yerde Button'un Click eventine bu kodu bağlayın:
        //button5.Click += BtnTogglePassword_Click;

// Gizle/Göster butonuna tıklandığında çalışan metot:
        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            if (isPasswordHidden)
            {
                txt_prssrf.UseSystemPasswordChar = false; // Şifreyi göster
                txt_srf.UseSystemPasswordChar=false;
                button5.Text = "Gizle"; // Butonun metnini güncelle
                button7.Text = "Gizle";
            }
            else
            {
                txt_prssrf.UseSystemPasswordChar = true; // Şifreyi gizle
                txt_srf.UseSystemPasswordChar = true;
                button5.Text = "Göster"; // Butonun metnini güncelle
                button7.Text = "Göster"; // Butonun metnini güncelle
            }

            isPasswordHidden = !isPasswordHidden; // Durumu tersine çevir
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Ayarlar.Default.BeniHatirlaPer == true)
            {
                txt_prsmail.Text = Ayarlar.Default.mail;
                txt_prssrf.Text = Ayarlar.Default.persifre;
                perBeniHatirla.Checked = true;
            }
            if (Ayarlar.Default.BeniHatirlaHalk == true)
            {
                txt_posta.Text= Ayarlar.Default.eposta;
                txt_srf.Text=Ayarlar.Default.sifre;
                halkBeniHatirla.Checked=true;
            }
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
        }

        private void txt_ad_TextChanged(object sender, EventArgs e)
        {
            string input = txt_ad.Text;
            foreach (char c in input)
            {

                if (Char.IsDigit(c) || Char.IsPunctuation(c))
                {
                    MessageBox.Show("Lütfen ad kısmına sayı veya noktalama işareti girmeyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txt_ad.Text = input.Remove(input.IndexOf(c), 1);

                    txt_ad.SelectionStart = txt_ad.Text.Length;
                    break;
                }
            }
        }

        private void txt_syd_TextChanged(object sender, EventArgs e)
        {
            string input = txt_syd.Text;
            foreach (char c in input)
            {

                if (Char.IsDigit(c) || Char.IsPunctuation(c))
                {
                    MessageBox.Show("Lütfen ad kısmına sayı veya noktalama işareti girmeyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txt_syd.Text = input.Remove(input.IndexOf(c), 1);

                    txt_syd.SelectionStart = txt_syd.Text.Length;
                    break;
                }
            }
        }

        private void btn_kydyap_Click(object sender, EventArgs e)
        {
            if (rsmhalk.Image == null)
            {
                MessageBox.Show("Resim giriniz !");
                
            }
           
            else
            {
                
                if (string.IsNullOrEmpty(txt_ad.Text.Trim()))
                {
                    errorProvider1.SetError(txt_ad, "Ad boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_ad, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_syd.Text.Trim()))
                {
                    errorProvider1.SetError(txt_syd, "Soyad boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_syd, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_mail.Text.Trim()))
                {
                    errorProvider1.SetError(txt_mail, "Eposta boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_mail, string.Empty);
                }
                string kullaniciEposta = txt_mail.Text.Trim();
                string epostaRegex = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com)$";

                if (System.Text.RegularExpressions.Regex.IsMatch(kullaniciEposta, epostaRegex))
                {
                    MessageBox.Show("E-posta adresi geçerli.");
                }
                else
                {
                    MessageBox.Show("Lütfen @gmail.com veya @hotmail.com uzantılı geçerli bir e-posta adresi girin.");
                    return;
                }
                if (string.IsNullOrEmpty(txt_sifre.Text.Trim()))
                {
                    errorProvider1.SetError(txt_sifre, "Şifre boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_sifre, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_onay.Text.Trim()))
                {
                    errorProvider1.SetError(txt_onay, "Tekrar şifre boş bırakılamaz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_onay, string.Empty);
                }
                if (txt_sifre.Text != txt_onay.Text)
                {
                    MessageBox.Show("Tekrar girilen şifre eşleşmedi yeniden deneyiniz! ");
                    return;
                }
                else
                {
                    //errorProvider1.SetError(txt_onay, String.Empty);
                    SqlConnection con = new SqlConnection(constring);
                    string md5liSifre = MD5Sifrele(txt_sifre.Text);
                    string md5liSifres = MD5Sifrele(txt_onay.Text);
                    string q = "insert into tbl_halk(ad,soyad,eposta,sifre,onaykodu,resim)values(@ad,@soyad,@eposta,@sifre,@onaykodu,@resim)";
                    SqlCommand cmd = new SqlCommand(q, con);
                    MemoryStream me = new MemoryStream();
                    rsmhalk.Image.Save(me, rsmhalk.Image.RawFormat);
                    cmd.Parameters.AddWithValue("ad", txt_ad.Text);
                    cmd.Parameters.AddWithValue("soyad", txt_syd.Text);
                    cmd.Parameters.AddWithValue("eposta", txt_mail.Text);
                    cmd.Parameters.AddWithValue("sifre",md5liSifre);
                    cmd.Parameters.AddWithValue("onaykodu", md5liSifres);
                    cmd.Parameters.AddWithValue("resim", me.ToArray());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Kaydınız olmuştur giriş kısmından uygulamaya girişinizi yapabilirsiniz !");
                txt_ad.Clear();
                txt_syd.Clear();
                txt_mail.Clear();
                txt_sifre.Clear();
                txt_onay.Clear();
                rsmhalk.Image = null;
                }
           
            }
        }

        private void rsmhalk_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter="select resim(*Jpg; *.png; *Gif|*.Jpg; *.png; *Gif";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rsmhalk.Image=Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void txt_mail_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_posta.Text.Trim()))
            {
                errorProvider1.SetError(txt_posta, "Eposta boş bırakılamaz");
                return;
            }
            else
            {
                errorProvider1.SetError(txt_posta, string.Empty);
            }
            string kullaniciEposta = txt_posta.Text.Trim();
            string epostaRegex = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com)$";

            if (System.Text.RegularExpressions.Regex.IsMatch(kullaniciEposta, epostaRegex))
            {
                MessageBox.Show("E-posta adresi geçerli.");
            }
            else
            {
                MessageBox.Show("Lütfen @gmail.com veya @hotmail.com uzantılı geçerli bir e-posta adresi girin.");
                return;
            }
            if (string.IsNullOrEmpty(txt_srf.Text.Trim()))
            {
                errorProvider1.SetError(txt_srf, "Şifre boş bırakılamaz");
                return;
            }
            else
            {
                errorProvider1.SetError(txt_srf, string.Empty);
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                string md5liSifre = MD5Sifrele(txt_srf.Text);
                string q = "select * from  tbl_halk  where eposta ='" + txt_posta.Text + "' and sifre='" + md5liSifre + "'";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader dataReader;
                dataReader = cmd.ExecuteReader();
                if (dataReader.HasRows == true)
                {
                    if (halkBeniHatirla.Checked == true)
                    {
                        Ayarlar.Default.BeniHatirlaHalk = true;
                        Ayarlar.Default.eposta = txt_posta.Text;
                        Ayarlar.Default.sifre = txt_srf.Text;
                        Ayarlar.Default.Save();
                    }
                    eMailHalk = txt_posta.Text;
                    panel1.BringToFront();
                    AnaSayfa form2 = new AnaSayfa();
                 
                        form2.Show();
                    this.Hide();
                    txt_posta.Text = "";
                    txt_srf.Text = "";
                }
                else
                {
                    MessageBox.Show("Lütfen e postanızı ve şifrenizi kontrol ediniz ");
                }

                con.Close();
            }

            //DataTable dt = vt.Select(@"select halk_id,eposta,sifre from tbl_halk 
            //where eposta='" + txt_posta.Text + "' and sifre='" + txt_srf.Text + "'");
            //if(dt.Rows.Count == 0)
            //{
            //    MessageBox.Show("Girilen bilgiler hatalıdır lütfen tekrar deneyiniz");
            //    return ;
            //}
            //MessageBox.Show("Giriş bilgisi doğru Hoş geldiniz");
            //AnaSayfa form2 = new AnaSayfa();
            //form2.Hide();
            //form2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 2;
            }
            else
            {
                timer1.Stop();
                AnaSayfa f2 = new AnaSayfa();
                this.Hide();
                f2.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
        }

            private void button6_Click(object sender, EventArgs e)
            {
            #region
            //string kullaniciEposta = txt_prsmail.Text.Trim();
            //string epostaRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            //if (System.Text.RegularExpressions.Regex.IsMatch(kullaniciEposta, epostaRegex))
            //{
            //    MessageBox.Show("E-posta adresi geçerli.");

            //}
            //else
            //{
            //    MessageBox.Show("Lütfen geçerli bir e-posta adresi girin.");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txt_prsmail.Text.Trim()))
            //{
            //    errorProvider1.SetError(txt_prsmail, "Ad boş bırakılamaz");
            //    return;
            //}
            //else
            //{
            //    errorProvider1.SetError(txt_prsmail, string.Empty);
            //}
            //if (string.IsNullOrEmpty(txt_prssrf.Text.Trim()))
            //{
            //    errorProvider1.SetError(txt_prssrf, "Ad boş bırakılamaz");
            //    return;
            //}
            //else
            //{
            //    errorProvider1.SetError(txt_prssrf, string.Empty);
            //}
            //string md5liSifre = MD5Sifrele(txt_prssrf.Text);
            //DataTable dt = vt.Select(@"select personel_id,personelMail,personelsifre from tbl_personel
            //where personelMail='" + txt_prsmail.Text + "' and personelsifre='" +md5liSifre+ "'");
            //if (dt.Rows.Count == 0)
            //{
            //    MessageBox.Show("Girilen bilgiler hatalıdır \n lütfen tekrar deneyiniz");
            //    return;
            //}
            //if (perBeniHatirla.Checked == true)
            //{
            //    Ayarlar.Default.BeniHatirlaPer=true;
            //    Ayarlar.Default.mail = txt_prsmail.Text;
            //    Ayarlar.Default.persifre=txt_prssrf.Text;
            //    Ayarlar.Default.Save();
            //}
            //MessageBox.Show("Giriş bilgisi doğru! Hoş geldiniz");
            //eMailPersonel = txt_prsmail.Text;
            //Chat chat = new Chat();
            //chat.Show();
            //this.Hide();
            //txt_prsmail.Text = "";
            //txt_prssrf.Text = "";
            #endregion
            string kullaniciEposta = txt_prsmail.Text.Trim();
            string epostaRegex = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|hotmail\.com)$";

            if (System.Text.RegularExpressions.Regex.IsMatch(kullaniciEposta, epostaRegex))
            {
                MessageBox.Show("E-posta adresi geçerli.");
            }
            else
            {
                MessageBox.Show("Lütfen @gmail.com veya @hotmail.com uzantılı geçerli bir e-posta adresi girin.");
                return;
            }

            if (string.IsNullOrEmpty(txt_prsmail.Text.Trim()))
            {
                errorProvider1.SetError(txt_prsmail, "E-posta boş bırakılamaz");
                return;
            }
            else
            {
                errorProvider1.SetError(txt_prsmail, string.Empty);
            }

            if (string.IsNullOrEmpty(txt_prssrf.Text.Trim()))
            {
                errorProvider1.SetError(txt_prssrf, "Şifre boş bırakılamaz");
                return;
            }
            else
            {
                errorProvider1.SetError(txt_prssrf, string.Empty);
            }

            string md5liSifre = MD5Sifrele(txt_prssrf.Text);
            DataTable dt = vt.Select(@"SELECT personel_id, personelMail, personelsifre FROM tbl_personel
                               WHERE personelMail='" + txt_prsmail.Text + "' AND personelsifre='" + md5liSifre + "'");
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Girilen bilgiler hatalıdır \n lütfen tekrar deneyiniz");
                return;
            }

            // personel_id'yi kontrol etme
            int personelId = Convert.ToInt32(dt.Rows[0]["personel_id"]);
            if (personelId == 29)
            {
                MessageBox.Show("Personel kayıt formuna yönlendiriliyorsunuz.");
                PersonelKaydi personelKayitFormu = new PersonelKaydi();
                personelKayitFormu.Show();
                this.Hide();
            }
            else
            {
                if (perBeniHatirla.Checked == true)
                {
                    Ayarlar.Default.BeniHatirlaPer = true;
                    Ayarlar.Default.mail = txt_prsmail.Text;
                    Ayarlar.Default.persifre = txt_prssrf.Text;
                    Ayarlar.Default.Save();
                }

                MessageBox.Show("Giriş bilgisi doğru! Hoş geldiniz");
                eMailPersonel = txt_prsmail.Text;
                Chat chat = new Chat();
                chat.Show();
                this.Hide();
            }

            // Giriş bilgilerinin temizlenmesi
            txt_prsmail.Text = "";
            txt_prssrf.Text = "";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void txt_prsmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

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

        private void perBeniHatirla_CheckedChanged(object sender, EventArgs e)
        {
            if (perBeniHatirla.Checked == false) 
            { 
                Ayarlar.Default.BeniHatirlaPer=false;
                Ayarlar.Default.mail = "";
                Ayarlar.Default.persifre = "";
                Ayarlar.Default.Save();
            }
        }

        private void halkBeniHatirla_CheckedChanged(object sender, EventArgs e)
        {
            if(halkBeniHatirla.Checked == false)
            {
                Ayarlar.Default.BeniHatirlaHalk=false;
                Ayarlar.Default.eposta = "";
                Ayarlar.Default.sifre = "";
                Ayarlar.Default.Save();
            }
        }

        private void VatandasAndPersonel(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Enter)
            {
                if (txt_posta.Text != "" && txt_srf.Text != "")
                {
                    button3.PerformClick();
                }
               else if (txt_prsmail.Text!=""&& txt_prssrf.Text != "") 
                {
                    button6.PerformClick(); 
                }
            }

        }

        private void gİRİŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void kAYDOLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
        }

        private void pERSONELGİRİŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
        }

        private void txt_ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Harf mi kontrolü yapıyoruz
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Eğer harf değilse, karakteri engelliyoruz
                errorProvider1.SetError(txt_ad, "Ad sadece harflerden oluşmalıdır");
            }
            else
            {
                errorProvider1.SetError(txt_ad, string.Empty); // Hata mesajını temizle
            }
        }

        private void txt_syd_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Harf mi kontrolü yapıyoruz
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Eğer harf değilse, karakteri engelliyoruz
                errorProvider1.SetError(txt_syd, "Soyad sadece harflerden oluşmalıdır");
            }
            else
            {
                errorProvider1.SetError(txt_syd, string.Empty); // Hata mesajını temizle
            }
        }

        private void txt_prssrf_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
