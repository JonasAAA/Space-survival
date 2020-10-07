using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Image
    {
        public readonly float width, height;

        private readonly Texture2D texture;
        private Vector2 origin;
        private readonly float scale;
        private readonly Color color;

        public Image(string imageName, float width, Color color)
        {
            texture = C.Content.Load<Texture2D>(imageName);
            origin = new Vector2(texture.Width * .5f, texture.Height * .5f);
            scale = width / texture.Width;
            this.width = width;
            height = scale * texture.Height;
            this.color = color;
        }

        public Image(string imageName, float width)
            : this(imageName, width, Color.White)
        { }

        // 0 means middle
        // -1 means min coord
        // 1 means max coord
        public void SetOrigin(int centerX, int centerY)
        {
            origin = new Vector2(texture.Width * (1 + centerX) * .5f, texture.Height * (1 - centerY) * .5f);
        }

        public void Draw(Vector2 position, float angle = 0)
        {
            C.SpriteBatch.Draw(texture, position, sourceRectangle: null, color, rotation: angle, origin, scale, effects: SpriteEffects.None, layerDepth: 0);
        }
    }
}
