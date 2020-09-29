using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class RoundImage
    {
        //public readonly float radius;

        private readonly Texture2D texture;
        private readonly Vector2 origin;
        private readonly float scale;
        private readonly Color color;

        public RoundImage(string imageName, float radius, Color color)
        {
            //this.radius = radius;

            texture = C.Content.Load<Texture2D>(imageName);
            origin = new Vector2(texture.Width * .5f, texture.Height * .5f);
            scale = 2 * radius / texture.Width;
            this.color = color;
        }

        public RoundImage(string imageName, float radius)
            : this(imageName, radius, Color.White)
        { }

        public void Draw(Vector2 position, float rotation = 0)
        {
            C.SpriteBatch.Draw(texture, position, sourceRectangle: null, color, rotation, origin, scale, effects: SpriteEffects.None, layerDepth: 0);
        }
    }
}
