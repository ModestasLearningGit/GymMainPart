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
    public partial class frmAddMember : Form
    {
        MemberBLL mbll = new MemberBLL();
        MemberDAL mdal = new MemberDAL();

        
        public frmAddMember()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            mbll.FirstName = txtFNAME.Text;
            mbll.Lastname = txtLNAME.Text;
            if(rbMale.Checked == true)
            {
                mbll.Gender = "Male";
            }
            else
            {
                mbll.Gender = "Female";
            }
            mbll.Email = txtEMAIL.Text;
            mbll.DOB = dtpDOB.Text.ToString();
            mbll.Address = txtAddress.Text;
            mbll.Mobile = txtMobile.Text;

            bool insert = mdal.Insert(mbll);

            if(insert == true)
            {
                MessageBox.Show("DATA INSERTED SUCCESFULLY");
                Clear();
            }
            else
            {
                MessageBox.Show("DATA FAILED TO INSERT");
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            txtFNAME.Clear();
            txtLNAME.Clear();
            rbMale.Checked = false;
            rbFemale.Checked = false;
            txtEMAIL.Clear();
            dtpDOB.Value = DateTime.Now;
            txtAddress.Clear();
            txtMobile.Clear();
        }

     
    }
}
