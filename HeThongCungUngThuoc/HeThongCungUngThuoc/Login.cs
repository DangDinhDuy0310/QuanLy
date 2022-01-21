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
   
    public class Info
    {
        public Info(int MaNhanVien, string HoTen,int SDT, string GioiTinh, string TaiKhoan, string MatKhau, string VaiTro)
        {
            this.MaNhanVien =MaNhanVien;
            this.HoTen = HoTen;
           
            
            this.GioiTinh = GioiTinh;
            this.SDT = SDT;
            this.TaiKhoan = TaiKhoan;
            this.MatKhau = MatKhau;
            this.VaiTro = VaiTro;
        }
        public int MaNhanVien { get; set; }
       
        public string HoTen { get; set; }
        public int SDT { get; set; }

        public string GioiTinh { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }


    }
    
    public partial class Login : Form
    {
          public static Info info;
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-9DJVGKJ;Initial Catalog=HeThongDuocPham;Integrated Security=True");

        public Login()
        {
            InitializeComponent();

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }
        public static string User;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (TaiKhoan.Text == "" || MatKhau.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select * from NhanVien where TaiKhoan= N'" + TaiKhoan.Text + "' and MatKhau='" + MatKhau.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // public Info(int SNum, string SName, string SDOB, string SPhone, string SAdd, string SGen, string Password, string Permission)
                    info = new Info(Convert.ToInt32(dt.Rows[0][0].ToString()), dt.Rows[0][1].ToString(), Convert.ToInt32( dt.Rows[0][2].ToString()), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString());
                    User = TaiKhoan.Text;
                    Menu obj = new Menu();
                    obj.Show();
                    this.Hide();
                    Con.Close();

                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu !");
                }
                Con.Close();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
