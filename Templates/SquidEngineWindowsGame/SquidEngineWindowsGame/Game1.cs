using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using SquidEngine;

namespace $safeprojectname$
{
    
    public class Game1 : SquidEngine.Game
    {
        SquidScene scene;
        public Game1()
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
			scene = SceneManager.LoadScene("Content/SquidEngineScenes/TestScene.squidscene");
        }
        
    }

    #region Program

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }

    #endregion
}
