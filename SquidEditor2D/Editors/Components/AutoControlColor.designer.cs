namespace SquidEditor.Editors.Components
{
    partial class AutoControlColor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelFriendlyName = new System.Windows.Forms.Label();
            this.panelValues = new System.Windows.Forms.Panel();
            this.labelB = new System.Windows.Forms.Label();
            this.labelA = new System.Windows.Forms.Label();
            this.textBoxValueB = new System.Windows.Forms.TextBox();
            this.textBoxValueA = new System.Windows.Forms.TextBox();
            this.labelR = new System.Windows.Forms.Label();
            this.labelG = new System.Windows.Forms.Label();
            this.textBoxValueR = new System.Windows.Forms.TextBox();
            this.textBoxValueG = new System.Windows.Forms.TextBox();
            this.panelValues.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelFriendlyName
            // 
            this.labelFriendlyName.Location = new System.Drawing.Point(0, 3);
            this.labelFriendlyName.Name = "labelFriendlyName";
            this.labelFriendlyName.Size = new System.Drawing.Size(93, 13);
            this.labelFriendlyName.TabIndex = 0;
            this.labelFriendlyName.Text = "labelFriendlyName";
            // 
            // panelValues
            // 
            this.panelValues.Controls.Add(this.labelB);
            this.panelValues.Controls.Add(this.labelA);
            this.panelValues.Controls.Add(this.textBoxValueB);
            this.panelValues.Controls.Add(this.textBoxValueA);
            this.panelValues.Controls.Add(this.labelR);
            this.panelValues.Controls.Add(this.labelG);
            this.panelValues.Controls.Add(this.textBoxValueR);
            this.panelValues.Controls.Add(this.textBoxValueG);
            this.panelValues.Location = new System.Drawing.Point(102, 0);
            this.panelValues.Name = "panelValues";
            this.panelValues.Size = new System.Drawing.Size(130, 43);
            this.panelValues.TabIndex = 6;
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(-1, 26);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(14, 13);
            this.labelB.TabIndex = 20;
            this.labelB.Text = "B";
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Location = new System.Drawing.Point(65, 26);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(14, 13);
            this.labelA.TabIndex = 19;
            this.labelA.Text = "A";
            // 
            // textBoxValueB
            // 
            this.textBoxValueB.AcceptsReturn = true;
            this.textBoxValueB.Location = new System.Drawing.Point(17, 23);
            this.textBoxValueB.Name = "textBoxValueB";
            this.textBoxValueB.Size = new System.Drawing.Size(42, 20);
            this.textBoxValueB.TabIndex = 18;
            this.textBoxValueB.Text = "255";
            this.textBoxValueB.Validated += new System.EventHandler(this.textBoxValue_Validated);
            this.textBoxValueB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // textBoxValueA
            // 
            this.textBoxValueA.Location = new System.Drawing.Point(82, 23);
            this.textBoxValueA.Name = "textBoxValueA";
            this.textBoxValueA.Size = new System.Drawing.Size(42, 20);
            this.textBoxValueA.TabIndex = 17;
            this.textBoxValueA.Text = "255";
            this.textBoxValueA.Validated += new System.EventHandler(this.textBoxValue_Validated);
            this.textBoxValueA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // labelR
            // 
            this.labelR.AutoSize = true;
            this.labelR.Location = new System.Drawing.Point(0, 3);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(15, 13);
            this.labelR.TabIndex = 8;
            this.labelR.Text = "R";
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.Location = new System.Drawing.Point(65, 3);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(15, 13);
            this.labelG.TabIndex = 7;
            this.labelG.Text = "G";
            // 
            // textBoxValueR
            // 
            this.textBoxValueR.Location = new System.Drawing.Point(17, 0);
            this.textBoxValueR.Name = "textBoxValueR";
            this.textBoxValueR.Size = new System.Drawing.Size(42, 20);
            this.textBoxValueR.TabIndex = 6;
            this.textBoxValueR.Text = "255";
            this.textBoxValueR.Validated += new System.EventHandler(this.textBoxValue_Validated);
            this.textBoxValueR.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // textBoxValueG
            // 
            this.textBoxValueG.Location = new System.Drawing.Point(82, 0);
            this.textBoxValueG.Name = "textBoxValueG";
            this.textBoxValueG.Size = new System.Drawing.Size(42, 20);
            this.textBoxValueG.TabIndex = 5;
            this.textBoxValueG.Text = "255";
            this.textBoxValueG.Validated += new System.EventHandler(this.textBoxValue_Validated);
            this.textBoxValueG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValue_KeyDown);
            // 
            // AutoControlColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelValues);
            this.Controls.Add(this.labelFriendlyName);
            this.Name = "AutoControlColor";
            this.Size = new System.Drawing.Size(232, 43);
            this.panelValues.ResumeLayout(false);
            this.panelValues.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelFriendlyName;
        private System.Windows.Forms.Panel panelValues;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.TextBox textBoxValueR;
        private System.Windows.Forms.TextBox textBoxValueG;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label labelA;
        private System.Windows.Forms.TextBox textBoxValueB;
        private System.Windows.Forms.TextBox textBoxValueA;
    }
}
