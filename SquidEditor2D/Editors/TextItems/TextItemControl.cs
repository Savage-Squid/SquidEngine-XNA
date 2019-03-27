#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SquidEngine;
using SquidEngine.Drawing;
using SquidEditor.GraphicsDeviceControls;
using SquidEngine.SceneItems;

#endregion

namespace SquidEditor.Editors.TextItems
{

    public class TextItemEditorControl : GraphicsDeviceControl
    {
        private TextItem _textItem;
        private TextItemEditor _parent;
        private float _zoom;

        #region Properties 

        internal TextItemEditor ParentEditor
        {
            get { return _parent; }
            set { _parent = value; }
        }

        internal TextItem TextItem
        {
            get { return _textItem; }
            set { _textItem = value; }
        }
        
        internal bool ShowWholeImage { get; set; }
        
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        #endregion

        protected override void Initialize()
        {            
            _zoom = 1;
            this.AutoScroll = true;
            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        /// <summary>
        /// Draws the control, using SpriteBatch and SpriteFont.
        /// </summary>
        protected override void Draw()
        {            
            ParentEditor.ZoomBox.Camera.Update(1/60f);
            GraphicsDevice.Clear(Color.RoyalBlue);

            _textItem.Draw(1f);

            
            DrawingManager.ViewPortSize = new Point(this.Width, this.Height);
            SquidEditorForm.SwapCameraAndRenderScene(ParentEditor.ZoomBox.Camera);
            _parent.Update(1 / 60f);
        }

        public TextItemEditorControl()
        {
            _textItem = new TextItem();
        }

        /// <summary>
        /// Get the WndProc messages to prevent flickering when scrolling
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // WM_HSCROLL or WM_VSCROLL
            if (m.Msg == 0x114 || m.Msg == 0x115)
            {
                this.Invalidate();
            }
            base.WndProc(ref m);
        }
    }
}
