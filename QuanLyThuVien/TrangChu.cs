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
    public partial class TrangChu : Form
    {
        string Conn = @"Data Source=ANHQUAN;Initial Catalog=QuanLyThuVienDB;Integrated Security=True";
        SqlConnection mySqlconnection;
        SqlCommand mySqlCommand;
        int bien = 1;

        private string _message;
        public TrangChu()
        {
            InitializeComponent();
        }

        public TrangChu(string Message) : this()
        {
            _message = Message;
         
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            mySqlconnection = new SqlConnection(Conn);
            mySqlconnection.Open();

            DocGia();
            NhanVien();
            Muontra();
            cbMuontra();
            thongke();
            tracuusach();

        }
        public void tracuusach()
        {
            string query = "select S.[Booktitle ], Ls.Book_type, XB.NhaXuatBan, TG.Author, S.[Page_Number ], S.[Selling_Price ], S.[Quantity ] \r\nfrom LoaiSach LS join Sach S on LS.Type_ID = S.[Type_ID ] \r\njoin NhaXuatBan XB on XB.MaXB = S.Publishing_ID join TacGia TG on TG.Author_ID = S.Author_ID";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvTimSach.DataSource = dt;
        }

        private void btnSachh_Click(object sender, EventArgs e)
        {
            Sach S = new Sach();
            S.Show();
        }

        private void btnLS_Click(object sender, EventArgs e)
        {
            LoaiSach LS = new LoaiSach();
            LS.Show();
        }

        private void btnTG_Click(object sender, EventArgs e)
        {
            TacGia TG = new TacGia();
            TG.Show();
        }

        private void btnXBB_Click(object sender, EventArgs e)
        {
            NhaXuatBann XB = new NhaXuatBann();
            XB.Show();
        }

        private void txtTimKiemm_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnTenSach.Checked)
            {
                string query = "select S.[Booktitle ], Ls.Book_type, XB.NhaXuatBan, TG.Author, S.[Page_Number ], S.[Selling_Price ], S.[Quantity ] \r\nfrom LoaiSach LS join Sach S on LS.Type_ID = S.[Type_ID ] join NhaXuatBan XB on XB.MaXB = S.Publishing_ID \r\njoin TacGia TG on TG.Author_ID = S.Author_ID  \r\nwhere S.[Booktitle ] like N'%" + txtTimKiemm.Text + "%'order by S.[Quantity ] ";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnLoaiSach.Checked)
            {
                string query = "SELECT \r\n    S.[Booktitle], \r\n    LS.Book_type, \r\n    XB.NhaXuatBan, \r\n    TG.Author, \r\n    S.[Page_Number], \r\n    S.[Selling_Price], \r\n    S.[Quantity] \r\nFROM \r\n    Sach S\r\nJOIN \r\n    LoaiSach LS ON S.[Type_ID] = LS.[Type_ID]\r\nJOIN \r\n    NhaXuatBan XB ON S.Publishing_ID = XB.MaXB\r\nJOIN \r\n    TacGia TG ON S.Author_ID = TG.Author_ID\r\nWHERE \r\n    LS.Book_type LIKE N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnTacGia.Checked)
            {
                string query = "select S.[Booktitle ], Ls.Book_type, XB.NhaXuatBan, TG.Author, S.[Page_Number ], S.[Selling_Price ], S.[Quantity ] \r\nfrom LoaiSach LS join Sach S on LS.Type_ID = S.[Type_ID ] join NhaXuatBan \r\nXB on XB.MaXB = S.Publishing_ID join TacGia TG on TG.Author_ID = S.Author_ID \r\nwhere TG.Author_ID like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
            if (btnNXB.Checked)
            {
                string query = "select S.Book_ID, Ls.Book_type, XB.NhaXuatBan, TG.Author, S.[Page_Number ], S.[Selling_Price ], S.[Quantity ]\r\nfrom LoaiSach LS join Sach S on LS.Type_ID = S.[Type_ID ] join NhaXuatBan XB on XB.MaXB = S.Publishing_ID join TacGia TG on TG.Author_ID = S.Author_ID \r\nwhere XB.NhaXuatBan like N'%" + txtTimKiemm.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvTimSach.DataSource = dt;
            }
        }

        private void DocGia()
        {
            string query = "select * from SinhVien";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvSinhVien.DataSource = dt;
            SetControls(false);
        }

        private void btnThemSV_Click(object sender, EventArgs e)
        {
            txtMaSinhVien.Clear();
            txtHoTen.Clear();
            txtNganhHoc.Clear();
            txtSDT.Clear();
            txtMaSinhVien.Focus();
            bien = 1;

            SetControls(true);
        }

        private void SetControls(bool edit)
        {
            txtMaSinhVien.Enabled = edit;
            txtHoTen.Enabled = edit;
            txtKhoa.Enabled = edit;
            txtNganhHoc.Enabled = edit;
            txtSDT.Enabled = edit;
            btnThemSV.Enabled = !edit;
            btnSuaSV.Enabled = !edit;
            btnXoaSV.Enabled = !edit;
            btnGhiSV.Enabled = edit;
            btnHuySV.Enabled = edit;
            //.Enabled = edit;
            
            cbTenSach.Enabled = edit;
            cbNgayMuon.Enabled = edit;
            cbNgayTra.Enabled = edit;
            cbMaSV.Enabled = edit;
            txtGhiChu.Enabled = edit;
            btnMuon.Enabled = !edit;
            btnGiaHan.Enabled = !edit;
            btnTraSach.Enabled = !edit;
            btnGhii.Enabled = edit;
            btnHuyy.Enabled = edit;
            cbNgayMuon.Visible = true;
            lbNgayMuon.Visible = true;
        }

        private void btnSuaSV_Click(object sender, EventArgs e)
        {
            txtHoTen.Focus();
            bien = 2;

            SetControls(true);
            txtMaSinhVien.Enabled = false;
        }

        private void btnXoaSV_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = new DialogResult();
                dlr = MessageBox.Show("Ban co chac chan muon xoa? ", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.No) return;
                int row = dgvSinhVien.CurrentRow.Index;
                string MaSV = dgvSinhVien.Rows[row].Cells[0].Value.ToString();
                string query3 = "delete from SinhVien where Student_ID = " + MaSV;
                mySqlCommand = new SqlCommand(query3, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                DocGia();
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot delete. This student is borrowing a book", "Notification");
            }
        }

        private void btnGhiSV_Click(object sender, EventArgs e)
        {
            if (bien == 1)
            {
                if (txtMaSinhVien.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNganhHoc.Text.Trim() == "" || txtKhoa.Text.Trim() == "" || txtSDT.Text.Trim() == "" || txtMaSinhVien.Text == "")
                {
                    MessageBox.Show("Please re-enter !!!");
                }
                else
                {
                    for (int i = 0; i < dgvSinhVien.RowCount; i++)
                    {
                        if (txtMaSinhVien.Text == dgvSinhVien.Rows[i].Cells[0].Value.ToString())
                        {
                            MessageBox.Show("Trùng mã sinh viên. Vui lòng Nhập lại", "Thông báo");
                            return;
                        }
                    }
                    double x;
                    bool kt = double.TryParse(txtMaSinhVien.Text, out x);
                    if (kt == false)
                    {
                        MessageBox.Show("Vui lòng Nhập lại dưới dạng số!", "Thông báo");
                        return;
                    }
                    string query1 = "insert into SinhVien values('" + txtMaSinhVien.Text + "',N'" + txtHoTen.Text + "',N'" + txtNganhHoc.Text + "', N'" + txtKhoa.Text + "', N'" + txtSDT.Text + "')";
                    mySqlCommand = new SqlCommand(query1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DocGia();
                    MessageBox.Show("Successful student name", "Notification");
                }
            }
            else
            {
                if (txtMaSinhVien.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNganhHoc.Text.Trim() == "" || txtKhoa.Text.Trim() == "" || txtSDT.Text.Trim() == "" || txtMaSinhVien.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập lại !!!");
                }
                else
                {
                    int row = dgvSinhVien.CurrentRow.Index;
                    string MaSV = dgvSinhVien.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update SinhVien set Student_ID = '" + txtMaSinhVien.Text + "', Student_Name = N'" + txtHoTen.Text + "', major = N'" + txtNganhHoc.Text + "', [Course ] = N'" + txtKhoa.Text + "', Phone_Number = N'" + txtSDT.Text + "' where Student_ID = " + MaSV;
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DocGia();
                    MessageBox.Show("Student edited successfully", "Notification");
                }
            }
        }

        private void btnHuySV_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void dgvSinhVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            txtMaSinhVien.Text = dgvSinhVien.Rows[r].Cells[0].Value.ToString();
            txtHoTen.Text = dgvSinhVien.Rows[r].Cells[1].Value.ToString();
            txtNganhHoc.Text = dgvSinhVien.Rows[r].Cells[2].Value.ToString();
            txtKhoa.Text = dgvSinhVien.Rows[r].Cells[3].Value.ToString();
            txtSDT.Text = dgvSinhVien.Rows[r].Cells[4].Value.ToString();
        }

        private void txtTimKiemSV_KeyUp(object sender, KeyEventArgs e)
        {
            if (btnMSV.Checked)
            {
                string query = "select * from SinhVien where Student_ID like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }

            if (btnTSV.Checked)
            {
                string query = "select * from SinhVien where Student_Name like N'%" + txtTimKiemSV.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvSinhVien.DataSource = dt;
            }
        }
        public void NhanVien() {
            string query = "select Employee_ID, Employee_Name, Phone_Number, Gender, Address from NhanVien";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvQuanLy.DataSource = dt;
            
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim() == "" || txtTenNV.Text.Trim() == "" || txtSoDienThoai.Text.Trim() == "" || cbGioiTinh.Text.Trim() == "" || txtDiaChi.Text.Trim() == "" || txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Input information cannot be empty", "Notification");
                return;
            }
            if(Convert.ToInt32(txtSoDienThoai.Text.Trim().Length) != 10 )
            {
                MessageBox.Show("Phone number must be 10 digits", "Notification");
                return;
            }
            double x;
            bool kt = double.TryParse(txtSoDienThoai.Text, out x);
            if (kt == false || Convert.ToInt32(txtSoDienThoai.Text) < 0)
            {
                MessageBox.Show("Please Re-Enter as Number", "Notification");
                return;
            }
            if (Convert.ToInt32(txtMatKhau.Text.Trim().Length) < 6 )
            {
                MessageBox.Show("Password must be at least 6 characters", "Notification");
                return;
            }
            if (Convert.ToInt32(txtMatKhau.Text.Trim().Length) != Convert.ToInt32(txtMatKhau.Text.Length))
            {
                MessageBox.Show("Password contains invalid characters. Please re-enter", "Notification");
                return;
            }

            string query1 = "insert into NhanVien(Employee_ID, Employee_Name, Phone_Number, Gender, Address, Password) values(N'" + txtMaNV.Text + "',N'" + txtTenNV.Text + "','" + txtSoDienThoai.Text + "', N'" + cbGioiTinh.Text + "', N'" + txtDiaChi.Text + "',  N'" + txtMatKhau.Text + "')";
            mySqlCommand = new SqlCommand(query1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            NhanVien();
            MessageBox.Show("Account registration successful", "Notification");

        }

        private void btnDoiPass_Click(object sender, EventArgs e)
        {
            DoiMatKhau DoiPass = new DoiMatKhau();
            DoiPass.Show();
        }

        public void Muontra()
        {
            string query = "select CallCard, StudentID, BookID,BorrowedDate,PaymentDate,Note from MuonTraSach MS join Sach S on BookID = Book_ID join SinhVien SV on StudentID = StudentID";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvMuonSach.DataSource = dt;

            txtMaPhieuMUon.Enabled = false;
            ttMaSach.Enabled = false;
            ttTenSach.Enabled = false;
            ttSoLuong.Enabled = false;
            ttTenTG.Enabled = false;
        }
        public void cbMuontra()
        {
            string sSql2 = "select Student_ID from SinhVien";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                cbMaSV.Items.Add(dr[0].ToString());
            }
            string sSql9 = "select [Booktitle ] from Sach";
            mySqlCommand = new SqlCommand(sSql9, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt9 = new DataTable();
            SqlDataAdapter da9 = new SqlDataAdapter(mySqlCommand);
            da9.Fill(dt9);
            foreach (DataRow dr in dt9.Rows)
            {
                cbTenSach.Items.Add(dr[0].ToString());
            }
        }

        private void cbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql1 = "select s.Book_ID, s.[Booktitle ], tg.Author, s.[Quantity ] from Sach s join TacGia tg on s.Author_ID = tg.Author_ID \r\nwhere s.[Booktitle ] = '" + cbTenSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            da1.Fill(dt1);
            foreach (DataRow dr in dt1.Rows)
            {
                ttMaSach.Text = dr["Book_ID"].ToString();
                ttTenSach.Text = dr["Booktitle "].ToString();
                ttTenTG.Text = dr["Author"].ToString();
                ttSoLuong.Text = dr["Quantity "].ToString();
            }
        }

        private void txtTimKiemSachMuon_KeyUp(object sender, KeyEventArgs e)
        {
            if (raMaSV.Checked)
            {
                string query = "select MS.CallCard, SV.Student_ID, SV.Student_Name, S.Book_ID, S.[Booktitle ],MS.[BorrowedDate ],MS.[PaymentDate ],MS.Note \r\nfrom MuonTraSach MS join Sach S on S.Book_ID = MS.BookID join SinhVien SV on SV.Student_ID = MS.StudentID \r\nwhere SV.Student_ID like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
            if (raMaSach.Checked)
            {
                string query = "select MS.CallCard, SV.Student_ID, SV.Student_Name, S.Book_ID, S.Booktitle ,MS.BorrowedDate ,MS.PaymentDate ,MS.Note \r\nfrom MuonTraSach MS join Sach S on S.Book_ID = MS.BookID join SinhVien SV on SV.Student_ID = MS.StudentID \r\nwhere S.Book_ID like N'%" + txtTimKiemSachMuon.Text + "%'";
                mySqlCommand = new SqlCommand(query, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
                SqlDataReader dr = mySqlCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvMuonSach.DataSource = dt;
            }
        }

        private void btnMuon_Click(object sender, EventArgs e)
        {
            //cb.Clear();
            //txtGhiChu.Clear();
            cbTenSach.Focus();
            bien = 5;
            SetControls(true);
            //cbNgayMuon.Enabled = false;
            cbNgayMuon.Visible = false;
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            //cbSV.Focus();
            bien = 6;

            SetControls(true);
            txtMaPhieuMUon.Enabled = false;
            cbTenSach.Enabled = false;
            cbMaSV.Enabled = false;
            txtGhiChu.Enabled = false;
            cbNgayMuon.Enabled = false;
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Are you sure you want to pay?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            int row = dgvMuonSach.CurrentRow.Index;
            string MaMuonTra = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
            string query3 = "delete from MuonTraSach where CallCard = " + MaMuonTra;
            mySqlCommand = new SqlCommand(query3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SoLuongSauTra();
            Muontra();
            MessageBox.Show("Book returned successfully.", "Notification");
        }
        private void btnGhii_Click(object sender, EventArgs e)
        {
            int SoNgay;
            string sSql2 = "SELECT DATEDIFF(day, GETDATE(),'" + cbNgayTra.Value + "')";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(mySqlCommand);
            da5.Fill(dt5);
            SoNgay = Convert.ToInt32(dt5.Rows[0][0].ToString());
            if (SoNgay > 0)
            {
                if (bien == 5)
                {
                    int SoLuongSach = 0;
                    //int MaSach = Convert.ToInt32(ttMaSach.Text);
                    //MessageBox.Show(Convert.ToString(MaSach));
                    string sSql1 = "select Quantity from Sach where Book_ID ='" + ttMaSach.Text + "'";
                    mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        SoLuongSach = Convert.ToInt32(dr["Quantity"].ToString());
                    }
                    if (SoLuongSach > 0)
                        {
                            string sSql8 = "select * from MuonTraSach where StudentID='" + cbMaSV.Text + "'";
                            mySqlCommand = new SqlCommand(sSql8, mySqlconnection);
                            mySqlCommand.ExecuteNonQuery();
                            DataTable dt8 = new DataTable();
                            SqlDataAdapter da8 = new SqlDataAdapter(mySqlCommand);
                            da8.Fill(dt8);
                            int count = Convert.ToInt32(dt8.Rows.Count.ToString());
                            if (count > 3)
                            {
                                MessageBox.Show("Sinh viên này đã mượn 3 cuốn, vui lòng trả sách để có thể tiếp tục mượn","Thông báo");
                                return;
                            }
                            else
                            {
                                string query2 = "insert into MuonTraSach( StudentID, BookID, BorrowedDate , PaymentDate , Note) values('" + cbMaSV.Text + "','" + ttMaSach.Text + "', GETDATE(),'" + cbNgayTra.Value + "',N'" + txtGhiChu.Text + "')";
                                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                                mySqlCommand.ExecuteNonQuery();
                                SoLuongSauMuon();
                                Muontra();
                                SetControls(false);
                                MessageBox.Show("Mượn sách thành công.", "Thông báo");

                            }
                        }
                    else
                    {
                        MessageBox.Show("Không có sẵn sách này", "Thông báo");
                    }
            }
                else
                {
                    int row = dgvMuonSach.CurrentRow.Index;
                    string MaMuonTra = dgvMuonSach.Rows[row].Cells[0].Value.ToString();
                    string query2 = "update MuonTraSach set PaymentDate  = '" + cbNgayTra.Value + "' where CallCard = " + MaMuonTra;
                    mySqlCommand = new SqlCommand(query2, mySqlconnection);
                    mySqlCommand.ExecuteNonQuery();
                    Muontra();
                    SetControls(false);
                    MessageBox.Show("Gia hạn thành công.", "Thông báo");
                }
                }
            else
            {
                MessageBox.Show("Thời gian trả không hợp lệ");
            }
        }

        private void btnHuyy_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }
        public void SoLuongSauMuon()// Hàm này để thay đổi số lượng sách khi mượn sách.
        {
            //int MaSach = Convert.ToInt32(cbMaSach.Text);
            //MessageBox.Show(Convert.ToString(MaSach));
            string sSql1 = "select Quantity from Sach where Book_ID ='" + ttMaSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int SoLuongSach = Convert.ToInt32(dr["Quantity"].ToString());
                int SoLuong = SoLuongSach - 1;
                //MessageBox.Show(Convert.ToString(SoLuong));
                string query2 = "update Sach set Quantity = " + SoLuong + " where Book_ID = '" + ttMaSach.Text + "'";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }
        public void SoLuongSauTra()// Hàm này để thay đổi số lượng sách khi trả sách.
        {
            //int MaSach = Convert.ToInt32(cbMaSach.Text);
            string sSql1 = "select Quantity from Sach where Book_ID ='" + ttMaSach.Text + "'";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int SoLuongSach = Convert.ToInt32(dr["Quantity"].ToString());
                int SoLuong = SoLuongSach + 1;
                //MessageBox.Show(Convert.ToString(SoLuong));
                string query2 = "update Sach set Quantity = " + SoLuong + " where Book_ID = '" + ttMaSach.Text + "'";
                mySqlCommand = new SqlCommand(query2, mySqlconnection);
                mySqlCommand.ExecuteNonQuery();
            }
        }

        private void dgvMuonSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //int r = e.RowIndex;
            //txtMaPhieuMUon.Text = dgvMuonSach.Rows[r].Cells[0].Value.ToString();
            //cbTenSach.Text = dgvMuonSach.Rows[r].Cells[4].Value.ToString();
            //cbMaSV.Text = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            //cbNgayMuon.Text = dgvMuonSach.Rows[r].Cells[5].ToString();
            //cbNgayTra.Text = dgvMuonSach.Rows[r].Cells[6].ToString();
            //txtGhiChu.Text = dgvMuonSach.Rows[r].Cells[7].Value.ToString();

            //string sSql1 = "select s.Book_ID, s.Booktitle, tg.Author, s.Quantity from Sach s join TacGia tg on s.Author_ID = tg.Author_ID where s.Booktitle = '" + cbTenSach.Text + "'";
            //mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            //mySqlCommand.ExecuteNonQuery();
            //DataTable dt1 = new DataTable();
            //SqlDataAdapter da1 = new SqlDataAdapter(mySqlCommand);
            //da1.Fill(dt1);
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    ttMaSach.Text = dr["Book_ID"].ToString();
            //    ttTenSach.Text = dr["Booktitle"].ToString();
            //    ttTenTG.Text = dr["Author"].ToString();
            //    ttSoLuong.Text = dr["Quantity"].ToString();
            //}

            //string sSql2 = "select Student_ID, Student_Name from SinhVien where Student_ID =  '" + cbMaSV.Text + "'";
            //mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            //mySqlCommand.ExecuteNonQuery();
            //DataTable dt2 = new DataTable();
            //SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            //da2.Fill(dt2);
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    ttMaSV.Text = dr["Student_ID"].ToString();
            //    ttTenSV.Text = dr["Student_Name"].ToString();

            //}
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            string query = "select MS.CallCard, SV.Student_ID, SV.Student_Name, SV.Phone_Number, S.[Booktitle ],MS.[BorrowedDate ],MS.[BorrowedDate ],MS.Note \r\nfrom MuonTraSach MS join Sach S on S.Book_ID = MS.BookID join SinhVien SV on SV.Student_ID = MS.StudentID \r\nwhere MS.[BorrowedDate ] <= CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = false;
            lbquahan.Visible = true;
            lbTong.Text = dgvDSMuon.RowCount.ToString();
        }

        private void btnDangMuon_Click(object sender, EventArgs e)
        {
            string query = "select MS.[BorrowedDate ], SV.Student_ID, SV.Student_Name, SV.Phone_Number, S.[Booktitle ],MS.[BorrowedDate ],MS.[PaymentDate ],MS.Note \r\nfrom MuonTraSach MS join Sach S on S.Book_ID = MS.BookID join SinhVien SV on SV.Student_ID = MS.StudentID \r\nwhere MS.[PaymentDate ] > CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            lbTong.Text = dgvDSMuon.RowCount.ToString();
        }
        public void thongke()
        {
            string query = "select MS.CallCard, SV.Student_ID, SV.Student_Name, SV.Phone_Number, S.[Booktitle ],MS.[BorrowedDate ],MS.[PaymentDate ],\r\nMS.Note \r\nfrom MuonTraSach MS join Sach S on S.Book_ID = MS.BookID join SinhVien SV on SV.Student_ID = MS.StudentID\r\nwhere MS.[PaymentDate ] > CONVERT(date,GETDATE())";
            mySqlCommand = new SqlCommand(query, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            SqlDataReader dr = mySqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSMuon.DataSource = dt;
            lbdangmuon.Visible = true;
            lbquahan.Visible = false;
            lbTong.Text = dgvDSMuon.RowCount.ToString();


            NhanVienn();
            SinhVien();
            Sach();
            MuonTraSach();
            LoaiSach();
            TacGia();
            NhaXuatBan();



        }
        public void NhanVienn()
        {
            string sSql1 = "select count(*) from NhanVien";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkAdmin.Text = dt.Rows[0][0].ToString();
        }
        public void SinhVien()
        {
            string sSql1 = "select count(*) from SinhVien";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSinhVien.Text = dt.Rows[0][0].ToString();
        }
        public void Sach()
        {
            string sSql1 = "select count(*) from Sach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkSach.Text = dt.Rows[0][0].ToString();
        }
        public void MuonTraSach()
        {
            string sSql1 = "select count(*) from MuonTraSach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TkMuonSach.Text = dt.Rows[0][0].ToString();
        }
        public void LoaiSach()
        {
            string sSql1 = "select count(*) from LoaiSach";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKLoaiSach.Text = dt.Rows[0][0].ToString();
        }
        public void TacGia()
        {
            string sSql1 = "select count(*) from TacGia";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKTacGia.Text = dt.Rows[0][0].ToString();
        }
        public void NhaXuatBan()
        {
            string sSql1 = "select count(*) from NhaXuatBan";
            mySqlCommand = new SqlCommand(sSql1, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
            da.Fill(dt);
            TKNhaXB.Text = dt.Rows[0][0].ToString();
        }

        private void cbMaSV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSql2 = "select Student_ID, Student_Name from SinhVien where Student_ID = '" + cbMaSV.Text + "'";
            mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                ttMaSV.Text = dr["Student_ID"].ToString();
                ttTenSV.Text = dr["Student_Name"].ToString();

            }
        }

        private void dgvDSMuon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //string MaSV, msv, tsv, sdt, sdtt;
            //int r = e.RowIndex;
            //MaSV = dgvMuonSach.Rows[r].Cells[1].Value.ToString();
            //
            //string sSql2 = "select MaSV, TenSV, SoDienThoai from SinhVien where MaSV = '" + MaSV + "'";
            //mySqlCommand = new SqlCommand(sSql2, mySqlconnection);
            //mySqlCommand.ExecuteNonQuery();
            //DataTable dt2 = new DataTable();
            //SqlDataAdapter da2 = new SqlDataAdapter(mySqlCommand);
            //da2.Fill(dt2);
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    msv = dr["MaSV"].ToString();
            //    tsv = dr["TenSV"].ToString();
            //    sdt = dr["SoDienThoai"].ToString();
            //    MessageBox.Show(sdt, "Thông Tin Sinh Viên");
            //}
            //sdtt = Convert.ToString(sdt);
            //MessageBox.Show(sdt,"Thông Tin Sinh Viên");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Are you sure you want to log out?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            this.Hide();
            Login DN = new Login();
            DN.Show();
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            DialogResult dlr = new DialogResult();
            dlr = MessageBox.Show("Bạn có chắc chắn muốn xóa? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.No) return;
            int row = dgvQuanLy.CurrentRow.Index;
            string MaNV = dgvQuanLy.Rows[row].Cells[0].Value.ToString();
            string query3 = "delete from NhanVien where Employee_ID = '" + MaNV + "'";
            mySqlCommand = new SqlCommand(query3, mySqlconnection);
            mySqlCommand.ExecuteNonQuery();
            NhanVien();
            MessageBox.Show("Delete successfully.", "Notification");

        }

        private void txtXinChao_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
    }
}
