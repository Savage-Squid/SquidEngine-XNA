namespace SquidEditor.Editors.TextItems
{
    partial class TextItemEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextItemEditor));
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxTint = new System.Windows.Forms.GroupBox();
            this.textBoxColorHTML = new System.Windows.Forms.TextBox();
            this.numericUpDownTintBlue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTintGreen = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTintRed = new System.Windows.Forms.NumericUpDown();
            this.pictureBoxTint = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxTexture = new System.Windows.Forms.GroupBox();
            this.buttonSelectTexture = new System.Windows.Forms.Button();
            this.labelTextureName = new System.Windows.Forms.Label();
            this.groupBoxProperties = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomNormal = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowWholeImage = new System.Windows.Forms.ToolStripButton();
            this.defaultControlPanel = new DefaultControlPanel();
            this.TextItemEditorControl = new SquidEditor.Editors.TextItems.TextItemEditorControl();
            this.groupBoxTint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTintBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTintGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTintRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTint)).BeginInit();
            this.groupBoxTexture.SuspendLayout();
            this.groupBoxProperties.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "folder_picture.png");
            this.imageListTreeView.Images.SetKeyName(1, "picture.png");
            // 
            // groupBoxTint
            // 
            this.groupBoxTint.Controls.Add(this.textBoxColorHTML);
            this.groupBoxTint.Controls.Add(this.numericUpDownTintBlue);
            this.groupBoxTint.Controls.Add(this.numericUpDownTintGreen);
            this.groupBoxTint.Controls.Add(this.numericUpDownTintRed);
            this.groupBoxTint.Controls.Add(this.pictureBoxTint);
            this.groupBoxTint.Controls.Add(this.label5);
            this.groupBoxTint.Controls.Add(this.label3);
            this.groupBoxTint.Controls.Add(this.label4);
            this.groupBoxTint.Location = new System.Drawing.Point(6, 101);
            this.groupBoxTint.Name = "groupBoxTint";
            this.groupBoxTint.Size = new System.Drawing.Size(188, 105);
            this.groupBoxTint.TabIndex = 4;
            this.groupBoxTint.TabStop = false;
            this.groupBoxTint.Text = "Tint Color";
            // 
            // textBoxColorHTML
            // 
            this.textBoxColorHTML.Location = new System.Drawing.Point(16, 79);
            this.textBoxColorHTML.Name = "textBoxColorHTML";
            this.textBoxColorHTML.Size = new System.Drawing.Size(72, 20);
            this.textBoxColorHTML.TabIndex = 4;
            this.textBoxColorHTML.Text = "#FFFFFF";
            this.textBoxColorHTML.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxColorHTML.Validated += new System.EventHandler(this.textBoxColorHTML_Validated);
            this.textBoxColorHTML.Click += new System.EventHandler(this.textBoxColorHTML_Click);
            this.textBoxColorHTML.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxColorHTML_KeyDown);
            this.textBoxColorHTML.Enter += new System.EventHandler(this.textBoxColorHTML_Enter);
            // 
            // numericUpDownTintBlue
            // 
            this.numericUpDownTintBlue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTintBlue.Location = new System.Drawing.Point(144, 79);
            this.numericUpDownTintBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownTintBlue.Name = "numericUpDownTintBlue";
            this.numericUpDownTintBlue.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownTintBlue.TabIndex = 12;
            this.numericUpDownTintBlue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTintBlue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownTintBlue.ValueChanged += new System.EventHandler(this.numericUpDownTintBlue_ValueChanged);
            // 
            // numericUpDownTintGreen
            // 
            this.numericUpDownTintGreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTintGreen.Location = new System.Drawing.Point(144, 53);
            this.numericUpDownTintGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownTintGreen.Name = "numericUpDownTintGreen";
            this.numericUpDownTintGreen.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownTintGreen.TabIndex = 11;
            this.numericUpDownTintGreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTintGreen.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownTintGreen.ValueChanged += new System.EventHandler(this.numericUpDownTintGreen_ValueChanged);
            // 
            // numericUpDownTintRed
            // 
            this.numericUpDownTintRed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownTintRed.Location = new System.Drawing.Point(144, 26);
            this.numericUpDownTintRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownTintRed.Name = "numericUpDownTintRed";
            this.numericUpDownTintRed.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownTintRed.TabIndex = 10;
            this.numericUpDownTintRed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTintRed.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownTintRed.ValueChanged += new System.EventHandler(this.numericUpDownTintRed_ValueChanged);
            // 
            // pictureBoxTint
            // 
            this.pictureBoxTint.BackColor = System.Drawing.Color.White;
            this.pictureBoxTint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxTint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxTint.Location = new System.Drawing.Point(28, 26);
            this.pictureBoxTint.Name = "pictureBoxTint";
            this.pictureBoxTint.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxTint.TabIndex = 9;
            this.pictureBoxTint.TabStop = false;
            this.pictureBoxTint.Click += new System.EventHandler(this.pictureBoxTint_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Blue";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Green";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Red";
            // 
            // groupBoxTexture
            // 
            this.groupBoxTexture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTexture.Controls.Add(this.buttonSelectTexture);
            this.groupBoxTexture.Controls.Add(this.labelTextureName);
            this.groupBoxTexture.Location = new System.Drawing.Point(6, 19);
            this.groupBoxTexture.Name = "groupBoxTexture";
            this.groupBoxTexture.Size = new System.Drawing.Size(182, 76);
            this.groupBoxTexture.TabIndex = 0;
            this.groupBoxTexture.TabStop = false;
            this.groupBoxTexture.Text = "SpriteFont";
            // 
            // buttonSelectTexture
            // 
            this.buttonSelectTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectTexture.Location = new System.Drawing.Point(46, 40);
            this.buttonSelectTexture.Name = "buttonSelectTexture";
            this.buttonSelectTexture.Size = new System.Drawing.Size(85, 23);
            this.buttonSelectTexture.TabIndex = 1;
            this.buttonSelectTexture.Text = "Select Font";
            this.buttonSelectTexture.UseVisualStyleBackColor = true;
            this.buttonSelectTexture.Click += new System.EventHandler(this.buttonSelectTexture_Click);
            // 
            // labelTextureName
            // 
            this.labelTextureName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextureName.AutoEllipsis = true;
            this.labelTextureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTextureName.Location = new System.Drawing.Point(3, 16);
            this.labelTextureName.MaximumSize = new System.Drawing.Size(173, 21);
            this.labelTextureName.Name = "labelTextureName";
            this.labelTextureName.Size = new System.Drawing.Size(173, 21);
            this.labelTextureName.TabIndex = 0;
            this.labelTextureName.Text = "Default";
            this.labelTextureName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxProperties
            // 
            this.groupBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxProperties.Controls.Add(this.groupBoxTexture);
            this.groupBoxProperties.Controls.Add(this.groupBoxTint);
            this.groupBoxProperties.Location = new System.Drawing.Point(469, 28);
            this.groupBoxProperties.Name = "groupBoxProperties";
            this.groupBoxProperties.Size = new System.Drawing.Size(200, 364);
            this.groupBoxProperties.TabIndex = 3;
            this.groupBoxProperties.TabStop = false;
            this.groupBoxProperties.Text = "Properties";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonZoomOut,
            this.toolStripButtonZoomNormal,
            this.toolStripButtonZoomIn,
            this.toolStripButtonShowWholeImage});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(679, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomOut.Image")));
            this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomOut.Text = "Zoom Out";
            // 
            // toolStripButtonZoomNormal
            // 
            this.toolStripButtonZoomNormal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomNormal.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomNormal.Image")));
            this.toolStripButtonZoomNormal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomNormal.Name = "toolStripButtonZoomNormal";
            this.toolStripButtonZoomNormal.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomNormal.Text = "Normal Zoom";
            // 
            // toolStripButtonZoomIn
            // 
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomIn.Image")));
            this.toolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            this.toolStripButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomIn.Text = "Zoom In";
            // 
            // toolStripButtonShowWholeImage
            // 
            this.toolStripButtonShowWholeImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonShowWholeImage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowWholeImage.Image")));
            this.toolStripButtonShowWholeImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowWholeImage.Name = "toolStripButtonShowWholeImage";
            this.toolStripButtonShowWholeImage.Size = new System.Drawing.Size(113, 22);
            this.toolStripButtonShowWholeImage.Text = "Show Whole Image";
            this.toolStripButtonShowWholeImage.Click += new System.EventHandler(this.toolStripButtonShowWholeImage_Click);
            // 
            // defaultControlPanel
            // 
            this.defaultControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultControlPanel.Location = new System.Drawing.Point(469, 398);
            this.defaultControlPanel.Name = "defaultControlPanel";
            this.defaultControlPanel.Size = new System.Drawing.Size(200, 24);
            this.defaultControlPanel.TabIndex = 6;
            // 
            // TextItemEditorControl
            // 
            this.TextItemEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextItemEditorControl.Location = new System.Drawing.Point(0, 28);
            this.TextItemEditorControl.Name = "TextItemEditorControl";
            this.TextItemEditorControl.Size = new System.Drawing.Size(463, 394);
            this.TextItemEditorControl.TabIndex = 1;
            this.TextItemEditorControl.Text = "TextItemEditorControl";
            this.TextItemEditorControl.Zoom = 0F;
            // 
            // TextItemEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 433);
            this.Controls.Add(this.defaultControlPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.TextItemEditorControl);
            this.Controls.Add(this.groupBoxProperties);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(300, 469);
            this.Name = "TextItemEditor";
            this.Text = "Text Item Editor";
            this.Load += new System.EventHandler(this.TextItemEditor_Load);
            this.groupBoxTint.ResumeLayout(false);
            this.groupBoxTint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTintBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTintGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTintRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTint)).EndInit();
            this.groupBoxTexture.ResumeLayout(false);
            this.groupBoxProperties.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageListTreeView;
        public TextItemEditorControl TextItemEditorControl;
        private System.Windows.Forms.GroupBox groupBoxTint;
        private System.Windows.Forms.NumericUpDown numericUpDownTintBlue;
        private System.Windows.Forms.NumericUpDown numericUpDownTintGreen;
        private System.Windows.Forms.NumericUpDown numericUpDownTintRed;
        private System.Windows.Forms.PictureBox pictureBoxTint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxTexture;
        private System.Windows.Forms.Button buttonSelectTexture;
        private System.Windows.Forms.Label labelTextureName;
        private System.Windows.Forms.GroupBox groupBoxProperties;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomNormal;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowWholeImage;
        private System.Windows.Forms.TextBox textBoxColorHTML;
        private DefaultControlPanel defaultControlPanel;
    }
}