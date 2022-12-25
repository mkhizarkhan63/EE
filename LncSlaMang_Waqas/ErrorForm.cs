using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LncSlaMang
{
    public partial class ErrorForm : Form
    {
        public ErrorForm(string errorMsg, string errorCode)
        {
            InitializeComponent();
            lblCode.Text = "Error "+errorCode;
            lblMsg.Text = errorMsg;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       


    }
}
