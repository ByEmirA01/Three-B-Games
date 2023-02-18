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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace tbg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;    
        SqlDataReader dr;
        private void button1_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");//tbg veri tabanı çekildi.
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * FROM Kullanici_tbl where Kullanici_adi='" + textBox1.Text + "' AND Kullanici_sifre='" + textBox2.Text + "'");//veri tabanından kullanıcı adı textbox1 ve kullanıcı şifresi textbox2 olan kullanıcı var mı diye kontrol sağlandı.
            dr = cmd.ExecuteReader();
            if (dr.Read())//eğer varsa çalışcak komutlar oluşturuldu.
            {
                magaza magaza = new magaza();
                magaza.Show();
                oyunlar oyunlar = new oyunlar();
                magaza.panel7.Controls.Clear();
                oyunlar.TopLevel = false;
                magaza.panel7.Controls.Add(oyunlar);
                oyunlar.Show();
                oyunlar.Dock = DockStyle.Fill;
                oyunlar.BringToFront();
                this.Hide();
                
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Yada Şifre Hatalı.");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//uygulamayı alta atılmasını sağlayan kod.
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();//uyulamayı kapatır.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            if (textBox1.Text == "" || textBox2.Text == "")
            {
               MessageBox.Show("Lüfen Boşluk Bırakmayınız !");
            }
            else
            {
                conn.Open();
                string sorgu = "Insert into Kullanici_tbl(Kullanici_adi,Kullanici_sifre)" +
              "values (@Kullanici_adi,@Kullanici_sifre)";//kullanıcı adı ve şifre girilen verilere göre veri tabanına eklendi.
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@Kullanici_adi", textBox1.Text);
                komut.Parameters.AddWithValue("@Kullanici_sifre", textBox2.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayınız Oluşturulmuştur.");
                Form1 magaza = new Form1();
                magaza.ShowDialog();
                conn.Close();
            }
        }
    }
}
