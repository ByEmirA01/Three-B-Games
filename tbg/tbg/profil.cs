using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace tbg
{
    public partial class profil : Form
    {
        public profil()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        private void profil_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["form1"];
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Kullanici_tbl WHERE Kullanici_adi='"+form1.textBox1.Text+"';");
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label2.Text = dr["Kullanici_hakkinda"].ToString();
                label4.Text ="Bakiye : "+dr["Kullanici_bakiye"].ToString()+"₺";
                label1.Text = dr["Kullanici_adi"].ToString();
                if (dr["Kullanici_rol"].ToString() == "1")
                {
                    groupBox1.Visible = true;
                }
            }
            pictureBox2.ImageLocation = "./profile/" + label1.Text + ".jpg";
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            button3.Visible = true;
            button2.Visible = true;
            button1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" )
            {
                
            }
            else
            {
                conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
                cmd = new SqlCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = ("UPDATE Kullanici_tbl SET Kullanici_adi='" + textBox1.Text + "' WHERE Kullanici_adi='" + label1.Text + "'; UPDATE Kullanici_tbl SET Kullanici_hakkinda='" + textBox2.Text + "'WHERE Kullanici_adi='" + label1.Text + "';");
                dr = cmd.ExecuteReader();
                conn.Close();
            }
            textBox1.Visible = false;
            textBox2.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Jpeg dosyası (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fotografDosyaYolu = openFileDialog1.FileName;
                byte[] fotograf = File.ReadAllBytes(fotografDosyaYolu);
                string dosyaYolu = Path.Combine(Application.StartupPath, "./profile/"+ label1.Text+".jpg");
                using (FileStream fileStream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    fileStream.Write(fotograf, 0, fotograf.Length);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cuzdan cuzdan = new Cuzdan();
            cuzdan.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kullanicilar kullanicilar = new kullanicilar();
            kullanicilar.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Doyunlar doyunlar = new Doyunlar();
            doyunlar.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            kategori kategori = new kategori();
            kategori.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            magaza magaza = new magaza();
            magaza.Close();
        }
    }
}
