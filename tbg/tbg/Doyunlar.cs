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

namespace tbg
{
    public partial class Doyunlar : Form
    {
        public Doyunlar()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlDataReader dr;
        SqlConnection conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
        void listeleme()//bu method veri tabanındaki oyunları çeker.
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from Oyun_tbl", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Doyunlar");
            dataGridView1.DataSource = ds.Tables["Doyunlar"];
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listeleme();
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            comboBox1.Visible = true;
            button4.Enabled = false;
            button5.Visible = true;
            button1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Boşluk Bırakmayınız !");
            }
            else
            {
                conn.Open();
                string sorgu = "Insert into Oyun_tbl(Oyun_ad,Oyun_kategori,Oyun_fiyat)" +
              "values (@Oyun_ad,@Oyun_kategori,@Oyun_fiyat)";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@Oyun_ad", textBox1.Text);
                komut.Parameters.AddWithValue("@Oyun_fiyat", textBox2.Text);
                komut.Parameters.AddWithValue("@Oyun_kategori", comboBox1.SelectedIndex+1);
                komut.ExecuteNonQuery();
                MessageBox.Show("Oyun Oluşturulmuştur.");
                conn.Close();
            }
            listeleme();
            button4.Enabled = true;
            label1.Visible = false;
            label2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            comboBox1.Visible = false;
            button5.Visible = false;
            button1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete from Oyun_tbl Where Oyun_id=@Oyun_id";
            SqlCommand komut = new SqlCommand(sorgu, conn);
            komut.Parameters.AddWithValue("@Oyun_id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            conn.Open();
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Başarıyla Silinmiştir");
            listeleme();
        }

        private void Doyunlar_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = ("SELECT * from Kategori_tbl");
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string item = dr["Kategori_ad"].ToString();
                comboBox1.Items.Add(item);
            }
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (!int.TryParse(textBox2.Text, out value))
            {
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Jpeg dosyası (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fotografDosyaYolu = openFileDialog1.FileName;
                byte[] fotograf = File.ReadAllBytes(fotografDosyaYolu);
                string dosyaYolu = Path.Combine(Application.StartupPath, "./games/" + textBox1.Text + ".jpg");
                using (FileStream fileStream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    fileStream.Write(fotograf, 0, fotograf.Length);
                }
            }
        }
    }
}
