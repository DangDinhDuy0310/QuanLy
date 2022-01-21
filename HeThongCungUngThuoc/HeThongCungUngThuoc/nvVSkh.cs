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

namespace QuanLyBanAn
{
    public partial class nvVSkh : Form
    {
        string quyen = Login.info.VaiTro;
        public nvVSkh()
        {
            InitializeComponent();
            LoadKhachHang();
            LoadNhanVien();
            LoadTopNv();
            LoadNhanVienMK();
            hamhien();

        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-9DJVGKJ;Initial Catalog=HeThongDuocPham;Integrated Security=True");
       
        void hamhien()
        {
            if(quyen=="Nhân Viên Bán Hàng")
            {
                guna2TabControl1.TabPages.Remove(tabPage1);
                guna2TabControl1.TabPages.Remove(tabPage3);
            }

        }
        private void LoadNhanVienMK()
        {
            Con.Open();
            string Query = "Select * from NhanVien ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            bangMK.DataSource = ds.Tables[0];
            Con.Close();
            foreach (DataGridViewRow row in bangMK.Rows)
            {
                if (row.Cells[1].Value != null | Convert.ToString(row.Cells[1].Value) == string.Empty)
                {
                    bangMK.Columns[0].Visible = false;
                    bangMK.Columns[2].Visible = false;
                    bangMK.Columns[3].Visible = false;
                    break;
                }
                else
                {
                    bangMK.Columns[0].Visible = true;
                    bangMK.Columns[2].Visible = true;
                    bangMK.Columns[3].Visible = true;
                    break;
                }
            }
        }


        private void LoadNhanVien()
        {
            Con.Open();
            string Query = "Select * from NhanVien ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BangNhanVien.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void LoadTopNv()
        {
            Con.Open();
            string Query = "	select x.HoTen, count(c.MaHoaDon)AS 'Số Hoa Đơn Đã Bán' ,sum(d.TongTien) As ' Tổng Tiền ' from NhanVien as x,HoaDon as c, ChiTietHoaDon as d where x.MaNhanVien = c.MaNhanVien and c.MaHoaDon = d.MaHoaDon group by x.HoTen";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BangTopNv.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void LoadKhachHang()                    
        {
            Con.Open();
            string Query = "Select * from DaiLy ";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BangKhachHang.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
     

        private void DanhsachND_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void DanhsachND_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void DanhsachND_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        void ResetNV()
        {
            HoTenNV.Text = "";
            SDTNV.Text = "";
            TaiKhoan.Text = "";
            MatKhau.Text = "";
            VaiTro.Text = "";
            GioiTinh.Text = "";
        }
        private void ThemNV_Click(object sender, EventArgs e)
        {
           
            if (HoTenNV.Text == "" || SDTNV.Text == "" || TaiKhoan.Text == ""|| MatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into NhanVien(HoTen,SDT,GioiTinh,MatKhau,TaiKhoan,VaiTro)values(@MN,@MK,@DC,@AK,@AC,@AA)", Con);
                    cmd.Parameters.AddWithValue("@MN", HoTenNV.Text);
                    cmd.Parameters.AddWithValue("@MK", SDTNV.Text);
                    cmd.Parameters.AddWithValue("@DC", GioiTinh.Text);
                    cmd.Parameters.AddWithValue("@AK", MatKhau.Text);
                    cmd.Parameters.AddWithValue("@AC", TaiKhoan.Text);
                    cmd.Parameters.AddWithValue("@AA", VaiTro.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    LoadNhanVien();
                    ResetNV();
                    LoadNhanVienMK();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        int Key = 0;
        private void XoaNV_Click(object sender, EventArgs e)
        {
            if (VaiTro.Text == "Quản Lý")
            {
                MessageBox.Show("Không được xóa Quản Lý ");
            }
            else
            {
                if (Key == 0)
                {
                    MessageBox.Show("Chọn một dòng để xóa !");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Delete from NhanVien where MaNhanVien = @MKey", Con);
                        cmd.Parameters.AddWithValue("@MKey", Key);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã xóa");
                        Con.Close();
                        LoadNhanVien();
                        ResetNV();
                        LoadNhanVienMK();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void SuaNV_Click(object sender, EventArgs e)
        {

            if (HoTenNV.Text == "" || SDTNV.Text == "" || TaiKhoan.Text == "" || MatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update NhanVien set HoTen=@MN,SDT=@Mk,GioiTinh=@DC,MatKhau=@AK,TaiKhoan=@AC,VaiTro=@AA where MaNhanVien=@Mkey", Con);
                    cmd.Parameters.AddWithValue("@MN", HoTenNV.Text);
                    cmd.Parameters.AddWithValue("@MK", SDTNV.Text);
                    cmd.Parameters.AddWithValue("@AK", MatKhau.Text);
                    cmd.Parameters.AddWithValue("@DC", GioiTinh.Text);
                    cmd.Parameters.AddWithValue("@AC", TaiKhoan.Text);
                    cmd.Parameters.AddWithValue("@AA", VaiTro.Text);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    LoadNhanVien();
                    ResetNV();
                    LoadNhanVienMK();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BangNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.BangNhanVien.Rows[e.RowIndex];
                HoTenNV.Text = BangNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
                SDTNV.Text = BangNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString();
                GioiTinh.Text = BangNhanVien.Rows[e.RowIndex].Cells[3].Value.ToString();
                TaiKhoan.Text = BangNhanVien.Rows[e.RowIndex].Cells[4].Value.ToString();
                MatKhau.Text = BangNhanVien.Rows[e.RowIndex].Cells[5].Value.ToString();
                VaiTro.Text = BangNhanVien.Rows[e.RowIndex].Cells[6].Value.ToString();


            }
            if (HoTenNV.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BangNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void BangKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ThemKhachHang_Click(object sender, EventArgs e)
        {

            if (HoTenKhachHang.Text == "" || SDTKH.Text == "" || DCKH.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DaiLy(TenDaiLy,DiaChi,SDT)values(@MN,@MK,@AK)", Con);
                    cmd.Parameters.AddWithValue("@MN", HoTenKhachHang.Text);
                    cmd.Parameters.AddWithValue("@AK", SDTKH.Text);
                    cmd.Parameters.AddWithValue("@MK", DCKH.Text);
               
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    LoadKhachHang();
                    ResetKH();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void ResetKH()
        {
            HoTenKhachHang.Text ="";
            SDTKH.Text = "";
            DCKH.Text = "";

        }
        private void XoaKH_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Chọn một dòng để xóa !");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from DaiLy where MaDaiLy= @MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa");
                    Con.Close();
                    LoadKhachHang();
                    ResetKH();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SuaKH_Click(object sender, EventArgs e)
        {
            if (HoTenKhachHang.Text == "" || SDTKH.Text == "" || DCKH.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update DaiLy set TenDaiLy=@MN,SDT=@Mk,DiaChi=@AK where MaDaiLy=@Mkey", Con);
                    cmd.Parameters.AddWithValue("@MN", HoTenKhachHang.Text);
                    cmd.Parameters.AddWithValue("@MK", SDTKH.Text);
                    cmd.Parameters.AddWithValue("@AK", DCKH.Text);
                    
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    LoadKhachHang();
                    ResetKH();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BangKhachHang_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.BangKhachHang.Rows[e.RowIndex];
                HoTenKhachHang.Text = BangKhachHang.Rows[e.RowIndex].Cells[1].Value.ToString();
                DCKH.Text = BangKhachHang.Rows[e.RowIndex].Cells[2].Value.ToString();
                SDTKH.Text = BangKhachHang.Rows[e.RowIndex].Cells[3].Value.ToString();



            }
            if (HoTenKhachHang.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BangKhachHang.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BangNVHD_Click(object sender, EventArgs e)
        {

        }

        private void BangTopNv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void GioiTinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void VaiTro_TextChanged(object sender, EventArgs e)
        {

        }

        private void TaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void MatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void SDTNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void HoTenNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void DCKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void SDTKH_TextChanged(object sender, EventArgs e)
        {

        }

        private void HoTenKhachHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DoiMK_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select count(*) from NhanVien where TaiKhoan=N'"+TenMK.Text+"' and MatKhau=N'"+MKC.Text+"'",Con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            errorProvider1.Clear();
            if (dt.Rows[0][0].ToString() == "1")
            {
                if (MKM.Text == NhapLaiMK.Text)
                {
                    if (MKM.Text.Length > 0)
                    {
                        SqlDataAdapter dax = new SqlDataAdapter("update NhanVien set MatKhau=N'" + MKM.Text + "' where TaiKhoan=N'" + TenMK.Text + "' and MatKhau=N'" + MKC.Text + "'", Con);
                        DataTable dtc = new DataTable();
                        dax.Fill(dtc);
                        MessageBox.Show("Đổi Mật Khẩu Thành Công ");
                    }
                    else 
                    {
                        errorProvider1.SetError(MKM, "Độ Dài Mật Khẩu Không đủ");
                    }
                }
                else
                {
                    errorProvider1.SetError(MKM, "Bạn Chưa điền Mật Khẩu ");
                    errorProvider1.SetError(NhapLaiMK, "Mật Khẩu Nhập Lại Chưa Đúng ");
                }
            }
            else
            {
                errorProvider1.SetError(TenMK, "Tên Người Dùng Không Đúng ");
            }
            LoadNhanVienMK();
        }
    }
}
