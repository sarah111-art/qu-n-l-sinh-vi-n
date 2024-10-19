using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class frmDSSV : Form
    {
        public frmDSSV()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDSSV_Load);
            //MessageBox.Show("Constructor được gọi!");
        }
        private string TuKhoa="";
        private void frmDSSV_Load(object sender, EventArgs e)
        {
            LoadDSSV();
            /*           MessageBox.Show("Hàm Load được gọi!");

                       var data = new Database().SelectData("exec SelectAllSinhVien\r\n");

                       if (data != null)
                       {
                           MessageBox.Show($"Số hàng: {data.Rows.Count}"); // Kiểm tra số hàng
                           if (data.Rows.Count > 0)
                           {
                               dgvSinhVien.DataSource = data;
                           }
                           else
                           {
                               MessageBox.Show("Không có dữ liệu để hiển thị.");
                           }
                       }
                       else
                       {
                           MessageBox.Show("Dữ liệu trả về null.");
                       }

                   */
        }
        private void LoadDSSV()
        {
            string TuKhoa = txtTuKhoa.Text;
            List <CustomParameter> lstPara = new List <CustomParameter>();
            lstPara.Add(new CustomParameter()
            {
                key ="@TuKhoa",           
                value=TuKhoa
            });

            dgvSinhVien.DataSource = null; //reset lại scdl hiển thị
            //load toan bo ds sv 
            dgvSinhVien.DataSource = new Database().SelectData("SelectAllSinhVien", lstPara);
            //dat ten cot
            dgvSinhVien.Columns["MaSinhVien"].HeaderText = "Mã sinh viên";
            dgvSinhVien.Columns["HoTen"].HeaderText = "Họ và tên ";
            dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
            dgvSinhVien.Columns["QueQuan"].HeaderText = "Quê quán";
            dgvSinhVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvSinhVien.Columns["Email"].HeaderText = "Email";
            dgvSinhVien.Columns["DienThoai"].HeaderText = "Điện thoại";

        }
        private void dgvSinhVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                var msv = dgvSinhVien.Rows[e.RowIndex].Cells["MaSinhVien"].Value.ToString();
                new frmSinhVien(msv).ShowDialog();
                LoadDSSV();
            }

        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            new frmSinhVien(null).ShowDialog();
            LoadDSSV();//load lại ds sv khi them thanh vien 
        }
    }
}
