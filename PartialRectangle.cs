using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class PartialRectangle
    {
        private readonly float width, height;
        private readonly Color color, partialColor;
        private readonly Texture2D pixel;
        private readonly Vector2 origin;

        public PartialRectangle(float width, float height, Color color, Color partialColor)
        {
            this.width = width;
            this.height = height;
            this.color = color;
            this.partialColor = partialColor;
            pixel = new Texture2D(C.GraphicsDevice, width: 1, height: 1);
            Color[] colorData = { Color.White };
            pixel.SetData(colorData);
            origin = new Vector2(.5f, .5f);
        }

        public void Draw(Vector2 center, float partialRatio)
        {
            Vector2 scale = new Vector2(width, height);
            C.SpriteBatch.Draw(texture: pixel, position: center, sourceRectangle: null, color, rotation: 0, origin, scale, effects: SpriteEffects.None, layerDepth: 0);

            Vector2 partialCenter = center - new Vector2(width * (1 - partialRatio) * .5f, 0),
                partialScale = new Vector2(width * partialRatio, height);
            C.SpriteBatch.Draw(texture: pixel, position: partialCenter, sourceRectangle: null, partialColor, rotation: 0, origin, partialScale, effects: SpriteEffects.None, layerDepth: 0);
        }
    }
}
