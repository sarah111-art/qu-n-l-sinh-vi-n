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
    public partial class frmSinhVien : Form
    {
        public frmSinhVien(string msv)
        {
            this.msv = msv;
            InitializeComponent();

        }
        private string msv;
        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.msv))
            {
                this.Text = "Thêm mới sinh viên ";
            }
            else
            {
                this.Text = "Cập nhật thông tin sinh viên ";
                var r = new Database().Select("selectSv'" + msv + "'");
                //MessageBox.Show(r[0].ToString());
                txtHo.Text = r["Ho"].ToString();
                txtTendem.Text = r["TenDem"].ToString();
                txtTen.Text = r["Ten"].ToString();
                var gioiTinhValue = r["GioiTinh"];
                if (gioiTinhValue != DBNull.Value)
                {
                    if (int.TryParse(gioiTinhValue.ToString(), out int gioiTinh))
                    {
                        if (gioiTinh == 0 || gioiTinh == 1) // Kiểm tra giá trị hợp lệ
                        {
                            rbtNam.Checked = (gioiTinh == 0);
                            rbtNu.Checked = (gioiTinh == 1);
                        }
                        else
                        {
                            MessageBox.Show("Giá trị GioiTinh không hợp lệ. Giá trị phải là 0 hoặc 1.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Giá trị GioiTinh không hợp lệ.");
                    }
                }
                else
                {
                    MessageBox.Show("Giá trị GioiTinh không có trong dữ liệu.");
                }
                txtQuequan.Text = r["QueQuan"].ToString();
                txtDiachi.Text = r["DiaChi"].ToString();
                txtDienthoai.Text = r["DienThoai"].ToString();
                txtEmail.Text = r["Email"].ToString();
            }
        }
        /*        private void frmSinhVien_Load(object sender, EventArgs e)
                {
                    if (string.IsNullOrEmpty(this.msv))
                    {
                        this.Text = "Thêm mới sinh viên";
                    }
                    else
                    {
                        this.Text = "Cập nhật thông tin sinh viên";
                        var r = new Database().Select("selectSv '" + msv + "'");

                        // Kiểm tra nếu r có dữ liệu
                        if (r != null)
                        {
                            txtHo.Text = r["Ho"].ToString();
                            txtTendem.Text = r["TenDem"].ToString();
                            txtTen.Text = r["Ten"].ToString();
                            mtbNgaysinh.Text = r["NgaySinh"]?.ToString() ?? string.Empty;

                            var gioiTinhValue = r["GioiTinh"];
                            if (gioiTinhValue != DBNull.Value)
                            {
                                if (int.TryParse(gioiTinhValue.ToString(), out int gioiTinh))
                                {
                                    if (gioiTinh == 0 || gioiTinh == 1) // Kiểm tra giá trị hợp lệ
                                    {
                                        rbtNam.Checked = (gioiTinh == 0);
                                        rbtNu.Checked = (gioiTinh == 1);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Giá trị GioiTinh không hợp lệ. Giá trị phải là 0 hoặc 1.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Giá trị GioiTinh không hợp lệ.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Giá trị GioiTinh không có trong dữ liệu.");
                            }


                            txtQuequan.Text = r["QueQuan"].ToString();

                            txtDiachi.Text = r["DiaChi"].ToString();
                            txtDienthoai.Text = r["DienThoai"].ToString();
                            txtEmail.Text = r["Email"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin sinh viên.");
                        }
                    }
                }
        */
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            string Ho=txtHo.Text;
            string TenDem=txtTendem.Text;
            string Ten=txtTen.Text;
            DateTime NgaySinh;
            try
            {
                NgaySinh=DateTime.ParseExact(mtbNgaysinh.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture);
            }
            catch (Exception){
                MessageBox.Show("Ngày Sinh Không hợp lệ");
                mtbNgaysinh.Select();
                return;
            }
            string GioiTinh = rbtNam.Checked ? "0" : "1";
            string DiaChi =txtDiachi.Text;
            string DeinThoai=txtDienthoai.Text;
            string Email=txtEmail.Text;
            string QueQuan=txtQuequan.Text;
            string DienThoai = txtDienthoai.Text.Trim();
            //khai báo ds tham số 
            List<CustomParameter> lstPara = new List<CustomParameter>(); 
            if (string.IsNullOrEmpty(msv))
            {
                sql = "ThemMoiSv";
              
            }
            else
            {
                sql = "updateSv";
                lstPara.Add(new CustomParameter()
                {
                    key = "@MaSinhVien",
                    value = msv
                }
                );
            }
            lstPara.Add(new CustomParameter()
            {
                key = "@Ho",
                value = Ho
            }
            );
            lstPara.Add(new CustomParameter()
            {
                key = "@TenDem",
                value = TenDem
            }
         );
            lstPara.Add(new CustomParameter()
            {
                key = "@Ten",
                value = Ten
            }
         );
            lstPara.Add(new CustomParameter()
            {
                key = "@NgaySinh",
                value = NgaySinh.ToString("yyyy-MM-dd")
            }
         );
            lstPara.Add(new CustomParameter()
            {
                key = "@GioiTinh",
                value = GioiTinh
            }
         );
            lstPara.Add(new CustomParameter()
            {
                key = "@QueQuan",
                value = QueQuan
            }
         );
            lstPara.Add(new CustomParameter()
            {
                key = "@DiaChi",
                value = DiaChi
            }
         );
            lstPara.Add(new CustomParameter()
            {
                key = "@Email",
                value = Email
            }
         );
            lstPara.Add(new CustomParameter()
            {
                key = "@DienThoai",
                value = DienThoai
            }
        );


            var rs = new Database().ExCute(sql, lstPara);
            if (rs==1)
            {
                if(string.IsNullOrEmpty(msv))
                {
                    MessageBox.Show("Thêm mới nhân mới thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công ");
                }
                this.Dispose();
            }else
            {
                MessageBox.Show("Thực thi thất bại");
            }
        }
    }
}
