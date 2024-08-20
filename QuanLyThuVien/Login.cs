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
using static System.Net.Mime.MediaTypeNames;
using Guna.UI2.WinForms;

namespace QuanLyThuVien
{
    public partial class Login : Form
    {
        string Conn = @"Data Source=.;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        string TenNV;
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {

            // Use parameterized query to prevent SQL injection
            string sSql = "SELECT * FROM NhanVien WHERE Employee_ID = @Username AND Password = @Password";
            SqlCommand mySqlCommand = new SqlCommand(sSql, mySqlconnection);

            // Get text values ​​from controls
            string username = txtAccount.Text;
            string password = txtPassword.Text;

            mySqlCommand.Parameters.AddWithValue("@Username", username);
            mySqlCommand.Parameters.AddWithValue("@Password", password);


            // Use DataAdapter to pour data into DataTable
            SqlDataAdapter adapter = new SqlDataAdapter(mySqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // There is data and processing here
                DataRow row = dt.Rows[0];
                string EmployeeName = row["Employee_Name"].ToString();

                this.Hide();
                TrangChu Home = new TrangChu(EmployeeName);
                Home.Show();
            }
            else
            {
                MessageBox.Show("Incorrect username or password");
            }

            // Đóng kết nối
            mySqlconnection.Close();
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtAccount.Text = "";
            txtPassword.Text = "";
        }
    }
}
