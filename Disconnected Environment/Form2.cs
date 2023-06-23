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

namespace Disconnected_Environment
{
    public partial class FormDataProdi : Form
    {
        private string stringConnection = "data source=INBOOK_X1_PRO\\rey;" + "database=TI;User ID=sa;Password=123";
        private SqlConnection koneksi;

        private void refreshfrom()
        {
            dataGridView1.Text = "";
            dataGridView1.Enabled = false;
            btnSave .Enabled = false;
            btnClear.Enabled = false;
        }
        public FormDataProdi()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            refreshfrom();
        }
        private void dataGridView()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.Prodi";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string nmProdi = nmp.Text;

            if (nmProdi == "")
            {
                MessageBox.Show("Masukkan Nama Prodi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                string str = "insert Into dbo.Prodi (nama_prodi" + "values(@id)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id", nmProdi));
                cmd.ExecuteNonQuery();

                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView();
                refreshfrom();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
            btnSave.Enabled = true;
            btnClear.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            refreshfrom();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void FormDataProdi_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 hu = new Form1();
            hu.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
