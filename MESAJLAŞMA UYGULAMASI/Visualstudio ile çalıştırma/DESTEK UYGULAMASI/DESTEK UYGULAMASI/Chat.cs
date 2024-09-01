
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace DESTEK_UYGULAMASI
{
    public partial class Chat : Form
    {
        private readonly AnaSayfa ana;
        public Chat()
        {
            InitializeComponent();
            //ana = sayfa;
            // UserControl1, UserControl2 ve UserControl3'ü ekle
            //UserControl1 userControl1 = new UserControl1();
            //flowLayoutPanel2.Controls.Add(userControl1);

            //UserControl2 userControl2 = new UserControl2();
            //flowLayoutPanel2.Controls.Add(userControl2);

            //UserControl3 userControl3 = new UserControl3();
            //flowLayoutPanel2.Controls.Add(userControl3);
        }
        VTI.Veritabani vt = new VTI.Veritabani();
        string consrtring = "Data Source=ATURUTA;Initial Catalog=db_chat;Integrated Security=True";





        private string baslik;
        public string Baslik
        {
            get { return baslik; }
            set { baslik = value; hlk_eposta.Text = value; }
        }

        private Image resim;
        public Image Resim
        {
            get { return resim; }
            set { resim = value; }
        }
        private string ad;
        public string Ad
        {
            get { return ad; }
            set { ad = value; }
        }
        private string soyad;
        public string Soyad
        {
            get { return soyad; }
            set { soyad = value; }
        }
        private void UserControl1_Click(object sender, EventArgs e)
        {
            UserControl1 clickedControl = sender as UserControl1;
            if (clickedControl != null)
            {
                SqlConnection con = new SqlConnection(consrtring);
                con.Open();
                //string q = "select halk_id  from tbl_halk where eposta ='" + clickedControl.Tag.ToString() + "' ";
                string q = "select a.* ,  b.eposta from tbl_mesaj a " +
                    "join tbl_halk b on a.halk_id = b.halk_id" +
                    " where eposta='" + clickedControl.Tag.ToString() + "' ";
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                sqlDataReader.Read();
                if (sqlDataReader.HasRows)
                {
                    label7.Tag = sqlDataReader["halk_id"];
                }
                con.Close();



                label7.Text = clickedControl.Tag.ToString();

                pictureBox3.Image = clickedControl.Resim;

                //DataRow clickedRow = clickedControl.Tag as DataRow;

                //if (clickedRow != null)
                //{
                //    // E-posta adresini göster
                //    label7.Text = clickedRow["gonderen_eposta"].ToString();

                //    // Resmi göster

                //using (MemoryStream stream = new MemoryStream((byte[])row["resim"]))
                //{
                //    pictureBox3.Image = new Bitmap(stream);
                //}

                //    // İlgili mesajları yükleyin veya diğer işlemleri yapın
                //LoadMessages(clickedControl.Tag.ToString());
                Mesaj(hlk_eposta.Text.ToString(), clickedControl.Tag.ToString());
            }
        }
        #region


        //private void LoadMessages(string eposta)
        //{
        //    if (button2.Tag.ToString() == "Personel")
        //    {
        //        string gondereneposta = hlk_eposta.Text;
        //        string gonderen = label3.Text;
        //        string alans = label6.Text;
        //        string alan = label7.Text;
        //        using (SqlConnection conn = new SqlConnection(consrtring))
        //        {
        //            conn.Open();

        //            string query = "select * from tbl_mesaj where gonderen_eposta=@gonderen_eposta and alan_eposta= @alan_eposta";

        //            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
        //            {
        //                adapter.SelectCommand.Parameters.AddWithValue("@gonderen_eposta", gondereneposta);
        //                adapter.SelectCommand.Parameters.AddWithValue("@alan_eposta", alan);

        //                DataTable table = new DataTable();
        //                adapter.Fill(table);

        //                flowLayoutPanel2.Controls.Clear();

        //                foreach (DataRow row in table.Rows)
        //                {
        //                    if (gondereneposta == row["gonderen_eposta"].ToString() && alan == row["alan_eposta"].ToString())
        //                    {
        //                        UserControl2 userControl2 = new UserControl2
        //                        {
        //                            Dock = DockStyle.Right,
        //                            Baslik = row["mesaj"].ToString()
        //                        };
        //                        flowLayoutPanel2.Controls.Add(userControl2);
        //                        flowLayoutPanel2.ScrollControlIntoView(userControl2);
        //                        UserControl3 userControl3 = new UserControl3
        //                        {
        //                            Dock = DockStyle.Left,
        //                            Baslik = row["mesaj"].ToString(),
        //                            Resim = pictureBox3.Image
        //                        };
        //                        flowLayoutPanel2.Controls.Add(userControl3);
        //                        flowLayoutPanel2.ScrollControlIntoView(userControl3);
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    else if (button2.Tag.ToString() == "Halk")
        //    {

        //        string gondereneposta = label6.Text;
        //        string alan = label3.Text;
        //        using (SqlConnection conn = new SqlConnection(consrtring))
        //        {
        //            conn.Open();

        //            string query = "select * from tbl_mesaj where gonderen_eposta=@gonderen_eposta and alan_eposta= @alan_eposta";

        //            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
        //            {
        //                adapter.SelectCommand.Parameters.AddWithValue("@gonderen_eposta", gondereneposta);
        //                adapter.SelectCommand.Parameters.AddWithValue("@alan_eposta", alan);

        //                DataTable table = new DataTable();
        //                adapter.Fill(table);

        //                flowLayoutPanel2.Controls.Clear();

        //                foreach (DataRow row in table.Rows)
        //                {
        //                    if (gondereneposta == row["gonderen_eposta"].ToString() && alan == row["alan_eposta"].ToString())
        //                    {
        //                        UserControl2 userControl2 = new UserControl2
        //                        {
        //                            Dock = DockStyle.Right,
        //                            Baslik = row["mesaj"].ToString()
        //                        };
        //                        flowLayoutPanel2.Controls.Add(userControl2);
        //                        flowLayoutPanel2.ScrollControlIntoView(userControl2);
        //                        UserControl3 userControl3 = new UserControl3
        //                        {
        //                            Dock = DockStyle.Left,
        //                            Baslik = row["mesaj"].ToString(),
        //                            Resim = pictureBox3.Image
        //                        };
        //                        flowLayoutPanel2.Controls.Add(userControl3);
        //                        flowLayoutPanel2.ScrollControlIntoView(userControl3);
        //                    }
        //                }
        //            }
        //        }
        //    }


        //}
        #endregion
        private void KullaniciItem()
        {
            flowLayoutPanel1.Controls.Clear();
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT a.gonderen_eposta, b.resim FROM (SELECT halk_id, gonderen_eposta FROM tbl_mesaj WHERE personel_id = @personel_id GROUP BY gonderen_eposta, halk_id) a JOIN tbl_halk b ON a.halk_id = b.halk_id", consrtring))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@personel_id", hlk_eposta.Tag);

                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    if (row["gonderen_eposta"].ToString() != hlk_eposta.Text)
                    {
                        UserControl1 userControls = new UserControl1
                        {
                            Baslik = row["gonderen_eposta"].ToString(),
                            Tag = row["gonderen_eposta"].ToString()
                        };

                        using (MemoryStream stream = new MemoryStream((byte[])row["resim"]))
                        {
                            userControls.Resim = new Bitmap(stream);
                        }
                        userControls.Click += new EventHandler(UserControl1_Click);
                        //userControls.Click += UserControl1_Click;
                        flowLayoutPanel1.Controls.Add(userControls);
                    }

                }
            }
            //flowLayoutPanel2.Controls.Clear();
            //using (SqlDataAdapter adapter = new SqlDataAdapter("select a.gonderen_eposta,b.resim from  (select halk_id, gonderen_eposta from tbl_mesaj where personel_id=" + hlk_eposta.Tag + " group by gonderen_eposta,halk_id)  a join tbl_halk b on a.halk_id = b.halk_id ", consrtring))
            //{
            //    DataTable table = new DataTable();
            //    adapter.Fill(table);
            //    if (table.Rows.Count > 0)
            //    {
            //        foreach (DataRow row in table.Rows)
            //        {
            //            UserControl1 userControls = new UserControl1();
            //            using (MemoryStream stream = new MemoryStream((byte[])row["resim"]))
            //            {
            //                userControls.Resim = new Bitmap(stream);
            //            }
            //            userControls.Baslik = row["gonderen_eposta"].ToString();
            //            flowLayoutPanel1.Controls.Add(userControls);
            //            //userControls.Click += button1_Click;
            //        }
            //    }
            //}
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            //Timer timer = new Timer();
            //timer.Interval = (10 * 1000);
            //timer.Tick += timer1_Tick;
            //timer.Start();

            //UserControl1 control1 = (UserControl1)sender;
            //label3.Text= control1.Baslik;
            //pictureBox1.Image= control1.Resim;
            //Mesaj();
            perProfilinigöster();



            if (Giris.eMailPersonel != "")
            {
                KullaniciItem();
                panel4.Visible = false;
                panel1.Visible = false;
                panel5.Visible = true;
                button2.Tag = "Personel";


            }
            else if (Giris.eMailHalk != "")
            {
                halkProfilDoldur();
                panel3.Visible = true;
                panel5.Visible = false;
                button2.Tag = "Halk";
                string alanss = gperMail.Text;
                string gonderenss = ghalkEposta.Text;

                Mesaj(gonderenss, alanss);
            }





        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (button2.Tag.ToString() == "Personel")
            {
                string gondereneposta = hlk_eposta.Text;
                int gonderenid = (int)hlk_eposta.Tag;
                int alanid = (int)label7.Tag;
                string alan = label7.Text;
                using (SqlConnection conn = new SqlConnection(consrtring))
                {
                    conn.Open();
                    string queryInsertMessage = "INSERT INTO tbl_mesaj(halk_id, personel_id, mesaj, zaman, aktif,gonderen_eposta,alan_eposta) VALUES (@halk_id, @personel_id, @mesaj, @zaman, @aktif,@gonderen_eposta,@alan_eposta)";
                    using (SqlCommand cmdInsertMessage = new SqlCommand(queryInsertMessage, conn))
                    {
                        cmdInsertMessage.Parameters.AddWithValue("@halk_id", alanid);
                        cmdInsertMessage.Parameters.AddWithValue("@personel_id", gonderenid);
                        cmdInsertMessage.Parameters.AddWithValue("@mesaj", textBox1.Text);
                        cmdInsertMessage.Parameters.AddWithValue("@zaman", DateTime.Now);
                        cmdInsertMessage.Parameters.AddWithValue("@aktif", true);
                        cmdInsertMessage.Parameters.AddWithValue("@gonderen_eposta", gondereneposta);
                        cmdInsertMessage.Parameters.AddWithValue("@alan_eposta", alan);
                        cmdInsertMessage.ExecuteNonQuery();
                        conn.Close();
                    }
                    textBox1.Clear();
                }
                Mesaj(hlk_eposta.Text, label7.Text);
            }

            if (button2.Tag.ToString() == "Halk")
            {
                string gondereneposta = ghalkEposta.Text;
                int gonderenid = (int)ghalkEposta.Tag;
                string alan = gperMail.Text;
                int alanid = (int)gperMail.Tag;
                using (SqlConnection conn = new SqlConnection(consrtring))
                {
                    conn.Open();
                    string queryInsertMessage = "INSERT INTO tbl_mesaj(halk_id, personel_id, mesaj, zaman, aktif,gonderen_eposta,alan_eposta) VALUES (@halk_id, @personel_id, @mesaj, @zaman, @aktif,@gonderen_eposta,@alan_eposta)";
                    using (SqlCommand cmdInsertMessage = new SqlCommand(queryInsertMessage, conn))
                    {
                        cmdInsertMessage.Parameters.AddWithValue("@halk_id", gonderenid);
                        cmdInsertMessage.Parameters.AddWithValue("@personel_id", alanid);
                        cmdInsertMessage.Parameters.AddWithValue("@mesaj", textBox1.Text);
                        cmdInsertMessage.Parameters.AddWithValue("@zaman", DateTime.Now);
                        cmdInsertMessage.Parameters.AddWithValue("@aktif", true);
                        cmdInsertMessage.Parameters.AddWithValue("@gonderen_eposta", gondereneposta);
                        cmdInsertMessage.Parameters.AddWithValue("@alan_eposta", alan);
                        cmdInsertMessage.ExecuteNonQuery();
                        conn.Close();
                    }
                    textBox1.Clear();
                }

                Mesaj(ghalkEposta.Text, gperMail.Text);

            }


            #region

            //using (SqlConnection conn = new SqlConnection)
            //{
            //    conn.Open();
            //    string kayit = "select a.* , b.eposta , c.personelMail from tbl_mesaj a " +
            //        "join tbl_halk b on a.halk_id = b.halk_id" +
            //        "join tbl_personel c on a.personel_id = c.personel_id " +
            //        "where b.eposta = @eposta ";
            //    using (SqlCommand cmdHalkId = new SqlCommand(kayit, conn))

            //        cmdHalkId.Parameters.AddWithValue("@eposta", label6.Text);



            //}




            //using (SqlConnection conn = new SqlConnection(consrtring))
            //{
            //    conn.Open();

            //    string queryHalkId = "SELECT halk_id FROM tbl_halk WHERE eposta = @eposta";
            //    using ()
            //    {
            //        cmdHalkId.Parameters.AddWithValue("@eposta", label6.Text);
            //        object halkIdObj = cmdHalkId.ExecuteScalar();
            //        if (halkIdObj == null)
            //        {
            //            throw new Exception("Halk ID bulunamadı.");
            //        }
            //        int halkId = (int)halkIdObj;

            //        string queryPersonelId = "SELECT personel_id FROM tbl_personel WHERE personelMail = @personelMail";
            //        using (SqlCommand cmdPersonelId = new SqlCommand(queryPersonelId, conn))
            //        {
            //            cmdPersonelId.Parameters.AddWithValue("@personelMail", label3.Text);
            //            object personelIdObj = cmdPersonelId.ExecuteScalar();
            //            if (personelIdObj == null)
            //            {
            //                throw new Exception("Personel ID bulunamadı.");
            //            }
            //            int personelId = (int)personelIdObj;
            //            DataTable table = new DataTable();
            //            adapter.Fill(table);
            //            if (label7.Text == row["eposta"].ToString() && hlk_eposta.Text == row["personelMail"].ToString())
            //            {
            //                string queryInsertMessage = "INSERT INTO tbl_mesaj(halk_id, personel_id, mesaj, zaman, aktif) VALUES (@halk_id, @personel_id, @mesaj, @zaman, @aktif)";
            //                using (SqlCommand cmdInsertMessage = new SqlCommand(queryInsertMessage, conn))
            //                {
            //                    cmdInsertMessage.Parameters.AddWithValue("@halk_id", halkId);
            //                    cmdInsertMessage.Parameters.AddWithValue("@personel_id", personelId);
            //                    cmdInsertMessage.Parameters.AddWithValue("@mesaj", textBox1.Text);
            //                    cmdInsertMessage.Parameters.AddWithValue("@zaman", DateTime.Now);
            //                    cmdInsertMessage.Parameters.AddWithValue("@aktif", true);

            //                    cmdInsertMessage.ExecuteNonQuery();
            //                }
            //            }


            //            perMesaj();
            //            textBox1.Clear();
            //        }
            //    }
            //}



            //SqlConnection conn = new SqlConnection(consrtring);
            //SqlConnection conns = new SqlConnection(consrtring);
            //SqlConnection connss = new SqlConnection(consrtring);
            //conn.Open();

            //// eposta eşleşen tbl_halk'tan halk_id almak
            //string s = "select halk_id from tbl_halk where eposta = @eposta";
            //SqlCommand cms = new SqlCommand(s, conn);
            //cms.Parameters.AddWithValue("@eposta", hlk_eposta.Text);

            //DataTable dataTable = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cms);
            //da.Fill(dataTable);

            //int halkid = 0;
            //if (dataTable.Rows.Count > 0)
            //{
            //    halkid = Convert.ToInt32(dataTable.Rows[0]["halk_id"]);
            //}
            //string personelMail = label3.Text; 
            //string q1 = "SELECT personel_id FROM tbl_personel WHERE personelMail = @personelMail";
            //SqlCommand cmd1 = new SqlCommand(q1, conns);
            //cmd1.Parameters.AddWithValue("@personelMail", personelMail);

            //conns.Open();
            //object result = cmd1.ExecuteScalar();
            //conns.Close();
            //if (result != null)
            //{
            //    int personelId = Convert.ToInt32(result);

            //    string q2 = "INSERT INTO tbl_mesaj(halk_id, personel_id, mesaj, zaman, aktif) VALUES (@halk_id, @personel_id, @mesaj, @zaman, @aktif)";
            //    SqlCommand cmd2 = new SqlCommand(q2, connss);
            //    cmd2.Parameters.AddWithValue("@halk_id", halkid);
            //    cmd2.Parameters.AddWithValue("@personel_id", personelId);
            //    cmd2.Parameters.AddWithValue("@mesaj", textBox1.Text);
            //    cmd2.Parameters.AddWithValue("@zaman", DateTime.Now);
            //    cmd2.Parameters.AddWithValue("@aktif", true);

            //    connss.Open();
            //    cmd2.ExecuteNonQuery();
            //    connss.Close();

            //    Mesaj();
            //    textBox1.Clear();
            //}
            //else
            //{
            //    MessageBox.Show("Personel bulunamadı.");
            //}
            //// Mesajı tbl_mesaj tablosuna eklemek
            //string q = "insert into tbl_mesaj(halk_id, personel_id, mesaj, zaman, aktif) values (@halk_id, @personel_id, @mesaj, @zaman, @aktif)";
            //SqlCommand cmd = new SqlCommand(q, conn);
            //cmd.Parameters.AddWithValue("@halk_id", halkid);
            //cmd.Parameters.AddWithValue("@personel_id", label1); // personel_id'nin bir yerde tanımlandığını varsayıyorum
            //cmd.Parameters.AddWithValue("@mesaj", textBox1.Text);
            //cmd.Parameters.AddWithValue("@zaman", DateTime.Now); // Şu anki zamanı eklemek istediğinizi varsayıyorum
            //cmd.Parameters.AddWithValue("@aktif", true); // aktif'in true olarak ayarlandığını varsayıyorum

            //cmd.ExecuteNonQuery();
            //conn.Close();
            //Mesaj();
            //textBox1.Clear();   
            #endregion
        }

        public void Mesaj(string gonderen, string alan)
        {
            #region


            //if (id.ToString()=="Personel")
            //{
            //    string gondereneposta = hlk_eposta.Text;
            //    string gonderen = label3.Text;
            //    string alans = label6.Text;
            //    string alan = label7.Text;
            //    using (SqlConnection conn = new SqlConnection(consrtring))
            //    {
            //        conn.Open();

            //        string query = "select * from tbl_mesaj where gonderen_eposta=@gonderen_eposta and alan_eposta= @alan_eposta";

            //        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            //        {
            //            adapter.SelectCommand.Parameters.AddWithValue("@gonderen_eposta", gondereneposta);
            //            adapter.SelectCommand.Parameters.AddWithValue("@alan_eposta", alan);

            //            DataTable table = new DataTable();
            //            adapter.Fill(table);

            //            flowLayoutPanel2.Controls.Clear();

            //            foreach (DataRow row in table.Rows)
            //            {
            //                if (gondereneposta == row["gonderen_eposta"].ToString() && alan == row["alan_eposta"].ToString())
            //                {
            //                    UserControl2 userControl2 = new UserControl2
            //                    {
            //                        Dock = DockStyle.Right,
            //                        Baslik = row["mesaj"].ToString()
            //                    };
            //                    flowLayoutPanel2.Controls.Add(userControl2);
            //                    flowLayoutPanel2.ScrollControlIntoView(userControl2);
            //                    UserControl3 userControl3 = new UserControl3
            //                    {
            //                        Dock = DockStyle.Left,
            //                        Baslik = row["mesaj"].ToString(),
            //                        Resim = pictureBox3.Image
            //                    };
            //                    flowLayoutPanel2.Controls.Add(userControl3);
            //                    flowLayoutPanel2.ScrollControlIntoView(userControl3);
            //                }

            //            }
            //        }
            //    }
            //}
            //else if (id.ToString() == "Halk")
            //{

            //    string gondereneposta = label6.Text;
            //    string alan = label3.Text;
            //    using (SqlConnection conn = new SqlConnection(consrtring))
            //    {
            //        conn.Open();

            //        string query = "select * from tbl_mesaj where gonderen_eposta=@gonderen_eposta and alan_eposta= @alan_eposta";

            //        using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            //        {
            //            adapter.SelectCommand.Parameters.AddWithValue("@gonderen_eposta", gondereneposta);
            //            adapter.SelectCommand.Parameters.AddWithValue("@alan_eposta", alan);

            //            DataTable table = new DataTable();
            //            adapter.Fill(table);

            //            flowLayoutPanel2.Controls.Clear();

            //            foreach (DataRow row in table.Rows)
            //            {
            //                if (gondereneposta == row["gonderen_eposta"].ToString() && alan == row["alan_eposta"].ToString())
            //                {
            //                    UserControl2 userControl2 = new UserControl2
            //                    {
            //                        Dock = DockStyle.Right,
            //                        Baslik = row["mesaj"].ToString()
            //                    };
            //                    flowLayoutPanel2.Controls.Add(userControl2);
            //                    flowLayoutPanel2.ScrollControlIntoView(userControl2);
            //                    UserControl3 userControl3 = new UserControl3
            //                    {
            //                        Dock = DockStyle.Left,
            //                        Baslik = row["mesaj"].ToString(),
            //                        Resim = pictureBox3.Image
            //                    };
            //                    flowLayoutPanel2.Controls.Add(userControl3);
            //                    flowLayoutPanel2.ScrollControlIntoView(userControl3);
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion
            #region
            // denendi onaylandı bu kod çalışır ama kime göre
            //SqlConnection conn = new SqlConnection(consrtring);
            //conn.Open();
            //SqlDataAdapter adapter = new SqlDataAdapter("select * from tbl_mesaj", consrtring);
            //DataTable table = new DataTable();
            //adapter.Fill(table);

            //flowLayoutPanel2.Controls.Clear(); 

            //if (table != null && table.Rows.Count > 0)
            //{
            //    for (int i = 0; i < table.Rows.Count; i++)
            //    {
            //        DataRow row = table.Rows[i];

            //        UserControl2 userControl2 = new UserControl2();
            //        userControl2.Dock = DockStyle.Top;
            //        userControl2.BringToFront();
            //        userControl2.Baslik = row["mesaj"].ToString();

            //        UserControl3 userControl3 = new UserControl3();
            //        userControl3.Dock = DockStyle.Top;
            //        userControl3.BringToFront();
            //        userControl3.Baslik = row["mesaj"].ToString();

            //        if (hlk_eposta.Text == row["gonderen_eposta"].ToString() && label7.Text == row["alan_eposta"].ToString())
            //        {
            //            userControl3.Resim = pictureBox3.Image;
            //        }
            //        else if (label6.Text == row["gonderen_eposta"].ToString() && label3.Text == row["alan_eposta"].ToString())
            //        {
            //            userControl3.Resim = pictureBox1.Image;
            //        }

            //        flowLayoutPanel2.Controls.Add(userControl2);
            //        flowLayoutPanel2.Controls.Add(userControl3);
            //    }

            //    // Son eklenen kontrolü görünür hale getir
            //    flowLayoutPanel2.ScrollControlIntoView(flowLayoutPanel2.Controls[flowLayoutPanel2.Controls.Count - 1]);
            //}

            //conn.Close();

            #endregion
            #region
            ////real code select* from tbl_mesaj
            //SqlConnection conn = new SqlConnection(consrtring);

            //conn.Open();
            //SqlDataAdapter adapter = new SqlDataAdapter("select * from tbl_mesaj  ", consrtring);
            //DataTable table = new DataTable();
            //adapter.Fill(table);

            //if (table != null && table.Rows.Count > 0)
            //{
            //    UserControl2[] userControl2s = new UserControl2[table.Rows.Count];
            //    UserControl3[] userControl3s = new UserControl3[table.Rows.Count];
            //    for (int i = 0; i < table.Rows.Count; i++)
            //    {
            //        DataRow row = table.Rows[i];
            //        if (hlk_eposta.Text == row["gonderen_eposta"].ToString() && label7.Text == row["alan_eposta"].ToString())
            //        {
            //            userControl2s[i] = new UserControl2();
            //            userControl2s[i].Dock = DockStyle.Top;
            //            userControl2s[i].BringToFront();
            //            userControl2s[i].Baslik = row["mesaj"].ToString();
            //            flowLayoutPanel2.Controls.Add(userControl2s[i]);
            //            flowLayoutPanel2.ScrollControlIntoView(userControl2s[i]);
            //            userControl3s[i] = new UserControl3();
            //            userControl3s[i].Dock = DockStyle.Top;
            //            userControl3s[i].BringToFront();
            //            userControl3s[i].Baslik = row["mesaj"].ToString();
            //            userControl3s[i].Resim = pictureBox3.Image;
            //            flowLayoutPanel2.Controls.Add(userControl2s[i]);
            //            flowLayoutPanel2.ScrollControlIntoView(userControl2s[i]);
            //        }
            //        else if (label6.Text == row["gonderen_eposta"].ToString() && label3.Text == row["alan_eposta"].ToString())
            //        {
            //            userControl2s[i] = new UserControl2();
            //            userControl2s[i].Dock = DockStyle.Top;
            //            userControl2s[i].BringToFront();
            //            userControl2s[i].Baslik = row["mesaj"].ToString();
            //            flowLayoutPanel2.Controls.Add(userControl2s[i]);
            //            flowLayoutPanel2.ScrollControlIntoView(userControl2s[i]);
            //            userControl3s[i] = new UserControl3();
            //            userControl3s[i].Dock = DockStyle.Top;
            //            userControl3s[i].BringToFront();
            //            userControl3s[i].Baslik = row["mesaj"].ToString();
            //            userControl3s[i].Resim = pictureBox1.Image;
            //            flowLayoutPanel2.Controls.Add(userControl2s[i]);
            //            flowLayoutPanel2.ScrollControlIntoView(userControl2s[i]);
            //        }
            //    }
            //}
            //conn.Close();
            #endregion
            #region
            //SqlConnection conn = new SqlConnection(consrtring);
            //conn.Open();
            //SqlDataAdapter adapter = new SqlDataAdapter("select * from tbl_mesaj", consrtring);
            //DataTable table = new DataTable();
            //adapter.Fill(table);

            //flowLayoutPanel2.Controls.Clear(); // Önceki kontrolleri temizle

            //if (table != null && table.Rows.Count > 0)
            //{
            //    foreach (DataRow row in table.Rows)
            //    {
            //        
            //        bool isSenderCurrentUser = hlk_eposta.Text == row["gonderen_eposta"].ToString();
            //        bool isReceiverCurrentUser = label7.Text == row["alan_eposta"].ToString();
            //        bool isSenderOtherUser = label6.Text == row["gonderen_eposta"].ToString();
            //        bool isReceiverOtherUser = label3.Text == row["alan_eposta"].ToString();

            //        if ((isSenderCurrentUser && isReceiverCurrentUser) || (isSenderOtherUser && isReceiverOtherUser))
            //        {
            //            UserControl2 userControl2 = new UserControl2();
            //            userControl2.Dock = DockStyle.Top;
            //            userControl2.Baslik = row["mesaj"].ToString();
            //            flowLayoutPanel2.Controls.Add(userControl2);

            //            UserControl3 userControl3 = new UserControl3();
            //            userControl3.Dock = DockStyle.Top;
            //            userControl3.Baslik = row["mesaj"].ToString();
            //            userControl3.Resim = isSenderCurrentUser ? pictureBox3.Image : pictureBox1.Image;
            //            flowLayoutPanel2.Controls.Add(userControl3);
            //        }
            //    }

            //    if (flowLayoutPanel2.Controls.Count > 0)
            //    {
            //        flowLayoutPanel2.ScrollControlIntoView(flowLayoutPanel2.Controls[flowLayoutPanel2.Controls.Count - 1]);
            //    }
            //}

            //conn.Close();
            #endregion
            #region

            //foreach (var message in table.Rows)
            //{
            //    if (message. == currentUserId)
            //    {
            //        // Mesaj gönderen kullanıcıysa sağa hizalar
            //        Console.WriteLine($"{message.Text}");
            //    }
            //    else
            //    {
            //        // Mesaj alan kullanıcıysa sola hizalar
            //        Console.WriteLine($"\t{message.Text}");
            //    }
            //}
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter = new SqlDataAdapter("select * from tbl_mesaj  ", consrtring);
            //DataTable table = new DataTable();
            //adapter.Fill(table);
            //if (table != null)
            //{
            //    UserControl2[] userControl2s = new UserControl2[table.Rows.Count];
            //    UserControl3[] userControl3s = new UserControl3[table.Rows.Count];
            //    for (int i = 0; i < 1; i++)
            //    {
            //        foreach (DataRow row in table.Rows)
            //        {
            //            if (hlk_eposta.Text == row["gonderen_eposta"].ToString() && label7.Text == row["alan_eposta"].ToString())
            //            {
            //                userControl2s[i] = new UserControl2();
            //                userControl2s[i].Dock = DockStyle.Top;
            //                userControl2s[i].BringToFront();
            //                userControl2s[i].Baslik = row["mesaj"].ToString();
            //                flowLayoutPanel2.Controls.Add(userControl2s[i]);
            //                flowLayoutPanel2.ScrollControlIntoView(userControl2s[i]);

            //            }
            //            else if (label6.Text == row["gonderen_eposta"].ToString() && label3.Text == row["alan_eposta"].ToString())
            //            {

            //                userControl3s[i] = new UserControl3();
            //                userControl3s[i].Dock = DockStyle.Top;
            //                userControl3s[i].BringToFront();
            //                userControl3s[i].Baslik = row["mesaj"].ToString();
            //                userControl3s[i].Resim = pictureBox1.Image;
            //                flowLayoutPanel2.Controls.Add(userControl2s[i]);
            //                flowLayoutPanel2.ScrollControlIntoView(userControl2s[i]);
            //            }
            //        }
            //    }
            //}
            #endregion
            SqlConnection conn = new SqlConnection(consrtring);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(" select * from (select * from tbl_mesaj where gonderen_eposta='" + gonderen + "' or alan_eposta='"
                + gonderen + "') t where t.gonderen_eposta='" + alan + "' or t.alan_eposta='" + alan + "'", consrtring);
            DataTable table = new DataTable();
            adapter.Fill(table);

            flowLayoutPanel2.Controls.Clear();


            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //flowLayoutPanel2.Controls.Clear();
                    DataRow row = table.Rows[i];

                    ////bool isSenderCurrentUser = hlk_eposta.Text == row["gonderen_eposta"].ToString();
                    //bool isSenderCurrentUser = gonderen == row["gonderen_eposta"].ToString();
                    ////bool isReceiverCurrentUser = label7.Text == row["alan_eposta"].ToString();
                    //bool isReceiverCurrentUser = alan == row["@alan"].ToString();
                    //bool isSenderOtherUser = label6.Text == row["@gonderen"].ToString();
                    //bool isReceiverOtherUser = label3.Text == row["@alan"].ToString();

                    if (row["gonderen_eposta"].ToString() == gonderen)
                    {
                        UserControl2 userControl2 = new UserControl2();
                        userControl2.Dock = DockStyle.Top;
                        userControl2.Baslik = row["mesaj"].ToString();
                        flowLayoutPanel2.Controls.Add(userControl2);
                    }
                    else
                    {
                        UserControl3 userControl3 = new UserControl3();
                        userControl3.Dock = DockStyle.Top;
                        userControl3.Baslik = row["mesaj"].ToString();
                        //userControl3.Resim = isSenderCurrentUser ? pictureBox1.Image : pictureBox3.Image;
                        userControl3.Resim = pictureBox3.Image;
                        userControl3.Resim1 = pictureBox1.Image;


                        flowLayoutPanel2.Controls.Add(userControl3);
                    }
                }

                // Kontroller varsa sonuncusunu görünür hale getir
                if (flowLayoutPanel2.Controls.Count > 0)
                {
                    flowLayoutPanel2.ScrollControlIntoView(flowLayoutPanel2.Controls[flowLayoutPanel2.Controls.Count - 1]);
                }
            }
            conn.Close();
        }


        public void halkProfilDoldur()
        {
            ghalkEposta.Text = Giris.eMailHalk;
            byte[] getresim = new byte[0];
            SqlConnection con = new SqlConnection(consrtring);
            con.Open();
            string q = "select * from tbl_halk where eposta ='" + ghalkEposta.Text + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            sqlDataReader.Read();
            if (sqlDataReader.HasRows)
            {
                ghalkEposta.Text = sqlDataReader["eposta"].ToString();
                ghalkEposta.Tag = sqlDataReader["halk_id"];
                label5.Text = sqlDataReader["ad"].ToString();
                label4.Text = sqlDataReader["soyad"].ToString();
                byte[] resim = (byte[])sqlDataReader["resim"];
                if (resim == null)
                {

                    pictureBox2.Image = null;

                }
                else
                {
                    MemoryStream me = new MemoryStream(resim);
                    pictureBox2.Image = Image.FromStream(me);
                }
            }
            con.Close();
            //label7.Text = Giris.eMailHalk;
            //byte[] getresims = new byte[0];
            //SqlConnection cons = new SqlConnection(consrtring);
            //cons.Open();
            //string qs = "select * from tbl_halk where eposta ='" + label7.Text + "' ";
            //SqlCommand cmds = new SqlCommand(qs, cons);
            //SqlDataReader sqlDataReaders = cmd.ExecuteReader();
            //sqlDataReaders.Read();
            //if (sqlDataReaders.HasRows)
            //{
            //    label7.Text = sqlDataReaders["eposta"].ToString();
            //    byte[] resims = (byte[])sqlDataReaders["resim"];
            //    if (resims == null)
            //    {

            //        pictureBox3.Image = null;

            //    }
            //    else
            //    {
            //        MemoryStream mes = new MemoryStream(resims);
            //        pictureBox3.Image = Image.FromStream(mes);
            //    }
            //}
            //con.Close();
        }
        public void perProfilinigöster()
        {
            hlk_eposta.Text = Giris.eMailPersonel;
            byte[] getresim1 = new byte[0];
            SqlConnection con1 = new SqlConnection(consrtring);
            con1.Open();
            string q1 = "select * from tbl_personel where personelMail ='" + hlk_eposta.Text + "' ";
            SqlCommand cmd1 = new SqlCommand(q1, con1);
            SqlDataReader sqlDataReader1 = cmd1.ExecuteReader();
            sqlDataReader1.Read();
            if (sqlDataReader1.HasRows)
            {
                hlk_eposta.Text = sqlDataReader1["personelMail"].ToString();
                hlk_eposta.Tag = sqlDataReader1["personel_id"];
                hlk_ad.Text = sqlDataReader1["personelAdi"].ToString();
                hlk_soyad.Text = sqlDataReader1["personelSoyadi"].ToString();
                byte[] resim1 = (byte[])sqlDataReader1["personelresim"];
                if (resim1 == null)
                {
                    pictureBox6.Image = null;
                }
                else
                {
                    MemoryStream me1 = new MemoryStream(resim1);
                    pictureBox6.Image = Image.FromStream(me1);
                }
            }
            con1.Close();
        }
        public void profiliGöster(int id)
        {
            byte[] getresim = new byte[0];
            SqlConnection con = new SqlConnection(consrtring);
            con.Open();
            string q = "select * from tbl_personel where mudurluk_id='" + id + "' ";
            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            sqlDataReader.Read();
            if (sqlDataReader.HasRows)
            {
                gperMail.Text = sqlDataReader["personelMail"].ToString();
                gperMail.Tag = sqlDataReader["personel_id"];
                label1.Text = sqlDataReader["personelAdi"].ToString();
                label2.Text = sqlDataReader["personelSoyadi"].ToString();
                byte[] resim = (byte[])sqlDataReader["personelresim"];
                if (resim == null)
                {
                    pictureBox6.Image = null;
                }
                else
                {
                    MemoryStream me = new MemoryStream(resim);
                    pictureBox1.Image = Image.FromStream(me);
                }
            }
            con.Close();
        }

        private void hlk_eposta_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Mesaj();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Close();
            flowLayoutPanel2.Controls.Clear();
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button23_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa1 = new AnaSayfa();
            anaSayfa1.Show();
            this.Close();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa1 = new AnaSayfa();
            anaSayfa1.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa1 = new AnaSayfa();
            anaSayfa1.Show();
            this.Close();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ChatKisayol(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void aNASAYFAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Close();
        }

        private void çIKIŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
