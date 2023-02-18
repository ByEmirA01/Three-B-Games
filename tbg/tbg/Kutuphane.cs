using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tbg.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace tbg
{
    public partial class Kutuphane : Form
    {
        public Kutuphane()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlDataReader dr;
        SqlConnection conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
        public void olusturucu(int width, int top, int counter)// veri tabanındaki her bir veri için (istenen) istediğimiz şekillerde label , picturebox oluşturan method.
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            oyunlar oyunlar = (oyunlar)Application.OpenForms["oyunlar"];
            PictureBox ptb = new PictureBox();
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            ////////////////////////////////////////
            btn.Enabled = true;
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(59, 59, 59);
            btn.Size = new Size(40, 30);
            btn.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            btn.Image = Resources.outline_download_white_18dp;
            btn.Left = (width * counter) + 175;
            btn.Top = top + 25;
            btn.Click += new EventHandler(btn_Click);
            void btn_Click(object sender, EventArgs e)
            {
                MessageBox.Show("İndirme özelliğimiz şuanda mevcut değil");
            }
            panel2.Controls.Add(btn);
            ////////////////////////////////////////
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl.Text = (string)dr["Kutuphane_oyun_ad"];
            lbl.Left = (width * counter) + 20;
            lbl.Top = top + 25;
            lbl.Width = width;
            panel2.Controls.Add(lbl);
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
        private void Kutuphane_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Ara";
            textBox1.ForeColor = Color.Gray;
            textBox1.Font = new Font("Century Gothic", 9, FontStyle.Bold);
            pictureBox1.Visible= true;
            cmd = new SqlCommand();
            conn.Open();
            string sorgu = "SELECT COUNT(*) FROM Kutuphane_tbl";
            using (SqlCommand command = new SqlCommand(sorgu, conn))
            {
                int rowCount = (int)command.ExecuteScalar();//veri tabanının içinde veri olup olmadığını bize 0 ve 1 olarak verir.
                if (rowCount > 0)
                {
                    pictureBox1.Visible = false;
                    cmd.Connection = conn;
                    cmd.CommandText = ("SELECT * from Kutuphane_tbl;");
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
                else
                {
                    pictureBox1.Visible = true;
                }
            }
            conn.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT Kutuphane_oyun_ad FROM Kutuphane_tbl WHERE Kutuphane_oyun_ad LIKE '" + textBox1.Text + "%'");
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

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
            textBox1.Font = new Font("Century Gothic", 9, FontStyle.Bold);
        }
    }
}
