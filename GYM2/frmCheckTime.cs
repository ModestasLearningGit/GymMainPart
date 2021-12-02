using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM2
{
    public partial class frmCheckTime : Form
    {
        MemberDAL mdal = new MemberDAL();
        MemberBLL mbll = new MemberBLL();
        public frmCheckTime()
        {
            InitializeComponent();
        }

        private void frmCheckTime_Load(object sender, EventArgs e)
        {
            DataTable dt = mdal.onLoadCalculate_CheckTime();
            dgvData.DataSource = dt;
        }

        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtID.Text = dgvData.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvData.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvData.Rows[rowIndex].Cells[2].Value.ToString();
            txtTimeLeft.Text = dgvData.Rows[rowIndex].Cells[3].Value.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            if (keyword != null)
            {
                DataTable dt = mdal.search(keyword);
                dgvData.DataSource = dt;
            }
            else
            {
                DataTable dt = mdal.loadData_CheckTIme();
                dgvData.DataSource = dt;
            }
        }
    }
}
