using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SquidEditor.Wizards
{
    public partial class NewSceneWizard : Form
    {
        public String NewSceneName
        {
            get { return textBoxName.Text; }
            set { textBoxName.Text = value; }
        }

        public NewSceneWizard()
        {
            InitializeComponent();
        }
    }
}