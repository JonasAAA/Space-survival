// if don't want to draw to minimap first, take a look at RenderTarget2D constructor
// argument usage: RenderTargetUsage.PreserveContents
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Minimap
    {
        public float scale;
        private readonly RenderTarget2D minimapRendTarg;
        public int Width { get { return minimapRendTarg.Width; } }
        public int Height { get { return minimapRendTarg.Height; } }
        private readonly Rectangle destinRect;
        private readonly Camera camera;

        // needs to be drawn into before all other draws
        public Minimap(int width, int height, bool left, bool top, float scale)
        {
            this.scale = scale;
            minimapRendTarg = new RenderTarget2D(C.GraphicsDevice, width, height);
            int x = 0, y = 0;
            if (!left)
                x = C.screenWidth - Width;
            if (!top)
                y = C.screenHeight - Height;

            destinRect = new Rectangle(x, y, Width, Height);
            camera = new Camera(width, height, scale);
        }

        public void Update(Vector2 center, float rotation = 0)
        {
            camera.Update(center, rotation);
        }

        public void BeginDrawToMinimap(Color background)
        {
            C.GraphicsDevice.SetRenderTarget(minimapRendTarg);
            C.GraphicsDevice.Clear(color: background);
            camera.BeginDraw();
        }

        public void EndDrawToMinimap()
        {
            camera.EndDraw();
            C.GraphicsDevice.SetRenderTarget(null);
        }

        public void Draw()
        {
            C.SpriteBatch.Draw(minimapRendTarg, destinRect, Color.White);
        }
    }
}
