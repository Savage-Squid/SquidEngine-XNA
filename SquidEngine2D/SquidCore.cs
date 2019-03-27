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
#endif

using System;
using System.Collections.Generic;
using System.Text;
using SquidEngine.Drawing;
using SquidEngine.Components;
using SquidEngine.Debug;
using SquidEngine.SceneItems;

namespace SquidEngine
{
    
    public static class SquidCore
    {
        //NETWORK COMMENTED OUT TO PREVENT QUESTIONS WHILE IT GETS FINALIZED
        #region Fields

        public static GameTime GameTime;
        
        /*enum NetworkDataType
        {
            UpdateSceneItem,
            NewSceneItem
        }*/
        //static PacketReader packetReader=new PacketReader();
        //static PacketWriter packetWriter=new PacketWriter();
        internal static bool activeSceneChanged = false;
        internal static GraphicsDevice graphicsDevice;
        private static List<SceneItem> _itemsToDelete = new List<SceneItem>();
        internal static SpriteBatch _afterBatch=null;
        #endregion

        #region Methods
        /// <summary>
        /// Queues Up The Drawable Items to Draw to the screen
        /// </summary>
        /// <param name="device">The GraphicsDevice to use</param>
        /// <param name="elapsed">The elapsed gametime since the last draw</param>
        /// <param name="scene">The SquidScene to draw</param>
        public static void DrawSquidEngineScene(GraphicsDevice device, float elapsed, SquidScene scene)
        {
            SquidProfiler.StartProfiling(SquidProfilerNames.ICE_CORE_DRAW);            
            foreach (SceneItem _item in scene.SceneItems)
            {
                if (_item.IsTemplate == true)
                {
                    continue;
                }
                _item.Draw(elapsed);
            }
            SquidProfiler.StopProfiling(SquidProfilerNames.ICE_CORE_DRAW);
        }

        /// <summary>
        /// Renders the Queued Items to the Screen
        /// </summary>
        public static void RenderSquidEngine()
        {
            SquidProfiler.StartProfiling(SquidProfilerNames.ICE_CORE_RENDER);           
            DrawingManager.RenderScene();                            
            SquidProfiler.StopProfiling(SquidProfilerNames.ICE_CORE_RENDER);
        }

        /// <summary>
        /// Calls Update on all enabled SceneItems and their Corresponding Components along with the SceneComponents
        /// </summary>
        /// <param name="elapsed">The elapsed game time since the last Update</param>
        internal static void UpdateSquidEngine(float elapsed)
        {
            if (SceneManager.ActiveScene == null || !SceneManager.ActiveScene.Enabled)
            {
                return;
            }

            // Unload the scenes in the queue (this will prevent a flicker from happening when unloading
            // scenes during update
            if (SceneManager.ScenesToUnload.Count > 0)
            {
                foreach (SquidScene oldScene in SceneManager.ScenesToUnload)
                {
                    oldScene.Unload();
                    oldScene.ContentManager.Unload();
                    oldScene.ContentManager.Dispose();

                    SceneManager.Scenes.Remove(oldScene);
                    if (oldScene == SceneManager.ActiveScene)
                        SceneManager.ActiveScene = null;
                    if (SceneManager.ActiveScene == null && SceneManager.Scenes.Count > 0) //Pick the top scene
                        SceneManager.ActiveScene = SceneManager.Scenes[SceneManager.Scenes.Count - 1];
                }

                SceneManager.ScenesToUnload.Clear();
            }

            SquidProfiler.StartProfiling(SquidProfilerNames.ICE_CORE_MAIN_UPDATE);
            // Differentiate between being in Milkshake and a running game
            if (SceneManager.ActiveScene.isInGame == false)
            {
                SceneManager.ActiveScene.isInGame = true;
            }
            if (SceneManager.ActiveScene._hasBeenUpdatedOnce == false)
            {
                SceneManager.ActiveScene._hasBeenUpdatedOnce = true;
            }            
            SceneManager.ActiveScene.RemoveItems();
            SceneManager.ActiveScene.RegisterItems();
                        
            //Pump input
            Input.InputCore.Update(elapsed);
            
            //First update any scene components
            for (int i = 0; i < SceneManager.ActiveScene.SceneComponents.Count; i++)
            {
                SquidSceneComponent comp = SceneManager.ActiveScene.SceneComponents[i];
                if (comp.Enabled == false)
                {
                    continue;
                }
                comp.Update(elapsed);
                if (activeSceneChanged == true)
                {
                    SquidProfiler.StopProfiling(SquidProfilerNames.ICE_CORE_MAIN_UPDATE);
                    return;
                }
            }

            // Physics.CollisionManager.CheckAndProcessCollisions();
            // Update Spatial Data
            for (int i = 0; i < SceneManager.ActiveScene.SceneItems.Count; i++)
            {
                SceneItem _item = SceneManager.ActiveScene.SceneItems[i];
                if (_item.IsTemplate == true)
                {
                    continue;
                }
                _item.Update(elapsed);
            }

            for (int i = 0; i < SceneManager.ActiveScene.SceneItems.Count; i++)
            {
                SceneItem _item = SceneManager.ActiveScene.SceneItems[i];
                if (_item.IsTemplate == true)
                {
                    continue;
                }
                // Update all its components
                UpdateItemsComponents(_item);
                if (_item.MarkForDelete == true)
                {
                    _itemsToDelete.Add(_item);
                }
                if (activeSceneChanged == true)
                {
                    SquidProfiler.StopProfiling(SquidProfilerNames.ICE_CORE_MAIN_UPDATE);
                    return;
                }
            }

            for (int i = 0; i < SceneManager.ActiveScene.ActiveCameras.Count; i++)
            {
                SceneManager.ActiveScene.ActiveCameras[i].Update(elapsed);
            }

            //if(SceneManager.networkSession != null)
            //{
                //UpdateNetworkToSend(elapsed);
            //}

            SquidProfiler.StopProfiling(SquidProfilerNames.ICE_CORE_MAIN_UPDATE);
        }

        //private static void UpdateNetworkToSend(float elapsed)  
        //{
        //    NetworkSession session = SceneManager.networkSession;
        //    if (SceneManager.IsNetworkOwner)
        //    {
        //        //foreach (var item in SceneManager.ActiveScene.SceneItems)
        //        //{
        //        //    packetWriter.Write((int)NetworkDataType.UpdateSceneItem);
        //        //    packetWriter.Write(item.Position);
        //        //}
        //        //LocalNetworkGamer server = (LocalNetworkGamer)session.Host;
        //        //server.SendData(packetWriter, SendDataOptions.InOrder);  
        //    }
        //    session.Update();
        //}

        private static void UpdateItemsComponents(SceneItem item)
        {
            if (item.Components == null)
            {
                return;
            }
            for (int i = 0; i < item.Components.Count; i++)
            {
                SquidComponent _component = item.Components[i];
                if (_component.Enabled == true)
                {
                    _component.Update(SquidEngine.Game.Instance.Elapsed);
                }                
            }
        }

        #endregion
    }
}
