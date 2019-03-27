using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SquidEngine;
using SquidEngine.Drawing;
using SquidEditor.SelectorDialogs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace SquidEditor.Editors
{
    public partial class MaterialEditor : Form
    {
        #region Static Methods

        public static void LoadMaterialsInTreeNode(List<Material> materials, TreeNode rootNode, String imageKey)
        {
            rootNode.Nodes.Clear();
            foreach (Material material in materials)
            {
                TreeNode newNode = rootNode.Nodes.Add(rootNode.Text + "Node" + material.Name,
                    material.Name, imageKey, imageKey);
                newNode.Tag = material;
            }
        }

        public static void LoadFontsInTreeNode(List<SquidFont> fonts, TreeNode rootNode, String imageKey)
        {
            rootNode.Nodes.Clear();
            foreach (SquidFont font in fonts)
            {
                TreeNode newNode = rootNode.Nodes.Add(rootNode.Text + "Node" + font.Name,
                    font.Name, imageKey, imageKey);
                newNode.Tag = font;
            }
        }

        public static void LoadFontsInTreeView(TreeView treeView, bool showEmbeddedMaterials, bool showLocalMaterials)
        {
            // Load the Embedded items if needed
            if (showEmbeddedMaterials)
            {
                MaterialEditor.LoadFontsInTreeNode(
                    SceneManager.EmbeddedFonts, treeView.Nodes[0], "picture.png");
            }
            // Load the Global items
            MaterialEditor.LoadFontsInTreeNode(
                SceneManager.GlobalDataHolder.Fonts, treeView.Nodes[1], "picture.png");
            treeView.Nodes[1].Expand();
            // Load the Local items if needed
            if (showLocalMaterials)
            {
                MaterialEditor.LoadFontsInTreeNode(
                 SceneManager.ActiveScene.Fonts, treeView.Nodes[2], "picture.png");
                treeView.Nodes[2].Expand();
            }
            else
            {
                // remove the local textures node
                treeView.Nodes.RemoveAt(2);
            }
            // remove the Embedded node if it is not needed
            if (!showEmbeddedMaterials)
            {
                treeView.Nodes.RemoveAt(0);
            }
        }

        public static void LoadItemsInTreeView(TreeView treeView, bool showEmbeddedMaterials, bool showLocalMaterials)
        {
            // Load the Embedded items if needed
            if (showEmbeddedMaterials)
            {
                MaterialEditor.LoadMaterialsInTreeNode(
                    SceneManager.EmbeddedMaterials, treeView.Nodes[0], "picture.png");
            }
            // Load the Global items
            MaterialEditor.LoadMaterialsInTreeNode(
                SceneManager.GlobalDataHolder.Materials, treeView.Nodes[1], "picture.png");
            treeView.Nodes[1].Expand();
            // Load the Local items if needed
            if (showLocalMaterials)
            {
                MaterialEditor.LoadMaterialsInTreeNode(
                 SceneManager.ActiveScene.Materials, treeView.Nodes[2], "picture.png");
                treeView.Nodes[2].Expand();
            }
            else
            {
                // remove the local textures node
                treeView.Nodes.RemoveAt(2);
            }
            // remove the Embedded node if it is not needed
            if (!showEmbeddedMaterials)
            {
                treeView.Nodes.RemoveAt(0);
            }
        }

        #endregion

        #region Fields

        private Material _selectedMaterial;
        private SquidFont _selectedFont;
        private List<SquidFont> _fontList;
        private bool _showLocalTextures = false;
        private List<Material> _materialList;

        #endregion

        public bool ShowLocalTextures
        {
            get { return _showLocalTextures; }
            set { _showLocalTextures = value;  }
        }

        public List<Material> Materials
        {
            get { return _materialList; }
            set { _materialList = value; }
        }

        public Material SelectedMaterial
        {
            get { return _selectedMaterial; }
            set { _selectedMaterial = value; }
        }

        public MaterialEditor()
        {
            InitializeComponent();

            this.OnLoad(new EventArgs());
        }

        private void MaterialEditor_Load(object sender, EventArgs e)
        {
            panelTexture.Visible = false;
            LoadNodes();
            CheckStatusOfXMLControls();
        }
        private void LoadNodes()
        {
            LoadNodes(false, 0);
        }
        private void LoadNodes(bool selectnew, int index)
        {
            treeViewMaterials.Nodes[0].Nodes.Clear();
            if (_showLocalTextures)
            {
                _materialList = SceneManager.ActiveScene.Materials;
                _fontList = SceneManager.ActiveScene.Fonts;
            }
            else
            {
                _materialList = SceneManager.GlobalDataHolder.Materials;
                _fontList = SceneManager.GlobalDataHolder.Fonts;
            }
            LoadMaterialsInTreeNode(_materialList, treeViewMaterials.Nodes[0], "picture.png");
            treeViewMaterials.Nodes[0].Expand();

            LoadFontsInTreeNode(_fontList, treeViewMaterials.Nodes[1], "font.png");
            treeViewMaterials.Nodes[1].Expand();
            

            if (selectnew)
            {
                if (index == 0)
                {
                    foreach (TreeNode node in treeViewMaterials.Nodes[0].Nodes)
                    {
                        if (node.Tag == _materialList[_materialList.Count - 1])
                        {
                            treeViewMaterials.SelectedNode = node;
                            break;
                        }
                    }
                }
                else if (index == 1)
                {
                    foreach (TreeNode node in treeViewMaterials.Nodes[1].Nodes)
                    {
                        if (node.Tag == _fontList[_fontList.Count - 1])
                        {
                            treeViewMaterials.SelectedNode = node;
                            break;
                        }
                    }
                }
            }
        }
        
        private void SelectTexture(Material material)
        {
            materialPreviewControl.Visible = true;
            richTextBoxPreview.Visible = false;
            buttonSaveFont.Visible = false;
            labelSRdefinition.Visible = true;
            textBoxSourceRectanglesXML.Visible = true;
            buttonBrowseXML.Visible = true;
            buttonBrowsePath.Visible = true;

            _selectedMaterial = material;
            txtName.Text = material.Name;
            PreviewMaterial(material);
        }

        private void SelectFont(SquidFont font)
        {
            materialPreviewControl.Visible = false;
            richTextBoxPreview.Visible = true;
            buttonSaveFont.Visible = true;
            labelSRdefinition.Visible = false;
            textBoxSourceRectanglesXML.Visible = false;
            buttonBrowseXML.Visible = false;
            buttonBrowsePath.Visible = false;
            textBoxPath.Enabled = false;

            _selectedFont = font;
            txtName.Text = font.Name;
            PreviewFont(font);
        }

        private void PreviewFont(SquidFont font)
        {
            if (File.Exists(Path.Combine(SquidEngineProject.Instance.Path, font.Filename)))
            {
                StreamReader reader = new StreamReader(Path.Combine(SquidEngineProject.Instance.Path, font.Filename));
                string fontText = reader.ReadToEnd();
                reader.Close();
                reader = null;

                richTextBoxPreview.Text = fontText;
            }
            else
            {
                richTextBoxPreview.Text = Properties.Resources.SpriteFont;
            }
        }

        private void PreviewMaterial(Material material)
        {
            textBoxSourceRectanglesXML.Text = _selectedMaterial.AreasDefinitionFilename;
            materialPreviewControl.Texture = material.Texture;
        }

        #region Events

        private TreeNode GetRootParentNode(TreeNode node)
        {            
            while (node.Parent != null)
            {
                // if it's a tagged subnode, retrieve the tag's parent node
                node = node.Parent;
            }
            return node;
        }

        private void treeViewTextures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // if it's a material
            if (e.Node.Tag is Material)
            {
                SelectTexture(e.Node.Tag as Material);
                toolStripButtonDeleteMaterial.Enabled = true;
                panelTexture.Visible = true;

                if (_selectedMaterial.Filename.Length > 0 || txtName.Text == "")
                {
                    textBoxPath.Enabled = false;
                    buttonBrowsePath.Enabled = false;
                }
                else
                {
                    textBoxPath.Enabled = true;
                    buttonBrowsePath.Enabled = true;
                }

                if (_selectedMaterial.AreasDefinitionFilename.Length > 0)
                {
                    textBoxSourceRectanglesXML.Enabled = false;
                    buttonBrowseXML.Enabled = false;
                }
                else
                {
                    textBoxSourceRectanglesXML.Enabled = true;
                    buttonBrowseXML.Enabled = true;
                }
            }
            else if (e.Node.Tag is SquidFont)
            {
                SelectFont(e.Node.Tag as SquidFont);
                toolStripButtonDeleteMaterial.Enabled = true;
                panelTexture.Visible = true;
            }
            else
            {
                toolStripButtonDeleteMaterial.Enabled = false;
                panelTexture.Visible = false;
            }
        }

        private void toolStripButtonDeleteMaterial_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewMaterials.SelectedNode;
            if (selectedNode.Tag != null)
            {
                if (selectedNode.Tag is Material)
                {
                    if (_materialList.Contains(selectedNode.Tag as Material))
                    {
                        SquidEditorForm.Instance.RemoveFileFromContentProject((selectedNode.Tag as Material).Filename);
                        if((selectedNode.Tag as Material).AreasDefinitionFilename.Length > 0)
                            SquidEditorForm.Instance.RemoveFileFromContentProject((selectedNode.Tag as Material).AreasDefinitionFilename);
                        string rootPath = SquidEngineProject.Instance.Path;
                        if(File.Exists(Path.Combine(rootPath, (selectedNode.Tag as Material).Filename).Replace("/", "\\")))
                        {
                            File.Delete(Path.Combine(rootPath, (selectedNode.Tag as Material).Filename).Replace("/", "\\"));
                        }
                        if((selectedNode.Tag as Material).AreasDefinitionFilename.Length > 0)
                            File.Delete(Path.Combine(rootPath, (selectedNode.Tag as Material).AreasDefinitionFilename).Replace("/", "\\"));
                        _materialList.Remove(selectedNode.Tag as Material);
                        selectedNode.Remove();
                    }
                }
                else if (selectedNode.Tag is SquidFont)
                {
                    if (_fontList.Contains(selectedNode.Tag as SquidFont))
                    {
                        SquidEditorForm.Instance.RemoveFileFromContentProject((selectedNode.Tag as SquidFont).Filename);
                        string rootPath = SquidEngineProject.Instance.Path;
                        if(File.Exists(Path.Combine(rootPath, (selectedNode.Tag as SquidFont).Filename).Replace("/", "\\")))
                            File.Delete(Path.Combine(rootPath, (selectedNode.Tag as SquidFont).Filename).Replace("/", "\\"));
                        _fontList.Remove(selectedNode.Tag as SquidFont);
                        selectedNode.Remove();
                    }
                }
            }
        }

        private void toolStripButtonAddMaterial_Click(object sender, EventArgs e)
        {
            String newMaterialName;
            // TO-DO: replace this with a unique name generator
            newMaterialName = "New Material";
            Material newMaterial = new Material(newMaterialName, 
                SceneManager.EmbeddedMaterials[0].Texture,
                (_showLocalTextures?AssetScope.Local:AssetScope.Global));
            if (newMaterial.Scope == AssetScope.Local)
            {
                newMaterial.Parent = SceneManager.ActiveScene;
            }
            else
            {
                newMaterial.Parent = SceneManager.GlobalDataHolder;
            }
            _materialList.Add(newMaterial);
            LoadNodes(true, 0);
        }

        private void toolStripButtonCreateFont_Click(object sender, EventArgs e)
        {
            String newMaterialName;
            // TO-DO: replace this with a unique name generator
            newMaterialName = "New Font";
            SquidFont newMaterial = new SquidFont(newMaterialName,
                SceneManager.EmbeddedFonts[0].Font,
                (_showLocalTextures ? AssetScope.Local : AssetScope.Global));
            if (newMaterial.Scope == AssetScope.Local)
            {
                newMaterial.Parent = SceneManager.ActiveScene;
            }
            else
            {
                newMaterial.Parent = SceneManager.GlobalDataHolder;
            }
            newMaterial.Filename = "";
            _fontList.Add(newMaterial);
            LoadNodes(true, 1);
        }


        private void buttonBrowsePath_Click(object sender, EventArgs e)
        {
            OpenLoadImageDialog();
        }

        private void OpenLoadImageDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            /*
            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "./");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;    
             */
                fileDialog.Title = "Load texture";
                fileDialog.Filter = "PNG Files (*.png)|*.png|" +
                                    "TGA Files (*.tga)|*.tga|" +
                                    "BMP Files (*.bmp)|*.bmp";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (_selectedMaterial != null)
                    {
                        
                        string _tempFilename = fileDialog.FileName;

                        string _rootPath = Path.Combine(SquidEngineProject.Instance.Path,
                            SquidEngineProject.Instance.ContentFolderRelativePath + "Materials\\");
                        if (!_tempFilename.Contains(_rootPath))
                        {
                            if (!Directory.Exists(Path.Combine(SquidEngineProject.Instance.Path + "\\" + SquidEngineProject.Instance.ContentFolderRelativePath,
                    "Materials\\")))
                            {
                                Directory.CreateDirectory(Path.Combine(SquidEngineProject.Instance.Path + "\\" + SquidEngineProject.Instance.ContentFolderRelativePath,
                                "Materials\\"));
                            }

                            //Copy the file
                            string _destFile = Path.Combine(_rootPath, txtName.Text + Path.GetExtension(_tempFilename));

                            if (File.Exists(_destFile))
                            {
                                if (MessageBox.Show("This file already exists. I would not recommend continuing. If you would like to use the same image across multiple scenes, save the image as a project material.  Would you like to continue with the overwrite?", "File Already Exists", MessageBoxButtons.YesNo) !=
                                    System.Windows.Forms.DialogResult.Yes)
                                {
                                    return;
                                }
                            }
                            
                            File.Copy(_tempFilename, _destFile, true);
                            _tempFilename = _destFile;
                        }

                        _tempFilename = _tempFilename.Replace(SquidEngineProject.Instance.Path, "")
                            .Replace("\\", "/").TrimStart('/');
                        _selectedMaterial.Filename = _tempFilename;

                        System.IO.FileStream fs = new System.IO.FileStream(fileDialog.FileName, System.IO.FileMode.Open);
                        _selectedMaterial.Texture = Texture2D.FromStream(DrawingManager.GraphicsDevice, fs);
                        fs.Close();

                        //TODO: Need ability to choose where to have material
                        SquidEditorForm.Instance.AddFileToContentProject(_selectedMaterial.Filename, true, false,
                            "TextureProcessor", "TextureImporter");
                        _selectedMaterial.Scope = AssetScope.Local;
                        textBoxPath.Text = _selectedMaterial.Filename;
                        PreviewMaterial(_selectedMaterial);

                        textBoxPath.Enabled = false;
                        buttonBrowsePath.Enabled = false;
                    }
                }
        }

        private void OpenLoadXMLDialog()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            /*
            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "./");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;    
             */
            fileDialog.Title = "Load texture areas definition file";
            fileDialog.Filter = "XML Files (*.xml)|*.xml";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (_selectedMaterial != null)
                {
                    string _tempFilename = fileDialog.FileName;
                    _selectedMaterial.LoadAreasDefinition(_tempFilename);
                    string _rootPath = Path.Combine(SquidEngineProject.Instance.Path, 
                        SquidEngineProject.Instance.ContentFolderRelativePath + "MaterialXML\\");
                    if (!_tempFilename.Contains(_rootPath))
                    {
                        if (!Directory.Exists(Path.Combine(SquidEngineProject.Instance.Path + "\\" + SquidEngineProject.Instance.ContentFolderRelativePath,
                    "MaterialXML\\")))
                        {
                            Directory.CreateDirectory(Path.Combine(SquidEngineProject.Instance.Path + "\\" + SquidEngineProject.Instance.ContentFolderRelativePath,
                            "MaterialXML\\"));
                        }
                        //Copy the file
                        string _destFile = Path.Combine(_rootPath, Path.GetFileName(_tempFilename));
                        File.Copy(_tempFilename, _destFile, true);
                        _tempFilename = _destFile;
                    }
                    //TODO: check if it exists already and if not add it to the content project. (tricky)

                    _tempFilename = _tempFilename.Replace(SquidEngineProject.Instance.Path, "")
                        .Replace("\\", "/").TrimStart('/');
                    _selectedMaterial.AreasDefinitionFilename = _tempFilename;                    
                    textBoxSourceRectanglesXML.Text = _tempFilename;
                    SquidEditorForm.Instance.AddFileToContentProject(_tempFilename, false, true, null, null);
                    PreviewMaterial(_selectedMaterial);

                    textBoxSourceRectanglesXML.Enabled = false;
                    buttonBrowseXML.Enabled = false;
                }
            }
        }

        #endregion       

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (treeViewMaterials.SelectedNode.Parent.Index == 0)
            {
                _selectedMaterial.Name = txtName.Text;
                treeViewMaterials.SelectedNode.Text = txtName.Text;
                textBoxPath.Text = _selectedMaterial.Filename;
            }
            else if (treeViewMaterials.SelectedNode.Parent.Index == 1)
            {
                _selectedFont.Name = txtName.Text;
                treeViewMaterials.SelectedNode.Text = txtName.Text;
                textBoxPath.Text = _selectedFont.Filename;
            }
        }

        private void CheckStatusOfXMLControls()
        {
            bool enabled = true;
            if (String.IsNullOrEmpty(textBoxPath.Text.Trim()))
            {
                enabled = false;
            }
            textBoxSourceRectanglesXML.Enabled = enabled;
            labelSRdefinition.Enabled = enabled;
            buttonBrowseXML.Enabled = enabled;
        }

        private void buttonBrowseXML_Click(object sender, EventArgs e)
        {
            OpenLoadXMLDialog();
        }

        private void textBoxPath_TextChanged(object sender, EventArgs e)
        {
            if (treeViewMaterials.SelectedNode.Parent.Index == 0)
            {
                _selectedMaterial.Filename = textBoxPath.Text;
                CheckStatusOfXMLControls();
            }
            else if (treeViewMaterials.SelectedNode.Parent.Index == 1)
            {
                _selectedFont.Filename = textBoxPath.Text;
            }
        }

        private void buttonSaveFont_Click(object sender, EventArgs e)
        {
            string filename = "";

            if (_selectedFont.Filename.Length > 0)
            {
                filename = Path.Combine(SquidEngineProject.Instance.Path, _selectedFont.Filename).Replace("/", "\\");
            }
            else
            {
                if (!Directory.Exists(Path.Combine(SquidEngineProject.Instance.Path + "\\" + SquidEngineProject.Instance.ContentFolderRelativePath,
                    "Fonts\\")))
                {
                    Directory.CreateDirectory(Path.Combine(SquidEngineProject.Instance.Path + "\\" + SquidEngineProject.Instance.ContentFolderRelativePath,
                    "Fonts\\"));
                }
                filename = Path.Combine(SquidEngineProject.Instance.Path + "\\" + SquidEngineProject.Instance.ContentFolderRelativePath,
                    "Fonts\\" + _selectedFont.Name.ToLower() + ".spritefont");
            }

            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }

            StreamWriter writer = new StreamWriter(filename);
            writer.Write(richTextBoxPreview.Text);
            writer.Close();
            writer = null;

            _selectedFont.Filename = filename.Replace(SquidEngineProject.Instance.Path, "").Replace("\\", "/").TrimStart('/');
            textBoxPath.Text = _selectedFont.Filename;

            SquidEditorForm.Instance.AddFileToContentProject(_selectedFont.Filename, true, false, 
                "FontDescriptionProcessor", "FontDescriptionImporter");

            MessageBox.Show("Sucessfully Saved Font!\n\nYou will have to save and restart the application (Ctrl+R) to see your changes in the editor.",
                "Success");
        }
    } 
}