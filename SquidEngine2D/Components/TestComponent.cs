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
using SquidEngine.Attributes;

namespace SquidEngine.Components
{
#if(DEBUG)
    public enum TestEnum
    {
        EnumItem1,
        EnumItem2,
        EnumItem3,
        EnumItem4,
    }

    [SquidComponentAttribute("TestComponent")]
    public class TestComponent:SquidComponent
    {
        #region Properties

        [SquidComponentProperty("Test Boolean", "true")]
        public bool TestBool
        {
            get;
            set;
        }

        [SquidComponentProperty("Test Integer", "0")]
        public int TestInt
        {
            get;
            set;
        }

        [SquidComponentProperty("Test Float", "0")]
        public float TestFloat
        {
            get;
            set;
        }

        [SquidComponentProperty("Test Vector2")]
        public Vector2 TestVector2
        {
            get;
            set;
        }

        [SquidComponentProperty("Test Point", "0")]
        public Point TestPoint
        {
            get;
            set;
        }

        [SquidComponentProperty("Test String", "test string :)")]
        public String TestString
        {
            get;
            set;
        }

        [SquidComponentProperty("Test Rectangle", "")]
        public Rectangle TestRectangle
        {
            get;
            set;
        }

        [SquidComponentProperty("Test Enum", "2")]
        public TestEnum TestEnum
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override void OnRegister()
        {
            Enabled = true;
        }

        public override void Update(float elapsedTime)
        {
            
        }

        #endregion
    }
#endif
}
