#if !XNATOUCH
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using System;
using System.Collections.Generic;
using System.Text;

namespace SquidEngine.Drawing
{
    public enum RenderTargetInstance
    {
        BackBuffer,
        A,
        B,
    }

    public class RenderTargetManager
    {
        #region Fields

        private GraphicsDevice _graphicsDevice;
        private static RenderTarget2D _renderTargetA;
        private static RenderTarget2D _renderTargetB;
        private static RenderTarget2D _backbuffer;

        #endregion

        #region Properties

        public GraphicsDevice GraphicsDevice
        {
            get { return _graphicsDevice; }
            set { _graphicsDevice = value; }
        }

        #endregion

        #region Constructor

        public RenderTargetManager()
        {

        }

        public RenderTargetManager(GraphicsDevice graphicsDevice)
        {
            this._graphicsDevice = graphicsDevice;
        }

        #endregion

        #region Methods

        public void LoadContent()
        {
            PresentationParameters pp = _graphicsDevice.PresentationParameters;
            SurfaceFormat surfaceFormat = pp.BackBufferFormat;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;
            _renderTargetA = new RenderTarget2D(_graphicsDevice, width,
                    height, false, surfaceFormat, DepthFormat.Depth24);
            _renderTargetB = new RenderTarget2D(_graphicsDevice, width,
                    height, false, surfaceFormat, DepthFormat.Depth24);
            _backbuffer = new RenderTarget2D(_graphicsDevice, width,
                    height, false, surfaceFormat, DepthFormat.Depth24);
        }

        public void UnloadContent()
        {
            _renderTargetA.Dispose();
            _renderTargetB.Dispose();
            _backbuffer.Dispose();
        }

        /// <summary>
        /// Recreates the necessary dependencies if the device was resized
        /// </summary>
        public void BackbufferResized(int newWidth, int newHeight)
        {
            if (_backbuffer != null && (_backbuffer.Width != newWidth
                || _backbuffer.Height != newHeight))
            {
                UnloadContent();
                LoadContent();
            }
        }

        public Texture2D GetTexture(RenderTargetInstance renderTarget)
        {
            if (renderTarget == RenderTargetInstance.A)
            {
                return _renderTargetA;
            }
            else if (renderTarget == RenderTargetInstance.B)
            {
                return _renderTargetB;
            }
            else
            {
                return _backbuffer;
            }
        }

        public void SwitchTo(RenderTargetInstance renderTarget)
        {
            if (renderTarget == RenderTargetInstance.A)
            {
                _graphicsDevice.SetRenderTarget(_renderTargetA);
            }
            else if (renderTarget == RenderTargetInstance.B)
            {
                _graphicsDevice.SetRenderTarget(_renderTargetB);
            }
            else
            {
                _graphicsDevice.SetRenderTarget(_backbuffer);
            }
        }

        #endregion

    }
}
#endif