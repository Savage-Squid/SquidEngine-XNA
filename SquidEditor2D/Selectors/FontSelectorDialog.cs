using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SquidEngine;
using SquidEngine.Drawing;
using SquidEditor.Editors;

namespace SquidEditor.SelectorDialogs
{
    public partial class FontSelectorDialog : Form
    {
        #region Fields

        private SquidFont _SelectedFont;
        private bool _ShowLocalFonts = false;

        #endregion

        public bool ShowLocalFonts
        {
            get { return _ShowLocalFonts; }
            set { _ShowLocalFonts = value;  }
        }

        public SquidFont SelectedFont
        {
            get { return _SelectedFont; }
            set { _SelectedFont = value; }
        } 

        public FontSelectorDialog()
        {
            InitializeComponent();            
        }

        private void TextureSelectorDialog_Load(object sender, EventArgs e)
        {
            MaterialEditor.LoadFontsInTreeView(treeViewMaterials, true, _ShowLocalFonts);
            SelectTextureNode();
        }

        private void SelectTextureNode()
        {
            /*
            TreeNode selectedNode = null;
            if (_SelectedFont.TextureScope == TextureScope.Default)
            {
                selectedNode = treeViewTextures.Nodes[0];
            }
            else if (_SelectedFont.TextureScope == TextureScope.Global)
            {
                foreach (TreeNode node in treeViewTextures.Nodes[1].Nodes)
                {
                    // if it's an untagged texture
                    if (node.ImageIndex == 1)
                    {
                        if (node.Text == _SelectedFont.UID)
                        {
                            selectedNode = node;
                            break;
                        }
                    }
                    // check the tagged textures
                    else
                    {
                        foreach (TreeNode subNode in node.Nodes)
                        {
                            if (subNode.Text == _SelectedFont.UID)
                            {
                                selectedNode = subNode;
                                break;
                            }
                        }
                    }
                }
                if (selectedNode == null)
                {
                    SquidMaker.DisplayErrorMessage("the global texture with UID \"" + _SelectedFont.UID + "\" wasn't found in the tree");
                }
            }
            else if (_SelectedFont.TextureScope == TextureScope.Local)
            {
                if (_ShowLocalFonts == true)
                {
                    SquidMaker.DisplayErrorMessage("The texture reference's scope is set to Local but the ShowLocalFonts option is set to false");
                }
                else
                {
                    foreach (TreeNode node in treeViewTextures.Nodes[2].Nodes)
                    {
                        // if it's an untagged texture
                        if (node.ImageIndex == 1)
                        {
                            if (node.Text == _SelectedFont.UID)
                            {
                                selectedNode = node;
                                break;
                            }
                        }
                        // check the tagged textures
                        else
                        {
                            foreach (TreeNode subNode in node.Nodes)
                            {
                                if (subNode.Text == _SelectedFont.UID)
                                {
                                    selectedNode = subNode;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (selectedNode == null)
                {
                    SquidMaker.DisplayErrorMessage("the local texture with UID \"" + _SelectedFont.UID + "\" wasn't found in the tree");
                }
            }
            // select the node
            treeViewTextures.SelectedNode = selectedNode;
            */
        }
        
        private void SelectTexture(SquidFont material)
        {
            materialPreviewControl.MaterialType = typeof(SquidFont);
            _SelectedFont = material;
            materialPreviewControl.FontDisplay = material;          
        }

        private void treeViewTextures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // if it's a material
            if (e.Node.Tag is SquidFont)
            {                
                SelectTexture(e.Node.Tag as SquidFont);
            }
        }

        private void treeViewMaterials_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // if it's a material
            if (e.Node.Tag is SquidFont)
            {
                // direct close
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    } 
}