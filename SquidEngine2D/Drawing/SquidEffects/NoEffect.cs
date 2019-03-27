#if !XNATOUCH
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidEngine.Drawing.SquidEffects
{
    public class NoEffect : SquidEffect
    {
        public NoEffect()
            : base(AssetScope.Embedded,"NoEffect")
        {

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
        }

        public override void LoadParameters()
        {
            
        }

        public override void SetParameters(SquidEffectParameters parameters)
        {
            
        }
    }
}
#endif
