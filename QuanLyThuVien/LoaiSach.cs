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
    public partial class LoaiSach : Form
    {
        string Conn = @"Data Source=ANHQUAN;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;
        public LoaiSach()
        {
            InitializeComponent();
        }

        private void LoaiSach_Load(object sender, EventArgs e)
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaLoai.Clear();
            txtLoaiSach.Clear();
            txtGhiChu.Clear();
            txtMaLoai.Focus();
            bien = 1;
            SetControls(true);
        }

        private void dgvLoaiSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaLoai.Text = dgvLoaiSach.Rows[r].Cells[0].Value.ToString();
            txtLoaiSach.Text = dgvLoaiSach.Rows[r].Cells[1].Value.ToString();
            txtGhiChu.Text = dgvLoaiSach.Rows[r].Cells[2].Value.ToString();
        }
        private void Display()
        {
            string query = "select * from LoaiSach";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvLoaiSach.DataSource = dt;
        }
        private void SetControls(bool edit)
        {
            txtMaLoai.Enabled = edit;
            txtLoaiSach.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnThem.Enabled = !edit;
            btnSua.Enabled = !edit;
            btnXoa.Enabled = !edit;
            btnGhi.Enabled = edit;
            btnHuy.Enabled = edit;
            //.Enabled = edit;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtLoaiSach.Focus();
            bien = 2;
            SetControls(true);
            txtMaLoai.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Do you really want to delete?? ", "Notification ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvLoaiSach.CurrentRow.Index;
                string Ma = dgvLoaiSach.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from LoaiSach where Type_ID  = '" + Ma + "'";
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                Display(); MessageBox.Show("Delete successfully", "Notification");
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot delete.", "Notification");
            }
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaLoai.Text == "" || txtLoaiSach.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Please re-enter !!!");
                }
                else
                {
                    for (int i = 0; i < dgvLoaiSach.RowCount; i++)
                    {
                        if (txtMaLoai.Text == dgvLoaiSach.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Duplicate Type Code. Please Re-Enter", "Notification");
                            return;
                        }
                    }
                    string query1 = "insert into LoaiSach values(N'" + txtMaLoai.Text + "',N'" + txtLoaiSach.Text + "',N'" + txtGhiChu.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Added successfully", "Notification");
                }
            }
            else
            {
                if (txtMaLoai.Text == "" || txtLoaiSach.Text == "" || txtGhiChu.Text == "")
                {
                    MessageBox.Show("Please re-enter !!!");
                }
                else
                {
                    int row = dgvLoaiSach.CurrentRow.Index;
                    string Ma = dgvLoaiSach.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update LoaiSach set Book_type = N'" + txtLoaiSach.Text + "', Note = N'" + txtGhiChu.Text + "' where Type_ID ='" + Ma + "'";
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Display();
                    MessageBox.Show("Edit Successful", "Notification");
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void txtTimKiem_KeyUp(object sender, KeyEventArgs e)
        {
            string query = "select * from LoaiSach where Book_type like N'%" + txtTimKiem.Text + "%'";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvLoaiSach.DataSource = dt;
        }
    }
}
