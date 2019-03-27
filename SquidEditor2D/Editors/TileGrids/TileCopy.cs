using System;
using System.Collections.Generic;
using System.Text;
using SquidEngine;
using SquidEngine.SceneItems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SquidEngine.SceneItems.TileGridClasses;

namespace SquidEditor.Editors.TileGrids
{
    public struct TileCopy
    {
        public Tile Tile
        {
            get;
            set;
        }
        public Point Displacement
        {
            get;
            set;
        }
    }
}
