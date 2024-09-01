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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DESTEK_UYGULAMASI
{
    public partial class PersonelKaydi : Form
    {
        public PersonelKaydi()
        {
            InitializeComponent();
            dgv_personel.CellClick += new DataGridViewCellEventHandler(dgv_personel_CellClick);
        }
        string constring = "Data Source=ATURUTA;Initial Catalog=db_chat;Integrated Security=True";
        VTI.Veritabani vt = new VTI.Veritabani();
        //private void cmbmudurlukDoldur()
        //{
        //    //DataTable dt = vt.Select(@"select mudurluk_id ,mudurlukAD from tbl_mudurlukler");
        //    //cmb_mdr.DisplayMember = "mudurlukAD";
        //    //cmb_mdr.ValueMember = "mudurluk_id";
        //    //cmb_mdr.DataSource = dt;
        //}
        private void lstBoxDoldur()
        {
                DataTable dt = vt.Select(@"select a.personel_id,a.personelAdi,a.personelSoyadi,a.personelMail,a.personelsifre,a.personelonay,b.mudurlukAD ,a.personelresim
                from tbl_personel a 
                join tbl_mudurlukler b on a.mudurluk_id = b.mudurluk_id");
                dgv_personel.DataSource = dt;
            dgv_personel.Columns["personel_id"].Visible = false;
            dgv_personel.Columns["personelonay"].Visible = false;
            cmb_mdr.DisplayMember = "mudurlukAD";
            cmb_mdr.ValueMember = "mudurluk_id";
            cmb_mdr.DataSource = dt;
            dgv_personel.DataSource = dt;
            
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void PersonelKaydi_Load(object sender, EventArgs e)
        {
            lstBoxDoldur();
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
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen resim ekleyiniz");
            }
            else
            {
                if (string.IsNullOrEmpty(cmb_mdr.Text.Trim()))
                {
                    errorProvider1.SetError(cmb_mdr, "Müdürlük seçiniz ");
                    return;
                }
                else
                {
                    errorProvider1.SetError(cmb_mdr, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_ad.Text.Trim()))
                {
                    errorProvider1.SetError(txt_ad, "Adınızı yazınız");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_ad, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_syd.Text.Trim()))
                {
                    errorProvider1.SetError(txt_syd, "Soyadınızı yazınız ");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_syd, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_ep.Text.Trim()))
                {
                    errorProvider1.SetError(txt_ep, "E-postanızı yazınız");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_ep, string.Empty);
                }
                string kullaniciEposta = txt_ep.Text.Trim();
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
                    errorProvider1.SetError(txt_srf, "Şifre giriniz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_srf, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_tkrsrf.Text.Trim()))
                {
                    errorProvider1.SetError(txt_tkrsrf, "Şifrenizi tekrar giriniz ");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_tkrsrf, string.Empty);
                }
                if (txt_srf.Text != txt_tkrsrf.Text)
                {
                    MessageBox.Show("Tekrar girilen şifre eşleşmedi yeniden deneyiniz! ");
                    return;
                }
                else
                {
                    SqlConnection con = new SqlConnection(constring);
                    string md5liSifre = MD5Sifrele(txt_srf.Text);
                    string md5liSifres = MD5Sifrele(txt_tkrsrf.Text);
                    string q2 = @"INSERT INTO tbl_personel 
                  (personelAdi, personelSoyadi, personelMail, personelsifre, personelonay,mudurluk_id, personelresim) 
                  VALUES 
                  (@personelAdi, @personelSoyadi, @personelMail, @personelsifre, @personelonay,@mudurluk_id,  @personelresim)";

                        using (SqlCommand cmd2 = new SqlCommand(q2, con))
                        {
                            MemoryStream me = new MemoryStream();
                            pictureBox1.Image.Save(me, pictureBox1.Image.RawFormat);
                            cmd2.Parameters.AddWithValue("@personelAdi", txt_ad.Text);
                            cmd2.Parameters.AddWithValue("@personelSoyadi", txt_syd.Text);
                            cmd2.Parameters.AddWithValue("@personelMail", txt_ep.Text);
                            cmd2.Parameters.AddWithValue("@personelsifre", md5liSifre);
                            cmd2.Parameters.AddWithValue("@personelonay", md5liSifres);
                            cmd2.Parameters.AddWithValue("@mudurluk_id", cmb_mdr.SelectedValue);
                            cmd2.Parameters.AddWithValue("@personelresim", me.ToArray());

                            con.Open();
                            cmd2.ExecuteNonQuery();
                        }
                    

   

                    txt_ad.Clear();
                    txt_syd.Clear();
                    txt_ep.Clear();
                    txt_srf.Clear();
                    txt_tkrsrf.Clear();
                    pictureBox1.Image = null;

                    MessageBox.Show("Personel kaydı yapıldı");
                    lstBoxDoldur();

                 }
              }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_personel.SelectedRows.Count == 0) {
                MessageBox.Show("Lütfen silmek istediğiniz personeli tablodan  seçiniz. ");
                return;
            }
            int kayitsay = vt.UpdateDelete("delete from tbl_personel where personel_id="
                    + dgv_personel.SelectedRows[0].Cells["personel_id"].Value);
            if(kayitsay > 0)
            {
                lstBoxDoldur();
                MessageBox.Show("Personel kaydı silindi ");

            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            string kullaniciEposta = txt_ep.Text.Trim();
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
            if (dgv_personel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellemek istediğiniz personeli seçip ondan sonra gerekli yerleri kutucuklar sayesinde düzeltebilirsiniz .");
                return;
            }
            if (txt_srf.Text != txt_tkrsrf.Text)
            {
                MessageBox.Show("Tekrar girilen şifre eşleşmedi yeniden deneyiniz! ");
                return;
            }
            else
            {
                string md5liSifre = MD5Sifrele(txt_srf.Text);
                string md5liSifres = MD5Sifrele(txt_tkrsrf.Text);
                int dt = vt.UpdateDelete(@"update tbl_personel 
            set personelAdi='" + txt_ad.Text + "', personelSoyadi='" + txt_syd.Text + "',personelMail='" + txt_ep.Text 
            + "',personelsifre='" + md5liSifre + "',personelonay='" + md5liSifres + "',mudurluk_id='" + cmb_mdr.SelectedValue + "' where personel_id= "
             + dgv_personel.SelectedRows[0].Cells["personel_id"].Value);
                if (dt > 0)
                {
                    lstBoxDoldur();
                    MessageBox.Show("Personelin kaydı güncellendi");
                }
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            txt_ad.Text = "";
            txt_syd.Text = "";
            txt_srf.Text = "";
            txt_tkrsrf.Text = "";
            txt_ep.Text = "";
            cmb_mdr.SelectedIndex = -1;
            pictureBox1.Image = null;
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

        private void txt_ep_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
                Application.Restart();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "select resim(*Jpg; *.png; *Gif|*.Jpg; *.png; *Gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void dgv_personel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_personel.Rows[e.RowIndex];

                txt_ad.Text = row.Cells["personelAdi"].Value.ToString();
                txt_syd.Text = row.Cells["personelSoyadi"].Value.ToString();
                txt_ep.Text = row.Cells["personelMail"].Value.ToString();
                txt_srf.Text = row.Cells["personelsifre"].Value.ToString();
                txt_tkrsrf.Text = row.Cells["personelonay"].Value.ToString();
                cmb_mdr.SelectedItem = row.Cells["mudurlukAD"];

                byte[] img = (byte[])row.Cells["personelresim"].Value;
                using (MemoryStream ms = new MemoryStream(img))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            }
        }

        private void eKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen resim ekleyiniz");
            }
            else
            {
                if (string.IsNullOrEmpty(cmb_mdr.Text.Trim()))
                {
                    errorProvider1.SetError(cmb_mdr, "Müdürlük seçiniz ");
                    return;
                }
                else
                {
                    errorProvider1.SetError(cmb_mdr, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_ad.Text.Trim()))
                {
                    errorProvider1.SetError(txt_ad, "Adınızı yazınız");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_ad, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_syd.Text.Trim()))
                {
                    errorProvider1.SetError(txt_syd, "Soyadınızı yazınız ");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_syd, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_ep.Text.Trim()))
                {
                    errorProvider1.SetError(txt_ep, "E-postanızı yazınız");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_ep, string.Empty);
                }
                string kullaniciEposta = txt_ep.Text.Trim();
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
                    errorProvider1.SetError(txt_srf, "Şifre giriniz");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_srf, string.Empty);
                }
                if (string.IsNullOrEmpty(txt_tkrsrf.Text.Trim()))
                {
                    errorProvider1.SetError(txt_tkrsrf, "Şifrenizi tekrar giriniz ");
                    return;
                }
                else
                {
                    errorProvider1.SetError(txt_tkrsrf, string.Empty);
                }
                if (txt_srf.Text != txt_tkrsrf.Text)
                {
                    MessageBox.Show("Tekrar girilen şifre eşleşmedi yeniden deneyiniz! ");
                    return;
                }
                else
                {
                    SqlConnection con = new SqlConnection(constring);
                    string md5liSifre = MD5Sifrele(txt_srf.Text);
                    string md5liSifres = MD5Sifrele(txt_tkrsrf.Text);
                    string q2 = @"INSERT INTO tbl_personel 
                  (personelAdi, personelSoyadi, personelMail, personelsifre, personelonay,mudurluk_id, personelresim) 
                  VALUES 
                  (@personelAdi, @personelSoyadi, @personelMail, @personelsifre, @personelonay,@mudurluk_id,  @personelresim)";

                    using (SqlCommand cmd2 = new SqlCommand(q2, con))
                    {
                        MemoryStream me = new MemoryStream();
                        pictureBox1.Image.Save(me, pictureBox1.Image.RawFormat);
                        cmd2.Parameters.AddWithValue("@personelAdi", txt_ad.Text);
                        cmd2.Parameters.AddWithValue("@personelSoyadi", txt_syd.Text);
                        cmd2.Parameters.AddWithValue("@personelMail", txt_ep.Text);
                        cmd2.Parameters.AddWithValue("@personelsifre", md5liSifre);
                        cmd2.Parameters.AddWithValue("@personelonay", md5liSifres);
                        cmd2.Parameters.AddWithValue("@mudurluk_id", cmb_mdr.SelectedValue);
                        cmd2.Parameters.AddWithValue("@personelresim", me.ToArray());

                        con.Open();
                        cmd2.ExecuteNonQuery();
                    }




                    txt_ad.Clear();
                    txt_syd.Clear();
                    txt_ep.Clear();
                    txt_srf.Clear();
                    txt_tkrsrf.Clear();
                    pictureBox1.Image = null;

                    MessageBox.Show("Personel kaydı yapıldı");
                    lstBoxDoldur();

                }
            }
        }

        private void sİLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv_personel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz personeli tablodan  seçiniz. ");
                return;
            }
            int kayitsay = vt.UpdateDelete("delete from tbl_personel where personel_id="
                    + dgv_personel.SelectedRows[0].Cells["personel_id"].Value);
            if (kayitsay > 0)
            {
                lstBoxDoldur();
                MessageBox.Show("Personel kaydı silindi ");

            }
        }

        private void gÜNCELLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string kullaniciEposta = txt_ep.Text.Trim();
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
            if (dgv_personel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellemek istediğiniz personeli seçip ondan sonra gerekli yerleri kutucuklar sayesinde düzeltebilirsiniz .");
                return;
            }
            if (txt_srf.Text != txt_tkrsrf.Text)
            {
                MessageBox.Show("Tekrar girilen şifre eşleşmedi yeniden deneyiniz! ");
                return;
            }
            else
            {
                string md5liSifre = MD5Sifrele(txt_srf.Text);
                string md5liSifres = MD5Sifrele(txt_tkrsrf.Text);
                int dt = vt.UpdateDelete(@"update tbl_personel 
            set personelAdi='" + txt_ad.Text + "', personelSoyadi='" + txt_syd.Text + "',personelMail='" + txt_ep.Text
            + "',personelsifre='" + md5liSifre + "',personelonay='" + md5liSifres + "',mudurluk_id='" + cmb_mdr.SelectedValue + "' where personel_id= "
             + dgv_personel.SelectedRows[0].Cells["personel_id"].Value);
                if (dt > 0)
                {
                    lstBoxDoldur();
                    MessageBox.Show("Personelin kaydı güncellendi");
                }
            }
        }

        private void tEMİZLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_ad.Text = "";
            txt_syd.Text = "";
            txt_srf.Text = "";
            txt_tkrsrf.Text = "";
            txt_ep.Text = "";
            cmb_mdr.SelectedIndex = -1;
            pictureBox1.Image = null;
        }

        private void gİRİŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void aNASAYFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Close();
        }

        private void çIKIŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
