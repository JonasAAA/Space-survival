using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Camera
    {
        private Matrix transform;
        private readonly float scale;
        private readonly int width, height;

        public Camera(int width, int height, float scale)
        {
            transform = Matrix.Identity;
            this.width = width;
            this.height = height;
            this.scale = scale;
        }

        public void Update(Vector2 center, float rotation = 0)
        {
            transform = Matrix.CreateTranslation(-center.X, -center.Y, 0) * Matrix.CreateScale(scale) * Matrix.CreateRotationZ(-rotation) * Matrix.CreateTranslation(width * .5f, height * .5f, 0);
        }

        public void BeginDraw()
        {
            C.SpriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: null, depthStencilState: null, rasterizerState: null, effect: null, transform);
        }

        public void EndDraw()
        {
            C.SpriteBatch.End();
        }
    }
}
