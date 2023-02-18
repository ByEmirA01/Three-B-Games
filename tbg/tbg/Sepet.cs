using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tbg.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace tbg
{
    public partial class Sepet : Form
    {
        public Sepet()
        {
            InitializeComponent();
        }
        int bakiye = 0;
        int ucret = 0;
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
            btn.Left = (width * counter) + 175;
            btn.Top = top + 25;
            btn.Click += new EventHandler(btn_Click);
            void btn_Click(object sender, EventArgs e)
            {
                btn.Enabled = true;
                string sorgu = "Delete from Sepet_tbl Where Sepet_oyun_ad='" + lbl.Text + "';";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                conn.Open();
                komut.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Başarıyla Silinmiştir");
                conn.Open();
                sorgu = "SELECT COUNT(*) FROM Sepet_tbl";
                using (SqlCommand command = new SqlCommand(sorgu, conn))
                {
                    int rowCount = (int)command.ExecuteScalar();
                    if (rowCount > 0)
                    {
                        cmd.CommandText = ("SELECT SUM(Sepet_oyun_fiyat) FROM Sepet_tbl");
                        ucret = (int)cmd.ExecuteScalar();
                        label2.Text = "ücret: " + ucret.ToString() + "₺";
                    }
                    else
                    {
                        button2.Enabled = false;
                        ucret = 0;
                        label2.Text = "ücret: " + ucret.ToString() + "₺";
                    }
                }
                conn.Close();
                btn.Enabled= false;
            }
            panel2.Controls.Add(btn);
            ////////////////////////////////////////
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl.Text = (string)dr["Sepet_oyun_ad"];
            lbl.Left = (width * counter) + 20;
            lbl.Top = top + 25;
            lbl.Width = width;
            panel2.Controls.Add(lbl);
            ////////////////////////////////////////
            lbl2.ForeColor = Color.White;
            lbl2.BackColor = Color.Transparent;
            lbl2.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl2.Text = dr["Sepet_Oyun_fiyat"].ToString();
            lbl2.Left = (width * counter) + 20;
            lbl2.Top = top + 50;
            lbl2.Width = width;
            panel2.Controls.Add(lbl2);
            ////////////////////////////////////////
            ptb.Size = new System.Drawing.Size(280, 120);
            ptb.SizeMode = PictureBoxSizeMode.Zoom;
            ptb.Left = (width * counter) + 20;
            ptb.Top = top - 90;
            ptb.Width = width;
            ptb.ImageLocation = "./Games/" + lbl.Text + ".jpg";
            panel2.Controls.Add(ptb);
            ////////////////////////////////////////
        }

        private void Sepet_Load(object sender, EventArgs e)
        {
            panel2.Enabled = true;
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Sepet_tbl;");
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
            Form1 form1 = (Form1)Application.OpenForms["form1"];
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Kullanici_tbl WHERE Kullanici_adi='" + form1.textBox1.Text + "';");
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                bakiye = (int)dr["Kullanici_bakiye"];
                label1.Text = "Bakiye : " + bakiye + "₺";
            }
            conn.Close();
            conn.Open();
            string sorgu = "SELECT COUNT(*) FROM Sepet_tbl";
            using (SqlCommand command = new SqlCommand(sorgu, conn))
            {
                int rowCount = (int)command.ExecuteScalar();//veri tabanının içinde veri olup olmadığını bize 0 ve 1 olarak verir.
                if (rowCount > 0)
                {
                    label1.Visible = true;
                    label2.Visible= true;
                    button2.Visible = true;
                    button2.Enabled = true;
                    conn.Close();
                    conn.Open();
                    cmd.CommandText = ("SELECT SUM(Sepet_oyun_fiyat) FROM Sepet_tbl");
                    ucret = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
                else
                {
                    pictureBox1.Visible= true;
                    button2.Enabled = false;
                }
            }
            label2.Text = "ücret: " + ucret.ToString() + "₺";
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["form1"];
            if (bakiye >= ucret)
            {
                bakiye = bakiye - ucret;
                conn.Open();
                string sorgu = "Insert into Kutuphane_tbl SELECT Sepet_oyun_ad FROM Sepet_tbl;";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.ExecuteNonQuery();
                MessageBox.Show("Sepet Onaylanmıştır.Lütfen sayfayı yenileyiniz!");
                conn.Close();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = ("DELETE FROM Sepet_tbl");
                dr = cmd.ExecuteReader();
                conn.Close();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText=("UPDATE Kullanici_tbl SET Kullanici_bakiye=" + bakiye + " WHERE Kullanici_adi='" + form1.textBox1.Text + "';");
                dr=cmd.ExecuteReader();
                button2.Enabled = false;
                panel2.Enabled = false;
            }
            else
            {
                MessageBox.Show("Bakiyeniz yetersiz!!");
            }
        }
    }
}
