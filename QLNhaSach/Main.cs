
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaSach
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

        }
        public string Connectionstring = @"Data Source=LAPTOP-8J9N4L4V;Integrated Security=True";


        private void bang_tblSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formtblSach và hỗ trợ mở lại form main này khi formtblSach bị đóng
            var childForm = new Forms.formtblSach();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_tblLoaiSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formtblLoaiSach và hỗ trợ mở lại form main này khi formtblLoaiSach bị đóng
            var childForm = new Forms.formtblLoaiSach();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_tblTacGiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formtblTacGia và hỗ trợ mở lại form main này khi formtblTacGia bị đóng
            var childForm = new Forms.formtblTacGia();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_tblKhachHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formtblKhachHang và hỗ trợ mở lại form main này khi formtblKhachHang bị đóng
            var childForm = new Forms.formtblKhachHang();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_tblHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formtblHoaDon và hỗ trợ mở lại form main này khi formtblHoaDon bị đóng
            var childForm = new Forms.formtblHoaDon();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_TheLoaiSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formTheLoaiSach và hỗ trợ mở lại form main này khi formTheLoaiSach bị đóng
            var childForm = new Forms.formTheLoaiSach();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_PhieuNhapSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formPhieuNhapSach và hỗ trợ mở lại form main này khi formPhieuNhapSach bị đóng
            var childForm = new Forms.formPhieuNhapSach();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_ChiTietHoaDonBanSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formChiTietHoaDonBanSach và hỗ trợ mở lại form main này khi formChiTietHoaDonBanSach bị đóng
            var childForm = new Forms.formChiTietHoaDonBanSach();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_BaoCaoTonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formBaoCaoTon và hỗ trợ mở lại form main này khi formBaoCaoTon bị đóng
            var childForm = new Forms.formBaoCaoTon();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        private void bang_BaoCaoCongNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hàm này sẽ mở một formBaoCaoCongNo và hỗ trợ mở lại form main này khi formBaoCaoCongNo bị đóng
            var childForm = new Forms.formBaoCaoCongNo();
            childForm.Owner = this;
            childForm.Show();
            this.Hide();
        }
        
        
        
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.Owner != null)
                {
                    this.Owner.Show();
                    this.Owner.Enabled = true;
                    this.Owner = null;
                    this.Dispose();
                }
            }
            catch (Exception) { }
        }
    }
}
