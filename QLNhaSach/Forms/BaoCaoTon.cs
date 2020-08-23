using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using QLNhaSach.Logics;
using QLNhaSach.Models;
using QLNhaSach.Enums;

namespace QLNhaSach.Forms
{
    public partial class formBaoCaoTon : Form
    {
        
        
        Repos.repo_BaoCaoTon repo = new Repos.repo_BaoCaoTon();
        DataTable source;

        // Danh sách này sẽ chứa một bản ghi, dùng để hoàn tác lại sự kiện vừa xảy ra
        // Quan trọng: sự kiện xoá sẽ chỉ cho phép hoàn tác lại bản ghi trên bảng này, không cho phép hoàn tác với các bảng liên kết
        List<Tuple<myEvents, BaoCaoTon>> userEvent = new List<Tuple<myEvents, BaoCaoTon>>();
        
        public formBaoCaoTon()
        {
            InitializeComponent();
        }
        
        // Hiện thông báo
        void ShowMessage(string message, bool result)
        {
            if (result)
            {
                this.lbMessageError.ForeColor = Color.Green;
            }
            else
            {
                this.lbMessageError.ForeColor = Color.Red;
            }

            if (chbNocation.Checked)
                MessageBox.Show(message);
            else
                lbMessageError.Text = message;
        }

        
        // Chuyển giá trị trong Textbox thành BaoCaoTon
        public BaoCaoTon Textbox_To_BaoCaoTon()
        {
            try
            {
                BaoCaoTon result = new BaoCaoTon();
            
                
                result.id = (int)this.numbid.Value;
                result.id_tblSach = (int)this.numbid_tblSach.Value;
                result.chiPhi = (int)this.numbchiPhi.Value;
                result.ngay = this.datengay.Value;
            result.moTa = (this.txtmoTa.Text == null ? null : (string)(this.txtmoTa.Text));
            
                return result;
            }
            catch (Exception ex) { ShowMessage(ex.Message, false); } // Không load được hoặc xảy ra lỗi
            return null;
        }
        
        // Làm trống input nhập vào
        public void MakeEmptyTextbox()
        {
            
			this.numbid.Value = 0;
			this.numbid_tblSach.Value = 0;
			this.numbchiPhi.Value = 0;
			this.datengay.Value = DateTime.Now;
			this.txtmoTa.Text = null;
        }

        // Chuyển giá trị trong DataGridView vào trong Textbox
        public void DataGridView_To_Textbox(int index)
        {
            try
            {
                
                
                var id = this.dgvData.Rows[index].Cells["id"].Value;
                if(id != null)
                    this.numbid.Value = id != null ? decimal.Parse(id.ToString()) : 0;
                
                var id_tblSach = this.dgvData.Rows[index].Cells["id_tblSach"].Value;
                if(id_tblSach != null)
                    this.numbid_tblSach.Value = id_tblSach != null ? decimal.Parse(id_tblSach.ToString()) : 0;
                
                var chiPhi = this.dgvData.Rows[index].Cells["chiPhi"].Value;
                if(chiPhi != null)
                    this.numbchiPhi.Value = chiPhi != null ? decimal.Parse(chiPhi.ToString()) : 0;
                
                var ngay = this.dgvData.Rows[index].Cells["ngay"].Value;
                if(ngay != null)
                    this.datengay.Value = ngay != null ? DateTime.Parse(ngay.ToString()) : DateTime.Now;
                
                var moTa = this.dgvData.Rows[index].Cells["moTa"].Value;
                if(moTa != null)
                    this.txtmoTa.Text = moTa.ToString();
            }
            catch (Exception ex) { ShowMessage(ex.Message, false); } // Không load được hoặc xảy ra lỗi
        }

        // Chuyển giá trị trong DataGridView thành BaoCaoTon
        public BaoCaoTon DataGridView_To_BaoCaoTon(int index)
        {
            try
            {
                BaoCaoTon result = new BaoCaoTon();
                
                
                var id = this.dgvData.Rows[index].Cells["id"].Value;
                if(id != null)
                    result.id = int.Parse(id.ToString());
                
                var id_tblSach = this.dgvData.Rows[index].Cells["id_tblSach"].Value;
                if(id_tblSach != null)
                    result.id_tblSach = (id_tblSach == null ? null : (int?)int.Parse(id_tblSach.ToString()));
                
                var chiPhi = this.dgvData.Rows[index].Cells["chiPhi"].Value;
                if(chiPhi != null)
                    result.chiPhi = (chiPhi == null ? null : (int?)int.Parse(chiPhi.ToString()));
                
                var ngay = this.dgvData.Rows[index].Cells["ngay"].Value;
                if(ngay != null)
                    result.ngay = (ngay == null ? null : (DateTime?)DateTime.Parse(ngay.ToString()));
                
                var moTa = this.dgvData.Rows[index].Cells["moTa"].Value;
                if(moTa != null)
                    result.moTa = (moTa == null ? null : (string)(moTa));

                return result;
            }
            catch (Exception ex) { ShowMessage(ex.Message, false); } // Không load được hoặc xảy ra lỗi
            return null;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
			FunctionHelper.ExportExcel(ref this.dgvData, "BaoCaoTon");
        }

