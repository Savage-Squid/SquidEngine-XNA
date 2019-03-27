using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace SquidEditor.Editors.Components
{
    public partial class AutoControlColor : AutoControl
    {
        public AutoControlColor()
            : base("{R:255 G:255 B:255 A:255}")
        {
            InitializeComponent();
        }

        public override void SetFriendlyName(String value)
        {
            labelFriendlyName.Text = value;
        }

        public override void SetFriendlyNameWidth(int width)
        {
            labelFriendlyName.Size = new Size(width, labelFriendlyName.Size.Height);
            panelValues.Location = new Point(width + LabelPadding, panelValues.Location.Y);
        }

        private byte[] ParseColor(String value)
        {
            byte[] bytes = new byte[4];
            value = value.Replace("{R:", "").Replace("G:", "")
                .Replace("B:", "").Replace("A:", "").Replace("}", "");
            int delimiter = value.LastIndexOf(' ');
            String[] separator = { " " };
            String[] v = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            
            bytes[0] = byte.Parse(v[0], CultureInfo.InvariantCulture);
            bytes[1] = byte.Parse(v[1], CultureInfo.InvariantCulture);
            bytes[2] = byte.Parse(v[2], CultureInfo.InvariantCulture);
            bytes[3] = byte.Parse(v[3], CultureInfo.InvariantCulture);
            return bytes;
        }

        private string CombineInts()
        {
            return "{R:" + textBoxValueR.Text + " G:" + textBoxValueG.Text
                + " B:" + textBoxValueB.Text + " A:" + textBoxValueA.Text + "}";
        }


        public override void SetValue(String value)
        {            
            if (IsValueValid(value))
            {
                byte[] values = ParseColor(value);
                textBoxValueR.Text = values[0].ToString(CultureInfo.InvariantCulture);
                textBoxValueG.Text = values[1].ToString(CultureInfo.InvariantCulture);
                textBoxValueB.Text = values[2].ToString(CultureInfo.InvariantCulture);
                textBoxValueA.Text = values[3].ToString(CultureInfo.InvariantCulture);
                this.OnValueChanged(EventArgs.Empty);
                base.SetValue(value);
            }
            else
            {
                RestoreValue("Cannot convert \"" + value + "\" to Color");
            }            
        }

        protected override bool IsValueValid(String value)
        {
            try
            {
                ParseColor(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override object GetValue()
        {
            string currentValue = CombineInts();
            if (IsValueValid(currentValue))
            {
                byte[] values = ParseColor(currentValue);
                return new SquidEngine.Drawing.SquidColor(values[0], values[1], values[2], values[3]);
            }
            else
            {
                throw new Exception("Invalid Color value: \"" + currentValue + "\"");
            }          
        }
                
        private void textBoxValue_Validated(object sender, EventArgs e)
        {
            SetValue(CombineInts());
        }

        private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetValue(CombineInts());
            }
        }
    }
}
