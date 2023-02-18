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
using tbg.Properties;

namespace tbg
{
    public partial class oyunlar : Form
    {
        public oyunlar()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        public void olusturucu(int width, int top, int counter)
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            oyunlar oyunlar = (oyunlar)Application.OpenForms["oyunlar"];
            PictureBox ptb = new PictureBox();
            Label lbl = new Label();
            Label lbl2 = new Label();
            ////////////////////////////////////////
            btn.Enabled = true;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(59, 59, 59);
            btn.Size = new Size(40, 30);
            btn.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            btn.Image = Resources.outline_shopping_basket_white_18dp;
            btn.Left = (width * counter) + 165;
            btn.Top = top + 25;
            btn.Click += new EventHandler(btn_Click);
            void btn_Click(object sender, EventArgs e)
            {
                conn.Open();
                string sorgu = "Insert into Sepet_tbl(Sepet_oyun_ad,Sepet_oyun_fiyat) values (@Sepet_oyun_ad,@Sepet_oyun_fiyat)";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@Sepet_oyun_ad", lbl.Text);
                komut.Parameters.AddWithValue("@Sepet_oyun_fiyat", lbl2.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Sepete Eklenmiştir.");
                btn.Enabled = false;
                conn.Close();
            }
            panel2.Controls.Add(btn);
            ////////////////////////////////////////
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl.Text = (string)dr["Oyun_ad"];
            lbl.Left = (width * counter) + 10;
            lbl.Top = top + 25;
            lbl.Width = width;
            panel2.Controls.Add(lbl);
            ////////////////////////////////////////
            lbl2.ForeColor = Color.White;
            lbl2.BackColor = Color.Transparent;
            lbl2.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl2.Text = dr["Oyun_fiyat"].ToString();
            lbl2.Left = (width * counter) + 10;
            lbl2.Top = top + 50;
            lbl2.Width = width;
            panel2.Controls.Add(lbl2);
            ////////////////////////////////////////
            ptb.Size = new System.Drawing.Size(280, 120);
            ptb.SizeMode = PictureBoxSizeMode.Zoom;
            ptb.Left = (width * counter) + 10;
            ptb.Top = top - 90;
            ptb.Width = width;
            ptb.ImageLocation = "./Games/" + lbl.Text + ".jpg";
            panel2.Controls.Add(ptb);
            ////////////////////////////////////////
        }
        void formgetir(Form frm)
        {
            panel2.Controls.Clear();
            frm.TopLevel = false;
            panel2.Controls.Add(frm);
            frm.Show();
            frm.Dock = DockStyle.Fill;
            frm.BringToFront();
        }
        private void oyunlar_Load(object sender, EventArgs e)
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
                label1.Text = "Bakiye: "+dr["Kullanici_bakiye"].ToString()+"₺";
            }
            conn.Close();
            conn.Open();
            cmd.CommandText = ("SELECT * from Kategori_tbl;");
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string item = dr["Kategori_ad"].ToString();
                comboBox1.Items.Add(item);
            }
            conn.Close();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * FROM Oyun_tbl;");
            dr = cmd.ExecuteReader();
            int width = 200;
            int counter = 0;
            int top = 100;
            while (dr.Read())
            {
                olusturucu(width, top, counter);
                counter++;
                if (counter % 4 == 0)
                {
                    counter = 0;
                    top = top + 150;
                }
            }
            conn.Close();
            textBox1.Text = "Ara";
            textBox1.ForeColor = Color.Gray;
            textBox1.Font = new Font("Century Gothic", 9, FontStyle.Bold);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                macera macera = new macera();
                formgetir(macera);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                aksiyon aksiyon = new aksiyon();
                formgetir(aksiyon);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                jrpg jrpg = new jrpg();
                formgetir(jrpg);
            }
            else if (comboBox1.SelectedIndex == 3)
            {
               strateji strateji = new strateji();
                formgetir(strateji);
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                simülasyon simülasyon = new simülasyon();
                formgetir(simülasyon);
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                bulmaca bulmaca = new bulmaca();
                formgetir(bulmaca);
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
            textBox1.Font = new Font("Century Gothic", 9, FontStyle.Bold);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT Oyun_ad,Oyun_fiyat FROM Oyun_tbl WHERE Oyun_ad LIKE '"+textBox1.Text+"%'");
            dr = cmd.ExecuteReader();
            int width = 200;
            int counter = 0;
            int top = 100;
            while (dr.Read())
            {
                olusturucu(width, top, counter);
                counter++;
                if (counter % 4 == 0)
                {
                    counter = 0;
                    top = top + 150;
                }
            }
            conn.Close();
            
        }
    }
}
