using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DESTEK_UYGULAMASI.Models;

namespace VTI
{
    internal class Veritabani
    {
        public Veritabani()
        {
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=ATURUTA;Initial Catalog=db_chat;Integrated Security=True");
        SqlCommand komut = new SqlCommand();
        SqlDataAdapter adtr = new SqlDataAdapter();
        DataTable dt = new DataTable();


        public DataTable Select(string sorgu)
        {
            dt = new DataTable();
            if (baglanti.State != ConnectionState.Open)
                baglanti.Open();

            komut.CommandText = sorgu;
            komut.Connection = baglanti;

            adtr.SelectCommand = komut;
            adtr.Fill(dt);

            baglanti.Close();

            return dt;
        }

        public object Insert(string sorgu)
        {
            dt = new DataTable();
            if (baglanti.State != ConnectionState.Open)
                baglanti.Open();
            komut.CommandText = sorgu;
            komut.Connection = baglanti;
            int kayitSay = komut.ExecuteNonQuery();
            baglanti.Close();

            return kayitSay;
        }

        public int UpdateDelete(string sorgu)
        {
            dt = new DataTable();
            if (baglanti.State != ConnectionState.Open)
                baglanti.Open();
            komut.CommandText = sorgu;
            komut.Connection = baglanti;
            int kayitSay = komut.ExecuteNonQuery();
            baglanti.Close();

            return kayitSay;
        }

        public List<Mudurluk> GetMudurlukler()
        {
            List<Mudurluk> mudurlukler= new List<Mudurluk>();
            string q = "select mudurluk_id,mudurlukAD from tbl_mudurlukler ";
            DataTable dt = Select(q);
            foreach (DataRow row in dt.Rows)
            {
                mudurlukler.Add(new Mudurluk()
                {
                    Id = Convert.ToInt32(row["mudurluk_id"].ToString()),
                    Name = row["mudurlukAD"].ToString(),

                });
            }
            return mudurlukler;
        }


    }
}

