#if XNATOUCH
using XnaTouch.Framework;
using XnaTouch.Framework.Audio;
using XnaTouch.Framework.Content;
using XnaTouch.Framework.GamerServices;
using XnaTouch.Framework.Graphics;
using XnaTouch.Framework.Input;
using XnaTouch.Framework.Media;
using XnaTouch.Framework.Net;
using XnaTouch.Framework.Storage;
using XnaGame = XnaTouch.Framework.Game;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XnaGame = Microsoft.Xna.Framework.Game;
#endif

using System;
using System.Collections.Generic;
using System.Text;
using SquidEngine.Drawing;
using SquidEngine.Components;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using SquidEngine.Input;
using SquidEngine.Debug;


namespace SquidEngine
{
    /// <summary>
    /// The main Game class
    /// </summary>
    public class Game : XnaGame
    {
        
        #region Fields

        private float _elapsed;
        public static Game Instance;
        protected GraphicsDeviceManager graphics;
        protected Audio.AudioManager _audioManager;

        protected Point _nativeResolution;
        protected Point _resolution; 
        protected bool enableVSync = true;

        #endregion
        
        #region Properties
        
        /// <summary>
        /// Gets the last Elapsed time since the last update.
        /// </summary>
        /// <remarks>This is updated before any SceneItems or Components are updated</remarks>
        public float Elapsed
        {
            get { return _elapsed; }
        }

        public Audio.AudioManager AudioManager
        {
            get { return _audioManager; }
            set { _audioManager = value; }
        }

        public Point NativeResolution
        {
            get { return _nativeResolution; }
            set { _nativeResolution = value; }
        }

        public Point Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }

        public Color DefaultClearColor
        {
            get;
            set;
        }

        #endregion
        
