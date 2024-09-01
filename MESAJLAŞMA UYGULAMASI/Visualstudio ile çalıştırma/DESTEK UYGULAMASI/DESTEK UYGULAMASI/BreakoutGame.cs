using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DESTEK_UYGULAMASI;

namespace ATURUTA
{
    public partial class BreakoutGame : Form
    {
        bool goLeft;
        bool goRight;
        bool isGameover;
        int score;
        int ballx;
        int bally;
        int playerSpeed;
        Random rnd = new Random();
        PictureBox[] blockArray;
       
        private List<PictureBox> respawnList;

        private int halkId;
        private int userHighestScore;
        private int highestScore;

        public BreakoutGame()
        {
            InitializeComponent();
            PlaceBlocks();
            timer1 = new Timer();
            timer1.Interval = 3000; // 3 saniye
            timer1.Tick += RespawnTimer_Tick;
            respawnList = new List<PictureBox>();
            epostadanIDalma();

        }
        string consrtring = "Data Source=ATURUTA;Initial Catalog=db_chat;Integrated Security=True";
        private void setupGame()
        {
            isGameover = false;
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            txtScore.Text = "Score : " + score;
            ball.Left = 368;
            ball.Top = 316;
            player.Left = 381;
            gameTImer.Start();
            timer1.Start();
            yuksekskorGoster();
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }
        private void epostadanIDalma()
        {
        

            using (SqlConnection connection = new SqlConnection(consrtring))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT halk_id FROM tbl_halk WHERE eposta = @eposta";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@eposta", Giris.eMailHalk); 

                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        halkId = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Bu e-posta adresine ait bir kullanıcı bulunamadı.");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }
        private void yuksekskorGoster()
        {
            using (SqlConnection connection = new SqlConnection(consrtring))
            {
                try
                {
                    connection.Open();

                    // O kullanıcıya ait en yüksek skoru al
                    string userQuery = "SELECT MAX(CAST(skor AS int)) FROM tbl_game WHERE halk_id = @halkId";
                    SqlCommand userCommand = new SqlCommand(userQuery, connection);
                    userCommand.Parameters.AddWithValue("@halkId", halkId);

                    object userResult = userCommand.ExecuteScalar();

                    if (userResult != DBNull.Value && userResult != null)
                    {
                        userHighestScore = Convert.ToInt32(userResult);
                        yuksekskor.Text = "Kişisel Yüksek Skor: " + userHighestScore;
                    }
                    else
                    {
                        userHighestScore = 0;
                        yuksekskor.Text = "Kişisel Yüksek Skor: 0";
                    }

                    // Genel en yüksek skoru al
                    string generalQuery = "SELECT MAX(CAST(skor AS int)) FROM tbl_game";
                    SqlCommand generalCommand = new SqlCommand(generalQuery, connection);

                    object generalResult = generalCommand.ExecuteScalar();

                    if (generalResult != DBNull.Value && generalResult != null)
                    {
                        highestScore = Convert.ToInt32(generalResult);
                        enyuksekskor.Text = "Genel Yüksek Skor: " + highestScore;
                    }
                    else
                    {
                        highestScore = 0;
                        enyuksekskor.Text = "Genel Yüksek Skor: 0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }
        private void gameOver()
        {
            isGameover = true;
            gameTImer.Stop();
            timer1.Stop();
            txtScore.Text = "Score: " + score;

            // Skoru veritabanına kaydet
            skorKaydet();

            if (score > userHighestScore)
            {
                yuksekskor.Text = "Yeni Kişisel Yüksek Skor! " + score;
                userHighestScore = score; // En yüksek skoru güncelle
            }

            if (score > highestScore)
            {
                enyuksekskor.Text = "Yeni Genel Yüksek Skor! " + score;
                highestScore = score; // Genel en yüksek skoru güncelle
            }

            DialogResult result = MessageBox.Show("Devam etmek ister misiniz?", "Oyun Bitti", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                removeBlocks();
                PlaceBlocks();
            }
            else if (result == DialogResult.No)
            {
                this.Close();
                AnaSayfa anaSayfa = new AnaSayfa();
                anaSayfa.Show();
            }
        }
        private void skorKaydet()
        {
            using (SqlConnection connection = new SqlConnection(consrtring))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO tbl_game (halk_id, skor) VALUES (@halkId, @skor)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@halkId", halkId);
                    command.Parameters.AddWithValue("@skor", score);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
            }
        }

        private void PlaceBlocks()
        {
            blockArray = new PictureBox[15];
            int a = 0;
            int top = 50;
            int left = 100;
            for (int i = 0; i < blockArray.Length; i++)
            {
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 32;
                blockArray[i].Width = 100;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White;
                if (a == 5)
                {
                    top = top + 50;
                    left = 100;
                    a = 0;
                }
                if (a < 5)
                {
                    a++;
                    blockArray[i].Left = left;
                    blockArray[i].Top = top;
                    this.Controls.Add(blockArray[i]);
                    left = left + 130;
                }
            }
            setupGame();
        }

        private void removeBlocks()
        {
            foreach (PictureBox x in blockArray)
            {
                this.Controls.Remove(x);
            }
        }

        private void RespawnTimer_Tick(object sender, EventArgs e)
        {
            List<PictureBox> respawnedBlocks = new List<PictureBox>();

            foreach (PictureBox block in respawnList)
            {
                if (!this.Controls.Contains(block))
                {
                    block.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    this.Controls.Add(block);
                    respawnedBlocks.Add(block);
                }
            }

            foreach (PictureBox block in respawnedBlocks)
            {
                respawnList.Remove(block);
            }
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Right < 776)
            {
                player.Left += playerSpeed;
            }
            ball.Left += ballx;
            ball.Top += bally;
            if (ball.Left < 0 || ball.Left > 776)
            {
                ballx = -ballx;
            }
            if (ball.Top < 0)
            {
                bally = -bally;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                ball.Top = 456;
                bally = rnd.Next(5, 12) * -1;
                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;
                        this.Controls.Remove(x);
                        respawnList.Add((PictureBox)x);
                    }
                }
            }
            if (ball.Top > 568)
            {
                gameOver();
            }
        }

        private void keyisDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Enter && isGameover == true)
            {
                removeBlocks();
                PlaceBlocks();
            }
        }

        private void BreakoutGame_Load(object sender, EventArgs e)
        {
            yuksekskorGoster();
            timer1.Start();
        }

        private void çIKIŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Close();
        }
    }
}
