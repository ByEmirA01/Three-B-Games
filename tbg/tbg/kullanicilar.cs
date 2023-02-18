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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using tbg.Properties;
using System.Runtime.CompilerServices;
using System.IO;

namespace tbg
{
    public partial class kullanicilar : Form
    {
        public kullanicilar()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlDataReader dr;
        SqlConnection conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
        public void olusturucu(int width, int top, int counter)
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            oyunlar oyunlar = (oyunlar)Application.OpenForms["oyunlar"];
            PictureBox ptb = new PictureBox();
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label();
            ////////////////////////////////////////
            btn.Enabled = true;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(59, 59, 59);
            btn.Size = new Size(40, 30);
            btn.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            btn.Image = Resources.outline_delete_white_18dp;
            btn.Left = (width * counter) + 140;
            btn.Top = top + 25;
            btn.Click += new EventHandler(btn_Click);
            void btn_Click(object sender, EventArgs e)
            {
                btn.Enabled = true;
                string sorgu = "Delete from Kullanici_tbl Where Kullanici_adi='" + lbl.Text + "';";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                conn.Open();
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Başarıyla Silinmiştir");
            }
            panel4.Controls.Add(btn);
            ////////////////////////////////////////
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl.Text = (string)dr["Kullanici_adi"];
            lbl.Left = (width * counter) + 55;
            lbl.Top = top + 25;
            lbl.Width = width;
            panel4.Controls.Add(lbl);
            ////////////////////////////////////////
            lbl2.ForeColor = Color.White;
            lbl2.BackColor = Color.Transparent;
            lbl2.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            if ((int)dr["Kullanici_rol"] == 1)
            {
                lbl2.Text = "Yönetici";
            }
            else if ((int)dr["Kullanici_rol"] == 2)
            {
                lbl2.Text = "Üye";
            }
            lbl2.Left = (width * counter) + 55;
            lbl2.Top = top + 50;
            lbl2.Width = width;
            panel4.Controls.Add(lbl2);
            ////////////////////////////////////////
            ptb.Size = new System.Drawing.Size(280, 120);
            ptb.SizeMode = PictureBoxSizeMode.Zoom;
            ptb.Left = (width * counter) + 20;
            ptb.Top = top - 90;
            ptb.Width = width;
            ptb.ImageLocation = "./profile/" + lbl.Text + ".jpg";
            panel4.Controls.Add(ptb);
            ////////////////////////////////////////
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kullanicilar_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Kullanici_tbl;");
            dr = cmd.ExecuteReader();
            int width = 200;
            int counter = 0;
            int top = 100;
            while (dr.Read())
            {
                olusturucu(width, top, counter);
                counter++;
                if (counter % 1 == 0)
                {
                    counter = 0;
                    top = top + 160;
                }
            }
            conn.Close();
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Rol_tbl");
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string item = dr["Rol_ad"].ToString();
                comboBox1.Items.Add(item);
            }
            comboBox1.SelectedIndex = 1;
            conn.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            comboBox1.Visible = true;
            button1.Visible = true;
            label3.Visible = true;
            textBox2.Visible = true;
            button2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            cmd = new SqlCommand();
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lüfen Boşluk Bırakmayınız !");
            }
            else
            {
                conn.Open();
                string sorgu = "Insert into Kullanici_tbl(Kullanici_adi,Kullanici_sifre,Kullanici_rol)" +
              "values (@Kullanici_adi,@Kullanici_sifre,@Kullanici_rol)";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@Kullanici_adi", textBox1.Text);
                komut.Parameters.AddWithValue("@Kullanici_sifre", textBox2.Text);
                komut.Parameters.AddWithValue("@Kullanici_rol", comboBox1.SelectedIndex+1);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayınız Oluşturulmuştur.");
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Jpeg dosyası (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fotografDosyaYolu = openFileDialog1.FileName;
                byte[] fotograf = File.ReadAllBytes(fotografDosyaYolu);
                string dosyaYolu = Path.Combine(Application.StartupPath, "./profile/" + textBox1.Text + ".jpg");
                using (FileStream fileStream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    fileStream.Write(fotograf, 0, fotograf.Length);
                }
            }
        }
    }
}