        private void btnHoanTac_Click(object sender, EventArgs e)
        {
			
            try
            {
                if (userEvent.Count <= 0)
                    return;
                
                var last = userEvent[userEvent.Count - 1];
                // Dựa vào sự kiện để hoàn tác
                switch (last.Item1)
                {
                    case myEvents.Insert:
                        var result = repo.Delete(last.Item2);
                        if (result)
                            ShowMessage("Hoàn tác, xoá bản ghi thành công!", result);
                        else
                        {
                            ShowMessage("Không thể hoàn tác, lỗi hệ thống!", result);
                        }
                        break;
                    case myEvents.Update:
                        var result2 = repo.Update(last.Item2);
                        if (result2)
                            ShowMessage("Hoàn tác, cập nhật bản ghi thành công!", result2);
                        else
                        {
                            ShowMessage("Không thể hoàn tác, lỗi hệ thống!", result2);
                        }
                        break;
                    case myEvents.Delete:
                        var result3 = repo.Create(last.Item2);
                        if (result3)
                            ShowMessage("Hoàn tác, thêm lại bản ghi thành công!", result3);
                        else
                        {
                            ShowMessage("Không thể hoàn tác, lỗi hệ thống!", result3);
                        }
                        break;
                    default:
                        return;
                }
                
                userEvent.RemoveAt(userEvent.Count - 1);
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex) { ShowMessage("Không thể hoàn tác, xin hãy kiểm tra lại!\nChi tiết lỗi: " + ex.Message, false); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
			
            try
            {
                var record = Textbox_To_BaoCaoTon();
                var result = repo.Delete(record);

                if (result)
                {
                    ShowMessage("Xoá bản ghi thành công!", result);
                    userEvent.Add(new Tuple<myEvents, BaoCaoTon>(myEvents.Delete, record));

                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    ShowMessage("Xoá bản ghi thất bại, xin hãy kiểm tra lại!", result);
                    return;
                }
            }
            catch (Exception ex) { ShowMessage(ex.Message, false); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
			
            try
            {
                var result = repo.Update(Textbox_To_BaoCaoTon());

                if (result)
                {
                    ShowMessage("Cập nhật bản ghi thành công!", result);
                    userEvent.Add(new Tuple<myEvents, BaoCaoTon>(myEvents.Update, DataGridView_To_BaoCaoTon(dgvData.SelectedRows[0].Index)));
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    ShowMessage("Cập nhật thất bại, xin hãy kiểm tra lại!", result);
                    return;
                }
            }
            catch (Exception ex) { ShowMessage(ex.Message, false); }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
			
            try
            {
                var record = Textbox_To_BaoCaoTon();
                var result = repo.Create(record);

                if (result)
                {
                    ShowMessage("Tạo bản ghi thành công!", result);
                    userEvent.Add(new Tuple<myEvents, BaoCaoTon>(myEvents.Insert, record));
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    ShowMessage("Tạo bản ghi thất bại, xin hãy kiểm tra lại!", result);
                    return;
                }
            }
            catch (Exception ex) { ShowMessage(ex.Message, false); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
			source = FunctionHelper.ToDataTable<BaoCaoTon>(repo.FindAll());
            this.dgvData.DataSource = source;

            dgvData_Sorted(sender, e);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(this.txtSearch.Text != null)
            {
                FunctionHelper.Find(this.txtSearch.Text, ref this.dgvData);
            }
            dgvData_Sorted(sender, e);
        }

        private void dgvData_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
			try
            {
                DataGridView_To_Textbox(this.dgvData.SelectedRows[0].Index);
            }
            catch (Exception ex) { /*ShowMessage(ex.Message, false);*/ }
        }

        private void formBaoCaoTon_Load(object sender, EventArgs e)
        {
            btnLamMoi_Click(sender, e);
        }

        private void chbNocation_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chbNocation.Checked)
                this.chbNocation.Text = "Hiện thông báo";
            else
                this.chbNocation.Text = "Tắt thông báo";
        }

        private void btnLamTrong_Click(object sender, EventArgs e)
        {
			MakeEmptyTextbox();
        }

        private void formBaoCaoTon_FormClosed(object sender, FormClosedEventArgs e)
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
            catch (Exception ex) { }
        }

        private void dgvData_Sorted(object sender, EventArgs e)
        {
            if (!this.chbTatMau.Checked)
                return;

            bool bluebackgroud = false;
            foreach (DataGridViewRow item in this.dgvData.Rows)
            {
                if (bluebackgroud)
                {
                    item.DefaultCellStyle.BackColor = Color.SkyBlue;
                    bluebackgroud = false;
                }
                else
                    bluebackgroud = true;
            }
        }

        private void chbTatMau_CheckedChanged(object sender, EventArgs e)
        {
            dgvData_Sorted(sender, e);
        }

		
        bool bfid_tblSach = false;
        private void btnfid_tblSach_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            bfid_tblSach = true;
            var form = new fSearchAndReturn("select * from tblSach", "id");
            form.Owner = this;
            form.Show();
        }
        
        
        private void formBaoCaoTon_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (bfid_tblSach)
                {
                    bfid_tblSach = false;
                    if(this.Tag == null)
                        return;
                    this.numbid_tblSach.Value = int.Parse((this.Tag as object[])[0].ToString());
                    this.Tag = null;
                    return;
                }
            }
            catch (Exception ex) { ShowMessage("Không thể lấy khoá tham chiếu.\nChi tiết lỗi: " + ex.Message, false); } // không thể lấy khoá tham chiếu
        }

    }
}