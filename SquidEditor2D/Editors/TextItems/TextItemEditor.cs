using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SquidEngine;
using SquidEngine.Drawing;
using SquidEditor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SquidEditor.SelectorDialogs;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using XnaColor = Microsoft.Xna.Framework.Color;
using Color = System.Drawing.Color;
using SquidEngine.SceneItems;

namespace SquidEditor.Editors.TextItems
{
    public partial class TextItemEditor : SceneItemEditor
    {
        private bool _ignoreNumericUpDownEvent = false;
        private bool _ignoreHTMLBoxClick = false;

        public ZoomBox ZoomBox
        {
            get;
            set;
        }

        public override SceneItem SceneItem
        {
            get
            {
                return base.SceneItem;
            }
            set
            {
                base.SceneItem = value;
                TextItem = base.SceneItem as TextItem;
            }
        }

        private TextItem TextItem
        {
            get { return TextItemEditorControl.TextItem; }
            set { TextItemEditorControl.TextItem = value; }
        }

        public TextItemEditor()
        {
            InitializeComponent();
        }

        private void TextItemEditor_Load(object sender, EventArgs e)
        {
            TextItemEditorControl.ParentEditor = this;
            ZoomBox = new ZoomBox();
            ZoomBox.SetToolStripButtomZoomIn(toolStripButtonZoomIn);
            ZoomBox.SetToolStripButtomZoomOut(toolStripButtonZoomOut);
            ZoomBox.SetToolStripButtomZoomNormal(toolStripButtonZoomNormal);

            // hack around designer bug that resize the control
            labelTextureName.Size = new Size(labelTextureName.Size.Width, 22);
            pictureBoxTint.BackColor = SquidEditorForm.GetGDIColor(TextItemEditorControl.TextItem.Tint);
            _ignoreNumericUpDownEvent = true;
            numericUpDownTintRed.Value = TextItemEditorControl.TextItem.Tint.R;
            numericUpDownTintGreen.Value = TextItemEditorControl.TextItem.Tint.G;
            numericUpDownTintBlue.Value = TextItemEditorControl.TextItem.Tint.B;
            textBoxColorHTML.Text = GetHTMLFromColor(TextItem.Tint);
            _ignoreNumericUpDownEvent = false;            
            _ignoreNumericUpDownEvent = true;
            _ignoreNumericUpDownEvent = false;
        }

        #region Events


        private void pictureBoxTint_Click(object sender, EventArgs e)
        {
            OpenTintSelectionDialog();
        }

        private void OpenTintSelectionDialog()
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.FullOpen = true;
            colorDialog.Color = SquidEditorForm.GetGDIColor(TextItemEditorControl.TextItem.Tint);
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxTint.BackColor = colorDialog.Color;
                TextItemEditorControl.TextItem.Tint = new XnaColor(colorDialog.Color.R,
                    colorDialog.Color.G, colorDialog.Color.B, TextItemEditorControl.TextItem.Tint.A);
                _ignoreNumericUpDownEvent = true;
                numericUpDownTintRed.Value = (decimal)colorDialog.Color.R;
                numericUpDownTintGreen.Value = (decimal)colorDialog.Color.G;
                numericUpDownTintBlue.Value = (decimal)colorDialog.Color.B;
                _ignoreNumericUpDownEvent = false;
                ApplyUpDownColors();
            }
        }

        private void ApplyUpDownColors()
        {
            if (_ignoreNumericUpDownEvent == false)
            {
                Color color = Color.FromArgb((int)numericUpDownTintRed.Value,
                    (int)numericUpDownTintGreen.Value, (int)numericUpDownTintBlue.Value);
                pictureBoxTint.BackColor = color;
                TextItemEditorControl.TextItem.Tint = new XnaColor(color.R,
                        color.G, color.B, TextItemEditorControl.TextItem.Tint.A);
                textBoxColorHTML.Text = GetHTMLFromColor(TextItem.Tint);
            }
        }

        private void numericUpDownTintRed_ValueChanged(object sender, EventArgs e)
        {
            ApplyUpDownColors();
        }

        private void numericUpDownTintGreen_ValueChanged(object sender, EventArgs e)
        {
            ApplyUpDownColors();
        }

        private void numericUpDownTintBlue_ValueChanged(object sender, EventArgs e)
        {
            ApplyUpDownColors();
        }

        private void textBoxColorHTML_Validated(object sender, EventArgs e)
        {
            _ignoreHTMLBoxClick = false;
            String text = textBoxColorHTML.Text.Trim().ToUpperInvariant();
            if (text.Length == 7 && text.Substring(0, 1) == "#")
            {
                // ignore the leading char '#'
                text = text.Substring(1);
            }
            if (text.Length == 6)
            {
                try
                {
                    decimal red = Int32.Parse(text.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                    decimal green = Int32.Parse(text.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                    decimal blue = Int32.Parse(text.Substring(4, 2), NumberStyles.AllowHexSpecifier);
                    _ignoreNumericUpDownEvent = true;
                    numericUpDownTintRed.Value = red;
                    numericUpDownTintGreen.Value = green;
                    numericUpDownTintBlue.Value = blue;
                    _ignoreNumericUpDownEvent = false;
                    ApplyUpDownColors();
                }
                catch
                {

                }
            }
            textBoxColorHTML.Text = GetHTMLFromColor(TextItem.Tint);
        }

        private void textBoxColorHTML_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBoxColorHTML_Validated(sender, EventArgs.Empty);
            }
        }

        internal static String GetHTMLFromColor(XnaColor color)
        {
            String html = "#";
            html += color.R.ToString("X2", CultureInfo.InvariantCulture);
            html += color.G.ToString("X2", CultureInfo.InvariantCulture);
            html += color.B.ToString("X2", CultureInfo.InvariantCulture);
            return html;
        }

        private void textBoxColorHTML_Enter(object sender, EventArgs e)
        {
            textBoxColorHTML.SelectAll();
        }

        private void toolStripButtonShowWholeImage_Click(object sender, EventArgs e)
        {
            if (toolStripButtonShowWholeImage.Text == "Show Whole Image")
            {
                toolStripButtonShowWholeImage.Text = "Show Partial Image";
                TextItemEditorControl.ShowWholeImage = true;
            }
            else
            {
                toolStripButtonShowWholeImage.Text = "Show Whole Image";
                TextItemEditorControl.ShowWholeImage = false;
            }
        }

        private void textBoxColorHTML_Click(object sender, EventArgs e)
        {
            if (_ignoreHTMLBoxClick == false)
            {
                textBoxColorHTML.SelectAll();
                _ignoreHTMLBoxClick = true;
            }
        }

        #endregion

        private void groupBoxTexture_Enter(object sender, EventArgs e)
        {

        }

        private void TextItemEditorControl_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void TextItemEditorControl_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void buttonSelectTexture_Click(object sender, EventArgs e)
        {
            FontSelectorDialog dialog = new FontSelectorDialog();
            dialog.SelectedFont = TextItemEditorControl.TextItem.Font;
            dialog.ShowLocalFonts = ItemIsLocal;
            if (dialog.ShowDialog() == DialogResult.OK
                && TextItemEditorControl.TextItem.Font != dialog.SelectedFont)
            {
                this.TextItem.Font = dialog.SelectedFont;
                labelTextureName.Text = dialog.SelectedFont.ToString();
            }
        }
    }
}