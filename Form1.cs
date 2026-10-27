using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lab07_ADONET_Connection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            try
            {
                using (SqlConnection conn = DbConnectionFactory.Create())
                {
                    watch.Start();
                    conn.Open();
                    watch.Stop();

                    lblTrangThai.Text = $"Server: {conn.DataSource} | DB: {conn.Database} | Thời gian: {watch.ElapsedMilliseconds} ms";
                    MessageBox.Show($"Kết nối thành công!\nPhiên bản SQL: {conn.ServerVersion}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Trạng thái: Kết nối thất bại!";
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = DbConnectionFactory.Create())
            {
                conn.Open();
                string query = @"SELECT sv.MaSV, sv.HoTen, hp.TenHP, dk.HocKy, dk.Diem
                                 FROM DangKy dk
                                 JOIN SinhVien sv ON dk.MaSV = sv.MaSV
                                 JOIN HocPhan hp ON dk.MaHP = hp.MaHP";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvSinhVien.DataSource = dt;
            }
        }
    }
}