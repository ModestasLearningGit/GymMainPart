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
    public partial class frmRemoveTIme : Form
    {
        MemberBLL mbll = new MemberBLL();
        MemberDAL mdal = new MemberDAL();
        public frmRemoveTIme()
        {
            InitializeComponent();
        }

        private void frmRemoveTIme_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = mdal.onLoadCalculate();
            dgvTable.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            if (keyword != null)
            {
                DataTable dt = mdal.search(keyword);
                dgvTable.DataSource = dt;
            }
            else
            {
                DataTable dt = mdal.afterAdd();
                dgvTable.DataSource = dt;
            }
        }

        private void dgvTable_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtID.Text = dgvTable.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvTable.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvTable.Rows[rowIndex].Cells[2].Value.ToString();
            txtValidTill.Text = dgvTable.Rows[rowIndex].Cells[3].Value.ToString();
            txtTimeLeft.Text = dgvTable.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            bool isSuccess = false;
            int daysToRemove;
            if (txtDaysToRemove.Text == "")
            {
                daysToRemove = 0;      
            }
            else
            {
                daysToRemove = int.Parse(txtDaysToRemove.Text);
            }
            
            if (int.Parse(txtTimeLeft.Text) <= 0) 
            {
                MessageBox.Show("No Time Left to Remove");
            }
            else
            {
                DateTime dateTime1 = DateTime.Parse(txtValidTill.Text);
                int newDaysLeft = int.Parse(txtTimeLeft.Text) - daysToRemove;
                if(newDaysLeft < 0)
                {
                    newDaysLeft = 0;
                }
                DateTime newValidTill = dateTime1.AddDays(-daysToRemove);

                mbll.ID = int.Parse(txtID.Text);
                mbll.TimeLeft = newDaysLeft;
                mbll.TimeValid = newValidTill.ToString();

                isSuccess = mdal.upadte_DleteTime(mbll);

                if(isSuccess == true)
                {
                    MessageBox.Show("TIME REMOVED");
                    DataTable dt = new DataTable();
                    dt = mdal.onLoadCalculate();
                    dgvTable.DataSource = dt;
                    Clear();
                }
                else
                {
                    MessageBox.Show("TIME FAILED TO REMOVE");
                }
            }
        }
        private void Clear()
        {
            txtID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtTimeLeft.Clear();
            txtValidTill.Clear();
            txtDaysToRemove.Clear();

        }
    }
}
