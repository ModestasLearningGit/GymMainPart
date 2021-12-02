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

namespace GYM2
{
    public partial class frmDeleteMember : Form
    {

        MemberBLL mbll = new MemberBLL();
        MemberDAL mdal = new MemberDAL();
        public frmDeleteMember()
        {
            InitializeComponent();
        }

        private void frmDeleteMember_Load(object sender, EventArgs e)
        {
            DataTable dt = mdal.Select_Delete();
            dgvTable.DataSource = dt;
        }

        private void dgvTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtID.Text = dgvTable.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvTable.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvTable.Rows[rowIndex].Cells[1].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (keyword != null)
            {
                DataTable dt = mdal.Search_Delete(keyword);
                dgvTable.DataSource = dt;
            }
            else
            {
                DataTable dt = mdal.Select_Delete();
                dgvTable.DataSource = dt;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            mbll.ID = int.Parse(txtID.Text);
            bool isSuccess = mdal.delete(mbll);

            if(isSuccess == true)
            {
                MessageBox.Show("Deleted Succesfully");
                DataTable dt = mdal.Select_Delete();
                dgvTable.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Delete Failed");
            }
            
        }
        private void Clear()
        {
            txtID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
        }
    }
}
