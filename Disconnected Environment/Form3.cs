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
    public partial class FormDataMahasiswa : Form
    {
        private string stringConnection = "data source=INBOOK_X1_PRO\\rey;" + "database=fakultas;User ID=sa;Password=123";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource CustomerBindingSource = new BindingSource();
        private void button1_Click(object sender, EventArgs e)
        {
            txtNIM.Text = "";
            txtNama.Text = "";
            txtAlamat.Text = "";
            dtTanggalLahir.Value = DateTime.Today;
            txtNIM.Enabled = true;
            txtNama.Enabled = true;
            cbxJenisKelamin.Enabled = true;
            txtAlamat.Enabled = true;
            dtTanggalLahir.Enabled = true;
            cbxJenisKelamin.Enabled = true;
            Prodicbx();
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nim = txtNIM.Text;
            nama = txtNama.Text;
            jk = cbxJenisKelamin.Text;
            alamat = txtAlamat.Text;
            tgl = dtTanggalLahir.Value;
            prodi = cbxProdi.Text;
            int hs = 0;
            koneksi.Open();
            string strs = "select id_prodi from dbo.Prodi where nama_prodi = @dd";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@dd", prodi));
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                hs = int.Parse(dr["id_prodi"].ToString());
            }
            dr.Close();
            string str = "insert into dbo.Mahasiswa (nim, nama_mahasiswa, jenis_kel, alamat, tgl_lahir, id_prodi)" + "values(@NIM, @Nm, @Jk, @Al, @Tgll, @Idp)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("NIM", nim));
            cmd.Parameters.Add(new SqlParameter("NIM", nama));
            cmd.Parameters.Add(new SqlParameter("Jk", jk));
            cmd.Parameters.Add(new SqlParameter("AL", alamat));
            cmd.Parameters.Add(new SqlParameter("Tgll", tgl));
            cmd.Parameters.Add(new SqlParameter("NIM", hs));
            cmd.ExecuteNonQuery();

            koneksi.Close();
            MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshform();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

       
        public FormDataMahasiswa()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
            this.bnMahasiswa.BindingSource = this.CustomerBindingSource;
            refreshform();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("Select m.nim, m.nama_mahasiswa, "
                + "m.alamat, m.jenis_kelamin, m.tgl_lahir, p.nama_prodi From dbo.Mahasiswa m "
                + "join dbo.Prodi p on m.id_prodi = p.id_prodi", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.bnMahasiswa.BindingSource.DataSource = ds.Tables[0];
            this.txtNIM.DataBindings.Add(
                new Binding("Text", this.bnMahasiswa.BindingSource, "NIM", true));
            this.txtNama.DataBindings.Add(
                new Binding("Text", this.bnMahasiswa.BindingSource, "nama_mahasiswa", true));
            this.txtAlamat.DataBindings.Add(
                new Binding("Text", this.bnMahasiswa.BindingSource, "alamat", true));
            this.cbxJenisKelamin.DataBindings.Add(
                new Binding("Text", this.bnMahasiswa.BindingSource, "jenis_kelamin", true));
            this.dtTanggalLahir.DataBindings.Add(
                new Binding("Text", this.bnMahasiswa.BindingSource, "tgl_lahir", true));
            this.cbxProdi.DataBindings.Add(
                new Binding("Text", this.bnMahasiswa.BindingSource, "nama_prodi", true));

            koneksi.Close();
        }
        private void clearBinding()
        {
            this.txtNIM.DataBindings.Clear();
            this.txtNama.DataBindings.Clear();
            this.txtAlamat.DataBindings.Clear();
            this.cbxJenisKelamin.DataBindings.Clear();
            this.dtTanggalLahir.DataBindings.Clear();
            this.cbxProdi.DataBindings.Clear();
        }
        private void refreshform()
        {
            txtNIM.Enabled = false;
            txtNama.Enabled = false;
            cbxJenisKelamin.Enabled = false;
            txtAlamat.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();


        }
        private void Prodicbx()
        {
            koneksi.Open();
            string str = "select nama_prodi from dbo.prodi";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteReader();
            koneksi.Close();
            cbxProdi.DisplayMember = "nama_prodi";
            cbxProdi.ValueMember = "id_prodi";
            cbxProdi.DataSource = ds.Tables[0];
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 fm = new Form1();
            fm.Show();
            this.Hide();
        }
    }
}