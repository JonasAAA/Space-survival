using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Camera
    {
        private Matrix transform;
        private readonly float scale;

        public Camera()
        {
            transform = Matrix.Identity;
            scale = 0.5f;
        }

        public void Update(Vector2 center, float rotation = 0)
        {
            transform = Matrix.CreateTranslation(-center.X, -center.Y, 0) * Matrix.CreateScale(scale) * Matrix.CreateRotationZ(-rotation) * Matrix.CreateTranslation(C.screenWidth * .5f, C.screenHeight * .5f, 0);
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
