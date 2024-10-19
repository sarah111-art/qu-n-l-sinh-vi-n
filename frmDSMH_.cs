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
    public partial class frmDSMH_ : Form
    {
        public frmDSMH_()
        {
            this.Load += new EventHandler(frmDSMH_Load);

            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private string tukhoa = "";
        private void dgvDSMH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                var mamh=dgvDSMH.Rows[e.RowIndex].Cells["mamonhoc"].Value.ToString();
                new frmMonHoc(mamh).ShowDialog();
                LoadDSMH();
            }
        }
        private void LoadDSMH()
        {
            List<
                CustomParameter> lstPara = new List< CustomParameter > ();
            lstPara.Add(new CustomParameter()
                    { 
                key="@tukhoa",
                value=tukhoa
            
            
                    });
            dgvDSMH.DataSource = new Database().SelectData("selectAllMonHoc",lstPara);
        }
        private void frmDSMH_Load(object sender, EventArgs e)
        {
            LoadDSMH();
            dgvDSMH.Columns["mamonhoc"].HeaderText = "Mã môn học";
            dgvDSMH.Columns["tenmonhoc"].HeaderText = "Tên môn học";
            dgvDSMH.Columns["sotinchi"].HeaderText = "Số tín chỉ";


        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            tukhoa=txtTimKiem.Text;
            LoadDSMH() ;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            new frmMonHoc(null).ShowDialog();
            LoadDSMH();
        }
    }
}
