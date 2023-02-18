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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace tbg
{
    public partial class magaza : Form
    {
        public magaza()
        {
            InitializeComponent();
        }
        void formgetir(Form frm)//bu method yeni bir panel geldiği zaman ekrandaki paneli temizler ve yeni gelen formu kendisinin şeklinde hizalayıp gösterir.
        {
            panel7.Controls.Clear();
            frm.TopLevel = false;
            panel7.Controls.Add(frm);
            frm.Show();
            frm.Dock = DockStyle.Fill;
            frm.BringToFront();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        private void magaza_Load(object sender, EventArgs e)
        {
            Form1 form1 = (Form1)Application.OpenForms["form1"];//form1 deki verileri burada kullanmamızı sağlayan kod.
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Kullanici_tbl WHERE Kullanici_adi='" + form1.textBox1.Text + "';");//form 1 deki kullanıcı isimli kişiyi veri tabanından çektik.
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label1.Text = dr["Kullanici_adi"].ToString();
            }
            pictureBox2.ImageLocation = "./profile/" + label1.Text + ".jpg";//debug klasöründeki fotoğrafı çeker.
            conn.Close();
            panel2.Visible = true;
            panel6.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel6.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            oyunlar oyunlar = new oyunlar();
            formgetir(oyunlar);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel2.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            Kutuphane kutuphane = new Kutuphane();
            formgetir(kutuphane);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel2.Visible = false;
            panel6.Visible = false;
            panel5.Visible = false;
            Sepet sepet = new Sepet();
            formgetir(sepet);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            panel2.Visible = false;
            panel6.Visible = false;
            panel4.Visible = false;
            profil profil = new profil();
            formgetir(profil);
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
