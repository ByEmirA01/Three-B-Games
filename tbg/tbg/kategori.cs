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

namespace tbg
{
    public partial class kategori : Form
    {
        public kategori()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlDataReader dr;
        SqlConnection conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
        void listeleme()
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from Kategori_tbl", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Kategori");
            dataGridView1.DataSource = ds.Tables["Kategori"];
            conn.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listeleme();
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu = "Delete from Kategori_tbl Where Kategori_id=@Kategori_id";
            SqlCommand komut = new SqlCommand(sorgu, conn);
            komut.Parameters.AddWithValue("@Kategori_id", dataGridView1.CurrentRow.Cells[0].Value.ToString());
            conn.Open();
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Başarıyla Silinmiştir");
            listeleme();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            textBox1.Visible = true;
            button5.Visible= true;
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection("server=.; Initial Catalog=tbg; Integrated Security=SSPI");
            cmd = new SqlCommand();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Lüfen Boşluk Bırakmayınız !");
            }
            else
            {
                conn.Open();
                string sorgu = "Insert into Kategori_tbl(Kategori_ad)" +
              "values (@Kategori_ad)";
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@Kategori_ad", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kategori Oluşturulmuştur.");
                conn.Close();
            }
            listeleme();
            button4.Enabled = true;
            label1.Visible = false;
            textBox1.Visible = false;
        }
    }
}
