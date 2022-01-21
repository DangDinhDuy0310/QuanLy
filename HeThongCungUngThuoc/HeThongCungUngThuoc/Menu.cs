using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanAn
{
    public partial class Menu : Form
    {
       
        string quyen = Login.info.VaiTro;
       
        public Menu()
        {
            InitializeComponent();
            hamAn();
            Ten.Text ="Xin Chào " +Login.User;

        }
        
       void hamAn()
        {
            if (quyen == "Nhân Viên Bán Hàng")
            {
                Thuoc.Visible = false;
                DoanhThu.Visible = false;      
            }
           else if (quyen == "Quản Kho")
            {
                BanHang.Visible = false;
                DoanhThu.Visible = false;
                NhanVien.Visible = false;
            }
          else  if (quyen == "Kế Toán")
            {
                Thuoc.Visible = false;
                BanHang.Visible = false;
                NhanVien.Visible = false;
            }

        }

        private Form currentFormChild;
        private void open(Form childForm)
        {
            if(currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2Panel2.Controls.Add(childForm);
            guna2Panel2.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

     

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

     

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            hamAn();
        }

        private void BanHang_Click(object sender, EventArgs e)
        {
            open(new BanHang());
         
            
        }

        private void NhanVien_Click(object sender, EventArgs e)
        {
            open(new nvVSkh());

            
        }

        private void Thuoc_Click(object sender, EventArgs e)
        {
            open(new Thuoc());


            
        }

        private void DoanhThu_Click(object sender, EventArgs e)
        {
            open(new DoanhThu());


           
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        void hien()
        {
            Login l = new Login();
            l.Show();
        }
        private void DangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
            hien();
        }

        private void BanHang_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void BanHang_MouseLeave(object sender, EventArgs e)
        {
      
        }

        private void BanHang_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
