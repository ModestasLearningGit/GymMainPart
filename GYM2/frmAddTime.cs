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
    public partial class frmAddTime : Form
    {

        MemberBLL mbll = new MemberBLL();
        MemberDAL mdal = new MemberDAL();
        public frmAddTime()
        {
            InitializeComponent();

        }

        private void frmAddTime_Load(object sender, EventArgs e)
        {
            DataTable dt = mdal.onLoadCalculate();
            dgvSearch.DataSource = dt;

        }
        
        private void btnAddTime_Click(object sender, EventArgs e)
        {
            int monthToAdd = 0;
            bool isSuccess = false;
            if(cmbAddedTime.Text == "")
            {
                MessageBox.Show("PLEASE ADD TIME");
            }
            else
            {
                mbll.ID = int.Parse(txtID.Text);
                mbll.FirstName = txtFirstName.Text;
                mbll.Lastname = txtLastName.Text;
                mbll.TimeValid = txtValidTill.Text;
                mbll.TimeLeft = int.Parse(txtTimeLeft.Text);
                if(cmbAddedTime.Text == "1 Month")
                {
                    monthToAdd = 1;    
                }
                else if(cmbAddedTime.Text == "3 Months")
                {
                    monthToAdd = 3;
                }
                else if(cmbAddedTime.Text == "6 Months")
                {
                    monthToAdd = 6;
                }
                else
                {
                    monthToAdd = 12;
                }
                isSuccess = mdal.SelectAddTime(mbll, monthToAdd);
                if(isSuccess)
                {
                    MessageBox.Show("Time added succesfully");
                }
                else
                {
                    MessageBox.Show("Failed to add time");
                }
            }
            clear();
            dgvSearch.DataSource = mdal.afterAdd();
        }

        private void dgvSearch_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtID.Text = dgvSearch.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvSearch.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvSearch.Rows[rowIndex].Cells[2].Value.ToString();
            txtValidTill.Text = dgvSearch.Rows[rowIndex].Cells[3].Value.ToString();
            txtTimeLeft.Text = dgvSearch.Rows[rowIndex].Cells[4].Value.ToString();
            
        }
      

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            txtID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtValidTill.Clear();
            txtTimeLeft.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;

            if(keyword != null)
            {
                DataTable dt = mdal.search(keyword);
                dgvSearch.DataSource = dt;
            }
            else
            {
                DataTable dt = mdal.afterAdd();
                dgvSearch.DataSource = dt;
            }
        }
    }
}
