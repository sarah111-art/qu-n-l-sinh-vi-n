﻿using System;
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
    public partial class frmDSGV : Form
    {
        public frmDSGV()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDSGV_Load);

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            Tukhoa=txtTuKhoa.Text;
            loadDSGV();
        }
        private string Tukhoa = "";
        private void loadDSGV()
        {
            //string sql = "selectAllGV";
            List<CustomParameter> lstPara = new List<CustomParameter>();
            lstPara.Add(new CustomParameter()
            { 
            key= "@Tukhoa",
            value= Tukhoa

            });
            dgvDSGV.DataSource = new Database().SelectData("SelectAllGV", lstPara);

            //dgvDSGV.DataSource = new Database().SelectData(sql,lstPara);
        }
        private void frmDSGV_Load(object sender, EventArgs e)
        {
            loadDSGV();
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            new frmGV(null).ShowDialog();
            loadDSGV();
        }

        private void dgvDSGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var mgv =dgvDSGV.Rows[e.RowIndex].Cells["magiaovien"].Value.ToString();
                new frmGV(mgv).ShowDialog();
                loadDSGV();
            }
        }
    }
}
