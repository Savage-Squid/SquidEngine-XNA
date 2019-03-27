using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SquidEditor
{
    public partial class ErrorForm : Form
    {
        public ErrorForm(Exception err)
        {
            InitializeComponent();
            lblErrorMessage.Text = err.Message;
            textBox1.Text = err.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
