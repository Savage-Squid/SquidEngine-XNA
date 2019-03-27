using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Windows.Forms;
using SquidEngine.SceneItems;
using Microsoft.Xna.Framework;
using GdiColor = System.Drawing.Color;
using XnaColor = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SquidEngine.Components;
using SquidEngine.Attributes;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SquidEditor.Tools
{
    public partial class SpriteSheetGenerator : Form
    {
        private int numSections = 1;
        private const int Y_OFFSET = 25;
        private List<TextBox> listNames = new List<TextBox>();
        private List<TextBox> listX = new List<TextBox>();
        private List<TextBox> listY = new List<TextBox>();
        private List<TextBox> listWidth = new List<TextBox>();
        private List<TextBox> listHeight = new List<TextBox>();

        public const int MAX_SAVED_ENTRIES = 50;
        public List<String> LastOutputDir { get; set; }
        public List<String> LastInputDir { get; set; }
        public List<String> LastNames { get; set; }

        public List<SpriteInfo> Sprites
        {
            get;
            set;
        }

        public GdiColor BaseTextureColor
        {
            get;
            set;
        }

        public SpriteSheetGenerator()
        {
            InitializeComponent();
            this.Sprites = new List<SpriteInfo>();
        }

        private List<String> ConvertStringToList(String input)
        {
            if (input == null)
            {
                return new List<String>();
            }
            else
            {
                return new List<String>(input.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private String ConvertListToString(List<String> input)
        {
            String output = "";
            for (int i = 0; i < input.Count; i++)
            {
                if (i > 0)
                {
                    output += ",";
                }
                output += input[i];
            }
            return output;
        }

        private void LoadCombobox(ComboBox combo, List<String> list)
        {
            combo.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                combo.Items.Add(list[i]);
            }
            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        private void CheckForNewEntry(String entry, List<String> list)
        {
            if (list.Contains(entry) == true)
            {
                list.Remove(entry);
            }
            list.Insert(0, entry);
            if (list.Count > MAX_SAVED_ENTRIES)
            {
                list.RemoveRange(MAX_SAVED_ENTRIES - 1, list.Count - 1);
            }
        }
        
        private void SpriteSheetGenerator_Load(object sender, EventArgs e)
        {        
            SquidEditorPreferences prefs = SquidEditorForm.Instance.Preferences;            
            this.LastInputDir = ConvertStringToList(prefs.ToolSpritesheetLastInputFolder);
            this.LastOutputDir = ConvertStringToList(prefs.ToolSpritesheetLastOutputFolder);
            this.LastNames = ConvertStringToList(prefs.ToolSpritesheetLastOutputName);
            LoadCombobox(comboBoxInputDir, this.LastInputDir);
            LoadCombobox(comboBoxOutputDir, this.LastOutputDir);
            LoadCombobox(comboBoxName, this.LastNames);            
            numericUpDownSafeBorderSize.Value = (decimal)prefs.ToolSpritesheetLastSafeBorderSize;
            checkBoxAlphaCorrection.Checked = prefs.ToolSpritesheetLastCorrectTransparencyColor;
            checkBoxOverrideBaseColor.Checked = prefs.ToolSpritesheetLastReplaceBaseColor;
            checkBoxPowerOf2.Checked = prefs.ToolSpritesheetLastPadTexturePowerOfTwo;
            XnaColor backColor = prefs.ToolSpritesheetLastBaseColor;
            this.BaseTextureColor = GdiColor.FromArgb(backColor.R, backColor.G, backColor.B);
            pictureBoxTint.BackColor = this.BaseTextureColor;
            CheckValidation();
        }

        private void CheckValidation()
        {
            bool enableState = false;
            bool d = Directory.Exists(comboBoxInputDir.Text);
            if (Directory.Exists(comboBoxInputDir.Text) && Directory.Exists(comboBoxOutputDir.Text)
                && String.IsNullOrEmpty(comboBoxName.Text) == false)
            {
                enableState = true;
            }
            buttonGenerate.Enabled = enableState;

        }

        #region Generation

        public void StartSpriteSheetGeneration()
        {
            SquidEditorPreferences prefs = SquidEditorForm.Instance.Preferences;
            CheckForNewEntry(comboBoxInputDir.Text, this.LastInputDir);
            CheckForNewEntry(comboBoxOutputDir.Text, this.LastOutputDir);
            CheckForNewEntry(comboBoxName.Text, this.LastNames);
            prefs.ToolSpritesheetLastInputFolder = ConvertListToString(this.LastInputDir);
            prefs.ToolSpritesheetLastOutputFolder = ConvertListToString(this.LastOutputDir);
            prefs.ToolSpritesheetLastOutputName = ConvertListToString(this.LastNames);
            prefs.ToolSpritesheetLastSafeBorderSize = (int)numericUpDownSafeBorderSize.Value;
            prefs.ToolSpritesheetLastCorrectTransparencyColor = checkBoxAlphaCorrection.Checked;
            prefs.ToolSpritesheetLastReplaceBaseColor = checkBoxOverrideBaseColor.Checked;
            prefs.ToolSpritesheetLastPadTexturePowerOfTwo = checkBoxPowerOf2.Checked;
            prefs.ToolSpritesheetLastBaseColor = new XnaColor(this.BaseTextureColor.R,
                    this.BaseTextureColor.G, this.BaseTextureColor.B);

            this.Sprites.Clear();
            String path = comboBoxInputDir.Text;
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] rgFiles = di.GetFiles("*.*");
            foreach (FileInfo fi in rgFiles)
            {
                String fileExt = fi.Extension.ToLower();
                if (fileExt == ".png" || fileExt == ".jpg"
                    || fileExt == ".dds" || fileExt == ".bmp" || fileExt == ".tga")
                {
                    Console.WriteLine(Sprites.Count + "] Packing sprite " + fi.Name);
                    SpriteInfo newInfo = new SpriteInfo(Path.GetFileNameWithoutExtension(fi.Name));

                    System.IO.FileStream fs = new System.IO.FileStream(fi.FullName, System.IO.FileMode.Open);
                    newInfo.Texture2D = Texture2D.FromStream(SquidEngine.Drawing.DrawingManager.GraphicsDevice, fs);
                    fs.Close();

                    newInfo.PixelData = new uint[newInfo.Texture2D.Width * newInfo.Texture2D.Height];
                    newInfo.Texture2D.GetData<uint>(newInfo.PixelData);
                    Sprites.Add(newInfo);
                }
            }
            PackSprites((int)numericUpDownSafeBorderSize.Value, comboBoxOutputDir.Text, comboBoxName.Text);
        }


        /// <summary>
        /// Comparison function for sorting sprites by size.
        /// </summary>
        public int CompareSpriteSizes(SpriteInfo a, SpriteInfo b)
        {
            int aSize = a.Height * 1024 + a.Width;
            int bSize = b.Height * 1024 + b.Width;
            return bSize.CompareTo(aSize);
        }

        /// <summary>
        /// Comparison function for sorting sprites by their original indices.
        /// </summary>
        static int CompareSpriteIndices(SpriteInfo a, SpriteInfo b)
        {
            return a.Index.CompareTo(b.Index);
        }

        private void PackSprites(int safeBorderSize, String outputPath, String sheetName)
        {
            for (int i = 0; i < this.Sprites.Count; i++)
            {
                SpriteInfo sprite = this.Sprites[i];
                sprite.Width = sprite.Texture2D.Width + safeBorderSize * 2;
                sprite.Height = sprite.Texture2D.Height + safeBorderSize * 2;
                sprite.Index = i;
            }
            this.Sprites.Sort(CompareSpriteSizes);

            // Work out how big the output bitmap should be.
            int outputWidth = GuessOutputWidth(this.Sprites);
            int outputHeight = 0;
            int totalSpriteSize = 0;
            // Choose positions for each sprite, one at a time.
            for (int i = 0; i < this.Sprites.Count; i++)
            {
                PositionSprite(this.Sprites, i, outputWidth);

                outputHeight = Math.Max(outputHeight, this.Sprites[i].Y + this.Sprites[i].Height);

                totalSpriteSize += this.Sprites[i].Width * this.Sprites[i].Height;
            }

            // Sort the sprites back into index order.
            this.Sprites.Sort(CompareSpriteIndices);

            Console.WriteLine("Packed {0} sprites into a {1}x{2} sheet, {3}% efficiency",
                this.Sprites.Count, outputWidth, outputHeight, totalSpriteSize * 100 / outputWidth / outputHeight);

            GenerateSpriteSheet(outputWidth, outputHeight, safeBorderSize, outputPath, sheetName);
        }

        public int GetNextPowerOf2(int value)
        {
            int[] powers = { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192 };
            for (int i = 0; i < powers.Length; i++)
            {
                if (i == powers.Length - 1 || powers[i] >= value)
                {
                    return powers[i];
                }
            }
            return powers[powers.Length - 1];
        }

        public void GenerateSpriteSheet(int width, int height, int safeBorderSize, String outputPath, String sheetName)
        {            
            if (checkBoxPowerOf2.Checked == true)
            {
                // if not a power of 2 already
                if ((width & (width - 1)) != 0)
                {
                    width = GetNextPowerOf2(width);
                }
                if ((height & (height - 1)) != 0)
                {
                    height = GetNextPowerOf2(height);
                }
            }
            bool overrideTransColor = checkBoxOverrideBaseColor.Checked;
            XnaColor baseColor = XnaColor.Black;
            if (overrideTransColor == true)
            {
                baseColor = new XnaColor(this.BaseTextureColor.R,
                    this.BaseTextureColor.G, this.BaseTextureColor.B, 0);
            }
            uint[] destinationData = new uint[width * height];
            for (int i = 0; i < destinationData.Length; i++)
            {
                destinationData[i] = baseColor.PackedValue;
            }            
            Texture2D spriteSheet = new Texture2D(SquidEngine.Drawing.DrawingManager.GraphicsDevice,
                width, height);
            foreach (SpriteInfo sprite in this.Sprites)
            {
                Texture2D source = sprite.Texture2D;
                int x = sprite.X;
                int y = sprite.Y;

                int w = source.Width;
                int h = source.Height;

                int b = safeBorderSize;

                int sourceSize = sprite.Texture2D.Width;
                int destSize = width;                
                
                // Copy the main sprite data to the output sheet.
                sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, 0, w, h),
                    ref destinationData, destSize, new Point(x + b, y + b),
                    baseColor.PackedValue, overrideTransColor);
                
                // Copy a border strip from each edge of the sprite, creating
                // a padding area to avoid filtering problems if the
                // sprite is scaled or rotated.
                for (int i = 0; i < safeBorderSize; i++)
                {
                    sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, 0, 1, h),
                                       ref destinationData, destSize, new Point(x + i, y + b),
                                       baseColor.PackedValue, overrideTransColor);

                    sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(w - 1, 0, 1, h),
                                       ref destinationData, destSize,
                                       new Point(x + w + i + b, y + b), baseColor.PackedValue, overrideTransColor);

                    sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, 0, w, 1),
                                       ref destinationData, destSize,
                                       new Point(x + b, y + i), baseColor.PackedValue, overrideTransColor);

                    sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, h - 1, w, 1),
                                       ref destinationData, destSize,
                                       new Point(x + b, y + h + i + b), baseColor.PackedValue, overrideTransColor);
                    
                    // Copy a single pixel from each corner of the sprite,
                    // filling in the corners of the padding area.                    
                    for (int j = 0; j < b; j++)
                    {
                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, 0, 1, 1),
                                           ref destinationData, destSize, new Point(x + j, y + i), baseColor.PackedValue, 
                                           overrideTransColor);
                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, 0, 1, 1),
                                           ref destinationData, destSize, new Point(x + i, y + j), baseColor.PackedValue,
                                           overrideTransColor);

                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(w - 1, 0, 1, 1),
                                       ref destinationData, destSize, new Point(x + w + b + i, y + j),
                                       baseColor.PackedValue, overrideTransColor);
                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(w - 1, 0, 1, 1),
                                       ref destinationData, destSize, new Point(x + w + b + j, y + i), baseColor.PackedValue,
                                       overrideTransColor);

                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, h - 1, 1, 1),
                                       ref destinationData, destSize, new Point(x + i, y + h + b + j), baseColor.PackedValue, 
                                       overrideTransColor);
                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(0, h - 1, 1, 1),
                                       ref destinationData, destSize, new Point(x + j, y + h + b + i), baseColor.PackedValue,
                                       overrideTransColor);

                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(w - 1, h - 1, 1, 1),
                                           ref destinationData, destSize, new Point(x + w + b + i, y + h + b + j),
                                           baseColor.PackedValue, overrideTransColor);
                        sprite.CopyPixels(sprite.PixelData, sourceSize, new Rectangle(w - 1, h - 1, 1, 1),
                                           ref destinationData, destSize, new Point(x + w + b + j, y + h + b + i), 
                                           baseColor.PackedValue, overrideTransColor);
                    }                 
                }
                sprite.Area = new Rectangle(x + safeBorderSize, y + safeBorderSize, 
                    sprite.Texture2D.Width, sprite.Texture2D.Height);                 
                 
            }
            if (checkBoxAlphaCorrection.Checked == true)
            {
                CorrectAlphaBorders(destinationData, width, height, 2);
            }
            spriteSheet.SetData<uint>(destinationData);

            string format = "";
            if (comboBoxFileFormat.SelectedIndex == 0) // PNG
                format = "png";
            else
                format = "jpg";

            String outputFilename = Path.Combine(outputPath, sheetName + "." + format);
            if(!File.Exists(outputFilename))
                File.Create(outputFilename);

            using (Stream s = File.OpenWrite(outputFilename))
            {
                if (format == "png")
                    spriteSheet.SaveAsPng(s, spriteSheet.Width, spriteSheet.Height);
                else
                    spriteSheet.SaveAsJpeg(s, spriteSheet.Width, spriteSheet.Height);
            }
            ExportXML(Path.Combine(outputPath, sheetName + ".xml"));
        }

        public void ExportXML(String filename)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode _rootNode = doc.CreateElement("Areas");
            doc.AppendChild(_rootNode);
            foreach (SpriteInfo sprite in this.Sprites)
            {
                XmlNode _rectNode = doc.CreateElement("Area");
                _rectNode.Attributes.Append(doc.CreateAttribute("Key")).InnerText = sprite.Name;
                _rectNode.Attributes.Append(doc.CreateAttribute("X")).InnerText = sprite.Area.X.ToString(CultureInfo.InvariantCulture);
                _rectNode.Attributes.Append(doc.CreateAttribute("Y")).InnerText = sprite.Area.Y.ToString(CultureInfo.InvariantCulture);
                _rectNode.Attributes.Append(doc.CreateAttribute("W")).InnerText = sprite.Area.Width.ToString(CultureInfo.InvariantCulture);
                _rectNode.Attributes.Append(doc.CreateAttribute("H")).InnerText = sprite.Area.Height.ToString(CultureInfo.InvariantCulture);
                _rootNode.AppendChild(_rectNode);
            }
            doc.Save(filename);
            doc = null;
        }


        /// <summary>
        /// Works out where to position a single sprite.
        /// </summary>
        public void PositionSprite(List<SpriteInfo> sprites,
                                   int index, int outputWidth)
        {
            int x = 0;
            int y = 0;

            while (true)
            {
                // Is this position free for us to use?
                int intersects = FindIntersectingSprite(sprites, index, x, y);

                if (intersects < 0)
                {
                    sprites[index].X = x;
                    sprites[index].Y = y;

                    return;
                }

                // Skip past the existing sprite that we collided with.
                x = sprites[intersects].X + sprites[intersects].Width;

                // If we ran out of room to move to the right,
                // try the next line down instead.
                if (x + sprites[index].Width > outputWidth)
                {
                    x = 0;
                    y++;
                }
            }
        }

        /// <summary>
        /// Checks if a proposed sprite position collides with anything
        /// that we already arranged.
        /// </summary>
        public int FindIntersectingSprite(List<SpriteInfo> sprites,
                                          int index, int x, int y)
        {
            int w = sprites[index].Width;
            int h = sprites[index].Height;
            for (int i = 0; i < index; i++)
            {
                if (sprites[i].X >= x + w)
                    continue;

                if (sprites[i].X + sprites[i].Width <= x)
                    continue;

                if (sprites[i].Y >= y + h)
                    continue;

                if (sprites[i].Y + sprites[i].Height <= y)
                    continue;

                return i;
            }
            return -1;
        }


        /// <summary>
        /// Heuristic guesses what might be a good output width for a list of sprites.
        /// </summary>
        public int GuessOutputWidth(List<SpriteInfo> sprites)
        {
            // Gather the widths of all our sprites into a temporary list.
            List<int> widths = new List<int>();

            foreach (SpriteInfo sprite in sprites)
            {
                widths.Add(sprite.Width);
            }

            // Sort the widths into ascending order.
            widths.Sort();

            // Extract the maximum and median widths.
            int maxWidth = widths[widths.Count - 1];
            int medianWidth = widths[widths.Count / 2];

            // Heuristic assumes an NxN grid of median sized sprites.
            int width = medianWidth * (int)Math.Round(Math.Sqrt(sprites.Count));

            // Make sure we never choose anything smaller than our largest sprite.
            return Math.Max(width, maxWidth);
        }

        private void CheckAroundPixel(uint[] pixels, bool[] mask, uint color, Point pixRef, int width, int height, int step, int maxStep)
        {
            if (step <= maxStep)
            {
                step++;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x != 0 || y != 0)
                        {
                            Point pix = new Point(pixRef.X + x, pixRef.Y + y);
                            if (pix.X >= 0 && pix.Y >= 0 && pix.X < width && pix.Y < height)
                            {
                                uint pos = (uint)(pix.Y * width + pix.X);
                                if (mask[pos] == false)
                                {                                    
                                    pixels[pos] = color;
                                    CheckAroundPixel(pixels, mask, color, pix, width, height, step, maxStep);
                                }                                
                            }
                        }
                    }
                }
            }
        }

        public void CorrectAlphaBorders(uint[] pixels, int width, int height, int maxSteps)
        {
            bool[] pixelsMask = new bool[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                // if Alpha > 0
                if (pixels[i] >= 16777216)
                {
                    pixelsMask[i] = true;
                }
            }
            for (int i = 0; i < pixelsMask.Length; i++)
            {
                // if alpha, check each borders to fill it with a copy
                if (pixelsMask[i] == true)
                {
                    double widthSize = (double)width;
                    Point pixRef = new Point((int)(i % widthSize), (int)Math.Floor(i / widthSize));
                    uint pixRefPos = (uint)(pixRef.Y * width + pixRef.X);
                    uint colorRef = pixels[pixRefPos];
                    XnaColor referenceColor = XnaColor.Black;
                    referenceColor.PackedValue = colorRef;
                    // remove alpha value
                    referenceColor.A = 0;
                    colorRef = referenceColor.PackedValue;
                    CheckAroundPixel(pixels, pixelsMask, colorRef, pixRef, width, height, 1, maxSteps);                               
                }
            }
        }

        #endregion

        #region Events

        private void OpenTintSelectionDialog()
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.FullOpen = true;
            colorDialog.Color = this.BaseTextureColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxTint.BackColor = colorDialog.Color;
                this.BaseTextureColor = colorDialog.Color;                
            }
        }

        private void buttonBrowseInput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                comboBoxInputDir.Text = folderDialog.SelectedPath;
            }
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                comboBoxOutputDir.Text = folderDialog.SelectedPath;
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                StartSpriteSheetGeneration();
                SquidEditorForm.ShowInfoMessage("Spritesheet \"" + comboBoxName.Text + "\" generated successfully");
            }
            catch (Exception ex)
            {
                SquidEditorForm.ShowErrorMessage("Could not generate spritesheet: " + ex.Message);
            }
        }

        #endregion

        private void textBoxInputDir_TextChanged(object sender, EventArgs e)
        {
            CheckValidation();
        }

        private void textBoxOutputDir_TextChanged(object sender, EventArgs e)
        {
            CheckValidation();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            CheckValidation();
        }

        private void pictureBoxTint_Click(object sender, EventArgs e)
        {
            OpenTintSelectionDialog();
        }

        private void comboBoxInputDir_SelectedValueChanged(object sender, EventArgs e)
        {
            CheckValidation();
        }

        private void buttonAddSection_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.buttonAddSection.Location = new System.Drawing.Point(this.buttonAddSection.Location.X, this.buttonAddSection.Location.Y + Y_OFFSET);
            this.buttonGenerateXML.Location = new System.Drawing.Point(this.buttonGenerateXML.Location.X, this.buttonGenerateXML.Location.Y + Y_OFFSET);

            Label l = new Label();
            l.Text = "Name";
            l.Location = new System.Drawing.Point(this.labelName.Location.X, this.labelName.Location.Y + (Y_OFFSET * numSections));
            l.Size = this.labelName.Size;

            Label l2 = new Label();
            l2.Text = "X";
            l2.Location = new System.Drawing.Point(this.labelX.Location.X, this.labelX.Location.Y + (Y_OFFSET * numSections));
            l2.Size = this.labelX.Size;

            Label l3 = new Label();
            l3.Text = "Y";
            l3.Location = new System.Drawing.Point(this.labelY.Location.X, this.labelY.Location.Y + (Y_OFFSET * numSections));
            l3.Size = this.labelY.Size;

            Label l4 = new Label();
            l4.Text = "Width";
            l4.Location = new System.Drawing.Point(this.labelWidth.Location.X, this.labelWidth.Location.Y + (Y_OFFSET * numSections));
            l4.Size = this.labelWidth.Size;

            Label l5 = new Label();
            l5.Text = "Height";
            l5.Location = new System.Drawing.Point(this.labelHeight.Location.X, this.labelHeight.Location.Y + (Y_OFFSET * numSections));
            l5.Size = this.labelHeight.Size;

            Label l6 = new Label();
            l6.Text = (numSections + 1) + ".";
            l6.Location = new System.Drawing.Point(labelSectionNum.Location.X, labelSectionNum.Location.Y + (Y_OFFSET * numSections));
            l6.Size = labelSectionNum.Size;
            l6.Font = labelSectionNum.Font;

            TextBox t = new TextBox();
            t.Size = this.textBoxName1.Size;
            t.Location = new System.Drawing.Point(this.textBoxName1.Location.X, this.textBoxName1.Location.Y + (Y_OFFSET * numSections));
            t.Name = "textBoxName" + (numSections+1);

            TextBox t2 = new TextBox();
            t2.Size = this.textBoxX1.Size;
            t2.Location = new System.Drawing.Point(this.textBoxX1.Location.X, this.textBoxX1.Location.Y + (Y_OFFSET * numSections));
            t2.Name = "textBoxX" + (numSections+1);

            TextBox t3 = new TextBox();
            t3.Size = this.textBoxY1.Size;
            t3.Location = new System.Drawing.Point(this.textBoxY1.Location.X, this.textBoxY1.Location.Y + (Y_OFFSET * numSections));
            t3.Name = "textBoxY" + (numSections + 1);

            TextBox t4 = new TextBox();
            t4.Size = this.textBoxWidth1.Size;
            t4.Location = new System.Drawing.Point(this.textBoxWidth1.Location.X, this.textBoxWidth1.Location.Y + (Y_OFFSET * numSections));
            t4.Name = "textBoxWidth" + (numSections + 1);

            TextBox t5 = new TextBox();
            t5.Size = this.textBoxHeight1.Size;
            t5.Location = new System.Drawing.Point(this.textBoxHeight1.Location.X, this.textBoxHeight1.Location.Y + (Y_OFFSET * numSections));
            t5.Name = "textBoxHeight" + (numSections + 1);

            this.tabControl1.TabPages[1].Controls.Add(l);
            this.tabControl1.TabPages[1].Controls.Add(l2);
            this.tabControl1.TabPages[1].Controls.Add(l3);
            this.tabControl1.TabPages[1].Controls.Add(l4);
            this.tabControl1.TabPages[1].Controls.Add(l5);
            this.tabControl1.TabPages[1].Controls.Add(l6);
            this.tabControl1.TabPages[1].Controls.Add(t);
            this.tabControl1.TabPages[1].Controls.Add(t2);
            this.tabControl1.TabPages[1].Controls.Add(t3);
            this.tabControl1.TabPages[1].Controls.Add(t4);
            this.tabControl1.TabPages[1].Controls.Add(t5);
            this.ResumeLayout(false);

            numSections++;
        }

        private void buttonGenerateXML_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode _rootNode = doc.CreateElement("Areas");
            doc.AppendChild(_rootNode);
            bool something = false;
            for (int i = 0; i < this.numSections; ++i)
            {
                bool valid = true; // all forms are filled out
                string name = "",x = "",y = "",width = "",height = "";
                foreach(Control c in this.tabControl1.TabPages[1].Controls)
                {
                    if(c.Name.EndsWith((i+1).ToString()))
                    {
                        if (c.Text.Length < 1)
                        {
                            valid = false;
                            break;
                        }
                        else
                        {
                            if(c.Name.Contains("textBoxName"))
                                name = c.Text;
                            else if(c.Name.Contains("textBoxX"))
                                x = c.Text;
                            else if(c.Name.Contains("textBoxY"))
                                y = c.Text;
                            else if(c.Name.Contains("textBoxWidth"))
                                width = c.Text;
                            else if(c.Name.Contains("textBoxHeight"))
                                height = c.Text;
                        }
                    }
                }

                if (valid)
                {
                    something = true;
                    XmlNode n = doc.CreateElement("Area");
                    n.Attributes.Append(doc.CreateAttribute("Key")).InnerText = name;
                    n.Attributes.Append(doc.CreateAttribute("X")).InnerText = x;
                    n.Attributes.Append(doc.CreateAttribute("Y")).InnerText = y;
                    n.Attributes.Append(doc.CreateAttribute("W")).InnerText = width;
                    n.Attributes.Append(doc.CreateAttribute("H")).InnerText = height;
                    _rootNode.AppendChild(n);
                }
                    
            }

            if (!something)
            {
                MessageBox.Show("Unable to create XML file, please fill out atleast 1 field");
                return;
            }

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "XML File|*.xml";
            fileDialog.Title = "Save SpriteSheet XML File";
            fileDialog.ShowDialog();

            if (fileDialog.FileName != "")
            {
                doc.Save(fileDialog.FileName);
            }

            doc = null;
        }
    }
}
