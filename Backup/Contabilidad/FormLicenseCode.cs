using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Contabilidad
{
    public partial class FormLicenseCode : Form
    {
        public string pass;

        public FormLicenseCode()
        {
            InitializeComponent();
            pass = "";
            txtPass.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            pass = txtPass.Text;
            txtPass.Text = "";
            this.Hide();
        }
    }
}
