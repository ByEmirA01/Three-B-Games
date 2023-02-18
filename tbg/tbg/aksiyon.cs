using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tbg.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace tbg
{
    public partial class aksiyon : Form
    {
        public aksiyon()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        public void olusturucu (int width , int top , int counter)
        {
            System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
            oyunlar oyunlar = (oyunlar)Application.OpenForms["oyunlar"];
            PictureBox ptb = new PictureBox();
            Label lbl = new Label();
            Label lbl2= new Label();
            ////////////////////////////////////////
            btn.Enabled= true;
            btn.ForeColor=Color.White;
            btn.BackColor = Color.FromArgb(59,59,59);
            btn.Size = new Size(40,30);
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
            panel1.Controls.Add(btn);
            ////////////////////////////////////////
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Transparent;
            lbl.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl.Text = (string)dr["Oyun_ad"];
            lbl.Left = (width * counter) + 10;
            lbl.Top = top+25;
            lbl.Width = width;
            panel1.Controls.Add(lbl);
            ////////////////////////////////////////
            lbl2.ForeColor = Color.White;
            lbl2.BackColor = Color.Transparent;
            lbl2.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl2.Text = dr["Oyun_fiyat"].ToString();
            lbl2.Left = (width * counter) + 10;
            lbl2.Top = top + 50;
            lbl2.Width = width;
            panel1.Controls.Add(lbl2);
            ////////////////////////////////////////
            ptb.Size = new System.Drawing.Size(280, 120);
            ptb.SizeMode = PictureBoxSizeMode.Zoom;
            ptb.Left = (width * counter)+10;
            ptb.Top = top-90;
            ptb.Width = width;
            ptb.ImageLocation = "./Games/"+lbl.Text+".jpg";
            panel1.Controls.Add(ptb);
            ////////////////////////////////////////
        }
        private void aksiyon_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * FROM Oyun_tbl Where Oyun_kategori=2 ORDER BY Oyun_ad ASC; SELECT * from Oyun_tbl Where Oyun_kategori=2;");
            dr = cmd.ExecuteReader();
            int width = 200;
            int counter = 0;
            int top = 100;
            while (dr.Read())
            {
                olusturucu(width,top,counter);
                counter++;
                if (counter%4==0)
                {
                    counter = 0;
                    top = top + 150;
                }
            }
            conn.Close();
        }
    }
}
