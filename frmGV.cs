using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class frmGV : Form
    {
        public frmGV(string mgv)
        {
            this.mgv = mgv;
            InitializeComponent();
        }
        private string mgv;
        private string nguoithucthi = "admin";
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmGV_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this.mgv))
            {
                this.Text = "Thêm mới giáo viên";
            }else
            {
                this.Text = "Câp nhật giáo viên";
                var r = new Database().Select("selectGV'" + int.Parse(mgv) + "'");
                txtHo.Text = r["Ho"].ToString();
                txtTenDem.Text = r["TenDem"].ToString() + "'";
                txtTen.Text = r["Ten"].ToString().ToLower();
                rbtNam.Checked = r["GioiTinh"].ToString()=="0" ? true:false;
                mtbNgaySinh.Text = r["ngsinh"].ToString();
                txtEmail.Text = r["Email"].ToString();
                txtDiaChi.Text = r["DiaChi"].ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            DateTime ngaysinh;
            List<CustomParameter> lstPara = new List<CustomParameter>();
            try
            {
                ngaysinh = DateTime.ParseExact(mtbNgaySinh.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                MessageBox.Show("Ngày Sinh Không hợp lệ");
                mtbNgaySinh.Select();
                return;
            }
            if(string.IsNullOrEmpty(mgv))
            {
                sql = "insertGV";
                lstPara.Add(new CustomParameter()
                {
                    key = "@NguoiTao",
                    value = nguoithucthi

                });
            }
            else
            {
                sql = "updateGV";
                lstPara.Add(new CustomParameter()
                { 
                        key ="@NguoiCapNhat",
                        value=nguoithucthi
                               
                });
                lstPara.Add(new CustomParameter()
                {
                    key = "@magiaovien",
                    value = mgv

                });


            }
            lstPara.Add(new CustomParameter()
            {
                key = "@ho",
                value = txtHo.Text
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@tendem",
                value = txtTenDem.Text
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@ten",
                value = txtTen.Text
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@ngaysinh",
                value = ngaysinh.ToString("yyyy-MM-dd")
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@gioitinh",
                value = rbtNam.Checked?"0":"1"
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@email",
                value = txtEmail.Text
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@diachi",
                value = txtDiaChi.Text
            });
            lstPara.Add(new CustomParameter()
            {
                key = "@dienthoai",
                value = txtDienThoai.Text
            });

            var rs = new Database().ExCute(sql,lstPara);
            if(rs==1)
            {
                if(string.IsNullOrEmpty(mgv))
                {
                    MessageBox.Show("Thêm mới giáo vien thành viên");
                }
                else
                {
                    MessageBox.Show("Cập nhật mới giáo viên thành công");
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Thực thi truy vấn thất bại");
            }
        }
    }
}
