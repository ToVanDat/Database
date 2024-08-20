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
    public partial class TacGia : Form
    {
        string Conn = @"Data Source=ANHQUAN;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;
        public TacGia()
        {
            InitializeComponent();
        }

        private void TacGia_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();
            SetControls(false);
            Display();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void Display()
        {
            string query = "select * from TacGia";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvTacGia.DataSource = dt;
        }
        private void dgvTacGia_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaTacGia.Text = dgvTacGia.Rows[r].Cells[0].Value.ToString();
            txtTenTacGia.Text = dgvTacGia.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvTacGia.Rows[r].Cells[2].Value.ToString();
        }
        private void SetControls(bool edit)
        {
            txtMaTacGia.Enabled = edit;
            txtTenTacGia.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaTacGia.Clear();
            txtTenTacGia.Clear();
            txtGhiChu.Clear();
            txtMaTacGia.Focus();
            bien = 1;
            SetControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenTacGia.Focus();
            bien = 2;
            SetControls(true);
            txtMaTacGia.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Are you sure you want to delete? ", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvTacGia.CurrentRow.Index;
                string Ma = dgvTacGia.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from TacGia where Author_ID = '" + Ma +"'";
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                Display();
                MessageBox.Show("Delete successfully", "Notification");
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot delete. ", "Notification");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaTacGia.Text == "" || txtTenTacGia.Text == "" || txtGhiChu.Text == "" )
                {
                    MessageBox.Show("Please re-enter !!!");
                }
                else
                {
                    for (int i = 0; i < dgvTacGia.RowCount; i++)
                    {
                        if (txtMaTacGia.Text == dgvTacGia.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã sinh viên. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }
                    string query1 = "insert into TacGia values(N'" + txtMaTacGia.Text + "',N'" + txtTenTacGia.Text + "',N'" + txtGhiChu.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Author added successfully", "Notification");
                }
            }
            else
            {
                if (txtMaTacGia.Text == "" || txtTenTacGia.Text == "" || txtGhiChu.Text == "" )
                {
                    MessageBox.Show("Please re-enter !!!");
                }
                else
                {
                    int row = dgvTacGia.CurrentRow.Index;
                    string Ma = dgvTacGia.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update TacGia set Author = N'" + txtTenTacGia.Text + "', Note = N'" + txtGhiChu.Text + "' where Author_ID ='" + Ma + "'";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Author edited successfully", "Notification");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            string query = "select * from TacGia where Author like N'%" + txtTimKiem.Text + "%'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvTacGia.DataSource = dt;
        }
    }
}
