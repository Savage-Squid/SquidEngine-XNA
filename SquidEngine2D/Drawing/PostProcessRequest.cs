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
using SquidEngine.Drawing;

namespace SquidEngine.Drawing
{
    public struct PostProcessRequest
    {
        public SquidEffect SquidEffect;
        public SquidEffectParameters SquidEffectParameters;

        public PostProcessRequest(SquidEffect iceEffect)
        {
            this.SquidEffect = iceEffect;
            SquidEffectParameters = new SquidEffectParameters();
            SquidEffectParameters.Parameter1 = 1;
            SquidEffectParameters.Parameter2 = 1;
            SquidEffectParameters.Parameter3 = 1;
            SquidEffectParameters.Parameter4 = 1;
            SquidEffectParameters.Parameter5 = 1;
            SquidEffectParameters.Parameter6 = 1;
            SquidEffectParameters.Parameter7 = 1;
            SquidEffectParameters.Parameter8 = 1;
        }
    }
}
#endif