        #region Constructor

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferMultiSampling = true;
            Game.Instance = this;
            Components.Add(new GamerServicesComponent(this));
            _nativeResolution = new Point(1280, 720);
            _resolution = _nativeResolution;
            this.DefaultClearColor = Color.CornflowerBlue;       
        }

        #endregion
        
        #region Methods

        protected override void Initialize()
        {
            try
            {
                SquidProfiler.StartProfiling(SquidProfilerNames.GAME_INITIALIZE);
                graphics.PreferredBackBufferWidth = this.NativeResolution.X;
                graphics.PreferredBackBufferHeight = this.NativeResolution.Y;
                graphics.GraphicsDevice.PresentationParameters.BackBufferFormat = SurfaceFormat.Color;
                graphics.SynchronizeWithVerticalRetrace = true;
                graphics.ApplyChanges();
                string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                //ComponentTypeContainer.SetAppDomain(AppDomain.CurrentDomain);
                ComponentTypeContainer.LoadAssemblyInformation(_path);
                Drawing.DrawingManager.Initialize();               
                SquidCore.graphicsDevice = graphics.GraphicsDevice;
                base.Initialize();

                _audioManager = new Audio.AudioManager(this, "Content/SquidEngineAssets/Audio/SquidEngineAudio.xgs");
                
                SquidProfiler.StopProfiling(SquidProfilerNames.GAME_INITIALIZE);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                throw err;
            }
        }

        /// <summary>
        /// Change the resolution and reload the content, aswell as setup fullscreen and vsync
        /// </summary>
        /// <param name="x">Width</param>
        /// <param name="y">Height</param>
        public virtual void SetVideoResolution(int width, int height, bool isFullScreen, bool vsync)
        {
            Point backBufferSize = new Point(graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                graphics.GraphicsDevice.PresentationParameters.BackBufferHeight);
            if (backBufferSize.X != width || backBufferSize.Y != height || graphics.IsFullScreen != isFullScreen
                || graphics.SynchronizeWithVerticalRetrace != vsync)
            {
                graphics.PreferredBackBufferWidth = width;
                graphics.PreferredBackBufferHeight = height;
                graphics.SynchronizeWithVerticalRetrace = vsync;
                if (graphics.IsFullScreen != isFullScreen)
                {
                    graphics.ToggleFullScreen();
                }
                graphics.ApplyChanges();
                graphics.GraphicsDevice.Reset();
            }
        }

        protected override void LoadContent()
        {
            SquidProfiler.StartProfiling(SquidProfilerNames.GAME_LOADCONTENT);
            SquidCore._afterBatch = new SpriteBatch(GraphicsDevice);
            DrawingManager.LoadContent(GraphicsDevice);
            if (!File.Exists("Content/SquidEngineAssets/Global.squid"))
            {
                throw new Exception("The file global.squid does not exist\nPlease make sure it is included in your Content project.");
            }
            SceneManager.InitializeEmbedded(Services, true);
            SceneManager.LoadGlobalData("");
#if XNATOUCH
			Point backBufferSize = new Point(graphics.GraphicsDevice.Viewport.Width, 
                graphics.GraphicsDevice.Viewport.Height);
#else
            Point backBufferSize = new Point(graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, 
                graphics.GraphicsDevice.PresentationParameters.BackBufferHeight);
#endif
            this.NativeResolution = SceneManager.GlobalDataHolder.NativeResolution;
            if (this.NativeResolution != backBufferSize)
            {
                graphics.SynchronizeWithVerticalRetrace = enableVSync;
                graphics.PreferredBackBufferWidth = this.NativeResolution.X;
                graphics.PreferredBackBufferHeight = this.NativeResolution.Y;
                graphics.ApplyChanges();
                DrawingManager.LoadContent(GraphicsDevice);
                SceneManager.InitializeEmbedded(Services, true);
                SceneManager.LoadGlobalData("");
            }
            
            SceneManager.InitializeGlobal();
            
            SquidProfiler.StopProfiling(SquidProfilerNames.GAME_LOADCONTENT);
        }

        protected override void UnloadContent()
        {
            SquidProfiler.StartProfiling(SquidProfilerNames.GAME_UNLOADCONTENT);
            #if !XNATOUCH
            DrawingManager.RenderTargetManager.UnloadContent();
#endif

            SquidProfiler.StopProfiling(SquidProfilerNames.GAME_UNLOADCONTENT);
            #if(PROFILE)
            SquidProfiler.SaveProfile();
            #endif
        }

        protected override void Update(GameTime gameTime)
        {
        
            SquidCore.GameTime = gameTime;
            _elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;            
            if (SceneManager.ActiveScene != null && !SquidCore.activeSceneChanged)
            {                
                SquidCore.UpdateSquidEngine(_elapsed);                
            }
            else
            {
                SquidCore.activeSceneChanged = false;
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;            
            if (DrawingManager.IgnoreClearBeforeRendering == false)
            {
                this.GraphicsDevice.Clear(this.DefaultClearColor);
            }

            if (SceneManager.ActiveScene != null)
            {
                if (SceneManager.Scenes.Count > 1)
                {
                    for (int i = 0; i < SceneManager.Scenes.Count; i++)
                    {
                        SquidScene item = SceneManager.Scenes[i];

                        if (item != SceneManager.ActiveScene && item.WillRenderNotActive)
                            SquidCore.DrawSquidEngineScene(graphics.GraphicsDevice, _elapsed, item);
                    }
                }
                if (SceneManager.ActiveScene._hasBeenUpdatedOnce == true)
                {
                    SquidCore.DrawSquidEngineScene(graphics.GraphicsDevice, _elapsed, SceneManager.ActiveScene);
                }
                SquidCore.RenderSquidEngine();
            }

            //for (int y = 0; y < SceneManager.ActiveScene._spatialGrid.Rows; y++)
            //{
            //    for (int x = 0; x < SceneManager.ActiveScene._spatialGrid.Cols; x++)
            //    {
            //        Drawing.DebugShapes.DrawLine(
            //            new Vector2(x * SceneManager.ActiveScene._spatialGrid.CellSize,
            //                        0),
            //            new Vector2(x * SceneManager.ActiveScene._spatialGrid.CellSize, 
            //                        SceneManager.ActiveScene._spatialGrid.SceneHeight),
            //            Color.White);

            //    }
            //    Drawing.DebugShapes.DrawLine(
            //            new Vector2(0,
            //                y * SceneManager.ActiveScene._spatialGrid.CellSize),
            //            new Vector2(SceneManager.ActiveScene._spatialGrid.SceneWidth,
            //                y * SceneManager.ActiveScene._spatialGrid.CellSize),
            //            Color.White);
            //}


            

            base.Draw(gameTime);
        }

        #endregion

    }
}
