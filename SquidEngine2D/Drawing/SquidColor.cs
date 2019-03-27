using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquidEngine.Drawing
{
    public class SquidColor
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public static implicit operator SquidColor(string s)
        {
            string temp = s.Replace("{R:", "").Replace("G:", "").Replace("B:", "").Replace("A:", "");
            temp = temp.TrimEnd('}');
            string[] ss = temp.Split(' ');
            return new SquidColor(byte.Parse(ss[0]), byte.Parse(ss[1]), byte.Parse(ss[2]), byte.Parse(ss[3]));
        }

        public SquidColor()
        {
            R = 0; G = 0; B = 0; A = 0;
        }

        public SquidColor(byte R, byte G, byte B, byte A)
        {
            this.R = R; this.G = G; this.B = B; this.A = A;
        }

        public Microsoft.Xna.Framework.Color GetXnaColor()
        {
            return new Microsoft.Xna.Framework.Color(R, G, B, A);
        }

        public override string ToString()
        {
            return String.Format("{{R:{0} G:{1} B:{2} A:{3}}}", R, G, B, A);
        }
    }
}
