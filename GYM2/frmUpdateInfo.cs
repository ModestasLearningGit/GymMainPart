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
    public partial class frmUpdateInfo : Form
    {

        MemberBLL mbll = new MemberBLL();
        MemberDAL mdal = new MemberDAL();
        public frmUpdateInfo()
        {
            InitializeComponent();
        }
        private void frmUpdateInfo_Load(object sender, EventArgs e)
        {
            DataTable dt = mdal.loadData_UpdateInfo();
            dgvData.DataSource = dt;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvData_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            string gender;

            try
            {
                    txtID.Text = dgvData.Rows[rowIndex].Cells[0].Value.ToString();
                    txtFname.Text = dgvData.Rows[rowIndex].Cells[1].Value.ToString();
                    txtLname.Text = dgvData.Rows[rowIndex].Cells[2].Value.ToString();
                    gender = dgvData.Rows[rowIndex].Cells[3].Value.ToString();
                    if (gender == "Male")
                    {
                        rbMale.Checked = true;
                        rbFemale.Checked = false;
                    }
                    else
                    {
                        rbMale.Checked = false;
                        rbFemale.Checked = true;
                    }
                    txtEmail.Text = dgvData.Rows[rowIndex].Cells[4].Value.ToString();
                    dtpDOB.Value = DateTime.Parse(dgvData.Rows[rowIndex].Cells[5].Value.ToString());
                    txtAddress.Text = dgvData.Rows[rowIndex].Cells[6].Value.ToString();
                    txtMobile.Text = dgvData.Rows[rowIndex].Cells[7].Value.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if(keyword != "")
            {
                DataTable dt = mdal.search_UpdateInfo(keyword);
                dgvData.DataSource = dt;
            }
            else
            {
                DataTable dt = mdal.loadData_UpdateInfo();
                dgvData.DataSource = dt;
            }

           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            mbll.ID = int.Parse(txtID.Text);
            mbll.FirstName = txtFname.Text;
            mbll.Lastname = txtLname.Text;
            if(rbMale.Checked == true)
            {
                mbll.Gender = "Male";
            }
            else
            {
                mbll.Gender = "Female";
            }
            mbll.Email = txtEmail.Text;
            mbll.DOB = dtpDOB.Value.ToString();
            mbll.Address = txtAddress.Text;
            mbll.Mobile = txtMobile.Text;

            bool isSuccess = mdal.upadte_UpdateInfo(mbll);

            if(isSuccess)
            {
                MessageBox.Show("DATA UPADTES SUCCESSFULLY");
            }
            else
            {
                MessageBox.Show("DATA FAILED TO UPDATE");
            }
            dgvData.DataSource = mdal.loadData_UpdateInfo();
            Clear();

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtID.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtMobile.Clear();
            rbMale.Checked = false;
            rbFemale.Checked = false;
            dtpDOB.Value = DateTime.Now;
        }

      
    }
}
