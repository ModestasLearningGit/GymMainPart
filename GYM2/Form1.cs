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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pADDNEW_Click(object sender, EventArgs e)
        {
            frmAddMember am = new frmAddMember();
            am.Show();
        }
        private void pADDTIME_Click(object sender, EventArgs e)
        {
            frmAddTime at = new frmAddTime();
            at.Show();
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            frmCheckTime ct = new frmCheckTime();
            ct.Show();
        }
        private void pnlUpdateInfo_Click(object sender, EventArgs e)
        {
            frmUpdateInfo ui = new frmUpdateInfo();
            ui.Show();
        }

        private void pnlRemoveMember_Click(object sender, EventArgs e)
        {
            frmDeleteMember dm = new frmDeleteMember();
            dm.Show();
        }
        private void pnlRemoveTime_Click(object sender, EventArgs e)
        {
            frmRemoveTIme rmt = new frmRemoveTIme();
            rmt.Show();
        }

        #region non_needed
        private void pnlRemoveTime_DoubleClick(object sender, EventArgs e)
        {

        }
        private void pADDTIME_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pnlUpdateInfo_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pnlRemoveMember_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pnlRemoveTime_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

       
    }
}
