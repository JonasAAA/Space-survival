using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Game1
{
    public static class C
    {
        public static readonly int screenWidth, screenHeight;
        public static SpriteBatch SpriteBatch { get; private set; }
        public static ContentManager Content { get; private set; }
        public static GraphicsDevice GraphicsDevice { get; private set; }
        public static readonly List<RoundObject> newRoundObjects;

        static C()
        {
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            newRoundObjects = new List<RoundObject>();
        }

        public static void Initialize(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            SpriteBatch = new SpriteBatch(graphicsDevice);
            C.Content = Content;
            GraphicsDevice = graphicsDevice;
        }

        public static Vector2 Direction(float rotation)
            => new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
    }
}
