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

namespace tbg
{
    public partial class Cuzdan : Form
    {
        public Cuzdan()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            button5.Visible = true;
            button4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            profil profil = (profil)Application.OpenForms["profil"];
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("UPDATE Kullanici_tbl SET Kullanici_bakiye=Kullanici_bakiye+10 WHERE Kullanici_adi='" + profil.label1.Text + "';");
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Hesabınıza " + label1.Text + " eklenmiştir görmek için sayfayı yenilemeniz gerekmektedir.");
        }

        private void Cuzdan_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Kullanici_tbl;");
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label4.Text = "Bakiye : " + dr["Kullanici_bakiye"].ToString() + "₺";
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            profil profil = (profil)Application.OpenForms["profil"];
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("UPDATE Kullanici_tbl SET Kullanici_bakiye=Kullanici_bakiye+20 WHERE Kullanici_adi='" + profil.label1.Text + "';");
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Hesabınıza " + label2.Text + " eklenmiştir görmek için sayfayı yenilemeniz gerekmektedir.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            profil profil = (profil)Application.OpenForms["profil"];
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("UPDATE Kullanici_tbl SET Kullanici_bakiye=Kullanici_bakiye+50 WHERE Kullanici_adi='" + profil.label1.Text + "';");
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Hesabınıza " + label3.Text + " eklenmiştir görmek için sayfayı yenilemeniz gerekmektedir.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            profil profil = (profil)Application.OpenForms["profil"];
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("UPDATE Kullanici_tbl SET Kullanici_bakiye=Kullanici_bakiye+100 WHERE Kullanici_adi='" + profil.label1.Text + "';");
            dr = cmd.ExecuteReader();
            conn.Close();
            MessageBox.Show("Hesabınıza " + label5.Text + " eklenmiştir görmek için sayfayı yenilemeniz gerekmektedir.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int value;
            if (int.TryParse(textBox1.Text, out value))//textboxın içinde sayı olup olmadığını kontrol ediyoruz.
            {
                if (value > 0)
                {
                    profil profil = (profil)Application.OpenForms["profil"];
                    conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
                    cmd = new SqlCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = ("UPDATE Kullanici_tbl SET Kullanici_bakiye=Kullanici_bakiye+" + textBox1.Text + " WHERE Kullanici_adi='" + profil.label1.Text + "';");
                    dr = cmd.ExecuteReader();
                    conn.Close();
                    MessageBox.Show("Hesabınıza " + textBox1.Text + "₺ eklenmiştir görmek için sayfayı yenilemeniz gerekmektedir.");
                }
                else
                {
                    MessageBox.Show("Lütfen sadece pozitif sayı giriniz!");
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(textBox1.Text, out value))
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }
    }
}
