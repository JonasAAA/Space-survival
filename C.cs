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
        public static readonly List<Bullet> newBullets;

        static C()
        {
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            newBullets = new List<Bullet>();
        }

        public static void Initialize(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            SpriteBatch = new SpriteBatch(graphicsDevice);
            C.Content = Content;
            GraphicsDevice = graphicsDevice;
        }

        public static float Angle(Vector2 vector2)
            => (float)Math.Atan2(vector2.Y, vector2.X) + MathHelper.PiOver2;
    }
}
