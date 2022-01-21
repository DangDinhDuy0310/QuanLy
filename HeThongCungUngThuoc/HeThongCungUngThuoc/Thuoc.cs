
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
    public partial class Thuoc : Form
    {
        public Thuoc()
        {
            InitializeComponent();
            
            LoadNhaCungCap();
            Thuocsx();
            hienthimansx();
            hienthiMaKho();
            LoadThongKeNCC();
            LoadKho();
            Loadthongkek();
           


        }


        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-9DJVGKJ;Initial Catalog=HeThongDuocPham;Integrated Security=True");
       
        private void Thuocsx()
        {
            Con.Open();
            string Query = "Select * from Thuoc";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ThuocKS.DataSource = ds.Tables[0];
            foreach (DataGridViewRow row in ThuocKS.Rows)
            {
                if (row.Cells[1].Value != null | Convert.ToString(row.Cells[1].Value) == string.Empty)
                {
                   ThuocKS.Columns[0].Visible = false;
                    
                    break;
                }
                else
                {
                    ThuocKS.Columns[0].Visible = true;
                   
                    break;
                }
            }
            Con.Close();
        }
       
        private void ResetNCC()
        {
            TenNCC.Text = "";
            DiaChi.Text = "";
            SDT.Text = "";
        }
        private void ResetThuoc()
        {
            TenThuoc.Text = "";
            SoLuongThuoc.Text = "";
            GiaBan.Text = "";
        }
        private void LoadNhaCungCap()
        {
            Con.Open();
            string Query = "Select * from NhaCungCap";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
           NhaCungCap.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Loadthongkek()
        {
            Con.Open();
            string Query = "	select x.TenKho,s.TenThuoc,s.SoLuong from Thuoc as s ,Kho as x where s.MaKho = x.MaKho";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ThongkeNhakho.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void LoadThongKeNCC()
        {
            Con.Open();
            string Query = "	select s.TenNCC,COUNT(x.TenNCC) as'Số Lần Nhập Thuốc ' from NhaCungCap as s,Thuoc as x where s.MaNCC = x.MaNCC group by s.TenNCC";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ThongKeNhapThuoc.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void LoadKho()
        {
            Con.Open();
            string Query = "Select * from Kho";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BangKho.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
          
        }
        int Key = 0;
        private void guna2Button9_Click(object sender, EventArgs e)
        {

             
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

         
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.NhaCungCap.Rows[e.RowIndex];
                TenNCC.Text = NhaCungCap.Rows[e.RowIndex].Cells[1].Value.ToString();
                DiaChi.Text = NhaCungCap.Rows[e.RowIndex].Cells[2].Value.ToString();
                SDT.Text = NhaCungCap.Rows[e.RowIndex].Cells[3].Value.ToString();

            }
            if (TenNCC.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(NhaCungCap.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            if (DiaChi.Text == "" || TenNCC.Text == "" || SDT.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into NhaCungCap(TenNCC,DiaChi,SDT)values(@MN,@MK,@AK)", Con);
                    cmd.Parameters.AddWithValue("@MN", TenNCC.Text);
                    cmd.Parameters.AddWithValue("@MK", DiaChi.Text);
                    cmd.Parameters.AddWithValue("@AK", SDT.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                   LoadNhaCungCap();
                    ResetNCC();
            
                    hienthimansx();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from NhaCungCap where MaNCC = @MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa");
                    Con.Close();
                    LoadNhaCungCap();
                    ResetNCC();
                   
                    hienthimansx();
                }   
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

            if (DiaChi.Text == "" || TenNCC.Text == "" || SDT.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa ");
            }
            else
            {   
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update NhaCungCap set TenNCC=@MN,DiaChi=@Mk,SDT=@AK where MaNCC=@Mkey", Con);
                    cmd.Parameters.AddWithValue("@MN", TenNCC.Text);
                    cmd.Parameters.AddWithValue("@MK", DiaChi.Text);
                    cmd.Parameters.AddWithValue("@AK", SDT.Text);

                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    LoadNhaCungCap();
                    ResetNCC();
                 
                    hienthimansx();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void hienthiMaKho()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Makho from Kho", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("MaKho", typeof(int));
            dt.Load(Rdr);
            MaKho.ValueMember = "Makho";
            MaKho.DataSource = dt;
            Con.Close();
           
        }

        private void hienthimansx()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select MaNCC from NhaCungCap", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("MaNCC", typeof(int));
            dt.Load(Rdr);
            NhaCC.ValueMember = "MaNCC";
            NhaCC.DataSource = dt;
            Con.Close();
        }
      
        private void MedManCb_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetSellName();
        }

        private void ThuocKS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.ThuocKS.Rows[e.RowIndex];
                TenThuoc.Text = ThuocKS.Rows[e.RowIndex].Cells[2].Value.ToString();
                SoLuongThuoc.Text = ThuocKS.Rows[e.RowIndex].Cells[3].Value.ToString();
                GiaBan.Text = ThuocKS.Rows[e.RowIndex].Cells[4].Value.ToString();
                NguoiSD.SelectedItem = ThuocKS.Rows[e.RowIndex].Cells[5].Value.ToString();
                LoaiThuoc.SelectedItem = ThuocKS.Rows[e.RowIndex].Cells[8].Value.ToString();
                MaKho.SelectedItem = ThuocKS.Rows[e.RowIndex].Cells[6].Value.ToString();
                TenNCC.Text = ThuocKS.Rows[e.RowIndex].Cells[7].Value.ToString();
            }       
            if (TenThuoc.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ThuocKS.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GiaBan.Text) > 0)
            {
                MessageBox.Show("Giá bán Phải Lớn Hơn 0");
            }
            else
            {
                if (TenThuoc.Text == "" || SoLuongThuoc.Text == "" || GiaBan.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin ");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into Thuoc(TenThuoc,SoLuong,GiaBan,MaNCC,DoiTuongSD,LoaiThuoc,TenNCC,MaKho) values (@MN,@MK,@GB,@MLT,@MNC,@LT,@FC,@MTY)", Con);
                        cmd.Parameters.AddWithValue("@MN", TenThuoc.Text);
                        cmd.Parameters.AddWithValue("@MK", SoLuongThuoc.Text);
                        cmd.Parameters.AddWithValue("@GB", GiaBan.Text);
                        cmd.Parameters.AddWithValue("@MLT", NhaCC.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@FC", TenNhaCC.Text);
                        cmd.Parameters.AddWithValue("@MNC", NguoiSD.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@LT", LoaiThuoc.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@MTY", MaKho.SelectedValue.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã lưu");
                        Con.Close();
                        Thuocsx();
                        ResetThuoc();
                        LoadThongKeNCC();


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from Thuoc where MaThuoc = @MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa");
                    Con.Close();
                    Thuocsx();
                    ResetThuoc();    
                    hienthimansx();
                    LoadThongKeNCC();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void GetSellName()
        {
            Con.Open();
            string Query = "Select * from NhaCungCap where MaNCC= '" + NhaCC.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TenNhaCC.Text = dr["TenNCC"].ToString();
            }
            Con.Close();
        }
        void HienThiKho()
        {
            Con.Open();
            string Query = "Select * from Kho where MaKho= '" + MaKho.SelectedValue.ToString()+ "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TenKho.Text = dr["TenKho"].ToString();
            }
            Con.Close();
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (TenThuoc.Text == "" || SoLuongThuoc.Text == "" || GiaBan.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update Thuoc set TenThuoc=@MN,SoLuong=@MK,GiaBan=@GB,MaNCC=@MLT,TenNCC=@FC,DoiTuongSD=@MNC,LoaiThuoc=@LT,MaKho=@MTY where MaThuoc=@Mkey", Con);
                    cmd.Parameters.AddWithValue("@MN", TenThuoc.Text);
                    cmd.Parameters.AddWithValue("@MK", SoLuongThuoc.Text);
                    cmd.Parameters.AddWithValue("@GB", GiaBan.Text);
                    cmd.Parameters.AddWithValue("@MLT", NhaCC.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@FC", TenNhaCC.Text);
                    cmd.Parameters.AddWithValue("@MNC", NguoiSD.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LT", LoaiThuoc.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MTY", MaKho.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    Thuocsx();
                    ResetThuoc();
                    hienthimansx();
                    LoadThongKeNCC();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void ThuocKS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void MaKho_SelectionChangeCommitted(object sender, EventArgs e)
        {
            HienThiKho();
        }

        private void ThemKho_Click(object sender, EventArgs e)
        {
            if (TenNhaKho.Text == "" )
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Kho(TenKho) values (@MN)", Con);
                    cmd.Parameters.AddWithValue("@MN", TenNhaKho.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    LoadKho();
                    resertkho();
                    HienThiKho();
                    hienthiMaKho();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void XoaKho_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete from Kho where MaKho = @MK", Con);
                    cmd.Parameters.AddWithValue("@MK", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa");
                    Con.Close();
                    LoadKho();
                    resertkho();
                    HienThiKho();
                    hienthiMaKho();
                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void resertkho()
        {
            TenNhaKho.Text = " ";
        }
        private void SuaKho_Click(object sender, EventArgs e)
        {
            if (TenNhaKho.Text == "" )
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa ");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update Kho set TenKho=@MN where MaKho=@Mk", Con);
                    cmd.Parameters.AddWithValue("@MN", TenNhaKho.Text);
                    cmd.Parameters.AddWithValue("@MK", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu");
                    Con.Close();
                    LoadKho();
                    resertkho();
                    HienThiKho();
                    hienthiMaKho();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BangKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.BangKho.Rows[e.RowIndex];
                TenNhaKho.Text = BangKho.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            if (TenNhaKho.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(BangKho.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
    }
}
