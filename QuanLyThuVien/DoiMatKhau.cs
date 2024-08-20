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

namespace QuanLyThuVien
{
    public partial class DoiMatKhau : Form
    {
        string Conn = @"Data Source=ANHQUAN;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            txtTKDoi.Focus();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            string sSql = "select * from NhanVien where Employee_ID='" + txtTKDoi.Text + "' and Password='" + txtMKCu.Text + "'";
            mySqlCommand = new SqlCommand(sSql, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            int count = Convert.ToInt32(dt.Rows.Count.ToString());
            if (count == 0)
            {
                MessageBox.Show("The old account or password is incorrect", "Notification");
                return;
            }
            else
            {
                if (Convert.ToInt32(txtMKMoi.Text.Trim().Length) != Convert.ToInt32(txtMKMoi.Text.Length))
                {
                    MessageBox.Show("New password contains invalid characters. Please re-enter", "Notification");
                    return;
                }
                if (Convert.ToInt32(txtMKMoi.Text.Trim().Length) < 6)
                {
                    MessageBox.Show("New password must have at least 6 characters", "Notification");
                    return;
                }
                else
                {
                    string query2 = "update NhanVien set Password = '" + txtMKMoi.Text + "' where Employee_ID = '" + txtTKDoi.Text + "'";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Password changed successfully", "Notification");
                    Clear();
                }

            }
        }
        public void Clear()
        {
            txtTKDoi.Text = "";
            txtMKMoi.Text = "";
            txtMKCu.Text = "";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
